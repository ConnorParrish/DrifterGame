using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreviewScript : MonoBehaviour {
    
    public GameObject itemInfoPanel;

    private Inventory inv;
    private ItemDatabase itemDatabase;

    private MeshFilter itemMeshRenderer;
    private Text itemTitleText;
    private Text itemTypeText;
    private Text itemDescriptionText;
    private Item item;

    void Start()
    {
        itemInfoPanel = gameObject.transform.GetChild(2).gameObject;
        itemMeshRenderer = transform.GetChild(1).GetChild(0).GetComponent<MeshFilter>();
        itemTitleText = itemInfoPanel.transform.GetChild(0).GetComponent<Text>();
        itemTypeText = itemInfoPanel.transform.GetChild(1).GetComponent<Text>();
        itemDescriptionText = itemInfoPanel.transform.GetChild(2).GetComponent<Text>();

        inv = GameObject.Find("Inventory Manager").GetComponent<Inventory>();
        itemDatabase = inv.gameObject.GetComponent<ItemDatabase>();
        itemInfoPanel.SetActive(false);
    }

    public void ChangeActiveItem(int id)
    {
        item = itemDatabase.FetchItemByID(id);
        itemMeshRenderer.mesh = Resources.Load<Mesh>("Models/beerbottle");
        itemTitleText.text = item.Title;
        itemTypeText.text = item.Type;
        itemDescriptionText.text = item.Description;
    }
}
