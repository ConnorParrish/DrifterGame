﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PanhandleActivator : ActionItem {
    public SplineInterpolator splineInterpolator;
    public SplineController splineController;
    public GameObject splineRoots;
    public bool hasInteracted;
    public int pathToSet;

    public override void MoveToInteraction(NavMeshAgent playerAgent)
    {
        //gameObject.GetComponent<BoxCollider>().enabled = false;
        splineRoots = transform.GetChild(0).gameObject;
        //splineRoots.GetComponent<SimpleFollow>().toFollow = this.gameObject;

        base.MoveToInteraction(playerAgent);
    }
    public override void Interact()
    {
        if (!hasInteracted)
        {
            SplineController splineController = Camera.main.gameObject.AddComponent<SplineController>();
            splineController.SplineRootHolder = splineRoots;
            splineController.AutoClose = false;
            splineController.Duration = 5f;
            //splineController.pathIndex = pathToSet;
            SplineInterpolator splineInterpolator = Camera.main.gameObject.AddComponent<SplineInterpolator>();
            hasInteracted = true;
            gameObject.SetActive(false);
        }
    }

}
