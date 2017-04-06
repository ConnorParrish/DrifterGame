using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/**
 * This script should be used to trigger something in the scene (ie. Start panhandling)
 **/

public class ActionItem : Interactable {

    public override void MoveToInteraction(NavMeshAgent pAgent)
    {
        pAgent.stoppingDistance = 0f;
        base.MoveToInteraction(pAgent);
    }

    public override void Interact(){
        base.Interact();
	}
}
