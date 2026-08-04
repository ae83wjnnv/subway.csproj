using System;
using UnityEngine;

// Token: 0x020000EA RID: 234
public class UIBasicButton : MonoBehaviour
{
	// Token: 0x060006AB RID: 1707 RVA: 0x000211A8 File Offset: 0x0001F3A8
	protected virtual void OnHover(bool isOver)
	{
		if ((isOver && this.trigger == UIBasicButton.Trigger.OnMouseOver) || (!isOver && this.trigger == UIBasicButton.Trigger.OnMouseOut))
		{
			this.Send();
		}
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x000211C8 File Offset: 0x0001F3C8
	protected virtual void OnPress(bool isPressed)
	{
		if ((isPressed && this.trigger == UIBasicButton.Trigger.OnPress) || (!isPressed && this.trigger == UIBasicButton.Trigger.OnRelease))
		{
			this.Send();
		}
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x000211E8 File Offset: 0x0001F3E8
	protected virtual void OnClick()
	{
		if (this.trigger == UIBasicButton.Trigger.OnClick)
		{
			this.Send();
		}
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x000211F8 File Offset: 0x0001F3F8
	protected virtual void Send()
	{
	}

	// Token: 0x040005B0 RID: 1456
	public UIBasicButton.Trigger trigger;

	// Token: 0x020001F7 RID: 503
	public enum Trigger
	{
		// Token: 0x04000BA8 RID: 2984
		OnClick,
		// Token: 0x04000BA9 RID: 2985
		OnMouseOver,
		// Token: 0x04000BAA RID: 2986
		OnMouseOut,
		// Token: 0x04000BAB RID: 2987
		OnPress,
		// Token: 0x04000BAC RID: 2988
		OnRelease
	}
}
