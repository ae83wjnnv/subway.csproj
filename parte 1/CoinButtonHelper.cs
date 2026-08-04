using System;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class CoinButtonHelper : MonoBehaviour
{
	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000232 RID: 562 RVA: 0x00009A54 File Offset: 0x00007C54
	public string Key
	{
		get
		{
			return this._inAppKey;
		}
	}

	// Token: 0x06000233 RID: 563 RVA: 0x00009A5C File Offset: 0x00007C5C
	public void Init(string key)
	{
		this.icon.spriteName = InAppData.inAppData[key].iconName;
		this.title.text = InAppData.inAppData[key].title;
		this.price.text = InAppData.inAppData[key].price;
		this.description.text = InAppData.inAppData[key].amountOfCoins.ToString() + " Coins";
		this._inAppKey = key;
		InAppManager instance = InAppManager.Instance;
		instance.onProductRequestSuccess = (Action)Delegate.Combine(instance.onProductRequestSuccess, new Action(this.UpdatePrice));
	}

	// Token: 0x06000234 RID: 564 RVA: 0x00009B11 File Offset: 0x00007D11
	private void UpdatePrice()
	{
		this.price.text = InAppData.inAppData[this._inAppKey].price;
	}

	// Token: 0x0400016B RID: 363
	public UISprite icon;

	// Token: 0x0400016C RID: 364
	public UILabel title;

	// Token: 0x0400016D RID: 365
	public UILabel price;

	// Token: 0x0400016E RID: 366
	public UILabel description;

	// Token: 0x0400016F RID: 367
	private string _inAppKey;
}
