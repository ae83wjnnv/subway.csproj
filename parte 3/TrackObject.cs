using System;
using UnityEngine;

// Token: 0x020000DE RID: 222
public class TrackObject : MonoBehaviour
{
	// Token: 0x06000667 RID: 1639 RVA: 0x0002008A File Offset: 0x0001E28A
	public void Activate()
	{
		if (this.OnActivate != null)
		{
			this.OnActivate();
		}
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0002009F File Offset: 0x0001E29F
	public void Deactivate()
	{
		if (this.OnDeactivate != null)
		{
			this.OnDeactivate();
		}
	}

	// Token: 0x04000572 RID: 1394
	public TrackObject.OnActivateDelegate OnActivate;

	// Token: 0x04000573 RID: 1395
	public TrackObject.OnDeactivateDelegate OnDeactivate;

	// Token: 0x020001EE RID: 494
	// (Invoke) Token: 0x06000C31 RID: 3121
	public delegate void OnActivateDelegate();

	// Token: 0x020001EF RID: 495
	// (Invoke) Token: 0x06000C35 RID: 3125
	public delegate void OnDeactivateDelegate();
}
