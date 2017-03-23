using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC.TimeOfDaySystemFree;


/// <summary>
/// Component to keep track of player stats relative to the time of day managers timeline. Has
/// public functions to check on the player stats and force a certain amount of time to pass.
/// decay rates for states can be publically set. Default max of any stat is 100. Default decay is 30 units per day cycle
/// </summary>
public class StatTracker : MonoBehaviour {

    // link this to the time of day manager in the hierarchy
    private TimeOfDayManager tdm;

    // decays can range from 0-100. They are the amount, out of 100, that the stat will decay in 1 day
    public float hungerDecay = 30;
    public float warmthDecay = 30;

    // section for the stats. If a script tries to set them over their max, then they are chopped down to their max
    public float Happiness { get { return happiness; } set { happiness = value;  if (happiness > maxHappiness) happiness = maxHappiness; } }
    public float Hunger { get { return hunger; } set { hunger = value; if (hunger > maxHunger) hunger = maxHunger; } }
    public float Warmth { get { return warmth; } set { warmth = value; if (warmth > maxWarmth) warmth = maxWarmth; } }

    public float maxHappiness = 100;
    public float maxHunger = 100;
    public float maxWarmth = 100;

    public string Charging;

    private float happiness = 100;
    private float hunger = 100;
    private float warmth = 100;


    // private float for scaling time.deltaTime appropriately with the total day length
    private float scaler;

	// Use this for initialization
	void Start () {
        tdm = GameObject.Find("Time Of Day Manager").GetComponent<TimeOfDayManager>();
        scaler = 1 / (tdm.dayInSeconds / 100);
        hungerDecay = hungerDecay / 100;
        warmthDecay = warmthDecay / 100;
    }
	
	// Update is called once per frame
	void Update () {
        warmth -= (Time.deltaTime * scaler * warmthDecay);
        hunger -= (Time.deltaTime * scaler * hungerDecay);
        happiness = (warmth + hunger) / 2;

        if (Charging != "")
            Debug.Log("Charging " + Charging);

        // adjust warmth, hunger, and happiness
        if (Charging == "warmth")
        {
            warmth += 2 * (Time.deltaTime * scaler * warmthDecay);
            return;
        }
        if (Charging == "hunger")
        {
            hunger += 2 * (Time.deltaTime * scaler * hungerDecay);
            return;
        }
        if (Charging == "happiness")
        {
            happiness += 1;
            return;
        }
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
