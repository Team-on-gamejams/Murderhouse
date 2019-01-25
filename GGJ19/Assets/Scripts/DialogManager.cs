using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
	public Text answerText;
	public Button[] questionsButton;

	enum Stat : byte {
		None,
		Cats,
		Dogs
	}

	class DialogQuestion {
		Stat linkedStat;
		public string[] questions;
		public List<DialogAnswer> answers;

		public DialogQuestion(Stat linkedStat, string[] questions, List<DialogAnswer> answers) {
			this.linkedStat = linkedStat;
			this.questions = questions;
			this.answers = answers;
		}

		public string GetAnswerInRange(int num){
			return answers[0].answer;
		}
	}

	class DialogAnswer {
		public sbyte minRange, maxRange;
		public string answer;

		public DialogAnswer(sbyte min, sbyte max, string answer) {
			minRange = min;
			maxRange = max;
			this.answer = answer;
		}
	}

	List<DialogQuestion> dialogs = new List<DialogQuestion>() {
		new DialogQuestion(
			Stat.Cats, 
			new string[]{ 
				"Cat1",
				"Cat2",
				"Cat3",
				"Cat4",
				"Cat5",
			}, 
			new List<DialogAnswer>(){
				new DialogAnswer(-100,	-50,    "-100, -50"),
				new DialogAnswer(-49,	-1,     "-49,  -1"),
				new DialogAnswer(0,		0,		"0"),
				new DialogAnswer(1,		50,		"1, 50"),
				new DialogAnswer(51,	100,	"51, 100"),
			}
		),

		new DialogQuestion(
			Stat.Dogs,
			new string[]{
				"Dog1",
				"Dog2",
				"Dog3",
				"Dog4",
				"Dog5",
			},
			new List<DialogAnswer>(){
				new DialogAnswer(-100,  -50,    "-100, -50"),
				new DialogAnswer(-49,   -1,     "-49,  -1"),
				new DialogAnswer(0,     0,      "0"),
				new DialogAnswer(1,     50,     "1, 50"),
				new DialogAnswer(51,    100,    "51, 100"),
			}
		),

	};

	// 0 - вибраний діалог
	// 1 - вибране питання
	int[][] choosed;

	void Start() {
		answerText.text = "Hello!";

		choosed = new int[questionsButton.Length][];

		for(int i = 0; i < questionsButton.Length; ++i){
			choosed[i] = new int[2];

			REPEAT_RANDOM:
			choosed[i][0] = Random.Range(0, dialogs.Count);
			choosed[i][1] = Random.Range(0, dialogs[choosed[i][0]].questions.Length);
			for (int j = 0; j < i; ++j)
				if (choosed[i][0] == choosed[j][0] && choosed[i][1] == choosed[j][1])
					goto REPEAT_RANDOM;

			questionsButton[i].GetComponentInChildren<Text>().text = dialogs[choosed[i][0]].questions[choosed[i][1]];
		}
	}

	void Update() {
		
	}

	public void ChooseQuestion(byte question){
		Debug.Log(choosed[question][0]);
		Debug.Log(choosed[question][1]);
		answerText.text = dialogs[choosed[question][0]].GetAnswerInRange(10);
	}

}
