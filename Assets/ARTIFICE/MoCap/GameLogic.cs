using UnityEngine;
using System.Collections;


public class GameLogic : MonoBehaviour {


	public ArrayList planeChickens;
	public float remainingSeconds = 300.0f;
	public bool gameStarted = true;


	// Use this for initialization
	void Start () {
		planeChickens = new ArrayList ();

	}
	
	// Update is called once per frame
	void Update () {

		if(gameStarted) {
			remainingSeconds -= Time.deltaTime;
			if(remainingSeconds<0.0f) 
				remainingSeconds = 0.0f;

			//Debug.Log (getTime ());
		}

		Debug.Log (planeChickens.Count);
	
	}

	string getTime() {
		int min = (int) (remainingSeconds / 60.0f);
		int sec = (int) remainingSeconds - (min * 60);
		return min + ":" + sec;
	}

	[RPC]
	public void collectChicken(GameObject chicken) {
		planeChickens.Add (chicken);
	}
}
