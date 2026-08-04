using System;
using UnityEngine;

// Token: 0x020000A7 RID: 167
public class PickupRotate : MonoBehaviour
{
	// Token: 0x060004ED RID: 1261 RVA: 0x00017C30 File Offset: 0x00015E30
	public void Awake()
	{
		TrackObject trackObject = base.GetComponent<TrackObject>() ?? base.gameObject.AddComponent<TrackObject>();
		trackObject.OnActivate = (TrackObject.OnActivateDelegate)Delegate.Combine(trackObject.OnActivate, new TrackObject.OnActivateDelegate(this.OnActivate));
		VisibleObject componentInChildren = base.GetComponentInChildren<VisibleObject>();
		if (componentInChildren != null)
		{
			componentInChildren.OnVisibleChange = (VisibleObject.OnVisibleChangeDelegate)Delegate.Combine(componentInChildren.OnVisibleChange, new VisibleObject.OnVisibleChangeDelegate(delegate(bool visible)
			{
				base.enabled = visible;
			}));
		}
		base.enabled = false;
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x00017CAC File Offset: 0x00015EAC
	private void OnActivate()
	{
		this.z = base.transform.position.z;
		base.enabled = true;
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00017CCB File Offset: 0x00015ECB
	private void Update()
	{
		this.target.localRotation = Quaternion.Euler(0f, Time.time * this.speed + this.z * this.rotatePhase, 0f);
	}

	// Token: 0x04000426 RID: 1062
	public Transform target;

	// Token: 0x04000427 RID: 1063
	public float speed = 180f;

	// Token: 0x04000428 RID: 1064
	public float rotatePhase = 0.9f;

	// Token: 0x04000429 RID: 1065
	private float z;
}
