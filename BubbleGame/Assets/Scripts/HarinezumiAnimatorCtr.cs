using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HarinezumiAnimatorCtr : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    private ObjController controller;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        controller = GetComponent<ObjController>();
    }

    private void Update()
    {

        if (agent.velocity.magnitude > 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        if (controller.ObjState != ObjState.OnGround)
        {
            animator.SetBool("Moving", false);
            animator.SetBool("Attacking", false);
        }
    }

    public void SetDownAnimation()
    {
        Debug.Log("Down");
    }
}
