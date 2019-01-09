﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoTriggerCtr : MonoBehaviour
{
    [SerializeField]
    private float minAngle = 10;

    private List<GameObject> bubbles = new List<GameObject>();

    public void TakeObjIn()
    {
        foreach (var bubble in bubbles)
        {
            if (!bubble)
                continue;

            var targetPositionXZ = bubble.transform.position - new Vector3(0, bubble.transform.position.y, 0);
            var selfPositionXZ = transform.position - new Vector3(0, transform.position.y, 0);

            var diff = targetPositionXZ - selfPositionXZ;

            var angle = Vector3.Angle(diff, transform.forward);
            if (angle <= minAngle)
            {
                bubble.GetComponent<BubbleController>().SetBubbleState(BubbleState.BeTakeIn);
                bubble.GetComponent<BubbleController>().SetTornadoPosition(transform.position);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 /*Bubble*/)
        {
            bubbles.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10 /*Bubble*/)
        {
            bubbles.Remove(other.gameObject);
        }
    }
}
