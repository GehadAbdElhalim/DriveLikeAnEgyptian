using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = GameObject.Find("1").transform.Find("Cube (4)").transform.position - GameObject.Find("2").transform.position; 
		GameObject.Find ("Cartest").transform.forward = direction;
		//GameObject.Find ("Car").GetComponent<Rigidbody> ().velocity.z;
		Debug.Log(Vector3.Angle(GameObject.Find("Car").transform.forward,direction));
	}
}
