using System;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class UINode
{
	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06000852 RID: 2130 RVA: 0x0002B8CB File Offset: 0x00029ACB
	// (set) Token: 0x06000853 RID: 2131 RVA: 0x0002B8ED File Offset: 0x00029AED
	public int visibleFlag
	{
		get
		{
			if (this.widget != null)
			{
				return this.widget.visibleFlag;
			}
			return this.mVisibleFlag;
		}
		set
		{
			if (this.widget != null)
			{
				this.widget.visibleFlag = value;
				return;
			}
			this.mVisibleFlag = value;
		}
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x0002B914 File Offset: 0x00029B14
	public UINode(Transform t)
	{
		this.trans = t;
		this.lastPos = this.trans.localPosition;
		this.lastRot = this.trans.localRotation;
		this.lastScale = this.trans.localScale;
		this.mGo = t.gameObject;
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0002B97C File Offset: 0x00029B7C
	public bool HasChanged()
	{
		bool flag = this.mGo.active && (this.widget == null || (this.widget.enabled && this.widget.color.a > 0.001f));
		if (this.lastActive != flag || (flag && (this.lastPos != this.trans.localPosition || this.lastRot != this.trans.localRotation || this.lastScale != this.trans.localScale)))
		{
			this.lastActive = flag;
			this.lastPos = this.trans.localPosition;
			this.lastRot = this.trans.localRotation;
			this.lastScale = this.trans.localScale;
			return true;
		}
		return false;
	}

	// Token: 0x0400074A RID: 1866
	private int mVisibleFlag = -1;

	// Token: 0x0400074B RID: 1867
	public Transform trans;

	// Token: 0x0400074C RID: 1868
	public UIWidget widget;

	// Token: 0x0400074D RID: 1869
	public bool lastActive;

	// Token: 0x0400074E RID: 1870
	public Vector3 lastPos;

	// Token: 0x0400074F RID: 1871
	public Quaternion lastRot;

	// Token: 0x04000750 RID: 1872
	public Vector3 lastScale;

	// Token: 0x04000751 RID: 1873
	public int changeFlag = -1;

	// Token: 0x04000752 RID: 1874
	private GameObject mGo;
}
