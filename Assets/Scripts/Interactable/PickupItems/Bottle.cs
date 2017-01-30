using UnityEngine;
using System.Collections;

public class Bottle : PickupItem {
	public override void Interact(){
		Debug.Log("Adding a bottle to your inventory");
		base.Interact();
	}
}
