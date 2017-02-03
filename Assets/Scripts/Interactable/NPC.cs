using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/**
 * This should be used on any NPCs to start dialogs
 **/

public class NPC : Interactable
{
    // This is overriden so that the stopping distance is larger in order to
    //  make the player stop near the NPC and not ontop of them.
    public override void MoveToInteraction(NavMeshAgent playerAgent)
    {
        this.playerAgent = playerAgent;
        playerAgent.stoppingDistance = 2.5f;
        playerAgent.destination = this.transform.position;
    }

    public override void Interact()
    {
		Debug.Log("Interacting with base NPC.");
	}
}
