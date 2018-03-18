using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityDesgin2 : MonoBehaviour
{

    public GameObject streetLane60mVertical;
    public GameObject streetLane60mHorizontal;
    public GameObject streetCrossXRoads;
    public GameObject streetTurn90DownLeft;
    public GameObject streetTurn90TopLeft;
    public GameObject streetTurn90DownRight;
    public GameObject streetTurn90TopRight;
    
    public int GridSize;
    const int up = 0;
    const int right = 1;
    const int left = 2;
    const int down = 3;
    Road[] roadKinds = new Road[16];
    
    Vector3[] start;
    Vector3[] end;
    Road[,] map;
    struct Road
    {
        public GameObject RoadName;
        public int start;
        public int end;
        public int offestX;
        public int offestZ;
        public Vector3 postion;
        public Quaternion Rotation;
        public string name;

        public void set(GameObject RoadName, int start, int end, int offestX, int offestZ, Vector3 Vec, Quaternion Quat, string name)
        {
            this.RoadName = RoadName;
            this.start = start;
            this.end = end;
            this.offestX = offestX;
            this.offestZ = offestZ;
            this.postion = Vec;
            this.Rotation = Quat;
            this.name = name;
        }
    }
    void Start()
    {
        map = new Road[GridSize, GridSize];
        start = new Vector3[GridSize]; end = new Vector3[GridSize];
        Road upDown = new Road();
        upDown.set(streetLane60mVertical, up, down, 0, -40, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "upDown");
        roadKinds[0] = upDown;

        Road downUp = new Road();
        downUp.set(streetLane60mVertical, down, up, 0, 40, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), "downUp");
        roadKinds[1] = downUp;

        Road leftRight = new Road();
        leftRight.set(streetLane60mHorizontal, left, right, 40, 0, new Vector3(0, 0, 0), Quaternion.Euler(0, -90, 0), "leftRight");
        roadKinds[2] = leftRight;

        Road rightLeft = new Road();
        rightLeft.set(streetLane60mHorizontal, right, left, -40, 0, new Vector3(0, 0, 0), Quaternion.Euler(0, -90, 0), "rightLeft");
        roadKinds[3] = rightLeft;


        Road crossRoadUpDown = new Road();
        crossRoadUpDown.set(streetCrossXRoads, up, down, 0, -40, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0), "crossRoadUpDown");
        roadKinds[4] = crossRoadUpDown;

        Road crossRoadDownUp = new Road();
        crossRoadDownUp.set(streetCrossXRoads, down, up, 0, 40, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0), "crossRoadDownUp");
        roadKinds[5] = crossRoadDownUp;

        Road crossRoadLeftRight = new Road();
        crossRoadLeftRight.set(streetCrossXRoads, left, right, 40, 0, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0), "crossRoadLeftRight");
        roadKinds[6] = crossRoadLeftRight;

        Road crossRoadRightLeft = new Road();
        crossRoadRightLeft.set(streetCrossXRoads, right, left, -40, 0, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0), "crossRoadRightLeft");
        roadKinds[7] = crossRoadRightLeft;

        Road LeftDown = new Road();
        LeftDown.set(streetTurn90DownLeft, left, down, 26, -67, new Vector3(12.8f, 0,-12.5f), Quaternion.Euler(-90, 315 + 135, 0), "LeftDown");
        roadKinds[8] = LeftDown;
        Road DownLeft = new Road();
        DownLeft.set(streetTurn90DownLeft, down, left, -92, 110, new Vector3(-13f, 0, 14), Quaternion.Euler(-90, 315 + 135, 0), "DownLeft");
        roadKinds[9] = DownLeft;

        Road LeftTop = new Road();
        LeftTop.set(streetTurn90TopLeft, left, up, 92, 110, new Vector3(12.8f, 0, 12.5f), Quaternion.Euler(-90, 45 + 135, 0), "LeftTop");
        roadKinds[10] = LeftTop;
        Road TopLeft = new Road();
        TopLeft.set(streetTurn90TopLeft, up, left, -110, -92, new Vector3(0, 0, 0), Quaternion.Euler(-90, 45 + 135, 0), "TopLeft");
        roadKinds[11] = TopLeft;

        Road TopRight = new Road();
        TopRight.set(streetTurn90TopRight, up, right, 110, -92, new Vector3(15, 0, -5), Quaternion.Euler(-90, 135 + 135, 0), "TopRight");
        roadKinds[12] = TopRight;
        Road RightTop = new Road();
        RightTop.set(streetTurn90TopRight, right, up, -92, 110, new Vector3(-14, 0, 13), Quaternion.Euler(-90, 135 + 135, 0), "RightTop");
        roadKinds[13] = RightTop;

        Road RightDown = new Road();
        RightDown.set(streetTurn90DownRight, right, down, -92, -110, new Vector3(-14, 0, -13), Quaternion.Euler(-90, 225 + 135, 0), "RightDown");
        roadKinds[14] = RightDown;
        Road DownRight = new Road();
        DownRight.set(streetTurn90DownRight, down, right, 110, 92, new Vector3(13, 0, 15), Quaternion.Euler(-90, 225 + 135, 0), "DownRight");
        roadKinds[15] = DownRight;

        Road[] arr = new Road[GridSize];
        int x = 0;
        int z = 0;
        /*arr[0] = upDown;
        // Instantiate(arr[0].RoadName, arr[0].postion+ new Vector3(x, 0, z), arr[0].Rotation);
        start[0] = new Vector3(x, 0, z);
        x += arr[0].offestX;
        z += arr[0].offestZ;
        end[0] = new Vector3(x, 0, z);
        for (int i = 1; i < GridSize; i++)
        {
            start[i] = new Vector3(x, 0, z);

            /* do
             {
                 arr[i] = chooseRoad(arr[i - 1].end);

             } while (!makeSure(arr[i], start[i],i));*/
        /*    arr[i] = chooseRoad(arr[i - 1].end);
            if (makeSure(arr[i], start[i], i))
            {
                i = 1;
            }

            // Instantiate(arr[i].RoadName, arr[i].postion + new Vector3(x, 0, z), arr[i].Rotation);
            x += arr[i].offestX;
            z += arr[i].offestZ;
            end[i] = new Vector3(x, 0, z);
        }
        
        for (int i = 1; i < GridSize; i++)
        {
            print(i);
            Instantiate(arr[i].RoadName, arr[i].postion + start[i], arr[i].Rotation);
        }

        */


        int p = 0;
         for (int i = 8; i < roadKinds.Length; i++)
         {
             for(int j = 0;j<roadKinds.Length;j++)
             {
                 if(agnist(roadKinds[i].end) == roadKinds[j].start)
                 {
                     x = 0;
                     z = 0;
                     Instantiate(roadKinds[i].RoadName, roadKinds[i].postion + new Vector3(x + p, 0, z), roadKinds[i].Rotation);
                     x += roadKinds[i].offestX;
                     z += roadKinds[i].offestZ;
                     Instantiate(roadKinds[j].RoadName, roadKinds[j].postion + new Vector3(x + p, 0, z), roadKinds[j].Rotation); 
                     p += 500; print(i+"==========="+j+"//"+ agnist(roadKinds[i].end)+"//"+ roadKinds[j].start);

                 }
             }
         }
    }

    Road chooseRoad(int end)
    {
        if (end == up)
        {
            return lookFor(down);
        }
        else if (end == down)
        {
            return lookFor(up);
        }
        else if (end == left)
        {
            return lookFor(right);
        }
        return lookFor(left);
    }
    Road lookFor(int start)
    {
        List<Road> choosen = new List<Road>();
        for (int i = 0; i < roadKinds.Length; i++)
        {
            if (roadKinds[i].start == start)
                choosen.Add(roadKinds[i]);
        }
        return choosen[Random.Range(0, choosen.Count)];
    }
    int agnist(int end)
    {
        if (end == up)
        {
            return down;
        }
        else if (end == down)
        {
            return up;
        }
        else if (end == left)
        {
            return right;
        }
        return left;
    }
    bool makeSure(Road road, Vector3 start2, int index)
    {
        for (int i = 0; i < index; i++)
        {
            if (intersect(new Vector3(road.offestX + start2.x, 0, road.offestZ + start2.z), start2, start[i], end[i]))
            {
                return false;
            }
        }
        return true;
    }

    bool intersect(Vector3 p1, Vector3 q1, Vector3 p2, Vector3 q2)
    {

        int o1 = orientation(p1, q1, p2);
        int o2 = orientation(p1, q1, q2);
        int o3 = orientation(p2, q2, p1);
        int o4 = orientation(p2, q2, q1);


        if (o1 != o2 && o3 != o4)
            return true;

        // Special Cases
        // p1, q1 and p2 are colinear and p2 lies on segment p1q1
        if (o1 == 0 && onSegment(p1, p2, q1)) return true;

        // p1, q1 and q2 are colinear and q2 lies on segment p1q1
        if (o2 == 0 && onSegment(p1, q2, q1)) return true;

        // p2, q2 and p1 are colinear and p1 lies on segment p2q2
        if (o3 == 0 && onSegment(p2, p1, q2)) return true;

        // p2, q2 and q1 are colinear and q1 lies on segment p2q2
        if (o4 == 0 && onSegment(p2, q1, q2)) return true;

        return false; // Doesn't fall in any of the above cases

    }
    bool onSegment(Vector3 p, Vector3 q, Vector3 r)
    {
        if (q.x <= Mathf.Max(p.x, r.x) && q.x >= Mathf.Min(p.x, r.x) &&
            q.y <= Mathf.Max(p.y, r.y) && q.y >= Mathf.Min(p.y, r.y))
            return true;

        return false;
    }
    int orientation(Vector3 p, Vector3 q, Vector3 r)
    {


        int val = (int)((q.y - p.y) * (r.x - q.x) -
                  (q.x - p.x) * (r.y - q.y));

        if (val == 0) return 0;  // colinear

        return (val > 0) ? 1 : 2; // clock or counterclock wise

    }
    void Update()
    {

    }
}
