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

    /// <summary>
    /// List of all merchants in the scene.
    /// </summary>
    public List<GameObject> Merchants;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            GameObject npc = transform.GetChild(0).transform.GetChild(i).gameObject;
            Pedestrians.Add(npc);
        }
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            GameObject npc = transform.GetChild(1).transform.GetChild(i).gameObject;
            Cops.Add(npc);
        }
        for (int i = 0; i < transform.GetChild(2).childCount; i++)
        {
            GameObject npc = transform.GetChild(2).transform.GetChild(i).gameObject;
            Merchants.Add(npc);
        }
	}
}
