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

//		public float SteeringAngle { get; set; }

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			//		JsonUtility.ToJson(obj[i]) + "*";

	//		GameObject skycar = GameObject.Find ("Car");
			State currentState = new State()
			{
				car_velocity = (GetComponent("CarController") as CarController).CurrentSpeed
			};

			string json = JsonUtility.ToJson(currentState,true);
			print (json+ "\t\n");
			Action newAction = new Action ();
//			print ((GetComponent("CarController") as CarController).CurrentSteerAngle);	
		}


		void logStateAction(string objects,string path){
			StreamWriter writer = new StreamWriter (path, true);
			writer.WriteLine (objects);
			writer.Close();
		}


		[System.Serializable]
		public class line
		{
			string name;
			Vector3 position;
		}

		[System.Serializable]
		public class State
		{
			public line line0;
			public line line5;
			public line line10;
			public line line15;
			public line line20;
			public line line25;
			public line line30;
			public line line35;
			public line line40;
			public line line45;
			public line line50;
			public line line55;
			public line line60;
			public line line65;
			public line line70;
			public line line75;
			public line line80;
			public line line85;
			public line line90;
			public line line95;
			public line line100;
			public line line105;
			public line line110;
			public line line115;
			public line line120;
			public line line125;
			public line line130;
			public line line135;
			public line line140;
			public line line145;
			public line line150;
			public line line155;
			public line line160;
			public line line165;
			public line line170;
			public line line175;

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
