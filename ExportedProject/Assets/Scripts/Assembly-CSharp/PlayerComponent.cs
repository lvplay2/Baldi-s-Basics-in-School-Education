using UnityEngine;
using UnityEngine.UI;

public class PlayerComponent : MonoBehaviour
{
	public GameHelper gameHelper;

	public Transform Inventory;

	public Image energyBar;

	public Text bsodaCounter;

	public Button bsodaButton;

	public GameObject bsodaBullet;

	private int coinCount;

	private float energy;

	private int bsodaCount;

	public int tryingCalc { get; set; }

	public int goodTry { get; set; }

	public Vector3 startPos { get; set; }

	public bool isRun { get; set; }

	public bool isCalculating { get; set; }

	public int CoinCount
	{
		get
		{
			return coinCount;
		}
		set
		{
			coinCount = value;
			UpdateCoins();
		}
	}

	public float Energy
	{
		get
		{
			return energy;
		}
		set
		{
			energy = value;
			UpdateBar();
		}
	}

	public int BsodaCount
	{
		get
		{
			return bsodaCount;
		}
		set
		{
			bsodaCount = value;
			UpdateBsoda();
		}
	}

	private void UpdateBsoda()
	{
		bsodaCounter.text = BsodaCount.ToString();
	}

	private void Start()
	{
		startPos = base.transform.position;
		Energy = 1f;
		UpdateBsoda();
		bsodaButton.onClick.RemoveAllListeners();
		bsodaButton.onClick.AddListener(BsodaShot);
	}

	private void UpdateBar()
	{
		energyBar.fillAmount = Energy;
	}

	private void UpdateCoins()
	{
		for (int i = 0; i < Inventory.childCount; i++)
		{
			Inventory.GetChild(i).gameObject.SetActive(false);
		}
		for (int j = 0; j < CoinCount; j++)
		{
			Inventory.GetChild(j).gameObject.SetActive(true);
		}
	}

	private void BsodaShot()
	{
		if (BsodaCount > 0)
		{
			BsodaCount--;
			GameObject gameObject = Object.Instantiate(bsodaBullet, base.transform.position, base.transform.rotation);
		}
	}
}
