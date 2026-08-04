using System;
using UnityEngine;

// Token: 0x0200008B RID: 139
[AddComponentMenu("NGUI/Examples/Load Level On Click")]
public class LoadLevelOnClick : MonoBehaviour
{
	// Token: 0x0600043D RID: 1085 RVA: 0x00012C49 File Offset: 0x00010E49
	private void OnClick()
	{
		if (!string.IsNullOrEmpty(this.levelName))
		{
			Application.LoadLevel(this.levelName);
		}
	}

	// Token: 0x0400039E RID: 926
	public string levelName;
}
