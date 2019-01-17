using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleJudgeCollider : MonoBehaviour
{
    private GameObject bubble;

    public bool HadObjInside { get; private set; }


    [SerializeField]
    private float factorToCalScale = 1.3f;
    // Use this for initialization
    void Start()
    {
        HadObjInside = false;
        bubble = transform.parent.Find("Bubble").gameObject;
    }

    public void AddObjInside()
    {
        HadObjInside = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (bubble == null)
            return;

        transform.position = bubble.transform.position;
        transform.localScale = bubble.transform.localScale* factorToCalScale;

    }
}
