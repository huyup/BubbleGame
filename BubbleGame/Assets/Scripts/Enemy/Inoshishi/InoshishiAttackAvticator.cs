using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class InoshishiAttackAvticator : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private CapsuleCollider capsuleCollider;

    [SerializeField]
    private BehaviorTree attack;

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("IsDushing") )
        {
            capsuleCollider.enabled = true;
        }
        else
        {
            capsuleCollider.enabled = false;
        }

    }
}
