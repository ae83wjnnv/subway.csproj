using System;
using UnityEngine;

// Token: 0x02000045 RID: 69
[ExecuteInEditMode]
public class DailyChallengeColorChanger : MonoBehaviour
{
	// Token: 0x06000285 RID: 645 RVA: 0x0000B43C File Offset: 0x0000963C
	private void Awake()
	{
		this._myLabel = base.gameObject.GetComponent<UILabel>();
		this._MyColorActive = NGUITools.EncodeColor(this.MyColorActive);
		this._MyColorInactive = NGUITools.EncodeColor(this.MyColorInactive);
		this._shadowColorActive = NGUITools.EncodeColor(this.shadowColorActive);
		this._shadowColorInactive = NGUITools.EncodeColor(this.shadowColorInactive);
	}

	// Token: 0x06000286 RID: 646 RVA: 0x0000B49E File Offset: 0x0000969E
	private void Update()
	{
		this.UpdateDailyWord();
	}

	// Token: 0x06000287 RID: 647 RVA: 0x0000B4A8 File Offset: 0x000096A8
	private void UpdateDailyWord()
	{
		if (this._myLabel == null)
		{
			this._myLabel = base.gameObject.GetComponent<UILabel>();
		}
		string text = PlayerInfo.Instance.dailyWord;
		if (string.IsNullOrEmpty(text))
		{
			text = string.Empty;
		}
		int length = text.Length;
		IntMask dailyWordUnlockedMask = PlayerInfo.Instance.dailyWordUnlockedMask;
		if (!(text == this._cachedDailyWord) || dailyWordUnlockedMask != this._cachedDailyMask)
		{
			this._cachedDailyWord = text;
			this._cachedDailyMask = dailyWordUnlockedMask;
			string text2 = string.Empty;
			for (int i = 0; i < length; i++)
			{
				text2 = ((!dailyWordUnlockedMask[i]) ? (text2 + "[" + this._MyColorInactive + "]") : (text2 + "[" + this._MyColorActive + "]"));
				text2 = text2 + text[i].ToString() + " ";
			}
			this._cachedText = text2;
			this._myLabel.text = this._cachedText;
			string text3 = string.Empty;
			for (int j = 0; j < length; j++)
			{
				text3 = ((!dailyWordUnlockedMask[j]) ? (text3 + "[" + this._shadowColorInactive + "]") : (text3 + "[" + this._shadowColorActive + "]"));
				text3 = text3 + text[j].ToString() + " ";
			}
			this.shadowLabel.text = text3;
		}
	}

	// Token: 0x040001BE RID: 446
	public UILabel shadowLabel;

	// Token: 0x040001BF RID: 447
	public Color MyColorActive;

	// Token: 0x040001C0 RID: 448
	public Color MyColorInactive;

	// Token: 0x040001C1 RID: 449
	public Color shadowColorActive;

	// Token: 0x040001C2 RID: 450
	public Color shadowColorInactive;

	// Token: 0x040001C3 RID: 451
	private string _MyColorActive;

	// Token: 0x040001C4 RID: 452
	private string _MyColorInactive;

	// Token: 0x040001C5 RID: 453
	private string _shadowColorActive;

	// Token: 0x040001C6 RID: 454
	private string _shadowColorInactive;

	// Token: 0x040001C7 RID: 455
	private UILabel _myLabel;

	// Token: 0x040001C8 RID: 456
	private string _cachedText = string.Empty;

	// Token: 0x040001C9 RID: 457
	private string _cachedDailyWord = string.Empty;

	// Token: 0x040001CA RID: 458
	private IntMask _cachedDailyMask = -1;
}
