using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class Randomizer : Selector
{
	// Token: 0x0600053E RID: 1342 RVA: 0x00019464 File Offset: 0x00017664
	public override void PerformSelection(List<GameObject> objectsToVisit)
	{
		int num = Random.Range(0, base.transform.childCount);
		for (int i = 0; i < base.transform.childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			if (i == num)
			{
				objectsToVisit.Add(gameObject);
			}
			else
			{
				gameObject.SetActiveRecursively(false);
			}
		}
	}
}
