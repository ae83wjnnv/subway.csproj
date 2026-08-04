using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class AdColonyCallbackHandler : MonoBehaviour
{
	// Token: 0x0600014F RID: 335 RVA: 0x00005240 File Offset: 0x00003440
	private void Awake()
	{
		AdColony.takeoverBegan = (Action)Delegate.Combine(AdColony.takeoverBegan, new Action(this.TakeOverBegan));
		AdColony.takeoverEndedWithVC = (Action<bool>)Delegate.Combine(AdColony.takeoverEndedWithVC, new Action<bool>(this.TakeOverEndedWithVC));
		AdColony.videoAdNotServed = (Action)Delegate.Combine(AdColony.videoAdNotServed, new Action(this.VideoAdNotServed));
		AdColony.virtualCurrencyAwarded = (Action<string, int>)Delegate.Combine(AdColony.virtualCurrencyAwarded, new Action<string, int>(this.VirtualCurrencyAwarded));
	}

	// Token: 0x06000150 RID: 336 RVA: 0x000052D0 File Offset: 0x000034D0
	private void OnDestroy()
	{
		AdColony.takeoverBegan = (Action)Delegate.Remove(AdColony.takeoverBegan, new Action(this.TakeOverBegan));
		AdColony.takeoverEndedWithVC = (Action<bool>)Delegate.Remove(AdColony.takeoverEndedWithVC, new Action<bool>(this.TakeOverEndedWithVC));
		AdColony.videoAdNotServed = (Action)Delegate.Remove(AdColony.videoAdNotServed, new Action(this.VideoAdNotServed));
	}

	// Token: 0x06000151 RID: 337 RVA: 0x0000533D File Offset: 0x0000353D
	private void TakeOverBegan()
	{
		this.adColonySoundOnBeforeMute = Settings.optionSound;
		Settings.optionSound = false;
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00005350 File Offset: 0x00003550
	private void TakeOverEndedWithVC(bool withVirtualCurrency)
	{
		Settings.optionSound = this.adColonySoundOnBeforeMute;
	}

	// Token: 0x06000153 RID: 339 RVA: 0x0000535D File Offset: 0x0000355D
	private void VideoAdNotServed()
	{
		DeviceUtility.showNativePopup("No videos available", "We are currently out of videos to show you. Please try again later.", "OK");
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00005373 File Offset: 0x00003573
	private void VirtualCurrencyAwarded(string currency, int amount)
	{
		PlayerInfo.Instance.amountOfCoins += amount;
		PlayerInfo.Instance.Save();
	}

	// Token: 0x04000071 RID: 113
	public const string ADCOLONY_APPID = "app2568a30bc18f470288d36d";

	// Token: 0x04000072 RID: 114
	public const string ADCOLONY_ZONEID = "vz714b7567808540889e4a44";

	// Token: 0x04000073 RID: 115
	private const string ADCOLONY_NOVIDEOS_NATIVE_POPUP_TITLE = "No videos available";

	// Token: 0x04000074 RID: 116
	private const string ADCOLONY_NOVIDEOS_NATIVE_POPUP_MESSAGE = "We are currently out of videos to show you. Please try again later.";

	// Token: 0x04000075 RID: 117
	private const string ADCOLONY_NOVIDEOS_NATIVE_POPUP_OKBUTTONTEXT = "OK";

	// Token: 0x04000076 RID: 118
	private bool adColonySoundOnBeforeMute;
}
