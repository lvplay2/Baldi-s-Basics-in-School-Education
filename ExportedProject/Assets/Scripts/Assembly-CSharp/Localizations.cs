using UnityEngine;
using UnityEngine.UI;

public class Localizations : MonoBehaviour
{
	public enum enTypeContent
	{
		TEXT = 0,
		SPRITE = 1
	}

	private SystemLanguage sl;

	public enTypeContent typeContent;

	[Header("Text")]
	public string rusText;

	public string engText;

	[Header("Sprite")]
	public Sprite rusSprite;

	public Sprite engSprite;

	private void Start()
	{
		sl = Application.systemLanguage;
		if (typeContent == enTypeContent.TEXT)
		{
			Text component = GetComponent<Text>();
			if (sl == SystemLanguage.Russian)
			{
				component.text = rusText;
			}
			else
			{
				component.text = engText;
			}
		}
		else if (typeContent == enTypeContent.SPRITE)
		{
			Image component2 = GetComponent<Image>();
			if (sl == SystemLanguage.Russian)
			{
				component2.sprite = rusSprite;
			}
			else
			{
				component2.sprite = engSprite;
			}
		}
	}
}
