using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class AutoMessageButton : MonoBehaviour
{
	// Token: 0x06000167 RID: 359 RVA: 0x000058BB File Offset: 0x00003ABB
	private void Awake()
	{
		this._SetupButton();
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000058C3 File Offset: 0x00003AC3
	public void Click()
	{
		Settings.optionAutoMessage = !Settings.optionAutoMessage;
		this._SetupButton();
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000058D8 File Offset: 0x00003AD8
	private void _SetupButton()
	{
		if (Settings.optionAutoMessage)
		{
			this.Label.text = this._labelOn;
			this.Icon.spriteName = this._iconOn;
			return;
		}
		this.Label.text = this._labelOff;
		this.Icon.spriteName = this._iconOff;
	}

	// Token: 0x040000B1 RID: 177
	public UISprite Icon;

	// Token: 0x040000B2 RID: 178
	private string _iconOn = "icon_autoMessage_on";

	// Token: 0x040000B3 RID: 179
	private string _iconOff = "icon_autoMessage_off";

	// Token: 0x040000B4 RID: 180
	public UILabel Label;

	// Token: 0x040000B5 RID: 181
	private string _labelOff = "Auto Message OFF";

	// Token: 0x040000B6 RID: 182
	private string _labelOn = "Auto Message ON";
}
