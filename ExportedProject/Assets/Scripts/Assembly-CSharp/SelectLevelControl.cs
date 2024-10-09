using UnityEngine;
using UnityEngine.UI;

public class SelectLevelControl : MonoBehaviour
{
	private GameManager _gm;

	private Button _button;

	public int _indexLevel;

	public SaveSceneIndex saveIndex;

	private void Awake()
	{
		saveIndex = Object.FindObjectOfType<SaveSceneIndex>();
	}

	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(delegate
		{
			saveIndex.index = _indexLevel + 1;
		});
	}
}
