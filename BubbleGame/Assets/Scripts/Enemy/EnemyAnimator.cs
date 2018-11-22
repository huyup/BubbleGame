using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// XXX:継承に失敗したので、空にしておきます
/// </summary>
public class EnemyAnimator : MonoBehaviour {
    
    protected Vector3 prePositionXZ;

    void Start()
    {
        prePositionXZ = transform.position - new Vector3(0, transform.position.y, 0);
    }
    /// <summary>
    /// TODO:今　空になっている
    /// </summary>
    public virtual void SetMoveAnimatorParameter()
    {

    }
}
