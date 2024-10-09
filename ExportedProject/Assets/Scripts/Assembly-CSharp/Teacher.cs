using UnityEngine;

public class Teacher : Character
{
	public bool giveCoin;

	public Transform giveCoinPoint;

	private AudioSource audioSource;

	protected override void Start()
	{
		base.Start();
		followDistance = 25f;
		agent.speed = 2.5f;
		audioSource = GetComponent<AudioSource>();
	}

	protected override void Update()
	{
		base.Update();
		if (giveCoin && Vector3.Distance(base.transform.position, giveCoinPoint.position) < 6f && !isAngry)
		{
			giveCoin = false;
			for (int i = 0; i < player.GetComponent<BonusController>().coinOnScene.Count; i++)
			{
				if (player.GetComponent<BonusController>().coinOnScene.Count > 0 && !player.GetComponent<BonusController>().coinOnScene[i].activeInHierarchy)
				{
					player.GetComponent<BonusController>().coinOnScene[i].SetActive(true);
					break;
				}
			}
		}
		if (isAngry)
		{
			player.GetComponent<AudioSource>().enabled = false;
		}
		else if (!isAngry && player.GetComponent<BonusController>().bonuses.Count > 0)
		{
			player.GetComponent<AudioSource>().enabled = true;
		}
		if (!isAngry)
		{
			ResetTarget();
		}
		if (isAngry && !audioSource.enabled)
		{
			audioSource.enabled = true;
		}
		if (!isAngry && audioSource.enabled)
		{
			audioSource.enabled = false;
		}
	}

	protected override void PatrolWayPoints()
	{
	}

	protected override void Сaught()
	{
		base.Сaught();
		if (gameHelper.gameState != GameHelper.enGameState.GAME_OVER)
		{
			gameHelper.GameOver();
			MakeHappy();
			isAngry = false;
		}
	}
}
