using UnityEngine;
using System.Collections;

public class PanhandlingScript : MonoBehaviour {
	public int begsRemaining;
    public bool infiniteBegs;

	private Inventory inventory;

	// Use this for initialization
	void Start () {
		inventory = GameObject.Find("Inventory Manager").GetComponent<Inventory>();
        Debug.Log(inventory.name);

	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Input.GetButtonDown("Fire1")){ // If the player clicks (left mouse)
			if (Physics.Raycast(ray, out hit, 100)){ // Returns true if the raycast hit something
				if (hit.collider.gameObject.tag == "Interactable Object" && hit.collider.gameObject.GetComponent<W_pedestrian>()){ // Checks to see if the player clicked a pedestrian
					if (begsRemaining > 0){
                        if (infiniteBegs)
                            hit.collider.gameObject.GetComponent<InteractionWithPlayer>().OnPanhandleClick(inventory);
                        else
                        {
                            hit.collider.gameObject.GetComponent<InteractionWithPlayer>().OnPanhandleClick(inventory);
                            begsRemaining--;
                        }
					} else {
						Debug.Log("No more begs");
					}
				}
			}
		}	
	}
}
