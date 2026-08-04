using System;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class FriendProgressHelper : MonoBehaviour
{
	// Token: 0x06000341 RID: 833 RVA: 0x0000F268 File Offset: 0x0000D468
	public Vector3 GetCoinPouchGlobalPosition()
	{
		return this.coinPouch.transform.position;
	}

	// Token: 0x04000294 RID: 660
	public UILabel label;

	// Token: 0x04000295 RID: 661
	public UISlider slider;

	// Token: 0x04000296 RID: 662
	public UISprite coinPouch;
}
