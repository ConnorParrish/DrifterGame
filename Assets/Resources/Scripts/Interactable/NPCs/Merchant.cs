﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Merchant : NPCInteraction {

    public SplineInterpolator splineInterpolator;
    public SplineController splineController;
    public GameObject splineRoots;

    private GameObject PlayerHUD;
    public GameObject merchantButton;
    private GameObject merchantUI;
    private Inventory merchantInv;
    private Animator anim; // for idle animation/reacting to purchases? (maybe)

	// Use this for initialization
	public override void Start () {
        merchantUI = transform.GetChild(1).gameObject;
        PlayerHUD = GameObject.Find("General UI Canvas");

        merchantUI.SetActive(false);

        merchantInv = merchantUI.transform.GetChild(0).GetComponent<Inventory>();
        Debug.Log(Player.Instance.Inventory.Money);
        

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
                //Time.timeScale = 0.0f;

            }
            else if (splineInterpolator.mState == "Stopped" && splineInterpolator.ended)
            {

            }
        }
    }

    public override void Interact()
    {

        merchantInv.AddMoney(Player.Instance.Inventory.Money);

        if (NPCData.ItemsForSale.Count == 0)
            foreach (ItemData itemData in Player.Instance.Inventory.inventoryMenu.GetComponentsInChildren<ItemData>().Where(i => i.item.ID != -1))
            {
                if (itemData.item.Type == NPCData.Filter)
                    for (int i = 0; i < itemData.amount; i++)
                        merchantInv.AddItem(itemData.item.ID);
            }
        else
            foreach (Dictionary<string, float> itemToSell in NPCData.ItemsForSale)
                for (int i = 0; i < itemToSell["amount"]; i++)
                    merchantInv.AddItem(int.Parse(itemToSell["itemID"].ToString()));

        playerAgent.Stop();
        PlayerHUD.SetActive(false);
        splineRoots.transform.GetChild(0).GetChild(0).rotation = Camera.main.transform.rotation;
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
