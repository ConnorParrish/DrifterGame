using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatGuiUpdater : MonoBehaviour {

    RectTransform happinessBar;
    RectTransform hungerBar;
    RectTransform warmthBar;

    StatTracker stats;


	// Use this for initialization
	void Start () {
        happinessBar = gameObject.transform.FindChild("HealthBar").GetComponent<RectTransform>();
        hungerBar = gameObject.transform.FindChild("HungerBar").GetComponent<RectTransform>();
        warmthBar = gameObject.transform.FindChild("WarmthBar").GetComponent<RectTransform>();
        stats = GameObject.Find("Player").GetComponent<StatTracker>();
    }
	
	// Update is called once per frame
	void Update () {
        happinessBar.localScale = new Vector3(stats.happiness/100, 1, 1);
        hungerBar.localScale = new Vector3(stats.hunger/100, 1, 1);
        warmthBar.localScale = new Vector3(stats.warmth/100, 1, 1);
	}
}
