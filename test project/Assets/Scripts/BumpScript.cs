using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpScript : MonoBehaviour {

	public GameObject bump;

	// Use this for initialization
	void Start () {
		GameObject obj = (GameObject)Instantiate(bump,new Vector3(0.0f,0.0f,Random.Range(-28.0f,28.0f)),  Quaternion.Euler(new Vector3(0, 0, 90)));
		obj.SetActive(true);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
