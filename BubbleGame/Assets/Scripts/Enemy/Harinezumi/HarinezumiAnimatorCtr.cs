using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class HarinezumiAnimatorCtr : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    private ObjController controller;

    [SerializeField]
    private BehaviorTree attack;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        controller = GetComponent<ObjController>();
        
    }

    private void Update()
    {

        if (agent.velocity.magnitude > 0||(bool)attack.GetVariable("IsRotating").GetValue())
        {
            animator.SetBool("Moving", true);
        }
        else if (agent.velocity.magnitude <= 0 && !(bool)attack.GetVariable("IsRotating").GetValue())
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
