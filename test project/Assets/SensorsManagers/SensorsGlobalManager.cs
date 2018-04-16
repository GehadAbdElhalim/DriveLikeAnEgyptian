using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorsGlobalManager : MonoBehaviour {

	public static SensorsGlobalManager Instance { get; set; }

	public int id0;
	public int id10;
	public int id20;
	public int id30;
	public int id40;
	public int id50;
	public int id60;
	public int id70;
	public int id80;
	public int id90;
	public int id100;
	public int id110;
	public int id120;
	public int id130;
	public int id140;
	public int id150;
	public int id160;
	public int id170;
	public int id180;
	public int id190;
	public int id200;
	public int id210;
	public int id220;
	public int id230;
	public int id240;
	public int id250;
	public int id260;
	public int id270;
	public int id280;
	public int id290;
	public int id300;
	public int id310;
	public int id320;
	public int id330;
	public int id340;
	public int id350;

	public string type0;
//	public string type5;
	public string type10;
//	public string type15;
	public string type20;
//	public string type25;
	public string type30;
//	public string type35;
	public string type40;
//	public string type45;
	public string type50;
//	public string type55;
	public string type60;
//	public string type65;
	public string type70;
//	public string type75;
	public string type80;
//	public string type85;
	public string type90;
//	public string type95;
	public string type100;
//	public string type105;
	public string type110;
//	public string type115;
	public string type120;
//	public string type125;
	public string type130;
//	public string type135;
	public string type140;
//	public string type145;
	public string type150;
//	public string type155;
	public string type160;
//	public string type165;
	public string type170;
//	public string type175;
	public string type180;
	public string type190;
	public string type200;
	public string type210;
	public string type220;
	public string type230;
	public string type240;
	public string type250;
	public string type260;
	public string type270;
	public string type280;
	public string type290;
	public string type300;
	public string type310;
	public string type320;
	public string type330;
	public string type340;
	public string type350;

	public Vector3 position0;
//	public Vector3 position5;
	public Vector3 position10;
//	public Vector3 position15;
	public Vector3 position20;
//	public Vector3 position25;
	public Vector3 position30;
//	public Vector3 position35;
	public Vector3 position40;
//	public Vector3 position45;
	public Vector3 position50;
//	public Vector3 position55;
	public Vector3 position60;
//	public Vector3 position65;
	public Vector3 position70;
//	public Vector3 position75;
	public Vector3 position80;
//	public Vector3 position85;
	public Vector3 position90;
//	public Vector3 position95;
	public Vector3 position100;
//	public Vector3 position105;
	public Vector3 position110;
//	public Vector3 position115;
	public Vector3 position120;
//	public Vector3 position125;
	public Vector3 position130;
//	public Vector3 position135;
	public Vector3 position140;
//	public Vector3 position145;
	public Vector3 position150;
//	public Vector3 position155;
	public Vector3 position160;
//	public Vector3 position165;
	public Vector3 position170;
//	public Vector3 position175;
	public Vector3 position180;
	public Vector3 position190;
	public Vector3 position200;
	public Vector3 position210;
	public Vector3 position220;
	public Vector3 position230;
	public Vector3 position240;
	public Vector3 position250;
	public Vector3 position260;
	public Vector3 position270;
	public Vector3 position280;
	public Vector3 position290;
	public Vector3 position300;
	public Vector3 position310;
	public Vector3 position320;
	public Vector3 position330;
	public Vector3 position340;
	public Vector3 position350;


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
