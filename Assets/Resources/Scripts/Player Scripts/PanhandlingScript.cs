﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class PanhandlingScript : MonoBehaviour {
	public int begsRemaining;
    public bool infiniteBegs;
    public bool canPivot = true;

    public SplineInterpolator splineInterp;
    public float navMeshRadius = .5f;
    public float panhandleRadius;
    public Inventory inv;
    private GameObject leaveButton;
    /*
        private void OnEnable()
        {
            splineInterp = Camera.main.GetComponent<SplineInterpolator>();
            GetComponent<NavMeshAgent>().radius = 0;
        }
        private void OnDisable()
        {
            GetComponent<NavMeshAgent>().radius = .5f;
        }
        */

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, panhandleRadius);
    }

    // Use this for initialization
    void Start () {
        //splineInterp = Camera.main.GetComponent<SplineInterpolator>();
        inv = GameObject.Find("Inventory Manager").GetComponent<Inventory>();
        this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (splineInterp != null && splineInterp.mState == "Stopped" && canPivot)
        {
            Vector3 temp = gameObject.transform.eulerAngles;
            float width = Input.mousePosition.x / Screen.currentResolution.width - .5f;
            float height = Input.mousePosition.y / Screen.currentResolution.height - .5f;
            temp.x += height * -15f + 15;
            temp.y += width * 30f;
            Camera.main.transform.eulerAngles = temp;
        }

		if (Input.GetButtonDown("Fire1")){ // If the player clicks (left mouse)
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, panhandleRadius)){ // Returns true if the raycast hit something
				if (hit.collider.gameObject.tag == "Interactable Object" && hit.collider.gameObject.GetComponent<Pedestrian>()){ // Checks to see if the player clicked a pedestrian
					if (begsRemaining > 0){
                        Debug.Log("begging");
                        if (!infiniteBegs)
                            begsRemaining--;
                        hit.collider.gameObject.GetComponent<Pedestrian>().OnPanhandleClick(inv);
 					} else {
						Debug.Log("No more begs");
					}
				}
			}
		}


	}
}
