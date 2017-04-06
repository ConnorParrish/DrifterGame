using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cop : NPCInteraction {
    public GameObject player;
    private Animator anim;
    private Vector3 originalPosition;
    public bool isInterrogating;

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
        Debug.Log("originalPosition: " + originalPosition);
        originalPosition = this.transform.position;
        Debug.Log("originalPosition: " + originalPosition);
        anim.SetBool("isRunning", true);
        NavMeshAgent copAgent = gameObject.GetComponent<NavMeshAgent>();
        copAgent.destination = playerTransform.position;
        copAgent.stoppingDistance = 4f;
        playerAgent = copAgent;
        this.pa_destination = playerTransform.position;
    }

    public override void Interact()
    {
        if (Player.Instance.PanhandlingScript.enabled)
        {
            isInterrogating = true;

            Player.Instance.WorldInteraction.canMove = false;


            anim.SetTrigger("hasArrived");
            anim.SetBool("isRunning", false);
            //player.GetComponent<PanhandlingScript>().canPivot = false;
            Player.Instance.PanhandlingScript.enabled = false;

            GameObject panhandlingActivatorGO = Camera.main.GetComponent<SplineController>().SplineRootHolder.transform.parent.gameObject;
            Camera.main.GetComponent<SplineInterpolator>().mState = "Once";
            Camera.main.GetComponent<SplineInterpolator>().ended = true;
            Camera.main.GetComponent<SplineInterpolator>().mCurrentIdx++;

            fDialog.showDialogue("negative");
        }
        else
        {
            fDialog.showDialogue("negative");
        }
    }

    public override void Update()
    {
        if (!fDialog.canvas.activeSelf && isInterrogating)
        {
            Debug.Log(isInterrogating);
            isInterrogating = false;
            Debug.Log(isInterrogating);
            Player.Instance.WorldInteraction.canMove = true;
            playerAgent.stoppingDistance = 0f;
            Debug.Log("playerAgent.destination: " + playerAgent.destination);
            playerAgent.destination = originalPosition;
            Debug.Log("playerAgent.destination: " + playerAgent.destination);
            playerAgent.Resume();
            
        }

        if (playerAgent != null && !playerAgent.pathPending)//&& this.pa_destination == new Vector2(playerAgent.destination.x, playerAgent.destination.z))
        {
            float speed = playerAgent.desiredVelocity.magnitude;

            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance * 1f)
            {
                if (!hasInteracted)
                {
                    playerAgent.Stop();
                    Debug.Log(playerAgent.transform.position);
                    Quaternion targetRotation = Quaternion.LookRotation(playerAgent.transform.position - transform.position);
                    float strength = Mathf.Min(.5f * Time.deltaTime, 1f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, strength);

                    Interact();
                    //Stopping(out speed);
                    hasInteracted = true;

                }
            }
            if (playerAgent.remainingDistance > playerAgent.stoppingDistance && hasInteracted)
            {
                //hasInteracted = false;
                if (fDialog != null)
                {
                    fDialog.endDialogue();
                }
            }
        }
    }
}
