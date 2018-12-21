using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class UribouAttackActivator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SphereCollider sphereCollider;

    [SerializeField]
    private BehaviorTree attack;
    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("IsAttacking")||
            (bool)attack.GetVariable("IsAttacking").GetValue())
        {
            sphereCollider.enabled = true;
        }
        else
        {
            sphereCollider.enabled = false;
        }

    }
}
