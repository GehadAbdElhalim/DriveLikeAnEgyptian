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
	public class RecordingScriptAbdelrahman : MonoBehaviour
	{
		public bool SpawnAd;
		public bool isAd;
		public static Transform OtherCar ;
		public List<Collider> collidedObstacles = new List<Collider>();	
		public List<Collider> collidedPedestrians = new List<Collider>();	

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
			// logFilePath_csv = chooseFile(Application.dataPath + "/LogFiles/normalCar");
			if(SpawnAd)
			RecordingScriptAbdelrahman.OtherCar = GetComponent<Transform>();
			// GameObject.Find ("Front Facing Camera").enabled = true;

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

		public enum ObstacleType
		{
			Roadblock = 0,
			Pedestrian = 1,
			Construction = 2
		};

		// Use this for initialization
		void Start ()
		{
			current_roadblock = getRoadBlock ();
			// Only one line can be uncommented at once
			// InvokeRepeating("logStateAction_json", 0.5f, 0.5f);	//logs to json file
			if(startLogging)
			InvokeRepeating("logStateAction_csv", 0.05f, 0.05f);	//logs to csv file
		}

		void OnCollisionEnter(Collision col) 
		{
			if (col.gameObject.name == "Finish(Clone)")
			{
                if(GameObject.Find ("NetworkManager").GetComponent<networkSocket> ().myfinished == false)
				{
                    GameObject.Find ("NetworkManager").GetComponent<networkSocket> ().myfinished = true;
				}
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

		void OnCollisionStay(Collision col)
		{
			OnCollisionEnter(col); //same as enter
		}

		public void OnCollisionExit(Collision col)
		{
			if (collidedObstacles.Contains(col.collider)) //ignoring normal obstacles' collisions (bumps and holes)
			{
				collidedObstacles.Remove (col.collider); 
			}
			if (collidedPedestrians.Contains(col.collider)) //ignoring normal obstacles' collisions (bumps and holes)
			{
				collidedPedestrians.Remove (col.collider); 
			}
		}

		void Sensors()
		{
			RaycastHit hit;
			Vector3 sensorLowStartPos = transform.position;
			Vector3 sensorHighStartPos = transform.position;
			sensorLowStartPos.y = 0.2f;
			sensorHighStartPos.y = 0.6f;

			var layerMask = 1 << 12; // carbody2 layer
			layerMask = ~layerMask;

			// bool calculated_angle = false;

			/*
			sensorHighStartPos: 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200, 210, 220, 230, 240, 250, 260, 270, 280, 290, 300, 310, 320, 330, 350
			sensorLowStartPos: 0, 10, 20, 340
			 */
			for(int i = 0; i < 36; i++)
			{
				if(i == 0 || i == 1 || i == 2 || i == 34)
				{
					if (Physics.Raycast (sensorLowStartPos, Quaternion.AngleAxis (i * 10, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
						SensorManager.Instance.sensors[i].id = hit.collider.GetInstanceID ().ToString();
						SensorManager.Instance.sensors[i].type = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
						SensorManager.Instance.sensors[i].distance = Vector3.Distance(transform.position, hit.point);

						Debug.DrawLine (sensorLowStartPos, hit.point);
					} else {
						SensorManager.Instance.sensors[i].id = null;
						SensorManager.Instance.sensors[i].type = null;
						SensorManager.Instance.sensors[i].distance = 0.0f;
					}
				} else {
					if (Physics.Raycast (sensorHighStartPos, Quaternion.AngleAxis (i * 10, transform.up) * transform.forward, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag!="Player") {
						SensorManager.Instance.sensors[i].id = hit.collider.GetInstanceID ().ToString();
						SensorManager.Instance.sensors[i].type = (hit.collider.gameObject.tag == "RoadBlock")?"RoadBlock":(hit.collider.gameObject.tag == "passanger")?"Pedestrian":hit.collider.name;
						SensorManager.Instance.sensors[i].distance = Vector3.Distance(transform.position, hit.point);

						Debug.DrawLine (sensorHighStartPos, hit.point);
					} else {
						SensorManager.Instance.sensors[i].id = null;
						SensorManager.Instance.sensors[i].type = null;
						SensorManager.Instance.sensors[i].distance = 0.0f;
					}
				}
			}

			Vector3 sensorPos = transform.position;
			Ray r = new Ray(sensorPos, transform.right);
			sensorPos.y = 0.02f;
			layerMask = 1 << 8; // carbody2 layer
			// print (current_roadblock.name);
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

		public bool trafficLights()
		{
			
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

		string chooseFile(string dirPath)
		{
				string [] filesArr = Directory.GetFiles(dirPath);
				List<int> FilesNumber = new List<int>();
				for(int i = 0; i < filesArr.Length; i++)
				{
					string [] dirSplit = filesArr[i].Split(new string[]{"\\"}, StringSplitOptions.None);
					if(dirSplit[dirSplit.Length-1].EndsWith(".csv"))
					{
						if(dirSplit[dirSplit.Length-1].Length >4)
						{
							String Filename =dirSplit[dirSplit.Length-1].Trim().Substring(1,dirSplit[dirSplit.Length-1].Length-5); 
							int tempNum = 0;
							if(int.TryParse(Filename ,out tempNum  ))
							{
								FilesNumber.Add(tempNum);
							}
						}
					}
				}
				int maxNum = 0;
				for(int i=0;i<FilesNumber.Count;i++)
				{
					maxNum = Math.Max (FilesNumber[i],maxNum);
				}
				maxNum += 1;
				if(startLogging)
				{
					File.Create(dirPath + "\\" + 2+maxNum.ToString() + ".csv");
				}
				return  dirPath + "\\" +2+ maxNum.ToString() + ".csv";		
		}

		// Update is called once per frame
		void Update()
		{
			Sensors();  
			if(SpawnAd)
			{
				RecordingScriptAbdelrahman.OtherCar = GetComponent<Transform>();
			}
		}

		public GameObject getRoadBlock()
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, Vector3.down, out hit, transform.position.y) && hit.collider.gameObject.tag == "RoadBlock")
			{
				return hit.collider.gameObject;
			} else {
				return current_roadblock;
			}
		}

		void logStateAction_csv()
		{
			string[] rowDataTemp = new string[120];

			int i = 0; // line0

			for(int j = 0; j < 36; j++)
			{
				rowDataTemp[i++] = SensorManager.Instance.sensors[j].id;
				rowDataTemp[i++] = SensorManager.Instance.sensors[j].type;
				rowDataTemp[i++] = SensorManager.Instance.sensors[j].distance.ToString();				
			}

			/*rowDataTemp[i++] = transform.GetComponent<Rigidbody>().velocity.x.ToString();
			rowDataTemp[i++] = transform.GetComponent<Rigidbody>().velocity.y.ToString();*/
			rowDataTemp[i++] = this.transform.InverseTransformDirection(this.transform.GetComponent<Rigidbody>().velocity).z.ToString();//108
			rowDataTemp[i++] = (CarAngle* 180/10/ Mathf.PI).ToString() ;//109
			// CarAngle = transform.eulerAngles.y - current_roadblock.transform.eulerAngles.y;
			rowDataTemp[i++] = trafficLights().ToString();//110
			rowDataTemp[i++] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().isRainy.ToString();//111
			// rowDataTemp [i++] = collidedObjects.Count.ToString();
			rowDataTemp[i++] = collidedObstacles.Count.ToString();//112
            rowDataTemp [i++] = collidedPedestrians.Count.ToString();//113
            rowDataTemp [i++] = collidedPavement.ToString();//114

            if (GameObject.Find ("CarDirection").GetComponent<testScript> ().DirectionAngle <= 80)
			{
                rowDataTemp [i++] = true.ToString();//115
            } else {
                rowDataTemp [i++] = false.ToString();
            }

			if(!SpawnAd)
			{
				rowDataTemp [i++] = 0f.ToString ();//116
				rowDataTemp [i++] = 0f.ToString ();//117
			} else {
				if(OtherCar.Equals(null))
				{
					rowDataTemp [i++] = 0.ToString();	
				} else {
					rowDataTemp [i++] = Vector3.Distance(transform.position,OtherCar.position).ToString();
				}
				Vector3 [] Nodes =  GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().getMiddleNodes().ToArray();
				Vector3 NodeToThisPostion =Nodes [0]; int NodeToThisIndex = 0;
				Vector3 NodeToOtherPostion=Nodes [0]; int NodeToOtherIndex = 0;

				for(int n = 0; n < Nodes.Length; n++)
				{
					if(Vector3.Distance(transform.position,Nodes[n]) < Vector3.Distance(NodeToThisPostion,transform.position))
					{
						NodeToThisPostion = Nodes[n];
						NodeToThisIndex = n;
					}
					if(Vector3.Distance(OtherCar.position,Nodes[n]) < Vector3.Distance(NodeToOtherPostion,OtherCar.position))
					{
						NodeToOtherPostion = Nodes[n];
						NodeToOtherIndex = n;
					}
				}
				
				if(NodeToThisIndex >NodeToOtherIndex )
				{
					rowDataTemp [i++] = (1).ToString ();
				} else if(NodeToThisIndex ==NodeToOtherIndex) {
					rowDataTemp [i++] = (0).ToString ();
				} else {
					rowDataTemp [i++] = (-1).ToString();
				}
				print(rowDataTemp[rowDataTemp.Length-2]);
				print(rowDataTemp[rowDataTemp.Length-1]);
			}
			//ACTION

			// rowDataTemp [i++] = ((GetComponent ("CarController") as CarController).AccelInput).ToString();
			// rowDataTemp [i++] = ((GetComponent ("CarController") as CarController).CurrentSteerAngle).ToString();

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

		public void changeFileLocation(string  PathAndName)
		{
			logFilePath_csv = PathAndName;
			recordFlag = true;
		}
	}
}
