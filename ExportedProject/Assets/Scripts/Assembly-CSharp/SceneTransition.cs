using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
	[Serializable]
	public class SceneMenuItem
	{
		public enum enTypeScene
		{
			TEXT = 0,
			GAME_OBJECT = 1
		}

		public bool show = true;

		public Button buttonTransition;

		public enTypeScene sceneType;

		[Space]
		public string sceneName;

		public GameObject sceneObject;

		public GameObject oldScene;

		public bool showAds;

		public UnityEvent events;
	}

	public List<SceneMenuItem> items = new List<SceneMenuItem>();

	public List<UnityEvent> events = new List<UnityEvent>();

	private void Awake()
	{
		for (int i = 0; i < items.Count; i++)
		{
			items[i].events = events[i];
		}
	}

	private void Start()
	{
		for (int i = 0; i < items.Count; i++)
		{
			int temp = i;
			items[temp].buttonTransition.onClick.RemoveAllListeners();
			items[temp].buttonTransition.onClick.AddListener(delegate
			{
				items[temp].events.Invoke();
				if (items[temp].sceneObject == null && items[temp].sceneName != string.Empty)
				{
					SceneManager.LoadScene(items[temp].sceneName);
				}
				else if (items[temp].sceneObject != null && items[temp].sceneName == string.Empty)
				{
					items[temp].sceneObject.SetActive(true);
					items[temp].oldScene.SetActive(false);
				}
			});
		}
	}

	public void AddElement()
	{
		items.Add(new SceneMenuItem());
		events.Add(new UnityEvent());
	}

	public void DeleteElement(int index)
	{
		items.RemoveAt(index);
		if (events[index] != null)
		{
			events.RemoveAt(index);
		}
	}

	public void CreateList(int _elementsNum)
	{
		ClearList();
		Creating(_elementsNum);
	}

	private void Creating(int _elements)
	{
		for (int i = 0; i < _elements; i++)
		{
			items.Add(new SceneMenuItem());
			events.Add(new UnityEvent());
		}
	}

	private void ClearList()
	{
		items.Clear();
		events.Clear();
	}
}
