using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleJudgeColliderFollow : MonoBehaviour
{
    private GameObject bubble;
    // Use this for initialization
    void Start()
    {
        bubble = transform.parent.Find("Bubble").gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (bubble == null)
            return;

        transform.position = bubble.transform.position;
        transform.localScale = bubble.transform.localScale;

    }
}
