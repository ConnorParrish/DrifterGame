using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {


    public GameObject player;
    public GameObject MainCam;
    public GameObject UI; 
    private fullDialogue fDialog;

    public void Start()
    {
        enabled = true;
        fDialog = GetComponent<fullDialogue>();
        MainCam.GetComponent<CameraController>().enabled = false;
        GetComponent<Animator>().SetBool("Is Cutscene", true); 
        UI.transform.GetChild(0).gameObject.SetActive(false);
        UI.transform.GetChild(1).gameObject.SetActive(false);
        UI.transform.GetChild(2).gameObject.SetActive(false);
        UI.transform.GetChild(3).gameObject.SetActive(false);
        UI.transform.GetChild(4).gameObject.SetActive(true);
        UI.transform.GetChild(5).gameObject.SetActive(false);
        player.SetActive(false);

    }

    void update()
    {
        if (Input.anyKey)

            SkipCutscene(); 
    }

    public void ShowCustomCutsceneDialogue(string message)
    {
        fDialog.showCustomDialogue(message);
    }


    public void FadeIn(float duration)
    {
        GetComponent<FadeManager>().Fade(false, duration);
    }

    public void FadeOut(float duration)
    {
        GetComponent<FadeManager>().Fade(true, duration);
    }


    public void PlacePlayer()
    {
        player.SetActive(true);
        MainCam.GetComponent<Animation>().enabled = false;
        
    }

    public void PlaceGameObject(GameObject Object, Vector3 NewLoc)
    {
        Object.GetComponent<Transform>().position = NewLoc; 
    }

    public void ActivatePause()
    {
        UI.transform.GetChild(5).gameObject.SetActive(true);
        MainCam.GetComponent<CameraController>().target = player.transform;
        MainCam.GetComponent<CameraController>().enabled = true;
        MainCam.GetComponent<CameraController>().offset = new Vector3(0, 12.57099f, 9.193005f);
    }

    public void DeactivatePause()
    {
        UI.transform.GetChild(5).gameObject.GetComponentInChildren<Animator>().enabled = false; 
        UI.transform.GetChild(5).gameObject.SetActive(false);
        
    }

    public void SkipCutscene()
    {
        GetComponent<Animator>().SetBool("Is Cutscene", false);
        MainCam.GetComponent<CameraController>().target = player.transform;
        MainCam.GetComponent<CameraController>().enabled = true;
        MainCam.GetComponent<Animation>().enabled = false;
        MainCam.GetComponent<CameraController>().offset = new Vector3(0, 12.57099f, 9.193005f);


        UI.transform.GetChild(2).gameObject.SetActive(true);
        UI.transform.GetChild(3).gameObject.SetActive(true);
        UI.transform.GetChild(4).gameObject.SetActive(false);
        UI.transform.GetChild(5).gameObject.SetActive(false);
    }

    public void EndOfCutscene()

    {
        UI.transform.GetChild(5).gameObject.SetActive(false); 
        UI.transform.GetChild(2).gameObject.SetActive(true);
        UI.transform.GetChild(3).gameObject.SetActive(true);
        Destroy(gameObject);
        

    }

}
