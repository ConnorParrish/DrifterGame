using UnityEngine;
using System.Collections;

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
