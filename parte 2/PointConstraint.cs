using System;
using UnityEngine;

// Token: 0x020000AA RID: 170
public class PointConstraint : MonoBehaviour
{
	// Token: 0x06000531 RID: 1329 RVA: 0x00018F6B File Offset: 0x0001716B
	private void Awake()
	{
		this.transformCached = base.transform;
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00018F7C File Offset: 0x0001717C
	private void LateUpdate()
	{
		this.transformCached.position = new Vector3(this.master.position.x, 0f, this.master.position.z);
		this.transformCached.localPosition = new Vector3(this.transformCached.localPosition.x, 0f, this.transformCached.localPosition.z);
	}

	// Token: 0x0400044D RID: 1101
	public Transform master;

	// Token: 0x0400044E RID: 1102
	private Transform transformCached;
}
