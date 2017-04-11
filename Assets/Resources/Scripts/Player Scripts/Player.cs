using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance { get { return instance; } }
    private static Player instance = null;

    public Inventory Inventory;
    public StatTracker Stats;
    public PanhandlingScript PanhandlingScript;
    public WorldInteraction WorldInteraction;

    void Awake()
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");

        //Check if any other instance, 
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        Inventory = GameObject.Find("General UI Canvas").transform.GetChild(1).GetChild(0).GetComponent<Inventory>();
        Stats = GetComponent<StatTracker>();
        PanhandlingScript = GetComponent<PanhandlingScript>();
        WorldInteraction = GetComponent<WorldInteraction>();
    }
}
