using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRescueTriggerCtr : MonoBehaviour
{

    public bool IsPlayerInResucePoint { get; private set; }

    public GameObject DeadPlayer { get; private set; }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name=="RescueTrigger" /*Player*/)
        {
            if (other.transform.parent.GetComponent<PlayerController>())
            {
                if (other.transform.parent.GetComponent<PlayerController>().IsDead)
                {
                    IsPlayerInResucePoint = true;
                    DeadPlayer = other.transform.parent.gameObject;
                }
            }
        }
    }
}
