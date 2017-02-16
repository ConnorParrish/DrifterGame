using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SouthFacing : PanhandleActivator {

    // Use this for initialization
    public override void MoveToInteraction(NavMeshAgent playerAgent)
    {
        base.pathToSet = 2;
        
        base.MoveToInteraction(playerAgent);
    }
}
