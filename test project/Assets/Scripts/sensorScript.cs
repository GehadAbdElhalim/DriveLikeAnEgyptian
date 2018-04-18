using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CarController))]
public class sensorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

//		print (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	void OnCollisionStay(Collision collide)
//	{
//		//Output the name of the GameObject you collide with
//		Debug.Log("Sensor "+this.gameObject.name+" hit the GameObject : " + collide.gameObject.name);
//	}
	void OnCollisionEnter(Collision collide)
	{
		Vector3 carPosition = GameObject.Find ("Car").transform.position;
		print (carPosition);

		string nearest_type=null;
		Vector3 nearest_position=new Vector3(0,0,0);
		float shortest_distance = float.MaxValue;

		float current_distance;

		foreach (ContactPoint contact in collide.contacts) {
			print (contact.point);
			current_distance = Vector3.Distance (GameObject.Find ("Car").transform.position, contact.point);
			if (current_distance < shortest_distance) {
				shortest_distance = current_distance;
				nearest_position = contact.point;
				nearest_type = contact.otherCollider.name;
			}
		}

		if (collide.gameObject.name != "Car") {
			
			switch (this.gameObject.name) {
			case "line0":
				if (SensorsGlobalManager.Instance.type0 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position0);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type0;
						nearest_position = SensorsGlobalManager.Instance.position0;
					}
				}
				SensorsGlobalManager.Instance.type0 = nearest_type;
				SensorsGlobalManager.Instance.position0 = nearest_position;
				break;

//			case "line5":
//				if (SensorsGlobalManager.Instance.type5 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position5);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type5;
//						nearest_position = SensorsGlobalManager.Instance.position5;
//					}
//				}
//				SensorsGlobalManager.Instance.type5 = nearest_type;
//				SensorsGlobalManager.Instance.position5 = nearest_position;
//				break;

			case "line10":
				if (SensorsGlobalManager.Instance.type10 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position10);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type10;
						nearest_position = SensorsGlobalManager.Instance.position10;
					}
				}
				SensorsGlobalManager.Instance.type10 = nearest_type;
				SensorsGlobalManager.Instance.position10 = nearest_position;
				break;

//			case "line15":
//				if (SensorsGlobalManager.Instance.type15 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position15);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type15;
//						nearest_position = SensorsGlobalManager.Instance.position15;
//					}
//				}
//				SensorsGlobalManager.Instance.type15 = nearest_type;
//				SensorsGlobalManager.Instance.position15 = nearest_position;
//				break;

			case "line20":
				if (SensorsGlobalManager.Instance.type20 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position20);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type20;
						nearest_position = SensorsGlobalManager.Instance.position20;
					}
				}
				SensorsGlobalManager.Instance.type20 = nearest_type;
				SensorsGlobalManager.Instance.position20 = nearest_position;
				break;

//			case "line25":
//				if (SensorsGlobalManager.Instance.type25 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position25);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type25;
//						nearest_position = SensorsGlobalManager.Instance.position25;
//					}
//				}
//				SensorsGlobalManager.Instance.type25 = nearest_type;
//				SensorsGlobalManager.Instance.position25 = nearest_position;
//				break;

			case "line30":
				if (SensorsGlobalManager.Instance.type30 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position30);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type30;
						nearest_position = SensorsGlobalManager.Instance.position30;
					}
				}
				SensorsGlobalManager.Instance.type30 = nearest_type;
				SensorsGlobalManager.Instance.position30 = nearest_position;
				break;

//			case "line35":
//				if (SensorsGlobalManager.Instance.type35 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position35);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type35;
//						nearest_position = SensorsGlobalManager.Instance.position35;
//					}
//				}
//				SensorsGlobalManager.Instance.type35 = nearest_type;
//				SensorsGlobalManager.Instance.position35 = nearest_position;
//				break;

			case "line40":
				if (SensorsGlobalManager.Instance.type40 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position40);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type40;
						nearest_position = SensorsGlobalManager.Instance.position40;
					}
				}
				SensorsGlobalManager.Instance.type40 = nearest_type;
				SensorsGlobalManager.Instance.position40 = nearest_position;
				break;

//			case "line45":
//				if (SensorsGlobalManager.Instance.type45 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position45);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type45;
//						nearest_position = SensorsGlobalManager.Instance.position45;
//					}
//				}
//				SensorsGlobalManager.Instance.type45 = nearest_type;
//				SensorsGlobalManager.Instance.position45 = nearest_position;
//				break;

			case "line50":
				if (SensorsGlobalManager.Instance.type50 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position50);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type50;
						nearest_position = SensorsGlobalManager.Instance.position50;
					}
				}
				SensorsGlobalManager.Instance.type50 = nearest_type;
				SensorsGlobalManager.Instance.position50 = nearest_position;
				break;

