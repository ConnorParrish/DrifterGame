using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioDB : MonoBehaviour {

    public AudioClip moneyJingle;
    public AudioClip chewFood;

    public AudioSource oneShotPlayer;

    public static AudioDB Instance { get { return instance; }}
    private static AudioDB instance;

    void Start()
    {
        instance = this;
    }
    
    public void playCoinJingle()
    {
        oneShotPlayer.PlayOneShot(moneyJingle);
    }

    public void playChewFood()
    {
        oneShotPlayer.PlayOneShot(chewFood);
    }
}
