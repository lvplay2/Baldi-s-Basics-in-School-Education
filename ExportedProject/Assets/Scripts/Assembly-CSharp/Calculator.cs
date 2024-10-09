using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
	private GameObject panelToggles;

	private GameObject panelQuestion;

	private GameObject panelEnter;

	private GameObject panelButtonAnswer;

	public GameObject canvasInterface;

	public GameObject blackFon;

	private InputField inputField;

	private List<Toggle> toggles = new List<Toggle>();

	private Text questDescription;

	private Text question;

	private Text phraseText;

	public Sprite checkMark;

	public Sprite unCheckMark;

	public List<BadPhrase> badPhrases;

	public List<GoodPhrases> goodPhrasses;

	private Text inputText;

	private Button buttonAnswer;

	public GameHelper gameHelper;

	private int finishAnswer;

	private int firstNumber;

	private int secondNumber;

	private bool plus;

	private char sign;

	private SystemLanguage sl;

	private int currentAnswers;

	private void Start()
	{
		PropertyAssign();
		sl = Application.systemLanguage;
		Assign();
		AddQuestion();
	}

	private void PropertyAssign()
	{
		panelToggles = base.transform.GetChild(0).gameObject;
		panelQuestion = base.transform.GetChild(1).gameObject;
		panelEnter = base.transform.GetChild(2).gameObject;
		panelButtonAnswer = base.transform.GetChild(3).gameObject;
		questDescription = panelQuestion.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		question = panelQuestion.transform.GetChild(0).GetChild(1).GetComponent<Text>();
		phraseText = panelQuestion.transform.GetChild(0).GetChild(2).GetComponent<Text>();
		phraseText.gameObject.SetActive(false);
		buttonAnswer = panelButtonAnswer.transform.GetChild(0).GetComponent<Button>();
		inputText = panelEnter.transform.GetChild(0).GetChild(1).GetComponent<Text>();
		inputField = panelEnter.transform.GetChild(0).GetComponent<InputField>();
		ToggleAssign();
	}

	private void Assign()
	{
		buttonAnswer.onClick.RemoveAllListeners();
		buttonAnswer.onClick.AddListener(ButtonAnswer);
		buttonAnswer.transform.GetChild(0).GetComponent<Text>().text = LocalizationBase.GetTranslate("AnswerButton");
	}

	public void RessetCalculator()
	{
		for (int i = 0; i < toggles.Count; i++)
		{
			toggles[i].graphic.GetComponent<Image>().sprite = checkMark;
			toggles[i].isOn = false;
		}
		question.gameObject.SetActive(true);
		questDescription.gameObject.SetActive(true);
		phraseText.gameObject.SetActive(false);
		buttonAnswer.transform.GetChild(0).GetComponent<Text>().text = LocalizationBase.GetTranslate("AnswerButton");
		currentAnswers = 0;
		Assign();
		AddQuestion();
	}

	public void AddQuestion()
	{
		QuestGeneration();
		questDescription.text = LocalizationBase.GetTranslate("QuestionNumber") + " " + (currentAnswers + 1);
		question.text = (firstNumber + " " + sign + " " + secondNumber).ToString();
	}

	private void ToggleAssign()
	{
		toggles.Clear();
		for (int i = 0; i < panelToggles.transform.childCount; i++)
		{
			toggles.Add(panelToggles.transform.GetChild(i).GetComponent<Toggle>());
		}
	}

	private void ButtonAnswer()
	{
		int num = int.Parse(inputText.text);
		if (num != finishAnswer)
		{
			QuestFailed();
		}
		else
		{
			CheckAnswer();
		}
		inputField.text = string.Empty;
	}

	private void QuestFailed()
	{
		if (currentAnswers < 3)
		{
			CloseDescription();
			toggles[currentAnswers].graphic.GetComponent<Image>().sprite = unCheckMark;
			toggles[currentAnswers].isOn = true;
			AddBadPhrasses();
			buttonAnswer.onClick.RemoveAllListeners();
			buttonAnswer.onClick.AddListener(ButtonAnswerToFailed);
		}
	}

	private void Close()
	{
		gameHelper.playerController.GetComponent<PlayerComponent>().isCalculating = false;
		RessetCalculator();
		gameHelper.playerController.enabled = true;
		canvasInterface.SetActive(true);
		base.gameObject.SetActive(false);
		blackFon.SetActive(false);
		gameHelper.playerController.joystickController.ResetTarget();
	}

	private void ButtonAnswerToFailed()
	{
		gameHelper.teacher.MakeAngry();
		Close();
	}

	private void AddBadPhrasses()
	{
		int index = Random.Range(0, badPhrases.Count);
		phraseText.gameObject.SetActive(true);
		if (sl == SystemLanguage.English)
		{
			phraseText.text = badPhrases[index].badPhrasesEng;
		}
		else if (sl == SystemLanguage.Russian)
		{
			phraseText.text = badPhrases[index].badPhrasesRus;
		}
		questDescription.gameObject.SetActive(false);
		question.gameObject.SetActive(false);
	}

	private void AddGoodPhrasses()
	{
		int index = Random.Range(0, goodPhrasses.Count);
		phraseText.gameObject.SetActive(true);
		if (sl == SystemLanguage.English)
		{
			phraseText.text = goodPhrasses[index].goodPhrasesEng;
		}
		else if (sl == SystemLanguage.Russian)
		{
			phraseText.text = goodPhrasses[index].goodPhrasesRus;
		}
		questDescription.gameObject.SetActive(false);
		question.gameObject.SetActive(false);
	}

	private void CheckAnswer()
	{
		if (currentAnswers < 3)
		{
			toggles[currentAnswers].isOn = true;
			currentAnswers++;
			AddQuestion();
			if (currentAnswers == 3)
			{
				Win();
			}
			else
			{
				AddQuestion();
			}
		}
	}

	private void CloseDescription()
	{
		question.gameObject.SetActive(false);
		questDescription.gameObject.SetActive(false);
		buttonAnswer.transform.GetChild(0).GetComponent<Text>().text = LocalizationBase.GetTranslate("ExitButton");
	}

	private void Win()
	{
		gameHelper.teacher.giveCoin = true;
		buttonAnswer.onClick.RemoveAllListeners();
		buttonAnswer.onClick.AddListener(ButtonAnswerToWin);
		CloseDescription();
		AddGoodPhrasses();
	}

	private void ButtonAnswerToWin()
	{
		gameHelper.teacher.MakeHappy();
		Close();
	}

	private void QuestGeneration()
	{
		firstNumber = Random.Range(3, 15);
		secondNumber = Random.Range(3, 15);
		if (firstNumber < secondNumber)
		{
			int num = firstNumber;
			firstNumber = secondNumber;
			secondNumber = num;
		}
		int num2 = Random.Range(0, 100);
		if (num2 < 50)
		{
			sign = '+';
			plus = true;
			finishAnswer = firstNumber + secondNumber;
		}
		else
		{
			sign = '-';
			plus = false;
			finishAnswer = firstNumber - secondNumber;
		}
	}
}
