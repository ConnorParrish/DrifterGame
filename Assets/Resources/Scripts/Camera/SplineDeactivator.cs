using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SplineDeactivator : MonoBehaviour
{
    GameObject player;
    GameObject PlayerHUD;

    void Start()
    {
        player = GameObject.Find("Player");
        PlayerHUD = GameObject.Find("General UI Canvas");
        Debug.Log("Hi");
    }
    private void DeactivateSpline()
    {
        Camera.main.GetComponent<SplineInterpolator>().mState = "Once";
        Camera.main.GetComponent<SplineInterpolator>().ended = true;
        Camera.main.GetComponent<SplineInterpolator>().mCurrentIdx++;

        GameObject rootHolder = Camera.main.GetComponent<SplineController>().SplineRootHolder.gameObject;
        rootHolder.transform.GetChild(0).gameObject.SetActive(true);

        for (int i = 0; i < rootHolder.transform.GetChild(0).childCount; i++)
        {
            rootHolder.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
        }

        player.GetComponent<WorldInteraction>().canMove = true;

        gameObject.SetActive(false);
        
    }

    public void DeactivatePanhandling()
    {
        player.GetComponent<PanhandlingScript>().enabled = false;
        DeactivateSpline();

        //transform.parent.GetChild(1).gameObject.SetActive(false);
    }

    public void DeactivateMerchant()
    {
        GameObject merchantUI = GameObject.Find("Expanded Buy_Sell");
        merchantUI.SetActive(false);

        PlayerHUD.SetActive(true);

        DeactivateSpline();
    }
}
