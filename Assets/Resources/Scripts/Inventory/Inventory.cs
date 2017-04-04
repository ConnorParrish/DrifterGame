using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Inventory is the object that holds all the information for the player's current inventory.
/// </summary>
public class Inventory : MonoBehaviour {
    
    /// <summary>
    /// The slot prefab.
    /// </summary>
    public GameObject inventorySlot;                                            // Prefab of the slot itself
    /// <summary>
    /// The default item prefab.
    /// </summary>
    public GameObject inventoryItem;                                            // Prefab that is the item

    /// <summary>
    /// Player's current amount of money.
    /// </summary>
    public float Money;
    /// <summary>
    /// Whether or not the inventory is full.
    /// </summary>
    public bool fullInventory;                                                  // Keeps track of whether the inventory is full already
    /// <summary>
    /// All of the player's current items.
    /// </summary>
    public List<Item> items = new List<Item>();                                 // List of items in the inventory
    /// <summary>
    /// All item slots in the inventory slots panel.
    /// </summary>
    public List<GameObject> slots = new List<GameObject>();                     // List of slots in the inventory

    /// <summary>
    /// The panel housing the inventory.
    /// </summary>
    public GameObject inventoryMenu;
    
    public int MaxSlots;


    GameObject slotPanel;                                                       // Reference to the panel with the slots
    /// <summary>
    /// The list of all possible items.
    /// </summary>
    ItemDatabase database;                                                      // This is the list of all items
    AmountDialog aDialog;
    ItemPreviewScript ips;
     
    public Text moneyText;
    int slotAmount;                                                             // Max number of slots

    private void OnValidate()
    {
        if (Application.isPlaying)
            if (transform.parent.name == "General UI Canvas")
                transform.parent.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = Money.ToString("$#0.00");
    }

    public virtual void Start()
    {
        inventoryMenu = transform.parent.GetChild(1).gameObject;
        database = GetComponent<ItemDatabase>();
        slotAmount = MaxSlots;
        if (transform.parent.name == "General UI Canvas")
        {
            moneyText = inventoryMenu.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
            slotPanel = inventoryMenu.transform.GetChild(1).GetChild(0).gameObject;

        }
        else
        {
            slotPanel = inventoryMenu.transform.GetChild(0).GetChild(0).gameObject;
            moneyText = inventoryMenu.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
        }
        aDialog = slotPanel.transform.parent.GetChild(1).GetComponent<AmountDialog>();
        ips = inventoryMenu.transform.GetChild(inventoryMenu.transform.childCount - 2).GetComponent<ItemPreviewScript>();

        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<SlotItem>().slotID = i;                           // This tells the slot it's ID in the slot panel
            slots[i].transform.SetParent(slotPanel.transform);                  // Sets the parent of the slot to the slot panel
        }

