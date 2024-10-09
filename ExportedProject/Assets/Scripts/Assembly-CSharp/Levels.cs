using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
	public GameObject[] children;

	private void Start()
	{
		UpdateLevels();
	}

	public void UpdateLevels()
	{
		for (int i = 0; i < children.Length; i++)
		{
			int num = i;
			int indexLevel = children[num].GetComponent<SelectLevelControl>()._indexLevel;
			if (PlayerPrefs.HasKey(SaveManager.GetKeyProject() + "ProductCap" + indexLevel))
			{
				MonoBehaviour.print("Accept: " + num);
				children[num].GetComponent<Button>().interactable = true;
				children[num].transform.GetChild(1).gameObject.SetActive(false);
			}
		}
	}
}
