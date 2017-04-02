using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : NPCInteraction {

    public SplineInterpolator splineInterpolator;
    public SplineController splineController;
    public GameObject splineRoots;

    private GameObject PlayerHUD;
    public GameObject merchantButton;
    private GameObject merchantUI;
    private Animator anim; // for idle animation/reacting to purchases? (maybe)

	// Use this for initialization
	public override void Start () {
        merchantUI = GameObject.Find("Expanded Buy_Sell");
        PlayerHUD = GameObject.Find("General UI Canvas");

        merchantUI.SetActive(false);


        //merchantButton = GameObject.Find("LeaveMerchant");
        //merchantButton.SetActive(false);
        splineRoots = transform.GetChild(0).gameObject;

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
                merchantButton.SetActive(true);

            }
            else if (splineInterpolator.mState == "Stopped" && splineInterpolator.ended)
            {
                
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
        Player.Instance.WorldInteraction.canMove = false;
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
