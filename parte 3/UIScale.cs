using System;
using UnityEngine;

// Token: 0x02000128 RID: 296
public class UIScale : MonoBehaviour
{
	// Token: 0x060008A5 RID: 2213 RVA: 0x0002E181 File Offset: 0x0002C381
	private void Start()
	{
		if (DeviceInfo.formFactor == DeviceInfo.FormFactor.iPad)
		{
			base.transform.localScale = new Vector3(this.iPadSize.x, this.iPadSize.y, base.transform.localScale.z);
		}
	}

	// Token: 0x0400079A RID: 1946
	private Vector2 iPadSize = new Vector2(390f, 520f);
}
