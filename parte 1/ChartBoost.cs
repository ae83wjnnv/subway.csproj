using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class ChartBoost : MonoBehaviour
{
	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000013 RID: 19 RVA: 0x00002215 File Offset: 0x00000415
	public static bool isInitialized
	{
		get
		{
			return ChartBoost.bridgeDelegate != null;
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002222 File Offset: 0x00000422
	private void OnDestroy()
	{
		if (ChartBoost.bridgeDelegate == this)
		{
			ChartBoost.bridgeDelegate = null;
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002237 File Offset: 0x00000437
	private void invokeHandlerIfNotNull(Action handler)
	{
		if (handler != null)
		{
			handler();
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002242 File Offset: 0x00000442
	private void bridge_didFailToLoadInterstitial()
	{
		this.invokeHandlerIfNotNull(ChartBoost.didFailToLoadInterstitial);
	}

	// Token: 0x06000017 RID: 23 RVA: 0x0000224F File Offset: 0x0000044F
	private void bridge_didDismissInterstitial()
	{
		this.invokeHandlerIfNotNull(ChartBoost.didDismissInterstitial);
	}

	// Token: 0x06000018 RID: 24 RVA: 0x0000225C File Offset: 0x0000045C
	private void bridge_didCloseInterstitial()
	{
		this.invokeHandlerIfNotNull(ChartBoost.didCloseInterstitial);
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002269 File Offset: 0x00000469
	private void bridge_didClickInterstitial()
	{
		this.invokeHandlerIfNotNull(ChartBoost.didClickInterstitial);
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002276 File Offset: 0x00000476
	private void bridge_didFailToLoadMoreApps()
	{
		this.invokeHandlerIfNotNull(ChartBoost.didFailToLoadMoreApps);
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002283 File Offset: 0x00000483
	private void bridge_didDismissMoreApps()
	{
		this.invokeHandlerIfNotNull(ChartBoost.didDismissMoreApps);
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002290 File Offset: 0x00000490
	private void bridge_didCloseMoreApps()
	{
		this.invokeHandlerIfNotNull(ChartBoost.didCloseMoreApps);
	}

	// Token: 0x0600001D RID: 29 RVA: 0x0000229D File Offset: 0x0000049D
	private void bridge_didClickMoreApps()
	{
		this.invokeHandlerIfNotNull(ChartBoost.didClickMoreApps);
	}

	// Token: 0x0600001E RID: 30 RVA: 0x000022AA File Offset: 0x000004AA
	public static void InitAndStartSession(string appId, string appSignature)
	{
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000022AC File Offset: 0x000004AC
	public static void CacheInterstitial()
	{
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000022AE File Offset: 0x000004AE
	public static void CacheInterstitial(string location)
	{
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000022B0 File Offset: 0x000004B0
	public static void ShowInterstitial()
	{
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000022B2 File Offset: 0x000004B2
	public static void ShowInterstitial(string location)
	{
	}

	// Token: 0x06000023 RID: 35 RVA: 0x000022B4 File Offset: 0x000004B4
	public static bool HasCachedInterstitial()
	{
		return ChartBoost.bridge_hasCachedInterstitial();
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000022BB File Offset: 0x000004BB
	public static bool HasCachedInterstitial(string location)
	{
		return ChartBoost.bridge_hasCachedInterstitialLocation(location);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000022C3 File Offset: 0x000004C3
	public static void CacheMoreApps()
	{
	}

	// Token: 0x06000026 RID: 38 RVA: 0x000022C5 File Offset: 0x000004C5
	public static void ShowMoreApps()
	{
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000022C7 File Offset: 0x000004C7
	public static void SetIdentityHidden(bool hidden)
	{
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000022C9 File Offset: 0x000004C9
	public static bool IsIdentityHidden()
	{
		return ChartBoost.bridge_isIdentityHidden();
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000022D0 File Offset: 0x000004D0
	public static bool GetShouldRequestInterstitial()
	{
		return ChartBoost.bridge_getShouldRequestInterstitial();
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000022D7 File Offset: 0x000004D7
	public static void SetShouldRequestInterstitial(bool should)
	{
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000022D9 File Offset: 0x000004D9
	public static bool GetShouldDisplayInterstitial()
	{
		return ChartBoost.bridge_getShouldDisplayInterstitial();
	}

	// Token: 0x0600002C RID: 44 RVA: 0x000022E0 File Offset: 0x000004E0
	public static void SetShouldDisplayInterstitial(bool should)
	{
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000022E2 File Offset: 0x000004E2
	public static bool GetShouldDisplayLoadingViewForMoreApps()
	{
		return ChartBoost.bridge_getShouldDisplayLoadingViewForMoreApps();
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000022E9 File Offset: 0x000004E9
	public static void SetShouldDisplayLoadingViewForMoreApps(bool should)
	{
	}

	// Token: 0x0600002F RID: 47 RVA: 0x000022EB File Offset: 0x000004EB
	public static bool GetShouldDisplayMoreApps()
	{
		return ChartBoost.bridge_getShouldDisplayMoreApps();
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000022F2 File Offset: 0x000004F2
	public static void SetShouldDisplayMoreApps(bool should)
	{
	}

	// Token: 0x06000031 RID: 49
	[DllImport("__Internal")]
	private static extern void bridge_initAndStartSession(string appId, string appSignature);

	// Token: 0x06000032 RID: 50
	[DllImport("__Internal")]
	private static extern void bridge_cacheInterstitial();

	// Token: 0x06000033 RID: 51
	[DllImport("__Internal")]
	private static extern void bridge_cacheInterstitialLocation(string location);

	// Token: 0x06000034 RID: 52
	[DllImport("__Internal")]
	private static extern void bridge_showInterstitial();

	// Token: 0x06000035 RID: 53
	[DllImport("__Internal")]
	private static extern void bridge_showInterstitialLocation(string location);

	// Token: 0x06000036 RID: 54
	[DllImport("__Internal")]
	private static extern bool bridge_hasCachedInterstitial();

	// Token: 0x06000037 RID: 55
	[DllImport("__Internal")]
	private static extern bool bridge_hasCachedInterstitialLocation(string location);

	// Token: 0x06000038 RID: 56
	[DllImport("__Internal")]
	private static extern void bridge_cacheMoreApps();

	// Token: 0x06000039 RID: 57
	[DllImport("__Internal")]
	private static extern void bridge_showMoreApps();

	// Token: 0x0600003A RID: 58
	[DllImport("__Internal")]
	private static extern void bridge_setIdentityHidden(bool hidden);

	// Token: 0x0600003B RID: 59
	[DllImport("__Internal")]
	private static extern bool bridge_isIdentityHidden();

	// Token: 0x0600003C RID: 60
	[DllImport("__Internal")]
	private static extern bool bridge_getShouldRequestInterstitial();

	// Token: 0x0600003D RID: 61
	[DllImport("__Internal")]
	private static extern void bridge_setShouldRequestInterstitial(bool should);

	// Token: 0x0600003E RID: 62
	[DllImport("__Internal")]
	private static extern bool bridge_getShouldDisplayInterstitial();

	// Token: 0x0600003F RID: 63
	[DllImport("__Internal")]
	private static extern void bridge_setShouldDisplayInterstitial(bool should);

	// Token: 0x06000040 RID: 64
	[DllImport("__Internal")]
	private static extern bool bridge_getShouldDisplayLoadingViewForMoreApps();

	// Token: 0x06000041 RID: 65
	[DllImport("__Internal")]
	private static extern void bridge_setShouldDisplayLoadingViewForMoreApps(bool should);

	// Token: 0x06000042 RID: 66
	[DllImport("__Internal")]
	private static extern bool bridge_getShouldDisplayMoreApps();

	// Token: 0x06000043 RID: 67
	[DllImport("__Internal")]
	private static extern void bridge_setShouldDisplayMoreApps(bool should);

	// Token: 0x0400000B RID: 11
	private const string BRIDGE_DELEGATE_GAMEOBJECT_NAME = "ChartBoostBridge";

	// Token: 0x0400000C RID: 12
	private static ChartBoost bridgeDelegate;

	// Token: 0x0400000D RID: 13
	public static Action didFailToLoadInterstitial;

	// Token: 0x0400000E RID: 14
	public static Action didDismissInterstitial;

	// Token: 0x0400000F RID: 15
	public static Action didCloseInterstitial;

	// Token: 0x04000010 RID: 16
	public static Action didClickInterstitial;

	// Token: 0x04000011 RID: 17
	public static Action didFailToLoadMoreApps;

	// Token: 0x04000012 RID: 18
	public static Action didDismissMoreApps;

	// Token: 0x04000013 RID: 19
	public static Action didCloseMoreApps;

	// Token: 0x04000014 RID: 20
	public static Action didClickMoreApps;
}
