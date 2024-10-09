using UnityEngine;
using UnityEngine.UI;

public class Bsoda : MonoBehaviour
{
	public PlayerComponent playerComponent;

	public float actionDistance = 2f;

	public Button buttonBuy;

	private void Start()
	{
		buttonBuy.onClick.RemoveAllListeners();
		buttonBuy.onClick.AddListener(BuyButton);
	}

	private void Update()
	{
		float num = Vector3.Distance(base.transform.position, playerComponent.transform.position);
		if (num <= actionDistance && playerComponent.CoinCount > 0)
		{
			buttonBuy.gameObject.SetActive(true);
		}
		else
		{
			buttonBuy.gameObject.SetActive(false);
		}
	}

	private void BuyButton()
	{
		playerComponent.CoinCount--;
		playerComponent.BsodaCount++;
	}
}
