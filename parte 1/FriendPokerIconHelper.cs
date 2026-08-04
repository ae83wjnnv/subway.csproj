using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class FriendPokerIconHelper : MonoBehaviour
{
	// Token: 0x0600033F RID: 831 RVA: 0x0000F246 File Offset: 0x0000D446
	private void Start()
	{
		if (DeviceInfo.isHighres)
		{
			base.GetComponent<UISprite>().spriteName = this.highResName;
		}
	}

	// Token: 0x04000293 RID: 659
	public string highResName;
}
