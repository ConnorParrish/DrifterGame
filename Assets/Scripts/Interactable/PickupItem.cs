using UnityEngine;
using System.Collections;

public class PickupItem : Interactable {
    public Inventory inv;
    
    public void Start()
    {
        inv = GameObject.Find("Inventory Manager").GetComponent<Inventory>();
        FloatingTextController.Initialize();
    }

	public override void Interact()
    {
		Debug.Log("This should add an item you pickup to your inventory");
        FloatingTextController.CreateFloatingText("+1 " + this.gameObject.name, transform);
        inv.AddItem(3);
	}
}
