using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class CurveParent : MonoBehaviour
{
	// Token: 0x0600027C RID: 636 RVA: 0x0000B217 File Offset: 0x00009417
	public void OnDrawGizmos()
	{
		CurvePoint.DrawCurve(base.transform, this.color);
	}

	// Token: 0x040001B8 RID: 440
	public Color color = Color.red;
}
