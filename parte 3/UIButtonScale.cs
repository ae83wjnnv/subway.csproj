using System;
using UnityEngine;

// Token: 0x020000F5 RID: 245
[AddComponentMenu("NGUI/Interaction/Button Scale")]
public class UIButtonScale : MonoBehaviour
{
	// Token: 0x060006E6 RID: 1766 RVA: 0x00021F54 File Offset: 0x00020154
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00021F5D File Offset: 0x0002015D
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00021F80 File Offset: 0x00020180
	private void OnDisable()
	{
		if (this.tweenTarget != null)
		{
			TweenScale component = this.tweenTarget.GetComponent<TweenScale>();
			if (component != null)
			{
				component.scale = this.mScale;
				component.enabled = false;
			}
		}
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x00021FC3 File Offset: 0x000201C3
	private void Init()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.transform;
		}
		this.mScale = this.tweenTarget.localScale;
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00021FF8 File Offset: 0x000201F8
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? this.mScale : Vector3.Scale(this.mScale, this.pressed)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x00022054 File Offset: 0x00020254
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mScale : Vector3.Scale(this.mScale, this.hover)).method = UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x040005E7 RID: 1511
	public Transform tweenTarget;

	// Token: 0x040005E8 RID: 1512
	public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);

	// Token: 0x040005E9 RID: 1513
	public Vector3 pressed = new Vector3(1.05f, 1.05f, 1.05f);

	// Token: 0x040005EA RID: 1514
	public float duration = 0.2f;

	// Token: 0x040005EB RID: 1515
	private Vector3 mScale;

	// Token: 0x040005EC RID: 1516
	private bool mInitDone;

	// Token: 0x040005ED RID: 1517
	private bool mStarted;

	// Token: 0x040005EE RID: 1518
	private bool mHighlighted;
}
