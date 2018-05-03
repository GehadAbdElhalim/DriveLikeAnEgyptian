using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

 

public class networkSocket : MonoBehaviour
{
    public String host = "localhost";
    public Int32 port = 50000;
	int a;

    internal Boolean socket_ready = false;
    internal String input_buffer = "";
    TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;



    void UpdateMe()
    {
		String message = readSocket ();
		//Debug.Log (readSocket());
		if (message == "Send the starting State") {
			Debug.Log ("arrived");
			if (State_is_done ()) {
				writeSocket ("done");
			} else {
				Debug.Log ("else part");
				writeSocket (getCurrentState ());
				/*writeSocket ("ok");
				Debug.Log(readSocket());
				//int i = 0;
				float[] state = getCurrentState();
                while(readSocket() != "send next element");
                Debug.Log(readSocket());
                String response = readSocket();
                Debug.Log(response);
                for (int i = 0 ; i < state.Length ; i++){
                    if (response == "send next element")
                    {
                        Debug.Log("sending........");
                        writeSocket(Convert.ToString(state[i]));
                        response = readSocket();
                    }
                    //Debug.Log("not sending........");
                }*/
				///////////////////////////////////////////////////////////////////
				/*while (readSocket() == "send next element") {
					Debug.Log ("sending........");
					writeSocket (Convert.ToString (state [i]));
					i++;
				}*/
			}
		} else {
			Debug.Log (message);
			a = Int32.Parse(message.Substring (10,1));
			Debug.Log (a);
			DoAction (a);
			writeSocket ("action done");
		}

	/*if (message == "take this action") 
		{
			Debug.Log ("actioooooooooooooon");
			writeSocket ("ok");
			String response = readSocket ();
			Debug.Log(Int32.Parse(response));
			DoAction(Int32.Parse(response));
			writeSocket ("action done");
		}*/
    }

	void Update(){
		DoAction (a);
	}


    void Awake()
    {
        setupSocket();
		a = 0;
        InvokeRepeating("UpdateMe", 3f, 1f);
    }

    void OnApplicationQuit()
    {
        closeSocket();
    }

    public void setupSocket()
    {
        try
        {
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

	public String getCurrentState(){
		float[] state_output = new float[43];
		/*for (int i = 0; i <= 35; i++) {
			Debug.Log (i);
			state_output [i] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.GetComponent<RecordingScript> ().lines [i].distance;
		}*/
		int i = 0;
		state_output[i++] = SensorsGlobalManager.Instance.distance0;
		state_output[i++] = SensorsGlobalManager.Instance.distance10;
		state_output[i++] = SensorsGlobalManager.Instance.distance20;
		state_output[i++] = SensorsGlobalManager.Instance.distance30;
		state_output[i++] = SensorsGlobalManager.Instance.distance40;
		state_output[i++] = SensorsGlobalManager.Instance.distance50;
		state_output[i++] = SensorsGlobalManager.Instance.distance60;
		state_output[i++] = SensorsGlobalManager.Instance.distance70;
		state_output[i++] = SensorsGlobalManager.Instance.distance80; 
		state_output[i++] = SensorsGlobalManager.Instance.distance90;
		state_output[i++] = SensorsGlobalManager.Instance.distance100;
		state_output[i++] = SensorsGlobalManager.Instance.distance110;
		state_output[i++] = SensorsGlobalManager.Instance.distance120;
		state_output[i++] = SensorsGlobalManager.Instance.distance130;
		state_output[i++] = SensorsGlobalManager.Instance.distance140;
		state_output[i++] = SensorsGlobalManager.Instance.distance150;
		state_output[i++] = SensorsGlobalManager.Instance.distance160;
		state_output[i++] = SensorsGlobalManager.Instance.distance170;
		state_output[i++] = SensorsGlobalManager.Instance.distance180;
		state_output[i++] = SensorsGlobalManager.Instance.distance190;
		state_output[i++] = SensorsGlobalManager.Instance.distance200;
		state_output[i++] = SensorsGlobalManager.Instance.distance210;
		state_output[i++] = SensorsGlobalManager.Instance.distance220;
		state_output[i++] = SensorsGlobalManager.Instance.distance230;
		state_output[i++] = SensorsGlobalManager.Instance.distance240;
		state_output[i++] = SensorsGlobalManager.Instance.distance250;
		state_output[i++] = SensorsGlobalManager.Instance.distance260;
		state_output[i++] = SensorsGlobalManager.Instance.distance270;
		state_output[i++] = SensorsGlobalManager.Instance.distance280;
		state_output[i++] = SensorsGlobalManager.Instance.distance290;
		state_output[i++] = SensorsGlobalManager.Instance.distance300;
		state_output[i++] = SensorsGlobalManager.Instance.distance310;
		state_output[i++] = SensorsGlobalManager.Instance.distance320;
		state_output[i++] = SensorsGlobalManager.Instance.distance330;
		state_output[i++] = SensorsGlobalManager.Instance.distance340;
		state_output[i++] = SensorsGlobalManager.Instance.distance350;



		state_output [36] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.transform.GetComponent<Rigidbody> ().velocity.x;
		state_output [37] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.transform.GetComponent<Rigidbody> ().velocity.y;
		state_output [38] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.transform.GetComponent<Rigidbody> ().velocity.z;
		//state_output [39] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.transform.eulerAngles.y - GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().Car.GetComponent<RecordingScript>().getRoadBlock().transform.eulerAngles.y;
		state_output [39] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.transform.eulerAngles.y - GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().Car.GetComponent<RecordingScript>().CarAngle;

		if (GameObject.Find ("StreetManger (1)").GetComponent<CityDesgin1> ().Car.GetComponent<RecordingScript> ().trafficLights ()) {
			state_output [40] = 1f;
		} else {
			state_output [40] = 0f;
		}

		if (GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().isRainy) {
			state_output [41] = 1f;
		} else {
			state_output [41] = 0f;
		}

		state_output [42] = (float) GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.GetComponent<RecordingScript> ().collidedObjects.Count;

		String output = "";
		for (int j = 0 ; j < 43 ; j++){
			output = output + Convert.ToString (state_output [j]) + ",";
		}

		return output;
	}

	public bool State_is_done() //COMPLETE THE CODE
	{
		/*float[] state = getCurrentState ();
		for (int i = 0; i <= 35; i++) {
			if (state[i] != 0f){
				return false;
			}
		}
		return true;*/
		return false;
	}

	public void DoAction(int action_number) //COMPLETE THE CODE
	{
		Debug.Log ("hi action");
		//GameObject.Find("Car(Clone)").GetComponent<CarRemoteControl> ().Acceleration = 1f;
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

}