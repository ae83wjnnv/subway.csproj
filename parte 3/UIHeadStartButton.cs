using System;
using UnityEngine;

// Token: 0x02000112 RID: 274
public class UIHeadStartButton : UIBasicButton
{
	// Token: 0x060007E1 RID: 2017 RVA: 0x00028E6C File Offset: 0x0002706C
	protected override void Send()
	{
		if (this.type == UIHeadStartButton.HeadStartType.headstart500)
		{
			Debug.Log("Use a headstart500");
			Game.Instance.StartHeadStart500();
		}
		else if (this.type == UIHeadStartButton.HeadStartType.headstart2000)
		{
			Debug.Log("Use a headstart2000");
			Game.Instance.StartHeadStart2000();
		}
		this.helper.HideHeadStart();
	}

	// Token: 0x040006DA RID: 1754
	public UIHeadStartHelper helper;

	// Token: 0x040006DB RID: 1755
	public UIHeadStartButton.HeadStartType type;

	// Token: 0x0200020D RID: 525
	public enum HeadStartType
	{
		// Token: 0x04000BF6 RID: 3062
		_notSet,
		// Token: 0x04000BF7 RID: 3063
		headstart500,
		// Token: 0x04000BF8 RID: 3064
		headstart2000
	}
}
