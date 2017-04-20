using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC.TimeOfDaySystemFree;
using UnityEngine.UI;
using System;

[Serializable]
public class StatThresholds
{
    [Range(0, 100)]
    public int HappinessThreshold;
    [Range(0, 100)]
    public int HungerThreshold;
    [Range(0, 100)]
    public int WarmthThreshold;
}

[Serializable]
public class MaximumValues
{
    public float maxHappiness = 100;
    public float maxHunger = 100;
    public float maxWarmth = 100;
}

[Serializable]
public class StateBooleans
{
    public bool isSad;
    public bool isHungry;
    public bool isFreezing;
    public bool isDrugged;
}

[Serializable]
public class OverlaySprites
{
    public Sprite SadnessOverlaySprite;
    public Sprite HungryOverlaySprite;
    public Sprite FreezingOverlaySprite;
    public Sprite DruggedOverlaySprite;

}

/// <summary>
/// Component to keep track of player stats relative to the time of day managers timeline. Has
/// public functions to check on the player stats and force a certain amount of time to pass.
/// decay rates for states can be publically set. Default max of any stat is 100. Default decay is 30 units per day cycle
/// </summary>
public class StatTracker : MonoBehaviour {

    // link this to the time of day manager in the hierarchy
    private TimeOfDayManager tdm;

    public StatThresholds statThresholds;
    public MaximumValues maxValues;
    public StateBooleans stateBools;
    

    // decays can range from 0-100. They are the amount, out of 100, that the stat will decay in 1 day
    [SerializeField]
    private float hungerDecay = 30;
    [SerializeField]
    private float warmthDecay = 30;

    // section for the stats. If a script tries to set them over their max, then they are chopped down to their max
    public float Happiness { get { return happiness; } set { happiness = value;  if (happiness > maxValues.maxHappiness) happiness = maxValues.maxHappiness; } }
    public float Hunger { get { return hunger; } set { hunger = value; if (hunger > maxValues.maxHunger) hunger = maxValues.maxHunger; } }
    public float Warmth { get { return warmth; } set { warmth = value; if (warmth > maxValues.maxWarmth) warmth = maxValues.maxWarmth; } }

    public float drugDuration = 0f;

    public string Charging;

    public OverlaySprites overlaySprites;


    private float happiness = 100;
    private float hunger = 100;
    private float warmth = 100;

    private Transform statusEffectOverlayTransform;
    private GameObject SadnessOverlay;
    private GameObject HungryOverlay;
    private GameObject FreezingOverlay;
    private GameObject DruggedOverlay;

    public float cleanAtTime;

    // private float for scaling time.deltaTime appropriately with the total day length
    private float scaler;

	// Use this for initialization
	void Start () {
        tdm = GameObject.Find("Time Of Day Manager").GetComponent<TimeOfDayManager>();
        scaler = 1 / (tdm.dayInSeconds / 100);
        hungerDecay = hungerDecay / 100;
        warmthDecay = warmthDecay / 100;

        statusEffectOverlayTransform = Player.Instance.Inventory.transform.parent.parent.GetChild(0);
        SadnessOverlay = statusEffectOverlayTransform.GetChild(0).gameObject;
        HungryOverlay = statusEffectOverlayTransform.GetChild(1).gameObject;
        FreezingOverlay = statusEffectOverlayTransform.GetChild(2).gameObject;
        DruggedOverlay = statusEffectOverlayTransform.GetChild(3).gameObject;

        SadnessOverlay.GetComponent<Image>().sprite = overlaySprites.SadnessOverlaySprite;
        HungryOverlay.GetComponent<Image>().sprite = overlaySprites.HungryOverlaySprite;
        FreezingOverlay.GetComponent<Image>().sprite = overlaySprites.FreezingOverlaySprite;
        DruggedOverlay.GetComponent<Image>().sprite = overlaySprites.DruggedOverlaySprite;
    }

    // Update is called once per frame
    void Update () {
        warmth -= (Time.deltaTime * scaler * warmthDecay);
        hunger -= (Time.deltaTime * scaler * hungerDecay);
        happiness = (warmth + hunger) / 2;
        
        stateBools.isSad = (happiness < statThresholds.HappinessThreshold);
        stateBools.isHungry = (hunger < statThresholds.HungerThreshold);
        stateBools.isFreezing = (warmth < statThresholds.WarmthThreshold);
        stateBools.isDrugged = (drugDuration > 0f);

        if (SadnessOverlay.activeSelf != stateBools.isSad)
        {
            if (stateBools.isSad)
            {
                Player.Instance.WorldInteraction.navMeshAgent.speed = Player.Instance.WorldInteraction.navMeshAgent.speed * .75f;
            }
            SadnessOverlay.SetActive(stateBools.isSad);

        }
        if (HungryOverlay.activeSelf != stateBools.isHungry)
        {
            if (stateBools.isHungry)
            {
                Player.Instance.WorldInteraction.navMeshAgent.speed = Player.Instance.WorldInteraction.navMeshAgent.speed * .75f;
            }
            HungryOverlay.SetActive(stateBools.isHungry);

        }
        if (FreezingOverlay.activeSelf != stateBools.isFreezing)
        {
            if (stateBools.isFreezing)
            {
                Player.Instance.WorldInteraction.navMeshAgent.speed = Player.Instance.WorldInteraction.navMeshAgent.speed * .75f;
            }
            FreezingOverlay.SetActive(stateBools.isFreezing);

        }

        if (DruggedOverlay.activeSelf != stateBools.isDrugged)
        {
            Debug.Log("drugged state changed: " + tdm.timeline);
            if (stateBools.isDrugged)
            {
                Player.Instance.WorldInteraction.navMeshAgent.speed = Player.Instance.WorldInteraction.navMeshAgent.speed * 2f;
                warmthDecay = warmthDecay * 1.3f;
                hungerDecay = hungerDecay * 1.3f;
                cleanAtTime = tdm.timeline + drugDuration;
                if (cleanAtTime != 0f) Debug.Log("Clean at: " + cleanAtTime);

            }
            else
            {
                Player.Instance.WorldInteraction.navMeshAgent.speed = Player.Instance.WorldInteraction.navMeshAgent.speed * 0.5f;
                Debug.Log("Hi");
                hungerDecay = hungerDecay * (10f/13f);
                warmthDecay = warmthDecay * (10f/13f);
            }
            DruggedOverlay.SetActive(stateBools.isDrugged);
        }

        if (tdm.timeline >= cleanAtTime && cleanAtTime != 0f)
        {
            drugDuration = 0f;

 
        }


        // adjust warmth, hunger, and happiness
        if (Charging == "warmth")
        {
			Warmth += 6 * (Time.deltaTime * scaler * warmthDecay);
            return;
        }
        if (Charging == "hunger")
        {
            Hunger += 6 * (Time.deltaTime * scaler * hungerDecay);
            return;
        }
        if (Charging == "happiness")
        {
            Happiness += 1;
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
