using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;
using System.Linq;

public class WorldInteraction : MonoBehaviour {
	public Sprite destinationSprite;
    public bool canMove = true;
	public UnityEngine.AI.NavMeshAgent navMeshAgent;
	public bool walking;
	private bool clickedPanhandle;
	private bool interacted;
	private GameObject destinationObject;
    private Animator anim;

	// Use this for initialization
	void Start () {
		destinationObject = new GameObject();
		destinationObject.AddComponent<SpriteRenderer>();
		destinationObject.GetComponent<SpriteRenderer>().sprite = destinationSprite;
        anim = GetComponentInChildren<Animator>();


		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire2"))
        {
            //Debug.Break();
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		    RaycastHit[] hits = Physics.RaycastAll(ray, 100);

			if (hits != null && canMove){
				foreach (RaycastHit hit in hits.Reverse<RaycastHit>())
                {
					Debug.Log (hit.collider.gameObject.tag);
					if (hit.collider.gameObject.tag == "Interactable Object")
                    {
						Debug.Log ("its an interactable");
                        hit.collider.gameObject.GetComponent<Interactable>().MoveToInteraction(navMeshAgent);
						anim.SetBool ("IsWalking", true);
                        break;

                    }
                    else if (canMove && hit.collider.gameObject.tag == "Walkable")
                    {
						
                        navMeshAgent.stoppingDistance = 0f;
                        //walking = true;
                        anim.SetBool("IsWalking", true);
                        destinationObject.SetActive(true);
                        destinationObject.transform.position = hit.point;
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
                destinationObject.SetActive(false);
			} else {
				walking = true;
				//anim.SetBool ("IsWalking", true);
				
			}
		}
	}
}
