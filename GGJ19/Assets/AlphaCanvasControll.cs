using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaCanvasControll : MonoBehaviour {
	public float initAlpha = 0f;
	public float changeSpeed = 0.05f;
	public bool isChanging = false;
	public bool isGrow = false;

	CanvasGroup canvasGroup;

	void Start() {
		canvasGroup = GetComponent<CanvasGroup>();
		isGrow = initAlpha == 1 ? false : true;

		canvasGroup.interactable = false;
		canvasGroup.alpha = initAlpha;
	}

	void Update() {
		if(isChanging){
			if (isGrow) {
				canvasGroup.alpha += changeSpeed;
				if(canvasGroup.alpha == 1){
					canvasGroup.interactable = true;
					isChanging = false;
				}
			}
			else {
				canvasGroup.alpha -= changeSpeed;
				if (canvasGroup.alpha == 0)
					isChanging = false;
			}
		}
	}

	public void Show() {
		isChanging = true;
		isGrow = true;
	}

	public void Hide() {
		isChanging = true;
		isGrow = false;
		canvasGroup.interactable = false;
	}
}
