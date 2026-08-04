using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BE RID: 190
public abstract class Selector : MonoBehaviour
{
	// Token: 0x06000576 RID: 1398
	public abstract void PerformSelection(List<GameObject> objectsToVisit);

	// Token: 0x06000577 RID: 1399 RVA: 0x0001B8E0 File Offset: 0x00019AE0
	public void Awake()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).gameObject.SetActiveRecursively(false);
		}
	}
}
