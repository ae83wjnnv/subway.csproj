using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class SocialTest : MonoBehaviour
{
	// Token: 0x060005F8 RID: 1528 RVA: 0x0001E0A0 File Offset: 0x0001C2A0
	private void OnGUI()
	{
		if (!this.started)
		{
			GUILayout.BeginArea(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
			if (GUILayout.Button("Start", Array.Empty<GUILayoutOption>()))
			{
				SocialManager.debugGUI = true;
				SocialManager instance = SocialManager.instance;
				this.started = true;
			}
			GUILayout.EndArea();
		}
	}

	// Token: 0x040004F7 RID: 1271
	private bool started;
}
