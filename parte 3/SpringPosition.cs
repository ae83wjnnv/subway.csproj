using System;
using UnityEngine;

// Token: 0x020000D2 RID: 210
[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : IgnoreTimeScale
{
	// Token: 0x0600061D RID: 1565 RVA: 0x0001EB5B File Offset: 0x0001CD5B
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0001EB6C File Offset: 0x0001CD6C
	private void Update()
	{
		float num = ((!this.ignoreTimeScale) ? Time.deltaTime : base.UpdateRealTimeDelta());
		if (this.worldSpace)
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.position).magnitude * 0.001f;
			}
			this.mTrans.position = NGUIMath.SpringLerp(this.mTrans.position, this.target, this.strength, num);
			if (this.mThreshold >= (this.target - this.mTrans.position).magnitude)
			{
				this.mTrans.position = this.target;
				base.enabled = false;
				return;
			}
		}
		else
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.localPosition).magnitude * 0.001f;
			}
			this.mTrans.localPosition = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, num);
			if (this.mThreshold >= (this.target - this.mTrans.localPosition).magnitude)
			{
				this.mTrans.localPosition = this.target;
				base.enabled = false;
			}
		}
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0001ECD8 File Offset: 0x0001CED8
	public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPosition springPosition = go.GetComponent<SpringPosition>();
		if (springPosition == null)
		{
			springPosition = go.AddComponent<SpringPosition>();
		}
		springPosition.target = pos;
		springPosition.strength = strength;
		if (!springPosition.enabled)
		{
			springPosition.mThreshold = 0f;
			springPosition.enabled = true;
		}
		return springPosition;
	}

	// Token: 0x04000524 RID: 1316
	public Vector3 target = Vector3.zero;

	// Token: 0x04000525 RID: 1317
	public float strength = 10f;

	// Token: 0x04000526 RID: 1318
	public bool worldSpace;

	// Token: 0x04000527 RID: 1319
	public bool ignoreTimeScale;

	// Token: 0x04000528 RID: 1320
	private Transform mTrans;

	// Token: 0x04000529 RID: 1321
	private float mThreshold;
}
