﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {

    public static FadeManager Instance { get { return instance; } }
    private static FadeManager instance;

    public Image fadeImage;
    public bool isInTranition;
    public float transition;
    public bool isShowing;
    public float duration;

    // Use this for initialization
    void Start()
    {
        instance = this;
        fadeImage.color = new Color(0, 0, 0, 255);
    }

    public void Fade(bool showing, float duration)
    {
        isShowing = showing;
        isInTranition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isInTranition)
            return;

        transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
        fadeImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);

        if (transition > 1 || transition < 0)
            isInTranition = false;
	}
}
