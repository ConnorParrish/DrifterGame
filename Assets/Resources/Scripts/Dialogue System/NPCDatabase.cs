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
				data[i]["name"].ToString(), data[i]["slug"].ToString(), data[i]["type"].ToString(), float.Parse(data[i]["money"].ToString()), data[i]["dFrames"], data[i]["items"]);
		}
	}
}

public class NPC
{
	public string Name { get; set; }
	public string Slug { get; set; }
    public string Type { get; set; }
    public float Money { get; set; }
    public List<Dictionary<string, string>> DialogueFrames = new List<Dictionary<string, string>>();
    public List<Dictionary<string, float>> ItemsForSale = new List<Dictionary<string, float>>();

	public NPC(string name, string slug, string type, float money, JsonData frames, JsonData items){
		Name = name;
		Slug = slug;
        Type = type;
        Money = money;
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
            Dictionary<string, float> temp = new Dictionary<string, float>();
            if (items[i]["itemID"].ToString() == "-1")
                return;

            temp["itemID"] = float.Parse(items[i]["itemID"].ToString());
            temp["price"] = float.Parse(items[i]["price"].ToString());
            temp["amount"] = float.Parse(items[i]["amount"].ToString());
            ItemsForSale.Add(temp);
        }
	}
}