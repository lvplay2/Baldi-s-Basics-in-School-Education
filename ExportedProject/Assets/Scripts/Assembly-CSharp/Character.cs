using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
	public bool isAngry;

	protected Animator anim;

	protected NavMeshAgent agent;

	public bool isAnimate;

	public Transform player;

	public GameHelper gameHelper;

	protected Vector3 stayPosition;

	protected float dieDistance;

	public float followDistance;

	public List<Transform> wayPoints;

	protected int currentWayPoint;

	protected int layer_mask;

	protected virtual void Start()
	{
		layer_mask = LayerMask.GetMask("Player", "Enemy");
		agent = GetComponent<NavMeshAgent>();
		if (isAnimate)
		{
			anim = base.transform.GetChild(0).GetComponent<Animator>();
		}
		MakeHappy();
		stayPosition = base.transform.position;
		dieDistance = GetComponent<NavMeshAgent>().stoppingDistance;
		agent.speed = 2.8f;
		RandomWayPoints();
	}

	protected virtual void Update()
	{
		if (Vector3.Distance(base.transform.position, player.position) <= dieDistance && !player.GetComponent<PlayerComponent>().isCalculating && isAngry)
		{
			Vector3 direction = base.transform.GetChild(0).transform.TransformDirection(Vector3.forward);
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.GetChild(0).transform.position, direction, out hitInfo, layer_mask) && hitInfo.transform.CompareTag("Player") && Vector3.Distance(base.transform.position, player.position) <= dieDistance && !player.GetComponent<PlayerComponent>().isCalculating)
			{
				Сaught();
			}
		}
		if (Vector3.Distance(base.transform.position, player.position) > followDistance)
		{
			isAngry = false;
			MakeHappy();
		}
		if (!isAngry)
		{
			PatrolWayPoints();
		}
	}

	protected virtual void Сaught()
	{
	}

	public virtual void MakeAngry()
	{
		isAngry = true;
		if (isAnimate)
		{
			anim.SetBool("isAngry", true);
		}
		CancelInvoke();
		InvokeRepeating("SetPath", 0f, 1f);
	}

	public virtual void MakeHappy()
	{
		isAngry = false;
		if (isAnimate)
		{
			anim.SetBool("isAngry", false);
		}
		CancelInvoke();
		InvokeRepeating("ResetTarget", 0f, 1f);
	}

	protected void ResetTarget()
	{
		agent.SetDestination(new Vector3(stayPosition.x, base.transform.position.y, stayPosition.z));
	}

	protected virtual void PatrolWayPoints()
	{
		if (currentWayPoint >= wayPoints.Count)
		{
			currentWayPoint = 0;
		}
		float num = Vector3.Distance(base.transform.position, wayPoints[currentWayPoint].position);
		if (num <= dieDistance)
		{
			currentWayPoint++;
			if (currentWayPoint > wayPoints.Count)
			{
				currentWayPoint = 0;
			}
		}
		else
		{
			InvokeRepeating("SetPathWaypoint", 0f, 1f);
		}
	}

	protected virtual void RandomWayPoints()
	{
		for (int i = 0; i < wayPoints.Count; i++)
		{
			int index = Random.Range(0, wayPoints.Count);
			Transform value = wayPoints[index];
			wayPoints[index] = wayPoints[i];
			wayPoints[i] = value;
		}
	}

	protected void SetPathWaypoint()
	{
		agent.SetDestination(wayPoints[currentWayPoint].position);
	}

	protected void SetPath()
	{
		agent.SetDestination(player.position);
	}
}
