using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000056 RID: 86
[AddComponentMenu("NGUI/Examples/Equip Random Item")]
public class EquipRandomItem : MonoBehaviour
{
	// Token: 0x060002D3 RID: 723 RVA: 0x0000C87C File Offset: 0x0000AA7C
	private void OnClick()
	{
		if (!(this.equipment == null))
		{
			List<InvBaseItem> items = InvDatabase.list[0].items;
			if (items.Count != 0)
			{
				int num = 12;
				int num2 = Random.Range(0, items.Count);
				InvBaseItem invBaseItem = items[num2];
				InvGameItem invGameItem = new InvGameItem(num2, invBaseItem);
				invGameItem.quality = (InvGameItem.Quality)Random.Range(0, num);
				invGameItem.itemLevel = NGUITools.RandomRange(invBaseItem.minItemLevel, invBaseItem.maxItemLevel);
				this.equipment.Equip(invGameItem);
			}
		}
	}

	// Token: 0x04000214 RID: 532
	public InvEquipment equipment;
}
