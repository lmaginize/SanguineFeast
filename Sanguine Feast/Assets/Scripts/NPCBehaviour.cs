using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    NavMeshAgent nAgent;
    int sightRange = 25;

    // NavMesh sight components
    public GameObject target;
    private NavMeshHit hit;
    public bool blocked = false;

    public List<PatrollerBehaviour> killList;
    public GameObject goKill;

    public bool isHypnotised = false;
    public bool isTurned = false;

    // Start is called before the first frame update
    void Start()
    {
        nAgent = gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine("Hypnotised");
        StartCoroutine("Turned");

        killList = FindObjectOfType<PatrollerManager>().gameObject.GetComponent<PatrollerManager>().patrollers;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHypnotised == false && isTurned == false)
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
        }
    }

    public IEnumerator Hypnotised()
    {

        while (isHypnotised == true)
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

        while (isTurned == true)
        {
            if (targeting == false)
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
}
