using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/**
 * This script is used to pickup an item
 **/

public class PickupItem : Interactable {
    public Inventory inv;                                                           // Used to add items to the inventory
    public ItemDatabase itemDB;                                                     // Needed to grab the item's information
    public int itemID;                                                              // itemDB needs this to grab its name

    public void Start()
    {
        inv = GameObject.Find("Inventory Manager").GetComponent<Inventory>();
        itemDB = inv.GetComponent<ItemDatabase>();
        FloatingTextController.Initialize();
    }

    public override void MoveToInteraction(NavMeshAgent pAgent)
    {
        pAgent.stoppingDistance = 1f;
        base.MoveToInteraction(pAgent);
    }

    public override void Interact()
    {

        string itemName = itemDB.FetchItemByID(itemID).Title;
        FloatingTextController.CreateFloatingText("+1 " + itemName, transform);
        inv.AddItem(itemID);
        Destroy(gameObject);
        base.Interact();
    }
}
