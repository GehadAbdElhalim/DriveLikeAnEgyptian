using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;
using UnityEditor.SceneManagement;

[RequireComponent(typeof(Button))]
public class QuitScript : MonoBehaviour {
	
	private Button quit_button;

	// Use this for initialization
	void Start () {
		quit_button = GetComponent<Button>();
		quit_button.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TaskOnClick(){
		GameObject.Find("NetworkManager").GetComponent<networkSocket>().quit = true;
	}
}
