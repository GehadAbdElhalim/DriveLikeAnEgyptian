using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace UnityStandardAssets.Vehicles.Car
{
	[RequireComponent(typeof(CarController))]
	public class RecordingScript : MonoBehaviour {

		public Line [] lines; 

		string logFilePath;

		[Header("Sensors")]
		public float sensorLength = 20.0f;
		public float angle0 = 0.0f;
		public float angle10 = 10;
		public float angle20 = 20;
		public float angle30 = 30;
		public float angle40 = 40;
		public float angle50 = 50;
		public float angle60 = 60;
		public float angle70 = 70;
		public float angle80 = 80;
		public float angle90 = 90;
		public float angle100 = 100;
		public float angle110 = 110;
		public float angle120 = 120;
		public float angle130 = 130;
		public float angle140 = 140;
		public float angle150 = 150;
		public float angle160 = 160;
		public float angle170 = 170;
		public float angle180 = 180;
		public float angle190 = 190;
		public float angle200 = 200;
		public float angle210 = 210;
		public float angle220 = 220;
		public float angle230 = 230;
		public float angle240 = 240;
		public float angle250 = 250;
		public float angle260 = 260;
		public float angle270 = 270;
		public float angle280 = 280;
		public float angle290 = 290;
		public float angle300 = 300;
		public float angle310 = 310;
		public float angle320 = 320;
		public float angle330 = 330;
		public float angle340 = 340;
		public float angle350 = 350;

		void Awake (){
			print (Application.dataPath);
			logFilePath = Application.dataPath + "/LogFiles/state-action.json";
		}

		// Use this for initialization
		void Start () {
			lines = new Line[36];
			InvokeRepeating("getStateAction", 0.5f, 0.5f);
		}

		void Sensors(){
			RaycastHit hit;
			Vector3 sensorStartPos = transform.position;
			sensorStartPos.y = 0.2f;

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle0, transform.up) * transform.forward, out hit, sensorLength)) {
				print (hit.collider.name);

				SensorsGlobalManager.Instance.id0 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type0 = hit.collider.name;
				SensorsGlobalManager.Instance.position0 = hit.point;

				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id0 = null;
				SensorsGlobalManager.Instance.type0 = null;
				SensorsGlobalManager.Instance.position0 = Vector3.zero;
			}
			print (SensorsGlobalManager.Instance.id0);

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle10, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id10 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type10 = hit.collider.name;
				SensorsGlobalManager.Instance.position10 = hit.point;

				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id10 = null;
				SensorsGlobalManager.Instance.type10 = null;
				SensorsGlobalManager.Instance.position10 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle20, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id20 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type20 = hit.collider.name;
				SensorsGlobalManager.Instance.position20 = hit.point;

				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id20 = null;
				SensorsGlobalManager.Instance.type20 = null;
				SensorsGlobalManager.Instance.position20 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle30, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id30 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type30 = hit.collider.name;
				SensorsGlobalManager.Instance.position30 = hit.point;

				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id30 = null;
				SensorsGlobalManager.Instance.type30 = null;
				SensorsGlobalManager.Instance.position30 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle40, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id40 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type40 = hit.collider.name;
				SensorsGlobalManager.Instance.position40 = hit.point;

				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id40 = null;
				SensorsGlobalManager.Instance.type40 = null;
				SensorsGlobalManager.Instance.position40 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle50, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id50 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type50 = hit.collider.name;
				SensorsGlobalManager.Instance.position50 = hit.point;

				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id50 = null;
				SensorsGlobalManager.Instance.type50 = null;
				SensorsGlobalManager.Instance.position50 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle60, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id60 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type60 = hit.collider.name;
				SensorsGlobalManager.Instance.position60 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id60 = null;
				SensorsGlobalManager.Instance.type60 = null;
				SensorsGlobalManager.Instance.position60 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle70, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id70 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type70 = hit.collider.name;
				SensorsGlobalManager.Instance.position70 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id70 = null;
				SensorsGlobalManager.Instance.type70 = null;
				SensorsGlobalManager.Instance.position70 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle80, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id80 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type80 = hit.collider.name;
				SensorsGlobalManager.Instance.position80 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id80 = null;
				SensorsGlobalManager.Instance.type80 = null;
				SensorsGlobalManager.Instance.position80 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle90, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id90 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type90 = hit.collider.name;
				SensorsGlobalManager.Instance.position90 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id90 = null;
				SensorsGlobalManager.Instance.type90 = null;
				SensorsGlobalManager.Instance.position90 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle100, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id100 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type100 = hit.collider.name;
				SensorsGlobalManager.Instance.position100 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id100 = null;
				SensorsGlobalManager.Instance.type100 = null;
				SensorsGlobalManager.Instance.position100 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle110, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id110 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type110 = hit.collider.name;
				SensorsGlobalManager.Instance.position110 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id110 = null;
				SensorsGlobalManager.Instance.type110 = null;
				SensorsGlobalManager.Instance.position110 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle120, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id120 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type120 = hit.collider.name;
				SensorsGlobalManager.Instance.position120 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id120 = null;
				SensorsGlobalManager.Instance.type120 = null;
				SensorsGlobalManager.Instance.position120 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle130, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id130 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type130 = hit.collider.name;
				SensorsGlobalManager.Instance.position130 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id130 = null;
				SensorsGlobalManager.Instance.type130 = null;
				SensorsGlobalManager.Instance.position130 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle140, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id140 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type140 = hit.collider.name;
				SensorsGlobalManager.Instance.position140 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id140 = null;
				SensorsGlobalManager.Instance.type140 = null;
				SensorsGlobalManager.Instance.position140 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle150, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id150 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type150 = hit.collider.name;
				SensorsGlobalManager.Instance.position150 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id150 = null;
				SensorsGlobalManager.Instance.type150 = null;
				SensorsGlobalManager.Instance.position150 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle160, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id160 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type160 = hit.collider.name;
				SensorsGlobalManager.Instance.position160 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id160 = null;
				SensorsGlobalManager.Instance.type160 = null;
				SensorsGlobalManager.Instance.position160 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle170, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id170 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type170 = hit.collider.name;
				SensorsGlobalManager.Instance.position170 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id170 = null;
				SensorsGlobalManager.Instance.type170 = null;
				SensorsGlobalManager.Instance.position170 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle180, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id180 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type180 = hit.collider.name;
				SensorsGlobalManager.Instance.position180 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id180 = null;
				SensorsGlobalManager.Instance.type180 = null;
				SensorsGlobalManager.Instance.position180 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle190, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id190 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type190 = hit.collider.name;
				SensorsGlobalManager.Instance.position190 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id190 = null;
				SensorsGlobalManager.Instance.type190 = null;
				SensorsGlobalManager.Instance.position190 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle200, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id200 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type200 = hit.collider.name;
				SensorsGlobalManager.Instance.position200 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id200 = null;
				SensorsGlobalManager.Instance.type200 = null;
				SensorsGlobalManager.Instance.position200 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle210, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id210 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type210 = hit.collider.name;
				SensorsGlobalManager.Instance.position210 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id210 = null;
				SensorsGlobalManager.Instance.type210 = null;
				SensorsGlobalManager.Instance.position210 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle220, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id220 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type220 = hit.collider.name;
				SensorsGlobalManager.Instance.position220 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id220 = null;
				SensorsGlobalManager.Instance.type220 = null;
				SensorsGlobalManager.Instance.position220 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle230, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id230 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type230 = hit.collider.name;
				SensorsGlobalManager.Instance.position230 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id230 = null;
				SensorsGlobalManager.Instance.type230 = null;
				SensorsGlobalManager.Instance.position230 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle240, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id240 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type240 = hit.collider.name;
				SensorsGlobalManager.Instance.position240 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id240 = null;
				SensorsGlobalManager.Instance.type240 = null;
				SensorsGlobalManager.Instance.position240 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle250, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id250 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type250 = hit.collider.name;
				SensorsGlobalManager.Instance.position250 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id250 = null;
				SensorsGlobalManager.Instance.type250 = null;
				SensorsGlobalManager.Instance.position250 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle260, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id260 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type260 = hit.collider.name;
				SensorsGlobalManager.Instance.position260 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id260 = null;
				SensorsGlobalManager.Instance.type260 = null;
				SensorsGlobalManager.Instance.position260 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle270, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id270 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type270 = hit.collider.name;
				SensorsGlobalManager.Instance.position270 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id270 = null;
				SensorsGlobalManager.Instance.type270 = null;
				SensorsGlobalManager.Instance.position270 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle280, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id280 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type280 = hit.collider.name;
				SensorsGlobalManager.Instance.position280 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id280 = null;
				SensorsGlobalManager.Instance.type280 = null;
				SensorsGlobalManager.Instance.position280 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle290, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id290 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type290 = hit.collider.name;
				SensorsGlobalManager.Instance.position290 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id290 = null;
				SensorsGlobalManager.Instance.type290 = null;
				SensorsGlobalManager.Instance.position290 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle300, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id300 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type300 = hit.collider.name;
				SensorsGlobalManager.Instance.position300 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id300 = null;
				SensorsGlobalManager.Instance.type300 = null;
				SensorsGlobalManager.Instance.position300 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle310, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id310 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type310 = hit.collider.name;
				SensorsGlobalManager.Instance.position310 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id310 = null;
				SensorsGlobalManager.Instance.type310 = null;
				SensorsGlobalManager.Instance.position310 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle320, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id320 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type320 = hit.collider.name;
				SensorsGlobalManager.Instance.position320 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id320 = null;
				SensorsGlobalManager.Instance.type320 = null;
				SensorsGlobalManager.Instance.position320 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle330, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id330 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type330 = hit.collider.name;
				SensorsGlobalManager.Instance.position330 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id330 = null;
				SensorsGlobalManager.Instance.type330 = null;
				SensorsGlobalManager.Instance.position330 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle340, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id340 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type340 = hit.collider.name;
				SensorsGlobalManager.Instance.position340 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id340 = null;
				SensorsGlobalManager.Instance.type340 = null;
				SensorsGlobalManager.Instance.position340 = Vector3.zero;
			}

			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis (angle350, transform.up) * transform.forward, out hit, sensorLength)) {
				SensorsGlobalManager.Instance.id350 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type350 = hit.collider.name;
				SensorsGlobalManager.Instance.position350 = hit.point;
			
				Debug.DrawLine (sensorStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id350 = null;
				SensorsGlobalManager.Instance.type350 = null;
				SensorsGlobalManager.Instance.position350 = Vector3.zero;
			}
//			print(tran
		}

		void trafficLights(){
			
			GameObject[] goWithTag = GameObject.FindGameObjectsWithTag("TrafficLight");
			Renderer TrafficLights_renderer;

			for (int i = 0; i < goWithTag.Length; ++i)
			{
				TrafficLights_renderer = goWithTag [i].GetComponent<Renderer> ();
				if (TrafficLights_renderer.isVisible && Vector3.Distance (transform.position, goWithTag [i].transform.position) <= 10.0f) {
					return goWithTag [i].Find("red_light").GetComponent<Light>().intensity!=0;
				}
			}

			return false;

//			GameObject TrafficLights = GameObject.Find ("LampPost_A");
//			Renderer TrafficLights_renderer = TrafficLights.GetComponent<Renderer> ();
		}

		// Update is called once per frame
		void Update () {
//			Sensors(); 
			print(transform.forward);
		}

		void getStateAction(){
			State currentState = new State()
			{
				line0 = new Line()
				{
					id = SensorsGlobalManager.Instance.id0,
					type = SensorsGlobalManager.Instance.type0,
					position = SensorsGlobalManager.Instance.position0
				},
				line10 = new Line()
				{
					id = SensorsGlobalManager.Instance.id10,
					type = SensorsGlobalManager.Instance.type10,
					position = SensorsGlobalManager.Instance.position10
				},
				line20 = new Line()
				{
					id = SensorsGlobalManager.Instance.id20,
					type = SensorsGlobalManager.Instance.type20,
					position = SensorsGlobalManager.Instance.position20
				},
				line30 = new Line()
				{
					id = SensorsGlobalManager.Instance.id30,
					type = SensorsGlobalManager.Instance.type30,
					position = SensorsGlobalManager.Instance.position30
				},
				line40 = new Line()
				{
					id = SensorsGlobalManager.Instance.id40,
					type = SensorsGlobalManager.Instance.type40,
					position = SensorsGlobalManager.Instance.position40
				},
				line50 = new Line()
				{
					id = SensorsGlobalManager.Instance.id50,
					type = SensorsGlobalManager.Instance.type50,
					position = SensorsGlobalManager.Instance.position50
				},
				line60 = new Line()
				{
					id = SensorsGlobalManager.Instance.id60,
					type = SensorsGlobalManager.Instance.type60,
					position = SensorsGlobalManager.Instance.position60
				},
				line70 = new Line()
				{
					id = SensorsGlobalManager.Instance.id70,
					type = SensorsGlobalManager.Instance.type70,
					position = SensorsGlobalManager.Instance.position70
				},
				line80 = new Line()
				{
					id = SensorsGlobalManager.Instance.id80,
					type = SensorsGlobalManager.Instance.type80,
					position = SensorsGlobalManager.Instance.position80
				},
				line90 = new Line()
				{
					id = SensorsGlobalManager.Instance.id90,
					type = SensorsGlobalManager.Instance.type90,
					position = SensorsGlobalManager.Instance.position90
				},
				line100 = new Line()
				{
					id = SensorsGlobalManager.Instance.id100,
					type = SensorsGlobalManager.Instance.type100,
					position = SensorsGlobalManager.Instance.position100
				},
				line110 = new Line()
				{
					id = SensorsGlobalManager.Instance.id110,
					type = SensorsGlobalManager.Instance.type110,
					position = SensorsGlobalManager.Instance.position110
				},
				line120 = new Line()
				{
					id = SensorsGlobalManager.Instance.id120,
					type = SensorsGlobalManager.Instance.type120,
					position = SensorsGlobalManager.Instance.position120
				},
				line130 = new Line()
				{
					id = SensorsGlobalManager.Instance.id130,
					type = SensorsGlobalManager.Instance.type130,
					position = SensorsGlobalManager.Instance.position130
				},
				line140 = new Line()
				{
					id = SensorsGlobalManager.Instance.id140,
					type = SensorsGlobalManager.Instance.type140,
					position = SensorsGlobalManager.Instance.position140
				},
				line150 = new Line()
				{
					id = SensorsGlobalManager.Instance.id150,
					type = SensorsGlobalManager.Instance.type150,
					position = SensorsGlobalManager.Instance.position150
				},
				line160 = new Line()
				{
					id = SensorsGlobalManager.Instance.id160,
					type = SensorsGlobalManager.Instance.type160,
					position = SensorsGlobalManager.Instance.position160
				},
				line170 = new Line()
				{
					id = SensorsGlobalManager.Instance.id170,
					type = SensorsGlobalManager.Instance.type170,
					position = SensorsGlobalManager.Instance.position170
				},line180 = new Line()
				{
					id = SensorsGlobalManager.Instance.id180,
					type = SensorsGlobalManager.Instance.type180,
					position = SensorsGlobalManager.Instance.position180
				},
				line190 = new Line()
				{
					id = SensorsGlobalManager.Instance.id190,
					type = SensorsGlobalManager.Instance.type190,
					position = SensorsGlobalManager.Instance.position190
				},
				line200 = new Line()
				{
					id = SensorsGlobalManager.Instance.id200,
					type = SensorsGlobalManager.Instance.type200,
					position = SensorsGlobalManager.Instance.position200
				},
				line210 = new Line()
				{
					id = SensorsGlobalManager.Instance.id210,
					type = SensorsGlobalManager.Instance.type210,
					position = SensorsGlobalManager.Instance.position210
				},
				line220 = new Line()
				{
					id = SensorsGlobalManager.Instance.id220,
					type = SensorsGlobalManager.Instance.type220,
					position = SensorsGlobalManager.Instance.position220
				},
				line230 = new Line()
				{
					id = SensorsGlobalManager.Instance.id230,
					type = SensorsGlobalManager.Instance.type230,
					position = SensorsGlobalManager.Instance.position230
				},
				line240 = new Line()
				{
					id = SensorsGlobalManager.Instance.id240,
					type = SensorsGlobalManager.Instance.type240,
					position = SensorsGlobalManager.Instance.position240
				},
				line250 = new Line()
				{
					id = SensorsGlobalManager.Instance.id250,
					type = SensorsGlobalManager.Instance.type250,
					position = SensorsGlobalManager.Instance.position250
				},
				line260 = new Line()
				{
					id = SensorsGlobalManager.Instance.id260,
					type = SensorsGlobalManager.Instance.type260,
					position = SensorsGlobalManager.Instance.position260
				},
				line270 = new Line()
				{
					id = SensorsGlobalManager.Instance.id270,
					type = SensorsGlobalManager.Instance.type270,
					position = SensorsGlobalManager.Instance.position270
				},
				line280 = new Line()
				{
					id = SensorsGlobalManager.Instance.id280,
					type = SensorsGlobalManager.Instance.type280,
					position = SensorsGlobalManager.Instance.position280
				},
				line290 = new Line()
				{
					id = SensorsGlobalManager.Instance.id290,
					type = SensorsGlobalManager.Instance.type290,
					position = SensorsGlobalManager.Instance.position290
				},
				line300 = new Line()
				{
					id = SensorsGlobalManager.Instance.id300,
					type = SensorsGlobalManager.Instance.type300,
					position = SensorsGlobalManager.Instance.position300
				},
				line310 = new Line()
				{
					id = SensorsGlobalManager.Instance.id310,
					type = SensorsGlobalManager.Instance.type310,
					position = SensorsGlobalManager.Instance.position310
				},
				line320 = new Line()
				{
					id = SensorsGlobalManager.Instance.id320,
					type = SensorsGlobalManager.Instance.type320,
					position = SensorsGlobalManager.Instance.position320
				},
				line330 = new Line()
				{
					id = SensorsGlobalManager.Instance.id330,
					type = SensorsGlobalManager.Instance.type330,
					position = SensorsGlobalManager.Instance.position330
				},
				line340 = new Line()
				{
					id = SensorsGlobalManager.Instance.id340,
					type = SensorsGlobalManager.Instance.type340,
					position = SensorsGlobalManager.Instance.position340
				},
				line350 = new Line()
				{
					id = SensorsGlobalManager.Instance.id350,
					type = SensorsGlobalManager.Instance.type350,
					position = SensorsGlobalManager.Instance.position350
				},

				car_velocity = (GetComponent("CarController") as CarController).CurrentSpeed,
				car_angle = transform.forward,
				traffic_light =  trafficLights(), //get current roadblock?? -> cheat

			};

			//			print (currentState_JSON+ "\t\n"	);
			Action newAction = new Action (){
				acceleration = (GetComponent("CarController") as CarController).AccelInput,
				steer_angle = (GetComponent("CarController") as CarController).CurrentSteerAngle
			};

			StateActionPair newPair = new StateActionPair() {
				a = newAction,
				s = currentState
			};

			string newPair_JSON = JsonUtility.ToJson(newPair,true);
			logStateAction (newPair_JSON, logFilePath);	

		}

		void logStateAction(string objects,string path){
			StreamWriter writer = new StreamWriter (path, true);
			writer.WriteLine (objects);
			writer.Close();
		}


		[System.Serializable]
		public class Line
		{
			public string id;
			public string type;
			public Vector3 position;
		}

		[System.Serializable]
		public class State
		{
			public Line line0;
			public Line line10;
			public Line line20;
			public Line line30;
			public Line line40;
			public Line line50;
			public Line line60;
			public Line line70;
			public Line line80;
			public Line line90;
			public Line line100;
			public Line line110;
			public Line line120;
			public Line line130;
			public Line line140;
			public Line line150;
			public Line line160;
			public Line line170;
			public Line line180;
			public Line line190;
			public Line line200;
			public Line line210;
			public Line line220;
			public Line line230;
			public Line line240;
			public Line line250;
			public Line line260;
			public Line line270;
			public Line line280;
			public Line line290;
			public Line line300;
			public Line line310;
			public Line line320;
			public Line line330;
			public Line line340;
			public Line line350;

			public bool rain;
			public float car_velocity;
			public float car_angle;
			public bool traffic_light; //{true:red, false:green}
		}

		[System.Serializable]
		public class Action
		{
			public float acceleration;
			public float steer_angle;

		}

		[System.Serializable]
		public class StateActionPair
		{
			public State s;
			public Action a;

		}
	}
}
