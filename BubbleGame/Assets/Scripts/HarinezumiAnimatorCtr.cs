using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HarinezumiAnimatorCtr : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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

        //if (controller.ObjState != ObjState.OnGround)
        //{
        //    animator.SetBool("Moving", false);
        //    animator.SetBool("Attacking", false);
        //    animator.SetBool("Preparing", false);
        //    animator.SetBool("Stopping", false);
        //}
    }

    public void SetDownAnimation()
    {
        Debug.Log("Down");
        animator.SetBool("Down", true);
    }
}
