using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {
    /// <summary>
    /// List of all pedestrians in the scene.
    /// </summary>
    public List<GameObject> Pedestrians;
    /// <summary>
    /// List of all cops in the scene.
    /// </summary>
    public List<GameObject> Cops;

	// Use this for initialization
	void Start () {
        GameObject NPCsObject = GameObject.Find("NPCs");
        for (int i = 0; i < NPCsObject.transform.childCount; i++)
        {
            GameObject npc = NPCsObject.transform.GetChild(i).gameObject;

            if (npc.GetComponent<Pedestrian>())
                Pedestrians.Add(npc);
            else if (npc.GetComponent<Cop>())
                Cops.Add(npc);
        }
	}
}
