using System;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public class SmoothDampFloat
{
	// Token: 0x17000083 RID: 131
	// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001BBBE File Offset: 0x00019DBE
	// (set) Token: 0x06000585 RID: 1413 RVA: 0x0001BBC6 File Offset: 0x00019DC6
	public float Target
	{
		get
		{
			return this.target;
		}
		set
		{
			this.target = value;
		}
	}

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001BBCF File Offset: 0x00019DCF
	// (set) Token: 0x06000587 RID: 1415 RVA: 0x0001BBD7 File Offset: 0x00019DD7
	public float Value
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001BBE0 File Offset: 0x00019DE0
	// (set) Token: 0x06000589 RID: 1417 RVA: 0x0001BBE8 File Offset: 0x00019DE8
	public float SmoothTime
	{
		get
		{
			return this.smoothTime;
		}
		set
		{
			this.smoothTime = value;
		}
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0001BBF1 File Offset: 0x00019DF1
	public SmoothDampFloat(float value, float smoothTime)
	{
		this.smoothTime = smoothTime;
		this.value = value;
		this.target = value;
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0001BC10 File Offset: 0x00019E10
	public void Update()
	{
		float num = Mathf.SmoothDamp(this.value, this.target, ref this.valueSpeed, this.smoothTime);
		if (!float.IsNaN(num))
		{
			this.value = num;
		}
	}

	// Token: 0x040004C4 RID: 1220
	private float smoothTime;

	// Token: 0x040004C5 RID: 1221
	private float value;

	// Token: 0x040004C6 RID: 1222
	public float valueSpeed;

	// Token: 0x040004C7 RID: 1223
	private float target;
}
