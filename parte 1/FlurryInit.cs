using System;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class FlurryInit : MonoBehaviour
{
	// Token: 0x060002E2 RID: 738 RVA: 0x0000CCB7 File Offset: 0x0000AEB7
	private void Awake()
	{
		Flurry.StartSession("CIJUE322XIHTDWNV519J");
	}

	// Token: 0x04000219 RID: 537
	private const string API_KEY = "CIJUE322XIHTDWNV519J";
}
