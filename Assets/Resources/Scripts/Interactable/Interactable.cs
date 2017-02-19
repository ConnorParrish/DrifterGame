using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/** 
 * The other scripts in this directory inherit this class.
 * This is used to store the NavMeshAgent for each of the smaller items
 **/

public class Interactable : MonoBehaviour {
	public NavMeshAgent playerAgent = new NavMeshAgent();
    public Vector3 pa_destination;
    public bool hasInteracted;

    
	public virtual void MoveToInteraction(UnityEngine.AI.NavMeshAgent pAgent){
        this.playerAgent = pAgent;
        playerAgent.destination = this.transform.position;
        this.pa_destination = playerAgent.destination;

    }

    public virtual void Update()
    {
        if (playerAgent != null && !playerAgent.pathPending && pa_destination == playerAgent.destination)
        {
            float speed = playerAgent.desiredVelocity.magnitude;


            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance *1f)
            {
                if (!hasInteracted)
                {
                    //playerAgent.transform.position = transform.position;

                    Interact();
                    //Stopping(out speed);
                    hasInteracted = true;

                }
            }
            if (playerAgent.remainingDistance > playerAgent.stoppingDistance && hasInteracted)
            {
                hasInteracted = false;
            }
        }
    }

    void Stopping(out float speed)
    {
        playerAgent.Stop();
        playerAgent.transform.position = transform.position;
        speed = 0f;

    }

	public virtual void Interact(){
        Debug.Log("Interacting with base Interactable");
    }
}
