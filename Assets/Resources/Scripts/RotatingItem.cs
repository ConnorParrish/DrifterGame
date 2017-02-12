using UnityEngine;
using System.Collections;

public class RotatingItem : MonoBehaviour {

	public float yRotation;

	// Update is called once per frame
	void FixedUpdate () {
		gameObject.transform.Rotate(0,yRotation,0);
	}
}
