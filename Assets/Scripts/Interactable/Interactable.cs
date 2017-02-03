using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/** 
 * The other scripts in this directory inherit this class.
 * This is used to store the NavMeshAgent for each of the smaller items
 **/

public class Interactable : MonoBehaviour {
	public NavMeshAgent playerAgent;

	public virtual void MoveToInteraction(NavMeshAgent playerAgent){
		this.playerAgent = playerAgent;
		playerAgent.stoppingDistance = 2.5f;
		playerAgent.destination = this.transform.position;

		Interact();
	}

	public virtual void Interact(){
		Debug.Log("Interacting with base class.");
	}
}
