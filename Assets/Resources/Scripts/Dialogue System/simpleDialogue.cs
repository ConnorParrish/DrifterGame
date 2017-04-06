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

    public Texture2D curs1;
    public Texture2D curs2;
    public Texture2D curs3;

	// Use this for initialization
	void Start () {
        // fetch a couple components and game objects for future use
        canvas = GameObject.Instantiate(popUpDialogue);
        canvas.transform.SetParent(transform);
        text = canvas.GetComponentInChildren<Text>();
        t = canvas.GetComponent<RectTransform>();
        // set the canvas to false because we don't need it yet
        canvas.SetActive(false);

        // set the follow object on the toFollow script so the canvas follows the game object we are attached to
        canvas.GetComponent<SimpleFollow>().toFollow = gameObject;
    }

    /// <summary>
    /// method for debugging, disable when not in use
    /// </summary>
    //void OnMouseDown()
    //{
    //    canvas.SetActive(true);
    //    text.text = "Test Test my man";
    //    StartCoroutine(growCanvas());
    //}

    void Update()
    {
        // update the direction the dialogue bubble faces
        if (canvas.activeSelf)
        {
            canvas.transform.rotation = Camera.main.transform.rotation;
        }
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

    /// <summary>
    /// used in other components to show custom dialogue messages
    /// </summary>
    /// <param name="customMessage"></param>
    public void showCustomDialogue(string customMessage)
    {
        // we don't want to start the courtine again once it's already been started, so check if it's running
        if (!canvas.activeSelf)
        {
            canvas.SetActive(true); // enable the canvas and the follow script on the canvas

            text.text = customMessage;
            StartCoroutine(growCanvas());
        }
    }

    public void showDialogue(string tag)
    {
        if (!canvas.activeSelf)
        {
            canvas.SetActive(true);
            List<Dictionary<string, string>> dialogWithSpecifiedTag = new List<Dictionary<string,string>>();
            foreach (Dictionary<string,string> d in NPCData.DialogueFrames)
            {
                if (d["tag"] == tag)
                    dialogWithSpecifiedTag.Add(d);
            }

            if (dialogWithSpecifiedTag.Count == 0)
                throw new Exception("Tag not found!");
            text.text = dialogWithSpecifiedTag[UnityEngine.Random.Range(0, dialogWithSpecifiedTag.Count)]["text"];
            StartCoroutine(growCanvas());
        }
    }

    /// <summary>
    /// this method will grow the canvas, display the message for set time, then shrink and disable the canvas
    /// </summary>
    /// <returns></returns>
    IEnumerator growCanvas() 
    {
        float waitTime = messageBloomTime;
        float endScale = .01f;
        float currentScale;
        float timePassed = 0f;

        // start cycling the mouse pointer
        StartCoroutine(cycleMousePointer());

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
            currentScale = endScale / (waitTime / (waitTime - timePassed));
            t.localScale = new Vector3(currentScale, currentScale, 0f);
            yield return new WaitForSeconds(.01f);
        }

        // this disabes the canvas and allows the coroutine to begin again
        canvas.SetActive(false);
    }

    IEnumerator cycleMousePointer()
    {
        float totalTime = messageBloomTime * 2 + messageDisplayTime;
        float timepassed = 0f;
        float cycleTime = .2f;
        while (true)
        {
            Cursor.SetCursor(curs1, Vector2.zero, CursorMode.Auto);
            yield return new WaitForSeconds(cycleTime);
            timepassed += cycleTime;
            if (timepassed > totalTime)
                break;

            Cursor.SetCursor(curs2, Vector2.zero, CursorMode.Auto);
            yield return new WaitForSeconds(cycleTime);
            timepassed += cycleTime;
            if (timepassed > totalTime)
                break;

            Cursor.SetCursor(curs3, Vector2.zero, CursorMode.Auto);
            yield return new WaitForSeconds(cycleTime);
            timepassed += cycleTime;
            if (timepassed > totalTime)
                break;
        }
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
