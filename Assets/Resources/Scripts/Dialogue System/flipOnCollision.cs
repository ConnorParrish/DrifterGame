using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// simple script whose only purpose is to flip the floating dioalogue bubble when it collides with something
/// </summary>
public class flipOnCollision : MonoBehaviour {
    private bool flipped = false;

    void OnTriggerEnter(Collider c)
    {
        if (!flipped)
        {
            flipped = true;
            gameObject.GetComponent<SimpleFollow>().offset.x = -gameObject.GetComponent<SimpleFollow>().offset.x;
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(1f, 0f);
        }
    }
}
