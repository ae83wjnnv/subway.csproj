using System;
using UnityEngine;

// Token: 0x020000F2 RID: 242
[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
	// Token: 0x060006D1 RID: 1745 RVA: 0x00021AB9 File Offset: 0x0001FCB9
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x00021AC2 File Offset: 0x0001FCC2
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x00021AE8 File Offset: 0x0001FCE8
	private void OnDisable()
	{
		if (this.tweenTarget != null)
		{
			TweenPosition component = this.tweenTarget.GetComponent<TweenPosition>();
			if (component != null)
			{
				component.position = this.mPos;
				component.enabled = false;
			}
		}
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x00021B2B File Offset: 0x0001FD2B
	private void Init()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.transform;
		}
		this.mPos = this.tweenTarget.localPosition;
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00021B60 File Offset: 0x0001FD60
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? this.mPos : (this.mPos + this.pressed)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00021BBC File Offset: 0x0001FDBC
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mPos : (this.mPos + this.hover)).method = UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x040005CC RID: 1484
	public Transform tweenTarget;

	// Token: 0x040005CD RID: 1485
	public Vector3 hover = Vector3.zero;

	// Token: 0x040005CE RID: 1486
	public Vector3 pressed = new Vector3(2f, -2f);

	// Token: 0x040005CF RID: 1487
	public float duration = 0.2f;

	// Token: 0x040005D0 RID: 1488
	private Vector3 mPos;

	// Token: 0x040005D1 RID: 1489
	private bool mInitDone;

	// Token: 0x040005D2 RID: 1490
	private bool mStarted;

	// Token: 0x040005D3 RID: 1491
	private bool mHighlighted;
}
