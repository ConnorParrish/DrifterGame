using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitOnClick : MonoBehaviour {

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Quit);
    }

    void Quit()
    {
        Debug.Log("TRYING TO QUIT");
        Application.Quit();
    }
}
