using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

/**
 * This script keeps track of interaction events between the user and the scene.
 * This also keeps track of:
 *          -- The actual item itself
 *          -- The number of the item's in it's stack
 *          -- The current slot the item is on
 **/

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler {
    public Item item;
    public int amount;                                                          // Used to track stackable item amounts
	public int slotID;                                                          // The current slot (Which slot the item is in)
	private int prevSlotID;

    private Inventory inv;                                                      // The inventory object (with information on items/slots)
    private Tooltip tooltip;
    private Vector2 offset;                                                     // Distance between mouse and middle of sprite
    private ItemPreviewScript ips;
    private Sprite nonFocusedSprite;
    private Sprite focusedSprite;

    void Start()
    {
        inv = transform.parent.parent.parent.parent.parent.GetChild(0).GetComponent<Inventory>();
        tooltip = GetComponent<Tooltip>();
        ips = transform.parent.parent.parent.parent.GetChild(transform.parent.parent.parent.parent.childCount - 2).GetComponent<ItemPreviewScript>();

        nonFocusedSprite = Resources.Load<Sprite>("Sprites/UI/Item Slot Graphic");
        focusedSprite = Resources.Load<Sprite>("Sprites/UI/Item Slot Graphic selected");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
        this.transform.position = eventData.position - offset;                  // Sets the sprite's position to look like it stays where the mouse picks it up
        //Debug.Log(ips.name);

        foreach (Image img in transform.parent.parent.GetComponentsInChildren<Image>())
            if (img.gameObject.GetComponent<SlotItem>())
                if (img.sprite.name != "Item Slot Graphic")
                    img.sprite = nonFocusedSprite;

        ips.ChangeActiveItem(this);

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.SetParent(this.transform.parent.parent);             // Brings the sprite outside of it's slot so it will render above the inventory slots
			prevSlotID = slotID;
            GetComponent<CanvasGroup>().blocksRaycasts = false;                 // Lets the drop recognize the slot underneath the sprite
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position - offset;              // Keeps the item's position relative to the mouse
        }
    }
	public void OED(){
		if (slotID == -1) 
		{
			ips.ChangeActiveItem();
			this.transform.SetParent (inv.slots [prevSlotID].transform);
			this.transform.position = inv.slots[prevSlotID].transform.position;
			slotID = prevSlotID;
            prevSlotID = -1;

		}

	}

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.SetParent(inv.slots[slotID].transform);                // Brings the item down into the new slot
            this.transform.position = inv.slots[slotID].transform.position;       // Sets the item in the new parent's slot
            GetComponent<CanvasGroup>().blocksRaycasts = true;                  // Lets the player pick it back up

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        //GameObject.Find("ItemPreview Panel").GetComponent<ItemPreviewScript>().ChangeActiveItem(item.ID);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //GameObject.Find("ItemPreview Panel").GetComponent<ItemPreviewScript>().ChangeActiveItem(item.ID);
        Debug.Log("entering");
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
}
