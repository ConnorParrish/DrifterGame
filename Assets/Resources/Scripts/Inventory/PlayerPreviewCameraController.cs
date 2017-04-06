using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreviewCameraController : MonoBehaviour {
    public bool rotatingLeft;
    public bool rotatingRight;
    private Transform pivot;

    void Start()
    {
        pivot = transform.parent;
    }
    void Update()
    {
        if (rotatingLeft)
            rotateLeft();
        else if (rotatingRight)
            rotateRight();
    }

    public void EnableRightRotation()
    {
        rotatingRight = true;
    }

    public void EnableLeftRotation()
    {
        rotatingLeft = true;
    }

    public void StopRotation()
    {
        rotatingLeft = false;
        rotatingRight = false;
    }

    private void rotateRight()
    {
        Debug.Log("Rotating Left!!!!");
        pivot.Rotate(new Vector3(0, 2, 0));
    }
    private void rotateLeft()
    {
        pivot.Rotate(new Vector3(0, -2, 0));
    }
}
