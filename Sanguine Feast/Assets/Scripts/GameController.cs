using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public bool night;
    public GameObject im;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(im);
    }
}
