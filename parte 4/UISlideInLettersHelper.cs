using System;

// Token: 0x0200012E RID: 302
public class UISlideInLettersHelper : UISlideIn
{
	// Token: 0x06000900 RID: 2304 RVA: 0x0003073A File Offset: 0x0002E93A
	public void SetupLetters()
	{
		base.gameObject.SetActiveRecursively(true);
		this.SlideIn();
	}
}
