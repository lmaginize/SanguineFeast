using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatrollerBehaviour : MonoBehaviour
{
    private NavMeshAgent nma;
    private GameController gc;
    private GameObject player;
    private GameObject eyes;
    private BloodSucking bs;
    private weirdBattle wb;
    private AttackBehaviour ab;
    private PatrollerManager pm;

    public Route patrolLoop;
    private int loopPos;
    public float detectRange;
    public float detectAngle;

    public bool canShoot = false;
    private bool distracted = false;
    private bool playerSeen;
    private bool playerHeard;
    private Vector3 checkSpot = new Vector3(0, -100, 0);
    public bool aggression;
    private Vector3 playerPos;
    private bool startPatrol;

    public float catchChance;
    public float dayMultiplier;

    public List<GameObject> beenThere;
    public float wanderChance;
    public float wanderDetectRange;
    public float wanderRadius;
    public float wanderTime;
    public bool canWander;
    private bool wandered;

    // Start is called before the first frame update
    void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        wb = gc.gameObject.GetComponent<weirdBattle>();
        player = GameObject.Find("Player");
        bs = player.GetComponent<BloodSucking>();
        eyes = transform.GetChild(0).gameObject;
        ab = GetComponent<AttackBehaviour>();
        pm = GameObject.Find("PatrollerManager").GetComponent<PatrollerManager>();

        if (startPatrol)
        {
            PatrolStart();
        }
    }

    private void Update()
    {
        if (startPatrol)
        {
            PatrolStart();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ListenForPlayer();
        TheEyes();

        RaycastHit hit;

        if (playerHeard && playerSeen && !aggression)
        {
            WokeUpAndChoseAnger();
        }
        else if (playerHeard)
        {
            if (Vector3.Angle(transform.forward, transform.position - player.transform.position) < detectAngle && Physics.Raycast(transform.position, transform.position - player.transform.position, out hit))
            {
                if (hit.collider.gameObject == player)
                {
                    playerSeen = true;
                }
            }
        }

        if (aggression == true && Vector3.Distance(player.transform.position, gameObject.transform.position) < detectRange)
        {
            FightingWords();
        }
        else
        {
            PatrolLoop();
        }

        Wander();
    }

    void TheEyes()
    {
        RaycastHit hit;

        if (Vector3.Angle(player.transform.position - eyes.transform.position, eyes.transform.forward) <= detectAngle && Physics.Raycast(eyes.transform.position, player.transform.position - eyes.transform.position, out hit, detectRange))
        {
            if (hit.collider.gameObject == player)
            {
                playerSeen = true;
            }
        }
    }

    void FightingWords()
    {
        transform.LookAt(transform.position - Vector3.ProjectOnPlane(transform.position - player.transform.position, Vector3.up));

        if (Vector3.Distance(player.transform.position, transform.position) > .5f)
        {
            nma.destination = player.transform.position;
        }
    }

    void PatrolLoop()
    {
        if (Vector3.Distance(nma.destination, transform.position) <= 1.2f)
        {
            if (!wandered)
            {
                loopPos++;
                canWander = true;

                if (loopPos >= patrolLoop.route.Length)
                {
                    loopPos = 0;
                }
            }
            else
            {
                wandered = false;
            }

            nma.destination = patrolLoop.route[loopPos].transform.position;
        }
    }

    public void StartPatrol()
    {
        startPatrol = true;

        StartCoroutine("Wander");
    }

    void PatrolStart()
    {
        nma.destination = patrolLoop.route[0].transform.position;
        startPatrol = false;
    }

    void ListenForPlayer()
    {
        Collider[] arr = Physics.OverlapSphere(transform.position, detectRange, LayerMask.GetMask("Player"));

        for (int x = 0; x < arr.Length; x++)
        {
            if (arr[x].gameObject == player)
            {
                if (pm.bloodSucking)
                {
                    playerHeard = true;
                    checkSpot = player.transform.position;
                }
            }
            else
            {
                playerHeard = false;

            }
        }

        if (arr.Length == 0)
        {
            playerHeard = false;
        }
    }

    IEnumerator Wander()
    {
        while (true)
        {
            if (canWander && !playerHeard && !playerSeen)
            {
                if (Random.Range(1f, 100f) < wanderChance)
                {
                    Collider[] arr = Physics.OverlapSphere(transform.position, wanderDetectRange, LayerMask.GetMask("WanderPoints"));
                    print(arr.Length);

                    if (arr.Length > 0)
                    {
                        Vector3 destination = (Random.insideUnitSphere * wanderRadius) + arr[0].gameObject.transform.position;
                        NavMeshHit hit;
                        NavMesh.SamplePosition(destination, out hit, wanderRadius, 1);
                        nma.destination = hit.position;
                        canWander = false;
                        wandered = true;
                    }
                }
            }

            yield return new WaitForSeconds(wanderTime);
        }
    }

    public void WokeUpAndChoseAnger()
    {
        print("anger");
        aggression = true;

        StartCoroutine("Anger");
    }

    IEnumerator Anger()
    {
        while (aggression)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= ab.reach[0])
            {
                nma.isStopped = true;
                ab.attack[0] = true;
            }
            else
            {
                nma.isStopped = false;
                ab.attack[0] = false;
            }

            yield return null;
        }
    }
}