//			case "line55":
//				if (SensorsGlobalManager.Instance.type55 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position55);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type55;
//						nearest_position = SensorsGlobalManager.Instance.position55;
//					}
//				}
//				SensorsGlobalManager.Instance.type55 = nearest_type;
//				SensorsGlobalManager.Instance.position55 = nearest_position;
//				break;

			case "line60":
				if (SensorsGlobalManager.Instance.type60 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position60);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type60;
						nearest_position = SensorsGlobalManager.Instance.position60;
					}
				}
				SensorsGlobalManager.Instance.type60 = nearest_type;
				SensorsGlobalManager.Instance.position60 = nearest_position;
				break;

//			case "line65":
//				if (SensorsGlobalManager.Instance.type65 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position65);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type65;
//						nearest_position = SensorsGlobalManager.Instance.position65;
//					}
//				}
//				SensorsGlobalManager.Instance.type65 = nearest_type;
//				SensorsGlobalManager.Instance.position65 = nearest_position;
//				break;

			case "line70":
				if (SensorsGlobalManager.Instance.type70 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position70);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type70;
						nearest_position = SensorsGlobalManager.Instance.position70;
					}
				}
				SensorsGlobalManager.Instance.type70 = nearest_type;
				SensorsGlobalManager.Instance.position70 = nearest_position;
				break;

//			case "line75":
//				if (SensorsGlobalManager.Instance.type75 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position75);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type75;
//						nearest_position = SensorsGlobalManager.Instance.position75;
//					}
//				}
//				SensorsGlobalManager.Instance.type75 = nearest_type;
//				SensorsGlobalManager.Instance.position75 = nearest_position;
//				break;

			case "line80":
				if (SensorsGlobalManager.Instance.type80 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position80);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type80;
						nearest_position = SensorsGlobalManager.Instance.position80;
					}
				}
				SensorsGlobalManager.Instance.type80 = nearest_type;
				SensorsGlobalManager.Instance.position80 = nearest_position;
				break;

//			case "line85":
//				if (SensorsGlobalManager.Instance.type85 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position85);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type85;
//						nearest_position = SensorsGlobalManager.Instance.position85;
//					}
//				}
//				SensorsGlobalManager.Instance.type85 = nearest_type;
//				SensorsGlobalManager.Instance.position85 = nearest_position;
//				break;

			case "line90":
				if (SensorsGlobalManager.Instance.type90 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position90);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type90;
						nearest_position = SensorsGlobalManager.Instance.position90;
					}
				}
				SensorsGlobalManager.Instance.type90 = nearest_type;
				SensorsGlobalManager.Instance.position90 = nearest_position;
				break;

//			case "line95":
//				if (SensorsGlobalManager.Instance.type95 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position95);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type95;
//						nearest_position = SensorsGlobalManager.Instance.position95;
//					}
//				}
//				SensorsGlobalManager.Instance.type95 = nearest_type;
//				SensorsGlobalManager.Instance.position95 = nearest_position;
//				break;

			case "line100":
				if (SensorsGlobalManager.Instance.type100 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position100);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type100;
						nearest_position = SensorsGlobalManager.Instance.position100;
					}
				}
				SensorsGlobalManager.Instance.type100 = nearest_type;
				SensorsGlobalManager.Instance.position100 = nearest_position;
				break;

//			case "line105":
//				if (SensorsGlobalManager.Instance.type105 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position105);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type105;
//						nearest_position = SensorsGlobalManager.Instance.position105;
//					}
//				}
//				SensorsGlobalManager.Instance.type105 = nearest_type;
//				SensorsGlobalManager.Instance.position105 = nearest_position;
//				break;

			case "line110":
				if (SensorsGlobalManager.Instance.type110 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position110);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type110;
						nearest_position = SensorsGlobalManager.Instance.position110;
					}
				}
				SensorsGlobalManager.Instance.type110 = nearest_type;
				SensorsGlobalManager.Instance.position110 = nearest_position;
				break;

