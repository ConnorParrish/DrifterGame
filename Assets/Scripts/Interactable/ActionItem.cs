using UnityEngine;
using System.Collections;

/**
 * This script should be used to trigger something in the scene (ie. Start panhandling)
 **/

public class ActionItem : Interactable {
	public override void Interact(){
		Debug.Log("Interacting with base ActionItem");
	}
}
