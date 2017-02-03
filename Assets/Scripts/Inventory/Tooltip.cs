using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * This script creates dynamic tooltips based off the item's information
 **/

public class Tooltip : MonoBehaviour {
    private Item item;
    private string data;
    public GameObject tooltip;

    void Start()
    {
        tooltip = transform.parent.parent.parent.parent.GetChild(3).gameObject; // Why can't this find it with GameObject.Find("Tooltip")?
        //Debug.Log(tooltip.name);
        tooltip.SetActive(false);
    }

    void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;                   // Sets the corner of the tooltip to the mouse position
        }
    }
        
    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.GetComponent<CanvasGroup>().blocksRaycasts = false;
        tooltip.SetActive(true);

    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        data = "<color=#000000><b>" + item.Title + "</b></color>\n" + "<i>" + item.Type + "</i>\n\n" + item.Description + "";
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
