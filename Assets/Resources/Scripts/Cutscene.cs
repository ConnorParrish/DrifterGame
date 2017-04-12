using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {


    public GameObject player;
    public GameObject MainCam;
    public GameObject Title;

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
 

    public void RetargetTitle()
    {
        Retarget(Title); 
    }

    public void RetargetBus()
    {
        Retarget(gameObject); 
    }

    public void DropOffPlayer()
    {
        player.transform.position = new Vector3(transform.position.x, transform.position.y + 6.4f, transform.position.z - 4.45f);
        Retarget(player);
    }

    public void EndOfCutscene()

    {
        Destroy(gameObject); 
    }

}
