using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanhandleDeactivator : MonoBehaviour
{
    public void DeactivatePanhandling()
    {
        Camera.main.GetComponent<SplineInterpolator>().mState = "Once";
        Camera.main.GetComponent<SplineInterpolator>().ended = true;
        Camera.main.GetComponent<SplineInterpolator>().mCurrentIdx++;
    }
}
