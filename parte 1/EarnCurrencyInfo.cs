using System;
using System.Text;
using UnityEngine;

// Token: 0x02000053 RID: 83
public static class EarnCurrencyInfo
{
	// Token: 0x060002C8 RID: 712 RVA: 0x0000C274 File Offset: 0x0000A474
	public static bool ShouldShowInGUI(int profileIndex)
	{
		EarnCurrencyInfo.EarnCurrencyProfile earnCurrencyProfile = EarnCurrencyInfo.profiles[profileIndex];
		string text = null;
		if (earnCurrencyProfile.repeatability != EarnCurrencyInfo.Repeatability.Once && earnCurrencyProfile.repeatability != EarnCurrencyInfo.Repeatability.OncePerVersion)
		{
			return true;
		}
		string text2 = EarnCurrencyInfo.GetProfileData(profileIndex);
		if (string.IsNullOrEmpty(text2))
		{
			return true;
		}
		if (earnCurrencyProfile.repeatability == EarnCurrencyInfo.Repeatability.OncePerVersion)
		{
			if (text == null)
			{
				text = DeviceUtility.GetBundleVersion();
			}
			if (text2 != text)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
	public static void Trigger(int profileIndex)
	{
		EarnCurrencyInfo.EarnCurrencyProfile earnCurrencyProfile = EarnCurrencyInfo.profiles[profileIndex];
		if (earnCurrencyProfile.type == EarnCurrencyInfo.Type.OpenURL)
		{
			if (earnCurrencyProfile.amountOfCoins > 0)
			{
				PlayerInfo.Instance.amountOfCoins += earnCurrencyProfile.amountOfCoins;
			}
		}
		else if (earnCurrencyProfile.type != EarnCurrencyInfo.Type.AdColony)
		{
			Debug.LogError("Unhandled earner type: " + earnCurrencyProfile.type.ToString());
		}
		if (earnCurrencyProfile.repeatability == EarnCurrencyInfo.Repeatability.Once || earnCurrencyProfile.repeatability == EarnCurrencyInfo.Repeatability.OncePerVersion)
		{
			string bundleVersion = DeviceUtility.GetBundleVersion();
			EarnCurrencyInfo.SetAndSaveProfileData(profileIndex, bundleVersion);
		}
		PlayerInfo.Instance.Save();
		if (earnCurrencyProfile.type == EarnCurrencyInfo.Type.OpenURL)
		{
			Application.OpenURL(earnCurrencyProfile.url);
			return;
		}
		if (earnCurrencyProfile.type == EarnCurrencyInfo.Type.AdColony)
		{
			if (!AdColony.isInitialized)
			{
				AdColony.Init("app2568a30bc18f470288d36d", "vz714b7567808540889e4a44");
			}
			AdColony.PlayVideoAdWithPrePopup(true, true);
			return;
		}
		Debug.LogError("Unhandled earner type: " + earnCurrencyProfile.type.ToString());
	}

	// Token: 0x060002CA RID: 714 RVA: 0x0000C3BC File Offset: 0x0000A5BC
	private static void InitProfileDataArrayIfNeeded()
	{
		if (EarnCurrencyInfo.profileData != null)
		{
			return;
		}
		EarnCurrencyInfo.profileData = new string[EarnCurrencyInfo.profiles.Length];
		string earnCurrenyData = PlayerInfo.Instance.earnCurrenyData;
		Debug.Log("EarnCurrenyInfo: Loaded raw profile data: " + earnCurrenyData);
		if (string.IsNullOrEmpty(earnCurrenyData))
		{
			return;
		}
		string[] array = earnCurrenyData.Split(EarnCurrencyInfo.DATA_PROFILE_ALL_SPLITS, StringSplitOptions.None);
		for (int i = 0; i < array.Length - 1; i++)
		{
			string text = array[i];
			string text2 = array[i + 1];
			if (Enum.IsDefined(typeof(EarnCurrencyInfo.Id), text))
			{
				EarnCurrencyInfo.Id id = (EarnCurrencyInfo.Id)((int)Enum.Parse(typeof(EarnCurrencyInfo.Id), text, true));
				for (int j = 0; j < EarnCurrencyInfo.profiles.Length; j++)
				{
					if (EarnCurrencyInfo.profiles[j].id == id)
					{
						EarnCurrencyInfo.profileData[j] = text2;
						break;
					}
				}
			}
		}
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0000C48B File Offset: 0x0000A68B
	private static string GetProfileData(int profileIndex)
	{
		EarnCurrencyInfo.InitProfileDataArrayIfNeeded();
		return EarnCurrencyInfo.profileData[profileIndex];
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0000C49C File Offset: 0x0000A69C
	private static void SetAndSaveProfileData(int profileIndex, string data)
	{
		EarnCurrencyInfo.InitProfileDataArrayIfNeeded();
		EarnCurrencyInfo.profileData[profileIndex] = data;
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < EarnCurrencyInfo.profileData.Length; i++)
		{
			if (EarnCurrencyInfo.profileData[i] != null)
			{
				stringBuilder.Append(EarnCurrencyInfo.profiles[i].id.ToString());
				stringBuilder.Append("=");
				stringBuilder.Append(EarnCurrencyInfo.profileData[i]);
				stringBuilder.Append(";");
			}
		}
		Debug.Log("EarnCurrenyInfo: Saving profile data: " + stringBuilder.ToString());
		PlayerInfo.Instance.earnCurrenyData = stringBuilder.ToString();
		PlayerInfo.Instance.Save();
	}

	// Token: 0x0400020E RID: 526
	private const string DATA_PROFILE_MAIN_SPLIT = ";";

	// Token: 0x0400020F RID: 527
	private const string DATA_PROFILE_SUB_SPLIT = "=";

	// Token: 0x04000210 RID: 528
	public static readonly EarnCurrencyInfo.EarnCurrencyProfile[] profiles = new EarnCurrencyInfo.EarnCurrencyProfile[]
	{
		new EarnCurrencyInfo.EarnCurrencyProfile
		{
			id = EarnCurrencyInfo.Id.AdColony,
			type = EarnCurrencyInfo.Type.AdColony,
			repeatability = EarnCurrencyInfo.Repeatability.Forever,
			amountOfCoins = 50,
			title = "Sponsored video",
			desc = "Watch get {0} coins",
			iconName = "icon_coinPack_1"
		},
		new EarnCurrencyInfo.EarnCurrencyProfile
		{
			amountOfCoins = 300,
			title = "Kiloo Facebook",
			desc = "'Like' get {0} coins",
			iconName = "icon_coinPack_1",
			url = "http://www.facebook.com/kiloogames"
		},
		new EarnCurrencyInfo.EarnCurrencyProfile
		{
			id = EarnCurrencyInfo.Id.FacebookSybo,
			amountOfCoins = 300,
			title = "Sybo Facebook",
			desc = "'Like' get {0} coins",
			iconName = "icon_coinPack_1",
			url = "http://www.facebook.com/sybogames"
		},
		new EarnCurrencyInfo.EarnCurrencyProfile
		{
			id = EarnCurrencyInfo.Id.TwitterKiloo,
			amountOfCoins = 300,
			title = "Kiloo Twitter",
			desc = "Follow get {0} coins",
			iconName = "icon_coinPack_1",
			url = "https://twitter.com/@kiloogames"
		},
		new EarnCurrencyInfo.EarnCurrencyProfile
		{
			id = EarnCurrencyInfo.Id.TwitterSybo,
			amountOfCoins = 300,
			title = "Sybo Twitter",
			desc = "Follow get {0} coins",
			iconName = "icon_coinPack_1",
			url = "https://twitter.com/@sybogames"
		},
		new EarnCurrencyInfo.EarnCurrencyProfile
		{
			id = EarnCurrencyInfo.Id.YoutubeKiloo,
			amountOfCoins = 300,
			title = "Kiloo YouTube",
			desc = "Subscribe get {0} coins",
			iconName = "icon_coinPack_1",
			url = "http://www.youtube.com/kiloomobile"
		},
		new EarnCurrencyInfo.EarnCurrencyProfile
		{
			id = EarnCurrencyInfo.Id.YoutubeSybo,
			amountOfCoins = 300,
			title = "Sybo YouTube",
			desc = "Subscribe get {0} coins",
			iconName = "icon_coinPack_1",
			url = "http://www.youtube.com/sybogames"
		}
	};

	// Token: 0x04000211 RID: 529
	private static readonly string[] DATA_PROFILE_ALL_SPLITS = new string[] { "=", ";" };

	// Token: 0x04000212 RID: 530
	private static string[] profileData;

	// Token: 0x0200018B RID: 395
	public enum Id
	{
		// Token: 0x0400096F RID: 2415
		FacebookKiloo,
		// Token: 0x04000970 RID: 2416
		FacebookSybo,
		// Token: 0x04000971 RID: 2417
		TwitterKiloo,
		// Token: 0x04000972 RID: 2418
		TwitterSybo,
		// Token: 0x04000973 RID: 2419
		YoutubeKiloo,
		// Token: 0x04000974 RID: 2420
		YoutubeSybo,
		// Token: 0x04000975 RID: 2421
		Review,
		// Token: 0x04000976 RID: 2422
		AdColony
	}

	// Token: 0x0200018C RID: 396
	public enum Type
	{
		// Token: 0x04000978 RID: 2424
		OpenURL,
		// Token: 0x04000979 RID: 2425
		AdColony
	}

	// Token: 0x0200018D RID: 397
	public enum Repeatability
	{
		// Token: 0x0400097B RID: 2427
		Once,
		// Token: 0x0400097C RID: 2428
		OncePerVersion,
		// Token: 0x0400097D RID: 2429
		Forever
	}

	// Token: 0x0200018E RID: 398
	public class EarnCurrencyProfile
	{
		// Token: 0x0400097E RID: 2430
		public EarnCurrencyInfo.Id id;

		// Token: 0x0400097F RID: 2431
		public EarnCurrencyInfo.Type type;

		// Token: 0x04000980 RID: 2432
		public EarnCurrencyInfo.Repeatability repeatability;

		// Token: 0x04000981 RID: 2433
		public int amountOfCoins;

		// Token: 0x04000982 RID: 2434
		public string title = string.Empty;

		// Token: 0x04000983 RID: 2435
		public string desc = string.Empty;

		// Token: 0x04000984 RID: 2436
		public string iconName = string.Empty;

		// Token: 0x04000985 RID: 2437
		public string url;
	}
}
