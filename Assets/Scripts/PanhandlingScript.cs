using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerInventory))]

public class PanhandlingScript : MonoBehaviour {
	public int begsRemaining;

	private PlayerInventory inventory;

	// Use this for initialization
	void Start () {
		inventory = GetComponent<PlayerInventory>();

	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Input.GetButtonDown("Fire1")){ // If the player clicks (left mouse)
			if (Physics.Raycast(ray, out hit, 100)){ // Returns true if the raycast hit something
				if (hit.collider.gameObject.tag == "Pedestrian"){ // Checks to see if the player clicked a pedestrian
					if (begsRemaining > 0){ 
						hit.collider.gameObject.GetComponent<InteractionWithPlayer>().OnPanhandleClick(inventory);
						begsRemaining--;
					} else {
						Debug.Log("No more begs");
					}
				}
			}
		}	
	}
}
