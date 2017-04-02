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

    public GameObject panhandleButton;
    
    private void Start()
    {
        //panhandleButton = GameObject.Find("LeavePanhandling");
        //panhandleButton.SetActive(false);
    }

    public override void MoveToInteraction(NavMeshAgent playerAgent)
    {
        //gameObject.GetComponent<BoxCollider>().enabled = false;
        splineRoots = transform.GetChild(0).gameObject;
        //splineRoots.GetComponent<SimpleFollow>().toFollow = this.gameObject;

        base.MoveToInteraction(playerAgent);
    }

    public override void Interact()
    {
        if (true)
        {
            splineController = Camera.main.gameObject.AddComponent<SplineController>();
            splineController.SplineRootHolder = splineRoots;
            //splineController.AutoClose = false;
            splineController.Duration = 3f;
            splineInterpolator = Camera.main.gameObject.GetComponent<SplineInterpolator>();
            //transform.GetChild(1).gameObject.SetActive(false);
            Player.Instance.WorldInteraction.canMove = false;
            Player.Instance.PanhandlingScript.enabled = true;
            Camera.main.gameObject.GetComponent<CameraController>().enabled = false;
            Player.Instance.transform.forward = gameObject.transform.forward;
			playerAgent = null;
        }
    }

    public override void Update()
    {
        base.Update();
        if (splineController != null)
        {
            Debug.Log("sI.mState: " + splineInterpolator.mState);
            if (splineInterpolator.mState == "Stopped")
            {
                if (!splineInterpolator.ended)
                    panhandleButton.SetActive(true);
                else
                    Camera.main.GetComponent<CameraController>().enabled = true;

            }
        }
    }

}
