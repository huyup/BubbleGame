using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BubbleSetController : MonoBehaviour
{
    private GameObject bubble;
    private GameObject bubbleExplosion;
    private BubbleExplosionEffController bubbleExplosionEffController;
    
    // Use this for initialization
    void Start()
    {
        bubble = transform.Find("Bubble").gameObject;
        bubbleExplosion = transform.Find("BubbleExplosion").gameObject;
        bubbleExplosionEffController = bubbleExplosion.GetComponent<BubbleExplosionEffController>();
    }
    public void DestroyBubbleSet()
    {
        if (this.gameObject == null || bubble == null)
            return;

        bubbleExplosionEffController.SaveEffStartProperty(bubble.transform.localScale);

        Destroy(bubble);

        bubbleExplosionEffController.PlayExplosionEff(bubble.transform.position);
    }
}
