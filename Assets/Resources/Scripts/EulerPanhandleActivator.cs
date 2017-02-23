using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EulerPanhandleActivator : ActionItem {
    bool activated;
    public override void Interact()
    {
        if (!activated)
        {
//            Camera.main.GetComponent<CameraController>().PanhandleRotate(transform.rotation.eulerAngles);
            activated = true;
        }
        else
        {
            return;
        }
    }
}
