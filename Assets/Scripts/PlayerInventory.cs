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
		itemDatabase = JsonMapper.ToObject(File.ReadAllText("ItemList.json"));
		inventoryDatabase = JsonMapper.ToObject(File.ReadAllText("InventoryList.json"));
		ConstructItemDatabase();

	}

	void ConstructItemDatabase(){
		for (int i = 0; i < inventoryDatabase.Count; i++){
			inventory.Add(new Item((int)inventoryDatabase[i]["id"], inventoryDatabase[i]["title"].ToString(), inventoryDatabase[i]["description"].ToString()));
		}
	}

	public void AddItem(int itemID) {
		//Item newItem = new Item(id, title, description);
		inventory.Add(new Item((int)itemDatabase[itemID]["id"], itemDatabase[itemID]["title"].ToString(), itemDatabase[itemID]["description"].ToString()));
		DebugInventory();
	}

	public void DebugInventory(){
		for (int i = 0; i < inventory.Count; i++){
			Debug.Log(inventory[i].Title);
		}
		Debug.Log("===========");
	}

}

public class Item {
	// Update is called once per frame
	public int ID { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }

	public Item(int id, string title, string description) {
		this.ID = id;
		this.Title = title;
		this.Description = description;
	}
}