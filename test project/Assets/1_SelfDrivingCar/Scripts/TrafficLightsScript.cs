using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightsScript : MonoBehaviour {

	int activeLight;
	float remainingTime;

	GameObject green_light;
	Light greenLightComp;
	GameObject red_light;
	Light redLightComp;

//	float remainingTime = 2.0f;
//
//	public float maxTime;
//	public float minTime;

	public enum ActiveLight{
		Red=0,
		Green = 1,
	};

	// Use this for initialization
	void Start () {
		green_light = GameObject.Find ("LampPost_A/green_light");
		greenLightComp = green_light.GetComponent<Light>();
		red_light = GameObject.Find ("LampPost_A/red_light");
		redLightComp = red_light.GetComponent<Light>();
		activeLight =  Random.Range (0, 2);
		remainingTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		remainingTime -= Time.deltaTime;
		// print(((activeLight == 0)?"red: ":"green: ")+remainingTime);
		if(remainingTime < 0.0f)
		{	
			remainingTime = Random.Range (15.0f, 90.0f);
			if(greenLightComp){
				//last active light was red, we need to change it and set the time
				if (activeLight == 0 ) {
					greenLightComp.intensity = 10;
					redLightComp.intensity = 0;
					activeLight = 1;
				} else {
					greenLightComp.intensity = 0;
					redLightComp.intensity = 10;
					activeLight = 0;
				}
			}
				
		}
	}
}
