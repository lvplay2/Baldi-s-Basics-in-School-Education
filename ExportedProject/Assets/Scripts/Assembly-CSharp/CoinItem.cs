using UnityEngine;

public class CoinItem : Item
{
	protected override void Action(Collider coll)
	{
		playerComponent.CoinCount++;
		playerComponent.GetComponent<BonusController>().RemoveCoin(base.gameObject);
		playerComponent.GetComponent<BonusController>().UpdateQuestFinish();
		base.Action(coll);
	}
}
