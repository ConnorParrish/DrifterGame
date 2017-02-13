using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/**
 * This should be used on any NPCs to start dialogs
 **/

public class NPCInteraction : Interactable
{
	public fullDialogue fDialog;
    public simpleDialogue sDialog;
    public NPC NPCData;

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


    // This is overriden so that the stopping distance is larger in order to
    //  make the player stop near the NPC and not ontop of them.
    public override void MoveToInteraction(NavMeshAgent pAgent)
    {
        pAgent.stoppingDistance = 4f;

        base.MoveToInteraction(pAgent);
    }

    public override void Update()
    {
        if (playerAgent != null && !playerAgent.pathPending)
        {
            float speed = playerAgent.desiredVelocity.magnitude;

            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance * 1f)
            {
                if (!hasInteracted)
                {
                    //playerAgent.transform.position = transform.position;

                    //playerAgent = null;
                    Interact();
                    //Stopping(out speed);
                    hasInteracted = true;

                }
            }
            if (playerAgent.remainingDistance > playerAgent.stoppingDistance && hasInteracted)
            {
                //hasInteracted = false;
                if (fDialog != null)
                {
                    fDialog.endDialogue();
                }
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
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
