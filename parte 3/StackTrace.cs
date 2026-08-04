using System;
using UnityEngine;

// Token: 0x020000D3 RID: 211
public class StackTrace : MonoBehaviour
{
	// Token: 0x06000621 RID: 1569 RVA: 0x0001ED43 File Offset: 0x0001CF43
	private void OnDisable()
	{
		Debug.Log("I was disabled!!");
		Debug.Break();
	}
}
