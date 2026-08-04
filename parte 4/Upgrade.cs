using System;
using UnityEngine;

// Token: 0x02000145 RID: 325
public class Upgrade
{
	// Token: 0x060009A6 RID: 2470 RVA: 0x0003460B File Offset: 0x0003280B
	public int getPrice(int tier)
	{
		if (this.pricesRaw == null)
		{
			Debug.LogWarning("Prices is not initialized");
			return -1;
		}
		return this.pricesRaw[tier] + this.levelPriceMultiplyer * Missions.Instance.currentMissionSet;
	}

	// Token: 0x04000854 RID: 2132
	public string name;

	// Token: 0x04000855 RID: 2133
	public string description;

	// Token: 0x04000856 RID: 2134
	public int numberOfTiers;

	// Token: 0x04000857 RID: 2135
	public float[] durations;

	// Token: 0x04000858 RID: 2136
	public float spawnProbability;

	// Token: 0x04000859 RID: 2137
	public int minimumMeters;

	// Token: 0x0400085A RID: 2138
	public int coinmagnetRange = 1;

	// Token: 0x0400085B RID: 2139
	public int[] pricesRaw;

	// Token: 0x0400085C RID: 2140
	public int levelPriceMultiplyer;

	// Token: 0x0400085D RID: 2141
	public string iconName;
}
