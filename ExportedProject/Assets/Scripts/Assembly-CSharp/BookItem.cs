using UnityEngine;

public class BookItem : Item
{
	public bool isCalculator;

	public Calculator calculator;

	public GameObject canvasInterface;

	public PlayerController plauerController;

	public GameObject blackFon;

	protected override void Action(Collider coll)
	{
		coll.GetComponent<BonusController>().AddBonus(this);
		if (isCalculator)
		{
			playerComponent.tryingCalc++;
			playerComponent.isCalculating = true;
			blackFon.SetActive(true);
			canvasInterface.SetActive(false);
			calculator.gameObject.SetActive(true);
			plauerController.enabled = false;
			playerComponent.isRun = false;
		}
		Object.Destroy(base.gameObject);
		base.Action(coll);
	}
}
