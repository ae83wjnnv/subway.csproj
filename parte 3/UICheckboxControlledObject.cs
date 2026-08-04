using System;
using UnityEngine;

// Token: 0x020000FD RID: 253
[AddComponentMenu("NGUI/Interaction/Checkbox Controlled Object")]
public class UICheckboxControlledObject : MonoBehaviour
{
	// Token: 0x06000732 RID: 1842 RVA: 0x00023E58 File Offset: 0x00022058
	private void OnActivate(bool isActive)
	{
		if (this.target != null)
		{
			NGUITools.SetActive(this.target, (!this.inverse) ? isActive : (!isActive));
		}
	}

	// Token: 0x0400063E RID: 1598
	public GameObject target;

	// Token: 0x0400063F RID: 1599
	public bool inverse;
}
