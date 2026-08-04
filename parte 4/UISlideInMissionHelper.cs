using System;

// Token: 0x0200012F RID: 303
public class UISlideInMissionHelper : UISlideIn
{
	// Token: 0x06000902 RID: 2306 RVA: 0x00030756 File Offset: 0x0002E956
	public void SetupSlideInMission(string message)
	{
		base.gameObject.SetActiveRecursively(true);
		this.line1.text = message;
		this.SlideIn();
	}

	// Token: 0x040007D7 RID: 2007
	public UILabel line1;
}
