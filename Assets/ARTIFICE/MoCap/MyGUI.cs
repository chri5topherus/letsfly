using UnityEngine;
using System.Collections;
using System;

public class MyGUI : MonoBehaviour {


	/// <summary>
	/// Unity Callback
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		Application.targetFrameRate = 60;
	}
	
	
	// <summary>
	/// Unity Callback
	/// OnGUI is called for rendering and handling GUI events.
	/// </summary>
	void OnGUI () {
		Rect windowRect2 = new Rect (240,0,240,140);
		//windowRect2 = GUI.Window(0, windowRect2, MakeWindow, "Your Time");
	}



	
	/// <summary>
	/// Sets up the GUI-elements
	/// </summary>
	/// <param name="id"></param>
	void MakeWindow(int id) {
		

			
			GUILayout.Label("CTime:");
			
			GUILayout.Space (5);
			

			GUILayout.Space (10);
	
	
	}

	
	
	
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



}
