using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour {
    public Vector3 StartPoint1;
    public Vector3 StartPoint2;
    public Vector3 EndPoint1;
    public Vector3 EndPoint2;
    public GameObject[] Obstacles; // 0 is the bump , 1 is the people , 2 is RoadBlock Left , 3 is RoadBlock Right
    public Vector3 bumpPosition;
    public Vector3 PeoplePosition;
    public Vector3 RoadBlockLeftPosition;
    public Vector3 RoadBlockRightPosition;

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
            bumpPosition = new Vector3((StartPoint1.x+StartPoint2.x)/2,0,Random.Range(StartPoint1.z,EndPoint1.z));
            Invoke("SpawnBump",1);
        }

        if(randomNumber == 1)
        {
            PeoplePosition = new Vector3(StartPoint1.x, 0, Random.Range(StartPoint1.z,EndPoint1.z));
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
            bumpPosition = new Vector3((StartPoint1.x + StartPoint2.x) / 2, 0, Random.Range(StartPoint1.z, EndPoint1.z-5));
            PeoplePosition = new Vector3(StartPoint1.x, 0, Random.Range(StartPoint1.z, EndPoint1.z));
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
        float X = (StartPoint1.x + StartPoint2.x) / 2;
        float Z = Random.Range((StartPoint1.z + EndPoint1.z) / 2, EndPoint1.z - 2);
        bumpPosition = new Vector3(X, -0.2f, Z);
        Instantiate(Obstacles[0], bumpPosition, Obstacles[0].transform.rotation);
    }

    void SpawnPeople()
    {
        float X = StartPoint1.x;
        float Z = Random.Range(StartPoint1.z, EndPoint1.z);
        PeoplePosition = new Vector3(X, 0, Z);
        Instantiate(Obstacles[1], PeoplePosition, Quaternion.identity);
    }

    void SpawnRoadBlockLeft()
    {
        float X = StartPoint2.x + 10f;
        float Z = Random.Range(StartPoint1.z + 5, EndPoint1.z - 5);
        RoadBlockLeftPosition = new Vector3(X, 3, Z);
        Instantiate(Obstacles[2], RoadBlockLeftPosition, Quaternion.identity);
    }

    void SpawnRoadBlockRight()
    {
        float X = StartPoint1.x - 13f;
        float Z = Random.Range(StartPoint1.z + 5, EndPoint1.z - 5);
        RoadBlockRightPosition = new Vector3(X, 4.05f, Z);
        Instantiate(Obstacles[3], RoadBlockRightPosition, Quaternion.identity);
    }
}
