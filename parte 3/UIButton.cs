using System;
using UnityEngine;

// Token: 0x020000EB RID: 235
[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : UIButtonColor
{
	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00021204 File Offset: 0x0001F404
	// (set) Token: 0x060006B1 RID: 1713 RVA: 0x00021228 File Offset: 0x0001F428
	public bool isEnabled
	{
		get
		{
			Collider component = base.GetComponent<Collider>();
			return component && component.enabled;
		}
		set
		{
			Collider component = base.GetComponent<Collider>();
			if (component && component.enabled != value)
			{
				component.enabled = value;
				this.UpdateColor(value, false);
			}
		}
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0002125C File Offset: 0x0001F45C
	protected override void Start()
	{
		base.Start();
		if (!this.isEnabled)
		{
			this.UpdateColor(false, true);
		}
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x00021274 File Offset: 0x0001F474
	private void UpdateColor(bool shouldBeEnabled, bool immediate)
	{
		if (this.tweenTarget)
		{
			Color defaultColor = base.defaultColor;
			if (!shouldBeEnabled)
			{
				defaultColor.r *= 0.65f;
				defaultColor.g *= 0.65f;
				defaultColor.b *= 0.65f;
			}
			TweenColor tweenColor = TweenColor.Begin(this.tweenTarget, 0.15f, defaultColor);
			if (immediate)
			{
				tweenColor.color = defaultColor;
				tweenColor.enabled = false;
			}
		}
	}
}
