using UnityEngine;
using System.Collections;

public class BeerBottle : PickupItem {
	public override void Interact(){
		//Debug.Log("Adding a Beer bottle to your inventory");
		base.Interact();
	}
}
