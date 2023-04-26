using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    NavMeshAgent nAgent;
    HealthBehaviour hb;
    int sightRange = 25;

    // NavMesh sight components
    public GameObject target;
    private NavMeshHit hit;
    public bool blocked = false;

    public bool isWandering = false;
    public bool isWalking = false;

    public List<PatrollerBehaviour> killList;
    public GameObject goKill;
    public Vector3 lastPos;

    public bool isHypnotised = false;
    public bool isTurned = false;
    public float stunTime;
    public bool isStunned = false;
    public bool locked = false;

    Vector3 des;
    private void Awake()
    {
        nAgent = gameObject.GetComponent<NavMeshAgent>();
        hb = GetComponent<HealthBehaviour>();
        StartCoroutine("Hypnotised");
        StartCoroutine("Turned");

        killList = FindObjectOfType<PatrollerManager>().gameObject.GetComponent<PatrollerManager>().patrollers;
        nAgent.speed = 2f;
        target = GameObject.Find("Player");
    }


    // Update is called once per frame
    void Update()
    {
        if (!isStunned && !locked)
        {
            if (!isHypnotised && !isTurned)
            {
                // Checks if player is not blocked by buildings
                blocked = NavMesh.Raycast(transform.position, target.transform.position, out hit, NavMesh.AllAreas);
                Debug.DrawLine(transform.position, target.transform.position, blocked ? Color.red : Color.green);


                if (!blocked && Vector3.Distance(target.transform.position, transform.position) < sightRange)
                {
                    Debug.Log("see");
                }
                else
                {
                    Debug.Log("not see");
                }


                if (!isWandering)
                {
                    StartCoroutine(Wander());
                }

                if (!isWalking)
                {
                    nAgent.destination = des;
                }
            }
        }
    }

    public IEnumerator Wander()
    {
        while (!isHypnotised && !isTurned)
        {
            isWandering = true;
            isWalking = true;
            yield return new WaitForSeconds(Random.Range(6, 9));
            isWalking = false;
            yield return new WaitForSeconds(1);

            des = new Vector3(Random.Range(transform.position.x + 15, transform.position.x - 15), transform.position.y, Random.Range(transform.position.z + 15, transform.position.z - 15));

            yield return new WaitForSeconds(2);

            isWandering = false;
        }
    }

    public IEnumerator Hypnotised()
    {
        
        while (isHypnotised)
        {

            Vector3 destination = target.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(destination, out hit, 0, 1);
            nAgent.destination = hit.position;

        }

        yield return null;
    }

    public IEnumerator Turned()
    {
        bool targeting = false;

        while (isTurned)
        {
            if (!targeting)
            {

                float closestPos = 300;

                foreach (PatrollerBehaviour pb in killList)
                {

                    float pos;

                    pos = Vector3.Distance(transform.position, pb.transform.position);

                    if (pos < closestPos)
                    {
                        closestPos = pos;
                        goKill = pb.transform.gameObject;
                    }
                }

                targeting = true;
            }

            Vector3 destination = goKill.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(destination, out hit, 0, 1);
            nAgent.destination = hit.position;

            if (goKill.gameObject == null)
            {
                targeting = false;
            }

        }

        yield return null;
    }

    public void StunNPC()
    {
        print("Stunned");
        isStunned = true;
        lastPos = nAgent.destination;
        nAgent.isStopped = true;

        StartCoroutine("UnStun");
    }

    IEnumerator UnStun()
    {
        yield return new WaitForSeconds(stunTime);
        print("unstun");
        isStunned = false;

        while (locked)
        {
            yield return null;
        }

        if (!locked)
        {
            nAgent.isStopped = false;
            nAgent.destination = lastPos;
            hb.UnStun();
        }

        yield return null;
    }

    public void Lock()
    {
        locked = true;
        nAgent.isStopped = true;
    }
}
