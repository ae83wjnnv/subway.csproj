using System;
using UnityEngine;

// Token: 0x02000108 RID: 264
[AddComponentMenu("NGUI/Examples/UI Equipment Slot")]
public class UIEquipmentSlot : UIItemSlot
{
	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06000785 RID: 1925 RVA: 0x0002638C File Offset: 0x0002458C
	protected override InvGameItem observedItem
	{
		get
		{
			if (this.equipment != null)
			{
				return this.equipment.GetItem(this.slot);
			}
			return null;
		}
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x000263AF File Offset: 0x000245AF
	protected override InvGameItem Replace(InvGameItem item)
	{
		if (this.equipment != null)
		{
			return this.equipment.Replace(this.slot, item);
		}
		return item;
	}

	// Token: 0x0400068C RID: 1676
	public InvEquipment equipment;

	// Token: 0x0400068D RID: 1677
	public InvBaseItem.Slot slot;
}
