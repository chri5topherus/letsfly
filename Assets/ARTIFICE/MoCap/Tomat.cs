using UnityEngine;
using System.Collections;

public class Tomat : MonoBehaviour {

	public float remainingSeconds = 10.0f;


	// Use this for initialization
	void Start () {
		remainingSeconds = 15.0f;

	}
	
	// Update is called once per frame
	void Update () {

		remainingSeconds -= Time.deltaTime;
		if(remainingSeconds<0.0f) {
			//GameObject.Destroy(gameObject);
			transform.position = new Vector3(0.0f, -100.0f, 0.0f);
			remainingSeconds = 15.0f;
		}

	}
}
