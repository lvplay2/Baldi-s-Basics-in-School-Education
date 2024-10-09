using Heyzap;
using UnityEngine;
using UnityEngine.Events;

public class HZController : MonoBehaviour
{
	public enum enTypeBanner
	{
		TOP = 0,
		BOTTOM = 1
	}

	public string publisherId = "4356a0f83771542c27d9a97a8512aaa5";

	[Header("Banner")]
	public bool loadBanerOnStart = true;

	public enTypeBanner typeBanner = enTypeBanner.BOTTOM;

	[Header("Revarded video")]
	public UnityEvent rewardedVideoComplete;

	public UnityEvent rewardedVideoIncomplete;

	[Header("Other")]
	public bool testHeyzapOnStart;

	public static HZController singleton;

	private void Awake()
	{
		if (singleton == null)
		{
			singleton = this;
		}
		HeyzapAds.Start(publisherId, 0);
		HZVideoAd.Fetch();
		HZIncentivizedAd.Fetch();
	}

	private void Start()
	{
		AddEvents();
		if (testHeyzapOnStart)
		{
			HeyzapAds.ShowMediationTestSuite();
		}
		if (loadBanerOnStart)
		{
			ShowBanner();
		}
	}

	private void AddEvents()
	{
		HZIncentivizedAd.AdDisplayListener displayListener = delegate(string adState, string adTag)
		{
			if (adState.Equals("incentivized_result_complete"))
			{
				rewardedVideoComplete.Invoke();
			}
			if (adState.Equals("incentivized_result_incomplete"))
			{
				rewardedVideoIncomplete.Invoke();
			}
		};
		HZIncentivizedAd.SetDisplayListener(displayListener);
	}

	public bool InterstitialAds()
	{
		if (HZInterstitialAd.IsAvailable())
		{
			HZInterstitialAd.Show();
			return true;
		}
		Debug.LogWarning("InterstitialAds not available");
		return false;
	}

	public bool VideoAds()
	{
		if (HZVideoAd.IsAvailable())
		{
			HZVideoAd.Show();
			return true;
		}
		Debug.LogWarning("VideoAds not available");
		return false;
	}

	public bool RewardedVideoAds()
	{
		if (HZIncentivizedAd.IsAvailable())
		{
			HZIncentivizedAd.Show();
			return true;
		}
		Debug.LogWarning("RevardedVideoAds not available");
		return false;
	}

	public void ShowBanner()
	{
		HZBannerShowOptions hZBannerShowOptions = new HZBannerShowOptions();
		hZBannerShowOptions.Position = ((typeBanner != 0) ? "bottom" : "top");
		HZBannerAd.ShowWithOptions(hZBannerShowOptions);
	}

	public void HideBanner()
	{
		HZBannerAd.Hide();
	}
}
