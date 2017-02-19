using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/** 
 * The other scripts in this directory inherit this class.
 * This is used to store the NavMeshAgent for each of the smaller items
 **/

public class Interactable : MonoBehaviour {
	public UnityEngine.AI.NavMeshAgent playerAgent = new NavMeshAgent();
    public bool hasInteracted;

	public virtual void MoveToInteraction(UnityEngine.AI.NavMeshAgent pAgent){
        this.playerAgent = pAgent;
        
        playerAgent.destination = this.transform.position;
        
	}

    public virtual void Update()
    {
        if (playerAgent != null&& !playerAgent.pathPending)
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
                    //playerAgent.transform.position = transform.position;

                    playerAgent = null;
                    Interact();
                    //Stopping(out speed);
                    hasInteracted = true;

                }
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
