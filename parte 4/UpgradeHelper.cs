using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000146 RID: 326
public class UpgradeHelper : MonoBehaviour
{
	// Token: 0x1700011B RID: 283
	// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0003464A File Offset: 0x0003284A
	// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00034652 File Offset: 0x00032852
	private UpgradeHelper.AnimState animState
	{
		get
		{
			return this._animState;
		}
		set
		{
			this._animState = value;
		}
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0003465B File Offset: 0x0003285B
	private void OnDestroy()
	{
		PlayerInfo instance = PlayerInfo.Instance;
		instance.onPowerupAmountChanged = (Action)Delegate.Remove(instance.onPowerupAmountChanged, new Action(this.UpdateLabel));
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x00034684 File Offset: 0x00032884
	public void InitSingle(PowerupType type)
	{
		if (base.gameObject.GetComponent<UIPanel>() != null)
		{
			Object.Destroy(base.gameObject.GetComponent<UIPanel>());
		}
		this._type = type;
		this.powerupIcon.spriteName = Upgrades.upgrades[type].iconName;
		this.titleLabel.text = Upgrades.upgrades[type].name.ToUpper();
		this.descLabel.text = Upgrades.upgrades[type].description;
		this.priceLabel.text = string.Empty + Upgrades.upgrades[type].getPrice(0).ToString();
		this.animState = UpgradeHelper.AnimState.Closed;
		if (type != PowerupType.mysterybox)
		{
			switch (type)
			{
			case PowerupType.skipmission1:
			{
				MissionInfo missionInfo = Missions.Instance.GetMissionInfo(0);
				string text = ((missionInfo.mission.goal != 1) ? missionInfo.template.ultraShortDescription : missionInfo.template.ultraShortDescriptionSingle);
				this.amountLabel.text = string.Format(text, missionInfo.mission.goal);
				break;
			}
			case PowerupType.skipmission2:
			{
				MissionInfo missionInfo2 = Missions.Instance.GetMissionInfo(1);
				string text2 = ((missionInfo2.mission.goal != 1) ? missionInfo2.template.ultraShortDescription : missionInfo2.template.ultraShortDescriptionSingle);
				this.amountLabel.text = string.Format(text2, missionInfo2.mission.goal);
				break;
			}
			case PowerupType.skipmission3:
			{
				MissionInfo missionInfo3 = Missions.Instance.GetMissionInfo(2);
				string text3 = ((missionInfo3.mission.goal != 1) ? missionInfo3.template.ultraShortDescription : missionInfo3.template.ultraShortDescriptionSingle);
				this.amountLabel.text = string.Format(text3, missionInfo3.mission.goal);
				break;
			}
			default:
				this.amountLabel.text = "You have: " + PlayerInfo.Instance.GetUpgradeAmount(type).ToString();
				break;
			}
		}
		else
		{
			this.amountLabel.text = "Opens immediately";
		}
		this.buyButton.initBuyButton(type);
		PlayerInfo instance = PlayerInfo.Instance;
		instance.onPowerupAmountChanged = (Action)Delegate.Combine(instance.onPowerupAmountChanged, new Action(this.UpdateLabel));
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x000348E4 File Offset: 0x00032AE4
	public void InitPermanent(PowerupType type)
	{
		if (base.gameObject.GetComponent<UIPanel>() != null)
		{
			Object.Destroy(base.gameObject.GetComponent<UIPanel>());
		}
		this._type = type;
		this.powerupIcon.spriteName = Upgrades.upgrades[type].iconName;
		this.titleLabel.text = Upgrades.upgrades[type].name.ToUpper();
		this.descLabel.text = Upgrades.upgrades[type].description;
		this.tierHelper.SetupTiers(type);
		this.animState = UpgradeHelper.AnimState.Closed;
		if (PlayerInfo.Instance.GetCurrentTier(type) >= Upgrades.upgrades[type].numberOfTiers - 1)
		{
			this.priceLabel.text = "Full";
			this.priceLabel.color = new Color(0.27058825f, 0.2627451f, 0.2627451f, 1f);
			this.priceLabel.effectColor = new Color(0.85490197f, 0.85490197f, 0.85490197f, 1f);
			this.priceLabel.effectStyle = UILabel.Effect.Shadow;
			this.priceLabel.pivot = UIWidget.Pivot.Top;
			this.priceLabel.transform.localPosition = new Vector3(101f, this.priceLabel.transform.localPosition.y, this.priceLabel.transform.localPosition.z);
			UISlicedSprite uislicedSprite = NGUITools.AddSprite(this.priceLabel.transform.parent.gameObject, this.coin.atlas, "background_buy_full") as UISlicedSprite;
			uislicedSprite.depth = 5;
			uislicedSprite.transform.localPosition = new Vector3(101f, 28f, 0f);
			uislicedSprite.transform.localScale = new Vector3(52f, 20f, 1f);
			uislicedSprite.pivot = UIWidget.Pivot.Center;
			uislicedSprite.color = new Color(0.6117647f, 0.6117647f, 0.6117647f, 1f);
			uislicedSprite.name = "4FullBG";
			uislicedSprite.fillCenter = true;
			uislicedSprite.MakePixelPerfect();
			NGUITools.Destroy(this.coin);
			Object.Destroy(this.buyButton.gameObject);
		}
		else
		{
			this.priceLabel.text = string.Empty + Upgrades.upgrades[type].getPrice(PlayerInfo.Instance.GetCurrentTier(type) + 1).ToString();
		}
		if (this.buyButton != null)
		{
			this.buyButton.initBuyButton(type);
		}
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x00034B7C File Offset: 0x00032D7C
	public void UpgradePurchased(PowerupType type)
	{
		Debug.Log("Purchased powerup: " + type.ToString());
		switch (type)
		{
		case PowerupType.hoverboard:
		case PowerupType.headstart500:
		case PowerupType.headstart2000:
			if (this.amountLabel != null)
			{
				this.amountLabel.text = "You have: " + PlayerInfo.Instance.GetUpgradeAmount(type).ToString();
			}
			break;
		case PowerupType.mysterybox:
		{
			PlayerInfo instance = PlayerInfo.Instance;
			int mysteryBoxesToUnlock = instance.mysteryBoxesToUnlock;
			instance.mysteryBoxesToUnlock = mysteryBoxesToUnlock + 1;
			UIScreenController.Instance.QueueMysteryBox();
			break;
		}
		case PowerupType.jetpack:
		case PowerupType.supersneakers:
		case PowerupType.coinmagnet:
		case PowerupType.letters:
		case PowerupType.doubleMultiplier:
			if (this.tierHelper.ResetTiers())
			{
				this.priceLabel.text = "Full";
				this.priceLabel.color = new Color(0.27058825f, 0.2627451f, 0.2627451f, 1f);
				this.priceLabel.effectColor = new Color(0.85490197f, 0.85490197f, 0.85490197f, 1f);
				this.priceLabel.effectStyle = UILabel.Effect.Shadow;
				this.priceLabel.pivot = UIWidget.Pivot.Top;
				this.priceLabel.transform.localPosition = new Vector3(101f, this.priceLabel.transform.localPosition.y, this.priceLabel.transform.localPosition.z);
				UISlicedSprite uislicedSprite = NGUITools.AddSprite(this.priceLabel.transform.parent.gameObject, this.coin.atlas, "background_buy_full") as UISlicedSprite;
				uislicedSprite.depth = 5;
				uislicedSprite.transform.localPosition = new Vector3(101f, 29f, 0f);
				uislicedSprite.transform.localScale = new Vector3(52f, 20f, 1f);
				uislicedSprite.pivot = UIWidget.Pivot.Center;
				uislicedSprite.color = new Color(0.6117647f, 0.6117647f, 0.6117647f, 1f);
				uislicedSprite.name = "4FullBG";
				uislicedSprite.fillCenter = true;
				uislicedSprite.MakePixelPerfect();
				NGUITools.Destroy(this.coin);
				Object.Destroy(this.buyButton.gameObject);
			}
			else
			{
				this.priceLabel.text = string.Empty + Upgrades.upgrades[type].getPrice(PlayerInfo.Instance.GetCurrentTier(type) + 1).ToString();
			}
			break;
		case PowerupType.skipmission1:
			Flurry.LogEventWithAParameter("Boost Mission Skip purchased", "Mission Set and Index", Missions.Instance.currentMissionSet.ToString() + "-0");
			Missions.Instance.SkipMission(0);
			break;
		case PowerupType.skipmission2:
			Flurry.LogEventWithAParameter("Boost Mission Skip purchased", "Mission Set and Index", Missions.Instance.currentMissionSet.ToString() + "-1");
			Missions.Instance.SkipMission(1);
			break;
		case PowerupType.skipmission3:
			Flurry.LogEventWithAParameter("Boost Mission Skip purchased", "Mission Set and Index", Missions.Instance.currentMissionSet.ToString() + "-2");
			Missions.Instance.SkipMission(2);
			break;
		}
		switch (type)
		{
		case PowerupType.hoverboard:
			Flurry.LogEvent("Boost Hoverboard purchased");
			return;
		case PowerupType.headstart500:
			Flurry.LogEvent("Boost Headstart500 purchased");
			return;
		case PowerupType.headstart2000:
			Flurry.LogEvent("Boost Headstart2000 purchased");
			return;
		case PowerupType.mysterybox:
			Flurry.LogEvent("Boost MysteryBox purchased");
			return;
		case PowerupType.jetpack:
			Flurry.LogEventWithAParameter("Boost jetpack purchased", "Tier", PlayerInfo.Instance.GetCurrentTier(type).ToString());
			return;
		case PowerupType.supersneakers:
			Flurry.LogEventWithAParameter("Boost supersneakers purchased", "Tier", PlayerInfo.Instance.GetCurrentTier(type).ToString());
			return;
		case PowerupType.coinmagnet:
			Flurry.LogEventWithAParameter("Boost Coinmagnet purchased", "Tier", PlayerInfo.Instance.GetCurrentTier(type).ToString());
			return;
		case PowerupType.letters:
			Flurry.LogEventWithAParameter("Boost letters purchased", "Tier", PlayerInfo.Instance.GetCurrentTier(type).ToString());
			return;
		case PowerupType.doubleMultiplier:
			Flurry.LogEventWithAParameter("Boost 2x multiplier purchased", "Tier", PlayerInfo.Instance.GetCurrentTier(type).ToString());
			return;
		default:
			return;
		}
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x00034FC8 File Offset: 0x000331C8
	private void UpdateLabel()
	{
		PowerupType type = this._type;
		if (type - PowerupType.hoverboard <= 2 && this.amountLabel != null)
		{
			this.amountLabel.text = "You have: " + PlayerInfo.Instance.GetUpgradeAmount(this._type).ToString();
		}
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0003501D File Offset: 0x0003321D
	private void OnClick()
	{
		this.AnimationStarted();
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x00035028 File Offset: 0x00033228
	public void AnimationStarted()
	{
		if (this.animState == UpgradeHelper.AnimState.Closed || this.animState == UpgradeHelper.AnimState.Closing)
		{
			this.animState = UpgradeHelper.AnimState.Opening;
			if (this._upgradeScreenSetup == null)
			{
				this._upgradeScreenSetup = base.transform.parent.GetComponent<UpgradeScreenSetup>();
			}
			using (List<UpgradeHelper>.Enumerator enumerator = this._upgradeScreenSetup.cachedUpgradeHelpers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UpgradeHelper upgradeHelper = enumerator.Current;
					if (upgradeHelper != this && upgradeHelper.animState == UpgradeHelper.AnimState.Open)
					{
						upgradeHelper.SendMessage("OnClick");
					}
				}
				goto IL_0094;
			}
		}
		this.animState = UpgradeHelper.AnimState.Closing;
		IL_0094:
		this.contentChanger.FoldClicked();
		Collider component = base.gameObject.GetComponent<Collider>();
		if (component != null)
		{
			component.enabled = false;
			Object.Destroy(component);
		}
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x00035108 File Offset: 0x00033308
	public void AnimationEnded()
	{
		if (this.animState == UpgradeHelper.AnimState.Closing || this.animState == UpgradeHelper.AnimState.Opening)
		{
			NGUITools.AddWidgetCollider(base.gameObject);
			this.animState = ((this.animState != UpgradeHelper.AnimState.Closing) ? UpgradeHelper.AnimState.Open : UpgradeHelper.AnimState.Closed);
			this.contentChanger.TriggerContent();
			return;
		}
		Debug.LogError("Unexpected anim state: " + this.animState.ToString(), this);
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x00035176 File Offset: 0x00033376
	private void ColliderFixer()
	{
	}

	// Token: 0x0400085E RID: 2142
	public UISprite powerupIcon;

	// Token: 0x0400085F RID: 2143
	public UILabel titleLabel;

	// Token: 0x04000860 RID: 2144
	public UILabel descLabel;

	// Token: 0x04000861 RID: 2145
	public UILabel priceLabel;

	// Token: 0x04000862 RID: 2146
	public UILabel amountLabel;

	// Token: 0x04000863 RID: 2147
	public BuyButtonIngame buyButton;

	// Token: 0x04000864 RID: 2148
	public UILabel buyButtonTitle;

	// Token: 0x04000865 RID: 2149
	public UISlicedSprite buyButtonBackground;

	// Token: 0x04000866 RID: 2150
	public UITierHelper tierHelper;

	// Token: 0x04000867 RID: 2151
	public UISprite coin;

	// Token: 0x04000868 RID: 2152
	private PowerupType _type;

	// Token: 0x04000869 RID: 2153
	private UpgradeScreenSetup _upgradeScreenSetup;

	// Token: 0x0400086A RID: 2154
	public ContentChange contentChanger;

	// Token: 0x0400086B RID: 2155
	private UpgradeHelper.AnimState _animState;

	// Token: 0x02000225 RID: 549
	private enum AnimState
	{
		// Token: 0x04000C58 RID: 3160
		Closed,
		// Token: 0x04000C59 RID: 3161
		Opening,
		// Token: 0x04000C5A RID: 3162
		Open,
		// Token: 0x04000C5B RID: 3163
		Closing
	}
}
