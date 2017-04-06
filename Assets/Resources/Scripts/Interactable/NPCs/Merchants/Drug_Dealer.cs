using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drug_Dealer : Merchant {

	// Use this for initialization
	public override void Start () {
        base.NPCData = GameObject.Find("NPC Manager").GetComponent<NPCDatabase>().npcDict["Drug Dealer"];
        base.Start();
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();
	}
}
