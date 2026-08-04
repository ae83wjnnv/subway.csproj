using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class BragButtonHelper : MonoBehaviour
{
	// Token: 0x06000197 RID: 407 RVA: 0x000062D6 File Offset: 0x000044D6
	public void EnableButton()
	{
		NGUITools.AddWidgetCollider(base.gameObject);
		this.fill.spriteName = this.activeButtonFillName;
		this.buttonEnabled = true;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x000062FC File Offset: 0x000044FC
	public void DisableButton()
	{
		if (base.gameObject.GetComponent<Collider>() != null)
		{
			Object.Destroy(base.gameObject.GetComponent<Collider>());
		}
		this.fill.spriteName = this.inactiveButtonFillName;
		this.buttonEnabled = false;
	}

	// Token: 0x040000D3 RID: 211
	public UISlicedSprite fill;

	// Token: 0x040000D4 RID: 212
	private string activeButtonFillName = "button_fill_brag";

	// Token: 0x040000D5 RID: 213
	private string inactiveButtonFillName = "button_fill_shopItem_gray";

	// Token: 0x040000D6 RID: 214
	[HideInInspector]
	public bool buttonEnabled;
}
