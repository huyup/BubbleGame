using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloatByDamage : MonoBehaviour
{
    [SerializeField]
    private Transform bubbleInstanceRef;

    [SerializeField]
    private Transform bubbleInstanceStartRef;

    [SerializeField]
    private float bubbleMaxSize;

    private Transform bubbleInstance;

    [SerializeField]
    private float increaseScaleVelocity;

    [SerializeField]
    private float factorToFloat;

    private bool canSetInitPosToBubble = true;

    public void CreateBubbleByDamage()
    {
        if (canSetInitPosToBubble)
        {
            CreateBubbleByDamageOnInit();
            canSetInitPosToBubble = false;
        }
        CreateBubbleByDamageOnUpdate();
    }

    private void CreateBubbleByDamageOnInit()
    {
        bubbleInstance = Instantiate(bubbleInstanceRef);
        bubbleInstance.position = bubbleInstanceStartRef.position;
    }

    private void CreateBubbleByDamageOnUpdate()
    {
        Vector3 scaleVelocity = new Vector3(increaseScaleVelocity, increaseScaleVelocity, increaseScaleVelocity) *
                                Time.fixedDeltaTime * 60;

        Vector3 upVelocity = Vector3.up * Time.fixedDeltaTime * 60 * factorToFloat;


        if (bubbleInstance.localScale.x < bubbleMaxSize)
        {
            bubbleInstance.localScale += scaleVelocity;
            bubbleInstance.GetComponent<Rigidbody>().velocity = upVelocity;
        }
        else
        {
            bubbleInstance.GetComponent<Rigidbody>().velocity = upVelocity * 10;
        }

    }

    public void Reset()
    {
        //TODO:ここリセットする
    }
}
