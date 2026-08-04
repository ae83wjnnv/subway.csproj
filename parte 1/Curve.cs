using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
public class Curve
{
	// Token: 0x06000274 RID: 628 RVA: 0x0000AEF4 File Offset: 0x000090F4
	public Curve()
	{
		this.curveX.postWrapMode = WrapMode.ClampForever;
		this.curveX.preWrapMode = WrapMode.ClampForever;
		this.curveY.postWrapMode = WrapMode.ClampForever;
		this.curveY.preWrapMode = WrapMode.ClampForever;
		this.curveZ.postWrapMode = WrapMode.ClampForever;
		this.curveZ.preWrapMode = WrapMode.ClampForever;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0000AF88 File Offset: 0x00009188
	public void AddKey(float t, Vector3 value)
	{
		this.curveX.AddKey(t, value.x);
		this.curveY.AddKey(t, value.y);
		this.curveZ.AddKey(t, value.z);
		if (t < this.min)
		{
			this.min = t;
		}
		if (t > this.max)
		{
			this.max = t;
		}
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0000AFF0 File Offset: 0x000091F0
	public void AddKey(float t, Vector3 value, Vector3 inTangent, Vector3 outTangent)
	{
		this.curveX.AddKey(new Keyframe(t, value.x, inTangent.x, outTangent.x));
		this.curveY.AddKey(new Keyframe(t, value.y, inTangent.y, outTangent.y));
		this.curveZ.AddKey(new Keyframe(t, value.z, inTangent.z, outTangent.z));
		if (t < this.min)
		{
			this.min = t;
		}
		if (t > this.max)
		{
			this.max = t;
		}
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0000B08C File Offset: 0x0000928C
	public void MoveKey(int index, float t, Vector3 value, Vector3 inTangent, Vector3 outTangent)
	{
		this.curveX.MoveKey(index, new Keyframe(t, value.x, inTangent.x, outTangent.x));
		this.curveY.MoveKey(index, new Keyframe(t, value.y, inTangent.y, outTangent.y));
		this.curveZ.MoveKey(index, new Keyframe(t, value.z, inTangent.z, outTangent.z));
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0000B110 File Offset: 0x00009310
	public void MoveKey(int index, float t, Vector3 value)
	{
		this.curveX.MoveKey(index, new Keyframe(t, value.x));
		this.curveY.MoveKey(index, new Keyframe(t, value.y));
		this.curveZ.MoveKey(index, new Keyframe(t, value.z));
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0000B168 File Offset: 0x00009368
	public void SmoothTangents(int index, float weight)
	{
		this.curveX.SmoothTangents(index, weight);
		this.curveY.SmoothTangents(index, weight);
		this.curveZ.SmoothTangents(index, weight);
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0000B191 File Offset: 0x00009391
	public Vector3 Evaluate(float t)
	{
		return new Vector3(this.curveX.Evaluate(t), this.curveY.Evaluate(t), this.curveZ.Evaluate(t));
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000B1BC File Offset: 0x000093BC
	public void DrawGizmos(Color color)
	{
		Gizmos.color = color;
		int num = 1000;
		Vector3 vector = this.Evaluate(0f);
		for (int i = 0; i < num; i++)
		{
			float num2 = (this.max - this.min) * (float)i / (float)(num - 1);
			Vector3 vector2 = this.Evaluate(num2);
			Gizmos.DrawLine(vector, vector2);
			vector = vector2;
		}
	}

	// Token: 0x040001B3 RID: 435
	public AnimationCurve curveX = new AnimationCurve();

	// Token: 0x040001B4 RID: 436
	public AnimationCurve curveY = new AnimationCurve();

	// Token: 0x040001B5 RID: 437
	public AnimationCurve curveZ = new AnimationCurve();

	// Token: 0x040001B6 RID: 438
	private float min = float.PositiveInfinity;

	// Token: 0x040001B7 RID: 439
	private float max = float.NegativeInfinity;
}
