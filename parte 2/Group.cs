using System;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class Group : MonoBehaviour
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060003B5 RID: 949 RVA: 0x00010DE8 File Offset: 0x0000EFE8
	// (set) Token: 0x060003B6 RID: 950 RVA: 0x00010DF0 File Offset: 0x0000EFF0
	public bool GroupActive
	{
		get
		{
			return this.groupActive;
		}
		set
		{
			this.groupActive = value;
			this.UpdateChildren();
		}
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00010DFF File Offset: 0x0000EFFF
	public void Start()
	{
		this.UpdateChildren();
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00010E07 File Offset: 0x0000F007
	public void OnEnable()
	{
		this.UpdateChildren();
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00010E0F File Offset: 0x0000F00F
	public void OnDisable()
	{
		this.UpdateChildren();
	}

	// Token: 0x060003BA RID: 954 RVA: 0x00010E17 File Offset: 0x0000F017
	public void UpdateChildren()
	{
		this.SetChildrenActive(this.groupActive && base.gameObject.active);
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00010E38 File Offset: 0x0000F038
	private void SetChildrenActive(bool active)
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).gameObject.SetActiveRecursively(active);
		}
	}

	// Token: 0x04000308 RID: 776
	private bool groupActive;
}
