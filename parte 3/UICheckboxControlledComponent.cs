using System;
using UnityEngine;

// Token: 0x020000FC RID: 252
[AddComponentMenu("NGUI/Interaction/Checkbox Controlled Component")]
public class UICheckboxControlledComponent : MonoBehaviour
{
	// Token: 0x06000730 RID: 1840 RVA: 0x00023E1E File Offset: 0x0002201E
	private void OnActivate(bool isActive)
	{
		if (base.enabled && this.target != null)
		{
			this.target.enabled = ((!this.inverse) ? isActive : (!isActive));
		}
	}

	// Token: 0x0400063C RID: 1596
	public MonoBehaviour target;

	// Token: 0x0400063D RID: 1597
	public bool inverse;
}
