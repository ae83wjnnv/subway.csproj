using System;
using UnityEngine;

// Token: 0x020000F4 RID: 244
[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	// Token: 0x060006DF RID: 1759 RVA: 0x00021DBE File Offset: 0x0001FFBE
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x00021DC7 File Offset: 0x0001FFC7
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x00021DEC File Offset: 0x0001FFEC
	private void OnDisable()
	{
		if (this.tweenTarget != null)
		{
			TweenRotation component = this.tweenTarget.GetComponent<TweenRotation>();
			if (component != null)
			{
				component.rotation = this.mRot;
				component.enabled = false;
			}
		}
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x00021E2F File Offset: 0x0002002F
	private void Init()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.transform;
		}
		this.mRot = this.tweenTarget.localRotation;
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x00021E64 File Offset: 0x00020064
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? this.mRot : (this.mRot * Quaternion.Euler(this.pressed))).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00021EC4 File Offset: 0x000200C4
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))).method = UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x040005DF RID: 1503
	public Transform tweenTarget;

	// Token: 0x040005E0 RID: 1504
	public Vector3 hover = Vector3.zero;

	// Token: 0x040005E1 RID: 1505
	public Vector3 pressed = Vector3.zero;

	// Token: 0x040005E2 RID: 1506
	public float duration = 0.2f;

	// Token: 0x040005E3 RID: 1507
	private Quaternion mRot;

	// Token: 0x040005E4 RID: 1508
	private bool mInitDone;

	// Token: 0x040005E5 RID: 1509
	private bool mStarted;

	// Token: 0x040005E6 RID: 1510
	private bool mHighlighted;
}
