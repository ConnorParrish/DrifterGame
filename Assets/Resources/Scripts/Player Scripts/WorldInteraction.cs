using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;
using System.Linq;

[Serializable]
public class PlayerStateBools
{
    public bool canMove;
    public bool walking;
    public bool beingInterrogated;
}

public class WorldInteraction : MonoBehaviour {
    public PlayerStateBools stateBools;

    [HideInInspector]
    public NavMeshAgent navMeshAgent;

	private bool clickedPanhandle;
    private Animator anim;
    private GameObject panhandleButton;
    private GameObject merchantButton;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

	// Use this for initialization
	void Start () {
        
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

			if (hits != null && stateBools.canMove){
				foreach (RaycastHit hit in hits.Reverse<RaycastHit>())
                {
					if (hit.collider.gameObject.tag == "Interactable Object")
                    {
                        hit.collider.gameObject.GetComponent<Interactable>().hasInteracted = false;
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
                    else if (stateBools.canMove && ( hit.collider.gameObject.tag == "Walkable" || hit.collider.gameObject.tag == "Sidewalk") )
                    {
						
                        navMeshAgent.stoppingDistance = 0f;
                        //walking = true;
                        anim.SetBool("IsWalking", true);
                        //destinationMarker.SetActive(true);
                        //destinationMarker.transform.position = hit.point;
                        navMeshAgent.destination = hit.point;
                        navMeshAgent.Resume();

                    }

                }
            }
            return;
		}

		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance){
			if (!navMeshAgent.hasPath || Mathf.Abs (navMeshAgent.velocity.sqrMagnitude) < float.Epsilon){
                stateBools.walking = false;
                anim.SetBool("IsWalking", false);
                //destinationMarker.SetActive(false);
			} else {
                stateBools.walking = true;
				//anim.SetBool ("IsWalking", true);
				
			}
		}
	}
}
