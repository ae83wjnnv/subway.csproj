using System;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class BuyButtonIngame : MonoBehaviour
{
	// Token: 0x1700000B RID: 11
	// (get) Token: 0x060001A2 RID: 418 RVA: 0x000067ED File Offset: 0x000049ED
	public PowerupType type
	{
		get
		{
			return this._type;
		}
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x000067F5 File Offset: 0x000049F5
	private void OnClick()
	{
		if (!this._purchaseInProgress)
		{
			Debug.Log("Buy: " + this._type.ToString());
			PurchaseHandler.Instance.PurchaseUpgrade(this._type, this);
		}
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00006830 File Offset: 0x00004A30
	public void initBuyButton(PowerupType type)
	{
		this._type = type;
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00006839 File Offset: 0x00004A39
	public void PurchaseSuccessful()
	{
		this.updater.UpgradePurchased(this._type);
		this._purchaseInProgress = false;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00006853 File Offset: 0x00004A53
	public void PurchaseFailure()
	{
		this._purchaseInProgress = false;
	}

	// Token: 0x040000DF RID: 223
	public UpgradeHelper updater;

	// Token: 0x040000E0 RID: 224
	private PowerupType _type;

	// Token: 0x040000E1 RID: 225
	private bool _purchaseInProgress;
}
