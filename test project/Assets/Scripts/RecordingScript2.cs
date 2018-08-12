using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;

namespace UnityStandardAssets.Vehicles.Car
{
	[RequireComponent(typeof(CarController))]
	public class RecordingScript2 : MonoBehaviour {
		public bool SpawnAd;
		public bool isAd;
		public static Transform OtherCar ;
		public List<Collider> collidedObstacles = new List<Collider>();	
		public List<Collider> collidedPedestrians = new List<Collider>();	

		public Line [] lines; 

		public float CarAngle;
		public bool collidedPavement; //boolean for hitting a pavement
		GameObject current_roadblock;

		string logFilePath_json;
		string logFilePath_csv;

		bool recordFlag;
		[Header("Sensors")]
		public float sensorLength = 10.0f;
		public bool startLogging;
		void Awake (){
			logFilePath_json = Application.dataPath + "/LogFiles/state-action.json";
			//logFilePath_csv = chooseFile(Application.dataPath + "/LogFiles/normalCar");
			if(SpawnAd)
			RecordingScript1.OtherCar = GetComponent<Transform>();
//			GameObject.Find ("Front Facing Camera").enabled = true;

			// USED FIRST TIME ONLY TO ADD CSV FILE HEADERS
			/*
			string[] rowDataTemp = new string[117];
			int i;
			for (i=0;i<36;i++) {
				rowDataTemp [i*3] = "id" + (i*10).ToString ();
				rowDataTemp [i*3+1] = "type" + (i*10).ToString ();
				rowDataTemp [i*3+2] = "distance" + (i*10).ToString ();
			}
			rowDataTemp[108] = "Vx";
			rowDataTemp[109] = "Vy";
			rowDataTemp[110] = "Vz";
			rowDataTemp[111] = "car_angle";
			rowDataTemp[112] = "traffic_light";
			rowDataTemp[113] = "rain";
			rowDataTemp[114] = "num_collisions";
			rowDataTemp[115] = "acceleration";
			rowDataTemp[116] = "steer_angle";
			using (FileStream fs = new FileStream(logFilePath_csv,FileMode.Append, FileAccess.Write))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine(string.Join (",", rowDataTemp));
				}
			}*/

		}

		public enum ObstacleType{
			Roadblock = 0,
			Pedestrian = 1,
			Construction = 2
		};

		// Use this for initialization
		void Start () {
			lines = new Line[36];
			current_roadblock = getRoadBlock ();
			//Only one line can be uncommented at once
//			InvokeRepeating("logStateAction_json", 0.5f, 0.5f);	//logs to json file
			if(startLogging)
			InvokeRepeating("logStateAction_csv", 0.05f, 0.05f);	//logs to csv file
		}

		void OnCollisionEnter(Collision col) 
		{
			if (col.gameObject.name == "Finish(Clone)") {
                if(GameObject.Find ("NetworkManager").GetComponent<networkSocket> ().myfinished == false)
                    GameObject.Find ("NetworkManager").GetComponent<networkSocket> ().myfinished = true;
			} else {
				if (col.gameObject.layer==11 && !collidedObstacles.Contains(col.collider)) 
				{
					collidedObstacles.Add(col.collider); 
				}
				if (col.gameObject.layer==14 && !collidedPedestrians.Contains(col.collider)) 
				{
					collidedPedestrians.Add(col.collider); 
				}
			}
		}

		void OnCollisionStay(Collision col) {
			OnCollisionEnter(col); //same as enter
		}

		public void OnCollisionExit(Collision col) {
			if (collidedObstacles.Contains(col.collider)) //ignoring normal obstacles' collisions (bumps and holes)
			{
				collidedObstacles.Remove (col.collider); 
			}
			if (collidedPedestrians.Contains(col.collider)) //ignoring normal obstacles' collisions (bumps and holes)
			{
				collidedPedestrians.Remove (col.collider); 
			}
		}

