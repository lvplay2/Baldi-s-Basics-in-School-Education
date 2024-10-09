using UnityEngine;

public class EneregyzerItem : Item
{
	protected override void Action(Collider coll)
	{
		base.Action(coll);
		coll.transform.GetComponent<PlayerComponent>().Energy = 1f;
		base.Action(coll);
	}
}
