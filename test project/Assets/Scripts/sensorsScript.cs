using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

		print (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionStay(Collision collision)
	{
		print (collision.gameObject);
		print (this);
//		foreach (ContactPoint contact in collision.contacts)
//		{
//		}
	}
}
