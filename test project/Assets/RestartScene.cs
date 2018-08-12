using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;
#if UNITY_EDITOR 
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

[RequireComponent(typeof(Button))]
public class RestartScene : MonoBehaviour {

	private Button restart_button;

	// Use this for initialization
	void Start () { 
		restart_button = GetComponent<Button>();
		restart_button.onClick.AddListener(TaskOnClick);
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void TaskOnClick() {
		GameObject.Find("NetworkManager").GetComponent<networkSocket>().done = true;
	}
}
