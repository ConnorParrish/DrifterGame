using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioDB : MonoBehaviour {

    public AudioClip moneyJingle;
    public AudioClip chewFood;
    public AudioClip Ambience1;
    public AudioClip Ambience2;
    public AudioClip BusNoises;

    public AudioSource oneShotPlayer;
    public AudioSource bChannel1;
    public AudioSource bChannel2;
    public AudioSource bChannel3;

    public static AudioDB Instance { get { return instance; }}
    private static AudioDB instance;

    void Start()
    {
        instance = this;
    }


    /// <summary>
    /// Play a one off sound the camera can always hear
    /// </summary>
    /// <param name="sound"></param>
    public void playOneShot(AudioClip sound)
    {
        oneShotPlayer.PlayOneShot(sound);
    }

    /// <summary>
    /// play a sound looping the camera can always hear
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="channel"></param>
    public void playLoop(AudioClip sound, int channel)
    {
        if (channel == 1)
        {
            bChannel1.PlayOneShot(sound);
        }
        else if (channel == 2)
        {
            bChannel2.PlayOneShot(sound);
        }
        else if (channel == 3)
        {
            bChannel3.PlayOneShot(sound);
        }
    }

    /// <summary>
    /// End a sound that is currently looping
    /// </summary>
    /// <param name="sound"></param>
    public void endLoop(AudioClip sound)
    {
        if (bChannel1.isPlaying.Equals(sound))
        {
            bChannel1.Stop();
        }
        else if (bChannel2.isPlaying.Equals(sound))
        {
            bChannel2.Stop();
        }
        else if (bChannel3.isPlaying.Equals(sound))
        {
            bChannel3.Stop();
        }
    }
}
