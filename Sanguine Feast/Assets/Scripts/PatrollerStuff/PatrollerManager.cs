using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerManager : MonoBehaviour
{
    public GameObject patrollerPrefab;

    public List<Route> patrolMarkers;
    public List<PatrollerBehaviour> patrollers;

    public bool anotherOne = false;

    private void Awake()
    {
        for (int x = 0; x < patrollers.Count; x++)
        {
            patrollers[x].patrolLoop = new Vector3[patrolMarkers[x].route.Length];

            for (int y = 0; y < patrolMarkers[x].route.Length; y++)
            {
                patrollers[x].patrolLoop[y] = patrolMarkers[x].route[y].transform.position;
            }
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
        patrollers[patrollers.Count - 1].patrolLoop = new Vector3[patrolMarkers[patrollers.Count - 1].route.Length];

        for (int x = 0; x < patrolMarkers[patrollers.Count - 1].route.Length; x++)
        {
            patrollers[patrollers.Count - 1].patrolLoop[x] = patrolMarkers[patrollers.Count - 1].route[0].transform.position;
        }
    }

    void AddRoute(GameObject[] markers)
    {
        patrolMarkers.Add(new Route(markers));
    }
}
