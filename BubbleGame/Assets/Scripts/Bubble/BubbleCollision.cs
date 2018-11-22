using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollision : MonoBehaviour
{
    BubbleSetController setController;

    public bool CanBeDestroy { get; private set; }
    // Use this for initialization
    void Start()
    {
        CanBeDestroy = false;
        setController = transform.parent.GetComponent<BubbleSetController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player"&& CanBeDestroy)
        {
            //Vector3 direction = collision.transform.forward + Vector3.down * 0.5f;
            //GetComponent<Rigidbody>().AddForce(direction * 5, ForceMode.VelocityChange);
            //Invoke("DestroyBubble", 1.6f);
            setController.DestoryNow();
        }
        if (collision.gameObject.tag == "Ground" && CanBeDestroy)
        {
            setController.DestoryNow();
        }
    }

    private void DestroyBubble()
    {
        setController.DestoryNow();
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player" && CanBeDestroy)
    //    {
    //        setController.DestoryNow();
    //    }
    //}
    public void SetDestroyFlag()
    {
        CanBeDestroy = true;
    }
}
