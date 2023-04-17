using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCreatedObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(decay());
    }

    IEnumerator decay()
    {
        yield return new WaitForSeconds(4f);
        float timeIncrease = Time.deltaTime;
        while(transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x-timeIncrease, transform.localScale.y - timeIncrease, transform.localScale.z - timeIncrease);
            yield return new WaitForSeconds(.1f);
            timeIncrease += .01f;
        }
        Destroy(gameObject);
    }
}
