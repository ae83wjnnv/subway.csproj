using System;
using UnityEngine;

// Token: 0x02000087 RID: 135
[AddComponentMenu("NGUI/Examples/Lag Position")]
public class LagPosition : MonoBehaviour
{
	// Token: 0x0600042F RID: 1071 RVA: 0x000128C4 File Offset: 0x00010AC4
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localPosition;
		if (this.ignoreTimeScale)
		{
			UpdateManager.AddCoroutine(this, this.updateOrder, new UpdateManager.OnUpdate(this.CoroutineUpdate));
			return;
		}
		UpdateManager.AddLateUpdate(this, this.updateOrder, new UpdateManager.OnUpdate(this.CoroutineUpdate));
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x00012927 File Offset: 0x00010B27
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mAbsolute = this.mTrans.position;
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x00012948 File Offset: 0x00010B48
	private void CoroutineUpdate(float delta)
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			Vector3 vector = parent.position + parent.rotation * this.mRelative;
			this.mAbsolute.x = Mathf.Lerp(this.mAbsolute.x, vector.x, Mathf.Clamp01(delta * this.speed.x));
			this.mAbsolute.y = Mathf.Lerp(this.mAbsolute.y, vector.y, Mathf.Clamp01(delta * this.speed.y));
			this.mAbsolute.z = Mathf.Lerp(this.mAbsolute.z, vector.z, Mathf.Clamp01(delta * this.speed.z));
			this.mTrans.position = this.mAbsolute;
		}
	}

	// Token: 0x04000391 RID: 913
	public int updateOrder;

	// Token: 0x04000392 RID: 914
	public Vector3 speed = new Vector3(10f, 10f, 10f);

	// Token: 0x04000393 RID: 915
	public bool ignoreTimeScale;

	// Token: 0x04000394 RID: 916
	private Transform mTrans;

	// Token: 0x04000395 RID: 917
	private Vector3 mRelative;

	// Token: 0x04000396 RID: 918
	private Vector3 mAbsolute;
}
