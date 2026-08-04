using System;
using UnityEngine;

// Token: 0x020000D1 RID: 209
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Internal/Spring Panel")]
public class SpringPanel : IgnoreTimeScale
{
	// Token: 0x06000619 RID: 1561 RVA: 0x0001E9B3 File Offset: 0x0001CBB3
	private void Start()
	{
		this.mPanel = base.GetComponent<UIPanel>();
		this.mDrag = base.GetComponent<UIDraggablePanel>();
		this.mTrans = base.transform;
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0001E9DC File Offset: 0x0001CBDC
	private void Update()
	{
		float num = base.UpdateRealTimeDelta();
		if (this.mThreshold == 0f)
		{
			this.mThreshold = (this.target - this.mTrans.localPosition).magnitude * 0.005f;
		}
		Vector3 localPosition = this.mTrans.localPosition;
		this.mTrans.localPosition = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, num);
		Vector3 vector = this.mTrans.localPosition - localPosition;
		Vector4 clipRange = this.mPanel.clipRange;
		clipRange.x -= vector.x;
		clipRange.y -= vector.y;
		this.mPanel.clipRange = clipRange;
		if (this.mDrag != null)
		{
			this.mDrag.UpdateScrollbars(false);
		}
		if (this.mThreshold >= (this.target - this.mTrans.localPosition).magnitude)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0001EAF0 File Offset: 0x0001CCF0
	public static SpringPanel Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPanel springPanel = go.GetComponent<SpringPanel>();
		if (springPanel == null)
		{
			springPanel = go.AddComponent<SpringPanel>();
		}
		springPanel.target = pos;
		springPanel.strength = strength;
		if (!springPanel.enabled)
		{
			springPanel.mThreshold = 0f;
			springPanel.enabled = true;
		}
		return springPanel;
	}

	// Token: 0x0400051E RID: 1310
	public Vector3 target = Vector3.zero;

	// Token: 0x0400051F RID: 1311
	public float strength = 10f;

	// Token: 0x04000520 RID: 1312
	private UIPanel mPanel;

	// Token: 0x04000521 RID: 1313
	private Transform mTrans;

	// Token: 0x04000522 RID: 1314
	private float mThreshold;

	// Token: 0x04000523 RID: 1315
	private UIDraggablePanel mDrag;
}
