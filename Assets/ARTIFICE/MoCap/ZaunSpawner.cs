using UnityEngine;
using System.Collections;

public class ZaunSpawner : MonoBehaviour {

	public Transform zaun_prefab1;

	// Use this for initialization
	void Start () {

		Network.Instantiate(zaun_prefab1, new Vector3(0.0f, 0.0f, 10.0f), Quaternion.identity, 0);
		Network.Instantiate(zaun_prefab1, new Vector3(0.0f, 0.0f, 10.0f), Quaternion.identity, 0);
		Network.Instantiate(zaun_prefab1, new Vector3(0.0f, 0.0f, 10.0f), Quaternion.identity, 0);
		Network.Instantiate(zaun_prefab1, new Vector3(0.0f, 0.0f, 10.0f), Quaternion.identity, 0);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
