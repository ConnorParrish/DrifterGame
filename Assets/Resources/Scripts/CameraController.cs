using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;
    public AnimationClip CameraPan0;
    public AnimationClip CameraPan90;
    public AnimationClip CameraPan180;
    public AnimationClip CameraPan270;
    
    Animator anim;
    
    Vector3 offset;

    void Start()
    {
        anim = GetComponent<Animator>();
        offset = transform.position - target.position;
        anim.enabled = false;
        //GetComponent<SplineInterpolator>().enabled = false;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    public void PanhandleRotate(Vector3 activatorRotation)
    {
        if (activatorRotation.y == 0)
        {
            anim.SetTrigger("is0");
            anim.enabled = true;
        }
        else if (activatorRotation.y == 90)
        {
            anim.SetTrigger("is90");
            anim.enabled = true;
        }
        else if (activatorRotation.y == 180)
        {
            anim.SetTrigger("is180");
            anim.enabled = true;
        }
        else if (activatorRotation.y == 270)
        {
            anim.SetTrigger("is270");
            anim.enabled = true;
        }
    }

}
