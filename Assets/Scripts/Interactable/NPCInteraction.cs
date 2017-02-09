using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/**
 * This should be used on any NPCs to start dialogs
 **/

public class NPCInteraction : Interactable
{
	fullDialogue fDialog;
	bool hasInteracted;

	void Start() {
		fDialog = GetComponent<fullDialogue> ();
	}
	public override void Update(){
		{
			if (playerAgent != null&& !playerAgent.pathPending)
			{
				if (playerAgent.remainingDistance < playerAgent.stoppingDistance)
				{
					if (!hasInteracted) {
						Interact ();
					}
				}
			}
		}
	
	}

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
		hasInteracted = true;
		if (!fDialog.canvas.activeSelf) {
			fDialog.showDialogue ();
		} else {
			fDialog.endDialogue ();
		}
	}
}
