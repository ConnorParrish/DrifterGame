using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountDialog : MonoBehaviour {
    ItemData itemData;
    Text quantityText;
    Slider slider;
    Sprite sprite;
    Inventory currentInv;
    void Start()
    {
        sprite = transform.GetChild(1).GetComponent<Image>().sprite;
        slider = transform.GetChild(2).gameObject.GetComponent<Slider>();
        quantityText = slider.transform.GetChild(3).GetComponent<Text>();

        currentInv = transform.parent.parent.parent.GetChild(0).GetComponent<Inventory>();
        gameObject.SetActive(false);
    }

    public void OpenDialog(ItemData itemData)
    {
        this.itemData = itemData;
        sprite = transform.GetChild(1).GetComponent<Image>().sprite = itemData.item.Sprite;
        if (currentInv == Player.Instance.Inventory)
            slider.maxValue = itemData.amount;
        else
        {
            int maxPurchase = 0;
            for (int i = 0; i <= itemData.amount; i++)
            {
                if (currentInv.Money > Player.Instance.Inventory.lastSellPrice * i)
                    maxPurchase = i;
            }

            slider.maxValue = maxPurchase;

        }
        gameObject.SetActive(true);
    }

    public void DestroyStack()
    {
        currentInv.ChangeItemAmount(itemData.slotID, -Convert.ToInt32(quantityText.text));
        
        gameObject.SetActive(false);
    }

    public void BuyStack()
    {
        if (currentInv.Money > Player.Instance.Inventory.lastSellPrice * Convert.ToInt32(quantityText.text))
        {
            currentInv.ChangeItemAmount(itemData.slotID, -Convert.ToInt32(quantityText.text));
            Player.Instance.Inventory.ChangeItemAmount(Player.Instance.Inventory.ItemInInventoryCheck(itemData.item), -Convert.ToInt32(quantityText.text));
            Player.Instance.Inventory.AddMoney(Player.Instance.Inventory.lastSellPrice * Convert.ToInt32(quantityText.text));
            currentInv.AddMoney(-Player.Instance.Inventory.lastSellPrice * Convert.ToInt32(quantityText.text));

            transform.parent.parent.parent.parent.GetComponent<fullDialogue>().showDialogue("success");

        }

        gameObject.SetActive(false);
        //return Convert.ToInt32(quantityText.text);
    }

    public void CloseDialog()
    {
        gameObject.SetActive(false);
    }
	public void UpdateText()
    {
        quantityText.text = slider.value.ToString();
    }
}
