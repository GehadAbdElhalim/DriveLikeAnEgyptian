using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour {
    public Vector3 StartPoint;
    public Vector3 EndPoint;
    public GameObject[] Obstacles; // 0 is the bump , 1 is the people , 2 is RoadBlock Left , 3 is RoadBlock Right
    public Vector3 bumpPosition;
    public Vector3 PeoplePosition;
    public Vector3 RoadBlockLeftPosition;
    public Vector3 RoadBlockRightPosition;
    public bool Rotated;

    // Use this for initialization
    /*void Start () {
        bumpPosition = new Vector3(0, 0, 0);
        PeoplePosition = new Vector3(0, 0, 0);
    }*/

    // Update is called once per frame
    void Start () {
        int randomNumber = Random.Range(0, 9);

        if (randomNumber == 0)
        {
            Invoke("SpawnBump",1);
        }

        if(randomNumber == 1)
        {
            Invoke("SpawnPeople", 1);
        }

        if(randomNumber == 2)
        {
            Invoke("SpawnRoadBlockLeft", 1);
        }

        if (randomNumber == 3)
        {
            Invoke("SpawnRoadBlockRight", 1);
        }

        if (randomNumber == 4)
        {
            Invoke("SpawnBump", 1);
            Invoke("SpawnPeople", 1);
        }

        if (randomNumber == 5)
        {
            Invoke("SpawnBump", 1);
            Invoke("SpawnRoadBlockLeft", 1);
        }

        if (randomNumber == 6)
        {
            Invoke("SpawnBump", 1);
            Invoke("SpawnRoadBlockRight", 1);
        }

        if (randomNumber == 7)
        {
            Invoke("SpawnPeople", 1);
            Invoke("SpawnRoadBlockLeft", 1);
        }

        if (randomNumber == 8)
        {
            Invoke("SpawnPeople", 1);
            Invoke("SpawnRoadBlockRight", 1);
        }
    }

    void SpawnBump()
    {
        float X ;
		float Z ;
        if (!Rotated)
        {
			X = StartPoint.x;
		 	Z = Random.Range(StartPoint.z , EndPoint.z);
            bumpPosition = new Vector3(X, -0.2f, Z);
            Instantiate(Obstacles[0], bumpPosition, Obstacles[0].transform.rotation);
        }
        else
        {
			X = Random.Range(StartPoint.x , EndPoint.x);
			Z = StartPoint.z;
            bumpPosition = new Vector3(X, -0.2f, Z);
            Instantiate(Obstacles[0], bumpPosition, Obstacles[0].transform.rotation*Quaternion.Euler(90,0,0));
        }
    }

    void SpawnPeople()
    {
		float X ;
        float Z ;
        if (!Rotated)
        {
			X = StartPoint.x + 5;
			Z = Random.Range(StartPoint.z, EndPoint.z);
            PeoplePosition = new Vector3(X, 0, Z);
            Instantiate(Obstacles[1], PeoplePosition, Quaternion.identity);
        }
        else
        {
			Z = StartPoint.z + 5;
			X = Random.Range(StartPoint.x, EndPoint.x);
            PeoplePosition = new Vector3(X, 0, Z);
            Instantiate(Obstacles[1], PeoplePosition, Quaternion.identity * Quaternion.Euler(0, -90, 0));
        }
    }

    void SpawnRoadBlockLeft()
    {
        float X ;
        float Z ;
        if (!Rotated)
        {
			X = StartPoint.x - 1;
			Z = Random.Range(StartPoint.z + 5, EndPoint.z - 5);
            RoadBlockLeftPosition = new Vector3(X, 3, Z);
            Instantiate(Obstacles[2], RoadBlockLeftPosition, Quaternion.identity);
        }
        else
        {
			Z = StartPoint.z + 1;
			X = Random.Range(StartPoint.x + 5, EndPoint.x - 5);
            RoadBlockLeftPosition = new Vector3(X, 3, Z);
            Instantiate(Obstacles[2], RoadBlockLeftPosition, Quaternion.identity * Quaternion.Euler(0, 90, 0));
        }
    }

    void SpawnRoadBlockRight()
    {
        float X ;
        float Z ;
        if (!Rotated)
        {
			X = StartPoint.x - 3;
			Z = Random.Range(StartPoint.z + 5 , EndPoint.z - 5);
            RoadBlockRightPosition = new Vector3(X, 4.05f, Z);
            Instantiate(Obstacles[3], RoadBlockRightPosition, Quaternion.identity);
        }
        else
        {
			Z = StartPoint.z + 3;
			X = Random.Range(StartPoint.x + 5, EndPoint.x - 5);
            RoadBlockRightPosition = new Vector3(X, 4.05f, Z);
            Instantiate(Obstacles[3], RoadBlockRightPosition, Quaternion.identity * Quaternion.Euler(0, 90, 0));
        }
    }
}
