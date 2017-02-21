using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour {
	public Sprite destinationSprite;
    public bool canMove = true;
	private UnityEngine.AI.NavMeshAgent navMeshAgent;
	public bool walking;
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
            //Debug.Break();
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		    RaycastHit[] hits = Physics.RaycastAll(ray, 100);

			if (hits != null && canMove){
                foreach (RaycastHit hit in hits)
                {
                    Debug.Log("Collider name" + hit.collider.gameObject.name);
                    if (hit.collider.gameObject.tag == "Interactable Object")
                    {
                        Debug.Log("Is Interacting...");
                        hit.collider.gameObject.GetComponent<Interactable>().MoveToInteraction(navMeshAgent);
                    }
                    else if (hit.collider.gameObject.tag == "Walkable")
                    {
                        Debug.Log("isWalking");
                        navMeshAgent.stoppingDistance = 0f;
                        walking = true;
                        Debug.Log("walking: " + walking);
                        anim.SetBool("IsWalking", true);
                        destinationObject.SetActive(true);
                        destinationObject.transform.position = hit.point;
                        navMeshAgent.destination = hit.point;
                        navMeshAgent.Resume();
                        

                    }

                }
            }
            return;
		}

		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance){
			if (!navMeshAgent.hasPath || Mathf.Abs (navMeshAgent.velocity.sqrMagnitude) < float.Epsilon){
                Debug.Log("if this shows up... hmmm");
				walking = false;
                anim.SetBool("IsWalking", false);
                destinationObject.SetActive(false);
			} else {
				walking = true;
				
			}
		}
	}
}
