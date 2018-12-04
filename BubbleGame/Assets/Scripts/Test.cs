using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private BoxCollider collider;
	// Use this for initialization
	void Start ()
	{
	    collider = GetComponent<BoxCollider>();

	}
	
	// Update is called once per frame
	void Update ()
	{
	    collider.size += Vector3.one;
	}
}
