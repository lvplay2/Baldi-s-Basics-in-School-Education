public class Cleaner : Character
{
	protected override void Start()
	{
		base.Start();
		agent.speed = 12f;
	}

	protected override void Update()
	{
		if (isAngry)
		{
			PatrolWayPoints();
		}
	}

	protected override void Сaught()
	{
	}
}
