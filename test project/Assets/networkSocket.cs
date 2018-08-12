using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;
#if UNITY_EDITOR 
using UnityEditor;
using UnityEditor.SceneManagement;
#endif


 

public class networkSocket : MonoBehaviour
{
    public String host = "localhost";
    public Int32 port = 65444 ;

	int a;
	float[] x;
	int stuck_counter;
	float last_velocity;
	bool crashed;
	bool ready;
	//bool correct_direction;
	//Vector3[] Waypoints;
	//int i;
	//bool restarted;
	public bool adFlag;
	public bool done;
	public bool quit;
	public bool A3C;

    internal Boolean socket_ready = false;
    internal String input_buffer = "";
    TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;

	Text [] actions_UI = new Text[9];
	//bool finished = false;
	public bool myfinished = false;
	 
    void UpdateMe()
    {	
		if (ready) {
			
			String Mymessage = readSocket ();
			print (Mymessage);
			//print (myfinished);
			print ("GUC");
			if (Mymessage == "Send the starting State") {
				// Debug.Log ("arrived");
				/*else if (myfinished) {
					myfinished = false;
					writeSocket ("finished");
				}*/
				if (quit) {
					quit = false;
					writeSocket ("quit");
				}
				if (State_is_done () || done) {
					done = false;
					writeSocket ("done");
				} else if (myfinished) {
					myfinished = false;
					writeSocket ("finished");
				} else {
					// Debug.Log ("else part");
					writeSocket (getCurrentState ());
				}
			} else if (Mymessage == "restart") {
				writeSocket ("oksh");
				//restarted = true;
				#if UNITY_EDITOR
				EditorSceneManager.LoadScene ("demo");
				#endif
				ready = false;
				Invoke ("activate_ready", 3f);
				//Waypoints = GameObject.Find ("StreetManger (1)").GetComponent<CityDesgin1> ().Waypoints;
			} else if (Mymessage == "Bye!") {
//			Application.Quit();
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				#endif
				
			} else {
				//if (!A3C) {
				print(Mymessage.Length);
				a = Int32.Parse (Mymessage.Substring (10, 1));
				// Debug.Log (a);
				DoAction (a);
				writeSocket ("action done");
				/*} else {
					string[] y;
					string[] z = new string[2];
					y = message.Split (',');
					//z [0] = Math.Round (Convert.ToDouble(y [0]), 4).ToString ();
					//z [1] = Math.Round (Convert.ToDouble (y [1]), 4).ToString ();
					//Debug.Log (z[0]);
					//x [0] = float.Parse (z [0],System.Globalization.CultureInfo.InvariantCulture);
					//x [1] = float.Parse (z [1],System.Globalization.CultureInfo.InvariantCulture);
					x[0] = Convert.ToSingle(y[0]);
					x[1] = Convert.ToSingle(y[1]);
					DoActionA3C (x[0],x[1]);
					writeSocket ("action done");
				}*/
			}

			if ((float)(Math.Round ((double)GameObject.Find ("Car(Clone)").transform.GetComponent<Rigidbody> ().velocity.z, 2)) == (float)(Math.Round ((double)last_velocity, 2))) {
				if ((float)(Math.Round ((double)last_velocity, 2)) == 0f) {
					stuck_counter++;
				}
			} else {
				stuck_counter = 0;
			}
		} else {
			myfinished = false;
		}
    }

	public void activate_ready(){
		ready = true;
	}

	void Update(){
		if (ready) {
				DoAction (a);
		}
		/*if(restarted){
			i = 1;
		}
		Vector3 CarToWaypoint = GameObject.Find ("Car(Clone)").transform.position - Waypoints [i];

		if(CarToWaypoint.magnitude < 7 ){
			i += 1;
		}

		Vector3 direction = Waypoints [i] - Waypoints [i - 1];
		print ("i = " + i);
		if (Vector3.Angle (GameObject.Find ("Car(Clone)").transform.forward, direction) <= 80) {
			correct_direction = true;
		} else {
			correct_direction = false;
		}*/
	}

	/*void putWaypoints(){
		if (restarted) {
			Waypoints = GameObject.FindGameObjectWithTag ("manager").GetComponent<CityDesgin1> ().Waypoints;
			restarted = false;
		}
	}*/

