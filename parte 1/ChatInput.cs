using System;
using UnityEngine;

// Token: 0x0200002F RID: 47
[AddComponentMenu("NGUI/Examples/Chat Input")]
[RequireComponent(typeof(UIInput))]
public class ChatInput : MonoBehaviour
{
	// Token: 0x0600021B RID: 539 RVA: 0x000093FC File Offset: 0x000075FC
	private void Start()
	{
		this.mInput = base.GetComponent<UIInput>();
		if (this.fillWithDummyData && this.textList != null)
		{
			for (int i = 0; i < 30; i++)
			{
				this.textList.Add(((i % 2 != 0) ? "[AAAAAA]" : "[FFFFFF]") + "This is an example paragraph for the text list, testing line " + i.ToString() + "[-]");
			}
		}
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000946A File Offset: 0x0000766A
	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Return))
		{
			if (!this.mIgnoreNextEnter && !this.mInput.selected)
			{
				this.mInput.selected = true;
			}
			this.mIgnoreNextEnter = false;
		}
	}

	// Token: 0x0600021D RID: 541 RVA: 0x000094A0 File Offset: 0x000076A0
	private void OnSubmit()
	{
		if (this.textList != null)
		{
			string text = NGUITools.StripSymbols(this.mInput.text);
			if (!string.IsNullOrEmpty(text))
			{
				this.textList.Add(text);
				this.mInput.text = string.Empty;
				this.mInput.selected = false;
			}
		}
		this.mIgnoreNextEnter = true;
	}

	// Token: 0x04000156 RID: 342
	public UITextList textList;

	// Token: 0x04000157 RID: 343
	public bool fillWithDummyData;

	// Token: 0x04000158 RID: 344
	private UIInput mInput;

	// Token: 0x04000159 RID: 345
	private bool mIgnoreNextEnter;
}
