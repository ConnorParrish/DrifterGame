using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NorthFacing : PanhandleActivator {

    // Use this for initialization
    public override void MoveToInteraction(NavMeshAgent playerAgent)
    {
        base.pathToSet = 0;

        base.MoveToInteraction(playerAgent);
    }
}
