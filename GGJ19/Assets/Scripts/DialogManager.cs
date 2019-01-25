using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
	public Text answerText;
	public Button[] questionsButton;

	class DialogQuestion {
		public StatsHolder.Stat linkedStat;
		public string[] questions;
		public List<DialogAnswer> answers;

		public DialogQuestion(StatsHolder.Stat linkedStat, string[] questions, List<DialogAnswer> answers) {
			this.linkedStat = linkedStat;
			this.questions = questions;
			this.answers = answers;
		}

		public string GetAnswerInRange(int num) {
			List<DialogAnswer> fitted = new List<DialogAnswer>();
			foreach (var i in answers)
				if (i.minRange <= num && num <= i.maxRange)
					fitted.Add(i);

			return fitted[Random.Range(0, fitted.Count)].answer;
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

	//Тут всі діалоги.
	//TODO: загрузка з XML
	List<DialogQuestion> dialogs = new List<DialogQuestion>() {
		new DialogQuestion(
			StatsHolder.Stat.Cat,
			new string[]{
				"Cat1",
				"Cat2",
				"Cat3",
				"Cat4",
				"Cat5",
			},
			new List<DialogAnswer>(){
				new DialogAnswer(-100,  -50,    "-100, -50"),
				new DialogAnswer(-49,   -1,     "-49,  -1"),
				new DialogAnswer(0,     0,      "0"),
				new DialogAnswer(1,     50,     "1, 50"),
				new DialogAnswer(51,    100,    "51, 100"),
				new DialogAnswer(-100,    100,    "-100, 100"),
			}
		),

		new DialogQuestion(
			StatsHolder.Stat.Dog,
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
				new DialogAnswer(-100,    100,    "-100, 100"),
			}
		),

	};

	// 0 - вибраний діалог
	// 1 - вибране питання
	int[][] choosed;
	StatsHolder currentStats;

	public void StartDialog(StatsHolder statsHolder){
		currentStats = statsHolder;
		answerText.text = "Hello!\n";
		answerText.text += currentStats.ToString();

		FillQuestions();
	}

	public void ChooseQuestion(byte question) {
		answerText.text = dialogs[choosed[question][0]].GetAnswerInRange(currentStats.GetStatValue(dialogs[choosed[question][0]].linkedStat));
		answerText.text += currentStats.ToString();

		FillQuestions();
	}

	void FillQuestions(){
		choosed = new int[questionsButton.Length][];

		for (int i = 0; i < questionsButton.Length; ++i) {
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
}
