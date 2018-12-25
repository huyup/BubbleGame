using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 10 /*Bubble*/)
        {
            if (_other.GetComponent<BubbleCollision>())
            {
                _other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

                _other.GetComponent<BubbleCollision>().SetInsideObjDestroyable();
                BubbleProperty bubbleProperty = _other.GetComponent<BubbleProperty>();
                BubbleSetController bubbleSetController = _other.transform.parent.GetComponent<BubbleSetController>();
                StartCoroutine(DelayDestroy(bubbleProperty.LastTime, bubbleSetController));
            }
        }
    }
    IEnumerator DelayDestroy(float _waitTime, BubbleSetController _bubbleSetController)
    {
        if (!_bubbleSetController)
            yield break;
        yield return new WaitForSeconds(_waitTime);
        if (!_bubbleSetController)
            yield break;
        _bubbleSetController.DestroyBubbleSet();
    }
}
