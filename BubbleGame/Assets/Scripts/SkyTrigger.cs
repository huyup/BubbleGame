using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //if (collision.transform.root.GetComponent<BubbleSetController>())
        //    collision.transform.root.GetComponent<BubbleSetController>().DestoryNow();
    }
}
