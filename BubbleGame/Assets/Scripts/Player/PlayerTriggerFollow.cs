using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerFollow : MonoBehaviour {
    
	// Update is called once per frame
	void Update ()
	{
	    transform.position = transform.parent.position;
	}
}
