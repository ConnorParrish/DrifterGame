using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC.TimeOfDaySystemFree;
using UnityEngine.UI;
using System;

/// <summary>
/// simple compenent to update the clock with the right time using the Time of Day Manager
/// </summary>
public class ClockUpdater : MonoBehaviour {

    TimeOfDayManager td;
    Text t;

	// Use this for initialization
	void Start () {
        td = GameObject.Find("Time Of Day Manager").GetComponent<TimeOfDayManager>();
        t = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        string newTime = ""; // TIme in base 12 system
        string m; // AM or PM

        if (td.Hour > 12)
        {
            newTime += td.Hour - 12;
            m = " PM";
        }
        else if (td.Hour == 0)
        {
            newTime += "12";
            m = " AM";
        }
        else if (td.Hour == 12)
        {
            newTime += "12";
            m = " PM";
        }
        else
        {
            newTime += td.Hour;
            m = " AM";
        }

        newTime += ":";

        newTime += Math.Truncate(td.Minute/10); // truncate the minutes to factors of 10 b/c it's distracting if its always updating
        newTime += "0";

        newTime += m;
        
        t.text = newTime;
	}
}
