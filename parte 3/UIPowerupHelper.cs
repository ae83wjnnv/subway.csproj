using System;
using UnityEngine;

// Token: 0x02000125 RID: 293
public class UIPowerupHelper : MonoBehaviour
{
	// Token: 0x06000896 RID: 2198 RVA: 0x0002DD00 File Offset: 0x0002BF00
	public void SetPowerup(ActivePowerup powerup)
	{
		this.icon.spriteName = Upgrades.upgrades[powerup.type].iconName;
		float num = powerup.timeLeft / PlayerInfo.Instance.GetPowerupDuration(powerup.type);
		this.slider.sliderValue = num;
		if (powerup.type == PowerupType.hoverboard)
		{
			this.amountLabel.text = PlayerInfo.Instance.GetUpgradeAmount(powerup.type).ToString();
			this.amountLabel.gameObject.active = true;
		}
		else
		{
			this.amountLabel.gameObject.active = false;
		}
		if (powerup.timeLeft < 0f)
		{
			if (this.slider.gameObject.active)
			{
				NGUITools.SetActive(this.slider.gameObject, false);
			}
			this.icon.color = Color.Lerp(Color.grey, Color.white, 0.5f + 0.5f * Mathf.Cos(powerup.timeLeft * 3.1415927f * 4f));
			return;
		}
		if (!this.slider.gameObject.active)
		{
			NGUITools.SetActive(this.slider.gameObject, true);
		}
		this.icon.color = Color.white;
	}

	// Token: 0x04000792 RID: 1938
	public UISlider slider;

	// Token: 0x04000793 RID: 1939
	public UISprite icon;

	// Token: 0x04000794 RID: 1940
	public UILabel amountLabel;
}
