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

   public void FadeIn(int duration)
   {
        GetComponent<FadeManager>().Fade(false, duration);
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
        Retarget(player);
        player.transform.position = new Vector3 (transform.position.x, transform.position.y + 6.4f, transform.position.z - 4.45f);
    }

    public void EndOfCutscene()

    {
        Destroy(gameObject); 
    }

}
