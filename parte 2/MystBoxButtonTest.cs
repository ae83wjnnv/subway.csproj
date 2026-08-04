using System;

// Token: 0x02000097 RID: 151
public class MystBoxButtonTest : UIBasicButton
{
	// Token: 0x06000475 RID: 1141 RVA: 0x000154A4 File Offset: 0x000136A4
	protected override void Send()
	{
		PlayerInfo.Instance.mysteryBoxesToUnlock = 1;
		if (this.doubleBox)
		{
			PlayerInfo.Instance.mysteryBoxesToUnlock = 2;
		}
	}

	// Token: 0x040003D5 RID: 981
	public bool doubleBox;
}
