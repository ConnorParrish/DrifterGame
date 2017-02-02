using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

/**
 * This script builds the Item Database found in ItemList.json
 * The script creates an Item object that has all the information the individual elements of the JSON has.
 **/

public class ItemDatabase : MonoBehaviour {
	private JsonData itemDatabase;                                              // A JSON of all the possible items a player could have
    private List<Item> database = new List<Item>();                             // The database we create based off of itemDatabase

    // Use this for initialization
    void Start () {
		itemDatabase = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + 
            "/StreamingAssets/ItemList.json"));	                                 // Loads the JSON to the itemDatabase
		ConstructItemDatabase();	                                             // Builds the item database from the JSON

        Debug.Log(FetchItemByID(0).Description);
	}
    
    // This grabs a specific item from the list of all possible items
    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < itemDatabase.Count; i++)
        {
            if (database[i].ID == id)
                return database[i];
        }
        return null;
    }

    // Builds the item database from the JSON
	void ConstructItemDatabase(){
		for (int i = 0; i < itemDatabase.Count; i++){
			database.Add(new Item((int)itemDatabase[i]["id"], itemDatabase[i]["title"].ToString(), itemDatabase[i]["description"].ToString(),
                itemDatabase[i]["slug"].ToString(), itemDatabase[i]["type"].ToString(), bool.Parse(itemDatabase[i]["stackable"].ToString()), 
                (int)itemDatabase[i]["resale"]));
		}
	}

}

public class Item {
	public int ID { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
    public string Slug { get; set; }
    public string Type { get; set; }
    public bool Stackable { get; set; }
    public int Resale { get; set; }
    public Sprite Sprite { get; set; }

	public Item(int id, string title, string description, string slug, string type, bool stackable, int resale) {
		this.ID = id;
		this.Title = title;
		this.Description = description;
        this.Slug = slug;
        this.Type = type;
        this.Stackable = stackable;
        this.Resale = resale;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);

	}

    public Item()
    {
        this.ID = -1;
    }
}