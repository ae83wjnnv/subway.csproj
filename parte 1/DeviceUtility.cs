using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class DeviceUtility
{
	// Token: 0x06000045 RID: 69
	[DllImport("__Internal")]
	private static extern string utilityGetBundleVersion();

	// Token: 0x06000046 RID: 70
	[DllImport("__Internal")]
	private static extern bool utilityOpenUrl(string url);

	// Token: 0x06000047 RID: 71
	[DllImport("__Internal")]
	private static extern bool utilityIsOtherAudioPlaying();

	// Token: 0x06000048 RID: 72
	[DllImport("__Internal")]
	private static extern void utilityShowNativePopup(string title, string message, string cancelButtonTitle);

	// Token: 0x06000049 RID: 73
	[DllImport("__Internal")]
	private static extern void utilityShowNativePopupWithCallback(string callbackGameObjectName, string callbackDidCloseFunctionName, string title, string message, string cancelButtonTitle, string optionalButton2, string optionalButton3);

	// Token: 0x0600004A RID: 74 RVA: 0x000022FC File Offset: 0x000004FC
	public static string GetBundleVersion()
	{
		return "1.0.1";
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00002303 File Offset: 0x00000503
	public static void showNativePopup(string title, string message, string cancelButtonTitle)
	{
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00002305 File Offset: 0x00000505
	public static void showNativePopupWithCallback(string callbackGameObjectName, string callbackDidCloseFunctionName, string title, string message, string cancelButtonTitle, string optionalButton2, string optionalButton3)
	{
	}

	// Token: 0x04000015 RID: 21
	public static bool DISABLE_ALL_PLUGINS = Application.isEditor;

	// Token: 0x04000016 RID: 22
	private static bool disable = DeviceUtility.DISABLE_ALL_PLUGINS;
}
