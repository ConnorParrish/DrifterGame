using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatGUIUpdater : MonoBehaviour {

    Slider happinessSlider;
    Slider hungerSlider;
    Slider warmthSlider;

    Slider happinessSliderInventory;
    Slider hungerSliderInventory;
    Slider warmthSliderInventory;

    StatTracker stats;


	// Use this for initialization
	void Start () {
        stats = GameObject.Find("Player").GetComponent<StatTracker>();

        // get the external sliders
        happinessSlider = transform.GetChild(0).GetComponent<Slider>();
        hungerSlider = transform.GetChild(1).GetComponent<Slider>();
        warmthSlider = transform.GetChild(2).GetComponent<Slider>();

        happinessSlider.maxValue = stats.maxHappiness;
        hungerSlider.maxValue = stats.maxHunger;
        warmthSlider.maxValue = stats.maxWarmth;

        // get the inventory sliders
        var temp = transform.parent.gameObject;
        temp = temp.transform.FindChild("Menu").transform.FindChild("Stats Panel").transform.FindChild("Stats").gameObject;
        happinessSliderInventory = temp.transform.FindChild("HappinessSlider").gameObject.GetComponent<Slider>();
        hungerSliderInventory = temp.transform.FindChild("HungerSlider").gameObject.GetComponent<Slider>();
        warmthSliderInventory = temp.transform.FindChild("WarmthSlider").gameObject.GetComponent<Slider>();

        happinessSliderInventory.maxValue = stats.maxHappiness;
        hungerSliderInventory.maxValue = stats.maxHunger;
        warmthSliderInventory.maxValue = stats.maxWarmth;
    }
	
	// Update is called once per frame
	void Update () {
        happinessSlider.value = stats.Happiness;
        hungerSlider.value = stats.Hunger;
        warmthSlider.value = stats.Warmth;

        happinessSliderInventory.value = stats.Happiness;
        hungerSliderInventory.value = stats.Hunger;
        warmthSliderInventory.value = stats.Warmth;
    }
}
