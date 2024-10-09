using UnityEngine;

public class Item : MonoBehaviour
{
	protected PlayerComponent playerComponent;

	protected void Start()
	{
		playerComponent = Object.FindObjectOfType<PlayerComponent>();
	}

	protected void OnTriggerEnter(Collider coll)
	{
		if (coll.transform.tag == "Player")
		{
			Action(coll);
		}
	}

	protected virtual void Action(Collider coll)
	{
		Object.Destroy(base.gameObject);
	}
}
