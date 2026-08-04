using System;
using UnityEngine;

// Token: 0x02000149 RID: 329
public static class Utility
{
	// Token: 0x060009BF RID: 2495 RVA: 0x000360F0 File Offset: 0x000342F0
	public static void SetLayerRecursively(Transform t, int layer)
	{
		t.gameObject.layer = layer;
		foreach (object obj in t)
		{
			Utility.SetLayerRecursively((Transform)obj, layer);
		}
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x00036150 File Offset: 0x00034350
	public static int NumberOfDigits(int number)
	{
		int num = 0;
		if (number == 0)
		{
			return 1;
		}
		while (number != 0)
		{
			number /= 10;
			num++;
		}
		return num;
	}
}
