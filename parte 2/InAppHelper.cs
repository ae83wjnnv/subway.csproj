using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000076 RID: 118
public class InAppHelper : MonoBehaviour
{
	// Token: 0x17000050 RID: 80
	// (get) Token: 0x060003DA RID: 986 RVA: 0x0001141C File Offset: 0x0000F61C
	public static InAppHelper Instance
	{
		get
		{
			InAppHelper inAppHelper;
			if ((inAppHelper = InAppHelper._instance) == null)
			{
				inAppHelper = (InAppHelper._instance = Object.FindObjectOfType(typeof(InAppHelper)) as InAppHelper);
			}
			return inAppHelper;
		}
	}

	// Token: 0x060003DB RID: 987 RVA: 0x00011444 File Offset: 0x0000F644
	public void SetupNativePopup(int cost)
	{
		int num = 0;
		num = cost - PlayerInfo.Instance.amountOfCoins;
		string text = string.Empty;
		if (InAppManager.Instance.productRequestSucceeded)
		{
			foreach (KeyValuePair<string, InAppProfile> keyValuePair in InAppData.inAppData)
			{
				if (keyValuePair.Value.validInApp && keyValuePair.Value.amountOfCoins > num)
				{
					if (!string.IsNullOrEmpty(text))
					{
						if (InAppData.inAppData[text].amountOfCoins > InAppData.inAppData[keyValuePair.Key].amountOfCoins)
						{
							text = keyValuePair.Key;
						}
					}
					else
					{
						text = keyValuePair.Key;
					}
				}
			}
		}
		this.inAppPurchaseKey = text;
		string text2 = "Not enough coins!";
		if (!string.IsNullOrEmpty(this.inAppPurchaseKey))
		{
			string text3 = string.Format("You need {0} more Coins to complete your purchase. Buy {1} for {2}?", num, InAppData.inAppData[text].amountOfCoins, InAppData.inAppData[text].price);
			DeviceUtility.showNativePopupWithCallback("3InAppController", "NativePurchaseInappPack", text2, text3, "Cancel", "Buy", null);
			return;
		}
		string text4 = string.Format("You need {0} more Coins to complete your purchase. Buy more in the store", num);
		DeviceUtility.showNativePopup(text2, text4, "Ok");
	}

	// Token: 0x060003DC RID: 988 RVA: 0x000115A4 File Offset: 0x0000F7A4
	public void NativePurchaseInappPack(string message)
	{
		if (message == "0")
		{
			this.inAppPurchaseKey = string.Empty;
			return;
		}
		if (InAppManager.Instance.productRequestSucceeded)
		{
			InAppManager.Instance.BuyFromPopup(this.inAppPurchaseKey);
			return;
		}
		this.inAppPurchaseKey = string.Empty;
	}

	// Token: 0x04000330 RID: 816
	private static InAppHelper _instance;

	// Token: 0x04000331 RID: 817
	private string inAppPurchaseKey = string.Empty;
}
