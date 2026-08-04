using System;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class ScrollPanelPixelPerfect : MonoBehaviour
{
	// Token: 0x06000573 RID: 1395 RVA: 0x0001B873 File Offset: 0x00019A73
	private void Start()
	{
		this._transform = base.transform;
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x0001B884 File Offset: 0x00019A84
	private void Update()
	{
		this._transform.localPosition = new Vector3(this._transform.localPosition.x, Mathf.Round(this._transform.localPosition.y), this._transform.localPosition.z);
	}

	// Token: 0x040004BA RID: 1210
	private Transform _transform;
}
