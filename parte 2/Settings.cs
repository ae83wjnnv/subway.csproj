using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class Settings : MonoBehaviour
{
	// Token: 0x17000081 RID: 129
	// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001BA86 File Offset: 0x00019C86
	// (set) Token: 0x0600057C RID: 1404 RVA: 0x0001BA92 File Offset: 0x00019C92
	public static bool optionAutoMessage
	{
		get
		{
			Settings.LoadOptionsIfNeeded();
			return Settings._optionAutoMessage;
		}
		set
		{
			if (!value && !PlayerPrefs.HasKey("OPTION_AUTOMESSAGE"))
			{
				Flurry.LogEvent("AutoBrag turned off");
			}
			Settings._optionAutoMessage = value;
			PlayerPrefs.SetInt("OPTION_AUTOMESSAGE", Settings._optionAutoMessage ? 1 : 0);
		}
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001BAC8 File Offset: 0x00019CC8
	// (set) Token: 0x0600057E RID: 1406 RVA: 0x0001BAD4 File Offset: 0x00019CD4
	public static bool optionSound
	{
		get
		{
			Settings.LoadOptionsIfNeeded();
			return Settings._optionSound;
		}
		set
		{
			Settings._optionSound = value;
			PlayerPrefs.SetInt("OPTION_SOUND", Settings._optionSound ? 1 : 0);
			AudioListener.volume = ((!Settings._optionSound) ? 0f : 1f);
		}
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x0001BB09 File Offset: 0x00019D09
	private void Awake()
	{
		Settings.LoadOptionsIfNeeded();
		SocialManager instance = SocialManager.instance;
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x0001BB18 File Offset: 0x00019D18
	private static void LoadOptionsIfNeeded()
	{
		if (!Settings._optionsLoaded)
		{
			Settings._optionSound = PlayerPrefs.GetInt("OPTION_SOUND", 1) != 0;
			AudioListener.volume = ((!Settings._optionSound) ? 0f : 1f);
			Settings._optionAutoMessage = PlayerPrefs.GetInt("OPTION_AUTOMESSAGE", 0) != 0;
			Settings._optionsLoaded = true;
		}
	}

	// Token: 0x040004BC RID: 1212
	private const string OPTION_SOUND_KEY = "OPTION_SOUND";

	// Token: 0x040004BD RID: 1213
	private const int OPTION_SOUND_DEFAULT = 1;

	// Token: 0x040004BE RID: 1214
	private const string OPTION_AUTOMESSAGE_KEY = "OPTION_AUTOMESSAGE";

	// Token: 0x040004BF RID: 1215
	private const int OPTION_AUTOMESSAGE_DEFAULT = 0;

	// Token: 0x040004C0 RID: 1216
	private static bool _optionsLoaded;

	// Token: 0x040004C1 RID: 1217
	private static bool _optionSound;

	// Token: 0x040004C2 RID: 1218
	private static bool _optionAutoMessage;
}
