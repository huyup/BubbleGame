using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UribouAnimaotrCtr : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
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
    }
}
