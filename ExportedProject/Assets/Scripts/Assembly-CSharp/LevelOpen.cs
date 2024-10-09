using UnityEngine;
using UnityEngine.UI;

public class LevelOpen : MonoBehaviour
{
	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(delegate
		{
			ButtonAds();
		});
	}

	private void ButtonAds()
	{
	}

	private void OpenItem()
	{
		PlayerPrefs.SetInt(SaveManager.GetKeyProject() + "ProductCap" + GetComponentInParent<SelectLevelControl>()._indexLevel, 1);
		GetComponentInParent<Levels>().UpdateLevels();
	}
}
