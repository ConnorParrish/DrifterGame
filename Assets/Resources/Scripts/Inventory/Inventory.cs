using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Inventory is the object that holds all the information for the player's current inventory.
/// </summary>
public class Inventory : MonoBehaviour {
    
    /// <summary>
    /// The slot prefab.
    /// </summary>
    //[HideInInspector]
    public GameObject inventorySlot;                                            // Prefab of the slot itself
    /// <summary>
    /// The default item prefab.
    /// </summary>
    //[HideInInspector]
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

    public int MaxSlots;

    /// <summary>
    /// All item slots in the inventory slots panel.
    /// </summary>
    public List<GameObject> slots = new List<GameObject>();                     // List of slots in the inventory

    /// <summary>
    /// The panel housing the inventory.
    /// </summary>
    [HideInInspector]
    public GameObject inventoryMenu;
    [HideInInspector]
    public AmountDialog aDialog;
    [HideInInspector]
    public Text moneyText;
    [HideInInspector]
    public float lastSellPrice;

    
    GameObject slotPanel;                                                       // Reference to the panel with the slots
    /// <summary>
    /// The list of all possible items.
    /// </summary>
    public ItemDatabase database;                                                      // This is the list of all items
    ItemPreviewScript ips;
     
    int slotAmount;                                                             // Max number of slots
    int lastChangedAmountDifference = 1;
    
    public bool buyer;

    private void OnValidate()
    {
        if (Application.isPlaying)
            if (transform.parent.parent.name == "General UI Canvas")
                transform.parent.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = Money.ToString("$#0.00");
    }

    public void OnEnable()
    {
        inventoryMenu = transform.parent.GetChild(1).gameObject;
    }

    public virtual void Start()
    {
        inventorySlot = Resources.Load<GameObject>("Prefabs/UI/Inventory/Slot");
        inventoryItem = Resources.Load<GameObject>("Prefabs/UI/Inventory/Item");

		inventoryMenu = transform.parent.GetChild(1).gameObject;
        database = GetComponent<ItemDatabase>();
        slotAmount = MaxSlots;
        if (transform.parent.parent.name == "General UI Canvas")
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
            if (inventorySlot == null) Debug.Log("slot prefab is null dammit");
            slots.Add(Instantiate(inventorySlot));
            if (slots[i] == null) Debug.Log("slots[" + i + "] is null in " + transform.parent.parent.name);
            slots[i].GetComponent<SlotItem>().slotID = i;                           // This tells the slot it's ID in the slot panel
            slots[i].transform.SetParent(slotPanel.transform);                  // Sets the parent of the slot to the slot panel
        }

        if (transform.parent.name == "PlayerInventory")
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
            transform.parent.parent.GetChild(2).gameObject.SetActive(true);
        } else
        {
            inventoryMenu.SetActive(true);
            transform.parent.parent.GetChild(2).gameObject.SetActive(false);
        }
    }
    
    public void ToggleXRay(bool state)
    {
        transform.parent.parent.GetChild(2).GetChild(2).gameObject.SetActive(state);
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

        // play sound to notify player money was changed
        AudioDB.Instance.playOneShot(AudioDB.Instance.moneyJingle);
    }

    /// <summary>
    /// Adds an item with the given ID to the inventory.
    /// </summary>
    /// <param name="id"></param>
    public void AddItem(int id)
    {
        if (Player.Instance.Inventory.ItemDB == null)
        {
            Debug.Log("database is null");
        }
        Item itemToAdd = Player.Instance.Inventory.ItemDB.FetchItemByID(id);
        if (slots[slots.Count - 1].transform.childCount != 0)
        {
            Debug.Log("Your Inventory is full!");
            fullInventory = true;
            return;
        }
        if (itemToAdd.Stackable && ItemInInventoryCheck(itemToAdd) != -1)
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
        //if (slots[slotID].transform.childCount == 0)
        //    return;

		ItemData data = slots[slotID].transform.GetChild(0).GetComponent<ItemData>();
        ips.ChangeActiveItem(data);
        ips.ChangeActiveItem();
        if (ItemInInventoryCheck(data.item) == -1)
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
            /*if (this == Player.Instance.Inventory)
                foreach (Inventory inv in GameObject.Find("NPC Manager").GetComponentsInChildren<Inventory>().Where(i => i.buyer))
                {
                    Debug.Log("inv name" + inv.transform.parent.parent.name);
                    inv.RemoveItem(inv.ItemInInventoryCheck(data.item));
                    Debug.Log(inv.transform.parent.name);
                }
                */
        }
        else
        {
            aDialog.OpenDialog(data);
        }
    }

    public void ChangeItemAmount(int slotID, int amount)
    {
        Debug.Log("olahs");
        ItemData data = slots[slotID].transform.GetChild(0).GetComponent<ItemData>();

        data.amount += amount;
        lastChangedAmountDifference = amount;

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

    /// <summary>
    /// Uses the item.
    /// </summary>
    /// <param name="item">Item.</param>
    public void UseItem(int slotID)
	{
		Item itemToUse = slots[slotID].transform.GetChild(0).GetComponent<ItemData>().item;
        Debug.Log(itemToUse.Type);

        if (itemToUse.Type != "Consumable" && itemToUse.Type != "Drug")
			throw new UnityException ("The item isn't consumable or a drug");

        if (itemToUse.Type == "Consumable" && itemToUse.Slug != "key" && itemToUse.Slug != "keycard")
            Player.Instance.Stats.Hunger += itemToUse.Strength;

        else if (itemToUse.Type == "Drug")
        {
            Player.Instance.Stats.stateBools.isDrugged = true;
            Player.Instance.Stats.drugDuration = itemToUse.Strength;
            Player.Instance.Stats.Hunger += 10f;
            Player.Instance.Stats.Warmth += 10f;
        }

        else if (itemToUse.Slug == "key")
        {
            Player.Instance.GetComponent<PlayerWin>().win();
            ShowInventory();
        }

        else if (itemToUse.Slug == "keycard")
        {
            Player.Instance.gameObject.GetComponent<SleepEnforcer>().sleep(true);
            ShowInventory();
        }

        RemoveItem (slotID);
	}

    public void SellItem(ItemData data, Inventory buyerInv, float price)
    {
        Item itemToBuy = data.item;
        lastSellPrice = price;
        

        if (buyerInv == Player.Instance.Inventory)
        {
            buyerInv.AddItem(itemToBuy.ID);
            RemoveItem(data.slotID);
            AddMoney(-price);
            buyerInv.AddMoney(-price);
        }
        else
        {
            buyerInv.RemoveItem(buyerInv.ItemInInventoryCheck(data.item));

            if (!data.item.Stackable)
            {
                Player.Instance.Inventory.RemoveItem(Player.Instance.Inventory.ItemInInventoryCheck(data.item));
                AddMoney(price);
                buyerInv.AddMoney(price);

            }


        }
    }

    // This is used to make sure the item we are stacking is already in the inventory
    public int ItemInInventoryCheck(Item item) // I MADE THIS PUBLIC TO USE IT IN THE DIALOGUE SCRIPT
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
            {
                return i;
            }
        }
        return -1;
    }
}
