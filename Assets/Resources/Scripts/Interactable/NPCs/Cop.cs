using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cop : NPCInteraction {
    [HideInInspector]
    public GameObject player;
    private Animator anim;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public bool isInterrogating;
    private bool isRotating;

    public override void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = transform.GetChild(0).GetComponent<Animator>();
        base.NPCData = GameObject.Find("NPC Manager").GetComponent<NPCDatabase>().npcDict["Police Officer"];
        base.Start();
        fDialog = gameObject.GetComponent<fullDialogue>();
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
        if (!fDialog.canvas.activeSelf)
        {
            if (Player.Instance.PanhandlingScript.enabled)
            {
                fDialog.showDialogue("negative");

                isInterrogating = true;

                Player.Instance.WorldInteraction.stateBools.canMove = false;


                anim.SetTrigger("hasArrived");
                anim.SetBool("isRunning", false);
                //player.GetComponent<PanhandlingScript>().canPivot = false;
                SplineDeactivator sp = GameObject.Find("DebugButtonCanvas").GetComponent<SplineDeactivator>();
                sp.DeactivatePanhandling();

            }
            else
            {
                fDialog.showDialogue("negative");
            }

        }
    }

    public override void Update()
    {
        if (isRotating)
        {
            Quaternion targetRotation = Quaternion.LookRotation(playerAgent.transform.position - transform.position);
            float strength = Mathf.Min(.5f * Time.deltaTime, 1f) * 4;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, strength);
        }
        else
        {
            float strength = Mathf.Min(.5f * Time.deltaTime, 1f) * 4;
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, strength);
        }

        if (!fDialog.canvas.activeSelf)
            isRotating = false;

        if (!fDialog.canvas.activeSelf && isInterrogating)
        {
            Debug.Log(isInterrogating);
            isInterrogating = false;
            Debug.Log(isInterrogating);
            Player.Instance.WorldInteraction.stateBools.canMove = true;
            playerAgent.stoppingDistance = 0f;
            Debug.Log("playerAgent.destination: " + playerAgent.destination);
            playerAgent.destination = originalPosition;
            Debug.Log("playerAgent.destination: " + playerAgent.destination);
            playerAgent.Resume();
            
        }

        if (playerAgent != null && !playerAgent.pathPending && this.pa_destination == new Vector2(playerAgent.destination.x, playerAgent.destination.z))
        {
            float speed = playerAgent.desiredVelocity.magnitude;

            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance * 1f)
            {
                if (!hasInteracted)
                {
                    originalRotation = transform.rotation;
                    playerAgent.Stop();
                    Debug.Log(playerAgent.transform.position);
                    isRotating = true;
                    

                    Interact();
                    //Stopping(out speed);
                    hasInteracted = true;

                }
            }
        }

        if (playerAgent != null && playerAgent.remainingDistance > playerAgent.stoppingDistance && hasInteracted)
        {
            isRotating = false;
            hasInteracted = false;
            Debug.Log("hi");
            //playerAgent = null;
            if (fDialog != null)
            {
                fDialog.endDialogue();
            }
        }

    }
}
