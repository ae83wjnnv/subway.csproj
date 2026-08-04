using System;
using UnityEngine;

// Token: 0x02000142 RID: 322
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Viewport Camera")]
[ExecuteInEditMode]
public class UIViewport : MonoBehaviour
{
	// Token: 0x0600096E RID: 2414 RVA: 0x000338E0 File Offset: 0x00031AE0
	private void Start()
	{
		this.mCam = base.GetComponent<Camera>();
		if (this.sourceCamera == null)
		{
			this.sourceCamera = Camera.main;
		}
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x00033908 File Offset: 0x00031B08
	private void LateUpdate()
	{
		if (this.topLeft != null && this.bottomRight != null)
		{
			Vector3 vector = this.sourceCamera.WorldToScreenPoint(this.topLeft.position);
			Vector3 vector2 = this.sourceCamera.WorldToScreenPoint(this.bottomRight.position);
			Rect rect = new Rect(vector.x / (float)Screen.width, vector2.y / (float)Screen.height, (vector2.x - vector.x) / (float)Screen.width, (vector.y - vector2.y) / (float)Screen.height);
			float num = this.fullSize * rect.height;
			if (rect != this.mCam.rect)
			{
				this.mCam.rect = rect;
			}
			if (this.mCam.orthographicSize != num)
			{
				this.mCam.orthographicSize = num;
			}
		}
	}

	// Token: 0x0400083B RID: 2107
	public Camera sourceCamera;

	// Token: 0x0400083C RID: 2108
	public Transform topLeft;

	// Token: 0x0400083D RID: 2109
	public Transform bottomRight;

	// Token: 0x0400083E RID: 2110
	public float fullSize = 1f;

	// Token: 0x0400083F RID: 2111
	private Camera mCam;
}
