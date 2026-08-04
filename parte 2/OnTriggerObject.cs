using System;
using UnityEngine;

// Token: 0x020000A2 RID: 162
public class OnTriggerObject : MonoBehaviour
{
	// Token: 0x060004DB RID: 1243 RVA: 0x000177B4 File Offset: 0x000159B4
	public void OnTriggerEnter(Collider collider)
	{
		if (this.OnEnter != null)
		{
			this.OnEnter(collider);
		}
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x000177CA File Offset: 0x000159CA
	public void OnTriggerExit(Collider collider)
	{
		if (this.OnExit != null)
		{
			this.OnExit(collider);
		}
	}

	// Token: 0x0400040F RID: 1039
	public OnTriggerObject.OnEnterDelegate OnEnter;

	// Token: 0x04000410 RID: 1040
	public OnTriggerObject.OnExitDelegate OnExit;

	// Token: 0x020001C7 RID: 455
	// (Invoke) Token: 0x06000B91 RID: 2961
	public delegate void OnEnterDelegate(Collider collider);

	// Token: 0x020001C8 RID: 456
	// (Invoke) Token: 0x06000B95 RID: 2965
	public delegate void OnExitDelegate(Collider collider);
}
