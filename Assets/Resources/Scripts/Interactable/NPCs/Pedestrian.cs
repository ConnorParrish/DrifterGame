using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : NPCInteraction { // see if animator work should be done here
    ParticleSystem resultPS;
    public int maxTimesBegged;
    public bool askingForHelp;

    private int timesBegged;
    private List<GameObject> cops;

	// Use this for initialization
	public override void Start () {
        if (maxTimesBegged == 0)
            maxTimesBegged = 2;

        cops = GameObject.Find("NPCs").GetComponent<NPCManager>().Cops;
        resultPS = transform.GetChild(2).GetComponent<ParticleSystem>();
        base.Start();
	}

    public override void Interact()
    {
        base.Interact();
 
        GetComponent<PedestrianWalker>().navAgent.Resume();
        GetComponentInChildren<Animator>().SetBool("isWalking", true);
    }

    /// <summary>
    /// This finds the closest cop for pedestrians in stress.
    /// </summary>
    /// <returns></returns>
    private GameObject GetClosestCop()
    {
        GameObject closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in cops)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestTarget = potentialTarget;
            }
        }

        return closestTarget;
    }

    /// <summary>
    /// Called when the player clicks a pedestrian while panhandling.
    /// </summary>
    /// <param name="inv"></param>
    public void OnPanhandleClick(Inventory inv)
    {

        if (timesBegged < maxTimesBegged)
        {
            int chance = Random.Range(0, 5); // Chance the pedestrian will give you $$$
            if (true)
            {
                if (chance < 3)
                {
                    Debug.Log("They cared enough");
                    sDialog.showDialogue("recieve");
                    resultPS.Play();

                    int change = Random.Range(1, 100); // The possible money you will receive in cents
                    Debug.Log("Money Before: " + inv.Money);
                    inv.AddMoney(change * (0.01f));
                    Debug.Log("Money After: " + inv.Money);
                }
                else
                {
                    Debug.Log("They didn't care");
                    sDialog.showDialogue("negative");
                }
                timesBegged++;
            }
        }
        else
        {
            Debug.Log("They're gonna call the cops!");
            askingForHelp = true; // Implement later to call cops
            sDialog.showDialogue("callHelp");
        }

        if (askingForHelp)
        {
            Debug.Log("Callin cops");
            GetClosestCop().GetComponent<Cop>().RunToPlayer(GameObject.FindWithTag("Player").transform);
        }

    }

}
