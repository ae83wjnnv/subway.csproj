using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class Rotator : MonoBehaviour
{
	// Token: 0x06000557 RID: 1367 RVA: 0x00019C18 File Offset: 0x00017E18
	private void Start()
	{
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x00019C1A File Offset: 0x00017E1A
	private void Update()
	{
		base.transform.Rotate(new Vector3(0.5f, 0f, 0f));
	}
}
