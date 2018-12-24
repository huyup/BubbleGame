using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
/// <summary>
/// TODO:ここを汎用クラスに修正
/// </summary>
public class BehaviorsCtr : MonoBehaviour
{
    [SerializeField]
    private List<BehaviorTree> behaviors = new List<BehaviorTree>();

    [SerializeField]
    private float dizzinessTime = 1.0f;

    [SerializeField]
    private GameObject dizzinessEff;

    [SerializeField] private InoshishiAnimatorCtr animatorCtr;

    [SerializeField] private ObjController controller;

    public void DisableBehaviors()
    {
        foreach (var behavior in behaviors)
        {
            behavior.StopAllCoroutines();
            behavior.enabled = false;
        }
    }

    public void RestartBehaviors()
    {
        foreach (var behavior in behaviors)
        {
            behavior.enabled = true;
        }

    }

    public void Dizziness()
    {
        dizzinessEff.GetComponent<ParticleSystem>().Play();
        DisableBehaviors();
        animatorCtr.ResetToDefaultState();
        StartCoroutine(DelayResumeBehavior());
    }
    IEnumerator DelayResumeBehavior()
    {
        yield return new WaitForSeconds(dizzinessTime);
        RestartBehaviors();
        dizzinessEff.GetComponent<ParticleSystem>().Stop();
        controller.SetObjState(ObjState.OnGround);
    }
}
