using UnityEngine;
using System.Collections;

public class Chicken : MonoBehaviour {


	public Vector3 direction = new Vector3(0.0f, 0.0f, -1.0f);
	public float speed;
	public float timePassed = 0.0f;
	public bool goingLeft = false;
	public bool goingRight = false;


	// Use this for initialization
	void Start () {
		direction = new Vector3(Random.Range(-100,100), 0, Random.Range(-100,100));
		direction.Normalize ();
		float xPos = Random.Range (-40, 40);
		float yPos = Random.Range (-40, 40);
		Vector3 spawnPosition = new Vector3 (xPos, 0.0f, yPos);
		transform.position = spawnPosition;
		speed = 1.5f;
		goingLeft = false;
		goingRight = false;


		transform.rotation = Quaternion.LookRotation (Quaternion.AngleAxis (90.0f, Vector3.up) * direction);

	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		Vector3 newPos = new Vector3(transform.position.x+direction.x*speed*Time.deltaTime, transform.position.y+direction.y*speed*Time.deltaTime, transform.position.z + direction.z*speed*Time.deltaTime);
		transform.position = newPos;

		if (timePassed >= 1) {
			float x = Random.Range (0.0f, 100.0f);
			if (x < 50.0f) {
					goingLeft = true;
					goingRight = false;
			} else if (x > 50.0f) {
					goingRight = true;
					goingLeft = false;
			}
			timePassed = 0.0f;
		}

		if(goingLeft) {
			float x = Random.Range (0.0f, 100.0f);
			if (x < 20.0f) {
				direction = Quaternion.AngleAxis (-2.0f, Vector3.up) * direction;
				transform.rotation = Quaternion.AngleAxis (-2.0f, Vector3.up) * transform.rotation;
			} 
		}

		if(goingRight) {
			float x = Random.Range (0.0f, 100.0f);
			if (x < 20.0f) {
				direction = Quaternion.AngleAxis (2.0f, Vector3.up) * direction;
				transform.rotation = Quaternion.AngleAxis (2.0f, Vector3.up) * transform.rotation;
			} 
		}

	}

	void OnCollisionEnter(Collision theCollision){
		Debug.Log ("hallo2");
	}
}
