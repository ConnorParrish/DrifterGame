using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanhandleDeactivator : MonoBehaviour
{
    public void DeactivatePanhandling()
    {
        GameObject panhandlingActivatorGO = Camera.main.GetComponent<SplineController>().SplineRootHolder.transform.parent.gameObject;
        Camera.main.GetComponent<SplineInterpolator>().mState = "Once";
        Camera.main.GetComponent<SplineInterpolator>().ended = true;
        Camera.main.GetComponent<SplineInterpolator>().mCurrentIdx++;

        panhandlingActivatorGO.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

        for (int i = 0; i < panhandlingActivatorGO.transform.GetChild(0).GetChild(0).childCount; i++)
        {
            panhandlingActivatorGO.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(true);
        }
        panhandlingActivatorGO.SetActive(true);

    }
}
