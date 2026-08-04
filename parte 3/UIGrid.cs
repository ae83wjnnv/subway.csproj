using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000111 RID: 273
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Grid")]
public class UIGrid : MonoBehaviour
{
	// Token: 0x060007DC RID: 2012 RVA: 0x00028C21 File Offset: 0x00026E21
	private void Start()
	{
		this.Reposition();
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x00028C29 File Offset: 0x00026E29
	private void Update()
	{
		if (this.repositionNow)
		{
			this.repositionNow = false;
			this.Reposition();
		}
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x00028C40 File Offset: 0x00026E40
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x00028C54 File Offset: 0x00026E54
	public void Reposition()
	{
		Transform transform = base.transform;
		int num = 0;
		int num2 = 0;
		if (this.sorted)
		{
			List<Transform> list = new List<Transform>();
			for (int i = 0; i < transform.childCount; i++)
			{
				list.Add(transform.GetChild(i));
			}
			list.Sort(new Comparison<Transform>(UIGrid.SortByName));
			int j = 0;
			int count = list.Count;
			while (j < count)
			{
				Transform transform2 = list[j];
				if (transform2.gameObject.active || !this.hideInactive)
				{
					float z = transform2.localPosition.z;
					transform2.localPosition = ((this.arrangement != UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, (0f - this.cellHeight) * (float)num, z) : new Vector3(this.cellWidth * (float)num, (0f - this.cellHeight) * (float)num2, z));
					if (++num >= this.maxPerLine && this.maxPerLine > 0)
					{
						num = 0;
						num2++;
					}
				}
				j++;
			}
		}
		else
		{
			for (int k = 0; k < transform.childCount; k++)
			{
				Transform child = transform.GetChild(k);
				if (child.gameObject.active || !this.hideInactive)
				{
					float z2 = child.localPosition.z;
					child.localPosition = ((this.arrangement != UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, (0f - this.cellHeight) * (float)num, z2) : new Vector3(this.cellWidth * (float)num, (0f - this.cellHeight) * (float)num2, z2));
					if (++num >= this.maxPerLine && this.maxPerLine > 0)
					{
						num = 0;
						num2++;
					}
				}
			}
		}
		UIDraggablePanel uidraggablePanel = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		if (uidraggablePanel != null)
		{
			uidraggablePanel.UpdateScrollbars(true);
		}
	}

	// Token: 0x040006D3 RID: 1747
	public UIGrid.Arrangement arrangement;

	// Token: 0x040006D4 RID: 1748
	public int maxPerLine;

	// Token: 0x040006D5 RID: 1749
	public float cellWidth = 200f;

	// Token: 0x040006D6 RID: 1750
	public float cellHeight = 200f;

	// Token: 0x040006D7 RID: 1751
	public bool repositionNow;

	// Token: 0x040006D8 RID: 1752
	public bool sorted;

	// Token: 0x040006D9 RID: 1753
	public bool hideInactive = true;

	// Token: 0x0200020C RID: 524
	public enum Arrangement
	{
		// Token: 0x04000BF3 RID: 3059
		Horizontal,
		// Token: 0x04000BF4 RID: 3060
		Vertical
	}
}
