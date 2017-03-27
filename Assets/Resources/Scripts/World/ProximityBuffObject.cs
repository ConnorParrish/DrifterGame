using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityBuffObject : MonoBehaviour {

    public enum Type { Happiness,Warmth,Hunger }

    public Type BuffType;
    //public float AreaOfEffectRadius;

    private StatTracker stats;
    private GameObject player;
    private Collider radiusTrigger;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<StatTracker>();
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Player effeced");
        if (col.gameObject.tag == "Player")
        {
            if (BuffType == Type.Happiness)
                stats.Charging = "happiness";
            else if (BuffType == Type.Hunger)
                stats.Charging = "hunger";
            else if (BuffType == Type.Warmth)
                stats.Charging = "warmth";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        stats.Charging = "";
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
