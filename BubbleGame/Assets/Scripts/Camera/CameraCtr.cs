using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtr : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> players = new List<GameObject>();

    [SerializeField]
    private float distanceToFocusPointY = 8.5f;

    [SerializeField]
    private float distanceToFocusPointZ = -10f;

    public void OnUpdate()
    {
        Vector3 positionSum = Vector3.zero;

        for (int i = 0; i < players.Count; i++)
        {
            positionSum += players[i].transform.position;
        }

        Vector3 centerPos = positionSum / players.Count;

        transform.position = new Vector3(centerPos.x,
            distanceToFocusPointY,
            centerPos.z + distanceToFocusPointZ);
    }
}
