using System;

// Token: 0x02000130 RID: 304
public class UISlideInMissionSetHelper : UISlideIn
{
	// Token: 0x06000904 RID: 2308 RVA: 0x0003077E File Offset: 0x0002E97E
	public void SetupSlideInMissionSet(int multiplier)
	{
		base.gameObject.SetActiveRecursively(true);
		this.line2.text = "Points x" + multiplier.ToString();
		this.SlideIn();
	}

	// Token: 0x040007D8 RID: 2008
	public UILabel line2;
}
