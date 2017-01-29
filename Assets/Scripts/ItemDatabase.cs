using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour {
	private List<Item> database = new List<Item>();
	private JsonData itemData;

	// Use this for initialization
	void Start () {
		// Item item = new Item();
		itemData = JsonMapper.ToObject(File.ReadAllText("ItemList.json"));
		Debug.Log(itemData[0]["title"]);
	}
}

