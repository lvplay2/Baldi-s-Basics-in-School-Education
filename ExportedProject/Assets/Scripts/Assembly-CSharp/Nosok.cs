using UnityEngine;

public class Nosok : Character
{
	public Transform nosokTeleport;

	public float detectRadius;

	private AudioSource audioSource;

	protected override void Start()
	{
		base.Start();
		wayPoints.Reverse();
		detectRadius = 20f;
		followDistance = 15f;
		agent.speed = 1.6f;
		audioSource = GetComponent<AudioSource>();
	}

	protected override void Update()
	{
		if (!isAngry)
		{
			PatrolWayPoints();
		}
		if (player.GetComponent<BonusController>().bonuses.Count == 0 && !isAngry)
		{
			isAngry = true;
		}
		if (isAngry)
		{
			if (Vector3.Distance(base.transform.position, player.position) <= dieDistance && !player.GetComponent<PlayerComponent>().isCalculating)
			{
				Сaught();
			}
			if (Vector3.Distance(base.transform.position, player.position) < detectRadius && !player.GetComponent<PlayerComponent>().isCalculating)
			{
				MakeAngry();
			}
			else
			{
				PatrolWayPoints();
			}
			if (!audioSource.enabled)
			{
				audioSource.enabled = true;
			}
		}
	}

	protected override void Сaught()
	{
		player.GetComponent<PlayerController>().TeleportToPoint(player.GetComponent<PlayerComponent>().startPos);
		base.transform.position = nosokTeleport.position;
	}
}
