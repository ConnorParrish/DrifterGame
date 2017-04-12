using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC.TimeOfDaySystemFree;
using UnityEngine.UI;

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

    [Range(0,100)]
    public int HappinessThreshold;
    [Range(0,100)]
    public int HungerThreshold;
    [Range(0,100)]
    public int WarmthThreshold;

    public bool isSad;
    public bool isHungry;
    public bool isFreezing;
    public bool isDrugged;

    public float drugDuration = 0f;

    public string Charging;

    private float happiness = 100;
    private float hunger = 100;
    private float warmth = 100;

    private Transform statusEffectOverlayTransform;
    private GameObject SadnessOverlay;
    private GameObject HungryOverlay;
    private GameObject FreezingOverlay;
    private GameObject DruggedOverlay;
    public Sprite SadnessOverlaySprite;
    public Sprite HungryOverlaySprite;
    public Sprite FreezingOverlaySprite;
    public Sprite DruggedOverlaySprite;

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

        SadnessOverlay.GetComponent<Image>().sprite = SadnessOverlaySprite;
        HungryOverlay.GetComponent<Image>().sprite = HungryOverlaySprite;
        FreezingOverlay.GetComponent<Image>().sprite = FreezingOverlaySprite;
        DruggedOverlay.GetComponent<Image>().sprite = DruggedOverlaySprite;
    }

    // Update is called once per frame
    void Update () {
        warmth -= (Time.deltaTime * scaler * warmthDecay);
        hunger -= (Time.deltaTime * scaler * hungerDecay);
        happiness = (warmth + hunger) / 2;
        
        isSad = (happiness < HappinessThreshold);
        isHungry = (hunger < HungerThreshold);
        isFreezing = (warmth < WarmthThreshold);
        isDrugged = (drugDuration > 0f);

        if (SadnessOverlay.activeSelf != isSad)
        {
            if (isSad)
            {
                Player.Instance.WorldInteraction.navMeshAgent.speed = Player.Instance.WorldInteraction.navMeshAgent.speed * .75f;
            }
            SadnessOverlay.SetActive(isSad);

        }
        if (HungryOverlay.activeSelf != isHungry)
        {
            if (isHungry)
            {
                Player.Instance.WorldInteraction.navMeshAgent.speed = Player.Instance.WorldInteraction.navMeshAgent.speed * .75f;
            }
            HungryOverlay.SetActive(isHungry);

        }
        if (FreezingOverlay.activeSelf != isFreezing)
        {
            if (isHungry)
            {
                Player.Instance.WorldInteraction.navMeshAgent.speed = Player.Instance.WorldInteraction.navMeshAgent.speed * .75f;
            }
            FreezingOverlay.SetActive(isFreezing);

        }

        if (DruggedOverlay.activeSelf != isDrugged)
        {
            Debug.Log("drugged state changed: " + tdm.timeline);
            if (isDrugged)
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
            DruggedOverlay.SetActive(isDrugged);
        }

        if (tdm.timeline >= cleanAtTime && cleanAtTime != 0f)
        {
            drugDuration = 0f;

 
        }


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
