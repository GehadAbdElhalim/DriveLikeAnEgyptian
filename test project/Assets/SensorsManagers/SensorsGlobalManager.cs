using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorsGlobalManager : MonoBehaviour {

	public static SensorsGlobalManager Instance { get; set; }

	public string id0;
	public string id10;
	public string id20;
	public string id30;
	public string id40;
	public string id50;
	public string id60;
	public string id70;
	public string id80;
	public string id90;
	public string id100;
	public string id110;
	public string id120;
	public string id130;
	public string id140;
	public string id150;
	public string id160;
	public string id170;
	public string id180;
	public string id190;
	public string id200;
	public string id210;
	public string id220;
	public string id230;
	public string id240;
	public string id250;
	public string id260;
	public string id270;
	public string id280;
	public string id290;
	public string id300;
	public string id310;
	public string id320;
	public string id330;
	public string id340;
	public string id350;

	public string type0;
	public string type10;
	public string type20;
	public string type30;
	public string type40;
	public string type50;
	public string type60;
	public string type70;
	public string type80;
	public string type90;
	public string type100;
	public string type110;
	public string type120;
	public string type130;
	public string type140;
	public string type150;
	public string type160;
	public string type170;
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

	public float distance0;
	public float distance10;
	public float distance20;
	public float distance30;
	public float distance40;
	public float distance50;
	public float distance60;
	public float distance70;
	public float distance80;
	public float distance90;
	public float distance100;
	public float distance110;
	public float distance120;
	public float distance130;
	public float distance140;
	public float distance150;
	public float distance160;
	public float distance170;
	public float distance180;
	public float distance190;
	public float distance200;
	public float distance210;
	public float distance220;
	public float distance230;
	public float distance240;
	public float distance250;
	public float distance260;
	public float distance270;
	public float distance280;
	public float distance290;
	public float distance300;
	public float distance310;
	public float distance320;
	public float distance330;
	public float distance340;
	public float distance350;


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
