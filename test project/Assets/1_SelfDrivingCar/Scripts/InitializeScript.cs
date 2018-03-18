using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class Startup {
	static Startup()
	{
		Debug.Log("Up and running");
		int count = UnityEngine.Random.Range (0, 6);
	}
}