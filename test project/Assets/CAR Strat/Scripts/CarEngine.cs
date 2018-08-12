﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CarEngine : MonoBehaviour {
	[Header("NavMesh")]
	public float separations;
	public List<Vector3> midPoints;
	public List<Vector3> leftPoints;
	public List<Vector3> rightPoints; 
	public bool rev ;
	public bool traffic;
	public int planAheadNodes;
	public NavMeshAgent NMA; 

	[Header("Car")]
	//public Transform path;
	public float maxSteerAngle = 45f;
	public float turnSpeed = 5f;
	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public WheelCollider wheelRL;
	public WheelCollider wheelRR;
	public float maxMotorTorque = 80f;
	public float maxBrakeTorque = 150f;
	public float currentSpeed;
	public float maxSpeed = 100f;
	public Vector3 centerOfMass;
	public bool isBraking = false;
	public Texture2D textureNormal;
	public Texture2D textureBraking;
	public Renderer carRenderer; 

	[Header("Sensors")]
	public float sensorLength = 3f;
	public Vector3 frontSensorPosition;
	public float frontSideSensorPosition = 0.2f;
	public float frontSensorAngle = 30f;

	private List<Transform> nodes;
	public int currectNode = 0;
	private bool avoiding = false;
	private float targetSteerAngle = 0;

	public bool NoNavMesh ;
	private void Start() {
		GetComponent<Rigidbody>().centerOfMass = centerOfMass;
		NMA = this.GetComponent<NavMeshAgent> ();
		if (NMA == null){
			NoNavMesh = true;
		}else{
			NoNavMesh = false;
		}
		/*Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++) {
            if (pathTransforms[i] != path.transform) {
                nodes.Add(pathTransforms[i]);
            }
        }*/
	}
	private void FixedUpdate() {
		
		if(NoNavMesh){
		ApplySteer();
		Drive();
		CheckWaypointDistanceWithNoPoint();
		Braking();
		LerpToSteerAngle();
		} else{
			CheckWaypointDistance();
		}
	}
	void speedUP (){
		wheelFL.motorTorque = maxMotorTorque*3;
		wheelFR.motorTorque = maxMotorTorque*3;
	}




	private void Drive() {
		currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

		if (currentSpeed < maxSpeed && !isBraking) {
			wheelFL.motorTorque = maxMotorTorque;
			wheelFR.motorTorque = maxMotorTorque;
		} else {
			wheelFL.motorTorque = 0;
			wheelFR.motorTorque = 0;
		}
	}

	private void CheckWaypointDistance() {
		/*   if (Vector3.Distance(transform.position, nodes[currectNode].position) < 1f) {
            if (currectNode == nodes.Count - 1) {
                currectNode = 0;
            } else {
                currectNode++;
            }
        }*/
		Vector3 BN =  bestNode (currectNode);
		if (!rev) {
			if (Vector3.Distance (transform.position,BN) < 1f) {
				if (currectNode == midPoints.Count - 1) {
					GameObject car =  Instantiate (gameObject, Vector3.Lerp (leftPoints [0], rightPoints [0], UnityEngine.Random.Range (0, 1)), Quaternion.Euler (0, 180, 0)) as GameObject;
					//currectNode = 0;
					//transform.position = Vector3.Lerp (leftPoints[0], rightPoints[0], UnityEngine.Random.Range(0,1)); BN =  bestNode (currectNode);
					car.GetComponent<CarEngine> ().midPoints = midPoints; 
					car.GetComponent<CarEngine> ().leftPoints = leftPoints;
					car.GetComponent<CarEngine> ().rightPoints = rightPoints;
					car.GetComponent<CarEngine> ().separations = separations;
					car.GetComponent<CarEngine> ().traffic = true;
					car.GetComponent<CarEngine> ().rev = false;
					car.GetComponent<CarEngine> ().currectNode = 0;
					Destroy (gameObject);
				} else {
					currectNode++;
				}
			}
		} else {if (Vector3.Distance (transform.position,BN) < 1f) {
				if (currectNode == 0) {
					/*Instantiate (gameObject, Vector3.Lerp (leftPoints [0], rightPoints [0], UnityEngine.Random.Range (0, 1)), Quaternion.Euler (0, 0, 0));
					currectNode = midPoints.Count - 1;
					transform.position = Vector3.Lerp (leftPoints[leftPoints.Count-1], rightPoints[leftPoints.Count-1], UnityEngine.Random.Range(0,1)); BN =  bestNode (currectNode);
					*/
					GameObject car =  Instantiate (gameObject, Vector3.Lerp (leftPoints[leftPoints.Count-1], rightPoints[leftPoints.Count-1], UnityEngine.Random.Range(0,1)), Quaternion.Euler (0, 0, 0)) as GameObject;
					//currectNode = 0;
					//transform.position = Vector3.Lerp (leftPoints[0], rightPoints[0], UnityEngine.Random.Range(0,1)); BN =  bestNode (currectNode);
					car.GetComponent<CarEngine> ().midPoints = midPoints; 
					car.GetComponent<CarEngine> ().leftPoints = leftPoints;
					car.GetComponent<CarEngine> ().rightPoints = rightPoints;
					car.GetComponent<CarEngine> ().separations = separations;
					car.GetComponent<CarEngine> ().traffic = true;
					car.GetComponent<CarEngine> ().rev = true;
					car.GetComponent<CarEngine> ().currectNode = midPoints.Count - 1;
					Destroy (gameObject);
				} else {
					currectNode--;
				}
			}
		}
		try{
			if(IsAgentOnNavMesh(gameObject))
			NMA.SetDestination (BN);
		}catch (Exception e){
			return;
		}
		Debug.DrawLine (transform.position, BN);
	}
	void StopCar (){
		if(traffic)
			Destroy (gameObject);
	}
	private void Braking() {
		if (isBraking) {
			carRenderer.material.mainTexture = textureBraking;
			wheelRL.brakeTorque = maxBrakeTorque;
			wheelRR.brakeTorque = maxBrakeTorque;
		} else {
			carRenderer.material.mainTexture = textureNormal;
			wheelRL.brakeTorque = 0;
			wheelRR.brakeTorque = 0;
		}
	}

	private Vector3 bestNode(int curNode){

		if (traffic) {
			if (!rev) {
				Vector3 RN = rightPoints [curNode];
				Vector3 MN = midPoints [curNode];
				Vector3 LN = leftPoints [curNode];
				Vector3 choosenNode = Vector3.Lerp (LN, RN, 1);
				float t = separations;
				float c = 1f;
				while (c > 0f) {
					Vector3 tryNode = Vector3.Lerp (LN, RN, c);
					if (ValidNode(tryNode))
					if (Vector3.Distance (transform.position, choosenNode) >Vector3.Distance (transform.position, tryNode)) {
						choosenNode = tryNode;

					}
					c -= t;
				}
				return choosenNode;
			} else {
				Vector3 RN = rightPoints [curNode];
				Vector3 MN = midPoints [curNode];
				Vector3 LN = leftPoints [curNode];
				Vector3 choosenNode = Vector3.Lerp (LN, RN, 1);
				float t = separations;
				float c = 1f;
				while (c > 0f) {
					Vector3 tryNode = Vector3.Lerp (LN, MN, c);
					if (ValidNode(tryNode))
					if (Vector3.Distance (transform.position, choosenNode) > Vector3.Distance (transform.position, tryNode)) {
						choosenNode = tryNode;
					}
					c -= t;
				}
				return choosenNode;
			}
		} else {
			Vector3 RN = rightPoints [curNode];
			Vector3 MN = midPoints [curNode];
			Vector3 LN = leftPoints [curNode];
			Vector3 choosenNode = Vector3.Lerp (LN, RN, 1);
			float t = separations;
			float c = 1f;
			while (c > 0f) {
				Vector3 tryNode = Vector3.Lerp (LN, RN, c);
				if (ValidNode(tryNode)) {
					if (Vector3.Distance (transform.position, choosenNode) > Vector3.Distance (transform.position, tryNode)) {
						choosenNode = tryNode;
					}
				}
				c -= t;
			}

			return choosenNode;
		}




	}

	private void CheckWaypointDistanceWithNoPoint(){
			Vector3 BN =  bestNode (currectNode);
		if (!rev) {
			if (Vector3.Distance (transform.position,BN) < 1f) {
				if (currectNode == midPoints.Count - 1) {
					GameObject car =  Instantiate (gameObject, Vector3.Lerp (leftPoints [0], rightPoints [0], UnityEngine.Random.Range (0, 1)), Quaternion.Euler (0, 180, 0)) as GameObject;
					//currectNode = 0;
					//transform.position = Vector3.Lerp (leftPoints[0], rightPoints[0], UnityEngine.Random.Range(0,1)); BN =  bestNode (currectNode);
					car.GetComponent<CarEngine> ().midPoints = midPoints; 
					car.GetComponent<CarEngine> ().leftPoints = leftPoints;
					car.GetComponent<CarEngine> ().rightPoints = rightPoints;
					car.GetComponent<CarEngine> ().separations = separations;
					car.GetComponent<CarEngine> ().traffic = true;
					car.GetComponent<CarEngine> ().rev = false;
					car.GetComponent<CarEngine> ().currectNode = 0;
					Destroy (gameObject);
				} else {
					currectNode++;
				}
			}
		} else {if (Vector3.Distance (transform.position,BN) < 1f) {
				if (currectNode == 0) {
					/*Instantiate (gameObject, Vector3.Lerp (leftPoints [0], rightPoints [0], UnityEngine.Random.Range (0, 1)), Quaternion.Euler (0, 0, 0));
					currectNode = midPoints.Count - 1;
					transform.position = Vector3.Lerp (leftPoints[leftPoints.Count-1], rightPoints[leftPoints.Count-1], UnityEngine.Random.Range(0,1)); BN =  bestNode (currectNode);
					*/
					GameObject car =  Instantiate (gameObject, Vector3.Lerp (leftPoints[leftPoints.Count-1], rightPoints[leftPoints.Count-1], UnityEngine.Random.Range(0,1)), Quaternion.Euler (0, 0, 0)) as GameObject;
					//currectNode = 0;
					//transform.position = Vector3.Lerp (leftPoints[0], rightPoints[0], UnityEngine.Random.Range(0,1)); BN =  bestNode (currectNode);
					car.GetComponent<CarEngine> ().midPoints = midPoints; 
					car.GetComponent<CarEngine> ().leftPoints = leftPoints;
					car.GetComponent<CarEngine> ().rightPoints = rightPoints;
					car.GetComponent<CarEngine> ().separations = separations;
					car.GetComponent<CarEngine> ().traffic = true;
					car.GetComponent<CarEngine> ().rev = true;
					car.GetComponent<CarEngine> ().currectNode = midPoints.Count - 1;
					Destroy (gameObject);
				} else {
					currectNode--;
				}
			}
		}
		
	}
	private void ApplySteer() {
		if (avoiding) return;
		Vector3 relativeVector = transform.InverseTransformPoint(bestNode(currectNode));
		float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
		targetSteerAngle = newSteer;
	}

	private bool ValidNode(Vector3 tryNode){
		RaycastHit hit;
		if (Physics.Raycast (tryNode +new  Vector3 (-0.5f, 0, 0), tryNode, out hit, 1)) {
			if(!hit.collider.CompareTag ("Bump") && !hit.collider.CompareTag ("RoadBlock"))
				return false;
		}
		if (Physics.Raycast (tryNode +new Vector3 (0.5f, 0, 0), tryNode, out hit, 1)) {
			if(!hit.collider.CompareTag ("Bump") && !hit.collider.CompareTag ("RoadBlock"))
				return false;
		}
		if (Physics.Raycast (tryNode -new Vector3 (0, 0, 0.5f), tryNode, out hit, 1)) {
			if(!hit.collider.CompareTag ("Bump") && !hit.collider.CompareTag ("RoadBlock"))
				return false;
		}
		if (Physics.Raycast (tryNode -new Vector3 (0, 0, -0.5f), tryNode, out hit, 1)) {
			if(!hit.collider.CompareTag ("Bump") && !hit.collider.CompareTag ("RoadBlock"))
				return false;
		}	
		return true;
	}

	private void LerpToSteerAngle() {
		wheelFL.steerAngle = Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
		wheelFR.steerAngle = Mathf.Lerp(wheelFR.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
	}
	float onMeshThreshold = 3;

	public bool IsAgentOnNavMesh(GameObject agentObject)
	{
		Vector3 agentPosition = agentObject.transform.position;
		NavMeshHit hit;

		// Check for nearest point on navmesh to agent, within onMeshThreshold
		if (NavMesh.SamplePosition(agentPosition, out hit, onMeshThreshold, NavMesh.AllAreas))
		{
			// Check if the positions are vertically aligned
			if (Mathf.Approximately(agentPosition.x, hit.position.x)
				&& Mathf.Approximately(agentPosition.z, hit.position.z))
			{
				// Lastly, check if object is below navmesh
				return agentPosition.y >= hit.position.y;
			}
		}

		return false;
	}
}