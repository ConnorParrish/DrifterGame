using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This component will create a worldspace canvas dialogue popup designed to show one simple message
/// </summary>

public class simpleDialogue : MonoBehaviour {

    public GameObject popUpDialogue;            // link this to the popUpDialogue prefab
    public float messageBloomTime = .7f;        // the amount of time the message spends blooming into and out of view
    public float messageDisplayTime = 3f;       // the amount of time the message is displayed for excluding bloom time


    public GameObject canvas;
    Text text;
    RectTransform t;
	public NPC NPCData;


	// Use this for initialization
	void Start () {
        // fetch a couple components and game objects for future use
        canvas = GameObject.Instantiate(popUpDialogue);
        text = canvas.GetComponentInChildren<Text>();
        t = canvas.GetComponent<RectTransform>();
        // set the canvas to false because we don't need it yet
        canvas.SetActive(false);

        // set the follow object on the toFollow script so the canvas follows the game object we are attached to
        canvas.GetComponent<SimpleFollow>().toFollow = gameObject;

    }

	
	void OnMouseDown () {
        //showDialogue();           // enable for debugging dialog
	}

    public void showDialogue()
    {
        // we don't want to start the courtine again once it's already been started, so check if it's running
        if (!canvas.activeSelf)
        {
            canvas.SetActive(true); // enable the canvas and the follow script on the canvas
			text.text = NPCData.DialogueFrames[UnityEngine.Random.Range(0, NPCData.DialogueFrames.Count)]["text"];
            StartCoroutine(growCanvas());
        }
    }

    IEnumerator growCanvas()
    {
        float waitTime = messageBloomTime;
        float endScale = .01f;
        float currentScale;
        float timePassed = 0f;

        // this first loop grows the scale of the canvas
        while (waitTime > timePassed)
        {
            timePassed += Time.deltaTime;
            currentScale = endScale / (waitTime / timePassed);
            t.localScale = new Vector3(currentScale, currentScale, 0f);
            yield return new WaitForSeconds(.01f);
        }

        // this waits for the message to display for the desired time
        yield return new WaitForSeconds(messageDisplayTime);
        timePassed = 0;
        
        // this shrinks the canvas back down now that we're done viewing
        while (waitTime > timePassed)
        {
            timePassed += Time.deltaTime;
            currentScale = endScale * ((1-timePassed) / waitTime);
            t.localScale = new Vector3(currentScale, currentScale, 0f);
            yield return new WaitForSeconds(.01f);
        }

        // this disabes the canvas and allows the coroutine to begin again
        canvas.SetActive(false);

    }
}
