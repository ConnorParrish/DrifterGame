using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class PlayerInventory : MonoBehaviour {
	public float money;
	private List<Item> inventory = new List<Item>();
	private JsonData itemDatabase;
	private JsonData inventoryDatabase;

	// Use this for initialization
	void Start () {
		itemDatabase = JsonMapper.ToObject(File.ReadAllText("ItemList.json"));	// This is all the possible items a player could have
		inventoryDatabase = JsonMapper.ToObject(File.ReadAllText("InventoryList.json"));	// This is all the player's current inventory (Used in saving)
		ConstructItemDatabase();	// Builds the inventory list from the database

	}

	void ConstructItemDatabase(){
		for (int i = 0; i < inventoryDatabase.Count; i++){
			inventory.Add(new Item((int)inventoryDatabase[i]["id"], inventoryDatabase[i]["title"].ToString(), 
				inventoryDatabase[i]["description"].ToString(), (bool)inventoryDatabase[i]["stackable"]));
		}
	}

	public void AddItem(int itemID) {
		inventory.Add(new Item((int)itemDatabase[itemID]["id"], itemDatabase[itemID]["title"].ToString(), 
			itemDatabase[itemID]["description"].ToString(), (bool)itemDatabase[itemID]["stackable"]));
		DebugInventory();
	}

	// Used to make sure inventory behaviour is working as intended
	public void DebugInventory(){
		for (int i = 0; i < inventory.Count; i++){
			Debug.Log(inventory[i].Title);
		}
		Debug.Log("===========");
	}

}

public class Item {
	public int ID { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }

	// This is the constructor. You should already know that, though.
	public Item(int id, string title, string description, bool stackable) {
		this.ID = id;
		this.Title = title;
		this.Description = description;
	}
}