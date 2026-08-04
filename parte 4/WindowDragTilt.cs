using System;
using UnityEngine;

// Token: 0x0200014F RID: 335
[AddComponentMenu("NGUI/Examples/Window Drag Tilt")]
public class WindowDragTilt : MonoBehaviour
{
	// Token: 0x060009D8 RID: 2520 RVA: 0x00036548 File Offset: 0x00034748
	private void Start()
	{
		UpdateManager.AddCoroutine(this, this.updateOrder, new UpdateManager.OnUpdate(this.CoroutineUpdate));
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x00036562 File Offset: 0x00034762
	private void OnEnable()
	{
		this.mInit = true;
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x0003656C File Offset: 0x0003476C
	private void CoroutineUpdate(float delta)
	{
		if (this.mInit)
		{
			this.mInit = false;
			this.mTrans = base.transform;
			this.mLastPos = this.mTrans.position;
		}
		Vector3 vector = this.mTrans.position - this.mLastPos;
		this.mLastPos = this.mTrans.position;
		this.mAngle += vector.x * this.degrees;
		this.mAngle = NGUIMath.SpringLerp(this.mAngle, 0f, 20f, delta);
		this.mTrans.localRotation = Quaternion.Euler(0f, 0f, 0f - this.mAngle);
	}

	// Token: 0x04000880 RID: 2176
	public int updateOrder;

	// Token: 0x04000881 RID: 2177
	public float degrees = 30f;

	// Token: 0x04000882 RID: 2178
	private Vector3 mLastPos;

	// Token: 0x04000883 RID: 2179
	private Transform mTrans;

	// Token: 0x04000884 RID: 2180
	private float mAngle;

	// Token: 0x04000885 RID: 2181
	private bool mInit = true;
}
