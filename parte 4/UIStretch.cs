using System;
using UnityEngine;

// Token: 0x02000138 RID: 312
[AddComponentMenu("NGUI/UI/Stretch")]
[ExecuteInEditMode]
public class UIStretch : MonoBehaviour
{
	// Token: 0x06000937 RID: 2359 RVA: 0x00031995 File Offset: 0x0002FB95
	private void OnEnable()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x000319CC File Offset: 0x0002FBCC
	private void Update()
	{
		if (!(this.uiCamera != null) || this.style == UIStretch.Style.None)
		{
			return;
		}
		if (this.mTrans == null)
		{
			this.mTrans = base.transform;
		}
		Rect pixelRect = this.uiCamera.pixelRect;
		float num = pixelRect.width;
		float num2 = pixelRect.height;
		if (this.mRoot != null && !this.mRoot.automatic && num2 > 1f)
		{
			float num3 = (float)this.mRoot.manualHeight / num2;
			num *= num3;
			num2 *= num3;
		}
		Vector3 localScale = this.mTrans.localScale;
		if (this.style == UIStretch.Style.BasedOnHeight)
		{
			localScale.x = this.relativeSize.x * num2;
			localScale.y = this.relativeSize.y * num2;
		}
		else
		{
			if (this.style == UIStretch.Style.Both || this.style == UIStretch.Style.Horizontal)
			{
				localScale.x = this.relativeSize.x * num;
			}
			if (this.style == UIStretch.Style.Both || this.style == UIStretch.Style.Vertical)
			{
				localScale.y = this.relativeSize.y * num2;
			}
		}
		if (this.mTrans.localScale != localScale)
		{
			this.mTrans.localScale = localScale;
		}
	}

	// Token: 0x040007FF RID: 2047
	public Camera uiCamera;

	// Token: 0x04000800 RID: 2048
	public UIStretch.Style style;

	// Token: 0x04000801 RID: 2049
	public Vector2 relativeSize = Vector2.one;

	// Token: 0x04000802 RID: 2050
	private Transform mTrans;

	// Token: 0x04000803 RID: 2051
	private UIRoot mRoot;

	// Token: 0x0200021B RID: 539
	public enum Style
	{
		// Token: 0x04000C31 RID: 3121
		None,
		// Token: 0x04000C32 RID: 3122
		Horizontal,
		// Token: 0x04000C33 RID: 3123
		Vertical,
		// Token: 0x04000C34 RID: 3124
		Both,
		// Token: 0x04000C35 RID: 3125
		BasedOnHeight
	}
}
