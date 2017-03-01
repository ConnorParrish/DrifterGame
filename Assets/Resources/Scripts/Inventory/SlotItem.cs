using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

/**
 * This script handles drag-n-drop in the inventory 
 **/

public class SlotItem : MonoBehaviour, IDropHandler, IPointerClickHandler {
    public int slotID;                                                              // Used to keep track of it's location on the board
    private Inventory inv;                                                      // Cache of the useful Inventory (with items and slots)
    //private ItemPreviewScript ips;
    private DeletionDialog dDialog;
    void Start()
    {
        inv = GameObject.Find("Inventory Manager").GetComponent<Inventory>();
        //    ips = GameObject.Find("ItemPreview Panel").GetComponent<ItemPreviewScript>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();  // Get's the item the mouse is dragging, then gets the component ItemData
        Debug.Log(inv.items[slotID].ID);

        if (gameObject.name == "Trash Slot" || gameObject.name == "Image")
        {
            Debug.Log("Deleting item: " + droppedItem.item.Title);
            inv.RemoveItem(droppedItem.item.ID);
            return;
        }

        if (inv.items[slotID].ID == -1)                                             // If the slot is empty
        {
            inv.slots[droppedItem.slotID].gameObject.name = "Slot(Clone)";
            inv.items[droppedItem.slotID] = new Item();                         // Sets the old slot to be an empty item (so we can return back to it)
            inv.slots[slotID].gameObject.name = "Slot #" + slotID + " - " + droppedItem.item.Title;
            inv.items[slotID] = droppedItem.item;                                   // The new slot's item will be the dropped item
            droppedItem.slotID = slotID;                                            // Updates the item's slot id
        } 
        else if (droppedItem.slotID != slotID)
        {
            Transform itemTransform = this.transform.GetChild(0);
            itemTransform.GetComponent<ItemData>().slotID = droppedItem.slotID; // Might want to use a setter that sets the item then moves the other to the correct slot
            itemTransform.SetParent(inv.slots[droppedItem.slotID].transform);
            itemTransform.position = inv.slots[droppedItem.slotID].transform.position;

            inv.slots[droppedItem.slotID].gameObject.name = "Slot #" + droppedItem.slotID + " - " + itemTransform.GetComponent<ItemData>().item.Title;
            inv.items[droppedItem.slotID] = itemTransform.GetComponent<ItemData>().item;

            droppedItem.slotID = slotID;
            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;

            inv.slots[slotID].gameObject.name = "Slot #" + slotID + " - " + droppedItem.item.Title;
            inv.items[slotID] = droppedItem.item;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemData clickedItem = eventData.pointerPress.GetComponent<ItemData>();
        Debug.Log("Hi");
        if (gameObject.name != "Trash Slot" && gameObject.name != "Image")
        {
            //ips.ChangeActiveItem(clickedItem.item.ID);
        }
    }
}
