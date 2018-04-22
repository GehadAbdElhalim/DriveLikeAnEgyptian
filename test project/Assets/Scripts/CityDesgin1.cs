using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
 * a rondom city genertator
 * 
 * 
 * 
 */
public class CityDesgin1 : MonoBehaviour { 
	/// <summary>
	/// 	the assets game objects that will be used to generate the map
	/// 	 
	/// </summary>
    public GameObject streetLane60mVertical;
	public GameObject streetCrossXRoads;
	public GameObject streetTurn90DownLeft;
	public GameObject streetBump;
	public GameObject streetHole;
	public GameObject intersection;

	/// <summary>
	/// 	if auto generate is checked 
	/// 		a rondam map with the number of road blocks is equal to NumberOfBlocks
	/// 		if the rondam map created succesfully
	/// 			it will be added in JSONFiLeWitten path 
	/// 	else 
	/// 		a map will be read from JSONFile path 
	/// 
	/// 	note that JSONfiles are normal .txt files 
	/// </summary>
	public string JSONFileWirttern;
	public bool AutoGenerte;
	public string JSONFile ;
    public int NumberOfBlocks ;
    public Vector3[] Waypoints;
    public GameObject CarAI;
    public GameObject Car;
    public GameObject ObstacleGenerator;


    const int up = 0;
    const int right = 1;
    const int left = 2;
    const int down = 3;
    int NumOfCol = 0;
    static Road[] roadKinds = new Road[28];
    Road[] arr;
    struct Myx
    {
        public float xmin;
        public float xmax;
    }
	private int curRoadsNum = 0;
    struct Road 
    {
        public GameObject roadType;
        public int start;
        public int end;
        public int offestX;
        public int offestZ;
        public Vector3 postion;
        public Quaternion Rotation;
        public string name;
        public Vector3 curPos;
        public Vector3 startPos;
        public Vector3 endPos;

        public void set(GameObject RoadType , int start, int end , int offestX , int offestZ, Vector3 Vec, Quaternion Quat, string name, Vector3 curPos)
		{
			this.roadType = RoadType;
			this.start = start;
			this.end = end;
			this.offestX = offestX;
			this.offestZ = offestZ;
			this.postion = Vec;
			this.Rotation = Quat;
			this.name = name;
			this.curPos = curPos;
		}
    }
	/// <summary>
	/// 	in the start we put our roads in structs
	/// 		then we either rondom a generated map 
	/// 		or we generate the map in the files 
	/// </summary>
    void Start()
    {
        //Gehad things
        Waypoints = new Vector3[NumberOfBlocks];
        //Gehad things

        Road upDown = new Road();
        upDown.set(streetLane60mVertical, up, down, 0, -60, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0), "upDown", new Vector3(0, 0, 0));
        roadKinds[0] = upDown;

