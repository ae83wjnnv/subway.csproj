using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008C RID: 140
[AddComponentMenu("NGUI/Internal/Localization")]
public class Localization : MonoBehaviour
{
	// Token: 0x1700005E RID: 94
	// (get) Token: 0x0600043F RID: 1087 RVA: 0x00012C6C File Offset: 0x00010E6C
	public static Localization instance
	{
		get
		{
			if (Localization.mInst == null)
			{
				Localization.mInst = Object.FindObjectOfType(typeof(Localization)) as Localization;
				if (Localization.mInst == null)
				{
					GameObject gameObject = new GameObject("_Localization");
					Object.DontDestroyOnLoad(gameObject);
					Localization.mInst = gameObject.AddComponent<Localization>();
				}
			}
			return Localization.mInst;
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000440 RID: 1088 RVA: 0x00012CCC File Offset: 0x00010ECC
	// (set) Token: 0x06000441 RID: 1089 RVA: 0x00012D48 File Offset: 0x00010F48
	public string currentLanguage
	{
		get
		{
			if (string.IsNullOrEmpty(this.mLanguage))
			{
				this.currentLanguage = PlayerPrefs.GetString("Language");
				if (string.IsNullOrEmpty(this.mLanguage))
				{
					this.currentLanguage = this.startingLanguage;
					if (string.IsNullOrEmpty(this.mLanguage) && this.languages != null && this.languages.Length != 0)
					{
						this.currentLanguage = this.languages[0].name;
					}
				}
			}
			return this.mLanguage;
		}
		set
		{
			if (!(this.mLanguage != value))
			{
				return;
			}
			if (!string.IsNullOrEmpty(value))
			{
				if (this.languages != null)
				{
					int i = 0;
					int num = this.languages.Length;
					while (i < num)
					{
						TextAsset textAsset = this.languages[i];
						if (textAsset != null && textAsset.name == value)
						{
							this.Load(textAsset);
							return;
						}
						i++;
					}
				}
				TextAsset textAsset2 = Resources.Load(value, typeof(TextAsset)) as TextAsset;
				if (textAsset2 != null)
				{
					this.Load(textAsset2);
					return;
				}
			}
			this.mDictionary.Clear();
			PlayerPrefs.DeleteKey("Language");
		}
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00012DED File Offset: 0x00010FED
	private void Awake()
	{
		if (Localization.mInst == null)
		{
			Localization.mInst = this;
			Object.DontDestroyOnLoad(base.gameObject);
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x00012E19 File Offset: 0x00011019
	private void OnEnable()
	{
		if (Localization.mInst == null)
		{
			Localization.mInst = this;
		}
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x00012E2E File Offset: 0x0001102E
	private void OnDestroy()
	{
		if (Localization.mInst == this)
		{
			Localization.mInst = null;
		}
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00012E44 File Offset: 0x00011044
	private void Load(TextAsset asset)
	{
		this.mLanguage = asset.name;
		PlayerPrefs.SetString("Language", this.mLanguage);
		ByteReader byteReader = new ByteReader(asset);
		this.mDictionary = byteReader.ReadDictionary();
		UIRoot.Broadcast("OnLocalize", this);
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x00012E8C File Offset: 0x0001108C
	public string Get(string key)
	{
		string text;
		if (this.mDictionary.TryGetValue(key, out text))
		{
			return text;
		}
		return key;
	}

	// Token: 0x0400039F RID: 927
	private static Localization mInst;

	// Token: 0x040003A0 RID: 928
	public string startingLanguage;

	// Token: 0x040003A1 RID: 929
	public TextAsset[] languages;

	// Token: 0x040003A2 RID: 930
	private Dictionary<string, string> mDictionary = new Dictionary<string, string>();

	// Token: 0x040003A3 RID: 931
	private string mLanguage;
}
