using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreviewScript : MonoBehaviour {
    
    public MeshFilter itemMeshFilter; //temporarily public
    private Text itemTitleText;
    private Text itemTypeText; 
    private Text itemDescriptionText;

    void Start()
    {
        itemMeshFilter = transform.GetChild(0).GetChild(0).GetComponent<MeshFilter>();
        itemTitleText = transform.GetChild(3).GetComponent<Text>();
        itemTypeText = transform.GetChild(4).GetComponent<Text>();
        itemDescriptionText = transform.GetChild(5).GetComponent<Text>();
        //gameObject.SetActive(false);

    }

    public void ChangeActiveItem(Item item)
    {
        gameObject.SetActive(true);
        
        itemMeshFilter.mesh = Resources.Load<Mesh>("Models/Props/" + item.Slug); // replace with Resources.Load<Mesh>("Models/Props/" + item.Slug); once we have all items
        itemTitleText.text = item.Title;
        itemTypeText.text = item.Type;
        itemDescriptionText.text = item.Description;
    }
}
