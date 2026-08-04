using System;
using UnityEngine;

// Token: 0x020000CD RID: 205
[AddComponentMenu("NGUI/Examples/Spin")]
public class Spin : MonoBehaviour
{
	// Token: 0x0600060D RID: 1549 RVA: 0x0001E7BB File Offset: 0x0001C9BB
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRb = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x0001E7D5 File Offset: 0x0001C9D5
	private void Update()
	{
		if (this.mRb == null)
		{
			this.ApplyDelta(Time.deltaTime);
		}
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x0001E7F0 File Offset: 0x0001C9F0
	private void FixedUpdate()
	{
		if (this.mRb != null)
		{
			this.ApplyDelta(Time.deltaTime);
		}
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x0001E80C File Offset: 0x0001CA0C
	public void ApplyDelta(float delta)
	{
		delta *= 360f;
		Quaternion quaternion = Quaternion.Euler(this.rotationsPerSecond * delta);
		if (this.mRb == null)
		{
			this.mTrans.rotation *= quaternion;
			return;
		}
		this.mRb.MoveRotation(this.mRb.rotation * quaternion);
	}

	// Token: 0x04000516 RID: 1302
	public Vector3 rotationsPerSecond = new Vector3(0f, 0.1f, 0f);

	// Token: 0x04000517 RID: 1303
	private Rigidbody mRb;

	// Token: 0x04000518 RID: 1304
	private Transform mTrans;
}
