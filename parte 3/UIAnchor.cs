using System;
using UnityEngine;

// Token: 0x020000E8 RID: 232
[AddComponentMenu("NGUI/UI/Anchor")]
[ExecuteInEditMode]
public class UIAnchor : MonoBehaviour
{
	// Token: 0x06000696 RID: 1686 RVA: 0x00020960 File Offset: 0x0001EB60
	private void Start()
	{
		if (this.stretchToFill)
		{
			this.stretchToFill = false;
			UIStretch uistretch = base.gameObject.AddComponent<UIStretch>();
			uistretch.style = UIStretch.Style.Both;
			uistretch.uiCamera = this.uiCamera;
		}
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x00020990 File Offset: 0x0001EB90
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mIsWindows = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WindowsEditor;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x000209F0 File Offset: 0x0001EBF0
	private void Update()
	{
		if (!(this.uiCamera != null))
		{
			return;
		}
		Rect pixelRect = this.uiCamera.pixelRect;
		float num = (pixelRect.xMin + pixelRect.xMax) * 0.5f;
		float num2 = (pixelRect.yMin + pixelRect.yMax) * 0.5f;
		Vector3 vector = new Vector3(num, num2, this.depthOffset);
		if (this.side != UIAnchor.Side.Center)
		{
			if (this.side == UIAnchor.Side.Right || this.side == UIAnchor.Side.TopRight || this.side == UIAnchor.Side.BottomRight)
			{
				vector.x = pixelRect.xMax;
			}
			else if (this.side == UIAnchor.Side.Top || this.side == UIAnchor.Side.Center || this.side == UIAnchor.Side.Bottom)
			{
				vector.x = num;
			}
			else
			{
				vector.x = pixelRect.xMin;
			}
			if (this.side == UIAnchor.Side.Top || this.side == UIAnchor.Side.TopRight || this.side == UIAnchor.Side.TopLeft)
			{
				vector.y = pixelRect.yMax;
			}
			else if (this.side == UIAnchor.Side.Left || this.side == UIAnchor.Side.Center || this.side == UIAnchor.Side.Right)
			{
				vector.y = num2;
			}
			else
			{
				vector.y = pixelRect.yMin;
			}
		}
		float width = pixelRect.width;
		float height = pixelRect.height;
		vector.x += this.relativeOffset.x * width;
		vector.y += this.relativeOffset.y * height;
		if (this.uiCamera.orthographic)
		{
			vector.x = (float)Mathf.RoundToInt(vector.x);
			vector.y = (float)Mathf.RoundToInt(vector.y);
			if (this.halfPixelOffset && this.mIsWindows)
			{
				vector.x -= 0.5f;
				vector.y += 0.5f;
			}
		}
		vector = this.uiCamera.ScreenToWorldPoint(vector);
		if (this.mTrans.position != vector)
		{
			this.mTrans.position = vector;
		}
	}

	// Token: 0x040005A3 RID: 1443
	public Camera uiCamera;

	// Token: 0x040005A4 RID: 1444
	public UIAnchor.Side side = UIAnchor.Side.Center;

	// Token: 0x040005A5 RID: 1445
	public bool halfPixelOffset = true;

	// Token: 0x040005A6 RID: 1446
	public float depthOffset;

	// Token: 0x040005A7 RID: 1447
	public Vector2 relativeOffset = Vector2.zero;

	// Token: 0x040005A8 RID: 1448
	[SerializeField]
	[HideInInspector]
	private bool stretchToFill;

	// Token: 0x040005A9 RID: 1449
	private Transform mTrans;

	// Token: 0x040005AA RID: 1450
	private bool mIsWindows;

	// Token: 0x020001F4 RID: 500
	public enum Side
	{
		// Token: 0x04000B94 RID: 2964
		BottomLeft,
		// Token: 0x04000B95 RID: 2965
		Left,
		// Token: 0x04000B96 RID: 2966
		TopLeft,
		// Token: 0x04000B97 RID: 2967
		Top,
		// Token: 0x04000B98 RID: 2968
		TopRight,
		// Token: 0x04000B99 RID: 2969
		Right,
		// Token: 0x04000B9A RID: 2970
		BottomRight,
		// Token: 0x04000B9B RID: 2971
		Bottom,
		// Token: 0x04000B9C RID: 2972
		Center
	}
}
