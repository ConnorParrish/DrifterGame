using UnityEngine;
using System.Collections;

public class FloatingTextController : MonoBehaviour {
	public static FloatingText popupText;
    private static GameObject canvas;

	public static void Initialize()
    {
        canvas = GameObject.Find("InventoryNotificationCanvas");
        if (!popupText)
            popupText = Resources.Load<FloatingText>("Prefabs/UI/PopupText/ItemPopupTextParent");
	}

	public static void CreateFloatingText(string text, Transform location)
    {
        Debug.Log(text);
        FloatingText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.GetComponent<FloatingText>().SetText(text);
	}
}
