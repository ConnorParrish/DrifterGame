using UnityEngine;
using System.Collections;

/**
 * This should be used on any NPCs to start dialogs
 **/

public class NPC : Interactable {
	public override void Interact(){
		Debug.Log("Interacting with NPC.");
	}
}
