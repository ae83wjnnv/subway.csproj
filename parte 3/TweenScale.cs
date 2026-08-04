using System;
using UnityEngine;

// Token: 0x020000E5 RID: 229
[AddComponentMenu("NGUI/Tween/Scale")]
public class TweenScale : UITweener
{
	// Token: 0x1700009E RID: 158
	// (get) Token: 0x0600068B RID: 1675 RVA: 0x00020635 File Offset: 0x0001E835
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

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x0600068C RID: 1676 RVA: 0x00020657 File Offset: 0x0001E857
	// (set) Token: 0x0600068D RID: 1677 RVA: 0x00020664 File Offset: 0x0001E864
	public Vector3 scale
	{
		get
		{
			return this.cachedTransform.localScale;
		}
		set
		{
			this.cachedTransform.localScale = value;
		}
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x00020674 File Offset: 0x0001E874
	protected override void OnUpdate(float factor)
	{
		this.cachedTransform.localScale = this.from * (1f - factor) + this.to * factor;
		if (!this.updateTable)
		{
			return;
		}
		if (this.mTable == null)
		{
			this.mTable = NGUITools.FindInParents<UITable>(base.gameObject);
			if (this.mTable == null)
			{
				this.updateTable = false;
				return;
			}
		}
		this.mTable.repositionNow = true;
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x000206F9 File Offset: 0x0001E8F9
	public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
	{
		TweenScale tweenScale = UITweener.Begin<TweenScale>(go, duration);
		tweenScale.from = tweenScale.scale;
		tweenScale.to = scale;
		return tweenScale;
	}

	// Token: 0x04000596 RID: 1430
	public Vector3 from = Vector3.one;

	// Token: 0x04000597 RID: 1431
	public Vector3 to = Vector3.one;

	// Token: 0x04000598 RID: 1432
	public bool updateTable;

	// Token: 0x04000599 RID: 1433
	private Transform mTrans;

	// Token: 0x0400059A RID: 1434
	private UITable mTable;
}
