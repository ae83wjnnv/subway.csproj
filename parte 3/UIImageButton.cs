using System;
using UnityEngine;

// Token: 0x02000114 RID: 276
[AddComponentMenu("NGUI/UI/Image Button")]
[ExecuteInEditMode]
public class UIImageButton : MonoBehaviour
{
	// Token: 0x060007EB RID: 2027 RVA: 0x000291A7 File Offset: 0x000273A7
	private void Start()
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<UISprite>();
		}
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x000291C3 File Offset: 0x000273C3
	private void OnHover(bool isOver)
	{
		if (this.target != null)
		{
			this.target.spriteName = ((!isOver) ? this.normalSprite : this.hoverSprite);
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x000291FA File Offset: 0x000273FA
	private void OnPress(bool pressed)
	{
		if (this.target != null)
		{
			this.target.spriteName = ((!pressed) ? this.normalSprite : this.pressedSprite);
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x040006E7 RID: 1767
	public UISprite target;

	// Token: 0x040006E8 RID: 1768
	public string normalSprite;

	// Token: 0x040006E9 RID: 1769
	public string hoverSprite;

	// Token: 0x040006EA RID: 1770
	public string pressedSprite;
}
