using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class NPCDatabase : MonoBehaviour {

    /// <summary>
    /// Dictionary of every NPC in the game.
    /// </summary>
    /// <typeparam name="NPC Name">The name of the NPC you're looking for.</param>
    /// <typeparam name="NPC">The NPC type variable defined by the NPC Name</param>
	public Dictionary<string, NPC> npcDict = new Dictionary<string, NPC>();
	private List<NPC> npcList;

	// Use this for initialization
	void Start () {
		JsonData dioDatabase = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + 
			"/StreamingAssets/NPCList.json"));
		ConstructNPCDatabase (dioDatabase);
        
	}
	
	void ConstructNPCDatabase(JsonData data){
		for (int i = 0; i < data.Count; i++) {
			npcDict [data [i] ["name"].ToString ()] = new NPC (
				data[i]["name"].ToString(), data[i]["slug"].ToString(), data[i]["type"].ToString(), data[i]["dFrames"], data[i]["items"]);
		}
	}
}

public class NPC
{
	public string Name { get; set; }
	public string Slug { get; set; }
    public string Type { get; set; }
	public List<Dictionary<string, string>> DialogueFrames = new List<Dictionary<string, string>>();
    public List<Dictionary<string, string>> ItemsForSale = new List<Dictionary<string, string>>();

	public NPC(string name, string slug, string type, JsonData frames, JsonData items){
		Name = name;
		Slug = slug;
        Type = type;
		for (int i = 0; i < frames.Count; i++) {
			Dictionary<string, string> temp = new Dictionary<string, string> ();
			temp["text"] = frames[i]["text"].ToString();
            temp["tag"] = frames[i]["tag"].ToString();
			temp["itemID"] = frames[i]["itemID"].ToString();
			temp["cost"] = frames[i]["cost"].ToString();
			DialogueFrames.Add(temp);
		}

        for (int i = 0; i < items.Count; i++)
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            if (items[i]["itemID"].ToString() == "-1")
                return;

            temp["itemID"] = items[i]["itemID"].ToString();
            temp["price"] = items[i]["price"].ToString();
            temp["amount"] = items[i]["amount"].ToString();
            ItemsForSale.Add(temp);
        }
	}
}