    void Awake()
    {
		QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
		//Waypoints = GameObject.FindGameObjectWithTag("manager").GetComponent<CityDesgin1> ().Waypoints;
		actions_UI[0] = GameObject.Find ("Canvas/None").GetComponent<Text> ();
		actions_UI[1] = GameObject.Find ("Canvas/Forward").GetComponent<Text> ();
		actions_UI[2] = GameObject.Find ("Canvas/Backward").GetComponent<Text> ();
		actions_UI[3] = GameObject.Find ("Canvas/Right").GetComponent<Text> ();
		actions_UI[4] = GameObject.Find ("Canvas/Left").GetComponent<Text> ();
		actions_UI[5] = GameObject.Find ("Canvas/Forward_Right").GetComponent<Text> ();
		actions_UI[6] = GameObject.Find ("Canvas/Backward_Right").GetComponent<Text> ();
		actions_UI[7] = GameObject.Find ("Canvas/Forward_Left").GetComponent<Text> ();
		actions_UI[8] = GameObject.Find ("Canvas/Backward_Left").GetComponent<Text> ();

		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Network");
		if (objs.Length > 1) {
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);
        setupSocket();
		//i = 1;
		a = 0;
		x = new float[2];
		stuck_counter = 0;
		crashed = done;
		ready = true;
		//restarted = true;
        InvokeRepeating("UpdateMe", 3f, 0.025f);
		InvokeRepeating("putWaypoints", 3f, 0.1f);
    }

    void OnApplicationQuit()
    {
        closeSocket();
    }

    public void setupSocket()
    {
        try
        {	print ("this port is "+ port);
			port = 65444;
            tcp_socket = new TcpClient(host, port);

            net_stream = tcp_socket.GetStream();
            socket_writer = new StreamWriter(net_stream);
            socket_reader = new StreamReader(net_stream);

            socket_ready = true;
        }
        catch (Exception e)
        {
        	// Something went wrong
            Debug.Log("Socket error: " + e);
        }
    }

    public void writeSocket(string line)
    {
        if (!socket_ready)
            return;
            
        line = line + "\r\n";
        socket_writer.Write(line);
        socket_writer.Flush();
    }

    public String readSocket()
    {
        if (!socket_ready)
        {
            Debug.Log("socket not ready");
            return "";
        }

        if (net_stream.DataAvailable)
            return socket_reader.ReadLine();

        return "";
    }

    public void closeSocket()
    {
        if (!socket_ready)
            return;

        socket_writer.Close();
        socket_reader.Close();
        tcp_socket.Close();
        socket_ready = false;
	}

	//public void set_finished(){
	//	finished = true;
	//}

	public void set_myfinished(){
		myfinished = true;
	}


