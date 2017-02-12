using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {
	public Animator animator;
	private Text infoText;

    void OnEnable(){
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
		Destroy(gameObject, clipInfo[0].clip.length);

		infoText= animator.GetComponent<Text>();
	}

	public void SetText(string text){
		infoText.text = text;
	}
}
