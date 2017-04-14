using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatGUIUpdater : MonoBehaviour {

    Slider happinessSlider;
    Slider hungerSlider;
    Slider warmthSlider;

	// Use this for initialization
	void Start () {

        // get the external sliders
        happinessSlider = transform.GetChild(0).GetComponent<Slider>();
        hungerSlider = transform.GetChild(1).GetComponent<Slider>();
        warmthSlider = transform.GetChild(2).GetComponent<Slider>();

        happinessSlider.maxValue = Player.Instance.Stats.maxValues.maxHappiness;
        hungerSlider.maxValue = Player.Instance.Stats.maxValues.maxHunger;
        warmthSlider.maxValue = Player.Instance.Stats.maxValues.maxWarmth;
    }
	
	// Update is called once per frame
	void Update () {
        happinessSlider.value = Player.Instance.Stats.Happiness;
        hungerSlider.value = Player.Instance.Stats.Hunger;
        warmthSlider.value = Player.Instance.Stats.Warmth;
    }
}
