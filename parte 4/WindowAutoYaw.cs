using System;
using UnityEngine;

// Token: 0x0200014E RID: 334
[AddComponentMenu("NGUI/Examples/Window Auto-Yaw")]
public class WindowAutoYaw : MonoBehaviour
{
	// Token: 0x060009D4 RID: 2516 RVA: 0x00036465 File Offset: 0x00034665
	private void OnDisable()
	{
		this.mTrans.localRotation = Quaternion.identity;
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x00036478 File Offset: 0x00034678
	private void Start()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mTrans = base.transform;
		UpdateManager.AddCoroutine(this, this.updateOrder, new UpdateManager.OnUpdate(this.CoroutineUpdate));
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x000364D0 File Offset: 0x000346D0
	private void CoroutineUpdate(float delta)
	{
		if (this.uiCamera != null)
		{
			Vector3 vector = this.uiCamera.WorldToViewportPoint(this.mTrans.position);
			this.mTrans.localRotation = Quaternion.Euler(0f, (vector.x * 2f - 1f) * this.yawAmount, 0f);
		}
	}

	// Token: 0x0400087C RID: 2172
	public int updateOrder;

	// Token: 0x0400087D RID: 2173
	public Camera uiCamera;

	// Token: 0x0400087E RID: 2174
	public float yawAmount = 20f;

	// Token: 0x0400087F RID: 2175
	private Transform mTrans;
}
