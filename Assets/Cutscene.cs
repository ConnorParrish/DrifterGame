using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {


    public GameObject playerPrefab;
    public GameObject maincamera;
    public GameObject Buscamera; 

    public void DropOffPlayer()
    {
        Instantiate(playerPrefab, transform);
        Buscamera.SetActive(false);
        maincamera.SetActive(true);

    }


    public void EndOfCutscene()

    {
        Destroy(gameObject);
        
    }

}
