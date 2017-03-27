using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreviewScript : MonoBehaviour {
    
    public GameObject itemModelPrefab; //temporarily public
    private Text itemTitleText;
    private Text itemTypeText; 
    private Text itemDescriptionText;
    private Vector3 itemModelPosition;
    void Start()
    {
        itemModelPrefab = transform.GetChild(0).GetChild(1).gameObject;
        itemModelPosition = transform.GetChild(0).GetChild(1).position;
        itemTitleText = transform.GetChild(3).GetComponent<Text>();
        itemTypeText = transform.GetChild(4).GetComponent<Text>();
        itemDescriptionText = transform.GetChild(5).GetComponent<Text>();
        //gameObject.SetActive(false);

    }

    public void ChangeActiveItem(Item item)
    {
        gameObject.SetActive(true);

        Destroy(itemModelPrefab);

        itemModelPrefab = Instantiate(Resources.Load<GameObject>("Prefabs/Items/" + item.Slug)); // replace with Resources.Load<Mesh>("Models/Props/" + item.Slug); once we have all items
        itemModelPrefab.transform.SetParent(transform.GetChild(0));
        itemModelPrefab.transform.position = itemModelPosition;
        itemModelPrefab.layer = 9;
        foreach (Transform piece in itemModelPrefab.GetComponentsInChildren<Transform>())
            piece.gameObject.layer = 9;
        itemTitleText.text = item.Title;
        itemTypeText.text = item.Type;
        itemDescriptionText.text = item.Description;
    }

    public void ChangeActiveItem() //doesnt seem to work yet
    {
        gameObject.SetActive(false);
        Destroy(itemModelPrefab);
        itemModelPrefab = Instantiate( new GameObject());
        itemTitleText.text = "";
        itemTypeText.text = "";
        itemDescriptionText.text = "";
    }
}
