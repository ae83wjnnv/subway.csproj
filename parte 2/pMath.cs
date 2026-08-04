using System;
using UnityEngine;

// Token: 0x02000152 RID: 338
public class pMath
{
	// Token: 0x060009E6 RID: 2534 RVA: 0x00036F58 File Offset: 0x00035158
	public static float Bell(float x)
	{
		return Mathf.SmoothStep(0f, 1f, 1f - Mathf.Abs(x - 0.5f) / 0.5f);
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x00036F81 File Offset: 0x00035181
	public static float Lerp(float xFrom, float xTo, float x, float yFrom, float yTo)
	{
		return Mathf.Lerp(yFrom, yTo, Mathf.Clamp01((x - xFrom) / (xTo - xFrom)));
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x00036F97 File Offset: 0x00035197
	public static float Square(float x)
	{
		if (x - (float)Mathf.FloorToInt(x) < 0.5f)
		{
			return 0f;
		}
		return 1f;
	}
}
