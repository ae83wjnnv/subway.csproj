using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007C RID: 124
[AddComponentMenu("NGUI/Examples/Item Database")]
[ExecuteInEditMode]
public class InvDatabase : MonoBehaviour
{
	// Token: 0x17000053 RID: 83
	// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00011ACC File Offset: 0x0000FCCC
	public static InvDatabase[] list
	{
		get
		{
			if (InvDatabase.mIsDirty)
			{
				InvDatabase.mIsDirty = false;
				InvDatabase.mList = Object.FindSceneObjectsOfType(typeof(InvDatabase)) as InvDatabase[];
			}
			return InvDatabase.mList;
		}
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00011AF9 File Offset: 0x0000FCF9
	private void OnEnable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00011B01 File Offset: 0x0000FD01
	private void OnDisable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00011B0C File Offset: 0x0000FD0C
	private InvBaseItem GetItem(int id16)
	{
		int i = 0;
		int count = this.items.Count;
		while (i < count)
		{
			InvBaseItem invBaseItem = this.items[i];
			if (invBaseItem.id16 == id16)
			{
				return invBaseItem;
			}
			i++;
		}
		return null;
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00011B4C File Offset: 0x0000FD4C
	private static InvDatabase GetDatabase(int dbID)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.databaseID == dbID)
			{
				return invDatabase;
			}
			i++;
		}
		return null;
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00011B84 File Offset: 0x0000FD84
	public static InvBaseItem FindByID(int id32)
	{
		InvDatabase database = InvDatabase.GetDatabase(id32 >> 16);
		if (database != null)
		{
			return database.GetItem(id32 & 65535);
		}
		return null;
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x00011BB4 File Offset: 0x0000FDB4
	public static InvBaseItem FindByName(string exact)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			int j = 0;
			int count = invDatabase.items.Count;
			while (j < count)
			{
				InvBaseItem invBaseItem = invDatabase.items[j];
				if (invBaseItem.name == exact)
				{
					return invBaseItem;
				}
				j++;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x00011C18 File Offset: 0x0000FE18
	public static int FindItemID(InvBaseItem item)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.items.Contains(item))
			{
				return (invDatabase.databaseID << 16) | item.id16;
			}
			i++;
		}
		return -1;
	}

	// Token: 0x0400034E RID: 846
	private static InvDatabase[] mList;

	// Token: 0x0400034F RID: 847
	private static bool mIsDirty = true;

	// Token: 0x04000350 RID: 848
	public int databaseID;

	// Token: 0x04000351 RID: 849
	public List<InvBaseItem> items = new List<InvBaseItem>();

	// Token: 0x04000352 RID: 850
	public UIAtlas iconAtlas;
}
