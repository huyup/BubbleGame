using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class BossBehaviorCtr : MonoBehaviour
{
    [SerializeField]
    private List<BehaviorTree> behaviors = new List<BehaviorTree>();

    [SerializeField]
    private float dizzinessTime = 1.0f;

    [SerializeField]
    private GameObject dizzinessEff;

    [SerializeField] private ObjController controller;

    public void DisableBehaviors()
    {
        foreach (var behavior in behaviors)
        {
            behavior.DisableBehavior();
        }
    }
    public void Dizziness()
    {
        dizzinessEff.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DelayResumeBehavior());
    }
    IEnumerator DelayResumeBehavior()
    {
        yield return new WaitForSeconds(dizzinessTime);
        dizzinessEff.GetComponent<ParticleSystem>().Stop();
        controller.SetObjState(ObjState.OnGround);
    }
}