//			case "line115":
//				if (SensorsGlobalManager.Instance.type115 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position115);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type115;
//						nearest_position = SensorsGlobalManager.Instance.position115;
//					}
//				}
//				SensorsGlobalManager.Instance.type115 = nearest_type;
//				SensorsGlobalManager.Instance.position115 = nearest_position;
//				break;

			case "line120":
				if (SensorsGlobalManager.Instance.type120 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position120);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type120;
						nearest_position = SensorsGlobalManager.Instance.position120;
					}
				}
				SensorsGlobalManager.Instance.type120 = nearest_type;
				SensorsGlobalManager.Instance.position120 = nearest_position;
				break;

//			case "line125":
//				if (SensorsGlobalManager.Instance.type125 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position125);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type125;
//						nearest_position = SensorsGlobalManager.Instance.position125;
//					}
//				}
//				SensorsGlobalManager.Instance.type125 = nearest_type;
//				SensorsGlobalManager.Instance.position125 = nearest_position;
//				break;

			case "line130":
				if (SensorsGlobalManager.Instance.type130 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position130);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type130;
						nearest_position = SensorsGlobalManager.Instance.position130;
					}
				}
				SensorsGlobalManager.Instance.type130 = nearest_type;
				SensorsGlobalManager.Instance.position130 = nearest_position;
				break;

//			case "line135":
//				if (SensorsGlobalManager.Instance.type135 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position135);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type135;
//						nearest_position = SensorsGlobalManager.Instance.position135;
//					}
//				}
//				SensorsGlobalManager.Instance.type135 = nearest_type;
//				SensorsGlobalManager.Instance.position135 = nearest_position;
//				break;

			case "line140":
				if (SensorsGlobalManager.Instance.type140 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position140);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type140;
						nearest_position = SensorsGlobalManager.Instance.position140;
					}
				}
				SensorsGlobalManager.Instance.type140 = nearest_type;
				SensorsGlobalManager.Instance.position140 = nearest_position;
				break;

//			case "line145":
//				if (SensorsGlobalManager.Instance.type145 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position145);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type145;
//						nearest_position = SensorsGlobalManager.Instance.position145;
//					}
//				}
//				SensorsGlobalManager.Instance.type145 = nearest_type;
//				SensorsGlobalManager.Instance.position145 = nearest_position;
//				break;
//
			case "line150":
				if (SensorsGlobalManager.Instance.type150 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position150);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type150;
						nearest_position = SensorsGlobalManager.Instance.position150;
					}
				}
				SensorsGlobalManager.Instance.type150 = nearest_type;
				SensorsGlobalManager.Instance.position150 = nearest_position;
				break;

//			case "line155":
//				if (SensorsGlobalManager.Instance.type155 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position155);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type155;
//						nearest_position = SensorsGlobalManager.Instance.position155;
//					}
//				}
//				SensorsGlobalManager.Instance.type155 = nearest_type;
//				SensorsGlobalManager.Instance.position155 = nearest_position;
//				break;

			case "line160":
				if (SensorsGlobalManager.Instance.type160 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position160);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type160;
						nearest_position = SensorsGlobalManager.Instance.position160;
					}
				}
				SensorsGlobalManager.Instance.type160 = nearest_type;
				SensorsGlobalManager.Instance.position160 = nearest_position;
				break;

//			case "line165":
//				if (SensorsGlobalManager.Instance.type165 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position165);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type165;
//						nearest_position = SensorsGlobalManager.Instance.position165;
//					}
//				}
//				SensorsGlobalManager.Instance.type165 = nearest_type;
//				SensorsGlobalManager.Instance.position165 = nearest_position;
//				break;

			case "line170":
				if (SensorsGlobalManager.Instance.type170 != null) {
					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position170);
					if (current_distance < shortest_distance) {
						nearest_type = SensorsGlobalManager.Instance.type170;
						nearest_position = SensorsGlobalManager.Instance.position170;
					}
				}
				SensorsGlobalManager.Instance.type170 = nearest_type;
				SensorsGlobalManager.Instance.position170 = nearest_position;
				break;

//			case "line175":
//				if (SensorsGlobalManager.Instance.type175 != null) {
//					current_distance = Vector3.Distance(GameObject.Find ("Car").transform.position, SensorsGlobalManager.Instance.position175);
//					if (current_distance < shortest_distance) {
//						nearest_type = SensorsGlobalManager.Instance.type175;
//						nearest_position = SensorsGlobalManager.Instance.position175;
//					}
//				}
//				SensorsGlobalManager.Instance.type175 = nearest_type;
//				SensorsGlobalManager.Instance.position175 = nearest_position;
//				break;

			}
			//Output the name of the GameObject you collide with
			Debug.Log ("Sensor " + this.gameObject.name + " hit the GameObject : " + collide.gameObject.name);
		}
	}

}
