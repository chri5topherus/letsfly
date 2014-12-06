using UnityEngine;
using System.Collections;


public class GameLogic : MonoBehaviour {


	public ArrayList planeChickens;
	public float remainingSeconds = 300.0f;
	public bool gameStarted = true;
	GameObject plane;

	public int caughtChickens = 0;


	// Use this for initialization
	void Start () {
		planeChickens = new ArrayList ();
		plane = GameObject.Find ("spinning_plane(Clone)");

	}
	
	// Update is called once per frame
	void Update () {

		if(gameStarted) {
			remainingSeconds -= Time.deltaTime;
			if(remainingSeconds<0.0f) 
				remainingSeconds = 0.0f;

			//Debug.Log (getTime ());
		}

		//Debug.Log (planeChickens.Count);
	
	}

	string getTime() {
		int min = (int) (remainingSeconds / 60.0f);
		int sec = (int) remainingSeconds - (min * 60);
		return min + ":" + sec;
	}


	public void collectChicken(GameObject chicken) {
		planeChickens.Add (chicken);
	}

	public void dropChickens() {
	
		if(isPlaneOverFence()) {
			Debug.Log ("Drop Chickens");
			foreach(GameObject go in planeChickens.ToArray()) {
				caughtChickens++;
				go.transform.parent = GameObject.Find ("SpawnTest").transform;
				go.transform.position = new Vector3(2.8f, 0.0f, 45.0f);
				go.collider.enabled = true;
				go.rigidbody.isKinematic = false;
				((Chicken) go.GetComponent<Chicken>()).isMoving = true;
			}
		}
	}

	public bool isPlaneOverFence() {
		Vector3 pos = plane.transform.position;
		if(pos.z >= 42.0f && pos.z <=56.0f && pos.x >=-7.5f && pos.x <= 6.6f) {
			return true;
		}
		return false;
	}
}
