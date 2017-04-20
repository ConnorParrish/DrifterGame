using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainyDayManager : MonoBehaviour {

    private bool raining = false;



    /// <summary>
    /// the chance of rain from 0-1
    /// </summary>
    [Range(0,1)]
    public float rainChance = 0.25f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
