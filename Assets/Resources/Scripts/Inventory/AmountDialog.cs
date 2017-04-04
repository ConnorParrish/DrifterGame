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
        Debug.Log("amount" + itemData.amount);
        Debug.Log("slider.name " + slider.name);
        sprite = transform.GetChild(1).GetComponent<Image>().sprite = itemData.item.Sprite;
        slider.maxValue = itemData.amount;
        gameObject.SetActive(true);
    }

    public void DestroyStack()
    {
        Debug.Log("quantity text: " + Convert.ToInt32(quantityText.text));
        currentInv.ChangeItemAmount(itemData.slotID, Convert.ToInt32(quantityText.text));
        
        gameObject.SetActive(false);
    }

    public void BuyStack()
    {
        currentInv.ChangeItemAmount(itemData.slotID, -Convert.ToInt32(quantityText.text));
        Player.Instance.Inventory.ChangeItemAmount(itemData.slotID, -Convert.ToInt32(quantityText.text));
        gameObject.SetActive(false);
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
