using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PanhandleActivator : ActionItem {
    /// <summary>
    /// The SplineInterpolator object used to interpolate between the camera's starting
    /// position and it's end position.
    /// </summary>
    public SplineInterpolator splineInterpolator;
    /// <summary>
    /// The SplineController object used to manage the settings of the SplineInterpolator
    /// </summary>
    public SplineController splineController;
    /// <summary>
    /// The parent object containing all the spline nodes for a path.
    /// </summary>
    public GameObject splineRoots;
    
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
            Camera.main.gameObject.GetComponent<CameraController>().enabled = false;
            player.transform.forward = gameObject.transform.forward;
			playerAgent = null;
        }
    }

}
