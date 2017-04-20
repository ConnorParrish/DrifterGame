using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotelKeeper : NPCInteraction {

    public override void Start()
    {
        base.NPCData = GameObject.Find("NPC Manager").GetComponent<NPCDatabase>().npcDict["Motel Keeper"];
        base.Start();
    }
}
