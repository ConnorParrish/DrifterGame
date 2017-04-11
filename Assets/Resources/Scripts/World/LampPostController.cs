using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC.TimeOfDaySystemFree;

public class LampPostController : MonoBehaviour {

    private Light light;
    private TimeOfDayManager tod;
    private bool lightActive = true;

    /// <summary>
    /// time, in 24 hour clock, when lights go on IE when it gets dark
    /// </summary>
    public int lightsOn = 18;

    /// <summary>
    /// time, in 24 hour clock, when lights go out IE when morning starts
    /// </summary>
    public int lightsOut = 6;

	// Use this for initialization
	void Start () {
        light = GetComponentInChildren<Light>();
        tod = GameObject.Find("Time Of Day Manager").GetComponent<TimeOfDayManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (lightActive && lightsOut < tod.timeline && tod.timeline < lightsOn)
        {
            lightActive = false;
            light.enabled = false;
        }

        else if (!lightActive &&( lightsOut > tod.timeline || tod.timeline > lightsOn))
        {
            lightActive = true;
            light.enabled = true;
        }
	}
}
