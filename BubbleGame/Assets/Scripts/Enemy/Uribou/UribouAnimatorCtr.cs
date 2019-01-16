using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class UribouAnimatorCtr : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    [SerializeField]
    private ObjController controller;

    private bool isRotating;

    [SerializeField] private BehaviorTree attack;
    private void Start()
    {
        isRotating = false;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isRotating = (bool)attack.GetVariable("IsRotating").GetValue();
        

        if (isRotating)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            if (agent.velocity.magnitude > 0.01f)
            {
                animator.SetBool("Moving", true);
            }
            else if (agent.velocity.magnitude < 0.01f)
            {
                animator.SetBool("Moving", false);
            }
        }


        if (controller.ObjState != ObjState.OnGround)
        {
            animator.SetBool("Moving", false);
            animator.SetBool("Attacking", false);
            animator.SetBool("Preparing", false);
            animator.SetBool("Stopping", false);
        }
    }
}
