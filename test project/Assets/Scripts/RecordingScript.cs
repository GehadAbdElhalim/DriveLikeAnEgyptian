using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace UnityStandardAssets.Vehicles.Car
{
	[RequireComponent(typeof(CarController))]
	public class RecordingScript : MonoBehaviour {

		public string logFilePath;
//		private CarController m_Car; // the car controller we want to use

//		public float SteeringAngle { get; set; b

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			//		JsonUtility.ToJson(obj[i]) + "*";

	//		GameObject skycar = GameObject.Find ("Car");
			State currentState = new State()
			{
				line0 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line5 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line10 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line15 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line20 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line25 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line30 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line35 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line40 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line45 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line50 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line55 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line60 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line65 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line70 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line75 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line80 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line85 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line90 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line95 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line100 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line105 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line110 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line115 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line120 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line125 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line130 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line135 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line140 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line145 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line150 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line155 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line160 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line165 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line170 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},
				line175 = new Line()
				{
					type = "v",
					position = new Vector3(0.0f,0.0f,0.0f)
				},

				car_velocity = (GetComponent("CarController") as CarController).CurrentSpeed,
//				traffic_light =  GameObject.Find ("") //get current roadblock
			};

			string currentState_JSON = JsonUtility.ToJson(currentState,true);
//			print (currentState_JSON+ "\t\n"	);
			Action newAction = new Action ();
//			print ((GetComponent("CarController") as CarController).CurrentSteerAngle);	
		}


		void logStateAction(string objects,string path){
			StreamWriter writer = new StreamWriter (path, true);
			writer.WriteLine (objects);
			writer.Close();
		}


		[System.Serializable]
		public class Line
		{
			public string type;
			public Vector3 position;
		}

		[System.Serializable]
		public class State
		{
			public Line line0;
			public Line line5;
			public Line line10;
			public Line line15;
			public Line line20;
			public Line line25;
			public Line line30;
			public Line line35;
			public Line line40;
			public Line line45;
			public Line line50;
			public Line line55;
			public Line line60;
			public Line line65;
			public Line line70;
			public Line line75;
			public Line line80;
			public Line line85;
			public Line line90;
			public Line line95;
			public Line line100;
			public Line line105;
			public Line line110;
			public Line line115;
			public Line line120;
			public Line line125;
			public Line line130;
			public Line line135;
			public Line line140;
			public Line line145;
			public Line line150;
			public Line line155;
			public Line line160;
			public Line line165;
			public Line line170;
			public Line line175;

			public bool rain;
			public float car_velocity;
			public float car_angle;
			public bool traffic_light;
		}

		[System.Serializable]
		public class Action
		{
			public float acceleration;
			public bool steer_left;
			public bool steer_right;

		}
	}
}
