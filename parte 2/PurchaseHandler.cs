using System;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class PurchaseHandler
{
	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001908E File Offset: 0x0001728E
	public static PurchaseHandler Instance
	{
		get
		{
			PurchaseHandler purchaseHandler;
			if ((purchaseHandler = PurchaseHandler._instance) == null)
			{
				purchaseHandler = (PurchaseHandler._instance = new PurchaseHandler());
			}
			return purchaseHandler;
		}
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x000190A4 File Offset: 0x000172A4
	public void PurchaseCharacter(CharacterModels.ModelType modelType, UICharacterBuyButton sender)
	{
		CharacterModels.Model model = CharacterModels.modelData[modelType];
		if (model.UnlockType != CharacterModels.UnlockType.coins)
		{
			Debug.Log("Cannot buy character with unlocktype: " + model.UnlockType.ToString());
			sender.PurchaseFailure();
			return;
		}
		int price = model.Price;
		if (PlayerInfo.Instance.amountOfCoins >= price)
		{
			Missions.Instance.PlayerDidThis(Missions.MissionTarget.SpendCoin, price);
			PlayerInfo.Instance.CollectToken(modelType, price);
			PlayerInfo.Instance.amountOfCoins -= price;
			sender.PurchaseSuccessful();
			PlayerInfo.Instance.Save();
			return;
		}
		InAppHelper.Instance.SetupNativePopup(price);
		sender.PurchaseFailure();
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00019150 File Offset: 0x00017350
	public void PurchaseUpgrade(PowerupType type, BuyButtonIngame sender)
	{
		int num = ((Upgrades.upgrades[type].numberOfTiers != 0) ? Upgrades.upgrades[type].getPrice(PlayerInfo.Instance.GetCurrentTier(type) + 1) : Upgrades.upgrades[type].getPrice(0));
		if (PlayerInfo.Instance.amountOfCoins >= num)
		{
			switch (type)
			{
			case PowerupType.hoverboard:
			case PowerupType.headstart500:
			case PowerupType.headstart2000:
				PlayerInfo.Instance.IncreaseUpgradeAmount(type, 1);
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.SpendCoin, num);
				break;
			case PowerupType.mysterybox:
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.SpendCoin, num);
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.BuyMysterybox, 1);
				break;
			case PowerupType.jetpack:
			case PowerupType.supersneakers:
			case PowerupType.coinmagnet:
			case PowerupType.letters:
			case PowerupType.doubleMultiplier:
				PlayerInfo.Instance.IncreasePowerupTier(type);
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.SpendCoin, num);
				break;
			case PowerupType.skipmission1:
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.SpendCoin, num);
				break;
			case PowerupType.skipmission2:
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.SpendCoin, num);
				break;
			case PowerupType.skipmission3:
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.SpendCoin, num);
				break;
			}
			PlayerInfo.Instance.amountOfCoins -= num;
			sender.PurchaseSuccessful();
			PlayerInfo.Instance.Save();
			return;
		}
		InAppHelper.Instance.SetupNativePopup(num);
		sender.PurchaseFailure();
	}

	// Token: 0x0400045F RID: 1119
	private static PurchaseHandler _instance;
}
