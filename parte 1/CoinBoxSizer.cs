using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class CoinBoxSizer : MonoBehaviour
{
	// Token: 0x1700001A RID: 26
	// (get) Token: 0x0600022A RID: 554 RVA: 0x00009700 File Offset: 0x00007900
	// (set) Token: 0x0600022B RID: 555 RVA: 0x00009708 File Offset: 0x00007908
	public bool updateAutomatically
	{
		get
		{
			return this._updateAutomatically;
		}
		set
		{
			this._updateAutomatically = value;
			if (value)
			{
				this.OnCoinsChanged();
			}
		}
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0000971A File Offset: 0x0000791A
	private void Awake()
	{
		this.cachedAmountLabel = this.AmountLabel.GetComponent<UILabel>();
		PlayerInfo instance = PlayerInfo.Instance;
		instance.onCoinsChanged = (Action)Delegate.Combine(instance.onCoinsChanged, new Action(this.OnCoinsChanged));
		this.OnCoinsChanged();
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00009759 File Offset: 0x00007959
	private void OnDestroy()
	{
		PlayerInfo instance = PlayerInfo.Instance;
		instance.onCoinsChanged = (Action)Delegate.Remove(instance.onCoinsChanged, new Action(this.OnCoinsChanged));
	}

	// Token: 0x0600022E RID: 558 RVA: 0x00009781 File Offset: 0x00007981
	private void Start()
	{
		this._AdjustSize();
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0000978C File Offset: 0x0000798C
	private void OnCoinsChanged()
	{
		if (this.updateAutomatically)
		{
			this.cachedAmountLabel.text = string.Empty + PlayerInfo.Instance.amountOfCoins.ToString();
			this._AdjustSize();
		}
	}

	// Token: 0x06000230 RID: 560 RVA: 0x000097D0 File Offset: 0x000079D0
	private void _AdjustSize()
	{
		if (this.AddFundsIcon != null)
		{
			float num = this.cachedAmountLabel.relativeSize.x;
			if (num < 1f)
			{
				num = 1f;
			}
			if (num != this.cachedWidth)
			{
				this.GrayFG.transform.localScale = new Vector3(num * 17f + 20f, this.GrayFG.transform.localScale.y, this.GrayFG.transform.localScale.z);
				this.CoinIcon.transform.localPosition = new Vector3(-1f * (num * 17f + 3f), this.CoinIcon.transform.localPosition.y, this.CoinIcon.transform.localPosition.z);
				this.AddFundsIcon.transform.localScale = new Vector3(this.GrayFG.transform.localScale.x + 16f, this.GrayFG.transform.localScale.y, this.GrayFG.transform.localScale.z);
				if (this.Shadow != null)
				{
					this.Shadow.transform.localScale = this.AddFundsIcon.transform.localScale;
				}
				this.cachedWidth = num;
			}
			return;
		}
		float num2 = this.cachedAmountLabel.relativeSize.x;
		if (num2 < 1f)
		{
			num2 = 1f;
		}
		if (num2 != this.cachedWidth)
		{
			this.GrayFG.transform.localScale = new Vector3(num2 * 17f + 20f, this.GrayFG.transform.localScale.y, this.GrayFG.transform.localScale.z);
			this.CoinIcon.transform.localPosition = new Vector3(-1f * (num2 * 17f + 3f), this.CoinIcon.transform.localPosition.y, this.CoinIcon.transform.localPosition.z);
			if (this.Shadow != null)
			{
				this.Shadow.transform.localScale = this.GrayFG.transform.localScale;
			}
			this.cachedWidth = num2;
		}
	}

	// Token: 0x04000163 RID: 355
	public GameObject AddFundsIcon;

	// Token: 0x04000164 RID: 356
	public GameObject GrayFG;

	// Token: 0x04000165 RID: 357
	public GameObject AmountLabel;

	// Token: 0x04000166 RID: 358
	public GameObject CoinIcon;

	// Token: 0x04000167 RID: 359
	public GameObject Shadow;

	// Token: 0x04000168 RID: 360
	private UILabel cachedAmountLabel;

	// Token: 0x04000169 RID: 361
	private float cachedWidth;

	// Token: 0x0400016A RID: 362
	private bool _updateAutomatically = true;
}
