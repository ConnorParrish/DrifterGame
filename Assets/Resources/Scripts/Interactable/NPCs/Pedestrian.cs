using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : NPCInteraction { // see if animator work should be done here
    ParticleSystem resultPS;
    public int maxTimesBegged;
    public bool askingForHelp;

    private bool canAsk = true;

    private int timesBegged;
    private List<GameObject> cops;

	// Use this for initialization
	public override void Start () {
        if (maxTimesBegged == 0)
            maxTimesBegged = 2;

        cops = GameObject.Find("NPC Manager").GetComponent<NPCManager>().Cops;
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
    public void OnPanhandleClick()
    {

        if (timesBegged < maxTimesBegged)
        {
            int chance = Random.Range(0, 5); // Chance the pedestrian will give you $$$
            if (canAsk)
            {
                if (chance < 3)
                {
                    //Debug.Log("They cared enough");
                    sDialog.showDialogue("recieve");
                    resultPS.Play();

                    int change = Random.Range(100, 1000); // The possible money you will receive in cents
                    //Debug.Log("Money Before: " + Player.Instance.Inventory.Money);
                    Player.Instance.Inventory.AddMoney(change * (0.01f));
                    //Debug.Log("Money After: " + Player.Instance.Inventory.Money);

                    // set the delay before can ask again
                    canAsk = false;
                    StartCoroutine(waitToAskAgain(4));
                }
                else
                {
                    //Debug.Log("They didn't care");
                    sDialog.showDialogue("negative");
                }
                //timesBegged++;
            }
        }
        else
        {
            //Debug.Log("They're gonna call the cops!");
            //askingForHelp = true; // Implement later to call cops
            //sDialog.showDialogue("callHelp");
        }

        if (askingForHelp)
        {
            //Debug.Log("Callin cops");
            //GetClosestCop().GetComponent<Cop>().RunToPlayer(Player.Instance.transform);
        }

    }

    IEnumerator waitToAskAgain(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        canAsk = true;
    }

}
