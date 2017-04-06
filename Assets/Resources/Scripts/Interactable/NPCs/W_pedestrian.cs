﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_pedestrian : Pedestrian {
    
    public override void Start()
    {
        base.NPCData = GameObject.Find("NPC Manager").GetComponent<NPCDatabase>().npcDict["Wealthy Pedestrian"];
        base.Start();   
    }
}
