using System;
using UnityEngine;

// Token: 0x020000B2 RID: 178
public class RendererActivator : MonoBehaviour
{
	// Token: 0x06000542 RID: 1346 RVA: 0x00019528 File Offset: 0x00017728
	public void Awake()
	{
		TrackObject trackObject = base.GetComponent<TrackObject>() ?? base.gameObject.AddComponent<TrackObject>();
		trackObject.OnActivate = (TrackObject.OnActivateDelegate)Delegate.Combine(trackObject.OnActivate, new TrackObject.OnActivateDelegate(this.OnActivate));
		trackObject.OnDeactivate = (TrackObject.OnDeactivateDelegate)Delegate.Combine(trackObject.OnDeactivate, new TrackObject.OnDeactivateDelegate(this.OnDeactivate));
		base.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00019599 File Offset: 0x00017799
	public void OnActivate()
	{
		base.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x000195A7 File Offset: 0x000177A7
	public void OnDeactivate()
	{
		base.GetComponent<Renderer>().enabled = false;
	}
}
