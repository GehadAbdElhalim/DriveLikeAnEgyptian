using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

namespace UnityStandardAssets.Vehicles.Car
{
	[RequireComponent(typeof(CarController))]
	public class RecordingScript : MonoBehaviour {
		
		public List<Collider> collidedObjects = new List<Collider>();	

		public Line [] lines; 
		public float CarAngle;
		GameObject current_roadblock;

		string logFilePath_json;
		string logFilePath_csv;


		[Header("Sensors")]
		public float sensorLength = 20.0f;

		void Awake (){
			logFilePath_json = Application.dataPath + "/LogFiles/state-action.json";
			logFilePath_csv = Application.dataPath + "/LogFiles/state-action.csv";

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
//			InvokeRepeating("logStateAction_csv", 0.5f, 0.5f);	//logs to csv file
		}

		void OnCollisionEnter(Collision col) 
		{
			print (col.gameObject.layer.ToString ());
			if (col.gameObject.layer==11 && !collidedObjects.Contains(col.collider)) //ignoring normal obstacles' collisions (bumps and holes)
			{
				collidedObjects.Add(col.collider); 
			}
		}

		void OnCollisionStay(Collision col) {
			OnCollisionEnter(col); //same as enter
		}

		void Sensors(){
			RaycastHit hit;
			Vector3 sensorLowStartPos = transform.position;
			Vector3 sensorHighStartPos = transform.position;
			sensorLowStartPos.y = 0.2f;
			sensorHighStartPos.y = 0.5f;

			var layerMask = 1 << 12;//carbody2 layer
			layerMask = ~layerMask;

			if (Physics.Raycast (sensorLowStartPos, Quaternion.AngleAxis (0, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id0 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type0 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance0 = Vector3.Distance(transform.position, hit.transform.position);

				Debug.DrawLine (sensorLowStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id0 = null;
				SensorsGlobalManager.Instance.type0 = null;
				SensorsGlobalManager.Instance.distance0 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (10, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id10 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type10 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance10 = Vector3.Distance(transform.position, hit.transform.position);

				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id10 = null;
				SensorsGlobalManager.Instance.type10 = null;
				SensorsGlobalManager.Instance.distance10 = 0.0f;
			}

			if (Physics.Raycast (sensorLowStartPos, Quaternion.AngleAxis (20, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id20 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type20 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance20 = Vector3.Distance(transform.position, hit.transform.position);

				Debug.DrawLine (sensorLowStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id20 = null;
				SensorsGlobalManager.Instance.type20 = null;
				SensorsGlobalManager.Instance.distance20 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (30, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id30 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type30 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance30 = Vector3.Distance(transform.position, hit.transform.position);

				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id30 = null;
				SensorsGlobalManager.Instance.type30 = null;
				SensorsGlobalManager.Instance.distance30 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (40, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id40 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type40 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance40 = Vector3.Distance(transform.position, hit.transform.position);

				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id40 = null;
				SensorsGlobalManager.Instance.type40 = null;
				SensorsGlobalManager.Instance.distance40 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (50, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id50 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type50 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance50 = Vector3.Distance(transform.position, hit.transform.position);

				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id50 = null;
				SensorsGlobalManager.Instance.type50 = null;
				SensorsGlobalManager.Instance.distance50 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (60, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id60 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type60 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance60 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id60 = null;
				SensorsGlobalManager.Instance.type60 = null;
				SensorsGlobalManager.Instance.distance60 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (70, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id70 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type70 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance70 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id70 = null;
				SensorsGlobalManager.Instance.type70 = null;
				SensorsGlobalManager.Instance.distance70 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (80, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id80 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type80 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance80 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id80 = null;
				SensorsGlobalManager.Instance.type80 = null;
				SensorsGlobalManager.Instance.distance80 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (90, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id90 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type90 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance90 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id90 = null;
				SensorsGlobalManager.Instance.type90 = null;
				SensorsGlobalManager.Instance.distance90 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (100, transform.up) * transform.forward, out hit, sensorLength ,layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id100 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type100 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance100 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id100 = null;
				SensorsGlobalManager.Instance.type100 = null;
				SensorsGlobalManager.Instance.distance100 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (110, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id110 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type110 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance110 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id110 = null;
				SensorsGlobalManager.Instance.type110 = null;
				SensorsGlobalManager.Instance.distance110 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (120, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id120 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type120 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance120 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id120 = null;
				SensorsGlobalManager.Instance.type120 = null;
				SensorsGlobalManager.Instance.distance120 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (130, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id130 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type130 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance130 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id130 = null;
				SensorsGlobalManager.Instance.type130 = null;
				SensorsGlobalManager.Instance.distance130 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (140, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id140 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type140 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance140 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id140 = null;
				SensorsGlobalManager.Instance.type140 = null;
				SensorsGlobalManager.Instance.distance140 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (150, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id150 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type150 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance150 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id150 = null;
				SensorsGlobalManager.Instance.type150 = null;
				SensorsGlobalManager.Instance.distance150 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (160, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id160 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type160 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance160 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id160 = null;
				SensorsGlobalManager.Instance.type160 = null;
				SensorsGlobalManager.Instance.distance160 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (170, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id170 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type170 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance170 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id170 = null;
				SensorsGlobalManager.Instance.type170 = null;
				SensorsGlobalManager.Instance.distance170 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (180, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id180 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type180 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance180 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id180 = null;
				SensorsGlobalManager.Instance.type180 = null;
				SensorsGlobalManager.Instance.distance180 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (190, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id190 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type190 =  (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance190 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id190 = null;
				SensorsGlobalManager.Instance.type190 = null;
				SensorsGlobalManager.Instance.distance190 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (200, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id200 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type200 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance200 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id200 = null;
				SensorsGlobalManager.Instance.type200 = null;
				SensorsGlobalManager.Instance.distance200 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (210, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id210 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type210 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance210 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id210 = null;
				SensorsGlobalManager.Instance.type210 = null;
				SensorsGlobalManager.Instance.distance210 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (220, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id220 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type220 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance220 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id220 = null;
				SensorsGlobalManager.Instance.type220 = null;
				SensorsGlobalManager.Instance.distance220 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (230, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id230 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type230 =  (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance230 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id230 = null;
				SensorsGlobalManager.Instance.type230 = null;
				SensorsGlobalManager.Instance.distance230 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (240, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id240 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type240 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance240 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id240 = null;
				SensorsGlobalManager.Instance.type240 = null;
				SensorsGlobalManager.Instance.distance240 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (250, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id250 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type250 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance250 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id250 = null;
				SensorsGlobalManager.Instance.type250 = null;
				SensorsGlobalManager.Instance.distance250 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (260, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id260 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type260 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance260 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id260 = null;
				SensorsGlobalManager.Instance.type260 = null;
				SensorsGlobalManager.Instance.distance260 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (270, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id270 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type270 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance270 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id270 = null;
				SensorsGlobalManager.Instance.type270 = null;
				SensorsGlobalManager.Instance.distance270 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (280, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id280 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type280 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance280 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id280 = null;
				SensorsGlobalManager.Instance.type280 = null;
				SensorsGlobalManager.Instance.distance280 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (290, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id290 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type290 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance290 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id290 = null;
				SensorsGlobalManager.Instance.type290 = null;
				SensorsGlobalManager.Instance.distance290 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (300, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id300 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type300 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance300 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id300 = null;
				SensorsGlobalManager.Instance.type300 = null;
				SensorsGlobalManager.Instance.distance300 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (310, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id310 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type310 =  (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance310 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id310 = null;
				SensorsGlobalManager.Instance.type310 = null;
				SensorsGlobalManager.Instance.distance310 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (320, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id320 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type320 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance320 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id320 = null;
				SensorsGlobalManager.Instance.type320 = null;
				SensorsGlobalManager.Instance.distance320 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (330, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id330 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type330 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance330 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id330 = null;
				SensorsGlobalManager.Instance.type330 = null;
				SensorsGlobalManager.Instance.distance330 = 0.0f;
			}

			if (Physics.Raycast (sensorLowStartPos, Quaternion.AngleAxis (340, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id340 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type340 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance340 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorLowStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id340 = null;
				SensorsGlobalManager.Instance.type340 = null;
				SensorsGlobalManager.Instance.distance340 = 0.0f;
			}

			if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (350, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
				SensorsGlobalManager.Instance.id350 = hit.collider.GetInstanceID ().ToString();
				SensorsGlobalManager.Instance.type350 = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
				SensorsGlobalManager.Instance.distance350 = Vector3.Distance(transform.position, hit.transform.position);
			
				Debug.DrawLine (sensorHighStartPos, hit.point);
			} else {
				SensorsGlobalManager.Instance.id350 = null;
				SensorsGlobalManager.Instance.type350 = null;
				SensorsGlobalManager.Instance.distance350 = 0.0f;
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

		// Update is called once per frame
		void Update () {
			Sensors(); 
						current_roadblock = getRoadBlock ();
			print(transform.GetComponent<Rigidbody>().velocity);
			print(transform.InverseTransformDirection(transform.GetComponent<Rigidbody>().velocity));
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
			string[] rowDataTemp = new string[117];
			//
			int i = 0; //line0
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id0;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type0; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance0.ToString();

			//line10
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id10;
			rowDataTemp[i++]  = SensorsGlobalManager.Instance.type10; 
			rowDataTemp[i++]  = SensorsGlobalManager.Instance.distance10.ToString();

			//line20
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id20;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type20; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance20.ToString();

			//line30
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id30;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type30; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance30.ToString();

			//line40
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id40;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type40; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance40.ToString();

			//line50
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id50;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type50; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance50.ToString();

			//line60
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id60;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type60; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance60.ToString();

			//line70
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id70;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type70; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance70.ToString();

			//line80
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id80;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type80; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance80.ToString();

			//line90
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id90;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type90; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance90.ToString();

			//line100
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id100;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type100; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance100.ToString();

			//line110
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id110;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type110; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance110.ToString();

			//line120
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id120;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type120; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance120.ToString();

			//line130
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id130;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type130; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance130.ToString();

			//line140
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id140;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type140; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance140.ToString();

			//line150
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id150;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type150; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance150.ToString();

			//line160
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id160;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type160; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance160.ToString();

			//line170
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id170;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type170; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance170.ToString();

			//line180
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id180;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type180; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance180.ToString();

			//line190
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id190;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type190; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance190.ToString();

			//line200
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id200;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type200; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance200.ToString();

			//line210
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id210;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type210; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance210.ToString();

			//line220
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id220;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type220; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance220.ToString();

			//line230
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id230;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type230; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance230.ToString();

			//line240
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id240;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type240; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance240.ToString();

			//line250
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id250;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type250; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance250.ToString();

			//line260
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id260;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type260; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance260.ToString();

			//line270
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id270;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type270; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance270.ToString();

			//line280
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id280;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type280; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance280.ToString();

			//line290
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id290;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type290; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance290.ToString();

			//line300
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id300;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type300; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance300.ToString();

			//line310
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id310;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type310; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance310.ToString();

			//line320
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id320;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type320; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance320.ToString();

			//line330
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id330;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type330; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance330.ToString();

			//line340
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id340;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type340; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance340.ToString();

			//line350
			rowDataTemp[i++] = SensorsGlobalManager.Instance.id350;
			rowDataTemp[i++] = SensorsGlobalManager.Instance.type350; 
			rowDataTemp[i++] = SensorsGlobalManager.Instance.distance350.ToString();


			rowDataTemp[i++] = transform.GetComponent<Rigidbody>().velocity.x.ToString();
			rowDataTemp[i++] = transform.GetComponent<Rigidbody>().velocity.y.ToString();
			rowDataTemp[i++] = transform.GetComponent<Rigidbody>().velocity.z.ToString();
			rowDataTemp[i++] = (transform.eulerAngles.y-current_roadblock.transform.eulerAngles.y).ToString();
			CarAngle = transform.eulerAngles.y - current_roadblock.transform.eulerAngles.y;
			rowDataTemp[i++] = trafficLights().ToString();
			rowDataTemp[i++] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().isRainy.ToString();
			rowDataTemp [i++] = collidedObjects.Count.ToString();

			//ACTION

//			rowDataTemp [i++] = ((GetComponent ("CarController") as CarController).AccelInput).ToString();
//			rowDataTemp [i++] = ((GetComponent ("CarController") as CarController).CurrentSteerAngle).ToString();

			rowDataTemp[i++] = Input.GetAxis("Vertical").ToString();
			rowDataTemp[i++] = Input.GetAxis("Horizontal").ToString();

			using (FileStream fs = new FileStream(logFilePath_csv,FileMode.Append, FileAccess.Write))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine(string.Join (",", rowDataTemp));
				
				}
			}
			collidedObjects.Clear();
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
				car_angle = transform.eulerAngles.y-current_roadblock.transform.eulerAngles.y,
				traffic_light =  trafficLights(), //get current roadblock?? -> cheat
				rain = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().isRainy,
				num_collisions = collidedObjects.Count
			};

			//			print (currentState_JSON+ "\t\n"	);
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
	
			collidedObjects.Clear();
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
			public int num_collisions;
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
