using UnityEngine;

public class SaveManager : MonoBehaviour
{
	[Header("On start scene")]
	public bool isDeleteAllKey;

	[Header("Names for save")]
	public string nameProject;

	private static SaveManager singleton;

	private void Awake()
	{
		if (singleton == null)
		{
			singleton = this;
			DeleteAllKeys();
			Object.DontDestroyOnLoad(this);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void DeleteAllKeys()
	{
		if (isDeleteAllKey)
		{
			PlayerPrefs.DeleteAll();
		}
	}

	public static string GetKeyProject()
	{
		return singleton.nameProject;
	}
}
