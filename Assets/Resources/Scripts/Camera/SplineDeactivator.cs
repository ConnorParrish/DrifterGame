using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SplineDeactivator : MonoBehaviour
{
    GameObject PlayerHUD;

    void Start()
    {
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

        Player.Instance.WorldInteraction.canMove = true;

        
        
    }

    public void DeactivatePanhandling()
    {
        Player.Instance.PanhandlingScript.enabled = false;
        DeactivateSpline();
        transform.GetChild(0).gameObject.SetActive(false);

    }

    public void DeactivateMerchant()
    {
        GameObject merchantUI = GameObject.Find("Merchant Inventory"); // TODO this needs to change for having more merchants (buttons within each UI canvas?)
        merchantUI.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        PlayerHUD.SetActive(true);

        DeactivateSpline();
    }
}
