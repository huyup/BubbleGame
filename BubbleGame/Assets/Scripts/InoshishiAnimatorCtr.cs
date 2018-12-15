using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InoshishiAnimatorCtr : MonoBehaviour
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
        //if (agent.velocity.magnitude > 0)
        //{
        //    animator.SetBool("IsDushing", true);
        //}
        //else
        //{
        //    animator.SetBool("IsDushing", false);
        //}

        //if (controller.ObjState != ObjState.OnGround)
        //{
        //    animator.SetBool("Moving", false);
        //    animator.SetBool("Attacking", false);
        //}
    }

    public void SetDownAnimation()
    {
        Debug.Log("Down");
    }
}
