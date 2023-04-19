using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDamageBehaviour : MonoBehaviour
{
    public float distance = 30f;
    public bool hitDetect = false;

    private GameObject player;

    Collider col;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        LayerMask mask = LayerMask.GetMask("Player");

        hitDetect = Physics.BoxCast(col.bounds.center, transform.localScale * 4, transform.forward, out hit, transform.rotation, distance, mask);

        if(GameObject.Find("GameController").GetComponent<GameController>().night == false)
        {
            if (!hitDetect)
            {
                if (player.GetComponent<PlayerHealth>().inShade == false)
                {
                    Debug.Log("hitting");
                    player.GetComponent<PlayerHealth>().weak = true;
                }
                else
                {
                    Debug.Log("not hitting");
                    player.GetComponent<PlayerHealth>().weak = false;
                }
            }
            else
            {
                Debug.Log("not hitting");
                player.GetComponent<PlayerHealth>().weak = false;
            }
        }
        else
        {
            return;
        }*/
    }
}
