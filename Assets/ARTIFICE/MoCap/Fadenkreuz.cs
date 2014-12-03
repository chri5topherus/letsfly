using UnityEngine;
using System.Collections;

public class Fadenkreuz : MonoBehaviour {

	public GameObject plane = null;
	public Transform fadenkreuz_prefab;

	// Use this for initialization
	void Start () {
		plane = GameObject.Find ("spinning_plane(Clone)");
		transform.parent = plane.transform;
		Vector3 spawnPos = new Vector3 (plane.transform.position.x, -1.39f, plane.transform.position.z - 25.0f);
		Network.Instantiate (fadenkreuz_prefab, new Vector3(10.0f, 1.0f, -10.0f), Quaternion.identity, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
