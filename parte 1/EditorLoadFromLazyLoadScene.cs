using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class EditorLoadFromLazyLoadScene : MonoBehaviour
{
	// Token: 0x060002CE RID: 718 RVA: 0x0000C764 File Offset: 0x0000A964
	private void Start()
	{
		if (!Game.HasLoaded)
		{
			Debug.Log("LoadLevel Level Loades " + Time.frameCount.ToString());
			Application.LoadLevel("LoadScene");
		}
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0000C79E File Offset: 0x0000A99E
	private void Update()
	{
	}
}
