using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollerBehaviour : MonoBehaviour
{
    private NavMeshAgent nma;
    private GameObject player;

    public Vector3[] patrolLoop;
    private int loopPos;
    public float detectRange;

    private bool distracted = false;
    private Vector3 playerPos;

    // Start is called before the first frame update
    void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
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
        if (nma.velocity.magnitude < 0.1f)
        {
            loopPos++;

            if (loopPos >= patrolLoop.Length)
            {
                loopPos = 0;
            }

            nma.destination = patrolLoop[loopPos];
        }
    }

    public void StartPatrol()
    {
        nma.destination = patrolLoop[0];
    }
}
