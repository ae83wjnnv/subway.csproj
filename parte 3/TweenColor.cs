using System;
using UnityEngine;

// Token: 0x020000E2 RID: 226
[AddComponentMenu("NGUI/Tween/Color")]
public class TweenColor : UITweener
{
	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06000679 RID: 1657 RVA: 0x00020374 File Offset: 0x0001E574
	// (set) Token: 0x0600067A RID: 1658 RVA: 0x000203D4 File Offset: 0x0001E5D4
	public Color color
	{
		get
		{
			if (this.mWidget != null)
			{
				return this.mWidget.color;
			}
			if (this.mLight != null)
			{
				return this.mLight.color;
			}
			if (this.mMat != null)
			{
				return this.mMat.color;
			}
			return Color.black;
		}
		set
		{
			if (this.mWidget != null)
			{
				this.mWidget.color = value;
			}
			if (this.mMat != null)
			{
				this.mMat.color = value;
			}
			if (this.mLight != null)
			{
				this.mLight.color = value;
				this.mLight.enabled = value.r + value.g + value.b > 0.01f;
			}
		}
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x00020458 File Offset: 0x0001E658
	private void Awake()
	{
		this.mWidget = base.GetComponentInChildren<UIWidget>();
		Renderer component = base.GetComponent<Renderer>();
		if (component != null)
		{
			this.mMat = component.material;
		}
		this.mLight = base.GetComponent<Light>();
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x00020499 File Offset: 0x0001E699
	protected override void OnUpdate(float factor)
	{
		this.color = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x000204C4 File Offset: 0x0001E6C4
	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration);
		tweenColor.from = tweenColor.color;
		tweenColor.to = color;
		return tweenColor;
	}

	// Token: 0x0400058A RID: 1418
	public Color from = Color.white;

	// Token: 0x0400058B RID: 1419
	public Color to = Color.white;

	// Token: 0x0400058C RID: 1420
	private Transform mTrans;

	// Token: 0x0400058D RID: 1421
	private UIWidget mWidget;

	// Token: 0x0400058E RID: 1422
	private Material mMat;

	// Token: 0x0400058F RID: 1423
	private Light mLight;
}
