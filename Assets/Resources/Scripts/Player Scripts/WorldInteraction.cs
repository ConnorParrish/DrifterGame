using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;
using System.Linq;

public class WorldInteraction : MonoBehaviour {
    public GameObject destinationMarker;
    public bool canMove;
	public NavMeshAgent navMeshAgent;
	public bool walking;
    public bool beingInterrogated;
	private bool clickedPanhandle;
	private bool interacted;
    private Animator anim;
    private GameObject panhandleButton;
    private GameObject merchantButton;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        panhandleButton = GameObject.Find("LeavePanhandling");
        merchantButton = GameObject.Find("LeaveMerchant");

        panhandleButton.SetActive(false);
        merchantButton.SetActive(false);
		navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire2"))
        {
            //Debug.Break();
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		    RaycastHit[] hits = Physics.RaycastAll(ray, 100);

			if (hits != null && canMove){
				foreach (RaycastHit hit in hits.Reverse<RaycastHit>())
                {
					if (hit.collider.gameObject.tag == "Interactable Object")
                    {
                        if (hit.collider.gameObject.GetComponent<PedestrianWalker>() == true)
                        {
                            hit.collider.gameObject.GetComponent<PedestrianWalker>().navAgent.Stop();
                            hit.collider.gameObject.GetComponentInChildren<Animator>().SetBool("isWalking", false);
                        }
                        if (hit.collider.gameObject.GetComponent<PanhandleActivator>() == true)
                            hit.collider.gameObject.GetComponent<PanhandleActivator>().panhandleButton = panhandleButton;
                        if (hit.collider.gameObject.GetComponent<Merchant>() == true)
                            hit.collider.gameObject.GetComponent<Merchant>().merchantButton = merchantButton;
                        navMeshAgent.destination = hit.transform.position;
						hit.collider.gameObject.GetComponent<Interactable>().MoveToInteraction(navMeshAgent);
                        
						anim.SetBool ("IsWalking", true);
                        break;

                    }
                    else if (canMove && ( hit.collider.gameObject.tag == "Walkable" || hit.collider.gameObject.tag == "Sidewalk") )
                    {
						
                        navMeshAgent.stoppingDistance = 0f;
                        //walking = true;
                        anim.SetBool("IsWalking", true);
                        //destinationMarker.SetActive(true);
                        //destinationMarker.transform.position = hit.point;
                        navMeshAgent.destination = hit.point;
                        navMeshAgent.Resume();
						interacted = false;

                    }

                }
            }
            return;
		}

		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance){
			if (!navMeshAgent.hasPath || Mathf.Abs (navMeshAgent.velocity.sqrMagnitude) < float.Epsilon){
                walking = false;
                anim.SetBool("IsWalking", false);
                //destinationMarker.SetActive(false);
			} else {
				walking = true;
				//anim.SetBool ("IsWalking", true);
				
			}
		}
	}
}
