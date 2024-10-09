using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huligan : MonoBehaviour
{
	private AudioSource audioSource;

	private bool isAngry = true;

	public PlayerComponent playerComponent;

	private int layer_mask;

	private float dieDistance;

	public List<GameObject> bullyWayArea;

	public List<Transform> teleportPoints;

	public float detectRadius;

	private int currentWayArea;

	public float TimeToTeleport = 16f;

	private void Start()
	{
		layer_mask = LayerMask.GetMask("Player", "Enemy");
		audioSource = GetComponent<AudioSource>();
		StartCoroutine(Teleport());
	}

	private void Update()
	{
		float num = Vector3.Distance(base.transform.position, playerComponent.transform.position);
		if (num <= detectRadius && playerComponent.CoinCount > 0 && !playerComponent.isCalculating && isAngry)
		{
			Vector3 direction = base.transform.GetChild(0).transform.TransformDirection(Vector3.forward);
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.GetChild(0).transform.position, direction, out hitInfo, layer_mask) && hitInfo.transform.CompareTag("Player"))
			{
				playerComponent.CoinCount--;
				isAngry = false;
				audioSource.Play();
				bullyWayArea[currentWayArea].SetActive(false);
			}
		}
	}

	private void Teleportation()
	{
		if (isAngry)
		{
			if (currentWayArea >= bullyWayArea.Count - 1)
			{
				currentWayArea = 0;
			}
			else
			{
				currentWayArea++;
			}
			base.transform.position = teleportPoints[currentWayArea].position;
			for (int i = 0; i < bullyWayArea.Count; i++)
			{
				bullyWayArea[i].SetActive(false);
			}
			bullyWayArea[currentWayArea].SetActive(true);
		}
	}

	private IEnumerator Teleport()
	{
		while (isAngry)
		{
			yield return new WaitForSeconds(TimeToTeleport);
			Teleportation();
		}
	}
}
