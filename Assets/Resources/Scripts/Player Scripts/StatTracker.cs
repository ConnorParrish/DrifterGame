using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC.TimeOfDaySystemFree;


/// <summary>
/// Component to keep track of player stats relative to the time of day managers timeline. Has
/// public functions to check on the player stats and force a certain amount of time to pass.
/// decay rates for states can be publically set. 
/// </summary>
public class StatTracker : MonoBehaviour {

    // link this to the time of day manager in the hierarchy
    private TimeOfDayManager tdm;

    // decays can range from 0-100. They are the amount, out of 100, that the stat will decay in 1 day
    public float hungerDecay = 30;
    public float warmthDecay = 30;

    // section for the stats
    public float hunger { set; get; }
    public float warmth { set; get; }
    public float happiness { set; get; }

    // private float for scaling time.deltaTime appropriately with the total day length
    private float scaler;

	// Use this for initialization
	void Start () {
        tdm = GameObject.Find("Time Of Day Manager").GetComponent<TimeOfDayManager>();
        scaler = 1 / (tdm.dayInSeconds / 100);
        hungerDecay = hungerDecay / 100;
        warmthDecay = warmthDecay / 100;
        warmth = 100;
        hunger = 100;
        happiness = 100;
    }
	
	// Update is called once per frame
	void Update () {
        // adjust warmth, hunger, and happiness
        warmth -= Time.deltaTime * scaler * warmthDecay;
        hunger -= Time.deltaTime * scaler * hungerDecay;
        happiness = (warmth + hunger) / 2;
	}

    /// <summary>
    /// for other scripts to call to pass a certain amount of time and reflect that
    /// in the stat decay. Example: The sleep script will call this when they sleep and pass
    /// in the amount of time they are sleeping for. Units are in terms of the timeline from
    /// TimeOfDaySystemFree
    /// </summary>
    /// <param name="timePassed"></param>
    public void passTime(float timePassed)
    {
        warmth -= (timePassed/24) * warmthDecay * 100;
        hunger -= (timePassed/24) * hungerDecay * 100;
    }
}
