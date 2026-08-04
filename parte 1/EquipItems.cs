using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
[AddComponentMenu("NGUI/Examples/Equip Items")]
public class EquipItems : MonoBehaviour
{
	// Token: 0x060002D1 RID: 721 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
	private void Start()
	{
		if (this.itemIDs != null && this.itemIDs.Length != 0)
		{
			InvEquipment invEquipment = base.GetComponent<InvEquipment>();
			if (invEquipment == null)
			{
				invEquipment = base.gameObject.AddComponent<InvEquipment>();
			}
			int num = 12;
			int i = 0;
			int num2 = this.itemIDs.Length;
			while (i < num2)
			{
				int num3 = this.itemIDs[i];
				InvBaseItem invBaseItem = InvDatabase.FindByID(num3);
				if (invBaseItem != null)
				{
					invEquipment.Equip(new InvGameItem(num3, invBaseItem)
					{
						quality = (InvGameItem.Quality)Random.Range(0, num),
						itemLevel = NGUITools.RandomRange(invBaseItem.minItemLevel, invBaseItem.maxItemLevel)
					});
				}
				else
				{
					Debug.LogWarning("Can't resolve the item ID of " + num3.ToString());
				}
				i++;
			}
		}
		Object.Destroy(this);
	}

	// Token: 0x04000213 RID: 531
	public int[] itemIDs;
}
