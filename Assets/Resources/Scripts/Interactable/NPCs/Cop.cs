using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop : NPCInteraction {

    public override void Start()
    {
        base.NPCData = GameObject.Find("NPC Manager").GetComponent<NPCDatabase>().npcDict["Police Officer"];
        base.Start();

    }
}
