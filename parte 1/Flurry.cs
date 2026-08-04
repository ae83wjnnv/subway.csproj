using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000006 RID: 6
public static class Flurry
{
	// Token: 0x06000078 RID: 120 RVA: 0x000024C0 File Offset: 0x000006C0
	public static void LogGenericSocialAction()
	{
		int num = PlayerPrefs.GetInt("flurry_social_total", 0);
		int num2 = PlayerPrefs.GetInt("flurry_social_unlogged", 0);
		num++;
		num2++;
		Debug.Log("LogGenericSocialAction: new unlogged total = " + num2.ToString());
		if (num2 == 10)
		{
			num2 = 0;
			Flurry.LogEventWithAParameter("10 social actions taken", "Total", num.ToString());
		}
		PlayerPrefs.SetInt("flurry_social_total", num);
		PlayerPrefs.SetInt("flurry_social_unlogged", num2);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00002536 File Offset: 0x00000736
	public static void LogGameCenterLogin()
	{
		if (!PlayerPrefs.HasKey("flurry_has_logged_gc"))
		{
			Flurry.LogEvent("First GameCenter Login");
			PlayerPrefs.SetInt("flurry_has_logged_gc", 1);
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00002559 File Offset: 0x00000759
	public static void LogFacebookLogin()
	{
		if (!PlayerPrefs.HasKey("flurry_has_logged_fb"))
		{
			Flurry.LogEvent("First Facebook Login");
			PlayerPrefs.SetInt("flurry_has_logged_fb", 1);
		}
	}

	// Token: 0x0600007B RID: 123
	[DllImport("__Internal")]
	private static extern void flurryStartSession(string apiKey);

	// Token: 0x0600007C RID: 124
	[DllImport("__Internal")]
	private static extern void flurrySetUserInfo(string userId, int age, int gender);

	// Token: 0x0600007D RID: 125
	[DllImport("__Internal")]
	private static extern void flurryLogEvent(string eventName);

	// Token: 0x0600007E RID: 126
	[DllImport("__Internal")]
	private static extern void flurryLogEventWithAParameter(string eventName, string argKey, string argValue);

	// Token: 0x0600007F RID: 127
	[DllImport("__Internal")]
	private static extern void flurryLogEventWithSeveralParameters(string eventName, string argKeys, string argValues);

	// Token: 0x06000080 RID: 128
	[DllImport("__Internal")]
	private static extern void flurryLogError(string errorName, string message);

	// Token: 0x06000081 RID: 129 RVA: 0x0000257C File Offset: 0x0000077C
	public static void StartSession(string apiKey)
	{
		bool flag = Flurry.inSession;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00002584 File Offset: 0x00000784
	public static void SetUserInfo(string userId)
	{
		Flurry.SetUserInfo(userId, 0, 0);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x0000258E File Offset: 0x0000078E
	public static void SetUserInfo(string userId, int age, int gender)
	{
		bool flag = Flurry.inSession;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00002596 File Offset: 0x00000796
	public static void LogEvent(string eventName)
	{
		bool flag = Flurry.inSession;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x0000259E File Offset: 0x0000079E
	public static void LogEventWithAParameter(string eventName, string argKey, string argValue)
	{
		bool flag = Flurry.inSession;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x000025A6 File Offset: 0x000007A6
	public static void LogEventWithSeveralParameters(string eventName, string argKeys, string argValues)
	{
		bool flag = Flurry.inSession;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x000025AE File Offset: 0x000007AE
	public static void LogError(string errorName, string message)
	{
		bool flag = Flurry.inSession;
	}

	// Token: 0x04000017 RID: 23
	public const string EVENT_UISCREEN_CHANGED_PREFIX = "UI Screen ";

	// Token: 0x04000018 RID: 24
	public const string EVENT_10_SOCIAL_ACTIONS_TAKEN = "10 social actions taken";

	// Token: 0x04000019 RID: 25
	public const string EVENT_FIRST_GAMECENTER_LOGIN = "First GameCenter Login";

	// Token: 0x0400001A RID: 26
	public const string EVENT_FIRST_FACEBOOK_LOGIN = "First Facebook Login";

	// Token: 0x0400001B RID: 27
	public const string EVENT_MYSTERY_BOX_OPENED = "Mystery Box opened";

	// Token: 0x0400001C RID: 28
	public const string EVENT_INAPPPURCHASE_COMPLETED = "InApp purchase completed";

	// Token: 0x0400001D RID: 29
	public const string EVENT_INAPPPURCHASE_COINPACK1 = "InApp Coin Pack 1 purchased";

	// Token: 0x0400001E RID: 30
	public const string EVENT_INAPPPURCHASE_COINPACK2 = "InApp Coin Pack 2 purchased";

	// Token: 0x0400001F RID: 31
	public const string EVENT_INAPPPURCHASE_COINPACK3 = "InApp Coin Pack 3 purchased";

	// Token: 0x04000020 RID: 32
	public const string EVENT_CHARACTER_UNLOCKED = "Character unlocked";

	// Token: 0x04000021 RID: 33
	public const string EVENT_AUTOMESSAGE_TURNED_OFF = "AutoBrag turned off";

	// Token: 0x04000022 RID: 34
	public const string EVENT_MISSIONSET_COMPLETED = "Mission Set completed";

	// Token: 0x04000023 RID: 35
	public const string EVENT_DAILY_CHALLENGE_COMPLETED = "Daily Challenge completed";

	// Token: 0x04000024 RID: 36
	public const string EVENT_BOOST_HEADSTART500_PURCHASED = "Boost Headstart500 purchased";

	// Token: 0x04000025 RID: 37
	public const string EVENT_BOOST_HEADSTART2000_PURCHASED = "Boost Headstart2000 purchased";

	// Token: 0x04000026 RID: 38
	public const string EVENT_BOOST_HOVERBOARD_PURCHASED = "Boost Hoverboard purchased";

	// Token: 0x04000027 RID: 39
	public const string EVENT_BOOST_COINMAGNET_PURCHASED = "Boost Coinmagnet purchased";

	// Token: 0x04000028 RID: 40
	public const string EVENT_BOOST_DOUBLEMULTIPLIER_PURCHASED = "Boost 2x multiplier purchased";

	// Token: 0x04000029 RID: 41
	public const string EVENT_BOOST_JETPACK_PURCHASED = "Boost jetpack purchased";

	// Token: 0x0400002A RID: 42
	public const string EVENT_BOOST_LETTERS_PURCHASED = "Boost letters purchased";

	// Token: 0x0400002B RID: 43
	public const string EVENT_BOOST_SUPERSNEAKERS_PURCHASED = "Boost supersneakers purchased";

	// Token: 0x0400002C RID: 44
	public const string EVENT_BOOST_MYSTERYBOX_PURCHASED = "Boost MysteryBox purchased";

	// Token: 0x0400002D RID: 45
	public const string EVENT_BOOST_MISSION_SKIP_PURCHASED = "Boost Mission Skip purchased";

	// Token: 0x0400002E RID: 46
	public const string EVENT_ARGKEY_ID = "Id";

	// Token: 0x0400002F RID: 47
	public const string EVENT_ARGKEY_TIER = "Tier";

	// Token: 0x04000030 RID: 48
	public const string EVENT_ARGKEY_UI_SCREENNAME = "Screen Name";

	// Token: 0x04000031 RID: 49
	public const string EVENT_ARGKEY_MISSIONSET = "Mission Set";

	// Token: 0x04000032 RID: 50
	public const string EVENT_ARGKEY_MISSIONSET_AND_INDEX = "Mission Set and Index";

	// Token: 0x04000033 RID: 51
	public const string EVENT_ARGKEY_TOTAL = "Total";

	// Token: 0x04000034 RID: 52
	private const bool disable = true;

	// Token: 0x04000035 RID: 53
	private static bool inSession;
}
