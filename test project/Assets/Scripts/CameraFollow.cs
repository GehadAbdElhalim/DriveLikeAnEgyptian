using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	Transform target;

	void LateUpdate(){
		try {
			target = GameObject.FindWithTag ("CarPlayer").transform;
			transform.position = new Vector3 (target.position.x, transform.position.y, target.position.z);
		} catch(System.Exception e){
			Debug.LogException(e, this);
		}
	}

	// Use this for initialization
	void Start () {
//		target = GameObject.Find ("Car(Clone)").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
