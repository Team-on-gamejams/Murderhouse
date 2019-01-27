using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour {
	public byte questionNum;
	DialogManager dm;

	void Start () {
		dm = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();

		gameObject.GetComponent<Button>().onClick.AddListener(() => dm.ChooseQuestion(questionNum));
	}
}
