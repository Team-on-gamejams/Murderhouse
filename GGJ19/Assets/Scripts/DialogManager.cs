using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
	class DialogQuestion {
		static public List<DialogAnswer> answersForAll = new List<DialogAnswer>();

		public StatsHolder.Stat linkedStat;
		public List<string> questions;
		public List<DialogAnswer> answers;

		public DialogQuestion() {
			questions = new List<string>();
			answers = new List<DialogAnswer>();
		}

		public DialogQuestion(StatsHolder.Stat linkedStat, string[] questions, List<DialogAnswer> answers) {
			this.linkedStat = linkedStat;
			this.questions = new List<string>();
			if (questions != null)
				this.questions.AddRange(questions);
			this.answers = answers;
		}

		public string GetAnswerInRange(int num) {
			List<DialogAnswer> fitted = new List<DialogAnswer>();
			foreach (var i in answers)
				if (i.minRange <= num && num <= i.maxRange)
					fitted.Add(i);
			foreach (var i in answersForAll)
				if (i.minRange <= num && num <= i.maxRange)
					fitted.Add(i);

			return fitted[Random.Range(0, fitted.Count)].answer;
		}
	}

	class DialogAnswer {
		public sbyte minRange, maxRange;
		public string answer;

		public DialogAnswer() {

		}

		public DialogAnswer(sbyte min, sbyte max, string answer) {
			minRange = min;
			maxRange = max;
			this.answer = answer;
		}
	}

	class WelcomeMessages {
		public List<string> regularMessages = new List<string>();
		public List<DialogQuestion> specialMessages = new List<DialogQuestion>();

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
		public List<string> regularMessages = new List<string>();
		public List<DialogQuestion> specialMessages = new List<DialogQuestion>();

		public string GetMessage(StatsHolder stat) {
			if (stat.Tired > 1)
				return "";

			List<string> possibleAnswers = new List<string>(regularMessages);
			foreach (var j in specialMessages)
				for (int i = 0; i < (int)StatsHolder.Stat.LAST_STAT; ++i)
					if (i == (int)j.linkedStat)
						possibleAnswers.Add(j.GetAnswerInRange(stat.GetStatValue(j.linkedStat)));

			return possibleAnswers[Random.Range(0, possibleAnswers.Count)];
		}
	}

	public GameObject dialogWindow;
	AlphaCanvasControll dialogWindowAlpha;
	public Text answerText;
	public Text statInfoText;
	public Button[] questionsButton;
	public Button inviteButton;
	public Button exitButton;

	MoneyController money;

	//Перший заведений діалог це завжди обучалка.
	bool isFirstDialog;
	int firstDialogStage;

	WelcomeMessages welcomeMessages;
	TiredMessages tiredMessages;
	List<DialogQuestion> dialogs;

	// 0 - вибраний діалог
	// 1 - вибране питання
	int[][] choosed;
	StatsHolder currentStats;
	DialogInitializer currentDI;
	bool dialogAwaliable;

	void Start() {
		money = GameObject.FindGameObjectWithTag("MoneyController").GetComponent<MoneyController>();
		dialogWindowAlpha = dialogWindow.GetComponent<AlphaCanvasControll>();
		dialogAwaliable = false;
		isFirstDialog = true;
		firstDialogStage = 1;

		choosed = new int[questionsButton.Length][];
		for (int i = 0; i < questionsButton.Length; ++i)
			choosed[i] = new int[2];

		inviteButton.onClick.AddListener(() => {
			//TODO: Перевірити чи хоче він зайти
			if (currentStats.IsHomeSatisfying()) {
				EndDialog();
				currentDI.InviteToHome();
				money.IncomeFromKill();
			}
		});

		exitButton.onClick.AddListener(() => {
			if (!isFirstDialog)
				EndDialog();
		});

		LoadDialoges("ua");

		foreach (var d in dialogs) {
			string str = "DIALOG---------------\n";
			str += "Stat: " + d.linkedStat + "\n";
			foreach (var q in d.questions) {
				str += "q: " + q + "\n";
			}
			foreach (var a in d.answers) {
				str += "a: " + a.minRange.ToString() + ".." + a.maxRange.ToString() + " " + a.answer + "\n";
			}
			str += "----------------------DIALOG";
			print(str);
		}

		foreach (var d in tiredMessages.specialMessages) {
			string str = "DIALOG---------------\n";
			str += "Stat: " + d.linkedStat + "\n";
			foreach (var q in d.questions) {
				str += "q: " + q + "\n";
			}
			foreach (var a in d.answers) {
				str += "a: " + a.minRange.ToString() + ".." + a.maxRange.ToString() + " " + a.answer + "\n";
			}
			str += "----------------------DIALOG";
			print(str);
		}

		foreach (var d in welcomeMessages.specialMessages) {
			string str = "DIALOG---------------\n";
			str += "Stat: " + d.linkedStat + "\n";
			foreach (var q in d.questions) {
				str += "q: " + q + "\n";
			}
			foreach (var a in d.answers) {
				str += "a: " + a.minRange.ToString() + ".." + a.maxRange.ToString() + " " + a.answer + "\n";
			}
			str += "----------------------DIALOG";
			print(str);
		}
	}

	public void LoadDialoges(string lang) {
		dialogs = new List<DialogQuestion>();
		welcomeMessages = new WelcomeMessages();
		tiredMessages = new TiredMessages();

		//Dialogs
		bool staticAnswers = false;
		TextAsset textAsset = (TextAsset)Resources.Load("dialogs_" + lang);
		XmlDocument xmldoc = new XmlDocument();
		xmldoc.LoadXml(textAsset.text);

		foreach (XmlNode rep in xmldoc.ChildNodes[0].ChildNodes) {
			DialogQuestion diag = new DialogQuestion();

			foreach (XmlNode i in rep.ChildNodes) {
				if (i.Name == "stat") {
					if (i.InnerText == "None") {
						staticAnswers = true;
					}
					else {
						diag.linkedStat = (StatsHolder.Stat)System.Enum.Parse(typeof(StatsHolder.Stat), i.InnerText);
						staticAnswers = false;
					}
				}
				else if (i.Name == "question") {
					diag.questions.Add(i.InnerText);
				}
				else if (i.Name == "answer") {
					DialogAnswer da = new DialogAnswer();
					da.answer = i.InnerText;
					foreach (XmlAttribute attr in i.Attributes) {
						if (attr.Name == "min")
							da.minRange = sbyte.Parse(attr.Value);
						else if (attr.Name == "max")
							da.maxRange = sbyte.Parse(attr.Value);
					}
					diag.answers.Add(da);
				}
			}
			if (staticAnswers) {
				foreach (var an in diag.answers)
					DialogQuestion.answersForAll.Add(an);
			}
			else
				dialogs.Add(diag);
		}

		//Dialogs welcome
		textAsset = (TextAsset)Resources.Load("welcomedialogs_" + lang);
		xmldoc = new XmlDocument();
		xmldoc.LoadXml(textAsset.text);

		foreach (XmlNode i in xmldoc.ChildNodes[0].ChildNodes) {
			if (i.Name == "regular") {
				welcomeMessages.regularMessages.Add(i.InnerText);
			}
			else if (i.Name != "#text") {
				DialogQuestion dq = new DialogQuestion();
				dq.linkedStat = (StatsHolder.Stat)System.Enum.Parse(typeof(StatsHolder.Stat), i.Name, true);

				foreach (XmlNode spec in i.ChildNodes) {
					DialogAnswer da = new DialogAnswer();
					da.answer = spec.InnerText;
					foreach (XmlAttribute attr in spec.Attributes) {
						if (attr.Name == "min")
							da.minRange = sbyte.Parse(attr.Value);
						else if (attr.Name == "max")
							da.maxRange = sbyte.Parse(attr.Value);
					}
					dq.answers.Add(da);
				}

				welcomeMessages.specialMessages.Add(dq);
			}
		}

		//Dialogs tired
		textAsset = (TextAsset)Resources.Load("tireddialogs_" + lang);
		xmldoc = new XmlDocument();
		xmldoc.LoadXml(textAsset.text);

		foreach (XmlNode i in xmldoc.ChildNodes[0].ChildNodes) {
			if (i.Name == "regular") {
				tiredMessages.regularMessages.Add(i.InnerText);
			}
			else if (i.Name != "#text") {
				DialogQuestion dq = new DialogQuestion();
				dq.linkedStat = (StatsHolder.Stat)System.Enum.Parse(typeof(StatsHolder.Stat), i.Name, true);

				foreach (XmlNode spec in i.ChildNodes) {
					DialogAnswer da = new DialogAnswer();
					da.answer = spec.InnerText;
					foreach (XmlAttribute attr in spec.Attributes) {
						if (attr.Name == "min")
							da.minRange = sbyte.Parse(attr.Value);
						else if (attr.Name == "max")
							da.maxRange = sbyte.Parse(attr.Value);
					}
					dq.answers.Add(da);
				}

				tiredMessages.specialMessages.Add(dq);
			}
		}
	}

	public void StartDialog(GameObject person) {
		dialogWindowAlpha.Show();
		dialogAwaliable = true;

		currentStats = person.GetComponent<StatsHolder>();
		currentDI = person.GetComponent<DialogInitializer>();

		statInfoText.text = currentStats.ToString() + '\n';

		if (!isFirstDialog) {
			answerText.text = welcomeMessages.GetMessage(currentStats) + '\n';
			FillQuestions();
		}
		else {
			FillFirstDialog();
		}
	}

	public void ChooseQuestion(byte question) {
		if (!dialogAwaliable)
			return;

		if (!isFirstDialog) {
			answerText.text = dialogs[choosed[question][0]].GetAnswerInRange(currentStats.GetStatValue(dialogs[choosed[question][0]].linkedStat)) + '\n';
			currentStats.GiveAnswer();
			answerText.text += tiredMessages.GetMessage(currentStats) + '\n';

			if (currentStats.Tired > 0)
				FillQuestions();
			else
				EndDialog();
		}
		else {
			FillFirstDialog();
		}

		statInfoText.text = currentStats.ToString() + '\n';
	}

	void EndDialog() {
		dialogWindowAlpha.Hide();
		dialogAwaliable = false;
		isFirstDialog = false;
		currentDI.EndDialog();

		answerText.text = "dialog ended";
		for (int i = 0; i < questionsButton.Length; ++i)
			questionsButton[i].GetComponentInChildren<Text>().text = "Exit";
	}

	void FillQuestions() {
		for (int i = 0; i < questionsButton.Length; ++i) {
		REPEAT_RANDOM:
			choosed[i][0] = Random.Range(0, dialogs.Count);
			choosed[i][1] = Random.Range(0, dialogs[choosed[i][0]].questions.Count);
			for (int j = 0; j < i; ++j)
				if (choosed[i][0] == choosed[j][0] && choosed[i][1] == choosed[j][1])
					goto REPEAT_RANDOM;

			questionsButton[i].GetComponentInChildren<Text>().text = dialogs[choosed[i][0]].questions[choosed[i][1]];
		}
	}

	void FillFirstDialog() {
		switch (firstDialogStage) {
			case 0:
				answerText.text = "Welcome! [Розказує хто ти].";
				foreach (var bq in questionsButton)
					bq.GetComponentInChildren<Text>().text = "Ок.";
				currentStats.Stats[0] = 100;
				break;
			case 1:
				answerText.text = "[Розказує що тобі треба робити].";
				foreach (var bq in questionsButton)
					bq.GetComponentInChildren<Text>().text = "Ок.";
				break;
			case 2:
				answerText.text = "[Пропонує поставити тобі щось для " + ((StatsHolder.Stat)(0)).ToString() + "].";
				foreach (var bq in questionsButton)
					bq.GetComponentInChildren<Text>().text = "Ок. Я поставив";
				break;
			default:
				answerText.text = "[Пропонує запросити його].";
				foreach (var bq in questionsButton)
					bq.GetComponentInChildren<Text>().text = "Це не запросити.";
				break;
		}
		++firstDialogStage;
	}
}
