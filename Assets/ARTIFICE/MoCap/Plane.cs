/* =====================================================================================
 * ARTiFICe - Augmented Reality Framework for Distributed Collaboration
 * ====================================================================================
 * Copyright (c) 2010-2012 
 * 
 * Annette Mossel, Christian Schönauer, Georg Gerstweiler, Hannes Kaufmann
 * mossel | schoenauer | gerstweiler | kaufmann @ims.tuwien.ac.at
 * Interactive Media Systems Group, Vienna University of Technology, Austria
 * www.ims.tuwien.ac.at
 * 
 * ====================================================================================
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *  
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *  
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * =====================================================================================
 */

using UnityEngine;
using System.Collections;
using System;
	


/// <summary>
/// Provides tracking data received from Kinect to Unity3D game objects.  
/// </summary>
public class Plane : MonoBehaviour {
	//Array of Limbs that is used to hold the GameObjects of the Character, which are to be transformed
	public Transform translationObject=null;
	public Vector3 direction = new Vector3(0.0f, 0.0f, -1.0f);
	public bool isMoving = true;
	public bool isFlying = false;

	public float minHeight = 0.0f;
	public float maxHeight = 10.5f;
	public float maxSpeed = 10.0f;
	public float minSpeed = 0.0f;
	public float speed = 0.0f;




	private bool initialized=false;//True if StartTrackingLimb has been called for all limbs

	/// <summary>
    /// Use Start() for initialization
    /// </summary>
	void Start () 
	{
		//reset root structure rotation so we dont store it in the initial rotation
		Quaternion rotOriginal=Quaternion.identity;
		Transform attachedObj=transform.parent;
		
		if(attachedObj)
		{
			if(attachedObj)
			{
				rotOriginal=attachedObj.rotation;
				attachedObj.rotation=Quaternion.identity;
			}
		}
		else
		{
			Debug.Log("Avatar needs to have a parent object");
		}
		

		initialized=true;
		//set rotation of object the avatar is attached to back to what it was
		if(attachedObj)
		{
			attachedObj.rotation=rotOriginal;
		}
		else
		{
			Debug.Log("Error: GameObject with AvatarSkript needs a parent Object to work");
		}

		GameObject.Find ("VirtualCamera").transform.parent = GameObject.Find ("spinning_plane(Clone)").transform;
		speed = 0.0f;
		minHeight = 1.0f;
		maxHeight = 15.0f;
		maxSpeed = 10.0f;
		minSpeed = 2.0f;
		speed = 0.0f;



	}
	
	/// <summary>
    /// Update is called once per frame
    /// </summary>
	void Update () 
	{



		if (isMoving) {

			bool stepMoved = false;
			bool speedUp = false;

			if (Input.GetKey (KeyCode.S)) {
				speedUp = true;
			}


			if (Input.GetKey (KeyCode.UpArrow)) {
				this.networkView.RPC ("movePlane", RPCMode.AllBuffered, false, false, true, false, speedUp);
				stepMoved = true;
			}


			if (Input.GetKey (KeyCode.DownArrow)) {
				this.networkView.RPC ("movePlane", RPCMode.AllBuffered, false, false, false, true, speedUp);
				stepMoved = true;
			}


			if (Input.GetKey (KeyCode.LeftArrow)) {
				this.networkView.RPC ("movePlane", RPCMode.AllBuffered, true, false, false, false, speedUp);
				stepMoved = true;
			}

			if (Input.GetKey (KeyCode.RightArrow)) {
				this.networkView.RPC ("movePlane", RPCMode.AllBuffered, false, true, false, false, speedUp);
				stepMoved = true;
			}

		

			if(Input.GetKey (KeyCode.F) && (speed == minSpeed) & (transform.position.y <= 1.5f)) {
				GameLogic gl = ((GameLogic)GameObject.Find ("GameLogic(Clone)").GetComponent<GameLogic> ());
				gl.isCrashed = false;
				gl.isFinished = true;
				isMoving = false;

			}

			if(!stepMoved) {
				this.networkView.RPC ("movePlane", RPCMode.AllBuffered, false, false, false, false, speedUp);
			}

		}


				
	}


	void OnEnable()
	{
		Debug.Log("Avatar Test.");
	}



	[RPC]
	public virtual void movePlane(bool left, bool right, bool down, bool up, bool speedUp) {



		NetworkView nV=this.GetComponent<NetworkView>();
		if((!nV)||(nV.isMine))
		{
			Vector3 newPos = new Vector3();
			float upOrDown = 0.0f;
			float newHeight = transform.position.y;

			float curveSpeed = 45.0f * Time.deltaTime;

			if(left) {
				direction = Quaternion.AngleAxis(-curveSpeed,  Vector3.up) * direction;
				transform.rotation = Quaternion.AngleAxis (-curveSpeed, Vector3.up) * transform.rotation;
			}
			
			if(right) {
				direction = Quaternion.AngleAxis(curveSpeed, Vector3.up) * direction;
				transform.rotation = Quaternion.AngleAxis (curveSpeed, Vector3.up) * transform.rotation;
			}


			if(speed >= minSpeed) {



				newHeight = transform.position.y + direction.y*speed*Time.deltaTime;
				if(up) {
					upOrDown += speed*Time.deltaTime;
					newHeight = Math.Min (maxHeight, newHeight + upOrDown);
				}

				if(down) {
					upOrDown -= speed*Time.deltaTime;
					newHeight = Math.Max (minHeight, newHeight + upOrDown);
				}
				if(speedUp) {
					
					speed += 0.1f;
					speed = Math.Min(speed, maxSpeed);
				}
			}

			else {
				if(speedUp) {
					speed += 0.01f;
					speed = Math.Min(speed, maxSpeed);
				}
			}
			
			


			if(speed >= minSpeed) {
				speed -= 0.01f;
				speed = Math.Max(minSpeed, speed);
			}

			newPos = new Vector3(transform.position.x+direction.x*speed*Time.deltaTime, newHeight, transform.position.z + direction.z*speed*Time.deltaTime);

			transform.position = newPos;

		}
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Crash");
		isMoving = false;

		GameLogic gl = ((GameLogic)GameObject.Find ("GameLogic(Clone)").GetComponent<GameLogic> ());
		gl.isCrashed = true;
		gl.isFinished = true;
		transform.collider.enabled = false;
		Destroy (transform.rigidbody);
		//transform.rigidbody = null;
	}

}



