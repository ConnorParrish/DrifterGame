using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : NPCInteraction {

    public SplineInterpolator splineInterpolator;
    public SplineController splineController;
    public GameObject splineRoots;

    private GameObject PlayerHUD;
    public GameObject merchantLeaver;
    private GameObject merchantUI;
    private Animator anim; // for idle animation/reacting to purchases? (maybe)

	// Use this for initialization
	public override void Start () {
        merchantUI = GameObject.Find("Expanded Buy_Sell");
        merchantUI.SetActive(false);
        PlayerHUD = GameObject.Find("General UI Canvas");
        merchantLeaver.SetActive(false);
        splineRoots = transform.GetChild(1).gameObject;

        anim = transform.GetChild(0).GetComponent<Animator>();
            
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
        if (splineController != null)

        {
            if (splineInterpolator.mState == "Stopped" && !splineInterpolator.ended)
            {
                merchantUI.SetActive(true);
                merchantLeaver.SetActive(true);

            }
            else if (splineInterpolator.mState == "Stopped" && splineInterpolator.ended)
            {
                merchantUI.SetActive(false);
                merchantLeaver.SetActive(false);
                PlayerHUD.SetActive(true);
            }
        }
    }

    public override void Interact()
    {
        playerAgent.Stop();
        PlayerHUD.SetActive(false);
        splineRoots.transform.GetChild(0).GetChild(0).position = Camera.main.transform.position;
        if (fDialog != null)
        {
            if (!fDialog.canvas.activeSelf)
            {
                
                //merchantUI.SetActive(true);
                fDialog.showDialogue("attention");
            }
            else
            {
                //merchantUI.SetActive(false);
                fDialog.endDialogue();
            }
        }
        //else if (sDialog != null)
        //    sDialog.showDialogue("merchant");
        playerAgent.GetComponent<WorldInteraction>().canMove = false;
    }
    
    public void Spline()
    {
        fDialog.endDialogue();
        splineController = Camera.main.gameObject.AddComponent<SplineController>();
        splineInterpolator = Camera.main.gameObject.GetComponent<SplineInterpolator>();
        splineController.SplineRootHolder = splineRoots;
        splineController.Duration = 4f;
        Camera.main.gameObject.GetComponent<CameraController>().enabled = false;
        
    }
}
