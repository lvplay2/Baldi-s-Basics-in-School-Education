using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public Image bar;

	public GameHelper gameHelper;

	private float health;

	public float Health
	{
		get
		{
			return health;
		}
		set
		{
			health = value;
			if (health < 0f)
			{
				health = 0f;
			}
			if (health == 0f)
			{
				Death();
			}
			UpdateViewHealth();
		}
	}

	private void Start()
	{
		Health = 100f;
	}

	private void UpdateViewHealth()
	{
		bar.fillAmount = Health / 100f;
	}

	private void Death()
	{
		gameHelper.DeathPanel.gameObject.SetActive(true);
		gameHelper.DeathPanel.SetTrigger("Show");
		GetComponent<PlayerController>().enabled = false;
		gameHelper.swipeController.enabled = false;
	}

	public void ResurrectionButton()
	{
	}

	private void Resurrection()
	{
		GetComponent<PlayerController>().enabled = true;
		gameHelper.swipeController.enabled = true;
		Health = 100f;
	}

	public void MenuButton()
	{
		GameHelper.singleton.IntoMenu();
		SceneManager.LoadScene("Main");
	}

	private void AdsQuestView()
	{
		gameHelper.DeathPanel.gameObject.SetActive(true);
		gameHelper.DeathPanel.SetTrigger("Show");
	}
}
