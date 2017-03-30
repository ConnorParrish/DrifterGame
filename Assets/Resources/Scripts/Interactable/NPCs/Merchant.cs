using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : NPCInteraction {

    //public fullDialogue fDialog;        // for BUY/SELL mode
    //public simpleDialogue sDialog;      // for getting player's attention
    private GameObject merchantUI;
    private Animator anim; // for idle animation/reacting to purchases? (maybe)

	// Use this for initialization
	public override void Start () {
        merchantUI = GameObject.Find("Expanded Buy/Sell");
        merchantUI.SetActive(false);

        anim = transform.GetChild(0).GetComponent<Animator>();
        // base.NPCData = GameObject.Find("NPC Manager").GetComponent<NPCDatabase>().npcDict[] //THIS NEEDS TO BE IN SPECIFIC MERCHANT CLASS
            
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update();	
	}

    public override void Interact()
    {
        playerAgent.Stop();
        if (fDialog != null)
        {
            if (!fDialog.canvas.activeSelf)
            {
                
                merchantUI.SetActive(true);
                fDialog.showDialogue("attention");
            }
            else
            {
                merchantUI.SetActive(false);
                fDialog.endDialogue();
            }
        }
        //else if (sDialog != null)
        //    sDialog.showDialogue("merchant");
    }
}
