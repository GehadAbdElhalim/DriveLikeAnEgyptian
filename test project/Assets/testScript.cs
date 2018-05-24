using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour {
	bool restarted;
	Vector3[] Waypoints;
	int i;
	public float DirectionAngle;

	// Use this for initialization
	void Start () {
		restarted = true;
		i = 1;
		Invoke ("putWaypoints", 1f);
	}

	void putWaypoints(){
		DirectionAngle = 0;
		Waypoints = GameObject.FindGameObjectWithTag ("manager").GetComponent<CityDesgin1> ().Waypoints;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = GameObject.Find ("Car(Clone)").transform.position + new Vector3 (0,5,0);
		if (Waypoints.Length > 0 && i < Waypoints.Length) {
			Vector3 CarToWaypoint = GameObject.Find ("Car(Clone)").transform.position - Waypoints [i];

			if (CarToWaypoint.magnitude < 10) {
				i += 1;
			}

			Vector3 direction = Waypoints [i] - Waypoints [i - 1];

			this.transform.forward = direction;
			//print (Vector3.Angle (this.transform.forward, GameObject.Find ("Car(Clone)").transform.forward));
			DirectionAngle = Vector3.Angle (this.transform.forward, GameObject.Find ("Car(Clone)").transform.forward);
		}
	}
}
