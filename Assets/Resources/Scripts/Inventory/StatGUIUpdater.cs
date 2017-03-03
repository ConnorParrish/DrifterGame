using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatGUIUpdater : MonoBehaviour {

    Slider happinessSlider;
    Slider hungerSlider;
    Slider warmthSlider;

    StatTracker stats;


	// Use this for initialization
	void Start () {
        stats = GameObject.Find("Player").GetComponent<StatTracker>();

        happinessSlider = transform.GetChild(0).GetComponent<Slider>();
        hungerSlider = transform.GetChild(1).GetComponent<Slider>();
        warmthSlider = transform.GetChild(2).GetComponent<Slider>();

        happinessSlider.maxValue = stats.Happiness;
        hungerSlider.maxValue = stats.Hunger;
        warmthSlider.maxValue = stats.Warmth;
    }
	
	// Update is called once per frame
	void Update () {
        happinessSlider.value = stats.Happiness;
        hungerSlider.value = stats.Hunger;
        warmthSlider.value = stats.Warmth;
    }
}
