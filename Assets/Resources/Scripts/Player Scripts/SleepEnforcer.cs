using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC.TimeOfDaySystemFree;
using UnityEngine.AI;
using System;

public class SleepEnforcer : MonoBehaviour {

    public Animator ani;
    public TimeOfDayManager timi;
    public StatTracker stats;
    public NavMeshAgent agent;
    public WorldInteraction interaction;
    public simpleDialogue dio;
    private Inventory inv;

    /// <summary>
    /// Time player must sleep in 24 hour format, default midnight
    /// </summary>
    public int sleepTime = 1;

    /// <summary>
    /// Time player wakes in 24 hour format, default 6am
    /// </summary>
    public int wakeTime = 7;

    /// <summary>
    /// Chance of player being mugged 0 = 0% : 100 = 100%. Default is 10
    /// </summary>
    public int mugChance = 100;

    /// <summary>
    /// Max amount of money that could be taken from the player when mugged. Default is 50
    /// </summary>
    public int maxMugLoss = 50;

    private bool drowsyFired = false;
    private bool sleepFired = false;
    private bool mugged = false;
    

	void Start () {
        // ensure some dummy doesn't mess up the sleeptime or waketime
        if (sleepTime > 24 || sleepTime < 0)
            sleepTime = 24;
        if (wakeTime > 24 || wakeTime < 0)
            wakeTime = 6;
        // get a copy of the inventory
        inv = GameObject.Find("General UI Canvas").GetComponentInChildren<Inventory>();
        timi = GameObject.Find("Time Of Day Manager").GetComponent<TimeOfDayManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!drowsyFired && Math.Truncate(timi.timeline) == sleepTime - 1)
        {
            DrowsyAnnouncement();
            drowsyFired = true;
        }
		if (!sleepFired && Math.Truncate(timi.timeline) == sleepTime)
        {
            sleep(false);
            sleepFired = true;
        }
	}

    /// <summary>
    /// call this to start the player sleeping. if safe is true then they can't be mugged
    /// </summary>
    /// <param name="safe"></param>
    public void sleep(bool safe)
    {
        StartCoroutine(Sleep(safe));
    }

    private void DrowsyAnnouncement()
    {
        dio.showCustomDialogue("I am starting to get really sleepy...");
    }

    private void MuggedAnnouncement()
    {
        dio.showCustomDialogue("What the hell! Someone took my Money!");
    }

    private void RollMugging()
    {
        var ran = new System.Random();

        int temp = ran.Next(0, 100);
        mugged = (temp <= mugChance && inv.Money != 0);
        if (mugged)
        {
            inv.AddMoney(-ran.Next(0, maxMugLoss));
        }
    }

    /// <summary>
    /// The Coroutine for sleeping
    /// - stop player from moving CHECK
    /// - plays sleep animation CHECK
    /// - fades screen to black CHECK
    /// - changes stats CHECK
    /// - fades screen out of black CHECK
    /// - plays wake up animation CHECK
    /// - resets both animations CHECK
    /// - reset sleep and drowsy fired bools CHECK
    /// </summary>
    /// <returns></returns>
    IEnumerator Sleep(bool safe)
    {
        // stop the player from moving
        interaction.canMove = false;
        agent.Stop();
        // trigger sleep animation
        ani.SetBool("IsWalking", false);
        ani.SetTrigger("Sleep");
        // start screen fade
        FadeManager.Instance.Fade(true, 5);

        yield return new WaitForSeconds(5);

        
        // adjust Stats
        if (!safe) // if sleep is safe, no stat decay
        {
            if (timi.timeline < wakeTime)
                stats.passTime(wakeTime - timi.timeline);
            else
                stats.passTime((24 - timi.timeline) + wakeTime);
        }

        // set time to wakeTime
        timi.timeline = wakeTime;


        // roll mugging if not safe sleep
        if (!safe)
            RollMugging();

        yield return new WaitForSeconds(1);

        // fade screen back to visible
        FadeManager.Instance.Fade(false, 5);
        // play wakeup animation
        ani.SetTrigger("Wake");

        yield return new WaitForSeconds(3);

        // re-enable walking
        interaction.canMove = true;
        ani.SetBool("IsWalking", false);

        // display muggin anouncement if necessary
        if (mugged)
            MuggedAnnouncement();

        // reset both animation triggers
        ani.ResetTrigger("Wake");
        ani.ResetTrigger("Sleep");
        // reset both trigger bools
        drowsyFired = false;
        sleepFired = false;
        // reset mugged
        mugged = false;
    }
}
