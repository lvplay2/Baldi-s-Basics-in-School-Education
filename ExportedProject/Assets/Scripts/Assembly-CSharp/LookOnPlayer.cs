using UnityEngine;

public class LookOnPlayer : MonoBehaviour
{
	public Transform player;

	private void Start()
	{
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}

	private void Update()
	{
		base.transform.LookAt(new Vector3(player.position.x, base.transform.position.y, player.position.z));
	}
}
