using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class CurvePoint : MonoBehaviour
{
	// Token: 0x0600027E RID: 638 RVA: 0x0000B240 File Offset: 0x00009440
	public static Curve CreateCurve(Transform curvePointsParent, Vector3 offset)
	{
		Curve curve = new Curve();
		CurvePoint[] componentsInChildren = curvePointsParent.GetComponentsInChildren<CurvePoint>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			CurvePoint curvePoint = componentsInChildren[i];
			curve.AddKey(curvePoint.t, curvePoint.transform.localPosition + offset, (curvePoint.transform.TransformPoint(-curvePoint.customIn.transform.localPosition) - curvePoint.transform.position) * curvePoint.weight, (curvePoint.transform.TransformPoint(curvePoint.customOut.transform.localPosition) - curvePoint.transform.position) * curvePoint.weight);
			if (curvePoint.smoothTangents)
			{
				curve.SmoothTangents(i, curvePoint.weight);
			}
		}
		return curve;
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0000B316 File Offset: 0x00009516
	public static void DrawCurve(Transform curvePointsParent, Color color)
	{
		CurvePoint.DrawCurve(curvePointsParent, Vector3.zero, color);
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0000B324 File Offset: 0x00009524
	public static void DrawCurve(Transform curvePointsParent, Vector3 offset, Color color)
	{
		CurvePoint.CreateCurve(curvePointsParent, offset).DrawGizmos(color);
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0000B334 File Offset: 0x00009534
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(base.transform.position, 0.5f);
		if (!this.smoothTangents)
		{
			Vector3 position = this.customIn.transform.position;
			Vector3 position2 = this.customOut.transform.position;
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(base.transform.position, position);
			Gizmos.DrawLine(base.transform.position, position2);
			Gizmos.color = Color.yellow * 0.8f;
			Gizmos.DrawSphere(position, 0.3f);
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(position2, 0.3f);
		}
	}

	// Token: 0x040001B9 RID: 441
	public float t;

	// Token: 0x040001BA RID: 442
	public bool smoothTangents = true;

	// Token: 0x040001BB RID: 443
	public CurvePointTangent customIn;

	// Token: 0x040001BC RID: 444
	public CurvePointTangent customOut;

	// Token: 0x040001BD RID: 445
	public float weight = 50f;
}
