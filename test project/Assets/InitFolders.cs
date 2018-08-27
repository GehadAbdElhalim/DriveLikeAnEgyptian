using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class InitFolders : MonoBehaviour {
	public bool LFSM;
	public bool LFR1;
	public bool LFR2;
	
	void Start () {
		setSessionNumber ();
	}
	
	
	
	void setSessionNumber () {
		int SMHN =0;int RS1HN=0;int RS2HN=0;

		
		if(LFSM)
		 	SMHN = getSNFStreetManger();
		if(LFR1)
			RS1HN = getSNFRecording1();
		if(LFR2)
			RS2HN = getSNFRecording2();
		int maxNum = Math.Max(SMHN,Math.Max(RS1HN,RS2HN));
		if(LFSM)
			SSNFStreetManger(Application.dataPath + "/LogFiles/CityDesign/0"+maxNum+".csv");
		if(LFR1)
			SSNFRecording1(Application.dataPath + "/LogFiles/AdCar/1"+maxNum+".csv");
		if(LFR2)
			SSNFRecording2(Application.dataPath + "/LogFiles/normalCar/2"+maxNum+".csv");
	}
	void SSNFStreetManger(String PathAndName){
		GameObject go = GameObject.Find("StreetManger (1)");
		CityDesgin1 cd1 =(CityDesgin1) go.GetComponent(typeof(CityDesgin1));
		File.Create(PathAndName);
		cd1.changeFileLocation( PathAndName);
	}
	void SSNFRecording1 (String PathAndName){
		GameObject go = GameObject.Find("CarADNav(Red)(Clone)");
		UnityStandardAssets.Vehicles.Car.RecordingScriptAbdelrahman RS1 =(UnityStandardAssets.Vehicles.Car.RecordingScriptAbdelrahman) go.GetComponent(typeof(UnityStandardAssets.Vehicles.Car.RecordingScriptAbdelrahman));
		File.Create(PathAndName);
		RS1.changeFileLocation( PathAndName);
	}
	void SSNFRecording2 (String PathAndName){
		GameObject go = GameObject.Find("Car(Clone)");
		UnityStandardAssets.Vehicles.Car.RecordingScriptAbdelrahman RS2 =(UnityStandardAssets.Vehicles.Car.RecordingScriptAbdelrahman) go.GetComponent(typeof(UnityStandardAssets.Vehicles.Car.RecordingScriptAbdelrahman));
		File.Create(PathAndName);
		RS2.changeFileLocation( PathAndName);
	}
	int getSNFStreetManger(){
		string [] filesArr = Directory.GetFiles(Application.dataPath + "/LogFiles/CityDesign");
		List<int> FilesNumber = new List<int>();
				for(int i=0;i<filesArr.Length;i++){
					string [] dirSplit = filesArr[i].Split(new string[]{"\\"}, StringSplitOptions.None);
					if(dirSplit[dirSplit.Length-1].EndsWith(".csv")){
						if(dirSplit[dirSplit.Length-1].Length >4){
						String Filename =dirSplit[dirSplit.Length-1].Trim().Substring(1,dirSplit[dirSplit.Length-1].Length-5); 
						int tempNum = 0;
						if(int.TryParse(Filename ,out tempNum  )){
							FilesNumber.Add(tempNum);
						}
						}
					}
				}
				int maxNum = 0;
				for(int i=0;i<FilesNumber.Count;i++){
					maxNum = Math.Max (FilesNumber[i],maxNum);
				}maxNum+=1 ;
				return maxNum;
	}
	int getSNFRecording1(){
		string [] filesArr = Directory.GetFiles(Application.dataPath + "/LogFiles/AdCar");
		List<int> FilesNumber = new List<int>();
				for(int i=0;i<filesArr.Length;i++){
					string [] dirSplit = filesArr[i].Split(new string[]{"\\"}, StringSplitOptions.None);
					if(dirSplit[dirSplit.Length-1].EndsWith(".csv")){
						if(dirSplit[dirSplit.Length-1].Length >4){
						String Filename =dirSplit[dirSplit.Length-1].Trim().Substring(1,dirSplit[dirSplit.Length-1].Length-5); 
						int tempNum = 0;
						if(int.TryParse(Filename ,out tempNum  )){
							FilesNumber.Add(tempNum);
						}
						}
					}
				}
				int maxNum = 0;
				for(int i=0;i<FilesNumber.Count;i++){
					maxNum = Math.Max (FilesNumber[i],maxNum);
				}maxNum+=1 ;
				return maxNum;
		
	}
	int getSNFRecording2(){
		string [] filesArr = Directory.GetFiles(Application.dataPath + "/LogFiles/normalCar");
		List<int> FilesNumber = new List<int>();
				for(int i=0;i<filesArr.Length;i++){
					string [] dirSplit = filesArr[i].Split(new string[]{"\\"}, StringSplitOptions.None);
					if(dirSplit[dirSplit.Length-1].EndsWith(".csv")){
						if(dirSplit[dirSplit.Length-1].Length >4){
						String Filename =dirSplit[dirSplit.Length-1].Trim().Substring(1,dirSplit[dirSplit.Length-1].Length-5); 
						int tempNum = 0;
						if(int.TryParse(Filename ,out tempNum  )){
							FilesNumber.Add(tempNum);
						}
						}
					}
				}
				int maxNum = 0;
				for(int i=0;i<FilesNumber.Count;i++){
					maxNum = Math.Max (FilesNumber[i],maxNum);
				}maxNum+=1 ;
				return maxNum;
	}
}
