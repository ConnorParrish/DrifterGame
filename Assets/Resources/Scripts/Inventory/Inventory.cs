using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/**
 * This is the script that needs to be referenced in order to add/subtract items from the player's inventory
 **/

public class Inventory : MonoBehaviour {
    GameObject inventoryMenu;
    GameObject inventoryPanel;                                                  // Main UI Panel
    GameObject slotPanel;                                                       // Reference to the panel with the slots
    ItemDatabase database;                                                      // This is the list of all items
    public GameObject inventorySlot;                                            // Prefab of the slot itself
    public GameObject inventoryItem;                                            // Prefab that is the item
    public float Money;                                                         // NEEEEED TO IMPLEMENNTTTT (you did (: )
    Text moneyText;
    public bool fullInventory;                                                  // Keeps track of whether the inventory is full already

    int slotAmount;                                                             // Max number of slots
    public List<Item> items = new List<Item>();                                 // List of items in the inventory
    public List<GameObject> slots = new List<GameObject>();                     // List of slots in the inventory
    
    void Start()
    {
        database = GetComponent<ItemDatabase>();
        slotAmount = 16;
        inventoryPanel = GameObject.Find("Inventory Panel");
        inventoryMenu = GameObject.Find("Menu");
        moneyText = GameObject.Find("Money Text").transform.GetChild(0).GetComponent<Text>();
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;

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

    public void ShowInventory()
    {
        if (inventoryPanel.transform.parent.gameObject.activeSelf)
        {
            Debug.Log("Hiding");
            inventoryPanel.transform.parent.gameObject.SetActive(false);
        } else
        {
            Debug.Log("Showing");
            inventoryPanel.transform.parent.gameObject.SetActive(true);
        }
    }

    public void HideInventory()
    {
        inventoryItem.transform.parent.gameObject.SetActive(true);
    }

    public void UpdateMoney()
    {
        moneyText.text = Money.ToString();
    }

    public void AddMoney(float change)
    {
        Money += change;
        inventoryMenu.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = Money.ToString("#.00");
    }

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

    public void RemoveItem(int id)
    {
        Item itemToRemove = database.FetchItemByID(id);
        if (ItemInInventoryCheck(itemToRemove) == false)
        {
            Debug.Log("You don't have any " + itemToRemove.Title + " in your inventory!");
        }
        if (itemToRemove.Stackable && ItemInInventoryCheck(itemToRemove))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    if (data.amount > 1)
                    {
                        data.amount--;                                          // If there's more than one of the stacked item, we lower it
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
                        Destroy(slots[i].transform.GetChild(0).gameObject);
                        break;
                    }
                }
            }
        }
        else
        {
            for (int i = items.Count - 1; i > 0; i--)
            {
                if (items[i].ID == id)
                {
                    if (fullInventory)
                    {
                        fullInventory = false;
                    }
                    slots[i].name = "Slot(Clone)";
                    items[i] = new Item();
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    break;
                }
            }
        }
    }
    
    // This is used to make sure the item we are stacking is already in the inventory
    bool ItemInInventoryCheck(Item item)
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
