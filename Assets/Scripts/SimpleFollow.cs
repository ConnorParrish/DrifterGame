using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour {

    /// <summary>
    /// very simple component designed to allow one game object to follow another at an offset
    /// </summary>

    public GameObject toFollow;
    public Vector3 offset;
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = toFollow.transform.position + offset;
	}
}