	public String getCurrentState(){
		float[] state_output = new float[82];
		/*for (int i = 0; i <= 35; i++) {
			Debug.Log (i);
			state_output [i] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.GetComponent<RecordingScript2> ().lines [i].distance;
		}*/

		//Sensor obstacle distance
		int i = 0;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type0);
		state_output[i++] = SensorsGlobalManager.Instance.distance0;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type10);
		state_output[i++] = SensorsGlobalManager.Instance.distance10;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type20);
		state_output[i++] = SensorsGlobalManager.Instance.distance20;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type30);
		state_output[i++] = SensorsGlobalManager.Instance.distance30;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type40);
		state_output[i++] = SensorsGlobalManager.Instance.distance40;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type50);
		state_output[i++] = SensorsGlobalManager.Instance.distance50;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type60);
		state_output[i++] = SensorsGlobalManager.Instance.distance60;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type70);
		state_output[i++] = SensorsGlobalManager.Instance.distance70;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type80);
		state_output[i++] = SensorsGlobalManager.Instance.distance80;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type90);
		state_output[i++] = SensorsGlobalManager.Instance.distance90;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type100);
		state_output[i++] = SensorsGlobalManager.Instance.distance100;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type110);
		state_output[i++] = SensorsGlobalManager.Instance.distance110;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type120);
		state_output[i++] = SensorsGlobalManager.Instance.distance120;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type130);
		state_output[i++] = SensorsGlobalManager.Instance.distance130;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type140);
		state_output[i++] = SensorsGlobalManager.Instance.distance140;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type150);
		state_output[i++] = SensorsGlobalManager.Instance.distance150;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type160);
		state_output[i++] = SensorsGlobalManager.Instance.distance160;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type170);
		state_output[i++] = SensorsGlobalManager.Instance.distance170;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type180);
		state_output[i++] = SensorsGlobalManager.Instance.distance180;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type190);
		state_output[i++] = SensorsGlobalManager.Instance.distance190;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type200);
		state_output[i++] = SensorsGlobalManager.Instance.distance200;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type210);
		state_output[i++] = SensorsGlobalManager.Instance.distance210;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type220);
		state_output[i++] = SensorsGlobalManager.Instance.distance220;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type230);
		state_output[i++] = SensorsGlobalManager.Instance.distance230;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type240);
		state_output[i++] = SensorsGlobalManager.Instance.distance240;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type250);
		state_output[i++] = SensorsGlobalManager.Instance.distance250;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type260);
		state_output[i++] = SensorsGlobalManager.Instance.distance260;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type270);
		state_output[i++] = SensorsGlobalManager.Instance.distance270;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type280);
		state_output[i++] = SensorsGlobalManager.Instance.distance280;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type290);
		state_output[i++] = SensorsGlobalManager.Instance.distance290;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type300);
		state_output[i++] = SensorsGlobalManager.Instance.distance300;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type310);
		state_output[i++] = SensorsGlobalManager.Instance.distance310;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type320);
		state_output[i++] = SensorsGlobalManager.Instance.distance320;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type330);
		state_output[i++] = SensorsGlobalManager.Instance.distance330;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type340);
		state_output[i++] = SensorsGlobalManager.Instance.distance340;
		state_output [i++] = get_type(SensorsGlobalManager.Instance.type350);
		state_output[i++] = SensorsGlobalManager.Instance.distance350;

		//Car velocity
		//state_output [36] = (float) GameObject.Find("Car(Clone)").transform.InverseTransformDirection(GameObject.Find("Car(Clone)").transform.GetComponent<Rigidbody>().velocity).x;
		//state_output [37] = (float) GameObject.Find("Car(Clone)").transform.InverseTransformDirection(GameObject.Find("Car(Clone)").transform.GetComponent<Rigidbody>().velocity).y;
		state_output [72] = (float) GameObject.Find("Car(Clone)").transform.InverseTransformDirection(GameObject.Find("Car(Clone)").transform.GetComponent<Rigidbody>().velocity).z;
		// Debug.Log(state_output [38]);
		last_velocity = state_output [72];
		//state_output [39] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.transform.eulerAngles.y - GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().Car.GetComponent<RecordingScript2>().getRoadBlock().transform.eulerAngles.y;

		//Car Angle
		state_output [73] = GameObject.Find("Car(Clone)").GetComponent<RecordingScript2>().CarAngle * (180/Mathf.PI) / 10;

		if (GameObject.Find("Car(Clone)").GetComponent<RecordingScript2> ().trafficLights ()) {
			state_output [74] = 1f;
		} else {
			state_output [74] = 0f;
		}

		if (GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().isRainy) {
			state_output [75] = 1f;
		} else {
			state_output [75] = 0f;
		}

		state_output [76] = (float) GameObject.Find("Car(Clone)").GetComponent<RecordingScript2> ().collidedObstacles.Count;
		 
		//just for step one training
		if(state_output[76] > 0){
			crashed = true;
		}
		
		state_output [77] = (float) GameObject.Find("Car(Clone)").GetComponent<RecordingScript2> ().collidedPedestrians.Count;

		//Pavement-hit
		if (GameObject.Find("Car(Clone)").GetComponent<RecordingScript2> ().collidedPavement) {
			state_output [78] = 1f;
		} else {
			state_output [78] = 0f;
		}

		if (GameObject.Find("CarDirection").GetComponent<testScript>().DirectionAngle <= 80) {
			state_output [79] = 1f;
		} else {
			state_output [79] = 0f;
		}

		//adversarial distance
		state_output[80] = 0f;

		//adversarial ahead or not
		state_output[81] = 0f;

		if(adFlag){
		//adversarial distance
        state_output[80] = (float) (GameObject.Find("Car(Clone)").transform.position - GameObject.Find("CarADNav(Red)(Clone)").transform.position).magnitude ;

        //adversarial ahead or not

        Vector3 [] Nodes =  GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().getMiddleNodes().ToArray();
        Vector3 NodeToThisPostion =Nodes [0]; int NodeToThisIndex = 0;
        Vector3 NodeToOtherPostion=Nodes [0]; int NodeToOtherIndex = 0;

        Transform OtherCar = GameObject.Find ("CarADNav(Red)(Clone)").transform; 

        for(int n=0;n<Nodes.Length;n++){
            if(Vector3.Distance(GameObject.Find("Car(Clone)").transform.position,Nodes[n]) < Vector3.Distance(NodeToThisPostion,GameObject.Find("Car(Clone)").transform.position)){
                NodeToThisPostion = Nodes[n];
                NodeToThisIndex = n;
            }
            if(Vector3.Distance(OtherCar.position,Nodes[n]) < Vector3.Distance(NodeToOtherPostion,OtherCar.position)){
                NodeToOtherPostion = Nodes[n];
                NodeToOtherIndex = n;
            }
        }

        if(NodeToThisIndex > NodeToOtherIndex )
        {
            state_output [81] = 1f;

        }else if(NodeToThisIndex == NodeToOtherIndex){
            state_output [81] = 0f;
        }else {
            state_output [81] = -1f;
        }
		}
		String output = "";
		for (int j = 0 ; j < state_output.Length ; j++){
			output = output + Convert.ToString (state_output [j]) + ",";
		}

		return output;
	}

	public bool State_is_done() //COMPLETE THE CODE
	{
		if (GameObject.Find ("Car(Clone)").transform.position.y < -3) {
			return true;
		} else if (stuck_counter >= 200 && GameObject.Find("Car(Clone)").GetComponent<RecordingScript2> ().trafficLights () == false) {
			stuck_counter = 0;
			return true;
		} else if(crashed){
			crashed = false;
			return true;
		} else {
			return false;
		}
	}

	public void DoAction(int action_number) 
	{
		// Debug.Log ("hi action");
		for (int i = 0; i < actions_UI.Length; i++) {
			actions_UI[i].fontSize = 16;
			actions_UI[i].fontStyle = FontStyle.Normal;
			actions_UI[i].color = Color.white;
		}

		actions_UI[action_number].fontSize = 18; 
		actions_UI[action_number].fontStyle = FontStyle.Bold;
		actions_UI[action_number].color = Color.red;

		if (action_number == 0) {
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().SteeringAngle = 0f;
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().Acceleration = 0f;
			//CrossPlatformInputManager.SetAxis ("Horizontal", 0f);
			//CrossPlatformInputManager.SetAxis ("Vertical", 0f);
		}

		if (action_number == 1) {
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().SteeringAngle = 0f;
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().Acceleration = 1f;
			//CrossPlatformInputManager.SetAxis ("Horizontal", 0f);
			//CrossPlatformInputManager.SetAxis ("Vertical", 1f);
		}

		if (action_number == 2) {
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().SteeringAngle = 0f;
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().Acceleration = -1f;
			//CrossPlatformInputManager.SetAxis ("Horizontal", 0f);
			//CrossPlatformInputManager.SetAxis ("Vertical", -1f);
		}

		if (action_number == 3) {
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().SteeringAngle = 1f;
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().Acceleration = 0f;
			//CrossPlatformInputManager.SetAxis ("Horizontal", 1f);
			//CrossPlatformInputManager.SetAxis ("Vertical", 0f);
		}

		if (action_number == 4) {
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().SteeringAngle = -1f;
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().Acceleration = 0f;
			//CrossPlatformInputManager.SetAxis ("Horizontal", -1f);
			//CrossPlatformInputManager.SetAxis ("Vertical", 0f);
		}

		if (action_number == 5) {
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().SteeringAngle = 1f;
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().Acceleration = 1f;
			//CrossPlatformInputManager.SetAxis ("Horizontal", 1f);
			//CrossPlatformInputManager.SetAxis ("Vertical", 1f);
		}

		if (action_number == 6) {
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().SteeringAngle = 1f;
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().Acceleration = -1f;
			//CrossPlatformInputManager.SetAxis ("Horizontal", 1f);
			//CrossPlatformInputManager.SetAxis ("Vertical", -1f);
		}

		if (action_number == 7) {
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().SteeringAngle = -1f;
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().Acceleration = 1f;
			//CrossPlatformInputManager.SetAxis ("Horizontal", -1f);
			//CrossPlatformInputManager.SetAxis ("Vertical", 1f);
		}

		if (action_number == 8) {
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().SteeringAngle = -1f;
			GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().Acceleration = -1f;
			//CrossPlatformInputManager.SetAxis ("Horizontal", -1f);
			//CrossPlatformInputManager.SetAxis ("Vertical", -1f);
		}
	}

	public void DoActionA3C(float A , float S){
		GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().SteeringAngle = S;
		GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().Acceleration = A;
	}

	public float get_type(String word){

		/*if (word == "Pavement (1)" || word == "Pavement"){
			return 1f;
		}

		if (word == "construction" || word == "construction(2)" ){
			return 2f;
		}

		if (word == "Pedestrian"){
			return 3f;
		}

		if (word == "hole"){
			return 4f;
		}

		if (word == "Body"){
			return 5f;
		}

		if (word == "bump"){
			return 6f;
		}

		if (word == "RoadBlock") {
			return 7f;
		}

		return 0f;*/
	if(word== null){
		return 0;
	}
	if (word.StartsWith("bump"))
        return 1;
    if (word.StartsWith("Pavement"))
        return 2;
    if (word.StartsWith("Cube"))
        return 3;
    if (word.StartsWith("hole"))
        return 4;
    if (word.StartsWith("LampPost_A"))
        return 5;
    if (word.StartsWith("RoadBlock"))
        return 6;
    if (word.StartsWith("Body"))
        return 7;
    if (word.StartsWith("construction"))
        return 8;
    if (word.StartsWith("Pedestrian"))
        return 9;
	if(word.StartsWith("Victim"))
		return 7; 
    return 0;

		/*if(word == "Pavement (1)" || word == "Pavement" || word == "construction" || word == "construction(2)" || word == "Pedestrian" || word == "Body" || word == "RoadBlock"){
			return 1f;
		}
		else{
			return 0f;
		}*/
	}

}