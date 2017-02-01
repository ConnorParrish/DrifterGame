using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanhandlingUIManager : MonoBehaviour {
	private GameObject Player;
	private PanhandlingScript panhandlingScript;
	private Inventory playerInventory;
	private GameObject moneyUI;
	private GameObject begUI;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindWithTag("Player");
		panhandlingScript = Player.GetComponent<PanhandlingScript>();
		playerInventory = Player.GetComponent<Inventory>();
		moneyUI = transform.GetChild(0).GetChild(0).gameObject;
		begUI = transform.GetChild(1).GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		moneyUI.GetComponent<Text>().text = playerInventory.Money.ToString();
		begUI.GetComponent<Text>().text = panhandlingScript.begsRemaining.ToString();
	}
}
