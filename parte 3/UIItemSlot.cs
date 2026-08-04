using System;
using UnityEngine;

// Token: 0x02000118 RID: 280
public abstract class UIItemSlot : MonoBehaviour
{
	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x0600080D RID: 2061
	protected abstract InvGameItem observedItem { get; }

	// Token: 0x0600080E RID: 2062
	protected abstract InvGameItem Replace(InvGameItem item);

	// Token: 0x0600080F RID: 2063 RVA: 0x00029DE3 File Offset: 0x00027FE3
	private void OnTooltip(bool show)
	{
		UITooltip.ShowItem((!show) ? null : this.mItem);
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00029DF6 File Offset: 0x00027FF6
	private void OnClick()
	{
		if (UIItemSlot.mDraggedItem != null)
		{
			this.OnDrop(null);
			return;
		}
		if (this.mItem != null)
		{
			UIItemSlot.mDraggedItem = this.Replace(null);
			if (UIItemSlot.mDraggedItem != null)
			{
				NGUITools.PlaySound(this.grabSound);
			}
			this.UpdateCursor();
		}
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00029E34 File Offset: 0x00028034
	private void OnDrag(Vector2 delta)
	{
		if (UIItemSlot.mDraggedItem == null && this.mItem != null)
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			UIItemSlot.mDraggedItem = this.Replace(null);
			NGUITools.PlaySound(this.grabSound);
			this.UpdateCursor();
		}
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00029E70 File Offset: 0x00028070
	private void OnDrop(GameObject go)
	{
		InvGameItem invGameItem = this.Replace(UIItemSlot.mDraggedItem);
		if (UIItemSlot.mDraggedItem == invGameItem)
		{
			NGUITools.PlaySound(this.errorSound);
		}
		else if (invGameItem != null)
		{
			NGUITools.PlaySound(this.grabSound);
		}
		else
		{
			NGUITools.PlaySound(this.placeSound);
		}
		UIItemSlot.mDraggedItem = invGameItem;
		this.UpdateCursor();
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00029EC8 File Offset: 0x000280C8
	private void UpdateCursor()
	{
		if (UIItemSlot.mDraggedItem != null && UIItemSlot.mDraggedItem.baseItem != null)
		{
			UICursor.Set(UIItemSlot.mDraggedItem.baseItem.iconAtlas, UIItemSlot.mDraggedItem.baseItem.iconName);
			return;
		}
		UICursor.Clear();
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00029F08 File Offset: 0x00028108
	private void Update()
	{
		InvGameItem observedItem = this.observedItem;
		if (this.mItem == observedItem)
		{
			return;
		}
		this.mItem = observedItem;
		InvBaseItem invBaseItem = ((observedItem == null) ? null : observedItem.baseItem);
		if (this.label != null)
		{
			string text = ((observedItem == null) ? null : observedItem.name);
			if (string.IsNullOrEmpty(this.mText))
			{
				this.mText = this.label.text;
			}
			this.label.text = ((text == null) ? this.mText : text);
		}
		if (this.icon != null)
		{
			if (invBaseItem == null || invBaseItem.iconAtlas == null)
			{
				this.icon.enabled = false;
			}
			else
			{
				this.icon.atlas = invBaseItem.iconAtlas;
				this.icon.spriteName = invBaseItem.iconName;
				this.icon.enabled = true;
				this.icon.MakePixelPerfect();
			}
		}
		if (this.background != null)
		{
			this.background.color = ((observedItem == null) ? Color.white : observedItem.color);
		}
	}

	// Token: 0x04000707 RID: 1799
	public UISprite icon;

	// Token: 0x04000708 RID: 1800
	public UIWidget background;

	// Token: 0x04000709 RID: 1801
	public UILabel label;

	// Token: 0x0400070A RID: 1802
	public AudioClip grabSound;

	// Token: 0x0400070B RID: 1803
	public AudioClip placeSound;

	// Token: 0x0400070C RID: 1804
	public AudioClip errorSound;

	// Token: 0x0400070D RID: 1805
	private InvGameItem mItem;

	// Token: 0x0400070E RID: 1806
	private string mText = string.Empty;

	// Token: 0x0400070F RID: 1807
	private static InvGameItem mDraggedItem;
}
