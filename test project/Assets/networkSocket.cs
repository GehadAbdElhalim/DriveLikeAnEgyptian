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

    internal Boolean socket_ready = false;
    internal String input_buffer = "";
    TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;



    void Update()
    {
		if ((readSocket() == "Send the starting State\n") || (readSocket() == "send next state\n")) 
		{
			if (State_is_done ()) {
				writeSocket ("done");
			} else {
				writeSocket ("ok");
				int i = 0;
				float[] state = getCurrentState();
				while (readSocket() == "send next element \n") {
					writeSocket (Convert.ToString (state [i]));
					i++;
				}
			}
		}

		if (readSocket() == "take this action\n") 
		{
			writeSocket ("ok");
			DoAction(Int32.Parse(readSocket()));
			writeSocket ("action done");
		}
    }


    void Awake()
    {
        setupSocket();
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
            return "";

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

	public float[] getCurrentState(){
		float[] state_output = new float[43];
		for (int i = 0; i < 35; i++) {
			state_output [i] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.GetComponent<RecordingScript> ().lines [i].distance;
		}

		state_output [36] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.transform.GetComponent<Rigidbody> ().velocity.x;
		state_output [37] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.transform.GetComponent<Rigidbody> ().velocity.y;
		state_output [38] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.transform.GetComponent<Rigidbody> ().velocity.z;
		state_output [39] = GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1> ().Car.transform.eulerAngles.y - GameObject.Find("StreetManger (1)").GetComponent<CityDesgin1>().Car.GetComponent<RecordingScript>().current_roadblock.transform.eulerAngles.y;
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
	
		return state_output;
	}

	public bool State_is_done() //COMPLETE THE CODE
	{
		return false;
	}

	public void DoAction(int action_number) //COMPLETE THE CODE
	{
		
	}

}