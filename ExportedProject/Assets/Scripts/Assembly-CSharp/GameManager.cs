using UnityEngine;

public class GameManager : MonoBehaviour
{
	private int _countPickUp;

	private int _countLower;

	public GameObject _poolPickUp;

	public GameObject _poolLowerObj;

	public GameObject[] _allPickUpTargets;

	public GameObject[] _allLowerObjTargets;

	public Transform _currentTarget;

	[HideInInspector]
	public int _countFinshedLevel;

	private int _currentLevel;

	private int _countLevelForFinishGame;

	private int _counterStepPlayer;

	public GameObject _textNextLevel;

	public GameObject _menu;

	public GameObject _gameScreen;

	public string _moreGames;

	public string _privacyPolicy;

	private void Awake()
	{
		_countFinshedLevel = PlayerPrefs.GetInt("_fnshLvl", 0);
		_countPickUp = 0;
		_countLower = 0;
		StartGame();
	}

	public void StartGame()
	{
		NextTargetPickUp();
	}

	public void InMenu()
	{
		_menu.SetActive(true);
		_gameScreen.SetActive(false);
		IntersitianBanner();
	}

	public void LoadLevel(int _getIndexLevel)
	{
		_menu.SetActive(false);
		_gameScreen.SetActive(true);
		_counterStepPlayer = 0;
		_currentLevel = _getIndexLevel;
		switch (_getIndexLevel)
		{
		case 0:
			_countLevelForFinishGame = 3;
			break;
		case 1:
			_countLevelForFinishGame = 5;
			break;
		case 2:
			_countLevelForFinishGame = 7;
			break;
		case 3:
			_countLevelForFinishGame = 10;
			break;
		case 4:
			_countLevelForFinishGame = 15;
			break;
		case 5:
			_countLevelForFinishGame = 20;
			break;
		case 6:
			_countLevelForFinishGame = 25;
			break;
		case 7:
			_countLevelForFinishGame = 30;
			break;
		}
		IntersitianBanner();
	}

	public void CheckNewLevel()
	{
		_currentLevel++;
		if (_currentLevel > _countFinshedLevel)
		{
			_countFinshedLevel = _currentLevel;
			PlayerPrefs.SetInt("_fnshLvl", _countFinshedLevel);
		}
		if (_countFinshedLevel < 8)
		{
			_textNextLevel.SetActive(false);
			_textNextLevel.SetActive(true);
			LoadLevel(_currentLevel);
		}
	}

	public void NextTargetPickUp()
	{
		_countPickUp++;
		if (_countPickUp < _allPickUpTargets.Length)
		{
			int num;
			do
			{
				num = Random.Range(0, _allPickUpTargets.Length);
			}
			while (_allPickUpTargets[num].activeInHierarchy);
			_allPickUpTargets[num].SetActive(true);
			_currentTarget = _allPickUpTargets[num].transform;
			_counterStepPlayer++;
			if (_counterStepPlayer == _countLevelForFinishGame)
			{
				CheckNewLevel();
			}
		}
	}

	public void NextDownLowerObj()
	{
		_countLower++;
		if (_countLower >= _allLowerObjTargets.Length)
		{
			_countLower = 0;
			GameObject[] allPickUpTargets = _allPickUpTargets;
			foreach (GameObject gameObject in allPickUpTargets)
			{
				gameObject.SetActive(false);
			}
			GameObject[] allLowerObjTargets = _allLowerObjTargets;
			foreach (GameObject gameObject2 in allLowerObjTargets)
			{
				gameObject2.SetActive(false);
			}
		}
		int num;
		do
		{
			num = Random.Range(0, _allLowerObjTargets.Length);
		}
		while (_allLowerObjTargets[num].activeInHierarchy);
		_allLowerObjTargets[num].SetActive(true);
		_currentTarget = _allLowerObjTargets[num].transform;
	}

	public void MoreGames()
	{
		Application.OpenURL(_moreGames);
	}

	public void PrivacyPolicy()
	{
		Application.OpenURL(_privacyPolicy);
	}

	public void IntersitianBanner()
	{
	}
}
