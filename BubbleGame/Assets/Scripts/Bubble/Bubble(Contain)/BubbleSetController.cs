using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BubbleSetController : MonoBehaviour
{
    private GameObject bubble;
    private GameObject bubbleCollider;
    private BubbleExplosionEffController bubbleExplosionEffController;

    // Use this for initialization
    void Start()
    {

        if (transform.Find("BubbleCollider"))
        {
            bubbleCollider = transform.Find("BubbleCollider").gameObject;
        }

        bubble = transform.Find("Bubble").gameObject;
        bubbleExplosionEffController = transform.Find("BubbleExplosion").gameObject.GetComponent<BubbleExplosionEffController>();
    }
    public void DestroyBubbleSet()
    {
        if (this.gameObject == null || bubble == null)
            return;

        bubbleExplosionEffController.SaveEffStartProperty(bubble.transform.localScale);
        if (bubbleCollider)
            Destroy(bubbleCollider);
        Destroy(bubble);

        bubbleExplosionEffController.PlayExplosionEff(bubble.transform.position);

        Destroy(this.gameObject, 1.5f);
    }
    public void DestroyBubbleItemSet()
    {
        if (this.gameObject == null || bubble == null)
            return;

        Destroy(bubble);
        bubbleExplosionEffController.PlayExplosionEff(bubble.transform.position);
        Destroy(this.gameObject, 1.5f);
    }
}
