using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorsGlobalManager : MonoBehaviour {

	public static SensorsGlobalManager Instance { get; set; }

	public string type0;
	public string type5;
	public string type10;
	public string type15;
	public string type20;
	public string type25;
	public string type30;
	public string type35;
	public string type40;
	public string type45;
	public string type50;
	public string type55;
	public string type60;
	public string type65;
	public string type70;
	public string type75;
	public string type80;
	public string type85;
	public string type90;
	public string type95;
	public string type100;
	public string type105;
	public string type110;
	public string type115;
	public string type120;
	public string type125;
	public string type130;
	public string type135;
	public string type140;
	public string type145;
	public string type150;
	public string type155;
	public string type160;
	public string type165;
	public string type170;
	public string type175;

	public Vector3 position0;
	public Vector3 position5;
	public Vector3 position10;
	public Vector3 position15;
	public Vector3 position20;
	public Vector3 position25;
	public Vector3 position30;
	public Vector3 position35;
	public Vector3 position40;
	public Vector3 position45;
	public Vector3 position50;
	public Vector3 position55;
	public Vector3 position60;
	public Vector3 position65;
	public Vector3 position70;
	public Vector3 position75;
	public Vector3 position80;
	public Vector3 position85;
	public Vector3 position90;
	public Vector3 position95;
	public Vector3 position100;
	public Vector3 position105;
	public Vector3 position110;
	public Vector3 position115;
	public Vector3 position120;
	public Vector3 position125;
	public Vector3 position130;
	public Vector3 position135;
	public Vector3 position140;
	public Vector3 position145;
	public Vector3 position150;
	public Vector3 position155;
	public Vector3 position160;
	public Vector3 position165;
	public Vector3 position170;
	public Vector3 position175;


	public void Awake(){
		if (Instance == null) {
			Instance = this;		
			DontDestroyOnLoad (Instance);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
