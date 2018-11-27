using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleRotation : MonoBehaviour
{
    private BubbleProperty property;

    // Use this for initialization
    void Start()
    {
        property = GetComponent<BubbleProperty>();
    }

    // Update is called once per frame
    void Update()
    {
        //ランダム軸から回転する
        Vector3 randomAxis = new Vector3(Time.fixedDeltaTime, Time.fixedDeltaTime, Time.fixedDeltaTime);
        float speed = property.RotateSpeed * Time.fixedDeltaTime;
        Quaternion rot = Quaternion.AngleAxis(speed, randomAxis);
        transform.rotation = rot * transform.rotation;
    }
}
