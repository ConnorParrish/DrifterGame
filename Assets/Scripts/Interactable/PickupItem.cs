using UnityEngine;
using System.Collections;

public class PickupItem : Interactable {
	public override void Interact(){
		Debug.Log("This should add an item you pickup to your inventory");
	}
}
