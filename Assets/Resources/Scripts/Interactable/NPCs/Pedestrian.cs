using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : NPCInteraction {
    ParticleSystem resultPS;
    simpleDialogue simpleD;

	// Use this for initialization
	public override void Start () {
        resultPS = transform.GetChild(3).GetComponent<ParticleSystem>();
        simpleD = GetComponent<simpleDialogue>();
        base.Start();
	}

    /// <summary>
    /// Called when the player clicks a pedestrian while panhandling.
    /// </summary>
    /// <param name="inv"></param>
    public void OnPanhandleClick(Inventory inv)
    {
        if (!hasInteracted)
        {
            int chance = Random.Range(0, 5); // Chance the pedestrian will give you $$$

            if (chance < 3)
            {
                Debug.Log("They cared enough");
                resultPS.Play();

                int change = Random.Range(1, 100); // The possible money you will receive in cents
                Debug.Log("Money Before: " + inv.Money);
                inv.AddMoney(change * (0.01f));
                Debug.Log("Money After: " + inv.Money);
            }
            else
            {
                Debug.Log("They didn't care");
                simpleD.showDialogue();
            }
            //hasInteracted = true; // Remembers the player has asked for money
        }
        else
        {
            //askingForHelp = true; // Implement later to call cops
        }

    }
}
