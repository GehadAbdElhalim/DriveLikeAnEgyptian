using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarRemoteControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        public float SteeringAngle { get; set; }
        public float Acceleration { get; set; }
        private Steering s;

		Text None;
		Text F;
		Text B;
		Text R;
		Text L;
		Text FR;
		Text BR;
		Text FL;
		Text BL;

        private void Awake()
		{
//			InvokeRepeating("ActionTaken", 0.5f, 0.5f);

//			None = GameObject.Find ("Canvas/None").GetComponent<Text> ();
//			F = GameObject.Find ("Canvas/Forward").GetComponent<Text> ();
//			B = GameObject.Find ("Canvas/Backward").GetComponent<Text> ();
//			R = GameObject.Find ("Canvas/Right").GetComponent<Text> ();
//			L = GameObject.Find ("Canvas/Left").GetComponent<Text> ();
//			FR = GameObject.Find ("Canvas/Forward_Right").GetComponent<Text> ();
//			BR = GameObject.Find ("Canvas/Backward_Right").GetComponent<Text> ();
//			FL = GameObject.Find ("Canvas/Forward_Left").GetComponent<Text> ();
//			BL = GameObject.Find ("Canvas/Backward_Left").GetComponent<Text> ();


            // get the car controller
            m_Car = GetComponent<CarController>();
            s = new Steering();
            s.Start();
        }

		private void ActionTaken(){
			FR.fontSize = 16; FR.fontStyle = FontStyle.Normal; FR.color = Color.white;
			FL.fontSize = 16; FL.fontStyle = FontStyle.Normal; FL.color = Color.white;
			F.fontSize = 16; F.fontStyle = FontStyle.Normal; F.color = Color.white;
			BR.fontSize = 16; BR.fontStyle = FontStyle.Normal; BR.color = Color.white;
			BL.fontSize = 16; BL.fontStyle = FontStyle.Normal; BL.color = Color.white;
			B.fontSize = 16; B.fontStyle = FontStyle.Normal; B.color = Color.white;
			R.fontSize = 16; R.fontStyle = FontStyle.Normal; R.color = Color.white;
			L.fontSize = 16; L.fontStyle = FontStyle.Normal; L.color = Color.white;
			None.fontSize = 16; None.fontStyle = FontStyle.Normal; None.color = Color.white;

			if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D)) {
				FR.fontSize = 18; FR.fontStyle = FontStyle.Bold; FR.color = Color.red;
			} else if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A)) {
				FL.fontSize = 18; FL.fontStyle = FontStyle.Bold; FL.color = Color.red;
			} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)) {
				BR.fontSize = 18; BR.fontStyle = FontStyle.Bold; BR.color = Color.red;
			} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) {
				BL.fontSize = 18; BL.fontStyle = FontStyle.Bold; BL.color = Color.red;
			} else if (Input.GetKey (KeyCode.W)) {
				F.fontSize = 18; F.fontStyle = FontStyle.Bold; F.color = Color.red;
			} else if (Input.GetKey (KeyCode.S)) {
				B.fontSize = 18; B.fontStyle = FontStyle.Bold; B.color = Color.red;
			} else if (Input.GetKey (KeyCode.D)) {
				R.fontSize = 18; R.fontStyle = FontStyle.Bold; R.color = Color.red;
			} else if (Input.GetKey (KeyCode.A)) {
				L.fontSize = 18; L.fontStyle = FontStyle.Bold; L.color = Color.red;
			} else {
				None.fontSize = 18; None.fontStyle = FontStyle.Bold; None.color = Color.red;
			}
		}

        private void FixedUpdate()
        {
            // If holding down W or S control the car manually
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
            {
                s.UpdateValues();
                m_Car.Move(s.H, s.V, s.V, 0f);
            } else
            {
				m_Car.Move(SteeringAngle, Acceleration, Acceleration, 0f);
            }
        }
    }
}
