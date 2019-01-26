using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
	class DialogQuestion {
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
		dialogAwaliable = false;

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

		LoadDialoges("ua");
	}

	public void LoadDialoges(string lang) {
		dialogs = new List<DialogQuestion>();
		welcomeMessages = new WelcomeMessages();
		tiredMessages = new TiredMessages();

		//Dialogs
		TextAsset textAsset = (TextAsset)Resources.Load("dialogs_" + lang);
		XmlDocument xmldoc = new XmlDocument();
		xmldoc.LoadXml(textAsset.text);

		foreach (XmlNode rep in xmldoc.ChildNodes[0].ChildNodes) {
			DialogQuestion diag = new DialogQuestion();

			foreach (XmlNode i in rep.ChildNodes) {
				if (i.Name == "stat") {
					diag.linkedStat = (StatsHolder.Stat)System.Enum.Parse(typeof(StatsHolder.Stat), i.InnerText);
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

			dialogs.Add(diag);
		}

		//Dialogs welcome
		textAsset = (TextAsset)Resources.Load("welcomedialogs_" + lang);
		xmldoc = new XmlDocument();
		xmldoc.LoadXml(textAsset.text);

		foreach (XmlNode i in xmldoc.ChildNodes[0].ChildNodes) {
			print(i.Name);
			if (i.Name == "regular") {
				welcomeMessages.regularMessages.Add(i.InnerText);
			}
			else if(i.Name != "#text"){
				DialogQuestion dq = new DialogQuestion();
				dq.linkedStat = (StatsHolder.Stat)System.Enum.Parse(typeof(StatsHolder.Stat), i.Name, true);

				foreach (XmlNode spec in i.ChildNodes) {
					DialogAnswer da = new DialogAnswer();
					da.answer = spec.InnerText;
					foreach (XmlAttribute attr in spec.Attributes) {
						print(attr.Name);
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
			print(i.Name);
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
						print(attr.Name);
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
		dialogAwaliable = true;

		currentStats = person.GetComponent<StatsHolder>();
		currentDI = person.GetComponent<DialogInitializer>();
		answerText.text = welcomeMessages.GetMessage(currentStats) + '\n';

		//DEBUG:
		answerText.text += currentStats.ToString() + '\n';

		FillQuestions();
	}

	public void ChooseQuestion(byte question) {
		if (!dialogAwaliable)
			return;

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
			choosed[i][1] = Random.Range(0, dialogs[choosed[i][0]].questions.Count);
			for (int j = 0; j < i; ++j)
				if (choosed[i][0] == choosed[j][0] && choosed[i][1] == choosed[j][1])
					goto REPEAT_RANDOM;

			questionsButton[i].GetComponentInChildren<Text>().text = dialogs[choosed[i][0]].questions[choosed[i][1]];
		}
	}

	void EndDialog() {
		dialogAwaliable = false;
		currentDI.EndDialog();

		answerText.text = "dialog ended";
		for (int i = 0; i < questionsButton.Length; ++i)
			questionsButton[i].GetComponentInChildren<Text>().text = "Exit";
	}
}
