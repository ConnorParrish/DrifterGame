using UnityEngine;
using System.Collections;

public class FlickerScript : MonoBehaviour {
	public bool useRange;
	public bool useIntensity;
	public float min;
	public float max;

	Light myLight;

	// Use this for initialization
	void Start () {
		myLight = GetComponent<Light>();
	}
	
	void FlickLight(){
		float ranStrength = Random.Range(min,max);
		if (useRange) {
			myLight.range = ranStrength;
		} else if (useIntensity) {
			myLight.intensity = ranStrength;
		}
	}

	// Update is called once per frame
	void Update () {
		FlickLight();
	}
}
