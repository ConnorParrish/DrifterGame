using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/** 
 * The other scripts in this directory inherit this class.
 * This is used to store the NavMeshAgent for each of the smaller items
 **/

/// <summary>
/// This parent class is used to track interactions between the player and the environment
/// </summary>
public class Interactable : MonoBehaviour {
    /// <summary>
    /// This is the NavMeshAgent of the player clicking on the interactable object.
    /// </summary>
	public NavMeshAgent playerAgent = new NavMeshAgent();
    public Vector2 pa_destination;
    /// <summary>
    /// Returns whether or not the object has been interacted.
    /// </summary>
    public bool hasInteracted;

    /// <summary>
    /// Should be called when the player clicks an interactable object.
    /// </summary>
    /// <param name="pAgent"></param>
	public virtual void MoveToInteraction(UnityEngine.AI.NavMeshAgent pAgent){
        pAgent.destination = this.transform.position;
		this.playerAgent = pAgent;
		this.pa_destination = new Vector2(transform.position.x, transform.position.z);

    }

    public virtual void Update()
    {
		if (playerAgent != null && !playerAgent.pathPending && this.pa_destination == new Vector2(transform.position.x, transform.position.z))
        {
            float speed = playerAgent.desiredVelocity.magnitude;

            if (playerAgent.remainingDistance > playerAgent.stoppingDistance && hasInteracted)
            {
                hasInteracted = false;
            }

            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance *1f)
            {
                if (!hasInteracted)
                {
                    this.Interact();
                    hasInteracted = true;

                }
            }
        }
    }

    /// <summary>
    ///  This will be called when the player is within range to interact with an Interactable
    ///  object.
    /// </summary>
	public virtual void Interact(){
        Debug.Log("Interacting with base Interactable");
    }
}
