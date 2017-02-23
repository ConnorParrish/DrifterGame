using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target;
    public float smoothing = 5f;

    Material opaqueMat;
    Material transparentMat;

    Vector3 screenPos;
    Vector3 offset;

    List<GameObject> GOInTheWay = new List<GameObject>();

    void Start()
    {
        offset = transform.position - target.position;
        opaqueMat = new Material(Resources.Load<Material>("Materials/DebugOpaque"));
        transparentMat = new Material(Resources.Load<Material>("Materials/DebugTransparent"));
    } 

    void Update()
    {
        RaycastHit hit;
        screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.yellow);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject go = hit.collider.gameObject;
            if (go.tag == "Building")
            {
                GOInTheWay.Add(go);
                if (go.tag == "Building")
                {
                    foreach (Renderer renderer in go.GetComponentsInChildren<Renderer>())
                        renderer.material = transparentMat;
                }
                else
                {
                    foreach (Renderer renderer in go.GetComponentsInChildren<Renderer>())
                        renderer.material = opaqueMat;
                }
            }
            else
            {
                if (GOInTheWay != null)
                {
                    foreach (GameObject goo in GOInTheWay)
                    {
                        if (goo.tag == "Building")
                        {
                            foreach (Renderer renderer in goo.GetComponentsInChildren<Renderer>())
                                renderer.material = opaqueMat;
                        }
                    }
                }
            }
        }
    }

    void LateUpdate()
    {
        
        

        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        //transform.position = targetCamPos;
    }

}