        Road downUp = new Road();
        downUp.set(streetLane60mVertical, down, up, 0, 60, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0), "downUp", new Vector3(0, 0, 0));
        roadKinds[1] = downUp;

        Road leftRight = new Road();
        leftRight.set(streetLane60mVertical, left, right, 60, 0, new Vector3(0, 0, 0), Quaternion.Euler(-90, -90, 0), "leftRight", new Vector3(0, 0, 0));
        roadKinds[2] = leftRight;

        Road rightLeft = new Road();
        rightLeft.set(streetLane60mVertical, right, left, -60, 0, new Vector3(0, 0, 0), Quaternion.Euler(-90, -90, 0), "rightLeft", new Vector3(0, 0, 0));
        roadKinds[3] = rightLeft;


        Road crossRoadUpDown = new Road();
        crossRoadUpDown.set(streetCrossXRoads, up, down, 0, -60, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "crossRoadUpDown", new Vector3(0, 0, 0));
        roadKinds[4] = crossRoadUpDown;

        Road crossRoadDownUp = new Road();
        crossRoadDownUp.set(streetCrossXRoads, down, up, 0, 60, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "crossRoadDownUp", new Vector3(0, 0, 0));
        roadKinds[5] = crossRoadDownUp;

        Road crossRoadLeftRight = new Road();
        crossRoadLeftRight.set(streetCrossXRoads, left, right, 60, 0, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "crossRoadLeftRight", new Vector3(0, 0, 0));
        roadKinds[6] = crossRoadLeftRight;

        Road crossRoadRightLeft = new Road();
        crossRoadRightLeft.set(streetCrossXRoads, right, left, -60, 0, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "crossRoadRightLeft", new Vector3(0, 0, 0));
        roadKinds[7] = crossRoadRightLeft;

        Road LeftDown = new Road();
        LeftDown.set(streetTurn90DownLeft, left, down, 15, -80, new Vector3(0, 0, -15), Quaternion.Euler(-90, 315, 0), "LeftDown", new Vector3(0, 0, 0));
        roadKinds[8] = LeftDown;
        Road DownLeft = new Road();
        DownLeft.set(streetTurn90DownLeft, down, left, -80, 15, new Vector3(-15, 0, 0), Quaternion.Euler(-90, 315, 0), "DownLeft", new Vector3(0, 0, 0));
        roadKinds[9] = DownLeft;

        Road LeftTop = new Road();
        LeftTop.set(streetTurn90DownLeft, left, up, 15, 80, new Vector3(0, 0, 15), Quaternion.Euler(-90, 45, 0), "LeftTop", new Vector3(0, 0, 0));
        roadKinds[10] = LeftTop;
        Road TopLeft = new Road();
        TopLeft.set(streetTurn90DownLeft, up, left, -80, -15, new Vector3(-15, 0, 0), Quaternion.Euler(-90, 45, 0), "TopLeft", new Vector3(0, 0, 0));
        roadKinds[11] = TopLeft;

        Road TopRight = new Road();
        TopRight.set(streetTurn90DownLeft, up, right, 80, -20, new Vector3(15, 0, -5), Quaternion.Euler(-90, 135, 0), "TopRight", new Vector3(0, 0, 0));
        roadKinds[12] = TopRight;
        Road RightTop = new Road();
        RightTop.set(streetTurn90DownLeft, right, up, -15, 80, new Vector3(0, 0, 15), Quaternion.Euler(-90, 135, 0), "RightTop", new Vector3(0, 0, 0));
        roadKinds[13] = RightTop;

        Road RightDown = new Road();
        RightDown.set(streetTurn90DownLeft, right, down, -15, -80, new Vector3(0, 0, -15), Quaternion.Euler(-90, 225, 0), "RightDown", new Vector3(0, 0, 0));
        roadKinds[14] = RightDown;
        Road DownRight = new Road();
        DownRight.set(streetTurn90DownLeft, down, right, 80, 20, new Vector3(15, 0, 5), Quaternion.Euler(-90, 225, 0), "DownRight", new Vector3(0, 0, 0));
		roadKinds[15] = DownRight;

		Road upDownBump = new Road();
		upDown.set(streetBump, up, down, 0, -60, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0), "upDown", new Vector3(0, 0, 0));
		roadKinds[16] = upDown;

		Road downUpBump = new Road();
		downUp.set(streetBump, down, up, 0, 60, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0), "downUp", new Vector3(0, 0, 0));
		roadKinds[17] = downUp;

		Road leftRightBump = new Road();
		leftRight.set(streetBump, left, right, 60, 0, new Vector3(0, 0, 0), Quaternion.Euler(-90, -90, 0), "leftRight", new Vector3(0, 0, 0));
		roadKinds[18] = leftRight;

		Road rightLeftBump = new Road();
		rightLeft.set(streetBump, right, left, -60, 0, new Vector3(0, 0, 0), Quaternion.Euler(-90, -90, 0), "rightLeft", new Vector3(0, 0, 0));
		roadKinds[19] = rightLeft;

		Road upDownHole = new Road();
		upDown.set(streetHole, up, down, 0, -60, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0), "upDown", new Vector3(0, 0, 0));
		roadKinds[20] = upDown;

		Road downUpHole = new Road();
		downUp.set(streetHole, down, up, 0, 60, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0), "downUp", new Vector3(0, 0, 0));
		roadKinds[21] = downUp;

		Road leftRightHole = new Road();
		leftRight.set(streetHole, left, right, 60, 0, new Vector3(0, 0, 0), Quaternion.Euler(-90, -90, 0), "leftRight", new Vector3(0, 0, 0));
		roadKinds[22] = leftRight;

		Road rightLeftHole = new Road();
		rightLeft.set(streetHole, right, left, -60, 0, new Vector3(0, 0, 0), Quaternion.Euler(-90, -90, 0), "rightLeft", new Vector3(0, 0, 0));
		roadKinds[23] = rightLeft;

		Road intersectionRoadUpDown = new Road();
		intersectionRoadUpDown.set(intersection, up, down, 0, -60, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "intersectionRoadUpDown", new Vector3(0, 0, 0));
		roadKinds[24] = intersectionRoadUpDown;

		Road intersectionRoadDownUp = new Road();
		intersectionRoadDownUp.set(intersection, down, up, 0, 60, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "intersectionRoadDownUp", new Vector3(0, 0, 0));
		roadKinds[25] = intersectionRoadDownUp;

		Road intersectionRoadLeftRight = new Road();
		intersectionRoadLeftRight.set(intersection, left, right, 60, 0, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "intersectionRoadLeftRight", new Vector3(0, 0, 0));
		roadKinds[26] = intersectionRoadLeftRight;

		Road intersectionRoadRightLeft = new Road();
		intersectionRoadRightLeft.set(intersection, right, left, -60, 0, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "intersectionRoadRightLeft", new Vector3(0, 0, 0));
		roadKinds[27] = intersectionRoadRightLeft;

		if (AutoGenerte)
			creatMap ();
		else
			ReadJSON ();
        
    }

	/// <summary>
	/// 	array of roads is equal to the number of blocks
	/// </summary>
    void creatMap()
    {
         arr= new Road[NumberOfBlocks];
       
       	// the frist road is fixed upDown Road 
        arr[0] = roadKinds[0];
        arr[0].startPos = new Vector3(0, 0, 0);
		arr[0].endPos = arr [0].startPos + new Vector3(arr[0].offestX,0,arr[0].offestZ);;
        
        int tryMe = 3;
        for (int i = 1; i < NumberOfBlocks; i++)
        {   
			// stop after 1000 collisions (usually there is a problem with and the map can't spawn)
			if(NumOfCol > 1000)
            {
                break;
            }
			//choose a road that can match the current road end 
            arr[i] = clone(chooseRoad(arr[i - 1].end));
			// the start of the newe road is the end of the one before
            arr[i].startPos = arr[i-1].endPos;
			// and the end is our start + offest 
            arr[i].endPos = arr[i].startPos + new Vector3(arr[i].offestX,0,arr[i].offestZ);
			// make sure no collision and back track if you hit 3 collisions
            if (!makeSure(arr[i], i)&&i>1)
            {
                i--;
                tryMe--;
                if (tryMe < 0) { i--; tryMe = 3;  }
                continue;
            }  
        }
		// if all of the road is made the code will make the map and save it
        for (int i = 0; i < NumberOfBlocks; i++)
        {
            Instantiate(arr[i].roadType, arr[i].postion + arr[i].startPos, arr[i].Rotation);
            //Obstacle Spawning
			if (arr[i].roadType == streetLane60mVertical) 
			{
				SpawnObsatcle(arr[i]);
			}
            //Obstacle Spawning
            Waypoints[i] = arr[i].postion + arr[i].startPos;
        }
        SpawnAICar();
        //SpawnCar();
        writeString(ToJSONFromArr(arr),JSONFileWirttern);
    }
	/// <summary>
	/// 	reverse the end of the road to know the start
	/// </summary>
	/// <returns>The road.</returns>
	/// <param name="end">End.</param>
    Road chooseRoad(int end)
    {
        if(end == up)
        {
         return lookFor(down);
        } 
        else if(end == down)
        {
         return lookFor(up);
        }
        else if(end == left)
        {
         return lookFor(right);
        }
         return lookFor(left);
    }
	/// <summary>
	/// 	look for a road that has this start
	/// </summary>
	/// <returns>The for.</returns>
	/// <param name="start">Start.</param>
    Road lookFor(int start)
    {
        List<Road> choosen = new List<Road>();
        for (int i = 0; i < roadKinds.Length; i++)
        {
            if(roadKinds[i].start==start)
                choosen.Add(roadKinds[i]);
        }
        return choosen[Random.Range(0, choosen.Count)];
    }
	/// <summary>
	/// 	make sure no collision happens with the other roads
	/// </summary>
	/// <returns><c>true</c>, if sure was made, <c>false</c> otherwise.</returns>
	/// <param name="road">Road.</param>
	/// <param name="start2">Start2.</param>
	/// <param name="index">Index.</param>
    bool makeSure(Road road ,int index)
    {
        for(int i = 0; i < index; i++)
        {
            if (intersect(arr[i] , road))
            {
                NumOfCol++;
                return false;
            }
        }
        return true;
    }
	/// <summary>
	/// 	returns the JSON string that will be saved in the file txt
	/// </summary>
	/// <returns>The JSON from arr.</returns>
	/// <param name="obj">Object.</param>
    private static string ToJSONFromArr(Road [] obj)
    { string returing="";
        for (int i = 0; i < obj.Length-1; i++)
        {
            returing += JsonUtility.ToJson(obj[i]) + "*";
        }
        returing += JsonUtility.ToJson(obj[obj.Length - 1]);
        return returing;
    }
	/// <summary>
	/// 	take a string and make it Object Road
	/// </summary>
	/// <returns>The road.</returns>
	/// <param name="JSONobj">JSO nobj.</param>
    private static Road ToRoad( string JSONobj)
    {
        return JsonUtility.FromJson<Road>(JSONobj);
    }
	/// <summary>
	/// 	just copying the info of the road
	/// </summary>
	/// <param name="p">P.</param>
    Road clone(Road p)
    {
        Road temp = new Road();
        temp.roadType = p.roadType;
        temp.start = p.start;
        temp.end = p.end;
        temp.offestX = p.offestX;
        temp.offestZ = p.offestZ;
        temp.postion = p.postion;
        temp.Rotation = p.Rotation;
        temp.name = p.name;
        temp.curPos = p.curPos;
        return temp;
    }
	/// <summary>
	/// 	check if two roads intersect with each other using overlapping
	/// </summary>
	/// <param name="road1">Road1.</param>
	/// <param name="road2">Road2.</param>
    bool intersect(Road road1, Road road2)
	{
        GameObject road1Obj = Instantiate(road1.roadType, road1.postion + road1.startPos,  road1.Rotation) as GameObject;
        GameObject road2Obj = Instantiate(road2.roadType, road2.postion + road2.startPos, road2.Rotation) as GameObject;

        road1Obj.transform.position = road1.postion + road1.startPos;
        road2Obj.transform.position = road2.postion + road2.startPos;
        road1Obj.transform.eulerAngles = road1.Rotation.eulerAngles;
        road2Obj.transform.eulerAngles = road2.Rotation.eulerAngles;


        Vector3 BoxObj1Min = road1Obj.GetComponent<BoxCollider>().bounds.min;
        Vector3 BoxObj1Max = road1Obj.GetComponent<BoxCollider>().bounds.max;

        Vector3 BoxObj2Min = road2Obj.GetComponent<BoxCollider>().bounds.min;
        Vector3 BoxObj2Max = road2Obj.GetComponent<BoxCollider>().bounds.max;

        Myx Box1x = new Myx();
        Myx Box2x = new Myx();

        Myx Box1y = new Myx();
        Myx Box2y = new Myx();

        Myx Box1z = new Myx();
        Myx Box2z = new Myx();

        Box1x.xmin = BoxObj1Min.x; Box1x.xmax = BoxObj1Max.x;
        Box1y.xmin = BoxObj1Min.y; Box1y.xmax = BoxObj1Max.y;
        Box1z.xmin = BoxObj1Min.z; Box1z.xmax = BoxObj1Max.z;

        Box2x.xmin = BoxObj2Min.x; Box2x.xmax = BoxObj2Max.x;
        Box2y.xmin = BoxObj2Min.y; Box2y.xmax = BoxObj2Max.y;
        Box2z.xmin = BoxObj2Min.z; Box2z.xmax = BoxObj2Max.z;

        Destroy(road1Obj);
        Destroy(road2Obj);
        return overlapping3D(Box1x, Box2x, Box1y, Box2y, Box1z, Box2z);
    }
    bool overlapping1D(Myx x1,Myx x2)
    {
        return x1.xmax >= x2.xmin && x2.xmax >= x1.xmin;
    }

    bool overlapping2D(Myx x1,Myx x2 ,Myx y1,Myx y2)
    {
        return overlapping1D(x1, x2)&& overlapping1D(y1, y2);
    }

    bool overlapping3D(Myx x1, Myx x2, Myx y1, Myx y2,Myx z1,Myx z2)
    {
        return overlapping1D(x1, x2) && overlapping1D(y1, y2)&& overlapping1D(z1,z2);
    }

	/// <summary>
	/// 	read from the file the Object and display it
	/// </summary>
	void ReadJSON(){	
		string JSONObjects = readTextFile (JSONFile);
		print (JSONObjects);
		string [] RoadsJson  = JSONObjects.Split(new char[]{'*'} );
		print (RoadsJson[0]);
		Road[] roads = ConvertToRoads (RoadsJson);
		for (int i = 0; i < roads.Length; i++) {
			
			Instantiate (stringToRoad(roads[i].name),roads[i].postion+roads[i].startPos,roads[i].Rotation);
		}
	}
	/// <summary>
	/// 	convert array of JSONObjects into Roads
	/// </summary>
	/// <returns>The to roads.</returns>
	/// <param name="RoadsJson">Roads json.</param>
	Road[]  ConvertToRoads(string [] RoadsJson){
		Road[] roads = new Road[RoadsJson.Length];
		for (int i = 0; i < RoadsJson.Length; i++) {
			roads [i] = ToRoad (RoadsJson [i]);
		}
		return roads;
	}
	string readTextFile(string file_path){
		StreamReader inp_stm = new StreamReader (file_path);
		return inp_stm.ReadToEnd ();
	}
	void writeString(string objects,string path){

		StreamWriter writer = new StreamWriter (path, true);
		writer.WriteLine (objects);
		writer.Close();

		
	}

	/// <summary>
	/// 	give me the GameObject that has the Name of roadName
	/// </summary>
	/// <returns>The to road.</returns>
	/// <param name="roadName">Road name.</param>
	static GameObject stringToRoad(string roadName){
		for (int i = 0; i < roadKinds.Length; i++) {
			if (roadKinds [i].name.Equals (roadName)) {
				return roadKinds [i].roadType;
			}
		}
		return null;
	}

    void SpawnAICar()
    {
        GameObject car = (GameObject)Instantiate(CarAI, new Vector3(0, 0, 0), Quaternion.identity);
        int i = 1;
        for (i = 1; i < Waypoints.Length; i++)
        {
            if (Waypoints[i] == new Vector3(0, 0, 0))
                break;
        }
        Vector3[] TrueWaypoints = new Vector3[i];
        for (int j = 0; j < TrueWaypoints.Length; j++)
        {
            TrueWaypoints[j] = Waypoints[j];
        }
        car.GetComponent<AI>().Lane = Waypoints;
    }

    void SpawnCar()
    {
        Instantiate(Car, new Vector3(0f, 1.5f, 0f), Quaternion.identity);
    }

    void SpawnObsatcle(Road a)
    {
		GameObject clone = (GameObject)Instantiate(ObstacleGenerator, new Vector3(0, 0, 0), Quaternion.identity);
		if (a.name == "upDown" || a.name == "downUp") {
			clone.GetComponent<SpawnObstacles> ().StartPoint = new Vector3 (a.startPos.x+a.postion.x , a.startPos.y+a.postion.y, a.startPos.z-30);
			clone.GetComponent<SpawnObstacles> ().EndPoint = new Vector3 (a.startPos.x+a.postion.x , a.startPos.y+a.postion.y, a.startPos.z+30);
			clone.GetComponent<SpawnObstacles> ().Rotated = false;
		} else {
			clone.GetComponent<SpawnObstacles> ().StartPoint = new Vector3 (a.startPos.x - 30 , a.startPos.y+a.postion.y, a.startPos.z+a.postion.z);
			clone.GetComponent<SpawnObstacles> ().EndPoint = new Vector3 (a.startPos.x + 30 , a.startPos.y+a.postion.y, a.startPos.z+a.postion.z);
			clone.GetComponent<SpawnObstacles> ().Rotated = true;
		}

    }
}
