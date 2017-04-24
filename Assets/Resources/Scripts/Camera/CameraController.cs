﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target;
    [SerializeField]
    private float smoothing = 5f;

    Material opaqueMat;
    Material transparentMat;

    Vector3 screenPos;
    public Vector3 offset;

    List<GameObject> GOInTheWay = new List<GameObject>();
    
    void OnEnable()
    {
        offset = transform.position - target.position;
    }
    
    void Start()
    {
        opaqueMat = new Material(Resources.Load<Material>("Materials/DebugOpaque"));
        transparentMat = new Material(Resources.Load<Material>("Materials/DebugTransparent"));
    } 

    void Update()
    {
        RaycastHit hit;
        screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.yellow);

        if (false)//Physics.Raycast(ray, out hit))
        {
            GameObject go = hit.collider.gameObject;
            if (go.layer == 11) //Buildings is Layer 11
            {
                Debug.Log("toggling xray");
                Player.Instance.Inventory.ToggleXRay(true);
                /*GOInTheWay.Add(go);
                if (go.tag == "Building")
                {
                    foreach (Renderer renderer in go.GetComponentsInChildren<Renderer>())
                        for (int i = 0; i < renderer.materials.Length; i++)
                            renderer.materials[i] = transparentMat;
                }
                else
                {
                    foreach (Renderer renderer in go.GetComponentsInChildren<Renderer>())
                        renderer.material = opaqueMat;
                }*/
            }
            else
            {
                Debug.Log("shouldd siabled sv");
                Player.Instance.Inventory.ToggleXRay(false);
                /*
                if (GOInTheWay != null)
                {
                    for (int i = 0; i < GOInTheWay.Count; i++)
                    {
                        if(GOInTheWay[i].tag == "Building")
                        {
                            foreach (Renderer renderer in GOInTheWay[i].GetComponentsInChildren<Renderer>())
                                renderer.material = opaqueMat;
                            GOInTheWay.RemoveAt(i);
                        }
                    }
                }*/
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
