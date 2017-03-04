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
    public float Money;                                                         // NEEEEED TO IMPLEMENNTTTT (you did (: )
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
    /// The overall menu housing the different panels.
    /// </summary>
    GameObject inventoryMenu;
    /// <summary>
    /// The panel housing the inventory.
    /// </summary>
    GameObject inventoryPanel;                                                  // Main UI Panel
    /// <summary>
    /// The panel housing the items in inventory slots.
    /// </summary>
    GameObject slotPanel;                                                       // Reference to the panel with the slots
    /// <summary>
    /// The list of all possible items.
    /// </summary>
    ItemDatabase database;                                                      // This is the list of all items
    DeletionDialog dDialog;
     
    Text moneyText;
    int slotAmount;                                                             // Max number of slots
    
    void Start()
    {
        database = GetComponent<ItemDatabase>();
        slotAmount = 16;
        inventoryPanel = GameObject.Find("Inventory Panel");
        inventoryMenu = GameObject.Find("Menu");
        moneyText = GameObject.Find("Money Text").transform.GetChild(0).GetComponent<Text>();
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        dDialog = transform.parent.GetChild(3).GetChild(6).GetComponent<DeletionDialog>();

        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<SlotItem>().slotID = i;                           // This tells the slot it's ID in the slot panel
            slots[i].transform.SetParent(slotPanel.transform);                  // Sets the parent of the slot to the slot panel
        }

        AddItem(0);
        AddItem(1);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);

        inventoryMenu.SetActive(false);
    }

    /// <summary>
    /// Enables/Disables the inventory menu.
    /// </summary>
    public void ShowInventory()
    {
        if (inventoryPanel.transform.parent.gameObject.activeSelf)
        {
            inventoryPanel.transform.parent.gameObject.SetActive(false);
        } else
        {
            inventoryPanel.transform.parent.gameObject.SetActive(true);
        }
    }
    
    /// <summary>
    /// Adds money to the player's inventory.
    /// </summary>
    /// <param name="change"></param>
    public void AddMoney(float change)
    {
        Money += change;

        inventoryMenu.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>().text = Money.ToString("#.00");
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
    public void RemoveItem(int id)
    {
        Item itemToRemove = database.FetchItemByID(id);
        if (ItemInInventoryCheck(itemToRemove) == false)
        {
            Debug.Log("You don't have any " + itemToRemove.Title + " in your inventory!");
            return;
        }

        if (!itemToRemove.Stackable)
        {
            Debug.Log("Not stackable");
            for (int i = items.Count - 1; i > -1; i--)
            {
                if (items[i].ID == id)
                {
                    if (fullInventory)
                    {
                        fullInventory = false;
                    }
                    slots[i].name = "Slot(Clone)";
                    items[i] = new Item();
                    Destroy(GameObject.Find(itemToRemove.Title));
                    break;

                }
            }
        }
        else
        {
            if (GameObject.Find(itemToRemove.Title).GetComponent<ItemData>().amount == 1)
            {
                Debug.Log("Not stackable");
                for (int i = items.Count - 1; i > -1; i--)
                {
                    if (items[i].ID == id)
                    {
                        if (fullInventory)
                        {
                            fullInventory = false;
                        }
                        slots[i].name = "Slot(Clone)";
                        items[i] = new Item();
                        Destroy(GameObject.Find(itemToRemove.Title));
                        break;

                    }
                }
            }
            else 
                dDialog.OpenDialog(GameObject.Find(itemToRemove.Title).GetComponent<ItemData>());
        }
    }

    public void RemoveItem(int id, int amountToDelete)
    {
        Item itemToRemove = database.FetchItemByID(id);

        if (itemToRemove.Stackable)
        {
            Debug.Log("Stackable...");

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = GameObject.Find(itemToRemove.Title).GetComponent<ItemData>();
                    if (data.amount > amountToDelete)
                    {
                        //dDialog.OpenDialog(data);
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
                        slots[i].name = "Slot(Clone)";                          // Returns the slot to it's default name
                        items[i] = new Item();                                  // We're creating a new blank item that is in every 
                        Destroy(data.gameObject);
                        break;
                    }
                }
            }
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
