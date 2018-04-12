using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

//		print (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionStay(Collision collision)
	{
		Debug.Log("Sensor "+this.gameObject.name+" hit the GameObject : " + collision.gameObject.name);
	}
}