        if (transform.parent.name == "General UI Canvas")
            inventoryMenu.SetActive(false);
    }

    /// <summary>
    /// Enables/Disables the inventory menu.
    /// </summary>
    public void ShowInventory()
    {
        if (inventoryMenu.activeSelf)
        {
            inventoryMenu.SetActive(false);
        } else
        {
            inventoryMenu.SetActive(true);
        }
    }
    
    /// <summary>
    /// Adds money to the player's inventory. Can't go below zero
    /// </summary>
    /// <param name="change"></param>
    public void AddMoney(float change)
    {
        Money += change;
        if (Money < 0)
            Money = 0f;
        moneyText.text = Money.ToString("$#0.00");
    }

    /// <summary>
    /// Adds an item with the given ID to the inventory.
    /// </summary>
    /// <param name="id"></param>
    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);
        if (slots[slots.Count - 1].transform.childCount != 0)
        {
            Debug.Log("Your Inventory is full!");
            fullInventory = true;
            return;
        }
        if (itemToAdd.Stackable && ItemInInventoryCheck(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;                                              // This will add the new item via an amount
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;                                                      // Stops it from creating a bunch of lesser stacks
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1 && !fullInventory)
                {
                    slots[i].name = "Slot #" + i + " - " + itemToAdd.Title;     // Labels the slot for easier reading
                    items[i] = itemToAdd;                                       // Adds the item
                    GameObject itemObj = Instantiate(inventoryItem);            // Creates a new GameObject for the item
                    itemObj.GetComponent<ItemData>().item = itemToAdd;          // Sets the item data structure for the ItemDatabase
                    itemObj.GetComponent<ItemData>().amount = 1;                // Sets the amount to 1 because it's creating the first
                    itemObj.GetComponent<ItemData>().slotID = i;                // Sets the slot number its originally in
                    itemObj.transform.SetParent(slots[i].transform);            // Sets the parent of the object to it's correct slot
                    itemObj.transform.position = slots[i].transform.position;   // Lines up the item in the middle of the slot
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;    // Sets the item's sprite
                    itemObj.name = itemToAdd.Title;                             // Labels the item for easier reading
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Removes an item with a given ID from the inventory.
    /// </summary>
    /// <param name="id"></param>
    public void RemoveItem(int slotID)
    {
		ItemData data = slots[slotID].transform.GetChild(0).GetComponent<ItemData>();
        ips.ChangeActiveItem();
        if (ItemInInventoryCheck(data.item) == false)
        {
            Debug.Log("You don't have any " + data.item.Title + " in your inventory!");
            return;
        }

		
        if (!data.item.Stackable || data.amount == 1)
        {
            if (fullInventory)
            {
                fullInventory = false;
            }
			Destroy (slots [slotID].transform.GetChild (0).gameObject);
			slots[slotID].name = "Slot(Clone)";
                    
			items[slotID] = new Item();            
        }
        else
        {
            aDialog.OpenDialog(data);
        }
    }

    public void ChangeItemAmount(int slotID, int amount)
    {
        ItemData data = slots[slotID].transform.GetChild(0).GetComponent<ItemData>();

        data.amount += amount;

        data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();

        if (data.amount == 0)
        {
            data.amount = 1;
            RemoveItem(data.slotID);
        }
    }

    public void RemoveItem(int slotID, int amountToDelete)
    {
        ItemData data = slots[slotID].transform.GetChild(0).GetComponent<ItemData>();

        if (data.item.Stackable)
        {
            if (data.amount > amountToDelete)
            {
                //aDialog.OpenDialog(data);
                Debug.Log(amountToDelete + " can be deleted.");

                data.amount -= amountToDelete;                                          // If there's more than one of the stacked item, we lower it
                data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
            }
            else
            {
                if (fullInventory)
                {
                    fullInventory = false;
                }
                slots[slotID].name = "Slot(Clone)";                          // Returns the slot to it's default name
                items[slotID] = new Item();                                  // We're creating a new blank item that is in every 
                Destroy(data.gameObject);
            }
        }
        else
            throw new UnityException("Item needs to be stackable");

    }

    /// <summary>
    /// Uses the item.
    /// </summary>
    /// <param name="item">Item.</param>
    public void UseItem(int slotID)
	{
		Item itemToUse = slots[slotID].transform.GetChild(0).GetComponent<ItemData>().item;

		if (itemToUse.Type != "Consumable")
			throw new UnityException ("The item isn't consumable");

		Player.Instance.Stats.Hunger += 20;
		RemoveItem (slotID);
	}

    public void SellItem(int slotID, Inventory buyerInv, float price)
    {
        Item itemToBuy = slots[slotID].transform.GetChild(0).GetComponent<ItemData>().item;

        AddMoney(price);
        buyerInv.AddMoney(-price);


        if (buyerInv == Player.Instance.Inventory)
            buyerInv.AddItem(itemToBuy.ID);
        else
        {
            buyerInv.RemoveItem(slotID);
            //RemoveItem(slotID);
            //Player.Instance.Inventory.ChangeItemAmount(slotID);
        }
    }

    // This is used to make sure the item we are stacking is already in the inventory
    public bool ItemInInventoryCheck(Item item) // I MADE THIS PUBLIC TO USE IT IN THE DIALOGUE SCRIPT
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
            {
                return true;
            }
        }
        return false;
    }
}
