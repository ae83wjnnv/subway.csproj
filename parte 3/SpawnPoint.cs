using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class SpawnPoint : Selector
{
	// Token: 0x06000606 RID: 1542 RVA: 0x0001E311 File Offset: 0x0001C511
	public override void PerformSelection(List<GameObject> objectsToVisit)
	{
		SpawnPointManager.Instance.PerformSelection(this, objectsToVisit);
	}

	// Token: 0x04000503 RID: 1283
	public GameObject dailyLetter;

	// Token: 0x04000504 RID: 1284
	public GameObject doubleScoreMultiplier;

	// Token: 0x04000505 RID: 1285
	public GameObject jetpackPickup;

	// Token: 0x04000506 RID: 1286
	public GameObject jumpBooster;

	// Token: 0x04000507 RID: 1287
	public GameObject magnetBooster;

	// Token: 0x04000508 RID: 1288
	public GameObject mysteryBox;
}
