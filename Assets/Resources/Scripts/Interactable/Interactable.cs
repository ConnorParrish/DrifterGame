using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/** 
 * The other scripts in this directory inherit this class.
 * This is used to store the NavMeshAgent for each of the smaller items
 **/

public class Interactable : MonoBehaviour {
	public NavMeshAgent playerAgent = new NavMeshAgent();
    public Vector2 pa_destination;
    public bool hasInteracted;

    
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


	public virtual void Interact(){
        Debug.Log("Interacting with base Interactable");
    }
}
