using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
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

	class WelcomeMessages {
		string[] regularMessages = new string[] {
			"Hi!",
			"Welcome!",
		};

		List<DialogQuestion> specialMessages = new List<DialogQuestion>() {
			new DialogQuestion(
				StatsHolder.Stat.Cat,null,
				new List<DialogAnswer>(){
					new DialogAnswer(-100,  -50,    "WELCOME Cat -100, -50"),
					new DialogAnswer(-49,   -1,     "WELCOME Cat -49,  -1"),
					new DialogAnswer(0,     0,      "WELCOME Cat 0"),
					new DialogAnswer(1,     50,     "WELCOME Cat 1, 50"),
					new DialogAnswer(51,    100,    "WELCOME Cat 51, 100"),
					new DialogAnswer(-100,    100,    "WELCOME Cat -100, 100"),
				}
			),

			new DialogQuestion(
				StatsHolder.Stat.Dog,null,
				new List<DialogAnswer>(){
					new DialogAnswer(-100,  -50,    "WELCOME Dog -100, -50"),
					new DialogAnswer(-49,   -1,     "WELCOME Dog -49,  -1"),
					new DialogAnswer(0,     0,      "WELCOME Dog 0"),
					new DialogAnswer(1,     50,     "WELCOME Dog 1, 50"),
					new DialogAnswer(51,    100,    "WELCOME Dog 51, 100"),
					new DialogAnswer(-100,    100,    "WELCOME Dog -100, 100"),
				}
			),
		};

		public string GetMessage(StatsHolder stat) {
			List<string> possibleAnswers = new List<string>(regularMessages);
			foreach (var j in specialMessages)
				for (int i = 0; i < (int)StatsHolder.Stat.LAST_STAT; ++i)
					if (i == (int)j.linkedStat)
						possibleAnswers.Add(j.GetAnswerInRange(stat.GetStatValue(j.linkedStat)));

			return possibleAnswers[Random.Range(0, possibleAnswers.Count)];
		}
	}

	class TiredMessages {
		string[] regularMessages = new string[] {
			"I am tired!",
			"I am running out of time!",
		};

		List<DialogQuestion> specialMessages = new List<DialogQuestion>() {
			new DialogQuestion(
				StatsHolder.Stat.Cat,null,
				new List<DialogAnswer>(){
					new DialogAnswer(-100,  -50,    "TIRED Cat -100, -50"),
					new DialogAnswer(-49,   -1,     "TIRED Cat -49,  -1"),
					new DialogAnswer(0,     0,      "TIRED Cat 0"),
					new DialogAnswer(1,     50,     "TIRED Cat 1, 50"),
					new DialogAnswer(51,    100,    "TIRED Cat 51, 100"),
					new DialogAnswer(-100,    100,  "TIRED Cat -100, 100"),
				}
			),

			new DialogQuestion(
				StatsHolder.Stat.Dog,null,
				new List<DialogAnswer>(){
					new DialogAnswer(-100,  -50,    "TIRED Dog -100, -50"),
					new DialogAnswer(-49,   -1,     "TIRED Dog -49,  -1"),
					new DialogAnswer(0,     0,      "TIRED Dog 0"),
					new DialogAnswer(1,     50,     "TIRED Dog 1, 50"),
					new DialogAnswer(51,    100,    "TIRED Dog 51, 100"),
					new DialogAnswer(-100,    100,  "TIRED Dog -100, 100"),
				}
			),
		};

		public string GetMessage(StatsHolder stat) {
			if (stat.Tired > 0)
				return "";

			List<string> possibleAnswers = new List<string>(regularMessages);
			foreach (var j in specialMessages)
				for (int i = 0; i < (int)StatsHolder.Stat.LAST_STAT; ++i)
					if (i == (int)j.linkedStat)
						possibleAnswers.Add(j.GetAnswerInRange(stat.GetStatValue(j.linkedStat)));

			return possibleAnswers[Random.Range(0, possibleAnswers.Count)];
		}
	}

	public Text answerText;
	public Button[] questionsButton;
	public Button inviteButton;
	public Button exitButton;

	public event System.Action InterruptedByUser;
	public event System.Action InviteHuman;
	public event System.Action TiredHuman;

	//Тут всі діалоги.
	//TODO: загрузка з XML
	WelcomeMessages welcomeMessages = new WelcomeMessages();
	TiredMessages tiredMessages = new TiredMessages();
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

	void Start() {
		choosed = new int[questionsButton.Length][];
		for (int i = 0; i < questionsButton.Length; ++i)
			choosed[i] = new int[2];

		inviteButton.onClick.AddListener(() => {
			if (InviteHuman != null)
				InviteHuman.Invoke();
			EndDialog();
		});

		exitButton.onClick.AddListener(() => {
			if (InterruptedByUser != null)
				InterruptedByUser.Invoke();
			EndDialog();
		});
	}

	public void StartDialog(StatsHolder statsHolder) {
		currentStats = statsHolder;
		answerText.text = welcomeMessages.GetMessage(currentStats) + '\n';

		//DEBUG:
		answerText.text += currentStats.ToString() + '\n';

		FillQuestions();
	}

	public void ChooseQuestion(byte question) {
		answerText.text = dialogs[choosed[question][0]].GetAnswerInRange(currentStats.GetStatValue(dialogs[choosed[question][0]].linkedStat)) + '\n';
		currentStats.GiveAnswer();

		if (currentStats.Tired > 0)
			FillQuestions();
		else {
			if (TiredHuman != null)
				TiredHuman.Invoke();
			EndDialog();
		}

		answerText.text += tiredMessages.GetMessage(currentStats) + '\n';

		//DEBUG:
		answerText.text += currentStats.ToString() + '\n';
	}

	void FillQuestions() {
		for (int i = 0; i < questionsButton.Length; ++i) {
		REPEAT_RANDOM:
			choosed[i][0] = Random.Range(0, dialogs.Count);
			choosed[i][1] = Random.Range(0, dialogs[choosed[i][0]].questions.Length);
			for (int j = 0; j < i; ++j)
				if (choosed[i][0] == choosed[j][0] && choosed[i][1] == choosed[j][1])
					goto REPEAT_RANDOM;

			questionsButton[i].GetComponentInChildren<Text>().text = dialogs[choosed[i][0]].questions[choosed[i][1]];
		}
	}

	void EndDialog() {
		for (int i = 0; i < questionsButton.Length; ++i)
			questionsButton[i].GetComponentInChildren<Text>().text = "Exit";
	}
}
