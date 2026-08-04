using System;
using UnityEngine;

// Token: 0x02000117 RID: 279
[AddComponentMenu("NGUI/UI/Input (Saved)")]
public class UIInputSaved : UIInput
{
	// Token: 0x0600080A RID: 2058 RVA: 0x00029D88 File Offset: 0x00027F88
	private void Start()
	{
		base.Init();
		if (!string.IsNullOrEmpty(this.playerPrefsField) && PlayerPrefs.HasKey(this.playerPrefsField))
		{
			base.text = PlayerPrefs.GetString(this.playerPrefsField);
		}
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x00029DBB File Offset: 0x00027FBB
	private void OnApplicationQuit()
	{
		if (!string.IsNullOrEmpty(this.playerPrefsField))
		{
			PlayerPrefs.SetString(this.playerPrefsField, base.text);
		}
	}

	// Token: 0x04000706 RID: 1798
	public string playerPrefsField;
}
