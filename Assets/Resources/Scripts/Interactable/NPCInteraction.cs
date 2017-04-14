using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/**
 * This should be used on any NPCs to start dialogs
 **/

public class NPCInteraction : Interactable
{
    [HideInInspector]
    public fullDialogue fDialog;
    [HideInInspector]
    public simpleDialogue sDialog;
    public NPC NPCData;

    public virtual void Start() {
        fDialog = GetComponent<fullDialogue>();
        sDialog = GetComponent<simpleDialogue>();
        if (fDialog != null)
        {
            fDialog.NPCData = this.NPCData;
        }
        if (sDialog != null)
        {
            sDialog.NPCData = this.NPCData;
        }
    }

    // This is overriden so that the stopping distance is larger in order to
    //  make the player stop near the NPC and not ontop of them.
    public override void MoveToInteraction(NavMeshAgent pAgent)
    {


        base.MoveToInteraction(pAgent);
        pAgent.stoppingDistance = 4f;
    }

    public override void Update()
    {
        if (playerAgent != null && !playerAgent.pathPending && this.pa_destination == new Vector2(playerAgent.destination.x, playerAgent.destination.z))
        {
            float speed = playerAgent.desiredVelocity.magnitude;

            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance * 1f)
            {
                if (!hasInteracted)
                {
                    playerAgent.Stop();

                    Quaternion targetRotation = Quaternion.LookRotation(playerAgent.transform.position - transform.position);
                    float strength = Mathf.Min(.5f * Time.deltaTime, 1f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, strength);
                    Interact();
                    //Stopping(out speed);

                    hasInteracted = true;
                }
            }
            if (playerAgent.remainingDistance > playerAgent.stoppingDistance && hasInteracted)
            {
                hasInteracted = false;
                //playerAgent = null;
                if (fDialog != null)
                {
                    fDialog.endDialogue();
                }
            }
        }
    }

    public override void Interact()
    {
        playerAgent.Stop();
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
            sDialog.showDialogue("negative");           
        }
    }
}
