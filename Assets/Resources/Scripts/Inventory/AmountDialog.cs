using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AmountDialog : MonoBehaviour {
    ItemData itemData;
    Text quantityText;
    Slider slider;
    Inventory currentInv;
    void Start()
    {
        slider = transform.GetChild(2).gameObject.GetComponent<Slider>();
        quantityText = slider.transform.GetChild(3).GetComponent<Text>();

        currentInv = transform.parent.parent.parent.GetChild(0).GetComponent<Inventory>();
        gameObject.SetActive(false);
    }

    public void OpenDialog(ItemData itemData)
    {
        this.itemData = itemData;
        slider.maxValue = itemData.amount;

        gameObject.SetActive(true);
    }

    public void DestroyStack()
    {
        currentInv.ChangeItemAmount(itemData.slotID, -Convert.ToInt32(quantityText.text));

        if (currentInv == Player.Instance.Inventory)
            foreach (Inventory inv in GameObject.Find("NPC Manager").GetComponentsInChildren<Inventory>().Where(i => i.buyer))
                inv.ChangeItemAmount(inv.ItemInInventoryCheck(itemData.item), -Convert.ToInt32(quantityText.text));
        
        gameObject.SetActive(false);
    }

    public void BuyStack()
    {
        if (currentInv.Money > Player.Instance.Inventory.lastSellPrice * Convert.ToInt32(quantityText.text))
        {
            currentInv.ChangeItemAmount(itemData.slotID, -Convert.ToInt32(quantityText.text));

//            Player.Instance.Inventory.RemoveItem(Player.Instance.Inventory.ItemInInventoryCheck(itemData.item), -Convert.ToInt32(quantityText.text));


            Player.Instance.Inventory.ChangeItemAmount(Player.Instance.Inventory.ItemInInventoryCheck(itemData.item), -Convert.ToInt32(quantityText.text));
            Player.Instance.Inventory.AddMoney(Player.Instance.Inventory.lastSellPrice * Convert.ToInt32(quantityText.text));
            currentInv.AddMoney(Player.Instance.Inventory.lastSellPrice * Convert.ToInt32(quantityText.text)); //negate for buying stacks

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
