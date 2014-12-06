using UnityEngine;
using System.Collections;

public class ChickenSpawner : MonoBehaviour {

	public Transform chicken_prefab;
	// Use this for initialization
	void Start () {
		for (int i = 0; i<30; i++) {
			float xPos = Random.Range (-40, 40);
			float yPos = Random.Range (-40, 40);
			Vector3 spawnPosition = new Vector3 (xPos, 0.0f, yPos);
			//Network.Instantiate(chicken_prefab, spawnPosition, Quaternion.identity, 0);
		}
	

	}
	
	// Update is called once per frame
	void Update () {

	}
}
