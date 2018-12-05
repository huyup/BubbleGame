using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    bool delayOnce = true;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (delayOnce)
        {
            StartCoroutine(DelayFunction());
            delayOnce = false;
        }

    }

    IEnumerator DelayFunction()
    {
        yield return new WaitForSeconds(1.5f);

        Debug.Log("DelayFunc");
    }
}