		void Sensors(){
			RaycastHit hit;
			Vector3 sensorLowStartPos = transform.position;
			Vector3 sensorHighStartPos = transform.position;
			sensorLowStartPos.y = 0.2f;
			sensorHighStartPos.y = 0.6f;

			var layerMask = 1 << 12;//carbody2 layer
			layerMask = ~layerMask;

//			bool calculated_angle = false;

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (90, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id90 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type90 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance90 = Vector3.Distance(transform.position, hit.point);

				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id90 = null;
				SensorsGlobalManager.Instance.type90 = null;
				SensorsGlobalManager.Instance.distance90 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (270, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id270 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type270 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance270 = Vector3.Distance(transform.position, hit.point);

				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id270 = null;
				SensorsGlobalManager.Instance.type270 = null;
				SensorsGlobalManager.Instance.distance270 = 0.0f;
			}
				
			if (Physics.Raycast (sensorLowStartPos, Quaternion.AngleAxis (0, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id0 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type0 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance0 = Vector3.Distance(transform.position, hit.point);

				Debug.DrawLine (sensorLowStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id0 = null;
				SensorsGlobalManager.Instance.type0 = null;
				SensorsGlobalManager.Instance.distance0 = 0.0f;
			}

			if (Physics.Raycast (sensorLowStartPos, Quaternion.AngleAxis (10, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id10 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type10 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance10 = Vector3.Distance(transform.position, hit.point);

				Debug.DrawLine (sensorLowStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id10 = null;
				SensorsGlobalManager.Instance.type10 = null;
				SensorsGlobalManager.Instance.distance10 = 0.0f;
			}

			if (Physics.Raycast (sensorLowStartPos, Quaternion.AngleAxis (20, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id20 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type20 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance20 = Vector3.Distance(transform.position, hit.point);

				Debug.DrawLine (sensorLowStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id20 = null;
				SensorsGlobalManager.Instance.type20 = null;
				SensorsGlobalManager.Instance.distance20 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (30, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id30 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type30 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance30 = Vector3.Distance(transform.position, hit.point);

				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id30 = null;
				SensorsGlobalManager.Instance.type30 = null;
				SensorsGlobalManager.Instance.distance30 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (40, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id40 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type40 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance40 = Vector3.Distance(transform.position, hit.point);

				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id40 = null;
				SensorsGlobalManager.Instance.type40 = null;
				SensorsGlobalManager.Instance.distance40 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (50, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id50 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type50 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance50 = Vector3.Distance(transform.position, hit.point);

				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id50 = null;
				SensorsGlobalManager.Instance.type50 = null;
				SensorsGlobalManager.Instance.distance50 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (60, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id60 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type60 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance60 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id60 = null;
				SensorsGlobalManager.Instance.type60 = null;
				SensorsGlobalManager.Instance.distance60 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (70, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id70 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type70 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance70 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id70 = null;
				SensorsGlobalManager.Instance.type70 = null;
				SensorsGlobalManager.Instance.distance70 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (80, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id80 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type80 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance80 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id80 = null;
				SensorsGlobalManager.Instance.type80 = null;
				SensorsGlobalManager.Instance.distance80 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (100, transform.up) * transform.forward, out hit, sensorLength ,layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id100 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type100 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance100 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id100 = null;
				SensorsGlobalManager.Instance.type100 = null;
				SensorsGlobalManager.Instance.distance100 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (110, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id110 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type110 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance110 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id110 = null;
				SensorsGlobalManager.Instance.type110 = null;
				SensorsGlobalManager.Instance.distance110 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (120, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id120 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type120 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance120 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id120 = null;
				SensorsGlobalManager.Instance.type120 = null;
				SensorsGlobalManager.Instance.distance120 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (130, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id130 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type130 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance130 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id130 = null;
				SensorsGlobalManager.Instance.type130 = null;
				SensorsGlobalManager.Instance.distance130 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (140, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id140 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type140 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance140 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id140 = null;
				SensorsGlobalManager.Instance.type140 = null;
				SensorsGlobalManager.Instance.distance140 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (150, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id150 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type150 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance150 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id150 = null;
				SensorsGlobalManager.Instance.type150 = null;
				SensorsGlobalManager.Instance.distance150 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (160, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id160 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type160 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance160 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id160 = null;
				SensorsGlobalManager.Instance.type160 = null;
				SensorsGlobalManager.Instance.distance160 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (170, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id170 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type170 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance170 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id170 = null;
				SensorsGlobalManager.Instance.type170 = null;
				SensorsGlobalManager.Instance.distance170 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (180, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id180 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type180 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance180 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id180 = null;
				SensorsGlobalManager.Instance.type180 = null;
				SensorsGlobalManager.Instance.distance180 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (190, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id190 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type190 =  (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance190 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id190 = null;
				SensorsGlobalManager.Instance.type190 = null;
				SensorsGlobalManager.Instance.distance190 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (200, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id200 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type200 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance200 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id200 = null;
				SensorsGlobalManager.Instance.type200 = null;
				SensorsGlobalManager.Instance.distance200 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (210, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id210 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type210 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance210 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id210 = null;
				SensorsGlobalManager.Instance.type210 = null;
				SensorsGlobalManager.Instance.distance210 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (220, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id220 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type220 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance220 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id220 = null;
				SensorsGlobalManager.Instance.type220 = null;
				SensorsGlobalManager.Instance.distance220 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (230, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id230 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type230 =  (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance230 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id230 = null;
				SensorsGlobalManager.Instance.type230 = null;
				SensorsGlobalManager.Instance.distance230 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (240, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id240 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type240 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance240 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id240 = null;
				SensorsGlobalManager.Instance.type240 = null;
				SensorsGlobalManager.Instance.distance240 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (250, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id250 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type250 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance250 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id250 = null;
				SensorsGlobalManager.Instance.type250 = null;
				SensorsGlobalManager.Instance.distance250 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (260, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id260 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type260 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance260 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id260 = null;
				SensorsGlobalManager.Instance.type260 = null;
				SensorsGlobalManager.Instance.distance260 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (280, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id280 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type280 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance280 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id280 = null;
				SensorsGlobalManager.Instance.type280 = null;
				SensorsGlobalManager.Instance.distance280 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (290, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id290 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type290 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance290 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id290 = null;
				SensorsGlobalManager.Instance.type290 = null;
				SensorsGlobalManager.Instance.distance290 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (300, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id300 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type300 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance300 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id300 = null;
				SensorsGlobalManager.Instance.type300 = null;
				SensorsGlobalManager.Instance.distance300 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (310, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id310 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type310 =  (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance310 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id310 = null;
				SensorsGlobalManager.Instance.type310 = null;
				SensorsGlobalManager.Instance.distance310 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (320, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id320 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type320 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance320 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id320 = null;
				SensorsGlobalManager.Instance.type320 = null;
				SensorsGlobalManager.Instance.distance320 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (330, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id330 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type330 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance330 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id330 = null;
				SensorsGlobalManager.Instance.type330 = null;
				SensorsGlobalManager.Instance.distance330 = 0.0f;
			}

			if (Physics.Raycast (sensorLowStartPos, Quaternion.AngleAxis (340, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id340 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type340 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance340 = Vector3.Distance(transform.position, hit.point);
			
				Debug.DrawLine (sensorLowStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id340 = null;
				SensorsGlobalManager.Instance.type340 = null;
				SensorsGlobalManager.Instance.distance340 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (350, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id350 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type350 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance350 = Vector3.Distance(transform.position, hit.point);	
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id350 = null;
				SensorsGlobalManager.Instance.type350 = null;
				SensorsGlobalManager.Instance.distance350 = 0.0f;
			}

			Vector3 sensorPos = transform.position;
			Ray r = new Ray(sensorPos, transform.right);
			sensorPos.y = 0.02f;
			layerMask = 1 << 8;//carbody2 layer
//			print (current_roadblock.name);
			current_roadblock = getRoadBlock();
			if (current_roadblock != null && current_roadblock.name != "CrossX" && current_roadblock.name != "intersection") {
				if (Physics.Raycast (sensorPos, Quaternion.AngleAxis (90, transform.up) * transform.forward, out hit, 20.0f, layerMask) && hit.collider.gameObject.tag == "RoadBlock") {
					Debug.DrawLine (sensorPos, hit.point, Color.green);
					float cosine = Vector3.Dot (r.direction, hit.normal);
					cosine = (cosine > 1.0f) ? 1.0f : (cosine < -1) ? -1.0f : cosine;

					CarAngle = (r.direction.z > hit.normal.z) ? Mathf.Acos (cosine) - Mathf.PI : Mathf.PI - Mathf.Acos (cosine);

				} else if (Physics.Raycast (sensorPos, Quaternion.AngleAxis (270, transform.up) * transform.forward, out hit, 20.0f, layerMask) && hit.collider.gameObject.tag == "RoadBlock") {

						Debug.DrawLine (sensorPos, hit.point, Color.green);
						float cosine = Vector3.Dot (r.direction, hit.normal);
						cosine = (cosine > 1.0f) ? 1.0f : (cosine < -1) ? -1.0f : cosine;

						CarAngle = (r.direction.z > hit.normal.z) ? -Mathf.Acos (cosine) : Mathf.Acos (cosine);
				}
				else {
					CarAngle = (CarAngle < 0.0f && CarAngle >= -Mathf.PI / 2) ? -Mathf.PI / 2 : Mathf.PI / 2;
				} 
			}
		}

		public bool trafficLights(){
			
			GameObject[] goWithTag = GameObject.FindGameObjectsWithTag("TrafficLight");
			Renderer TrafficLights_renderer;

			for (int i = 0; i < goWithTag.Length; ++i)
			{
				TrafficLights_renderer = goWithTag [i].GetComponent<Renderer> ();
				if (TrafficLights_renderer.isVisible && Vector3.Distance (transform.position, goWithTag [i].transform.position) <= 10.0f) {
					return (goWithTag [i]).transform.Find("red_light").GetComponent<Light>().intensity!=0;
				}
			}

			return false;
		}
		string chooseFile(string dirPath){
				string [] filesArr = Directory.GetFiles(dirPath);
				List<int> FilesNumber = new List<int>();
				for(int i=0;i<filesArr.Length;i++){
					string [] dirSplit = filesArr[i].Split(new string[]{"\\"}, StringSplitOptions.None);
					if(dirSplit[dirSplit.Length-1].EndsWith(".csv")){
						if(dirSplit[dirSplit.Length-1].Length >4){
						String Filename =dirSplit[dirSplit.Length-1].Trim().Substring(1,dirSplit[dirSplit.Length-1].Length-5); 
						int tempNum = 0;
						if(int.TryParse(Filename ,out tempNum  )){
							FilesNumber.Add(tempNum);
						}
						}
					}
				}
				int maxNum = 0;
				for(int i=0;i<FilesNumber.Count;i++){
					maxNum = Math.Max (FilesNumber[i],maxNum);
				}maxNum+=1 ;
				if(startLogging)
				File.Create(dirPath + "\\" + 2+maxNum.ToString() + ".csv");
				return  dirPath + "\\" +2+ maxNum.ToString() + ".csv";
				
		}
		// Update is called once per frame
		void Update () {
			Sensors();  
			if(SpawnAd)
			RecordingScript1.OtherCar = GetComponent<Transform>();
		}

		public GameObject getRoadBlock(){
			RaycastHit hit;
			if (Physics.Raycast(transform.position, Vector3.down, out hit, transform.position.y) && hit.collider.gameObject.tag == "RoadBlock") {
				return hit.collider.gameObject;
			} else {
				return current_roadblock;
			}
		}

		void logStateAction_csv(){
			string[] rowDataTemp = new string[120];
			//
			int i = 0; //line0
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id0;//0
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type0; //1
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance0.ToString();//2

			//line10
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id10;//3
			rowDataTemp[i++]  = SensorsGlobalManager.Instance.type10;//4 
			rowDataTemp[i++]  = SensorsGlobalManager.Instance.distance10.ToString();//5

			//line20
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id20;//6
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type20;//7 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance20.ToString();//8

			//line30
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id30;//9
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type30;//10 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance30.ToString();//11

			//line40
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id40;//12
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type40;//13 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance40.ToString();//14

			//line50
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id50;//15
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type50;//16 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance50.ToString();//17

			//line60
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id60;//18
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type60;//19 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance60.ToString();//20

			//line70
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id70;//21
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type70;//22 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance70.ToString();//23

			//line80
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id80;//24
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type80;//25 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance80.ToString();//26

			//line90
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id90;//27
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type90;//28 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance90.ToString();//29

			//line100
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id100;//30
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type100;//31
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance100.ToString();//32

			//line110
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id110;//33
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type110;//34 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance110.ToString();//35

			//line120
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id120;//36
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type120;//37 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance120.ToString();//38

			//line130
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id130;//39
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type130;//40 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance130.ToString();//41

			//line140
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id140;//42
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type140;//43 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance140.ToString();//44

			//line150
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id150;//45
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type150;//46 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance150.ToString();//47

			//line160
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id160;//48
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type160;//49 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance160.ToString();//50

			//line170
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id170;//51
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type170;//52 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance170.ToString();//53

			//line180
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id180;//54
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type180;//55
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance180.ToString();//56

			//line190
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id190;//57
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type190;//58 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance190.ToString();//59

			//line200
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id200;//60
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type200;//61 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance200.ToString();//62

			//line210
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id210;//63
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type210;//64
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance210.ToString();//65

			//line220
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id220;//66
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type220;//67 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance220.ToString();//68

			//line230
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id230;//69
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type230;//70
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance230.ToString();//71

			//line240
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id240;//72
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type240;//73 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance240.ToString();//74

			//line250
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id250;//75
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type250;//76 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance250.ToString();//77

			//line260
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id260;//78
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type260;//79
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance260.ToString();//80

			//line270
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id270;//81
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type270;//82 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance270.ToString();//83

			//line280
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id280;//84
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type280;//85
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance280.ToString();//86

			//line290
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id290;//87
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type290;//88 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance290.ToString();//89

			//line300
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id300;//90
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type300;//91 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance300.ToString();//92

			//line310
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id310;//93
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type310;//94 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance310.ToString();//95

			//line320
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id320;//96
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type320;//97 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance320.ToString();//98

			//line330
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id330;//99
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type330;//100 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance330.ToString();//101

			//line340
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id340;//102
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type340;//103 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance340.ToString();//104

			//line350
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id350;//105
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type350;//106 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance350.ToString();//107


			/*rowDataTemp[i++] = transform.GetComponent<Rigidbody>().velocity.x.ToString();
			rowDataTemp[i++] = transform.GetComponent<Rigidbody>().velocity.y.ToString();*/
			rowDataTemp[i++] = this.transform.InverseTransformDirection(this.transform.GetComponent<Rigidbody>().velocity).z.ToString();//108
			rowDataTemp[i++] = (CarAngle* 180/10/ Mathf.PI).ToString() ;//109
//			CarAngle = transform.eulerAngles.y - current_roadblock.transform.eulerAngles.y;
			rowDataTemp[i++] = trafficLights().ToString();//110
			rowDataTemp[i++] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().isRainy.ToString();//111
//			rowDataTemp [i++] = collidedObjects.Count.ToString();
			rowDataTemp[i++] = collidedObstacles.Count.ToString();//112
            rowDataTemp [i++] = collidedPedestrians.Count.ToString();//113
            rowDataTemp [i++] = collidedPavement.ToString();//114

            if (GameObject.Find ("CarDirection").GetComponent<testScript> ().DirectionAngle <= 80) {
                rowDataTemp [i++] = true.ToString();//115
            } else {
                rowDataTemp [i++] = false.ToString();
            }
			if(!SpawnAd){
				rowDataTemp [i++] = 0f.ToString ();//116
				rowDataTemp [i++] = 0f.ToString ();//117
			} else {
				if(OtherCar.Equals(null)){
				rowDataTemp [i++] = 0.ToString();	
				}else{
				rowDataTemp [i++] = Vector3.Distance(transform.position,OtherCar.position).ToString();
				}
				Vector3 [] Nodes =  GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().getMiddleNodes().ToArray();
				Vector3 NodeToThisPostion =Nodes [0]; int NodeToThisIndex = 0;
				Vector3 NodeToOtherPostion=Nodes [0]; int NodeToOtherIndex = 0;

				for(int n=0;n<Nodes.Length;n++){
					if(Vector3.Distance(transform.position,Nodes[n]) < Vector3.Distance(NodeToThisPostion,transform.position)){
						NodeToThisPostion = Nodes[n];
						NodeToThisIndex = n;
					}
					if(Vector3.Distance(OtherCar.position,Nodes[n]) < Vector3.Distance(NodeToOtherPostion,OtherCar.position)){
						NodeToOtherPostion = Nodes[n];
						NodeToOtherIndex = n;
					}
				}
				
				if(NodeToThisIndex >NodeToOtherIndex )
				{
					rowDataTemp [i++] = (1).ToString ();
				}else if(NodeToThisIndex ==NodeToOtherIndex){
					rowDataTemp [i++] = (0).ToString ();
				}else {
					rowDataTemp [i++] = (-1).ToString();
				}
				print(rowDataTemp[rowDataTemp.Length-2]);
				print(rowDataTemp[rowDataTemp.Length-1]);
			}
			//ACTION

//			rowDataTemp [i++] = ((GetComponent ("CarController") as CarController).AccelInput).ToString();
//			rowDataTemp [i++] = ((GetComponent ("CarController") as CarController).CurrentSteerAngle).ToString();

			rowDataTemp[i++] = Input.GetAxis("Vertical").ToString();//118
			rowDataTemp[i++] = Input.GetAxis("Horizontal").ToString();//119
			if(recordFlag){
			using (FileStream fs = new FileStream(logFilePath_csv,FileMode.Append, FileAccess.Write))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine(string.Join (",", rowDataTemp));
				
				}
			}
			collidedObstacles.Clear ();
			collidedPedestrians.Clear();
			}
		}

		void logStateAction_json(){
			State currentState = new State()
			{
				line0 = new Line()
				{
					id = SensorsGlobalManager.Instance.id0,
					type = SensorsGlobalManager.Instance.type0,
					distance = SensorsGlobalManager.Instance.distance0
				},
				line10 = new Line()
				{
					id = SensorsGlobalManager.Instance.id10,
					type = SensorsGlobalManager.Instance.type10,
					distance = SensorsGlobalManager.Instance.distance10
				},
				line20 = new Line()
				{
					id = SensorsGlobalManager.Instance.id20,
					type = SensorsGlobalManager.Instance.type20,
					distance = SensorsGlobalManager.Instance.distance20
				},
				line30 = new Line()
				{
					id = SensorsGlobalManager.Instance.id30,
					type = SensorsGlobalManager.Instance.type30,
					distance = SensorsGlobalManager.Instance.distance30
				},
				line40 = new Line()
				{
					id = SensorsGlobalManager.Instance.id40,
					type = SensorsGlobalManager.Instance.type40,
					distance = SensorsGlobalManager.Instance.distance40
				},
				line50 = new Line()
				{
					id = SensorsGlobalManager.Instance.id50,
					type = SensorsGlobalManager.Instance.type50,
					distance = SensorsGlobalManager.Instance.distance50
				},
				line60 = new Line()
				{
					id = SensorsGlobalManager.Instance.id60,
					type = SensorsGlobalManager.Instance.type60,
					distance = SensorsGlobalManager.Instance.distance60
				},
				line70 = new Line()
				{
					id = SensorsGlobalManager.Instance.id70,
					type = SensorsGlobalManager.Instance.type70,
					distance = SensorsGlobalManager.Instance.distance70
				},
				line80 = new Line()
				{
					id = SensorsGlobalManager.Instance.id80,
					type = SensorsGlobalManager.Instance.type80,
					distance = SensorsGlobalManager.Instance.distance80
				},
				line90 = new Line()
				{
					id = SensorsGlobalManager.Instance.id90,
					type = SensorsGlobalManager.Instance.type90,
					distance = SensorsGlobalManager.Instance.distance90
				},
				line100 = new Line()
				{
					id = SensorsGlobalManager.Instance.id100,
					type = SensorsGlobalManager.Instance.type100,
					distance = SensorsGlobalManager.Instance.distance100
				},
				line110 = new Line()
				{
					id = SensorsGlobalManager.Instance.id110,
					type = SensorsGlobalManager.Instance.type110,
					distance = SensorsGlobalManager.Instance.distance110
				},
				line120 = new Line()
				{
					id = SensorsGlobalManager.Instance.id120,
					type = SensorsGlobalManager.Instance.type120,
					distance = SensorsGlobalManager.Instance.distance120
				},
				line130 = new Line()
				{
					id = SensorsGlobalManager.Instance.id130,
					type = SensorsGlobalManager.Instance.type130,
					distance = SensorsGlobalManager.Instance.distance130
				},
				line140 = new Line()
				{
					id = SensorsGlobalManager.Instance.id140,
					type = SensorsGlobalManager.Instance.type140,
					distance = SensorsGlobalManager.Instance.distance140
				},
				line150 = new Line()
				{
					id = SensorsGlobalManager.Instance.id150,
					type = SensorsGlobalManager.Instance.type150,
					distance = SensorsGlobalManager.Instance.distance150
				},
				line160 = new Line()
				{
					id = SensorsGlobalManager.Instance.id160,
					type = SensorsGlobalManager.Instance.type160,
					distance = SensorsGlobalManager.Instance.distance160
				},
				line170 = new Line()
				{
					id = SensorsGlobalManager.Instance.id170,
					type = SensorsGlobalManager.Instance.type170,
					distance = SensorsGlobalManager.Instance.distance170
				},line180 = new Line()
				{
					id = SensorsGlobalManager.Instance.id180,
					type = SensorsGlobalManager.Instance.type180,
					distance = SensorsGlobalManager.Instance.distance180
				},
				line190 = new Line()
				{
					id = SensorsGlobalManager.Instance.id190,
					type = SensorsGlobalManager.Instance.type190,
					distance = SensorsGlobalManager.Instance.distance190
				},
				line200 = new Line()
				{
					id = SensorsGlobalManager.Instance.id200,
					type = SensorsGlobalManager.Instance.type200,
					distance = SensorsGlobalManager.Instance.distance200
				},
				line210 = new Line()
				{
					id = SensorsGlobalManager.Instance.id210,
					type = SensorsGlobalManager.Instance.type210,
					distance = SensorsGlobalManager.Instance.distance210
				},
				line220 = new Line()
				{
					id = SensorsGlobalManager.Instance.id220,
					type = SensorsGlobalManager.Instance.type220,
					distance = SensorsGlobalManager.Instance.distance220
				},
				line230 = new Line()
				{
					id = SensorsGlobalManager.Instance.id230,
					type = SensorsGlobalManager.Instance.type230,
					distance = SensorsGlobalManager.Instance.distance230
				},
				line240 = new Line()
				{
					id = SensorsGlobalManager.Instance.id240,
					type = SensorsGlobalManager.Instance.type240,
					distance = SensorsGlobalManager.Instance.distance240
				},
				line250 = new Line()
				{
					id = SensorsGlobalManager.Instance.id250,
					type = SensorsGlobalManager.Instance.type250,
					distance = SensorsGlobalManager.Instance.distance250
				},
				line260 = new Line()
				{
					id = SensorsGlobalManager.Instance.id260,
					type = SensorsGlobalManager.Instance.type260,
					distance = SensorsGlobalManager.Instance.distance260
				},
				line270 = new Line()
				{
					id = SensorsGlobalManager.Instance.id270,
					type = SensorsGlobalManager.Instance.type270,
					distance = SensorsGlobalManager.Instance.distance270
				},
				line280 = new Line()
				{
					id = SensorsGlobalManager.Instance.id280,
					type = SensorsGlobalManager.Instance.type280,
					distance = SensorsGlobalManager.Instance.distance280
				},
				line290 = new Line()
				{
					id = SensorsGlobalManager.Instance.id290,
					type = SensorsGlobalManager.Instance.type290,
					distance = SensorsGlobalManager.Instance.distance290
				},
				line300 = new Line()
				{
					id = SensorsGlobalManager.Instance.id300,
					type = SensorsGlobalManager.Instance.type300,
					distance = SensorsGlobalManager.Instance.distance300
				},
				line310 = new Line()
				{
					id = SensorsGlobalManager.Instance.id310,
					type = SensorsGlobalManager.Instance.type310,
					distance = SensorsGlobalManager.Instance.distance310
				},
				line320 = new Line()
				{
					id = SensorsGlobalManager.Instance.id320,
					type = SensorsGlobalManager.Instance.type320,
					distance = SensorsGlobalManager.Instance.distance320
				},
				line330 = new Line()
				{
					id = SensorsGlobalManager.Instance.id330,
					type = SensorsGlobalManager.Instance.type330,
					distance = SensorsGlobalManager.Instance.distance330
				},
				line340 = new Line()
				{
					id = SensorsGlobalManager.Instance.id340,
					type = SensorsGlobalManager.Instance.type340,
					distance = SensorsGlobalManager.Instance.distance340
				},
				line350 = new Line()
				{
					id = SensorsGlobalManager.Instance.id350,
					type = SensorsGlobalManager.Instance.type350,
					distance = SensorsGlobalManager.Instance.distance350
				},

				car_velocity = transform.GetComponent<Rigidbody>().velocity,
				car_angle = CarAngle,
				traffic_light =  trafficLights(), //get current roadblock?? -> cheat
				rain = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().isRainy,
				num_collidedObstacles = collidedObstacles.Count,
				num_collidedPedestrians = collidedObstacles.Count
			};

			Action newAction = new Action (){
				acceleration = (GetComponent("CarController") as CarController).AccelInput,
				steer_angle = (GetComponent("CarController") as CarController).CurrentSteerAngle
			};

			StateActionPair newPair = new StateActionPair() {
				action = newAction,
				state = currentState
			};

			string newPair_JSON = JsonUtility.ToJson(newPair,true);

			StreamWriter writer = new StreamWriter (logFilePath_json, true);
			writer.WriteLine (newPair_JSON);
			writer.Close();
	
//			collidedObjects.Clear();
		}
		public void changeFileLocation(string  PathAndName){
			logFilePath_csv = PathAndName;
			recordFlag = true;
		}
		[System.Serializable]
		public class Line
		{
			public string id;
			public string type;
			public float distance;
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
			public Vector3 car_velocity;
			public float car_angle;
			public bool traffic_light; //{true:red, false:green}
			public int num_collidedObstacles;
			public int num_collidedPedestrians;
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
			public State state;
			public Action action;

		}
	}
}
