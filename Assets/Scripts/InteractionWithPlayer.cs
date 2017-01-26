using UnityEngine;
using System.Collections;

public class InteractionWithPlayer : MonoBehaviour {
	public Sprite successSprite;
	public Sprite failedSprite;
	public bool hasInteracted; // Determines if the player has bothered them
	public bool askingForHelp; // Use this to call the cops!

	private SpriteRenderer resultSR;
	private ParticleSystem resultPS;

	// Use this for initialization
	void Start () {
		resultSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
		resultPS = GetComponent<ParticleSystem>();
	}
	

	// Update is called once per frame
	void Update () {
	
	}

	public void OnPanhandleClick(PlayerInventory inventory){
		if (!hasInteracted){
			int chance = Random.Range(0,5); // Chance the pedestrian will give you $$$

			if ( chance > 3){
				Debug.Log("They cared enough");
				resultSR.sprite = successSprite;
				resultSR.enabled = true;
				resultPS.Play();
				
				int change = Random.Range(1,100); // The possible money you will receive in cents
				Debug.Log("Money Before: " + inventory.money);
				inventory.money += change*(0.01f);
				Debug.Log("Money After: " + inventory.money);
			} else {
				Debug.Log("They didn't care");
				resultSR.sprite = failedSprite;
				resultSR.enabled = true;
			}
			hasInteracted = true; // Remembers the player has asked for money
		} else {
			askingForHelp = true; // Implement later to call cops
		}
	}
}
