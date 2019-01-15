using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuadCtr : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bubble)
            transform.position = bubble.transform.position;
    }
}
