using System;
using UnityEngine;

// Token: 0x020000C1 RID: 193
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Examples/Shader Quality")]
public class ShaderQuality : MonoBehaviour
{
	// Token: 0x06000582 RID: 1410 RVA: 0x0001BB78 File Offset: 0x00019D78
	private void Update()
	{
		int num = (QualitySettings.GetQualityLevel() + 1) * 100;
		if (this.mCurrent != num)
		{
			this.mCurrent = num;
			Shader.globalMaximumLOD = this.mCurrent;
		}
	}

	// Token: 0x040004C3 RID: 1219
	private int mCurrent = 600;
}
