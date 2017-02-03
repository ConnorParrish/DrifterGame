using UnityEngine;
using System.Collections;

/**
 * This script is used to pickup an item
 **/

public class PickupItem : Interactable {
    public Inventory inv;
    public ItemDatabase itemDB;
    public int itemID;    
    public void Start()
    {
        inv = GameObject.Find("Inventory Manager").GetComponent<Inventory>();
        itemDB = inv.GetComponent<ItemDatabase>();
        FloatingTextController.Initialize();
    }

	public override void Interact()
    {
        string itemName = itemDB.FetchItemByID(itemID).Title;
        Debug.Log("Adding " + itemName);
        FloatingTextController.CreateFloatingText("+1 " + itemName, transform);
        inv.AddItem(itemID);
        Destroy(gameObject);
	}
}
