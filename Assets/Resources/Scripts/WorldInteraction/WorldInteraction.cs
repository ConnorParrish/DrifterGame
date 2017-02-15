using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour {
	public Sprite destinationSprite;
    public bool canMove = true;
	private UnityEngine.AI.NavMeshAgent navMeshAgent;
	private bool walking;
	private bool clickedPanhandle;
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
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		    RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100) && canMove){
				if (hit.collider.gameObject.tag == "Interactable Object"){
					hit.collider.gameObject.GetComponent<Interactable>().MoveToInteraction(navMeshAgent);
				} else {
					navMeshAgent.stoppingDistance = 0f;
					walking=true;
                    anim.SetBool("IsWalking", walking); 
					destinationObject.SetActive(true);
					destinationObject.transform.position = hit.point;
					navMeshAgent.destination = hit.point;
					navMeshAgent.Resume();

				}
			}
		}

		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance){
			if (!navMeshAgent.hasPath || Mathf.Abs (navMeshAgent.velocity.sqrMagnitude) < float.Epsilon){
				walking = false;
                anim.SetBool("IsWalking", walking);
                destinationObject.SetActive(false);
			} else {
				walking = true;
				
			}
		}
	}
}
