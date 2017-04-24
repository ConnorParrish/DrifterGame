using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/**
 * This script is used to pickup an item
 **/

public class PickupItem : Interactable {
    public ItemDatabase itemDB;                                                     // Needed to grab the item's information
    public int itemID;                                                              // itemDB needs this to grab its name

    public void Start()
    {
        itemDB = Player.Instance.Inventory.GetComponent<ItemDatabase>();
        FloatingTextController.Initialize();
    }

    public override void MoveToInteraction(NavMeshAgent pAgent)
    {
        pAgent.stoppingDistance = 1f;
        base.MoveToInteraction(pAgent);
    }

    public override void Interact()
    {

		string itemName = Player.Instance.Inventory.ItemDB.FetchItemByID(itemID).Title;
		FloatingTextController.Initialize ();
        FloatingTextController.CreateFloatingText("+1 " + itemName, transform);
        Player.Instance.Inventory.AddItem(itemID);
        Destroy(gameObject);
        base.Interact();
    }
}
