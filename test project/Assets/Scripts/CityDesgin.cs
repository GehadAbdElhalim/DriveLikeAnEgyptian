using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityDesgin : MonoBehaviour {
    public GameObject streetLane60mVerticalTopDown;
    public GameObject streetLane60mVerticalDownTop;

    public GameObject streetLane60mHorizontalLeftRight;
    public GameObject streetLane60mHorizontalRightLeft;

    public GameObject streetCrossXRoadsVertical;
    public GameObject streetCrossXRoadsHorizontal;

    public GameObject streetTurn90DownLeft;
    public GameObject streetTurn90TopLeft;
    public GameObject streetTurn90DownRight;
    public GameObject streetTurn90TopRight;

    const int TopDown = 1;
    const int DownTop = 2;
    const int LeftRight = 3;
    const int RightLeft = 4;
    const int CrossTopDown = 5;
    const int CrossDownTop = 6;
    const int CrossRightLeft = 7;
    const int CrossLeftRight = 8;
    const int LeftDownLeft = 9;
    const int DownDownLeft = 10;
    const int LeftTopLeft = 11;
    const int TopTopLeft = 12;
    const int DownDownRight = 13;
    const int TopTopRight = 14;
    const int RightDownRight = 15;
    const int RightTopRight = 16;


    Dictionary<int, GameObject> htbStreets = new Dictionary<int, GameObject>();
    Dictionary<int, int> htbXoffest = new Dictionary<int, int>();
    Dictionary<int, int> htbZoffest = new Dictionary<int, int>();
    Dictionary<int, int []> htbNextPartS = new Dictionary<int, int []>();
    Dictionary<int, Vector3> htbPosition = new Dictionary<int, Vector3>();
    Dictionary<int, Quaternion> htbQuaternion = new Dictionary<int, Quaternion>();




    void Start() {
        //StreetLane60mVertical S60V = new StreetLane60mVertical();
        //S60V.Set(streetLane60mVertical, 0, 60, );
        //1

        htbStreets.Add(TopDown, streetLane60mVerticalTopDown);
        htbXoffest.Add(TopDown, 0);
        htbZoffest.Add(TopDown, 60);
        htbNextPartS.Add(TopDown, new int[] { TopDown, CrossTopDown, DownDownLeft, DownDownRight });
        htbPosition.Add(TopDown, new Vector3(0, 0, 0));
        htbQuaternion.Add(TopDown,  Quaternion.Euler(-90,0,0));
        //2
        htbStreets.Add(DownTop, streetLane60mVerticalDownTop);
        htbXoffest.Add(DownTop, 0);
        htbZoffest.Add(DownTop, -60);
        htbNextPartS.Add(DownTop, new int[] { DownTop, CrossDownTop, TopTopLeft, TopTopRight });
        htbPosition.Add(DownTop, new Vector3(0, 0, 0));
        htbQuaternion.Add(DownTop, Quaternion.Euler(-90, 0, 0));
        //3
        htbStreets.Add(LeftRight, streetLane60mHorizontalLeftRight);
        htbXoffest.Add(LeftRight, 60);
        htbZoffest.Add(LeftRight, 0);
        htbNextPartS.Add(LeftRight, new int[] { LeftRight, CrossLeftRight, LeftDownLeft, LeftTopLeft });
        htbPosition.Add(LeftRight, new Vector3(0, 0, 0));
        htbQuaternion.Add(LeftRight, Quaternion.Euler(-90, -90, 0));
        //4
        htbStreets.Add(RightLeft, streetLane60mHorizontalRightLeft);
        htbXoffest.Add(RightLeft, -60);
        htbZoffest.Add(RightLeft, 0);
        htbNextPartS.Add(RightLeft, new int[] { RightLeft, CrossRightLeft, RightDownRight, RightTopRight });
        htbPosition.Add(RightLeft, new Vector3(0, 0, 0));
        htbQuaternion.Add(RightLeft, Quaternion.Euler(-90, -90, 0));
        //5
        htbStreets.Add(CrossTopDown, streetCrossXRoadsVertical);
        htbXoffest.Add(CrossTopDown, 0);
        htbZoffest.Add(CrossTopDown, -60);
        htbNextPartS.Add(CrossTopDown, new int[] { TopDown, CrossTopDown, TopTopLeft, TopTopRight });
        htbPosition.Add(CrossTopDown, new Vector3(0, 0, 0));
        htbQuaternion.Add(CrossTopDown, Quaternion.Euler(0, 0, 0));
        //6
        htbStreets.Add(CrossDownTop, streetCrossXRoadsVertical);
        htbXoffest.Add(CrossDownTop, 0);
        htbZoffest.Add(CrossDownTop, 60);
        htbNextPartS.Add(CrossDownTop, new int[] { DownTop, CrossDownTop, DownDownLeft, DownDownRight });
        htbPosition.Add(CrossDownTop, new Vector3(0, 0, 0));
        htbQuaternion.Add(CrossDownTop, Quaternion.Euler(0, 0, 0));
        //7

        htbStreets.Add(CrossRightLeft, streetCrossXRoadsHorizontal);
        htbXoffest.Add(CrossRightLeft, -60);
        htbZoffest.Add(CrossRightLeft, 0);
        htbNextPartS.Add(CrossRightLeft, new int[] { RightLeft, CrossRightLeft, RightDownRight, RightTopRight });
        htbPosition.Add(CrossRightLeft, new Vector3(0, 0, 0));
        htbQuaternion.Add(CrossRightLeft, Quaternion.Euler(0, 0, 0));

        //8
        htbStreets.Add(CrossLeftRight, streetCrossXRoadsHorizontal);
        htbXoffest.Add(CrossLeftRight, -60);
        htbZoffest.Add(CrossLeftRight, 0);
        htbNextPartS.Add(CrossLeftRight, new int[] { LeftRight, CrossLeftRight, LeftDownLeft, LeftTopLeft });
        htbPosition.Add(CrossLeftRight, new Vector3(0, 0, 0));
        htbQuaternion.Add(CrossLeftRight, Quaternion.Euler(0, 0, 0));
        //9
        htbStreets.Add(DownDownLeft, streetTurn90DownLeft);
        htbXoffest.Add(DownDownLeft,-80);
        htbZoffest.Add(DownDownLeft, 15);
        htbNextPartS.Add(DownDownLeft, new int[] { RightLeft, CrossRightLeft, RightDownRight, RightTopRight });
        htbPosition.Add(DownDownLeft, new Vector3(-15, 0, 0));
        htbQuaternion.Add(DownDownLeft, Quaternion.Euler(-90, 315, 0));
        //10
        htbStreets.Add(LeftDownLeft, streetTurn90DownLeft);
        htbXoffest.Add(LeftDownLeft, 15);
        htbZoffest.Add(LeftDownLeft, -80);
        htbNextPartS.Add(LeftDownLeft, new int[] {TopDown, CrossTopDown, TopTopLeft, TopTopRight });
        htbPosition.Add(LeftDownLeft, new Vector3(0, 0, -15));
        htbQuaternion.Add(LeftDownLeft, Quaternion.Euler(-90, 315, 0));
        //
        htbStreets.Add(TopTopLeft, streetTurn90TopLeft);
        htbXoffest.Add(TopTopLeft, -80);
        htbZoffest.Add(TopTopLeft, -15);
        htbNextPartS.Add(TopTopLeft, new int[] { RightLeft, CrossRightLeft, RightDownRight, RightTopRight });
        htbPosition.Add(TopTopLeft, new Vector3(-15, 0, 0));
        htbQuaternion.Add(TopTopLeft, Quaternion.Euler(-90, 45, 0));

        htbStreets.Add(LeftTopLeft, streetTurn90TopLeft);
        htbXoffest.Add(LeftTopLeft, 15);
        htbZoffest.Add(LeftTopLeft, 80);
        htbNextPartS.Add(LeftTopLeft, new int[] { DownTop, CrossDownTop, DownDownRight, DownDownLeft });
        htbPosition.Add(LeftTopLeft, new Vector3(0, 0, 15));
        htbQuaternion.Add(LeftTopLeft, Quaternion.Euler(-90, 45, 0));
        //

        htbStreets.Add(DownDownRight, streetTurn90DownRight);
        htbXoffest.Add(DownDownRight, 80);
        htbZoffest.Add(DownDownRight, 20);
        htbNextPartS.Add(DownDownRight, new int[] { LeftRight, CrossLeftRight, LeftDownLeft, LeftTopLeft });
        htbPosition.Add(DownDownRight, new Vector3(15, 0, 5));
        htbQuaternion.Add(DownDownRight, Quaternion.Euler(-90, 225, 0));


        htbStreets.Add(RightDownRight, streetTurn90DownRight);
        htbXoffest.Add(RightDownRight, -15);
        htbZoffest.Add(RightDownRight,-80);
        htbNextPartS.Add(RightDownRight, new int[] { TopDown, CrossTopDown, TopTopLeft, TopTopRight });
        htbPosition.Add(RightDownRight, new Vector3(0, 0, -15));
        htbQuaternion.Add(RightDownRight, Quaternion.Euler(-90, 225, 0));

        htbStreets.Add(RightTopRight, streetTurn90TopRight);
        htbXoffest.Add(RightTopRight, -15);
        htbZoffest.Add(RightTopRight, 80);
        htbNextPartS.Add(RightTopRight, new int[] { DownTop, CrossDownTop, DownDownLeft, DownDownRight });
        htbPosition.Add(RightTopRight, new Vector3(0, 0, 15));
        htbQuaternion.Add(RightTopRight, Quaternion.Euler(-90, 135, 0));

        htbStreets.Add(TopTopRight, streetTurn90TopRight);
        htbXoffest.Add(TopTopRight,80);
        htbZoffest.Add(TopTopRight, -20);
        htbNextPartS.Add(TopTopRight, new int[] { LeftRight, CrossLeftRight, LeftDownLeft, LeftTopLeft });
        htbPosition.Add(TopTopRight, new Vector3(15, 0, -5));
        htbQuaternion.Add(TopTopRight, Quaternion.Euler(-90, 135, 0));

        int [] arr = new int [100];
        int x = 0;
        int z = 0;
      /*     for (int i = 1; i < 17; i++)
            {
                x += 60;
                z += 60;
                print(i);
                Instantiate(htbStreets[i], htbPosition[i]+new Vector3(x,0,z), htbQuaternion[i]);
            }*/
          arr[0] = RightLeft;
       Instantiate(htbStreets[arr[0]], htbPosition[arr[0]] + new Vector3(x, 0, z), htbQuaternion[arr[0]]);
       x += htbXoffest[RightLeft];
           z += htbZoffest[RightLeft];

         for (int i = 1; i < 100; i++){
                arr[i] = htbNextPartS[arr[i - 1]][Random.Range(0,htbNextPartS[arr[i - 1]].Length)];
                 Instantiate(htbStreets[arr[i]], htbPosition[arr[i]] + new Vector3(x,0,z), htbQuaternion[arr[i]]);
                 x += htbXoffest[arr[i]];
                 z += htbZoffest[arr[i]];


             print(arr[i] + " x:" + x+" z:"+ z );
              Instantiate(htbStreets[arr[i]], new Vector3(x, 0, z), new Quaternion(0, 0, 0, 0));
               x += 60;
               z += 60;

        }
   /*   int p = 0;
        for (int i = 1 ; i < 17; i++)
        {
            for (int j = 0; j < htbNextPartS[i].Length; j++)
            {
                x = 0;
                z = 0;
                Instantiate(htbStreets[i], htbPosition[i] + new Vector3(x+p,0,z ), htbQuaternion[i]);
                x += htbXoffest[i];
                z += htbZoffest[i];
                Instantiate(htbStreets[htbNextPartS[i][j]], htbPosition[htbNextPartS[i][j]] + new Vector3(x+ p, 0, z ), htbQuaternion[htbNextPartS[i][j]]);
                p += 500;
            }
        }*/
        }
   

    void Update () {
		
	}
}
