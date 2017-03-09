using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC.TimeOfDaySystemFree;
using UnityEngine.UI;
using System;

public class ClockUpdater : MonoBehaviour {

    TimeOfDayManager td;
    Text t;

	// Use this for initialization
	void Start () {
        td = gameObject.transform.parent.parent.gameObject.GetComponent<TimeOfDayManager>();
        t = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        string newTime = "";
        string m;

        if (td.Hour > 12)
        {
            newTime += td.Hour - 12;
            m = " PM";
        }
        else
        {
            newTime += td.Hour;
            m = " AM";
        }

        newTime += ":";

        newTime += Convert.ToInt32(td.Minute/10);
        newTime += "0";

        newTime += m;
        
        t.text = newTime;
	}
}
