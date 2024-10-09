using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
	[Serializable]
	public class SpawnActors
	{
		public Transform actor;

		public Transform spawnPoint;
	}

	public List<SpawnActors> actors;

	public void Spawning()
	{
		for (int i = 0; i < actors.Count; i++)
		{
			actors[i].actor.position = actors[i].spawnPoint.position;
		}
	}
}
