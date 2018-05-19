using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour {
	bool restarted;
	Vector3[] Waypoints;
	int i;

	// Use this for initialization
	void Start () {
		restarted = true;
		i = 1;
		Invoke ("putWaypoints", 3f);
	}

	void putWaypoints(){
		if (restarted) {
			Waypoints = GameObject.FindGameObjectWithTag ("manager").GetComponent<CityDesgin1> ().Waypoints;
			restarted = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = GameObject.Find ("Car(Clone)").transform.position + new Vector3 (0,5,0);
		if (Waypoints.Length > 0) {
			Vector3 CarToWaypoint = GameObject.Find ("Car(Clone)").transform.position - Waypoints [i];

			if (CarToWaypoint.magnitude < 10) {
				i += 1;
			}

			Vector3 direction = Waypoints [i] - Waypoints [i - 1];

			this.transform.forward = direction;
			print (Vector3.Angle (this.transform.forward, GameObject.Find ("Car(Clone)").transform.forward));
		}
	}
}
