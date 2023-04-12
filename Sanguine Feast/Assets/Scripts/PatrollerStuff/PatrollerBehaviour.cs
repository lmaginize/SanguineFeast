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
    private BloodSucking bs;
    private weirdBattle wb;

    public Route patrolLoop;
    private int loopPos;
    public float detectRange;

    private bool distracted = false;
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

        PatrolLoop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckForPlayer();

        Wander();
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

    void TakingChances()
    {
        if (Random.Range(1f, 100f) <= catchChance * (gc.night ? 1 : dayMultiplier))
        {
            wb.battleBegin();
        }
    }

    void CheckForPlayer()
    {
        Collider[] arr = Physics.OverlapSphere(transform.position, detectRange, LayerMask.GetMask("Player"));

        for (int x = 0; x < arr.Length; x++)
        {
            if (arr[x].gameObject == player)
            {
                bs.yesButton.onClick.AddListener(TakingChances);
            }
            else
            {
                bs.yesButton.onClick.RemoveListener(TakingChances);
            }
        }

        if (arr.Length == 0)
        {
            bs.yesButton.onClick.RemoveListener(TakingChances);
        }
    }

    IEnumerator Wander()
    {
        while (true)
        {
            if (canWander)
            {
                print("Test");
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
}
