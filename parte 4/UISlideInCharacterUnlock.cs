using System;

// Token: 0x0200012D RID: 301
public class UISlideInCharacterUnlock : UISlideIn
{
	// Token: 0x060008FE RID: 2302 RVA: 0x0003070D File Offset: 0x0002E90D
	public void SetupSlideInCharacter(string message)
	{
		base.gameObject.SetActiveRecursively(true);
		this.CharacterName.text = message.ToUpper();
		this.SlideIn();
	}

	// Token: 0x040007D6 RID: 2006
	public UILabel CharacterName;
}
