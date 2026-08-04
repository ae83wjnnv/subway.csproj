using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class RandomizerTarget : Selector
{
	// Token: 0x06000540 RID: 1344 RVA: 0x000194C8 File Offset: 0x000176C8
	public override void PerformSelection(List<GameObject> objectsToVisit)
	{
		int num = Random.Range(0, this.Targets.Count);
		for (int i = 0; i < this.Targets.Count; i++)
		{
			GameObject gameObject = this.Targets[i];
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

	// Token: 0x04000468 RID: 1128
	public List<GameObject> Targets;
}
