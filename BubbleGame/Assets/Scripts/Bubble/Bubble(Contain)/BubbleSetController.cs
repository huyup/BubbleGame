using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BubbleSetController : MonoBehaviour
{
    private GameObject bubble;
    private BubbleExplosionEffController bubbleExplosionEffController;
    
    // Use this for initialization
    void Start()
    {
        bubble = transform.Find("Bubble").gameObject;
        bubbleExplosionEffController = transform.Find("BubbleExplosion").gameObject.GetComponent<BubbleExplosionEffController>();
    }
    public void DestroyBubbleSet()
    {
        if (this.gameObject == null || bubble == null)
            return;

        bubbleExplosionEffController.SaveEffStartProperty(bubble.transform.localScale);

        Destroy(bubble);

        bubbleExplosionEffController.PlayExplosionEff(bubble.transform.position);

        Destroy(this.gameObject,1.5f);
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
