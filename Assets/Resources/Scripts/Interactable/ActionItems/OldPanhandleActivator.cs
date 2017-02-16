using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OldPanhandleActivator : ActionItem {

    public override void Interact()
    {
        SceneManager.LoadScene(1);
    }
}
