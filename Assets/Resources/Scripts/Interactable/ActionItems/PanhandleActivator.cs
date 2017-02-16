using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PanhandleActivator : ActionItem {
    public SplineInterpolator splineInterpolator;
    public SplineController splineController;
    public GameObject splineRoots;
    public int pathToSet;

    private GameObject player;

    public override void MoveToInteraction(NavMeshAgent playerAgent)
    {
        //gameObject.GetComponent<BoxCollider>().enabled = false;
        splineRoots = transform.GetChild(0).gameObject;
        player = GameObject.Find("Player");
        //splineRoots.GetComponent<SimpleFollow>().toFollow = this.gameObject;

        base.MoveToInteraction(playerAgent);
    }
    public override void Interact()
    {
        if (true)
        {
            SplineController splineController = Camera.main.gameObject.AddComponent<SplineController>();
            splineController.SplineRootHolder = splineRoots;
            //splineController.AutoClose = false;
            splineController.Duration = 3f;
            //SplineInterpolator splineInterpolator = Camera.main.gameObject.AddComponent<SplineInterpolator>();
            gameObject.SetActive(false);
            player.GetComponent<WorldInteraction>().canMove = false;
            player.GetComponent<PanhandlingScript>().enabled = true;
            player.transform.forward = gameObject.transform.forward;
        }
    }

}
