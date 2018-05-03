using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class move_the_car_plz : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//GameObject.FindGameObjectWithTag("Player").GetComponent<CarRemoteControl>().Acceleration = 1f;
		GameObject.Find("Car").GetComponent<CarRemoteControl>().Acceleration = 1f;
	}
}
