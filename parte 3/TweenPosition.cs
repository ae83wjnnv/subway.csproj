using System;
using UnityEngine;

// Token: 0x020000E3 RID: 227
[AddComponentMenu("NGUI/Tween/Position")]
public class TweenPosition : UITweener
{
	// Token: 0x1700009A RID: 154
	// (get) Token: 0x0600067F RID: 1663 RVA: 0x000204FE File Offset: 0x0001E6FE
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x06000680 RID: 1664 RVA: 0x00020520 File Offset: 0x0001E720
	// (set) Token: 0x06000681 RID: 1665 RVA: 0x0002052D File Offset: 0x0001E72D
	public Vector3 position
	{
		get
		{
			return this.cachedTransform.localPosition;
		}
		set
		{
			this.cachedTransform.localPosition = value;
		}
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0002053B File Offset: 0x0001E73B
	protected override void OnUpdate(float factor)
	{
		this.cachedTransform.localPosition = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0002056B File Offset: 0x0001E76B
	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos)
	{
		TweenPosition tweenPosition = UITweener.Begin<TweenPosition>(go, duration);
		tweenPosition.from = tweenPosition.position;
		tweenPosition.to = pos;
		return tweenPosition;
	}

	// Token: 0x04000590 RID: 1424
	public Vector3 from;

	// Token: 0x04000591 RID: 1425
	public Vector3 to;

	// Token: 0x04000592 RID: 1426
	private Transform mTrans;
}
