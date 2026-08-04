using System;
using UnityEngine;

// Token: 0x020000A4 RID: 164
public class ParticleFollow : MonoBehaviour
{
	// Token: 0x060004E1 RID: 1249 RVA: 0x0001793C File Offset: 0x00015B3C
	private void Awake()
	{
		this.baseRotation = base.transform.localEulerAngles;
		this.baseTargetRotation = this.Target.localEulerAngles;
		this.baseScale = base.transform.localScale;
		base.gameObject.SetActiveRecursively(false);
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x00017988 File Offset: 0x00015B88
	private void LateUpdate()
	{
		Vector3 position = base.transform.position;
		Vector3 position2 = this.Target.position;
		Vector3 position3 = this.Target.position;
		position3.x = base.transform.position.x;
		float num = Mathf.SmoothDamp(position3.x, this.Target.position.x, ref this.tweenVelocity, this.TweenTime);
		if (!float.IsNaN(num))
		{
			position3.x = num;
		}
		base.transform.position = position3;
		Vector3 vector = this.baseRotation;
		Vector3 localEulerAngles = this.Target.localEulerAngles;
		Vector3 vector2 = this.baseRotation;
		vector2.y = Mathf.SmoothDampAngle(vector2.y, this.Target.localEulerAngles.y, ref this.tweenRotVelocity, this.RotationTweenTime);
		float num2 = Mathf.Sin(this.SineOffset + Time.time * this.SineSpeed) * 5.5f;
		vector2.y += num2;
		base.transform.localEulerAngles = vector2;
	}

	// Token: 0x04000416 RID: 1046
	public Transform Target;

	// Token: 0x04000417 RID: 1047
	public float TweenTime;

	// Token: 0x04000418 RID: 1048
	private float tweenVelocity;

	// Token: 0x04000419 RID: 1049
	private Vector3 baseRotation;

	// Token: 0x0400041A RID: 1050
	private Vector3 baseTargetRotation;

	// Token: 0x0400041B RID: 1051
	private Vector3 baseScale;

	// Token: 0x0400041C RID: 1052
	private float tweenRotVelocity;

	// Token: 0x0400041D RID: 1053
	public float SineOffset;

	// Token: 0x0400041E RID: 1054
	public float SineSpeed;

	// Token: 0x0400041F RID: 1055
	public float RotationTweenTime;
}
