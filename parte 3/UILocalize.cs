using System;
using UnityEngine;

// Token: 0x0200011B RID: 283
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/UI/Localize")]
public class UILocalize : MonoBehaviour
{
	// Token: 0x0600083A RID: 2106 RVA: 0x0002AE68 File Offset: 0x00029068
	private void OnLocalize(Localization loc)
	{
		if (this.mLanguage != loc.currentLanguage)
		{
			UIWidget component = base.GetComponent<UIWidget>();
			UILabel uilabel = component as UILabel;
			UISprite uisprite = component as UISprite;
			if (string.IsNullOrEmpty(this.mLanguage) && string.IsNullOrEmpty(this.key) && uilabel != null)
			{
				this.key = uilabel.text;
			}
			string text = ((!string.IsNullOrEmpty(this.key)) ? loc.Get(this.key) : loc.Get(component.name));
			if (uilabel != null)
			{
				uilabel.text = text;
			}
			else if (uisprite != null)
			{
				uisprite.spriteName = text;
				uisprite.MakePixelPerfect();
			}
			this.mLanguage = loc.currentLanguage;
		}
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x0002AF2B File Offset: 0x0002912B
	private void OnEnable()
	{
		if (this.mStarted && Localization.instance != null)
		{
			this.OnLocalize(Localization.instance);
		}
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x0002AF4D File Offset: 0x0002914D
	private void Start()
	{
		this.mStarted = true;
		if (Localization.instance != null)
		{
			this.OnLocalize(Localization.instance);
		}
	}

	// Token: 0x0400072E RID: 1838
	public string key;

	// Token: 0x0400072F RID: 1839
	private string mLanguage;

	// Token: 0x04000730 RID: 1840
	private bool mStarted;
}
