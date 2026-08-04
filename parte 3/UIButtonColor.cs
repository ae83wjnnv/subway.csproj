using System;
using UnityEngine;

// Token: 0x020000ED RID: 237
[AddComponentMenu("NGUI/Interaction/Button Color")]
public class UIButtonColor : MonoBehaviour
{
	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00021431 File Offset: 0x0001F631
	// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00021439 File Offset: 0x0001F639
	public Color defaultColor
	{
		get
		{
			return this.mColor;
		}
		set
		{
			this.mColor = value;
		}
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x00021442 File Offset: 0x0001F642
	protected virtual void Start()
	{
		this.mStarted = true;
		if (!this.mInitDone)
		{
			this.Init();
		}
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x00021459 File Offset: 0x0001F659
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x0002147C File Offset: 0x0001F67C
	private void OnDisable()
	{
		if (this.tweenTarget != null)
		{
			TweenColor component = this.tweenTarget.GetComponent<TweenColor>();
			if (component != null)
			{
				component.color = this.mColor;
				component.enabled = false;
			}
		}
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x000214C0 File Offset: 0x0001F6C0
	private void Init()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
		UIWidget component = this.tweenTarget.GetComponent<UIWidget>();
		if (component != null)
		{
			this.mColor = component.color;
			return;
		}
		Renderer component2 = this.tweenTarget.GetComponent<Renderer>();
		if (component2 != null)
		{
			this.mColor = component2.material.color;
			return;
		}
		Light component3 = this.tweenTarget.GetComponent<Light>();
		if (component3 != null)
		{
			this.mColor = component3.color;
			return;
		}
		Debug.LogWarning(NGUITools.GetHierarchy(base.gameObject) + " has nothing for UIButtonColor to color", this);
		base.enabled = false;
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x0002157B File Offset: 0x0001F77B
	private void OnPress(bool isPressed)
	{
		if (!this.mInitDone)
		{
			this.Init();
		}
		if (base.enabled)
		{
			TweenColor.Begin(this.tweenTarget, this.duration, (!isPressed) ? this.mColor : this.pressed);
		}
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x000215B8 File Offset: 0x0001F7B8
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenColor.Begin(this.tweenTarget, this.duration, (!isOver) ? this.mColor : this.hover);
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x040005B5 RID: 1461
	public GameObject tweenTarget;

	// Token: 0x040005B6 RID: 1462
	public Color hover = new Color(0.6f, 1f, 0.2f, 1f);

	// Token: 0x040005B7 RID: 1463
	public Color pressed = Color.grey;

	// Token: 0x040005B8 RID: 1464
	public float duration = 0.2f;

	// Token: 0x040005B9 RID: 1465
	protected Color mColor;

	// Token: 0x040005BA RID: 1466
	protected bool mInitDone;

	// Token: 0x040005BB RID: 1467
	protected bool mStarted;

	// Token: 0x040005BC RID: 1468
	protected bool mHighlighted;
}
