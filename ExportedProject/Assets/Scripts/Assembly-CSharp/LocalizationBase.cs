using System.Collections.Generic;
using UnityEngine;

public class LocalizationBase : MonoBehaviour
{
	[Header("Elements")]
	public List<LocElement> elements = new List<LocElement>();

	private static LocalizationBase singleton;

	private void Awake()
	{
		singleton = this;
	}

	public static string GetTranslate(string _key)
	{
		for (int i = 0; i < singleton.elements.Count; i++)
		{
			if (singleton.elements[i].key == _key)
			{
				if (Application.systemLanguage == SystemLanguage.Russian)
				{
					return singleton.elements[i].rusText;
				}
				if (Application.systemLanguage == SystemLanguage.English)
				{
					return singleton.elements[i].engText;
				}
			}
		}
		return null;
	}
}
