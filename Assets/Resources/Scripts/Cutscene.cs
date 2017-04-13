using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {


    public GameObject player;
    public GameObject MainCam;
    private fullDialogue fDialog;

    public void Start()
    {
        fDialog = GetComponent<fullDialogue>();
        MainCam.GetComponent<CameraController>().enabled = false;
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
        MainCam.GetComponent<CameraController>().target = player.transform;
        MainCam.GetComponent<CameraController>().enabled = true;
        
        

    }
    public void PlaceGameObject(GameObject Object, Vector3 NewLoc)
    {
        Object.GetComponent<Transform>().position = NewLoc; 
    }

    public void EndOfCutscene()

    {
        Destroy(gameObject);
    }

}
