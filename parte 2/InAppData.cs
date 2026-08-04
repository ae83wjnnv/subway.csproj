using System;
using System.Collections.Generic;

// Token: 0x02000075 RID: 117
public class InAppData
{
	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060003D6 RID: 982 RVA: 0x000112C6 File Offset: 0x0000F4C6
	public int InAppPurchaseCount
	{
		get
		{
			return InAppData.inAppData.Count;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060003D7 RID: 983 RVA: 0x000112D4 File Offset: 0x0000F4D4
	public string CommaSeparatedProductIds
	{
		get
		{
			string text = string.Empty;
			int num = 0;
			foreach (KeyValuePair<string, InAppProfile> keyValuePair in InAppData.inAppData)
			{
				if (num > 0)
				{
					text += ",";
				}
				text += keyValuePair.Key;
				num++;
			}
			return text;
		}
	}

	// Token: 0x0400032C RID: 812
	public static readonly string inAppTier1 = "com.kiloo.subwaysurfers.coinstier1";

	// Token: 0x0400032D RID: 813
	public static readonly string inAppTier2 = "com.kiloo.subwaysurfers.coinstier2";

	// Token: 0x0400032E RID: 814
	public static readonly string inAppTier3 = "com.kiloo.subwaysurfers.coinstier3";

	// Token: 0x0400032F RID: 815
	public static readonly Dictionary<string, InAppProfile> inAppData = new Dictionary<string, InAppProfile>
	{
		{
			InAppData.inAppTier1,
			new InAppProfile
			{
				amountOfCoins = 7500,
				title = "A pile of coins",
				iconName = "icon_coinPack_1"
			}
		},
		{
			InAppData.inAppTier2,
			new InAppProfile
			{
				amountOfCoins = 45000,
				title = "A bunch of coins",
				iconName = "icon_coinPack_2"
			}
		},
		{
			InAppData.inAppTier3,
			new InAppProfile
			{
				amountOfCoins = 180000,
				title = "A chest of coins",
				iconName = "icon_coinPack_3"
			}
		}
	};
}
