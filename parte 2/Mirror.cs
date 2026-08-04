using System;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class Mirror : MonoBehaviour
{
	// Token: 0x0600044F RID: 1103 RVA: 0x00012FD4 File Offset: 0x000111D4
	private void Awake()
	{
		this.trackObject = base.GetComponent<TrackObject>() ?? base.gameObject.AddComponent<TrackObject>();
		TrackObject trackObject = this.trackObject;
		trackObject.OnActivate = (TrackObject.OnActivateDelegate)Delegate.Combine(trackObject.OnActivate, new TrackObject.OnActivateDelegate(this.OnActivate));
		this.children = new Transform[base.transform.childCount];
		for (int i = 0; i < base.transform.childCount; i++)
		{
			this.children[i] = base.transform.GetChild(i);
		}
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x00013064 File Offset: 0x00011264
	public void OnActivate()
	{
		int num = Random.Range(0, 2) * 2 - 1;
		for (int i = 0; i < this.children.Length; i++)
		{
			Vector3 localPosition = this.children[i].localPosition;
			localPosition.x *= (float)num;
			this.children[i].localPosition = localPosition;
		}
	}

	// Token: 0x040003A9 RID: 937
	private Transform[] children;

	// Token: 0x040003AA RID: 938
	private TrackObject trackObject;
}
