using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
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

	public GameObject finish_line;


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
	public PhysicMaterial Rainy;
	public PhysicMaterial Normal;
	public bool isRainy;

    const int up = 0;
    const int right = 1;
    const int left = 2;
    const int down = 3;
    int NumOfCol = 0;
    static Road[] roadKinds = new Road[25];
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

		NumberOfBlocks += 1;

        //Gehad things
        Waypoints = new Vector3[NumberOfBlocks];
		Rainhandler (isRainy);
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

//		Road intersectionRoadUpDown = new Road();
//		intersectionRoadUpDown.set(intersection, up, down, 0, -60, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "intersectionRoadUpDown", new Vector3(0, 0, 0));
//		roadKinds[24] = intersectionRoadUpDown;

		Road intersectionRoadDownUp = new Road();
		intersectionRoadDownUp.set(intersection, down, up, 0, 60, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "intersectionRoadDownUp", new Vector3(0, 0, 0));
		roadKinds[24] = intersectionRoadDownUp;
//
//		Road intersectionRoadLeftRight = new Road();
//		intersectionRoadLeftRight.set(intersection, left, right, 60, 0, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "intersectionRoadLeftRight", new Vector3(0, 0, 0));
//		roadKinds[26] = intersectionRoadLeftRight;
//
//		Road intersectionRoadRightLeft = new Road();
//		intersectionRoadRightLeft.set(intersection, right, left, -60, 0, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "intersectionRoadRightLeft", new Vector3(0, 0, 0));
//		roadKinds[27] = intersectionRoadRightLeft;
		numberOfCarsToSeparation();
		if (AutoGenerte) {
			creatMap ();
		} else {
			ReadJSON ();
		}
		makeWay ();
		//SpawnCar();
		//Spawntraffic ();
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
			//if ( (arr[i].roadType == streetLane60mVertical) || (arr[i].roadType == streetBump) || (arr[i].roadType == streetHole) ) 
			//{
			//	SpawnObsatcle(arr[i]);
			//}
            //Obstacle Spawning
			if (arr [i].roadType == streetTurn90DownLeft) {
				//Waypoints[i] = arr [i].roadType.transform.Find ("Cube (4)").transform.position + arr [i].postion;
				Waypoints [i] = arr [i].postion + arr [i].startPos;
			} else {
				Waypoints [i] = arr [i].postion + arr [i].startPos;
			}
		}
        SpawnCar();
        
        //writeString(ToJSONFromArr(arr),JSONFileWirttern);
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
		// print (JSONObjects);
		string [] RoadsJson  = JSONObjects.Split(new char[]{'*'} );
		// print (RoadsJson[0]);
		Road[] roads = ConvertToRoads (RoadsJson);
		arr= new Road[NumberOfBlocks];;
		for (int i = 0; i < roads.Length; i++) {
			arr [i] = roads [i];
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
		Debug.Log ("please spawn car");
		Instantiate(Car, new Vector3(0f, 1.5f, 0f), Quaternion.Euler(new Vector3(0.0f,180.0f,0.0f)));
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

	void Rainhandler(bool rain){
		if (rain) {
			streetLane60mVertical.GetComponentInChildren<MeshCollider> ().material = Rainy;
			for (int i = 0; i < streetCrossXRoads.GetComponentsInChildren<MeshCollider> ().Length; i++) {
				streetCrossXRoads.GetComponentsInChildren<MeshCollider> () [i].material = Rainy;
			}
			streetTurn90DownLeft.GetComponentInChildren<MeshCollider> ().material = Rainy;
			streetBump.GetComponentInChildren<MeshCollider> ().material = Rainy;
			streetHole.GetComponentInChildren<MeshCollider> ().material = Rainy;
			for (int i = 0; i < intersection.GetComponentsInChildren<MeshCollider> ().Length; i++) {
				intersection.GetComponentsInChildren<MeshCollider> () [i].material = Rainy;
			}
		} else {
			streetLane60mVertical.GetComponentInChildren<MeshCollider> ().material = Normal;
			for (int i = 0; i < streetCrossXRoads.GetComponentsInChildren<MeshCollider> ().Length; i++) {
				streetCrossXRoads.GetComponentsInChildren<MeshCollider> () [i].material = Normal;
			}
			streetTurn90DownLeft.GetComponentInChildren<MeshCollider> ().material = Normal;
			streetBump.GetComponentInChildren<MeshCollider> ().material = Normal;
			streetHole.GetComponentInChildren<MeshCollider> ().material = Normal;
			for (int i = 0; i < intersection.GetComponentsInChildren<MeshCollider> ().Length; i++) {
				intersection.GetComponentsInChildren<MeshCollider> () [i].material = Normal;
			}
		}
	}



	[Header("Traffic")]
	public GameObject strategicCar;
	int TrafficSeparation ;

	public int numberOfCarsInOneLane;
	const float straightSeparation = .5f;
	const float lerpSepration = .1f;
	const float turnSeparation = .05f; 
	const float separationJump = .1f;

	List<Vector3> MiddleNodes = new List<Vector3>();
	List<Vector3> LeftNodes = new List<Vector3>();
	List<Vector3> RightNodes = new List<Vector3>();
	void numberOfCarsToSeparation(){
		if(numberOfCarsInOneLane <= 0){
			TrafficSeparation= (int) 1e9;
		}
		else if (MiddleNodes.Count > numberOfCarsInOneLane )
		{
			TrafficSeparation = MiddleNodes.Count / numberOfCarsInOneLane ;
		} else{
			TrafficSeparation = 1 ;
		}
		 
	}
	void SpawnStrategicCar()
	{
		GameObject car = Instantiate(strategicCar, new Vector3(0, 1.5f, 0), Quaternion.Euler(0,180,0)) as GameObject;
		car.GetComponent<CarEngine> ().midPoints = MiddleNodes; 
		car.GetComponent<CarEngine> ().leftPoints = LeftNodes;
		car.GetComponent<CarEngine> ().rightPoints = RightNodes;
		car.GetComponent<CarEngine> ().separations = separationJump;
		car.GetComponent<CarEngine> ().traffic = false;
		car.GetComponent<CarEngine> ().rev = false;
		car.GetComponent<CarEngine> ().currectNode = 0;

	}


	void SpawntrafficCars(int place)
	{	Vector3 spawnPlace = Vector3.Lerp (MiddleNodes [place], RightNodes [place], Random.Range (0, 1)) + new Vector3 (0, 1.5f, 0);
		GameObject car = Instantiate(strategicCar,spawnPlace, Quaternion.Euler(0,180,0)) as GameObject;
		car.GetComponent<CarEngine> ().midPoints = MiddleNodes; 
		car.GetComponent<CarEngine> ().leftPoints = LeftNodes;
		car.GetComponent<CarEngine> ().rightPoints = RightNodes;
		car.GetComponent<CarEngine> ().separations = separationJump;
		car.GetComponent<CarEngine> ().traffic = true;
		car.GetComponent<CarEngine> ().rev = false;
		car.GetComponent<CarEngine> ().currectNode = place;

	}
	void SpawntrafficRevCars(int place)
	{	Vector3 spawnPlace = Vector3.Lerp (MiddleNodes [place], RightNodes [place], Random.Range (0, 1)) + new Vector3 (0, 1.5f, 0);
		GameObject car = Instantiate(strategicCar,spawnPlace, Quaternion.Euler(0,0,0)) as GameObject;
		car.GetComponent<CarEngine> ().midPoints = MiddleNodes; 
		car.GetComponent<CarEngine> ().leftPoints = LeftNodes;
		car.GetComponent<CarEngine> ().rightPoints = RightNodes;
		car.GetComponent<CarEngine> ().separations = separationJump;
		car.GetComponent<CarEngine> ().traffic = true;
		car.GetComponent<CarEngine> ().rev = true;
		car.GetComponent<CarEngine> ().currectNode = place;
	}
	void makeWayPoint (int i){
		if (i == 0) {
			RightNodes.Add (new Vector3 (-5, 0, 0));

			/*MiddleNodes.Add (new Vector3 (0, 0, 0));
			MiddleNodes.Add (new Vector3 (0, 0, -30));*/
			MiddleNodes.Add (new Vector3 (0, 0, 0));
			StraightPoints (new Vector3 (0, 0, 0) ,  new Vector3 (0, 0, -25) );

			LeftNodes.Add (new Vector3 (5, 0, 0));

		} else { 
			Vector3 lastPoint = MiddleNodes [MiddleNodes.Count - 1]; 
			if (arr [i].start == oppsite (arr [i].end)) { // well this means straight 
				if (arr [i].start == up) {
					StraightPoints (lastPoint , arr [i].startPos + new Vector3 (0, 0, 0) );
					StraightPoints (arr [i].startPos + new Vector3 (0, 0, 0)  , arr [i].startPos + new Vector3 (0, 0, -25) );

				} else if (arr [i].start == down) {
					StraightPoints (lastPoint , arr [i].startPos + new Vector3 (0, 0, 0) );
					StraightPoints (arr [i].startPos + new Vector3 (0, 0, 0)  , arr [i].startPos + new Vector3 (0, 0, +25) );

				} else if (arr [i].start == left) {
					StraightPoints (lastPoint , arr [i].startPos + new Vector3 (0, 0, 0) );
					StraightPoints (arr [i].startPos + new Vector3 (0, 0, 0)  , arr [i].startPos + new Vector3 (+25, 0, 0) );

				} else if (arr [i].start == right) {
					StraightPoints (lastPoint , arr [i].startPos + new Vector3 (0, 0, 0) );
					StraightPoints (arr [i].startPos + new Vector3 (0, 0, 0)  , arr [i].startPos + new Vector3 (-25, 0, 0) );
				} 

			} else {
				//MiddleNodes.Add (arr [i].startPos + arr[i].postion + new Vector3 (0, 0, 0));
				if (arr [i].start == left && arr [i].end == up) {
					//MiddleNodes.Add (arr [i].startPos + arr[i].postion + new Vector3 (-30, 0, -30));
					//MiddleNodes.Add (arr [i].startPos + arr[i].postion + new Vector3 (-25, 0, -25));
					/*MiddleNodes.Add (arr [i].startPos + arr[i].postion + new Vector3 (-20, 0, -12));
					MiddleNodes.Add (arr [i].startPos + arr[i].postion + new Vector3 (-15, 0, -10));
					MiddleNodes.Add (arr [i].startPos + arr[i].postion + new Vector3 (-10, 0, -8));
					MiddleNodes.Add (arr [i].startPos + arr[i].postion + new Vector3 (-5, 0, -5));

					MiddleNodes.Add (arr [i].startPos + arr[i].postion + new Vector3 (0, 0, 0));

					MiddleNodes.Add (arr [i].startPos + arr[i].postion + new Vector3 (5f , 0, 5));
					MiddleNodes.Add (arr [i].startPos + arr[i].postion + new Vector3 (8, 0, 10));
					MiddleNodes.Add (arr [i].startPos + arr[i].postion + new Vector3 (10, 0, 15));
					MiddleNodes.Add (arr [i].startPos + arr[i].postion + new Vector3 (12, 0, 20));*/
					Vector3 p0 = arr [i].startPos + arr [i].postion + new Vector3 (-26, 0, -14);
					Vector3 p1 = arr [i].startPos + arr [i].postion + new Vector3 (0, 0, -10);
					Vector3 p2 = arr [i].startPos + arr[i].postion + new Vector3 (14, 0, 26);
					Vector3 [] Points = BezierArc (p0, p1, p2).ToArray();
					for(int z=0;z<Points.Length;z++){
						MiddleNodes.Add (Points[z]);
					}


				}
				if (arr [i].start == up && arr [i].end == right) {
					Vector3 p0 = arr [i].startPos + arr[i].postion + new Vector3 (-14, 0, 26);
					Vector3 p1 = arr [i].startPos + arr [i].postion + new Vector3 (0, 0, -10);
					Vector3 p2 = arr [i].startPos + arr[i].postion + new Vector3 (26, 0, -14);
					Vector3 [] Points = BezierArc (p0, p1, p2).ToArray();
					for(int z=0;z<Points.Length;z++){
						MiddleNodes.Add (Points[z]);
					}
				}
				if (arr [i].start == right && arr [i].end == down) {
					Vector3 p0 = arr [i].startPos + arr[i].postion + new Vector3 (26, 0, 14);
					Vector3 p1 = arr [i].startPos + arr [i].postion + new Vector3 (0, 0, 10);
					Vector3 p2 = arr [i].startPos + arr[i].postion + new Vector3 (-14, 0, -26);
					Vector3 [] Points = BezierArc (p0, p1, p2).ToArray();
					for(int z=0;z<Points.Length;z++){
						MiddleNodes.Add (Points[z]);
					}
				}
				if (arr [i].start == down && arr [i].end == left) {
					Vector3 p0 = arr [i].startPos + arr[i].postion + new Vector3 (14, 0, -26);
					Vector3 p1 = arr [i].startPos + arr [i].postion + new Vector3 (0, 0, 10);
					Vector3 p2 = arr [i].startPos + arr[i].postion + new Vector3 (-26, 0, 14);
					Vector3 [] Points = BezierArc (p0, p1, p2).ToArray();
					for(int z=0;z<Points.Length;z++){
						MiddleNodes.Add (Points[z]);
					}

				}


				if (arr [i].start == up && arr [i].end == left) {
					Vector3 p0 = arr [i].startPos + arr[i].postion + new Vector3 (14, 0, 26);
					Vector3 p1 = arr [i].startPos + arr [i].postion + new Vector3 (10, 0, 0);
					Vector3 p2 = arr [i].startPos + arr[i].postion + new Vector3 (-26, 0, -14);
					Vector3 [] Points = BezierArc (p0, p1, p2).ToArray();
					for(int z=0;z<Points.Length;z++){
						MiddleNodes.Add (Points[z]);
					}
				}

				if (arr [i].start == left && arr [i].end == down) {
					Vector3 p0 = arr [i].startPos + arr[i].postion + new Vector3 (-26, 0, 14);
					Vector3 p1 = arr [i].startPos + arr [i].postion + new Vector3 (0, 0, 10);
					Vector3 p2 = arr [i].startPos + arr[i].postion + new Vector3 (14, 0, -26);
					Vector3 [] Points = BezierArc (p0, p1, p2).ToArray();
					for(int z=0;z<Points.Length;z++){
						MiddleNodes.Add (Points[z]);
					}
				}


				if (arr [i].start == down && arr [i].end == right) {
					Vector3 p0 = arr [i].startPos + arr[i].postion + new Vector3 (-14, 0, -26);
					Vector3 p1 = arr [i].startPos + arr [i].postion + new Vector3 (0, 0, 10);
					Vector3 p2 = arr [i].startPos + arr[i].postion + new Vector3 (26, 0, 14);
					Vector3 [] Points = BezierArc (p0, p1, p2).ToArray();
					for(int z=0;z<Points.Length;z++){
						MiddleNodes.Add (Points[z]);
					}
				}
				if (arr [i].start == right && arr [i].end == up) {
					Vector3 p0 = arr [i].startPos + arr[i].postion + new Vector3 (26, 0, -14);
					Vector3 p1 = arr [i].startPos + arr [i].postion + new Vector3 (0, 0, -10);
					Vector3 p2 = arr [i].startPos + arr[i].postion + new Vector3 (-14, 0, 26);
					Vector3[] Points = BezierArc (p0, p1, p2).ToArray ();
					for(int z=0;z<Points.Length;z++){
						MiddleNodes.Add (Points[z]);
					}
				}
			}
		}
	}

	void StraightPoints(Vector3 start , Vector3 end ){	
		Vector3 t = straightSeparation*(end - start);
		float c = 1;
		Vector3 currentPoint = start+t;
		while (c > 0) {
			MiddleNodes.Add (currentPoint);
			currentPoint += t;
			c -= straightSeparation;
		}
	}
	void leftNodes(){
		Vector3 [] arrNodes  = MiddleNodes.ToArray();
		for (int i = 1; i < arrNodes.Length; i++) {
			Vector3 v = shiftRotate (arrNodes [i - 1], arrNodes [i], 90);
			LeftNodes.Add (v);
		}

	}
	void rightNodes(){
		Vector3 [] arrNodes  = MiddleNodes.ToArray();
		for (int i = 1; i < arrNodes.Length; i++) {
			Vector3 v = shiftRotate (arrNodes [i - 1], arrNodes [i], -90);
			RightNodes.Add (v);
		}

	}
	int oppsite(int end){

		if(end == up)
		{
			return down;
		} 
		else if(end == down)
		{
			return up;
		}
		else if(end == left)
		{
			return right;
		}
		return left;
	}
	List <Vector3> BezierArc(Vector3 p0,Vector3 p1,Vector3 p2){
		List <Vector3> output = new List<Vector3> ();
		float i=0.0f;
		while (i<=1){
			output.Add (BezierPoint (p0, p1, p2, i));
			i += turnSeparation;
		}
		return output;
	}

	Vector3 BezierPoint(Vector3 p0,Vector3 p1,Vector3 p2, float t){
		Vector3 output = Vector3.zero;
		output.x = (1 - t) * (1 - t) * p0.x + 2 * (1 - t) * t * p1.x + t * t * p2.x;
		output.z = (1 - t) * (1 - t) * p0.z + 2 * (1 - t) * t * p1.z + t * t * p2.z;
		return output;
	}
	Vector3 shiftRotate (Vector3 p0,Vector3 p1 , float theta){
		Vector3 p = rotate (p1-p0, theta);
		p = p.normalized*3.7f;
		return p+p0;
	}

	Vector3 rotate (Vector3 p , float theta) {
		float rad = DEG_TO_RAD (theta);
		return new Vector3 (p.x * Mathf.Cos (rad) - p.z * Mathf.Sin (rad), p.y, p.x * Mathf.Sin (rad) - p.z * Mathf.Cos (rad));
	}

	float DEG_TO_RAD (float theta){
		return theta / 180 * 22 / 7; 
	}
	Vector3 uniteVector (Vector3 p){
		return p.normalized;
	}
	void Spawntraffic(){
		numberOfCarsToSeparation();
		for (int i = TrafficSeparation; i < MiddleNodes.Count - TrafficSeparation; i+=TrafficSeparation) {
			SpawntrafficCars (Random.Range(i-TrafficSeparation/2,i+TrafficSeparation/2));
			SpawntrafficRevCars (Random.Range(i-TrafficSeparation/2,i+TrafficSeparation/2));
		}
	}
	void makeWay(){
		for (int i = 0; i < arr.Length; i++) {
			makeWayPoint (i);
		}
		leftNodes ();
		rightNodes ();
	}
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		for(int i = 0; i < MiddleNodes.Count; i++) {
			Vector3 currentNode = RightNodes[i]+new  Vector3(0,1.5f,0); 
			Vector3 previousNode = LeftNodes[i]+new  Vector3(0,1.5f,0);

			Gizmos.DrawLine(previousNode, currentNode);

			float x =lerpSepration;
			while (x < 1) {

				Gizmos.DrawWireSphere(Vector3.Lerp(previousNode, currentNode,x), 0.3f);
				x += lerpSepration;
			}
		}
		Gizmos.color = Color.red;
		for(int i = 0; i < MiddleNodes.Count; i++) {
			Vector3 currentNode = MiddleNodes [i]+new  Vector3(0,1.5f,0); 
			Vector3 previousNode = Vector3.zero+new  Vector3(0,1.5f,0);

			if (i > 0) {
				previousNode = MiddleNodes[i - 1]+new  Vector3(0,1.5f,0);
			} 
			Gizmos.DrawLine(previousNode, currentNode);
			Gizmos.DrawWireSphere(currentNode, 0.3f);
		}
		Gizmos.color = Color.green;
		for(int i = 0; i < LeftNodes.Count; i++) {
			Vector3 currentNode = LeftNodes [i]+new  Vector3(0,1.5f,0); 
			Vector3 previousNode = Vector3.zero+new  Vector3(0,1.5f,0);

			if (i > 0) {
				previousNode = LeftNodes[i - 1]+new  Vector3(0,1.5f,0);
			} 
			Gizmos.DrawLine(previousNode, currentNode);
			Gizmos.DrawWireSphere(currentNode, 0.3f);
		}

		Gizmos.color = Color.blue;
		for(int i = 0; i < RightNodes.Count; i++) {
			Vector3 currentNode = RightNodes [i]+new  Vector3(0,1.5f,0); 
			Vector3 previousNode = Vector3.zero+new  Vector3(0,1.5f,0);

			if (i > 0) {
				previousNode = RightNodes[i - 1]+new  Vector3(0,1.5f,0);
			} 
			Gizmos.DrawLine(previousNode, currentNode);
			Gizmos.DrawWireSphere(currentNode, 0.3f);

		}
			
	}


}
/* this is a new push cuz this is not working */