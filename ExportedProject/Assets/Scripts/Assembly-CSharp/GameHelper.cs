using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHelper : MonoBehaviour
{
	public enum enGameState
	{
		GAME = 0,
		WIN = 1,
		GAME_OVER = 2,
		WIN_LOSE = 3
	}

	public string sceneIndex;

	private int index;

	public Animator DeathPanel;

	public SwipePlayerController swipeController;

	public GameObject winPanel;

	public enGameState gameState;

	public PlayerController playerController;

	public Teacher teacher;

	public static GameHelper singleton;

	private void Awake()
	{
		if (singleton == null)
		{
			singleton = this;
		}
	}

	public void ContinueLevel()
	{
		SceneManager.LoadScene(int.Parse(sceneIndex) + 1);
	}

	public void EnemyOff()
	{
	}

	public void IntoMenu()
	{
		PlayerPrefs.SetInt(SaveManager.GetKeyProject() + "GameScreen", 1);
		SceneManager.LoadScene(0);
	}

	public void GameOver()
	{
		gameState = enGameState.GAME_OVER;
		playerController.enabled = false;
	}

	public void Win()
	{
		gameState = enGameState.WIN;
		playerController.enabled = false;
	}

	private void RessurectGame()
	{
		gameState = enGameState.GAME;
		playerController.enabled = true;
	}
}
