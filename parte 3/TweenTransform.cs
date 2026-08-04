using System;
using UnityEngine;

// Token: 0x020000E6 RID: 230
[AddComponentMenu("NGUI/Tween/Transform")]
public class TweenTransform : UITweener
{
	// Token: 0x06000691 RID: 1681 RVA: 0x00020734 File Offset: 0x0001E934
	protected override void OnUpdate(float factor)
	{
		if (this.from != null && this.to != null)
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			this.mTrans.position = this.from.position * (1f - factor) + this.to.position * factor;
			this.mTrans.localScale = this.from.localScale * (1f - factor) + this.to.localScale * factor;
			this.mTrans.rotation = Quaternion.Slerp(this.from.rotation, this.to.rotation, factor);
		}
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x00020814 File Offset: 0x0001EA14
	public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
	{
		TweenTransform tweenTransform = UITweener.Begin<TweenTransform>(go, duration);
		tweenTransform.from = from;
		tweenTransform.to = to;
		return tweenTransform;
	}

	// Token: 0x0400059B RID: 1435
	public Transform from;

	// Token: 0x0400059C RID: 1436
	public Transform to;

	// Token: 0x0400059D RID: 1437
	private Transform mTrans;
}
