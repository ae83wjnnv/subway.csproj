using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class SmoothDampVector3
{
	// Token: 0x17000086 RID: 134
	// (get) Token: 0x0600058C RID: 1420 RVA: 0x0001BC4A File Offset: 0x00019E4A
	// (set) Token: 0x0600058D RID: 1421 RVA: 0x0001BC52 File Offset: 0x00019E52
	public Vector3 Target
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

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x0600058E RID: 1422 RVA: 0x0001BC5B File Offset: 0x00019E5B
	// (set) Token: 0x0600058F RID: 1423 RVA: 0x0001BC63 File Offset: 0x00019E63
	public Vector3 Value
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

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001BC6C File Offset: 0x00019E6C
	// (set) Token: 0x06000591 RID: 1425 RVA: 0x0001BC74 File Offset: 0x00019E74
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

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001BC7D File Offset: 0x00019E7D
	// (set) Token: 0x06000593 RID: 1427 RVA: 0x0001BC85 File Offset: 0x00019E85
	public Vector3 Velocity
	{
		get
		{
			return this.velocity;
		}
		set
		{
			this.velocity = value;
		}
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x0001BC8E File Offset: 0x00019E8E
	public SmoothDampVector3(Vector3 value, float smoothTime)
	{
		this.smoothTime = smoothTime;
		this.value = value;
		this.target = value;
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x0001BCB8 File Offset: 0x00019EB8
	public void Update()
	{
		float num = Mathf.SmoothDamp(this.value.x, this.target.x, ref this.velocity.x, this.smoothTime, float.PositiveInfinity, Time.deltaTime);
		if (!float.IsNaN(num))
		{
			this.value.x = num;
		}
		float num2 = Mathf.SmoothDamp(this.value.y, this.target.y, ref this.velocity.y, this.smoothTime, float.PositiveInfinity, Time.deltaTime);
		if (!float.IsNaN(num2))
		{
			this.value.y = num2;
		}
		float num3 = Mathf.SmoothDamp(this.value.z, this.target.z, ref this.velocity.z, this.smoothTime, float.PositiveInfinity, Time.deltaTime);
		if (!float.IsNaN(num3))
		{
			this.value.z = num3;
		}
	}

	// Token: 0x040004C8 RID: 1224
	private float smoothTime;

	// Token: 0x040004C9 RID: 1225
	private Vector3 value;

	// Token: 0x040004CA RID: 1226
	private Vector3 target;

	// Token: 0x040004CB RID: 1227
	private Vector3 velocity = Vector3.zero;
}
