using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

//		print (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	void OnCollisionStay(Collision collide)
//	{
//		//Output the name of the GameObject you collide with
//		Debug.Log("Sensor "+this.gameObject.name+" hit the GameObject : " + collide.gameObject.name);
//	}
	void OnCollisionEnter(Collision collide)
	{
		//Output the name of the GameObject you collide with
		Debug.Log("Sensor "+this.gameObject.name+" hit the GameObject : " + collide.gameObject.name);
	}

}
