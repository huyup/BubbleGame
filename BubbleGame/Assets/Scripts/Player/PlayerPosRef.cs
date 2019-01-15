using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosRef : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private float playerInitY;
    private void Start()
    {
        playerInitY = player.transform.position.y;
        transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
    }

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, playerInitY, player.transform.position.z);
    }
}
