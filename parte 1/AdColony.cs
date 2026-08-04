using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class AdColony : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static bool isInitialized
	{
		get
		{
			return AdColony.bridgeDelegate != null;
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000205D File Offset: 0x0000025D
	private void OnDestroy()
	{
		if (AdColony.bridgeDelegate == this)
		{
			AdColony.bridgeDelegate = null;
		}
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002072 File Offset: 0x00000272
	private void invokeHandlerIfNotNull(Action handler)
	{
		if (handler != null)
		{
			handler();
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000207D File Offset: 0x0000027D
	private void bridge_noVideoFill()
	{
		this.invokeHandlerIfNotNull(AdColony.noVideoFill);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000208A File Offset: 0x0000028A
	private void bridge_videoAdsReady()
	{
		this.invokeHandlerIfNotNull(AdColony.videoAdsReady);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002097 File Offset: 0x00000297
	private void bridge_videoAdsNotReady()
	{
		this.invokeHandlerIfNotNull(AdColony.videoAdsNotReady);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000020A4 File Offset: 0x000002A4
	private void bridge_virtualCurrencyAwarded(string message)
	{
		Action<string, int> action = AdColony.virtualCurrencyAwarded;
		if (action == null)
		{
			return;
		}
		string[] array = message.Split(new char[] { ';' });
		if (array.Length != 2)
		{
			Debug.LogError("bridge_virtualCurrencyAwarded: Failed to parse message: " + message);
			return;
		}
		int num = 0;
		if (int.TryParse(array[1], out num))
		{
			string text = array[0];
			action(text, num);
			return;
		}
		Debug.LogError("bridge_virtualCurrencyAwarded: Failed to parse amount: " + message);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002110 File Offset: 0x00000310
	private void bridge_virtualCurrencyNotAwarded(string message)
	{
		Action<string, int, string> action = AdColony.virtualCurrencyNotAwarded;
		if (action == null)
		{
			return;
		}
		string[] array = message.Split(new char[] { ';' });
		if (array.Length != 3)
		{
			Debug.LogError("bridge_virtualCurrencyNotAwarded: Failed to parse message: " + message);
			return;
		}
		int num = 0;
		if (int.TryParse(array[1], out num))
		{
			string text = array[0];
			string text2 = array[2];
			action(text, num, text2);
			return;
		}
		Debug.LogError("bridge_virtualCurrencyNotAwarded: Failed to parse amount: " + message);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002182 File Offset: 0x00000382
	private void bridge_takeoverBegan()
	{
		this.invokeHandlerIfNotNull(AdColony.takeoverBegan);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002190 File Offset: 0x00000390
	private void bridge_takeoverEndedWithVC(string message)
	{
		Action<bool> action = AdColony.takeoverEndedWithVC;
		if (action != null)
		{
			bool flag = message == "1";
			action(flag);
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000021B9 File Offset: 0x000003B9
	private void bridge_videoAdNotServed()
	{
		this.invokeHandlerIfNotNull(AdColony.videoAdNotServed);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000021C6 File Offset: 0x000003C6
	public static void Init(string appId, string zoneId)
	{
		if (AdColony.bridgeDelegate == null)
		{
			GameObject gameObject = new GameObject("AdColonyBridge");
			Object.DontDestroyOnLoad(gameObject);
			AdColony.bridgeDelegate = gameObject.AddComponent<AdColony>();
		}
		AdColony.bridge_initAdColony(appId, zoneId);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000021F6 File Offset: 0x000003F6
	public static bool VirtualCurrencyAwardAvailable()
	{
		return AdColony.bridge_virtualCurrencyAwardAvailable();
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000021FD File Offset: 0x000003FD
	public static void PlayVideoAdWithPrePopup(bool prePopup, bool postPopup)
	{
		AdColony.bridge_playVideoAd(prePopup, postPopup);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002206 File Offset: 0x00000406
	private static void bridge_initAdColony(string appId, string zoneId)
	{
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002208 File Offset: 0x00000408
	private static bool bridge_virtualCurrencyAwardAvailable()
	{
		return false;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x0000220B File Offset: 0x0000040B
	private static void bridge_playVideoAd(bool prePopup, bool postPopup)
	{
	}

	// Token: 0x04000001 RID: 1
	private const string BRIDGE_DELEGATE_GAMEOBJECT_NAME = "AdColonyBridge";

	// Token: 0x04000002 RID: 2
	private static AdColony bridgeDelegate;

	// Token: 0x04000003 RID: 3
	public static Action noVideoFill;

	// Token: 0x04000004 RID: 4
	public static Action videoAdsReady;

	// Token: 0x04000005 RID: 5
	public static Action videoAdsNotReady;

	// Token: 0x04000006 RID: 6
	public static Action<string, int> virtualCurrencyAwarded;

	// Token: 0x04000007 RID: 7
	public static Action<string, int, string> virtualCurrencyNotAwarded;

	// Token: 0x04000008 RID: 8
	public static Action takeoverBegan;

	// Token: 0x04000009 RID: 9
	public static Action<bool> takeoverEndedWithVC;

	// Token: 0x0400000A RID: 10
	public static Action videoAdNotServed;
}
