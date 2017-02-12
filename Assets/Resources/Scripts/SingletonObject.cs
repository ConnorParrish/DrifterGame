using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonObject : MonoBehaviour {
    public static SingletonObject Instance { get { return instance; } }
    private static SingletonObject instance = null;
    void Awake()
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");

        //Check if any other instance, 
        if (instance != null && instance != this)
        {
            //Debug.Log("Another instance detected, " + SaveScript.TMD.noOfDeath + " destroyed");
            instance.transform.GetChild(3).gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}
