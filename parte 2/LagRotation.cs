using System;
using UnityEngine;

// Token: 0x02000088 RID: 136
[AddComponentMenu("NGUI/Examples/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	// Token: 0x06000433 RID: 1075 RVA: 0x00012A58 File Offset: 0x00010C58
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localRotation;
		this.mAbsolute = this.mTrans.rotation;
		if (this.ignoreTimeScale)
		{
			UpdateManager.AddCoroutine(this, this.updateOrder, new UpdateManager.OnUpdate(this.CoroutineUpdate));
			return;
		}
		UpdateManager.AddLateUpdate(this, this.updateOrder, new UpdateManager.OnUpdate(this.CoroutineUpdate));
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00012ACC File Offset: 0x00010CCC
	private void CoroutineUpdate(float delta)
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			this.mAbsolute = Quaternion.Slerp(this.mAbsolute, parent.rotation * this.mRelative, delta * this.speed);
			this.mTrans.rotation = this.mAbsolute;
		}
	}

	// Token: 0x04000397 RID: 919
	public int updateOrder;

	// Token: 0x04000398 RID: 920
	public float speed = 10f;

	// Token: 0x04000399 RID: 921
	public bool ignoreTimeScale;

	// Token: 0x0400039A RID: 922
	private Transform mTrans;

	// Token: 0x0400039B RID: 923
	private Quaternion mRelative;

	// Token: 0x0400039C RID: 924
	private Quaternion mAbsolute;
}
