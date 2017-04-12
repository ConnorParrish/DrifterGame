using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {


    public GameObject player;
    public GameObject Actor; 
    public GameObject MainCam;


    private void Retarget(GameObject target)
    {
        MainCam.GetComponent<CameraController>().target = target.transform;
    }

    public void FadeIn(float duration)
    {
        GetComponent<FadeManager>().Fade(false, duration);
    }

    public void FadeOut(float duration)
    {
        GetComponent<FadeManager>().Fade(true, duration);
    }

    public void DropOffActor()
    {
        Actor.GetComponent<Animation>().enabled = true;
        Actor.GetComponentInChildren<Animator>().SetBool("IsCutscene", true);
    }

    public void PlacePlayer()
    {
        MainCam.GetComponent<CameraController>().enabled = true;
        Retarget(player);
        player.GetComponent<Transform>().position = new Vector3(66.89f, 0.46f, 9.17f);
        
        
    }

    public void EndOfCutscene()

    {
        Destroy(Actor);
        Destroy(gameObject);
    }

}
