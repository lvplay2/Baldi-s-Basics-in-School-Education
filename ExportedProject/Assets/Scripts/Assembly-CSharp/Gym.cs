using UnityEngine;

public class Gym : Character
{
	public float detectRadius;

	public Transform prisonPosition;

	private AudioSource audioSource;

	private void Awake()
	{
		isAnimate = false;
	}

	protected override void Start()
	{
		base.Start();
		detectRadius = 15f;
		followDistance = detectRadius;
		audioSource = GetComponent<AudioSource>();
	}

	protected override void Update()
	{
		base.Update();
		float num = Vector3.Distance(base.transform.position, player.position);
		if (num <= detectRadius)
		{
			Vector3 direction = base.transform.GetChild(0).transform.TransformDirection(Vector3.forward);
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.GetChild(0).transform.position, direction, out hitInfo, layer_mask) && hitInfo.transform.CompareTag("Player") && player.GetComponent<PlayerComponent>().isRun && !player.GetComponent<PlayerComponent>().isCalculating && !isAngry)
			{
				audioSource.Play();
				isAngry = true;
				MakeAngry();
			}
		}
	}

	protected override void Сaught()
	{
		if (isAngry)
		{
			base.Сaught();
			player.GetComponent<PlayerController>().TeleportToPoint(prisonPosition.position);
			isAngry = false;
			MakeHappy();
		}
	}
}
