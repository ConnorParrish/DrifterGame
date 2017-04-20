using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentManager : NPCInteraction {

    public override void Start()
    {
        base.NPCData = GameObject.Find("NPC Manager").GetComponent<NPCDatabase>().npcDict["Apartment Manager"];
        base.Start();
    }
}
