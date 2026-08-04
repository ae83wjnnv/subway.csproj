using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007B RID: 123
[Serializable]
public class InvBaseItem
{
	// Token: 0x04000343 RID: 835
	public int id16;

	// Token: 0x04000344 RID: 836
	public string name;

	// Token: 0x04000345 RID: 837
	public string description;

	// Token: 0x04000346 RID: 838
	public InvBaseItem.Slot slot;

	// Token: 0x04000347 RID: 839
	public int minItemLevel = 1;

	// Token: 0x04000348 RID: 840
	public int maxItemLevel = 50;

	// Token: 0x04000349 RID: 841
	public List<InvStat> stats = new List<InvStat>();

	// Token: 0x0400034A RID: 842
	public GameObject attachment;

	// Token: 0x0400034B RID: 843
	public Color color = Color.white;

	// Token: 0x0400034C RID: 844
	public UIAtlas iconAtlas;

	// Token: 0x0400034D RID: 845
	public string iconName = string.Empty;

	// Token: 0x020001AD RID: 429
	public enum Slot
	{
		// Token: 0x040009E9 RID: 2537
		None,
		// Token: 0x040009EA RID: 2538
		Weapon,
		// Token: 0x040009EB RID: 2539
		Shield,
		// Token: 0x040009EC RID: 2540
		Body,
		// Token: 0x040009ED RID: 2541
		Shoulders,
		// Token: 0x040009EE RID: 2542
		Bracers,
		// Token: 0x040009EF RID: 2543
		Boots,
		// Token: 0x040009F0 RID: 2544
		Trinket,
		// Token: 0x040009F1 RID: 2545
		_LastDoNotUse
	}
}
