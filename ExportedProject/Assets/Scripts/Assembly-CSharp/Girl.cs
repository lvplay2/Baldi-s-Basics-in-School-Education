using System.Collections;
using UnityEngine;

public class Girl : Character
{
	public float detectRadius;

	internal bool isCaught;

	public int jumpCount = 5;

	public float resetTime = 10f;

	private bool stay;

	private AudioSource audioSource;

	protected override void Update()
	{
		base.Update();
		float num = Vector3.Distance(base.transform.position, player.position);
		if (num <= detectRadius)
		{
			Vector3 direction = base.transform.GetChild(0).transform.TransformDirection(Vector3.forward);
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.GetChild(0).transform.position, direction, out hitInfo, layer_mask) && hitInfo.transform.CompareTag("Player") && !isCaught && !player.GetComponent<PlayerComponent>().isCalculating && !isAngry)
			{
				isAngry = true;
				MakeAngry();
			}
		}
	}

	private void Awake()
	{
		isAnimate = false;
	}

	protected override void Start()
	{
		base.Start();
		wayPoints.Reverse();
		detectRadius = 15f;
		followDistance = detectRadius;
		resetTime = 20f;
		agent.speed = 1.8f;
		audioSource = GetComponent<AudioSource>();
	}

	protected override void Сaught()
	{
		audioSource.Play();
		base.Сaught();
		stay = true;
		isCaught = true;
		isAngry = false;
		StartCoroutine(JumpingRope());
	}

	protected override void PatrolWayPoints()
	{
		if (!stay)
		{
			base.PatrolWayPoints();
		}
		else
		{
			agent.SetDestination(base.transform.position);
		}
	}

	public void RestTimeStart()
	{
		StartCoroutine(resetTimes());
	}

	private void ResetOpt()
	{
		isCaught = false;
	}

	private IEnumerator JumpingRope()
	{
		player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
		player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
		player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
		player.GetComponent<PlayerController>().enabled = false;
		player.GetComponent<CharacterController>().enabled = false;
		player.transform.GetChild(0).GetComponent<Animator>().SetBool("jumpRope", true);
		for (int i = 0; i < jumpCount; i++)
		{
			player.position = new Vector3(player.position.x, player.position.y + 1f, player.position.z);
			yield return new WaitForSeconds(1f);
		}
		stay = false;
		player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		MakeHappy();
		player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
		player.GetComponent<CharacterController>().enabled = true;
		player.GetComponent<PlayerController>().enabled = true;
		player.transform.GetChild(0).GetComponent<Animator>().SetBool("jumpRope", false);
		RestTimeStart();
	}

	private IEnumerator resetTimes()
	{
		yield return new WaitForSeconds(resetTime);
		ResetOpt();
	}
}
