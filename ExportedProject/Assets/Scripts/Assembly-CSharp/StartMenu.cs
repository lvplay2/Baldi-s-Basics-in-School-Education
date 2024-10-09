using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
	[Header("Button Play")]
	public Button buttonPlay;

	public GameObject nextScreen;

	public bool isShowLargeBannerMainMenu;

	public SensitivityOption sensaOpt;

	public Slider sensaSlider;

	[Header("Button More games")]
	public Button buttonMoreGames;

	public string linkMoreGames;

	[Header("Button Privacy policy")]
	public Button buttonPrivacyPolicy;

	public Animator panelSettings;

	public string linkPrivacyPolicy;

	private void Start()
	{
		buttonPlay.onClick.AddListener(ButtonPlay);
		buttonPrivacyPolicy.onClick.AddListener(ButtonSettings);
		panelSettings.transform.Find("OK").GetComponent<Button>().onClick.AddListener(ButtonPrivacyPolicy);
		panelSettings.transform.Find("Close").GetComponent<Button>().onClick.AddListener(delegate
		{
			panelSettings.SetTrigger("Hide");
		});
		buttonMoreGames.onClick.AddListener(ButtonMoreGames);
		if (PlayerPrefs.GetInt(SaveManager.GetKeyProject() + "GameScreen", 0) != 0)
		{
			PlayerPrefs.SetInt(SaveManager.GetKeyProject() + "GameScreen", 0);
			ButtonPlay();
		}
	}

	public void ButtonPlay()
	{
		sensaOpt.CameraSens = sensaSlider.value;
		nextScreen.SetActive(true);
		base.gameObject.SetActive(false);
	}

	public void ButtonMoreGames()
	{
		Application.OpenURL(linkMoreGames);
	}

	public void ButtonSettings()
	{
		panelSettings.gameObject.SetActive(true);
		panelSettings.SetTrigger("Show");
	}

	public void ButtonPrivacyPolicy()
	{
		Application.OpenURL(linkPrivacyPolicy);
	}
}
