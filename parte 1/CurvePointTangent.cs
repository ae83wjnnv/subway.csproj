using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class CurvePointTangent : MonoBehaviour
{
	// Token: 0x06000283 RID: 643 RVA: 0x0000B407 File Offset: 0x00009607
	public void OnDrawGizmosSelected()
	{
		if (base.transform.parent != null)
		{
			base.transform.parent.GetComponent<CurvePoint>().OnDrawGizmosSelected();
		}
	}
}
