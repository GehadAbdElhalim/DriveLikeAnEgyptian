using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CarController))]
public class sensorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionExit(Collision collide)
	{
		
	}

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
				SensorsGlobalManager.Instance.id0 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id10 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id20 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id30 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id40 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id50 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id60 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id70 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id80 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id90 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id100 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id110 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id120 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id130 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id140 = this.gameObject.GetInstanceID ();
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

			case "line150":
				SensorsGlobalManager.Instance.id150 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id160 = this.gameObject.GetInstanceID ();
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
				SensorsGlobalManager.Instance.id170 = this.gameObject.GetInstanceID ();
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

			case "line180":
				SensorsGlobalManager.Instance.id180 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type180 = nearest_type;
				SensorsGlobalManager.Instance.position180 = nearest_position;
				break;

			case "line190":
				SensorsGlobalManager.Instance.id190 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type190 = nearest_type;
				SensorsGlobalManager.Instance.position190 = nearest_position;
				break;

			case "line200":
				SensorsGlobalManager.Instance.id200 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type200 = nearest_type;
				SensorsGlobalManager.Instance.position200 = nearest_position;
				break;

			case "line210":
				SensorsGlobalManager.Instance.id210 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type210 = nearest_type;
				SensorsGlobalManager.Instance.position210 = nearest_position;
				break;

			case "line220":
				SensorsGlobalManager.Instance.id220 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type220 = nearest_type;
				SensorsGlobalManager.Instance.position220 = nearest_position;
				break;

			case "line230":
				SensorsGlobalManager.Instance.id230 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type230 = nearest_type;
				SensorsGlobalManager.Instance.position230 = nearest_position;
				break;

			case "line240":
				SensorsGlobalManager.Instance.id240 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type240 = nearest_type;
				SensorsGlobalManager.Instance.position240 = nearest_position;
				break;

			case "line250":
				SensorsGlobalManager.Instance.id250 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type250 = nearest_type;
				SensorsGlobalManager.Instance.position250 = nearest_position;
				break;

			case "line260":
				SensorsGlobalManager.Instance.id260 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type260 = nearest_type;
				SensorsGlobalManager.Instance.position260 = nearest_position;
				break;

			case "line270":
				SensorsGlobalManager.Instance.id270 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type270 = nearest_type;
				SensorsGlobalManager.Instance.position270 = nearest_position;
				break;

			case "line280":
				SensorsGlobalManager.Instance.id280 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type280 = nearest_type;
				SensorsGlobalManager.Instance.position280 = nearest_position;
				break;

			case "line290":
				SensorsGlobalManager.Instance.id290 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type290 = nearest_type;
				SensorsGlobalManager.Instance.position290 = nearest_position;
				break;

			case "line300":
				SensorsGlobalManager.Instance.id300 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type300 = nearest_type;
				SensorsGlobalManager.Instance.position300 = nearest_position;
				break;

			case "line310":
				SensorsGlobalManager.Instance.id310 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type310 = nearest_type;
				SensorsGlobalManager.Instance.position310 = nearest_position;
				break;

			case "line320":
				SensorsGlobalManager.Instance.id320 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type320 = nearest_type;
				SensorsGlobalManager.Instance.position320 = nearest_position;
				break;

			case "line330":
				SensorsGlobalManager.Instance.id330 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type330 = nearest_type;
				SensorsGlobalManager.Instance.position330 = nearest_position;
				break;

			case "line340":
				SensorsGlobalManager.Instance.id340 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type340 = nearest_type;
				SensorsGlobalManager.Instance.position340 = nearest_position;
				break;

			case "line350":
				SensorsGlobalManager.Instance.id350 = this.gameObject.GetInstanceID ();
				SensorsGlobalManager.Instance.type350 = nearest_type;
				SensorsGlobalManager.Instance.position350 = nearest_position;
				break;

			}
			//Output the name of the GameObject you collide with
			Debug.Log ("Sensor " + this.gameObject.name + " hit the GameObject : " + collide.gameObject.name);
		}
	}

}
