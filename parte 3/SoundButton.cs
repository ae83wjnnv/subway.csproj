using System;
using UnityEngine;

// Token: 0x020000C9 RID: 201
public class SoundButton : MonoBehaviour
{
	// Token: 0x06000602 RID: 1538 RVA: 0x0001E265 File Offset: 0x0001C465
	private void Awake()
	{
		this._SetupButton();
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0001E26D File Offset: 0x0001C46D
	public void Click()
	{
		Settings.optionSound = !Settings.optionSound;
		this._SetupButton();
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x0001E284 File Offset: 0x0001C484
	private void _SetupButton()
	{
		if (Settings.optionSound)
		{
			this.soundLabel.text = this._labelOn;
			this.soundIcon.spriteName = this._iconOn;
			return;
		}
		this.soundLabel.text = this._labelOff;
		this.soundIcon.spriteName = this._iconOff;
	}

	// Token: 0x040004FD RID: 1277
	public UISprite soundIcon;

	// Token: 0x040004FE RID: 1278
	private string _iconOn = "icon_soundOn";

	// Token: 0x040004FF RID: 1279
	private string _iconOff = "icon_soundOff";

	// Token: 0x04000500 RID: 1280
	public UILabel soundLabel;

	// Token: 0x04000501 RID: 1281
	private string _labelOff = "Sound OFF";

	// Token: 0x04000502 RID: 1282
	private string _labelOn = "Sound ON";
}
