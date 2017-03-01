using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cop : NPCInteraction {
    public GameObject player;
    private Animator anim;

    public override void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = transform.GetChild(0).GetComponent<Animator>();
        base.NPCData = GameObject.Find("NPC Manager").GetComponent<NPCDatabase>().npcDict["Police Officer"];
        base.Start();
    }

    /// <summary>
    /// Cop will run to player.
    /// </summary>
    public void RunToPlayer(Transform playerTransform)
    {
        Debug.Log("Imma comming for ya");
        anim.SetBool("isRunning", true);
        NavMeshAgent copAgent = gameObject.GetComponent<NavMeshAgent>();
        copAgent.destination = playerTransform.position;
        copAgent.stoppingDistance = 4f;
        playerAgent = copAgent;
        this.pa_destination = playerTransform.position;
    }

    public override void Interact()
    {
        anim.SetTrigger("hasArrived");
        anim.SetBool("isRunning", false);
        player.GetComponent<PanhandlingScript>().canPivot = false;
        player.GetComponent<PanhandlingScript>().enabled = false;

        GameObject panhandlingActivatorGO = Camera.main.GetComponent<SplineController>().SplineRootHolder.transform.parent.gameObject;
        Camera.main.GetComponent<SplineInterpolator>().mState = "Once";
        Camera.main.GetComponent<SplineInterpolator>().ended = true;
        Camera.main.GetComponent<SplineInterpolator>().mCurrentIdx++;

        fDialog.showDialogue("negative");
        Debug.Log("I'm interacting with you");
    }

    public override void Update()
    {
        if (!fDialog.canvas.activeSelf)
        {
            player.GetComponent<WorldInteraction>().canMove = true;
        }
        base.Update();
    }
}
