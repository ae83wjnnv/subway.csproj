using System;
using UnityEngine;

// Token: 0x02000107 RID: 263
public class UIDynamicContent : MonoBehaviour
{
	// Token: 0x06000782 RID: 1922 RVA: 0x0002631C File Offset: 0x0002451C
	private void Start()
	{
		this.InitElements();
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00026324 File Offset: 0x00024524
	public void InitElements()
	{
		for (int i = 0; i < this.PanelElements.Length; i++)
		{
			NGUITools.AddChild(base.gameObject, this.PanelElements[i]);
		}
		for (int j = 0; j < this.HeaderElements.Length; j++)
		{
			NGUITools.AddWidgetCollider(NGUITools.AddChild(this.Header, this.HeaderElements[j]));
		}
	}

	// Token: 0x04000689 RID: 1673
	public GameObject[] PanelElements;

	// Token: 0x0400068A RID: 1674
	public GameObject Header;

	// Token: 0x0400068B RID: 1675
	public GameObject[] HeaderElements;
}
