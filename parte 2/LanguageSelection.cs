using System;
using UnityEngine;

// Token: 0x02000089 RID: 137
[AddComponentMenu("NGUI/Interaction/Language Selection")]
[RequireComponent(typeof(UIPopupList))]
public class LanguageSelection : MonoBehaviour
{
	// Token: 0x06000436 RID: 1078 RVA: 0x00012B3C File Offset: 0x00010D3C
	private void Start()
	{
		this.mList = base.GetComponent<UIPopupList>();
		this.UpdateList();
		this.mList.eventReceiver = base.gameObject;
		this.mList.functionName = "OnLanguageSelection";
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x00012B74 File Offset: 0x00010D74
	private void UpdateList()
	{
		if (!(Localization.instance != null) || Localization.instance.languages == null)
		{
			return;
		}
		this.mList.items.Clear();
		int i = 0;
		int num = Localization.instance.languages.Length;
		while (i < num)
		{
			TextAsset textAsset = Localization.instance.languages[i];
			if (textAsset != null)
			{
				this.mList.items.Add(textAsset.name);
			}
			i++;
		}
		this.mList.selection = Localization.instance.currentLanguage;
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x00012C05 File Offset: 0x00010E05
	private void OnLanguageSelection(string language)
	{
		if (Localization.instance != null)
		{
			Localization.instance.currentLanguage = language;
		}
	}

	// Token: 0x0400039D RID: 925
	private UIPopupList mList;
}
