using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DeathEnforcer : MonoBehaviour {

    /// <summary>
    /// used to know if the player is currently alive or dead
    /// </summary>
    public static bool dead = false;
	
	// Update is called once per frame
	void Update () {
        StatTracker stats = Player.Instance.Stats;
        bool sleeping = SleepEnforcer.sleeping;
        if (stats != null && !dead && !sleeping)
        {
            if (stats.Hunger <= 0)
            {
                StartCoroutine(Die("hunger"));
                dead = true;
            }
            else if (stats.Warmth <= 0)
            {
                StartCoroutine(Die("cold"));
                dead = true;
            }
        }
	}

    IEnumerator Die(string cause)
    {
        // get dio
        fullDialogue dio = Player.Instance.GetComponent<fullDialogue>();
        // stop player moving
        Player.Instance.WorldInteraction.stateBools.canMove = false;
        Player.Instance.GetComponent<NavMeshAgent>().Stop();

        // show cause of death
        if (cause == "cold")
        {
            dio.showCustomDialogue("I feel so cold...");
        }
        else
        {
            dio.showCustomDialogue("I'm so hungry...");
        }
        // wait 2 seconds
        yield return new WaitForSeconds(5);

        // drama
        FadeManager.Instance.Fade(true, 15);
        dio.showCustomDialogue("I think I have to rest for a second...");
        yield return new WaitForSeconds(5);

        dio.showCustomDialogue("Why is everything going dark?...");
        yield return new WaitForSeconds(5);

        dio.showCustomDialogue("Mom?......");
        yield return new WaitForSeconds(5);
        dio.endDialogue();

        // load main menu
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("opening menu");
    }
}
