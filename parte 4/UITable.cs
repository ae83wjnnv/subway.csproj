using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000139 RID: 313
[AddComponentMenu("NGUI/Interaction/Table")]
[ExecuteInEditMode]
public class UITable : MonoBehaviour
{
	// Token: 0x0600093A RID: 2362 RVA: 0x00031B20 File Offset: 0x0002FD20
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x00031B34 File Offset: 0x0002FD34
	private void RepositionVariableSize(List<Transform> children)
	{
		float num = 0f;
		float num2 = 0f;
		object obj = ((this.columns <= 0) ? 1 : (children.Count / this.columns + 1));
		int num3 = ((this.columns <= 0) ? children.Count : this.columns);
		object obj2 = obj;
		Bounds[,] array = new Bounds[obj2, num3];
		Bounds[] array2 = new Bounds[num3];
		Bounds[] array3 = new Bounds[obj2];
		int num4 = 0;
		int num5 = 0;
		int i = 0;
		int count = children.Count;
		while (i < count)
		{
			Transform transform = children[i];
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform);
			Vector3 localScale = transform.localScale;
			bounds.min = Vector3.Scale(bounds.min, localScale);
			bounds.max = Vector3.Scale(bounds.max, localScale);
			array[num5, num4] = bounds;
			array2[num4].Encapsulate(bounds);
			array3[num5].Encapsulate(bounds);
			if (++num4 >= this.columns && this.columns > 0)
			{
				num4 = 0;
				num5++;
			}
			i++;
		}
		num4 = 0;
		num5 = 0;
		int j = 0;
		int count2 = children.Count;
		while (j < count2)
		{
			Transform transform2 = children[j];
			Bounds bounds2 = array[num5, num4];
			Bounds bounds3 = array2[num4];
			Bounds bounds4 = array3[num5];
			Vector3 localPosition = transform2.localPosition;
			localPosition.x = num + bounds2.extents.x - bounds2.center.x;
			localPosition.y = 0f - (num2 + bounds2.extents.y + bounds2.center.y);
			localPosition.x += bounds2.min.x - bounds3.min.x + this.padding.x;
			localPosition.y += (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - this.padding.y;
			num += bounds3.max.x - bounds3.min.x + this.padding.x * 2f;
			transform2.localPosition = localPosition;
			if (++num4 >= this.columns && this.columns > 0)
			{
				num4 = 0;
				num5++;
				num = 0f;
				num2 += bounds4.size.y + this.padding.y * 2f;
			}
			j++;
		}
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x00031DE8 File Offset: 0x0002FFE8
	public void Reposition()
	{
		Transform transform = base.transform;
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (!this.hideInactive || child.gameObject.active)
			{
				list.Add(child);
			}
		}
		if (this.sorted)
		{
			list.Sort(new Comparison<Transform>(UITable.SortByName));
		}
		if (list.Count > 0)
		{
			this.RepositionVariableSize(list);
		}
		if (this.mPanel != null && this.mDrag == null)
		{
			this.mPanel.ConstrainTargetToBounds(transform, true);
		}
		if (this.mDrag != null)
		{
			this.mDrag.UpdateScrollbars(true);
		}
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x00031EA5 File Offset: 0x000300A5
	private void Start()
	{
		if (this.keepWithinPanel)
		{
			this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
			this.mDrag = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		}
		this.Reposition();
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00031ED7 File Offset: 0x000300D7
	private void LateUpdate()
	{
		if (this.repositionNow)
		{
			this.repositionNow = false;
			this.Reposition();
		}
	}

	// Token: 0x04000804 RID: 2052
	public int columns;

	// Token: 0x04000805 RID: 2053
	public Vector2 padding = Vector2.zero;

	// Token: 0x04000806 RID: 2054
	public bool sorted;

	// Token: 0x04000807 RID: 2055
	public bool hideInactive = true;

	// Token: 0x04000808 RID: 2056
	public bool repositionNow;

	// Token: 0x04000809 RID: 2057
	public bool keepWithinPanel;

	// Token: 0x0400080A RID: 2058
	private UIPanel mPanel;

	// Token: 0x0400080B RID: 2059
	private UIDraggablePanel mDrag;
}
