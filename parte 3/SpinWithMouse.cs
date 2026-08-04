using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
[AddComponentMenu("NGUI/Examples/Spin With Mouse")]
public class SpinWithMouse : MonoBehaviour
{
	// Token: 0x06000612 RID: 1554 RVA: 0x0001E898 File Offset: 0x0001CA98
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0001E8A8 File Offset: 0x0001CAA8
	private void OnDrag(Vector2 delta)
	{
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		if (this.target != null)
		{
			this.target.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.target.localRotation;
			return;
		}
		this.mTrans.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.mTrans.localRotation;
	}

	// Token: 0x04000519 RID: 1305
	public Transform target;

	// Token: 0x0400051A RID: 1306
	public float speed = 1f;

	// Token: 0x0400051B RID: 1307
	private Transform mTrans;
}
