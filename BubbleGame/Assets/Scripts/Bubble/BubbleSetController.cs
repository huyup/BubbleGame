using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BubbleSetController : MonoBehaviour
{
    private GameObject bubble;
    private GameObject bubbleExplosion;
    private BubbleExplosionEffController bubbleExplosionEffCtr;
    
    // Use this for initialization
    void Start()
    {
        bubble = transform.Find("Bubble").gameObject;
        bubbleExplosion = transform.Find("BubbleExplosion").gameObject;
        bubbleExplosionEffCtr = bubbleExplosion.GetComponent<BubbleExplosionEffController>();
    }
    public void DestroyBubbleSet()
    {
        if (this.gameObject == null || bubble == null)
            return;

        bubbleExplosionEffCtr.SaveEffStartProperty(bubble.transform.localScale);

        Destroy(bubble);

        bubbleExplosionEffCtr.PlayExplosionEff(bubble.transform.position);
    }
}
