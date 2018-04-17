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
		int id;
		foreach (ContactPoint contact in collide.contacts) {
//			current_distance = Vector3.Distance (GameObject.Find ("Car").transform.position, contact.point);
//			if (current_distance < shortest_distance) {
//				shortest_distance = current_distance;
//				nearest_position = contact.point;
//				nearest_type = contact.otherCollider.name;
			id = contact.otherCollider.GetInstanceID ();
			if (collide.gameObject.name != "Car") {

				switch (this.gameObject.name) {
				case "line0":
					if (id == SensorsGlobalManager.Instance.id0) {
						SensorsGlobalManager.Instance.id0 = -1;
						SensorsGlobalManager.Instance.type0 = null;
						SensorsGlobalManager.Instance.position0 = Vector3.zero;
					}
					break;

				case "line10":
					if (id == SensorsGlobalManager.Instance.id10) {
						SensorsGlobalManager.Instance.id10 = -1;
						SensorsGlobalManager.Instance.type10 = null;
						SensorsGlobalManager.Instance.position10 = Vector3.zero;
					}
					break;

				case "line20":
					if (id == SensorsGlobalManager.Instance.id20) {
						SensorsGlobalManager.Instance.id20 = -1;
						SensorsGlobalManager.Instance.type20 = null;
						SensorsGlobalManager.Instance.position20 = Vector3.zero;
					}
					break;

				case "line30":
					if (id == SensorsGlobalManager.Instance.id30) {
						SensorsGlobalManager.Instance.id30 = -1;
						SensorsGlobalManager.Instance.type30 = null;
						SensorsGlobalManager.Instance.position30 = Vector3.zero;
					}
					break;

				case "line40":
					if (id == SensorsGlobalManager.Instance.id40) {
						SensorsGlobalManager.Instance.id40 = -1;
						SensorsGlobalManager.Instance.type40 = null;
						SensorsGlobalManager.Instance.position40 = Vector3.zero;
					}
					break;

				case "line50":
					if (id == SensorsGlobalManager.Instance.id50) {
						SensorsGlobalManager.Instance.id50 = -1;
						SensorsGlobalManager.Instance.type50 = null;
						SensorsGlobalManager.Instance.position50 = Vector3.zero;
					}
					break;

				case "line60":
					if (id == SensorsGlobalManager.Instance.id60) {
						SensorsGlobalManager.Instance.id60 = -1;
						SensorsGlobalManager.Instance.type60 = null;
						SensorsGlobalManager.Instance.position60 = Vector3.zero;
					}
					break;

				case "line70":
					if (id == SensorsGlobalManager.Instance.id70) {
						SensorsGlobalManager.Instance.id70 = -1;
						SensorsGlobalManager.Instance.type70 = null;
						SensorsGlobalManager.Instance.position70 = Vector3.zero;
					}
					break;

				case "line80":
					if (id == SensorsGlobalManager.Instance.id80) {
						SensorsGlobalManager.Instance.id80 = -1;
						SensorsGlobalManager.Instance.type80 = null;
						SensorsGlobalManager.Instance.position80 = Vector3.zero;
					}
					break;

				case "line90":
					if (id == SensorsGlobalManager.Instance.id90) {
						SensorsGlobalManager.Instance.id90 = -1;
						SensorsGlobalManager.Instance.type90 = null;
						SensorsGlobalManager.Instance.position90 = Vector3.zero;
					}
					break;

				case "line100":
					if (id == SensorsGlobalManager.Instance.id100) {
						SensorsGlobalManager.Instance.id100 = -1;
						SensorsGlobalManager.Instance.type100 = null;
						SensorsGlobalManager.Instance.position100 = Vector3.zero;
					}
					break;

				case "line110":
					if (id == SensorsGlobalManager.Instance.id110) {
						SensorsGlobalManager.Instance.id110 = -1;
						SensorsGlobalManager.Instance.type110 = null;
						SensorsGlobalManager.Instance.position110 = Vector3.zero;
					}
					break;

				case "line120":
					if (id == SensorsGlobalManager.Instance.id120) {
						SensorsGlobalManager.Instance.id120 = -1;
						SensorsGlobalManager.Instance.type120 = null;
						SensorsGlobalManager.Instance.position120 = Vector3.zero;
					}
					break;

				case "line130":
					if (id == SensorsGlobalManager.Instance.id130) {
						SensorsGlobalManager.Instance.id130 = -1;
						SensorsGlobalManager.Instance.type130 = null;
						SensorsGlobalManager.Instance.position130 = Vector3.zero;
					}
					break;

				case "line140":
					if (id == SensorsGlobalManager.Instance.id140) {
						SensorsGlobalManager.Instance.id140 = -1;
						SensorsGlobalManager.Instance.type140 = null;
						SensorsGlobalManager.Instance.position140 = Vector3.zero;
					}
					break;

				case "line150":
					if (id == SensorsGlobalManager.Instance.id150) {
						SensorsGlobalManager.Instance.id150 = -1;
						SensorsGlobalManager.Instance.type150 = null;
						SensorsGlobalManager.Instance.position150 = Vector3.zero;
					}
					break;

				case "line160":
					if (id == SensorsGlobalManager.Instance.id160) {
						SensorsGlobalManager.Instance.id160 = -1;
						SensorsGlobalManager.Instance.type160 = null;
						SensorsGlobalManager.Instance.position160 = Vector3.zero;
					}
					break;

				case "line170":
					if (id == SensorsGlobalManager.Instance.id170) {
						SensorsGlobalManager.Instance.id170 = -1;
						SensorsGlobalManager.Instance.type170 = null;
						SensorsGlobalManager.Instance.position170 = Vector3.zero;
					}
					break;

				case "line180":
					if (id == SensorsGlobalManager.Instance.id180) {
						SensorsGlobalManager.Instance.id180 = -1;
						SensorsGlobalManager.Instance.type180 = null;
						SensorsGlobalManager.Instance.position180 = Vector3.zero;
					}
					break;

				case "line190":
					if (id == SensorsGlobalManager.Instance.id190) {
						SensorsGlobalManager.Instance.id190 = -1;
						SensorsGlobalManager.Instance.type190 = null;
						SensorsGlobalManager.Instance.position190 = Vector3.zero;
					}
					break;

				case "line200":
					if (id == SensorsGlobalManager.Instance.id200) {
						SensorsGlobalManager.Instance.id200 = -1;
						SensorsGlobalManager.Instance.type200 = null;
						SensorsGlobalManager.Instance.position200 = Vector3.zero;
					}
					break;

				case "line210":
					if (id == SensorsGlobalManager.Instance.id210) {
						SensorsGlobalManager.Instance.id210 = -1;
						SensorsGlobalManager.Instance.type210 = null;
						SensorsGlobalManager.Instance.position210 = Vector3.zero;
					}
					break;

				case "line220":
					if (id == SensorsGlobalManager.Instance.id220) {
						SensorsGlobalManager.Instance.id220 = -1;
						SensorsGlobalManager.Instance.type220 = null;
						SensorsGlobalManager.Instance.position220 = Vector3.zero;
					}
					break;

				case "line230":
					if (id == SensorsGlobalManager.Instance.id230) {
						SensorsGlobalManager.Instance.id230 = -1;
						SensorsGlobalManager.Instance.type230 = null;
						SensorsGlobalManager.Instance.position230 = Vector3.zero;
					}
					break;

				case "line240":
					if (id == SensorsGlobalManager.Instance.id240) {
						SensorsGlobalManager.Instance.id240 = -1;
						SensorsGlobalManager.Instance.type240 = null;
						SensorsGlobalManager.Instance.position240 = Vector3.zero;
					}
					break;

				case "line250":
					if (id == SensorsGlobalManager.Instance.id250) {
						SensorsGlobalManager.Instance.id250 = -1;
						SensorsGlobalManager.Instance.type250 = null;
						SensorsGlobalManager.Instance.position250 = Vector3.zero;
					}
					break;

				case "line260":
					if (id == SensorsGlobalManager.Instance.id260) {
						SensorsGlobalManager.Instance.id260 = -1;
						SensorsGlobalManager.Instance.type260 = null;
						SensorsGlobalManager.Instance.position260 = Vector3.zero;
					}
					break;

				case "line270":
					if (id == SensorsGlobalManager.Instance.id270) {
						SensorsGlobalManager.Instance.id270 = -1;
						SensorsGlobalManager.Instance.type270 = null;
						SensorsGlobalManager.Instance.position270 = Vector3.zero;
					}
					break;

				case "line280":
					if (id == SensorsGlobalManager.Instance.id280) {
						SensorsGlobalManager.Instance.id280 = -1;
						SensorsGlobalManager.Instance.type280 = null;
						SensorsGlobalManager.Instance.position280 = Vector3.zero;
					}
					break;

				case "line290":
					if (id == SensorsGlobalManager.Instance.id290) {
						SensorsGlobalManager.Instance.id290 = -1;
						SensorsGlobalManager.Instance.type290 = null;
						SensorsGlobalManager.Instance.position290 = Vector3.zero;
					}
					break;

				case "line300":
					if (id == SensorsGlobalManager.Instance.id300) {
						SensorsGlobalManager.Instance.id300 = -1;
						SensorsGlobalManager.Instance.type300 = null;
						SensorsGlobalManager.Instance.position300 = Vector3.zero;
					}
					break;

				case "line310":
					if (id == SensorsGlobalManager.Instance.id310) {
						SensorsGlobalManager.Instance.id310 = -1;
						SensorsGlobalManager.Instance.type310 = null;
						SensorsGlobalManager.Instance.position310 = Vector3.zero;
					}
					break;

				case "line320":
					if (id == SensorsGlobalManager.Instance.id320) {
						SensorsGlobalManager.Instance.id320 = -1;
						SensorsGlobalManager.Instance.type320 = null;
						SensorsGlobalManager.Instance.position320 = Vector3.zero;
					}
					break;

				case "line330":
					if (id == SensorsGlobalManager.Instance.id330) {
						SensorsGlobalManager.Instance.id330 = -1;
						SensorsGlobalManager.Instance.type330 = null;
						SensorsGlobalManager.Instance.position330 = Vector3.zero;
					}
					break;

				case "line340":
					if (id == SensorsGlobalManager.Instance.id340) {
						SensorsGlobalManager.Instance.id340 = -1;
						SensorsGlobalManager.Instance.type340 = null;
						SensorsGlobalManager.Instance.position340 = Vector3.zero;
					}
					break;

				case "line350":
					if (id == SensorsGlobalManager.Instance.id350) {
						SensorsGlobalManager.Instance.id350 = -1;
						SensorsGlobalManager.Instance.type350 = null;
						SensorsGlobalManager.Instance.position350 = Vector3.zero;
					}
					break;

				}
			}
		}
	}

	void OnCollisionEnter(Collision collide)
	{
		Vector3 carPosition = GameObject.Find ("Car").transform.position;
		print (carPosition);

		string nearest_type=null;
		Vector3 nearest_position=new Vector3(0,0,0);
		int nearest_id = 0;
		float shortest_distance = float.MaxValue;
		

		float current_distance;

		foreach (ContactPoint contact in collide.contacts) {
//			print (contact.point);
			current_distance = Vector3.Distance (GameObject.Find ("Car").transform.position, contact.point);
			if (current_distance < shortest_distance) {
				shortest_distance = current_distance;
				nearest_position = contact.point;
				nearest_type = contact.otherCollider.name;
				nearest_id = contact.otherCollider.GetInstanceID ();
			}
		}

		if (collide.gameObject.name != "Car") {
			
			switch (this.gameObject.name) {
			case "line0":
				SensorsGlobalManager.Instance.id0 = nearest_id;
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
				SensorsGlobalManager.Instance.id10 = nearest_id;
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
				SensorsGlobalManager.Instance.id20 = nearest_id;
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
				SensorsGlobalManager.Instance.id30 = nearest_id;
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
				SensorsGlobalManager.Instance.id40 = nearest_id;
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
				SensorsGlobalManager.Instance.id50 = nearest_id;
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
				SensorsGlobalManager.Instance.id60 = nearest_id;
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
				SensorsGlobalManager.Instance.id70 = nearest_id;
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
				SensorsGlobalManager.Instance.id80 = nearest_id;
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
				SensorsGlobalManager.Instance.id90 = nearest_id;
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
				SensorsGlobalManager.Instance.id100 = nearest_id;
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
				SensorsGlobalManager.Instance.id110 = nearest_id;
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
				SensorsGlobalManager.Instance.id120 = nearest_id;
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
				SensorsGlobalManager.Instance.id130 = nearest_id;
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
				SensorsGlobalManager.Instance.id140 = nearest_id;
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
				SensorsGlobalManager.Instance.id150 = nearest_id;
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
				SensorsGlobalManager.Instance.id160 = nearest_id;
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
				SensorsGlobalManager.Instance.id170 = nearest_id;
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
				SensorsGlobalManager.Instance.id180 = nearest_id;
				SensorsGlobalManager.Instance.type180 = nearest_type;
				SensorsGlobalManager.Instance.position180 = nearest_position;
				break;

			case "line190":
				SensorsGlobalManager.Instance.id190 = nearest_id;
				SensorsGlobalManager.Instance.type190 = nearest_type;
				SensorsGlobalManager.Instance.position190 = nearest_position;
				break;

			case "line200":
				SensorsGlobalManager.Instance.id200 = nearest_id;
				SensorsGlobalManager.Instance.type200 = nearest_type;
				SensorsGlobalManager.Instance.position200 = nearest_position;
				break;

			case "line210":
				SensorsGlobalManager.Instance.id210 = nearest_id;
				SensorsGlobalManager.Instance.type210 = nearest_type;
				SensorsGlobalManager.Instance.position210 = nearest_position;
				break;

			case "line220":
				SensorsGlobalManager.Instance.id220 = nearest_id;
				SensorsGlobalManager.Instance.type220 = nearest_type;
				SensorsGlobalManager.Instance.position220 = nearest_position;
				break;

			case "line230":
				SensorsGlobalManager.Instance.id230 = nearest_id;
				SensorsGlobalManager.Instance.type230 = nearest_type;
				SensorsGlobalManager.Instance.position230 = nearest_position;
				break;

			case "line240":
				SensorsGlobalManager.Instance.id240 = nearest_id;
				SensorsGlobalManager.Instance.type240 = nearest_type;
				SensorsGlobalManager.Instance.position240 = nearest_position;
				break;

			case "line250":
				SensorsGlobalManager.Instance.id250 = nearest_id;
				SensorsGlobalManager.Instance.type250 = nearest_type;
				SensorsGlobalManager.Instance.position250 = nearest_position;
				break;

			case "line260":
				SensorsGlobalManager.Instance.id260 = nearest_id;
				SensorsGlobalManager.Instance.type260 = nearest_type;
				SensorsGlobalManager.Instance.position260 = nearest_position;
				break;

			case "line270":
				SensorsGlobalManager.Instance.id270 = nearest_id;
				SensorsGlobalManager.Instance.type270 = nearest_type;
				SensorsGlobalManager.Instance.position270 = nearest_position;
				break;

			case "line280":
				SensorsGlobalManager.Instance.id280 = nearest_id;
				SensorsGlobalManager.Instance.type280 = nearest_type;
				SensorsGlobalManager.Instance.position280 = nearest_position;
				break;

			case "line290":
				SensorsGlobalManager.Instance.id290 = nearest_id;
				SensorsGlobalManager.Instance.type290 = nearest_type;
				SensorsGlobalManager.Instance.position290 = nearest_position;
				break;

			case "line300":
				SensorsGlobalManager.Instance.id300 = nearest_id;
				SensorsGlobalManager.Instance.type300 = nearest_type;
				SensorsGlobalManager.Instance.position300 = nearest_position;
				break;

			case "line310":
				SensorsGlobalManager.Instance.id310 = nearest_id;
				SensorsGlobalManager.Instance.type310 = nearest_type;
				SensorsGlobalManager.Instance.position310 = nearest_position;
				break;

			case "line320":
				SensorsGlobalManager.Instance.id320 = nearest_id;
				SensorsGlobalManager.Instance.type320 = nearest_type;
				SensorsGlobalManager.Instance.position320 = nearest_position;
				break;

			case "line330":
				SensorsGlobalManager.Instance.id330 = nearest_id;
				SensorsGlobalManager.Instance.type330 = nearest_type;
				SensorsGlobalManager.Instance.position330 = nearest_position;
				break;

			case "line340":
				SensorsGlobalManager.Instance.id340 = nearest_id;
				SensorsGlobalManager.Instance.type340 = nearest_type;
				SensorsGlobalManager.Instance.position340 = nearest_position;
				break;

			case "line350":
				SensorsGlobalManager.Instance.id350 = nearest_id;
				SensorsGlobalManager.Instance.type350 = nearest_type;
				SensorsGlobalManager.Instance.position350 = nearest_position;
				break;

			}
			//Output the name of the GameObject you collide with
			Debug.Log ("Sensor " + this.gameObject.name + " hit the GameObject : " + collide.gameObject.name);
		}
	}

}
