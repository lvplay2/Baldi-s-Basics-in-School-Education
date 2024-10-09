using UnityEngine;

public class WinTrigger : MonoBehaviour
{
	public Animator WinPanel;

	public GameHelper gameHelper;

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player")
		{
			gameHelper.Win();
			OpenItem();
			WinPanel.gameObject.SetActive(true);
			WinPanel.SetTrigger("Show");
		}
	}

	private void OpenItem()
	{
		PlayerPrefs.SetInt(SaveManager.GetKeyProject() + "ProductCap" + gameHelper.sceneIndex, 1);
	}
}
