using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

/**
 * This script handles drag-n-drop in the inventory 
 **/

public class SlotItem : MonoBehaviour, IDropHandler {
    public int slotID;                                                              // Used to keep track of it's location on the board
    private Inventory currentInv;                                                      // Cache of the useful currentInventory (with items and slots)
    void Start()
    {
        if (gameObject.name == "Trash Slot")
            currentInv = transform.parent.parent.GetChild(0).GetComponent<Inventory>();
        else
            currentInv = transform.parent.parent.parent.parent.GetChild(0).GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();  // Get's the item the mouse is dragging, then gets the component ItemData
        //Debug.Log(inv.items[slotID].ID);

        if (gameObject.name == "Trash Slot" || gameObject.name == "Image")
        {
            Debug.Log("Deleting item: " + droppedItem.item.Title);

			//inv.slots [droppedItem.slotID].gameObject.name = droppedItem.item.Title;
			currentInv.items [droppedItem.slotID] = droppedItem.item;
			droppedItem.slotID = -1;
			droppedItem.OED();
			Debug.Log ("droppedItem.slotID: " + droppedItem.slotID);
			currentInv.RemoveItem(droppedItem.slotID);
            return;
        }

		else if (currentInv.items [slotID].ID == -1) {                                             // If the slot is empty
			Debug.Log("Slot is empty! -> Moving");
			currentInv.slots [droppedItem.slotID].gameObject.name = "Slot(Clone)";
			currentInv.items [droppedItem.slotID] = new Item ();                         // Sets the old slot to be an empty item (so we can return back to it)
			currentInv.slots [slotID].gameObject.name = "Slot #" + slotID + " - " + droppedItem.item.Title;
			currentInv.items [slotID] = droppedItem.item;                                   // The new slot's item will be the dropped item
			droppedItem.slotID = slotID;                                            // Updates the item's slot id
		} else if (droppedItem.slotID != slotID)
        {	
			Debug.Log ("slotID's do not match -> swapping");
            Transform itemTransform = this.transform.GetChild(0);
            itemTransform.GetComponent<ItemData>().slotID = droppedItem.slotID; // Might want to use a setter that sets the item then moves the other to the correct slot
            itemTransform.SetParent(currentInv.slots[droppedItem.slotID].transform);
            itemTransform.position = currentInv.slots[droppedItem.slotID].transform.position;

            currentInv.slots[droppedItem.slotID].gameObject.name = "Slot #" + droppedItem.slotID + " - " + itemTransform.GetComponent<ItemData>().item.Title;
            currentInv.items[droppedItem.slotID] = itemTransform.GetComponent<ItemData>().item;

            droppedItem.slotID = slotID;
            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;

            currentInv.slots[slotID].gameObject.name = "Slot #" + slotID + " - " + droppedItem.item.Title;
            currentInv.items[slotID] = droppedItem.item;
        }
    }
}
