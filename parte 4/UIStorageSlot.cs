using System;
using UnityEngine;

// Token: 0x02000137 RID: 311
[AddComponentMenu("NGUI/Examples/UI Storage Slot")]
public class UIStorageSlot : UIItemSlot
{
	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000934 RID: 2356 RVA: 0x00031946 File Offset: 0x0002FB46
	protected override InvGameItem observedItem
	{
		get
		{
			if (this.storage != null)
			{
				return this.storage.GetItem(this.slot);
			}
			return null;
		}
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x00031969 File Offset: 0x0002FB69
	protected override InvGameItem Replace(InvGameItem item)
	{
		if (this.storage != null)
		{
			return this.storage.Replace(this.slot, item);
		}
		return item;
	}

	// Token: 0x040007FD RID: 2045
	public UIItemStorage storage;

	// Token: 0x040007FE RID: 2046
	public int slot;
}
