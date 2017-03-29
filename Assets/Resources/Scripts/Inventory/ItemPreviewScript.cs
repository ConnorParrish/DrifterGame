using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreviewScript : MonoBehaviour {
    
    public GameObject itemModelPrefab; //temporarily public

	private ItemData focusedItem;
    private Text itemTitleText;
    private Text itemTypeText; 
    private Text itemDescriptionText;
    private Vector3 itemModelPosition;
	private GameObject useButton;

	private Inventory inv;

	void Start()
    {
        itemModelPrefab = transform.GetChild(0).GetChild(1).gameObject;
        itemModelPosition = transform.GetChild(0).GetChild(1).position;
        itemTitleText = transform.GetChild(3).GetComponent<Text>();
        itemTypeText = transform.GetChild(4).GetComponent<Text>();
        itemDescriptionText = transform.GetChild(5).GetComponent<Text>();
        
		inv = GameObject.Find ("Inventory Manager").GetComponent<Inventory>();

		useButton = transform.GetChild(6).gameObject;
		useButton.SetActive (false);
		//gameObject.SetActive(false); 

    }

	/// <summary>
	/// Changes the active item.
	/// </summary>
	/// <param name="item">Item.</param>
    public void ChangeActiveItem(ItemData itemData)
    {
        gameObject.SetActive(true);

		focusedItem = itemData;

        Destroy(itemModelPrefab);

        itemModelPrefab = Instantiate(Resources.Load<GameObject>("Prefabs/Items/" + itemData.item.Slug)); // replace with Resources.Load<Mesh>("Models/Props/" + itemData.item.Slug); once we have all items
        itemModelPrefab.transform.SetParent(transform.GetChild(0));
        itemModelPrefab.transform.position = itemModelPosition;
        itemModelPrefab.layer = 9;
        foreach (Transform piece in itemModelPrefab.GetComponentsInChildren<Transform>())
            piece.gameObject.layer = 9;
        
		itemTitleText.text = itemData.item.Title;
        itemTypeText.text = itemData.item.Type;
        itemDescriptionText.text = itemData.item.Description;

		if (itemData.item.Type == "Consumable")
			useButton.SetActive (true);
		else
			useButton.SetActive (false);
    }

	/// <summary>
	/// Changes the active item to an empty item.
	/// </summary>
    public void ChangeActiveItem() //doesnt seem to work yet
    {
        gameObject.SetActive(false);
        Destroy(itemModelPrefab);
        itemModelPrefab = Instantiate( new GameObject());
        itemTitleText.text = "";
        itemTypeText.text = "";
        itemDescriptionText.text = "";

		useButton.SetActive (false);

    }

	public void UseItem()
	{
		//Debug.Break ();
		inv.UseItem (focusedItem.slotID);
		ChangeActiveItem ();
	}
}
