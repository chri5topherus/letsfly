using UnityEngine;
using System.Collections;

public class Fadenkreuz : MonoBehaviour {

	public GameObject plane = null;
	public Transform fadenkreuz_prefab;

	// Use this for initialization
	void Start () {
		Debug.Log ("Fadenkreuz initialized");
		plane = GameObject.Find ("spinning_plane(Clone)");
		//transform.parent = plane.transform;

		if(plane != null) {
	
	
		GameObject.Find ("fadenkreuz_prefab(Clone)").transform.parent = plane.transform;
			GameObject.Find ("fadenkreuz_prefab(Clone)").transform.position = new Vector3(0.0f, -50.0f, 0.0f);
		

		}

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
