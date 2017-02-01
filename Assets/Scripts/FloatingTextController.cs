using UnityEngine;
using System.Collections;

public class FloatingTextController : MonoBehaviour {
	private static FloatingText popupText;
	public static void Initialize(){
		popupText = Resources.Load<FloatingText>("Prefabs/ItemPopupTextParent");
	}

	public static void CreateFloatingText(string text, Transform location){

	}
}
