﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityBuffObject : MonoBehaviour {

    public enum Type { Happiness,Warmth,Hunger }

    public Type BuffType;
    //public float AreaOfEffectRadius;

    private Collider radiusTrigger;

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Player effeced");
        if (col.gameObject.tag == "Player")
        {
            if (BuffType == Type.Happiness)
                Player.Instance.Stats.Charging = "happiness";
            else if (BuffType == Type.Hunger)
                Player.Instance.Stats.Charging = "hunger";
            else if (BuffType == Type.Warmth)
                Player.Instance.Stats.Charging = "warmth";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player.Instance.Stats.Charging = "";
    }

    /**
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stats.Charging = "";
        }
    }**/
}
