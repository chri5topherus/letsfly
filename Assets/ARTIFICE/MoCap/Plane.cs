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
		
	}
	
	/// <summary>
    /// Update is called once per frame
    /// </summary>
	void Update () 
	{


		if(Input.GetKey (KeyCode.UpArrow))
		{
			this.networkView.RPC("movePlane", RPCMode.AllBuffered);
		}


				
	}


	void OnEnable()
	{
		Debug.Log("Avatar Test");
	}



	[RPC]
	public virtual void movePlane() {
		NetworkView nV=this.GetComponent<NetworkView>();
		if((!nV)||(nV.isMine))
		{
			Vector3 newPos = new Vector3(transform.position.x+0.01f, transform.position.y, transform.position.z);
			transform.position = newPos;
				
		}
	}



}



