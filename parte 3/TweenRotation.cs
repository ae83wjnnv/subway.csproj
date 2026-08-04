using System;
using UnityEngine;

// Token: 0x020000E4 RID: 228
[AddComponentMenu("NGUI/Tween/Rotation")]
public class TweenRotation : UITweener
{
	// Token: 0x1700009C RID: 156
	// (get) Token: 0x06000685 RID: 1669 RVA: 0x0002058F File Offset: 0x0001E78F
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

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000686 RID: 1670 RVA: 0x000205B1 File Offset: 0x0001E7B1
	// (set) Token: 0x06000687 RID: 1671 RVA: 0x000205BE File Offset: 0x0001E7BE
	public Quaternion rotation
	{
		get
		{
			return this.cachedTransform.localRotation;
		}
		set
		{
			this.cachedTransform.localRotation = value;
		}
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x000205CC File Offset: 0x0001E7CC
	protected override void OnUpdate(float factor)
	{
		this.cachedTransform.localRotation = Quaternion.Slerp(Quaternion.Euler(this.from), Quaternion.Euler(this.to), factor);
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x000205F8 File Offset: 0x0001E7F8
	public static TweenRotation Begin(GameObject go, float duration, Quaternion rot)
	{
		TweenRotation tweenRotation = UITweener.Begin<TweenRotation>(go, duration);
		tweenRotation.from = tweenRotation.rotation.eulerAngles;
		tweenRotation.to = rot.eulerAngles;
		return tweenRotation;
	}

	// Token: 0x04000593 RID: 1427
	public Vector3 from;

	// Token: 0x04000594 RID: 1428
	public Vector3 to;

	// Token: 0x04000595 RID: 1429
	private Transform mTrans;
}
