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
    private Text itemValueText;
    private Vector3 itemModelPosition;
	private GameObject useButton;
    private GameObject buyButton;

    private Inventory currentInv;
    private Sprite nonFocusedSprite;
    private Sprite focusedSprite;

    void Start()
    {
        itemModelPrefab = transform.GetChild(0).GetChild(1).gameObject;
        itemModelPosition = transform.GetChild(0).GetChild(1).position;
        itemTitleText = transform.GetChild(3).GetComponent<Text>();
        itemTypeText = transform.GetChild(4).GetComponent<Text>();
        itemDescriptionText = transform.GetChild(5).GetComponent<Text>();
        itemValueText = transform.GetChild(6).GetComponent<Text>();
        
		currentInv = transform.parent.parent.GetChild(0).GetComponent<Inventory>();

		useButton = transform.GetChild(7).gameObject;
		useButton.SetActive(false);
        //buyButton = transform.GetChild(7).gameObject;
        //buyButton.SetActive(false);
        //gameObject.SetActive(false); 

        nonFocusedSprite = Resources.Load<Sprite>("Sprites/UI/Item Slot Graphic");
        focusedSprite = Resources.Load<Sprite>("Sprites/UI/Item Slot Graphic selected");

    }

    /// <summary>
    /// Changes the active item.
    /// </summary>
    /// <param name="item">Item.</param>
    public void ChangeActiveItem(ItemData itemData)
    {
        if (focusedItem != null)
            focusedItem.transform.parent.GetComponent<Image>().sprite = nonFocusedSprite;

        itemData.transform.parent.GetComponent<Image>().sprite = focusedSprite;


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

        if (currentInv.gameObject.name == "Inventory Manager")
        {
            if (itemData.item.Type == "Consumable")
                useButton.SetActive(true);
            else
                useButton.SetActive(false);

            itemValueText.text = itemData.item.Resale.ToString("$#0.00");
        }
        else
        {
            if (currentInv.transform.parent.parent.GetComponent<Merchant>().NPCData.ItemsForSale.Count == 0)
                itemValueText.text = itemData.item.Resale.ToString("$#0.00");
            else
            {
                foreach (Dictionary<string,float> itemInfo in currentInv.transform.parent.parent.GetComponent<Merchant>().NPCData.ItemsForSale)
                    Debug.Log(itemInfo["itemID"]);

                Dictionary<string, float> ifs = new Dictionary<string, float>();

                foreach (Dictionary<string, float> itemForSale in currentInv.transform.parent.parent.GetComponent<Merchant>().NPCData.ItemsForSale)
                    if (itemForSale["itemID"] == focusedItem.item.ID)
                        ifs = itemForSale;

                itemValueText.text = ifs["price"].ToString("$#0.00");

            }

            useButton.SetActive(true);

            

            //buyButton.SetActive(true);

        }
    }

	/// <summary>
	/// Changes the active item to an empty item.
	/// </summary>
    public void ChangeActiveItem() //doesnt seem to work yet
    {
        gameObject.SetActive(false);
        Destroy(itemModelPrefab);

        if (focusedItem != null)
            focusedItem.transform.parent.GetComponent<Image>().sprite = nonFocusedSprite;

    }

	public void UseItem()
	{
		//Debug.Break ();
		currentInv.UseItem (focusedItem.slotID);
		ChangeActiveItem ();
	}

    public void BuyItem()
    {
        if (currentInv.transform.parent.parent.GetComponent<Merchant>().NPCData.ItemsForSale.Count == 0)
        {
            Player.Instance.Inventory.SellItem(focusedItem.slotID, currentInv, focusedItem.item.Resale);
            //currentInv.RemoveItem(focusedItem.slotID)
            ChangeActiveItem();
        }
        else
        {
            if (Player.Instance.Inventory.Money >= focusedItem.item.Resale)
            {
                Dictionary<string, float> ifs = new Dictionary<string, float>();

                foreach (Dictionary<string, float> itemForSale in currentInv.transform.parent.parent.GetComponent<Merchant>().NPCData.ItemsForSale)
                    if (itemForSale["itemID"] == focusedItem.item.ID)
                        ifs = itemForSale;

                currentInv.SellItem(focusedItem.slotID, Player.Instance.Inventory, ifs["price"]);

                transform.parent.parent.parent.GetComponent<fullDialogue>().showDialogue("success");

                ChangeActiveItem();
            }
            else
            {
                transform.parent.parent.parent.GetComponent<fullDialogue>().showDialogue("notenough");
            }
            //Debug.Log(Player.Instance.Inventory.items[0]);

        }

    }
    
}
