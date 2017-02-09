using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/**
 * This should be used on any NPCs to start dialogs
 **/

public class NPCInteraction : Interactable
{
	public fullDialogue fDialog;
    public simpleDialogue sDialog;
    public NPC NPCData;
	bool hasInteracted;

	public virtual void Start() {
		fDialog = GetComponent<fullDialogue> ();
        sDialog = GetComponent<simpleDialogue>();
        
        if (fDialog != null)
        {
            fDialog.NPCData = this.NPCData;
        } else if (sDialog != null)
        {
            sDialog.NPCData = this.NPCData;
        }
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
        if (fDialog != null)
        {
            if (!fDialog.canvas.activeSelf)
            {
                fDialog.showDialogue();
            }
            else
            {
                fDialog.endDialogue();
            }
        } else if (sDialog != null)
        {
            sDialog.showDialogue();
            // if the canvas is displaying, force its rotation to face the main camera
            if (sDialog.canvas.activeSelf)
                sDialog.canvas.transform.rotation = Camera.main.transform.rotation;
            
        }
    }
}
