using UnityEngine;

public class SensitivityOption : MonoBehaviour
{
	private float cameraSens;

	public float CameraSens
	{
		get
		{
			return cameraSens;
		}
		set
		{
			cameraSens = value;
			PlayerPrefs.SetFloat(SaveManager.GetKeyProject() + "CameraSensa", value);
		}
	}

	private void Start()
	{
		cameraSens = PlayerPrefs.GetFloat(SaveManager.GetKeyProject() + "CameraSensa", 0.8f);
	}
}
