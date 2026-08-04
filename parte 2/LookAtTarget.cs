using System;
using UnityEngine;

// Token: 0x0200008D RID: 141
[AddComponentMenu("NGUI/Examples/Look At Target")]
public class LookAtTarget : MonoBehaviour
{
	// Token: 0x06000448 RID: 1096 RVA: 0x00012EBF File Offset: 0x000110BF
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x00012ED0 File Offset: 0x000110D0
	private void LateUpdate()
	{
		if (this.target != null)
		{
			Vector3 vector = this.target.position - this.mTrans.position;
			if (vector.magnitude > 0.001f)
			{
				Quaternion quaternion = Quaternion.LookRotation(vector);
				this.mTrans.rotation = Quaternion.Slerp(this.mTrans.rotation, quaternion, Mathf.Clamp01(this.speed * Time.deltaTime));
			}
		}
	}

	// Token: 0x040003A4 RID: 932
	public int level;

	// Token: 0x040003A5 RID: 933
	public Transform target;

	// Token: 0x040003A6 RID: 934
	public float speed = 8f;

	// Token: 0x040003A7 RID: 935
	private Transform mTrans;
}
