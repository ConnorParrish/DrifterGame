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

	// Use this for initialization
	void Start () {
        // get the external sliders
        happinessSlider = transform.GetChild(0).GetComponent<Slider>();
        hungerSlider = transform.GetChild(1).GetComponent<Slider>();
        warmthSlider = transform.GetChild(2).GetComponent<Slider>();

        happinessSlider.maxValue = Player.Instance.Stats.maxHappiness;
        hungerSlider.maxValue = Player.Instance.Stats.maxHunger;
        warmthSlider.maxValue = Player.Instance.Stats.maxWarmth;

        // get the inventory sliders
        var temp = transform.parent.parent.gameObject;
        temp = temp.transform.FindChild("Inventory Menu").transform.FindChild("Stats Panel").transform.FindChild("Stats").gameObject;
        happinessSliderInventory = temp.transform.FindChild("HappinessSlider").gameObject.GetComponent<Slider>();
        hungerSliderInventory = temp.transform.FindChild("HungerSlider").gameObject.GetComponent<Slider>();
        warmthSliderInventory = temp.transform.FindChild("WarmthSlider").gameObject.GetComponent<Slider>();

        happinessSliderInventory.maxValue = Player.Instance.Stats.maxHappiness;
        hungerSliderInventory.maxValue = Player.Instance.Stats.maxHunger;
        warmthSliderInventory.maxValue = Player.Instance.Stats.maxWarmth;
    }
	
	// Update is called once per frame
	void Update () {
        happinessSlider.value = Player.Instance.Stats.Happiness;
        hungerSlider.value = Player.Instance.Stats.Hunger;
        warmthSlider.value = Player.Instance.Stats.Warmth;

        happinessSliderInventory.value = Player.Instance.Stats.Happiness;
        hungerSliderInventory.value = Player.Instance.Stats.Hunger;
        warmthSliderInventory.value = Player.Instance.Stats.Warmth;
    }
}
