using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This component will create a worldspace canvas dialogue popup designed to show one simple message
/// </summary>

public class simpleDialogue : MonoBehaviour {

    public GameObject popUpDialogue;
    public float messageBloomTime = .7f;
    public float messageDisplayTime = 3f;

    GameObject canvas;
    Text text;
    RectTransform t;

	// Use this for initialization
	void Start () {
        canvas = GameObject.Instantiate(popUpDialogue);
        text = canvas.GetComponentInChildren<Text>();
        t = canvas.GetComponent<RectTransform>();
        canvas.SetActive(false);
    }
	
	void OnMouseDown () {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100) && hit.collider.gameObject.Equals(gameObject))
            showDialogue();
	}

    private void showDialogue()
    {
        if (!canvas.activeSelf)
        {
            canvas.SetActive(true);
            text.text = "KJNBASKDJNAKSD";
            StartCoroutine(growCanvas());
        }
    }

    IEnumerator growCanvas()
    {
        float waitTime = messageBloomTime;
        float endScale = .01f;
        float currentScale;
        float timePassed = 0f;

        while (waitTime > timePassed)
        {
            timePassed += Time.deltaTime;
            currentScale = endScale / (waitTime / timePassed);
            t.localScale = new Vector3(currentScale, currentScale, 0f);
            yield return new WaitForSeconds(.01f);
        }

        yield return new WaitForSeconds(messageDisplayTime);
        timePassed = 0;
        
        while (waitTime > timePassed)
        {
            timePassed += Time.deltaTime;
            currentScale = endScale * ((1-timePassed) / waitTime);
            t.localScale = new Vector3(currentScale, currentScale, 0f);
            yield return new WaitForSeconds(.01f);
        }

        canvas.SetActive(false);

    }
}
