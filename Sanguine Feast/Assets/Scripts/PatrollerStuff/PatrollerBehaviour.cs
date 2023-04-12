using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollerBehaviour : MonoBehaviour
{
    private NavMeshAgent nma;
    private GameObject player;

    public Route patrolLoop;
    private int loopPos;
    public float detectRange;

    private bool distracted = false;
    private Vector3 playerPos;
    private bool startPatrol;

    // Start is called before the first frame update
    void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");

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
        Collider[] arr = Physics.OverlapSphere(transform.position, detectRange, LayerMask.GetMask("Player"));

        for (int x = 0; x < arr.Length; x++)
        {
            if (arr[x].gameObject == player)
            {
                // Run the slot machine
            }
        }
    }

    void PatrolLoop()
    {
        if (Vector3.Distance(nma.destination, transform.position) <= 1.2f)
        {
            loopPos++;

            if (loopPos >= patrolLoop.route.Length)
            {
                loopPos = 0;
            }

            nma.destination = patrolLoop.route[loopPos].transform.position;
        }
    }

    public void StartPatrol()
    {
        startPatrol = true;
    }

    void PatrolStart()
    {
        nma.destination = patrolLoop.route[0].transform.position;
        startPatrol = false;
    }
}
