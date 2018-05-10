using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car{
	public class PavementScript : MonoBehaviour {

		void OnTriggerEnter(Collider other){
			if(other.gameObject.tag == "Player")
				GameObject.Find ("Car(Clone)").GetComponent<RecordingScript> ().pavement = true;
		}

		void OnTriggerStay(Collider other){
			OnTriggerEnter(other);
		}

		void OnTriggerExit(Collider other){
			if(other.gameObject.tag == "Player")
				GameObject.Find ("Car(Clone)").GetComponent<RecordingScript> ().pavement = false;
		}
	}

}