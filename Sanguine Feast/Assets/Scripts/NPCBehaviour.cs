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

    // Start is called before the first frame update
    void Start()
    {
        nAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
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
