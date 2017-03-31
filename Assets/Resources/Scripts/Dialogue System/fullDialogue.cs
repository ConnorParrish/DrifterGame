using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fullDialogue : MonoBehaviour
{

    /// <summary>
    /// This component is designed to support full dialogue for NPC's. Make sure you enter the data about the NPC containing
    /// this component into the NPC Json file. 
    /// </summary>

    public GameObject Dialogue;                  // link this to the fullDialogue prefab
    public int messageSpeed = 10;                // scale from 1-10 about how fast you want the message to write

    public GameObject canvas;
    Text text;
    GameObject image;
	public NPC NPCData;
    Inventory inv;
    GameObject PlayerHUD;

    string currentText;
    int currentItem;
    float currentCost;
    IEnumerator currentCoroutine;
	IEnumerator messageCycler;

    void Start()
    {
        PlayerHUD = GameObject.Find("General UI Canvas");

        // fetch a couple components and game objects for future use
        canvas = GameObject.Instantiate(Dialogue);
        canvas.transform.SetParent(transform);
        text = canvas.transform.FindChild("Image").gameObject.GetComponentInChildren<Text>();
        image = canvas.transform.FindChild("Image").gameObject;
        Button button = image.GetComponent<Button>();
        button.onClick.AddListener(showNextMessage);
        // set the canvas to false because we don't need it yet
        canvas.SetActive(false);

        // this is just to avoid a null reference error, ignore it.
        currentCoroutine = writeMessage();

        // get a reference to the inventory for adding items
        inv = GameObject.Find("General UI Canvas").GetComponentInChildren<Inventory>();

    }

	public void endDialogue()
    {
        // sort of a placeholder right now
        StartCoroutine(shrinkCanvas());
        canvas.transform.FindChild("Decline").gameObject.SetActive(false);
        canvas.transform.FindChild("Accept").gameObject.SetActive(false);
    }

    private void showNextMessage()
    {
        // code up here will cycle through the message set
		messageCycler.MoveNext();

        // stop the old coroutine if it has not finished
        StopCoroutine(currentCoroutine);

        // this section is for creating the next frame
        currentCoroutine = writeMessage();

        // starts typing the current message
        StartCoroutine(currentCoroutine);
    }

	public void showDialogue()
    {
        canvas.SetActive(true);
		messageCycler = cycleMessages ();
        StartCoroutine(growCanvas());
    }

    public void showDialogue(string tag)
    {
        canvas.SetActive(true);
        messageCycler = cycleMessages(tag);
        StartCoroutine(growCanvas());
    }

    public void OpenMerchantUI()
    {
        setButtonState(false, "merchant");
        gameObject.GetComponent<Merchant>().Spline();
//        GameObject.Find("General UI Canvas").SetActive(false);
    }

    public void sellOrBuyItem() // linked to "yes" button for selling or buying items in dialogue
    {
        setButtonState(false);
        if (currentCost >= 0) // if the current cost is positive, buy the item
        {
            if (inv.Money - currentCost < 0)
            {
                currentText = "(You rifle through your pockets, and sadly realize you can't afford it)";
                StopCoroutine(currentCoroutine);
                currentCoroutine = writeMessage();
                StartCoroutine(currentCoroutine);
            }
            else
            {
                inv.AddItem(currentItem);
                inv.AddMoney(-currentCost);
                endDialogue();
            }
        }
        else // otherwise, we are selling an item to the NPC
        {
            if (inv.ItemInInventoryCheck(new Item() { ID = currentItem }))
            {
                inv.AddMoney(-currentCost);
                inv.RemoveItem(currentItem);
                endDialogue();
            }
            else
            {
                currentText = "(You dig through your backpack and sadly realize you don't have what they want...)";
                StopCoroutine(currentCoroutine);
                currentCoroutine = writeMessage();
                StartCoroutine(currentCoroutine);
            }
        }
    }


    IEnumerator writeMessage()
    {
        text.text = "";
        char[] c = currentText.ToCharArray();
        foreach (char t in c)
        {
            text.text += t;
            if (t == '.')
                yield return new WaitForSeconds(.3f / messageSpeed);
            else
                yield return new WaitForSeconds(.1f / messageSpeed);
        }
    }

    IEnumerator growCanvas()
    {
        float waitTime = .3f;
        float endScale = .9f;
        float currentScale;
        float timePassed = 0f;
        RectTransform t = image.GetComponent<RectTransform>();

        // grow the scale of image game object which contains the text game object
        while (waitTime > timePassed)
        {
            timePassed += Time.deltaTime;
            currentScale = endScale / (waitTime / timePassed);
            t.localScale = new Vector3(currentScale, currentScale, 0f);
            yield return new WaitForSeconds(.01f);
        }

		// start writing the first message when this finishes
		showNextMessage();
    }

    IEnumerator shrinkCanvas()
    {
        currentText = "";
        float waitTime = .3f;
        float startScale = .9f;
        float currentScale;
        float timePassed = 0f;
        RectTransform t = image.GetComponent<RectTransform>();

        // shrink the scale of image game object which contains the text game object
        while (waitTime > timePassed)
        {
            timePassed += Time.deltaTime;
            currentScale = startScale / (waitTime / (waitTime - timePassed));
            t.localScale = new Vector3(currentScale, currentScale, 0f);
            yield return new WaitForSeconds(.01f);
        }

        canvas.SetActive(false);
    }

	IEnumerator cycleMessages(){
		List<Dictionary<string, string>> messages = NPCData.DialogueFrames;
		foreach (Dictionary<string, string> d in messages) {
			currentText = d ["text"];
            if (Convert.ToInt32(d["itemID"]) != -1) // if the dialogue frame contains an item
            {
                setButtonState(true);
                currentCost = float.Parse(d["cost"]);
                currentItem = Convert.ToInt32(d["itemID"]);
            }
			yield return null;
		}

        endDialogue();
	}

    IEnumerator cycleMessages(string tag) // try to bring this and cycleMessages() closer together
    {
        List<Dictionary<string, string>> messages = NPCData.DialogueFrames;
        foreach (Dictionary<string,string> d in messages)
        {
            if (d["tag"] == tag)
            {
                currentText = d["text"];
                if (Convert.ToInt32(d["itemID"]) != -1)
                {
                    setButtonState(true);
                    currentCost = float.Parse(d["cost"]);
                    currentItem = Convert.ToInt32(d["itemID"]);
                }
                else if(NPCData.Type == "merchant")
                {
                    setButtonState(true, "merchant");
                }
                yield return null;
            }
        }
        endDialogue();
    }

    private void setButtonState(bool state)
    {
        canvas.transform.FindChild("Decline").gameObject.GetComponent<Button>().onClick.AddListener(endDialogue);
        canvas.transform.FindChild("Decline").gameObject.SetActive(state);
        canvas.transform.FindChild("Accept").gameObject.GetComponent<Button>().onClick.AddListener(sellOrBuyItem);
        canvas.transform.FindChild("Accept").gameObject.SetActive(state);
    }

    private void setButtonState(bool state, string type)
    {
        canvas.transform.FindChild("Decline").gameObject.GetComponent<Button>().onClick.AddListener(endDialogue);
        canvas.transform.FindChild("Decline").gameObject.SetActive(state);
        if (type == "merchant")
            canvas.transform.FindChild("Accept").gameObject.GetComponent<Button>().onClick.AddListener(OpenMerchantUI);
        canvas.transform.FindChild("Accept").gameObject.SetActive(state);
    }

}
