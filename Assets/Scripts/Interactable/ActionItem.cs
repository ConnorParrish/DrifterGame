using UnityEngine;
using System.Collections;

public class ActionItem : Interactable {
	public virtual void Interact(){
		Debug.Log("Interacting with base ActionItem");
	}
}
