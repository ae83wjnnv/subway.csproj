using System;
using UnityEngine;

// Token: 0x0200014C RID: 332
public class VisibleObject : MonoBehaviour
{
	// Token: 0x060009CB RID: 2507 RVA: 0x00036332 File Offset: 0x00034532
	public void OnBecameVisible()
	{
		if (this.OnVisibleChange != null)
		{
			this.OnVisibleChange(true);
		}
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x00036348 File Offset: 0x00034548
	public void OnBecameInvisible()
	{
		if (this.OnVisibleChange != null)
		{
			this.OnVisibleChange(false);
		}
	}

	// Token: 0x04000879 RID: 2169
	public VisibleObject.OnVisibleChangeDelegate OnVisibleChange;

	// Token: 0x02000229 RID: 553
	// (Invoke) Token: 0x06000CA6 RID: 3238
	public delegate void OnVisibleChangeDelegate(bool isVisible);
}
