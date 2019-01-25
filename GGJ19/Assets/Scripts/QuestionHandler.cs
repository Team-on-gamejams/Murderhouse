using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour {
	public byte questionNum;
	DialogManager dm;
	Text text;

	void Start () {
		text = GetComponent<Text>();
		dm = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();

		text.text = "";
	}

	void OnMouseDown() {
		dm.ChooseQuestion(questionNum);
		text.text = "";
	}
}
