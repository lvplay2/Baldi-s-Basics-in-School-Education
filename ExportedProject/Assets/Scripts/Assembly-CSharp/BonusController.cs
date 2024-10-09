using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusController : MonoBehaviour
{
	public Text counterText;

	public GameObject FindWayOutText;

	public GameObject bloodPanel;

	public GameHelper gameHelper;

	public GameObject WinTrigger;

	public List<Item> bonuses;

	public List<GameObject> coinOnScene;

	private int maxCountBonus;

	private void Start()
	{
		maxCountBonus = bonuses.Count;
		UpdateText();
		InvokeRepeating("UpdateUnfinished", 1f, 1f);
	}

	public void RemoveCoin(GameObject _coin)
	{
		coinOnScene.Remove(_coin);
	}

	public void AddBonus(Item _bonusClient)
	{
		bonuses.Remove(_bonusClient);
		UpdateText();
	}

	public void UpdateQuestFinish()
	{
		if (bonuses.Count == 0)
		{
			GameOver();
		}
	}

	private void UpdateText()
	{
		counterText.text = maxCountBonus - bonuses.Count + "/" + maxCountBonus;
	}

	private void UpdateUnfinished()
	{
		if (bonuses.Count == 0)
		{
			GetComponent<AudioSource>().enabled = false;
			CancelInvoke();
			gameHelper.teacher.MakeHappy();
			GameOver();
		}
	}

	private void GameOver()
	{
		FindWayOutText.SetActive(true);
		bloodPanel.SetActive(true);
		WinTrigger.SetActive(true);
	}

	public void EndGame()
	{
	}
}
