using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleRotation : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 0.15f;
    // Update is called once per frame
    void Update()
    {
        //ランダム軸から回転する
        Vector3 randomAxis = new Vector3(Time.fixedDeltaTime, Time.fixedDeltaTime, Time.fixedDeltaTime);
        float speed = rotateSpeed * Time.fixedDeltaTime;
        Quaternion rot = Quaternion.AngleAxis(speed, randomAxis);
        transform.rotation = rot * transform.rotation;
    }
}
