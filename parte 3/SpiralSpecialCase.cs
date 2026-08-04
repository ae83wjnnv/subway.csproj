using System;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class SpiralSpecialCase : MonoBehaviour
{
	// Token: 0x06000615 RID: 1557 RVA: 0x0001E95C File Offset: 0x0001CB5C
	private void Start()
	{
		if (DeviceInfo.isHighres)
		{
			base.GetComponent<UITiledSprite>().spriteName = this.highResName;
		}
	}

	// Token: 0x0400051C RID: 1308
	public string highResName;
}
