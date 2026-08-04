using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class CoinEarnerButtonHelper : MonoBehaviour
{
	// Token: 0x06000236 RID: 566 RVA: 0x00009B3B File Offset: 0x00007D3B
	public void Init(int earnCurrencyProfileIndex, string title, string desc, string iconName)
	{
		this.earnCurrencyProfileIndex = earnCurrencyProfileIndex;
		this.title.text = title;
		this.icon.spriteName = iconName;
		this.description.text = desc;
	}

	// Token: 0x06000237 RID: 567 RVA: 0x00009B69 File Offset: 0x00007D69
	private void OnClick()
	{
		EarnCurrencyInfo.Trigger(this.earnCurrencyProfileIndex);
		NGUITools.FindInParents<CoinScreenSetup>(base.gameObject).RefreshCurrencyEarners();
	}

	// Token: 0x04000170 RID: 368
	public UISprite icon;

	// Token: 0x04000171 RID: 369
	public UILabel title;

	// Token: 0x04000172 RID: 370
	public UILabel description;

	// Token: 0x04000173 RID: 371
	private int earnCurrencyProfileIndex;
}
