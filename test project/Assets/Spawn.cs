using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject agent;
    public GameObject goalObject;
	// Use this for initialization
	void Start () {
        Invoke("SpawnAgent", 1);
	}
	
    void SpawnAgent()
    {
        GameObject na = (GameObject)Instantiate(agent, this.transform.position, Quaternion.identity);
        na.GetComponent<walkTo>().goal = goalObject.transform;
        Invoke("SpawnAgent", Random.Range(1, 5));
    }

	// Update is called once per frame
	void Update () {
		
	}
}
