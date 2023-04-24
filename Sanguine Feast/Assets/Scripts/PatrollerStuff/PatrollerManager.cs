using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerManager : MonoBehaviour
{
    public GameObject patrollerPrefab;

    public List<Route> patrolMarkers;
    public List<PatrollerBehaviour> patrollers;

    public bool anotherOne = false;
    public bool bloodSucking;

    private void Awake()
    {
        for (int x = 0; x < patrollers.Count; x++)
        {
            patrollers[x].patrolLoop = patrolMarkers[x];

            patrollers[x].StartPatrol();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (anotherOne)
        {
            PlusOne();

            anotherOne = false;
        }
    }

    void PlusOne()
    {
        patrollers.Add(Instantiate(patrollerPrefab, patrolMarkers[patrollers.Count - 1].route[0].transform.position, Quaternion.identity).GetComponent<PatrollerBehaviour>());
        patrollers[patrollers.Count - 1].patrolLoop = patrolMarkers[patrollers.Count - 1];
        patrollers[patrollers.Count - 1].StartPatrol();
    }

    public void AddRoute(GameObject[] markers)
    {
        patrolMarkers.Add(new Route(markers));
    }
}
