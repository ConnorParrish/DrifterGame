using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {


    public GameObject player;
    public GameObject Actor; 
    public GameObject MainCam;
    private bool BusStop; 

    public void update()
    {
        if (BusStop == true)
        {
            player.transform.Translate(0,-.1f,-.1f); 
        }
        else if(BusStop ==true&& player.transform.position.y == 0)
        {
            BusStop = false;
            player.GetComponent<Animator>().SetBool("cutscene", false);
        }
    }

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
        BusStop = true;
        Actor.GetComponentInChildren<Animator>().SetBool("IsCutscene", true);
    }

    public void PlacePlayer()
    {
       
        PlaceGameObject(player, new Vector3(66.89f, 0.46f, 9.17f));
        MainCam.GetComponent<CameraController>().enabled = true;
        Retarget(player);
        PlaceGameObject(MainCam, new Vector3(66.89f, 8.98f, 22.18f));



    }
    public void PlaceGameObject(GameObject Object, Vector3 NewLoc)
    {
        Object.GetComponent<Transform>().position = NewLoc; 
    }

    public void EndOfCutscene()

    {
        Destroy(Actor);
        Destroy(gameObject);
    }

}
