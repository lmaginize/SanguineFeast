using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeBehaviour : MonoBehaviour
{
    public float distance = 30f;
    public bool hitDetect = false;
    public bool playerInShade = false;

    Collider col;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.up) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        hitDetect = Physics.BoxCast(col.bounds.center, transform.localScale, transform.up, out hit, transform.rotation, distance);
        if (hitDetect && hit.collider.tag == "Player")
        {
            playerInShade = true;
        }
        else
        {
            playerInShade = false;
        }
    }
}
