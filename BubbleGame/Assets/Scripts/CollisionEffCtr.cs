using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEffCtr : MonoBehaviour
{
    [SerializeField]
    private float duration=1.5f;
	// Use this for initialization
	void Start ()
	{
	    Destroy(this.gameObject, duration);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
