using System;
using UnityEngine;

// Token: 0x020000F6 RID: 246
[AddComponentMenu("NGUI/Interaction/Button Sound")]
public class UIButtonSound : MonoBehaviour
{
	// Token: 0x060006ED RID: 1773 RVA: 0x0002210A File Offset: 0x0002030A
	private void OnHover(bool isOver)
	{
		if (base.enabled && ((isOver && this.trigger == UIButtonSound.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonSound.Trigger.OnMouseOut)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x00022144 File Offset: 0x00020344
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonSound.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonSound.Trigger.OnRelease)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0002217E File Offset: 0x0002037E
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonSound.Trigger.OnClick)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x040005EF RID: 1519
	public AudioClip audioClip;

	// Token: 0x040005F0 RID: 1520
	public UIButtonSound.Trigger trigger;

	// Token: 0x040005F1 RID: 1521
	public float volume = 1f;

	// Token: 0x040005F2 RID: 1522
	public float pitch = 1f;

	// Token: 0x020001FB RID: 507
	public enum Trigger
	{
		// Token: 0x04000BBF RID: 3007
		OnClick,
		// Token: 0x04000BC0 RID: 3008
		OnMouseOver,
		// Token: 0x04000BC1 RID: 3009
		OnMouseOut,
		// Token: 0x04000BC2 RID: 3010
		OnPress,
		// Token: 0x04000BC3 RID: 3011
		OnRelease
	}
}
