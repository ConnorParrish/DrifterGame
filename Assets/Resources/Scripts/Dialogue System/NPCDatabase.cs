using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class NPCDatabase : MonoBehaviour {

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
				data[i]["name"].ToString(), data[i]["slug"].ToString(), data[i]["dFrames"]);
		}
	}
}

public class NPC
{
	public string Name{ get; set; }
	public string Slug{ get; set; }
	public List<Dictionary<string, string>> DialogueFrames = new List<Dictionary<string, string>>();

	public NPC(string name, string slug, JsonData frames){
		Name = name;
		Slug = slug;
		for (int i = 0; i < frames.Count; i++) {
			Dictionary<string, string> temp = new Dictionary<string, string> ();
			temp ["text"] = frames [i] ["text"].ToString ();
            temp["tag"] = frames[i]["tag"].ToString();
			temp ["itemID"] = frames [i] ["itemID"].ToString ();
			temp ["cost"] = frames [i] ["cost"].ToString ();
			DialogueFrames.Add (temp);
		}
	}
}