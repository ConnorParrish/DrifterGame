using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour {
	public Sprite destinationSprite;
	private NavMeshAgent navMeshAgent;
	private bool walking;
	private bool clickedPanhandle;
	private GameObject destinationObject;
	

	// Use this for initialization
	void Start () {
		destinationObject = new GameObject();
		destinationObject.AddComponent<SpriteRenderer>();
		destinationObject.GetComponent<SpriteRenderer>().sprite = destinationSprite;
				
		navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Input.GetButtonDown ("Fire2")){
			if (Physics.Raycast(ray, out hit, 100)){
				if (hit.collider.gameObject.tag == "Interactable Object"){
					hit.collider.gameObject.GetComponent<Interactable>().MoveToInteraction(navMeshAgent);
				} else {
					navMeshAgent.stoppingDistance = 0f;
					walking=true;
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
				destinationObject.SetActive(false);
			} else {
				walking = true;
				
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "PanhandleActivate"){
			Debug.Log("Yo");
			Application.LoadLevel(1);
		}
	}
}
