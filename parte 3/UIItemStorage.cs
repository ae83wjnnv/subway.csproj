using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000119 RID: 281
[AddComponentMenu("NGUI/Examples/UI Item Storage")]
public class UIItemStorage : MonoBehaviour
{
	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x06000816 RID: 2070 RVA: 0x0002A02C File Offset: 0x0002822C
	public List<InvGameItem> items
	{
		get
		{
			while (this.mItems.Count < this.maxItemCount)
			{
				this.mItems.Add(null);
			}
			return this.mItems;
		}
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x0002A055 File Offset: 0x00028255
	public InvGameItem GetItem(int slot)
	{
		if (slot < this.items.Count)
		{
			return this.mItems[slot];
		}
		return null;
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x0002A073 File Offset: 0x00028273
	public InvGameItem Replace(int slot, InvGameItem item)
	{
		if (slot < this.maxItemCount)
		{
			InvGameItem invGameItem = this.items[slot];
			this.mItems[slot] = item;
			return invGameItem;
		}
		return item;
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x0002A09C File Offset: 0x0002829C
	private void Start()
	{
		if (!(this.template != null))
		{
			return;
		}
		int num = 0;
		Bounds bounds = default(Bounds);
		for (int i = 0; i < this.maxRows; i++)
		{
			for (int j = 0; j < this.maxColumns; j++)
			{
				GameObject gameObject = NGUITools.AddChild(base.gameObject, this.template);
				gameObject.transform.localPosition = new Vector3((float)this.padding + ((float)j + 0.5f) * (float)this.spacing, (float)(-(float)this.padding) - ((float)i + 0.5f) * (float)this.spacing, 0f);
				UIStorageSlot component = gameObject.GetComponent<UIStorageSlot>();
				if (component != null)
				{
					component.storage = this;
					component.slot = num;
				}
				bounds.Encapsulate(new Vector3((float)this.padding * 2f + (float)((j + 1) * this.spacing), (float)(-(float)this.padding) * 2f - (float)((i + 1) * this.spacing), 0f));
				if (++num >= this.maxItemCount)
				{
					if (this.background != null)
					{
						this.background.transform.localScale = bounds.size;
					}
					return;
				}
			}
		}
		if (this.background != null)
		{
			this.background.transform.localScale = bounds.size;
		}
	}

	// Token: 0x04000710 RID: 1808
	public int maxItemCount = 8;

	// Token: 0x04000711 RID: 1809
	public int maxRows = 4;

	// Token: 0x04000712 RID: 1810
	public int maxColumns = 4;

	// Token: 0x04000713 RID: 1811
	public GameObject template;

	// Token: 0x04000714 RID: 1812
	public UIWidget background;

	// Token: 0x04000715 RID: 1813
	public int spacing = 128;

	// Token: 0x04000716 RID: 1814
	public int padding = 10;

	// Token: 0x04000717 RID: 1815
	private List<InvGameItem> mItems = new List<InvGameItem>();
}
