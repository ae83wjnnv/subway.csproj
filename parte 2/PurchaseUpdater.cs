using System;
using UnityEngine;

// Token: 0x020000AE RID: 174
public class PurchaseUpdater : MonoBehaviour
{
	// Token: 0x0600053A RID: 1338 RVA: 0x000192A0 File Offset: 0x000174A0
	public void UpgradePurchased(PowerupType type)
	{
		Debug.Log("Purchased powerup: " + type.ToString());
		switch (type)
		{
		case PowerupType.hoverboard:
		case PowerupType.headstart500:
		case PowerupType.headstart2000:
			if (this.haveAmount != null)
			{
				this.haveAmount.text = "You have: " + PlayerInfo.Instance.GetUpgradeAmount(type).ToString();
				return;
			}
			break;
		case PowerupType.mysterybox:
		case PowerupType.coinpouch:
		case PowerupType.skipmission1:
		case PowerupType.skipmission2:
		case PowerupType.skipmission3:
			break;
		case PowerupType.jetpack:
		case PowerupType.supersneakers:
		case PowerupType.coinmagnet:
		case PowerupType.letters:
		case PowerupType.doubleMultiplier:
			if (this.tierHelper.ResetTiers())
			{
				NGUITools.Destroy(this.coin);
				this.price.text = string.Empty;
				this.buyButtonTitle.text = "Max";
				Object.Destroy(this.buyButton.GetComponent<BoxCollider>());
				return;
			}
			this.price.text = string.Empty + Upgrades.upgrades[type].getPrice(PlayerInfo.Instance.GetCurrentTier(type) + 1).ToString();
			break;
		default:
			return;
		}
	}

	// Token: 0x04000460 RID: 1120
	public UILabel price;

	// Token: 0x04000461 RID: 1121
	public UISprite coin;

	// Token: 0x04000462 RID: 1122
	public UITierHelper tierHelper;

	// Token: 0x04000463 RID: 1123
	public UILabel haveAmount;

	// Token: 0x04000464 RID: 1124
	public GameObject buyButton;

	// Token: 0x04000465 RID: 1125
	public UILabel buyButtonTitle;

	// Token: 0x04000466 RID: 1126
	public UISlicedSprite buyButtonBackground;
}
