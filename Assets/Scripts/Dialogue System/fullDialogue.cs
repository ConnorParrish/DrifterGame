using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fullDialogue : MonoBehaviour
{

    /// <summary>
    /// This component is designed to support full dialogue for NPC's. Make sure you enter the data about the NPC containing
    /// this component into the NPC Json file. 
    /// </summary>

    public GameObject Dialogue;                  // link this to the fullDialogue prefab
    public int messageSpeed = 10;                // scale from 1-10 about how fast you want the message to write

    GameObject canvas;
    Text text;
    GameObject image;
    Button button;

    string currentText = "This is going to be replaced with dict stuff. Testing. Testing. woop..."; // this is temporary, ignore.
    IEnumerator currentCoroutine;

    void Start()
    {
        // fetch a couple components and game objects for future use
        canvas = GameObject.Instantiate(Dialogue);
        text = canvas.transform.FindChild("Image").gameObject.GetComponentInChildren<Text>();
        image = canvas.transform.FindChild("Image").gameObject;
        button = image.GetComponent<Button>();
        button.onClick.AddListener(showNextMessage);
        // set the canvas to false because we don't need it yet
        canvas.SetActive(false);

        // this is just to avoid a null reference error, ignore it.
        currentCoroutine = writeMessage();
    }

    void Update()
    {
        // need to put way of checking if they clicked away from the npc to move so the dialogue closes
    }

    void OnMouseDown()
    {
        // if they click the NPC and the dialogue isn't active, show the dialogue. Otherwise, close the dialogue
        if (!canvas.activeSelf)
            showDialogue();
        else
            endDialogue();
    }

    private void endDialogue()
    {
        // sort of a placeholder right now
        StartCoroutine(shrinkCanvas());
    }

    private void showNextMessage()
    {
        // code up here will cycle through the message set

        // stop the old coroutine if it has not finished
        StopCoroutine(currentCoroutine);

        // this section is for creating the next frame
        currentCoroutine = writeMessage();

        // starts typing the current message
        StartCoroutine(currentCoroutine);
    }

    private void showDialogue()
    {
        canvas.SetActive(true);
        StartCoroutine(growCanvas());
        text.text = "";
    }


    IEnumerator writeMessage()
    {
        text.text = "";
        char[] c = currentText.ToCharArray();
        foreach (char t in c)
        {
            text.text += t;
            if (t == '.')
                yield return new WaitForSeconds(.3f / messageSpeed);
            else
                yield return new WaitForSeconds(.1f / messageSpeed);
        }
    }

    IEnumerator growCanvas()
    {
        float waitTime = .3f;
        float endScale = .9f;
        float currentScale;
        float timePassed = 0f;
        RectTransform t = image.GetComponent<RectTransform>();

        // grow the scale of image game object which contains the text game object
        while (waitTime > timePassed)
        {
            timePassed += Time.deltaTime;
            currentScale = endScale / (waitTime / timePassed);
            t.localScale = new Vector3(currentScale, currentScale, 0f);
            yield return new WaitForSeconds(.01f);
        }
    }

    IEnumerator shrinkCanvas()
    {
        float waitTime = .3f;
        float startScale = .9f;
        float currentScale;
        float timePassed = 0f;
        RectTransform t = image.GetComponent<RectTransform>();

        // shrink the scale of image game object which contains the text game object
        while (waitTime > timePassed)
        {
            timePassed += Time.deltaTime;
            currentScale = startScale / (waitTime / (.3f - timePassed));
            t.localScale = new Vector3(currentScale, currentScale, 0f);
            yield return new WaitForSeconds(.01f);
        }

        canvas.SetActive(false);
    }
}
