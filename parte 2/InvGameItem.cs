using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007E RID: 126
[Serializable]
public class InvGameItem
{
	// Token: 0x17000055 RID: 85
	// (get) Token: 0x0600040C RID: 1036 RVA: 0x00011EB9 File Offset: 0x000100B9
	public int baseItemID
	{
		get
		{
			return this.mBaseItemID;
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x0600040D RID: 1037 RVA: 0x00011EC1 File Offset: 0x000100C1
	public InvBaseItem baseItem
	{
		get
		{
			if (this.mBaseItem == null)
			{
				this.mBaseItem = InvDatabase.FindByID(this.baseItemID);
			}
			return this.mBaseItem;
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x0600040E RID: 1038 RVA: 0x00011EE2 File Offset: 0x000100E2
	public string name
	{
		get
		{
			if (this.baseItem == null)
			{
				return null;
			}
			return this.quality.ToString() + " " + this.baseItem.name;
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x0600040F RID: 1039 RVA: 0x00011F14 File Offset: 0x00010114
	public float statMultiplier
	{
		get
		{
			float num = 0f;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				num = 0f;
				break;
			case InvGameItem.Quality.Cursed:
				num = -1f;
				break;
			case InvGameItem.Quality.Damaged:
				num = 0.25f;
				break;
			case InvGameItem.Quality.Worn:
				num = 0.9f;
				break;
			case InvGameItem.Quality.Sturdy:
				num = 1f;
				break;
			case InvGameItem.Quality.Polished:
				num = 1.1f;
				break;
			case InvGameItem.Quality.Improved:
				num = 1.25f;
				break;
			case InvGameItem.Quality.Crafted:
				num = 1.5f;
				break;
			case InvGameItem.Quality.Superior:
				num = 1.75f;
				break;
			case InvGameItem.Quality.Enchanted:
				num = 2f;
				break;
			case InvGameItem.Quality.Epic:
				num = 2.5f;
				break;
			case InvGameItem.Quality.Legendary:
				num = 3f;
				break;
			}
			float num2 = (float)this.itemLevel / 50f;
			return num * Mathf.Lerp(num2, num2 * num2, 0.5f);
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000410 RID: 1040 RVA: 0x00011FE4 File Offset: 0x000101E4
	public Color color
	{
		get
		{
			Color color = Color.white;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				color = new Color(0.4f, 0.2f, 0.2f);
				break;
			case InvGameItem.Quality.Cursed:
				color = Color.red;
				break;
			case InvGameItem.Quality.Damaged:
				color = new Color(0.4f, 0.4f, 0.4f);
				break;
			case InvGameItem.Quality.Worn:
				color = new Color(0.7f, 0.7f, 0.7f);
				break;
			case InvGameItem.Quality.Sturdy:
				color = new Color(1f, 1f, 1f);
				break;
			case InvGameItem.Quality.Polished:
				color = NGUIMath.HexToColor(3774856959U);
				break;
			case InvGameItem.Quality.Improved:
				color = NGUIMath.HexToColor(2480359935U);
				break;
			case InvGameItem.Quality.Crafted:
				color = NGUIMath.HexToColor(1325334783U);
				break;
			case InvGameItem.Quality.Superior:
				color = NGUIMath.HexToColor(12255231U);
				break;
			case InvGameItem.Quality.Enchanted:
				color = NGUIMath.HexToColor(1937178111U);
				break;
			case InvGameItem.Quality.Epic:
				color = NGUIMath.HexToColor(2516647935U);
				break;
			case InvGameItem.Quality.Legendary:
				color = NGUIMath.HexToColor(4287627519U);
				break;
			}
			return color;
		}
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00012104 File Offset: 0x00010304
	public InvGameItem(int id)
	{
		this.mBaseItemID = id;
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x00012121 File Offset: 0x00010321
	public InvGameItem(int id, InvBaseItem bi)
	{
		this.mBaseItemID = id;
		this.mBaseItem = bi;
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00012148 File Offset: 0x00010348
	public List<InvStat> CalculateStats()
	{
		List<InvStat> list = new List<InvStat>();
		if (this.baseItem != null)
		{
			float statMultiplier = this.statMultiplier;
			List<InvStat> stats = this.baseItem.stats;
			int i = 0;
			int count = stats.Count;
			while (i < count)
			{
				InvStat invStat = stats[i];
				int num = Mathf.RoundToInt(statMultiplier * (float)invStat.amount);
				if (num != 0)
				{
					bool flag = false;
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						InvStat invStat2 = list[j];
						if (invStat2.id == invStat.id && invStat2.modifier == invStat.modifier)
						{
							invStat2.amount += num;
							flag = true;
							break;
						}
						j++;
					}
					if (!flag)
					{
						list.Add(new InvStat
						{
							id = invStat.id,
							amount = num,
							modifier = invStat.modifier
						});
					}
				}
				i++;
			}
			list.Sort(new Comparison<InvStat>(InvStat.CompareArmor));
		}
		return list;
	}

	// Token: 0x04000355 RID: 853
	[SerializeField]
	private int mBaseItemID;

	// Token: 0x04000356 RID: 854
	public InvGameItem.Quality quality = InvGameItem.Quality.Sturdy;

	// Token: 0x04000357 RID: 855
	public int itemLevel = 1;

	// Token: 0x04000358 RID: 856
	private InvBaseItem mBaseItem;

	// Token: 0x020001AE RID: 430
	public enum Quality
	{
		// Token: 0x040009F3 RID: 2547
		Broken,
		// Token: 0x040009F4 RID: 2548
		Cursed,
		// Token: 0x040009F5 RID: 2549
		Damaged,
		// Token: 0x040009F6 RID: 2550
		Worn,
		// Token: 0x040009F7 RID: 2551
		Sturdy,
		// Token: 0x040009F8 RID: 2552
		Polished,
		// Token: 0x040009F9 RID: 2553
		Improved,
		// Token: 0x040009FA RID: 2554
		Crafted,
		// Token: 0x040009FB RID: 2555
		Superior,
		// Token: 0x040009FC RID: 2556
		Enchanted,
		// Token: 0x040009FD RID: 2557
		Epic,
		// Token: 0x040009FE RID: 2558
		Legendary,
		// Token: 0x040009FF RID: 2559
		_LastDoNotUse
	}
}
