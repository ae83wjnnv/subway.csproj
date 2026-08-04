using System;
using UnityEngine;

// Token: 0x020000A3 RID: 163
[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithMouse : IgnoreTimeScale
{
	// Token: 0x060004DE RID: 1246 RVA: 0x000177E8 File Offset: 0x000159E8
	private void Start()
	{
		this.mTrans = base.transform;
		this.mStart = this.mTrans.localRotation;
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x00017808 File Offset: 0x00015A08
	private void Update()
	{
		float num = base.UpdateRealTimeDelta();
		Vector3 mousePosition = Input.mousePosition;
		float num2 = (float)Screen.width * 0.5f;
		float num3 = (float)Screen.height * 0.5f;
		if (this.range < 0.1f)
		{
			this.range = 0.1f;
		}
		float num4 = Mathf.Clamp((mousePosition.x - num2) / num2 / this.range, -1f, 1f);
		float num5 = Mathf.Clamp((mousePosition.y - num3) / num3 / this.range, -1f, 1f);
		this.mRot = Vector2.Lerp(this.mRot, new Vector2(num4, num5), num * 5f);
		this.mTrans.localRotation = this.mStart * Quaternion.Euler((0f - this.mRot.y) * this.degrees.y, this.mRot.x * this.degrees.x, 0f);
	}

	// Token: 0x04000411 RID: 1041
	public Vector2 degrees = new Vector2(5f, 3f);

	// Token: 0x04000412 RID: 1042
	public float range = 1f;

	// Token: 0x04000413 RID: 1043
	private Transform mTrans;

	// Token: 0x04000414 RID: 1044
	private Quaternion mStart;

	// Token: 0x04000415 RID: 1045
	private Vector2 mRot = Vector2.zero;
}
