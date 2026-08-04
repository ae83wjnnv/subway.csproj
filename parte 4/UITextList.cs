using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200013A RID: 314
[AddComponentMenu("NGUI/UI/Text List")]
public class UITextList : MonoBehaviour
{
	// Token: 0x06000940 RID: 2368 RVA: 0x00031F08 File Offset: 0x00030108
	public void Clear()
	{
		this.mParagraphs.Clear();
		this.UpdateVisibleText();
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x00031F1B File Offset: 0x0003011B
	public void Add(string text)
	{
		this.Add(text, true);
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00031F28 File Offset: 0x00030128
	protected void Add(string text, bool updateVisible)
	{
		UITextList.Paragraph paragraph;
		if (this.mParagraphs.Count < this.maxEntries)
		{
			paragraph = new UITextList.Paragraph();
		}
		else
		{
			paragraph = this.mParagraphs[0];
			this.mParagraphs.RemoveAt(0);
		}
		paragraph.text = text;
		this.mParagraphs.Add(paragraph);
		if (this.textLabel != null && this.textLabel.font != null)
		{
			paragraph.lines = this.textLabel.font.WrapText(paragraph.text, this.maxWidth / this.textLabel.transform.localScale.y, true, true).Split(this.mSeparator);
			this.mTotalLines = 0;
			int i = 0;
			int count = this.mParagraphs.Count;
			while (i < count)
			{
				this.mTotalLines += this.mParagraphs[i].lines.Length;
				i++;
			}
		}
		if (updateVisible)
		{
			this.UpdateVisibleText();
		}
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x00032034 File Offset: 0x00030234
	private void Awake()
	{
		if (this.textLabel == null)
		{
			this.textLabel = base.GetComponentInChildren<UILabel>();
		}
		if (this.textLabel != null)
		{
			this.textLabel.lineWidth = 0;
		}
		Collider component = base.GetComponent<Collider>();
		if (component != null)
		{
			if (this.maxHeight <= 0f)
			{
				this.maxHeight = component.bounds.size.y / base.transform.lossyScale.y;
			}
			if (this.maxWidth <= 0f)
			{
				this.maxWidth = component.bounds.size.x / base.transform.lossyScale.x;
			}
		}
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x000320F3 File Offset: 0x000302F3
	private void OnSelect(bool selected)
	{
		this.mSelected = selected;
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x000320FC File Offset: 0x000302FC
	protected void UpdateVisibleText()
	{
		if (!(this.textLabel != null))
		{
			return;
		}
		if (!(this.textLabel.font != null))
		{
			return;
		}
		int num = 0;
		int num2 = ((this.maxHeight <= 0f) ? 100000 : Mathf.FloorToInt(this.maxHeight / this.textLabel.cachedTransform.localScale.y));
		int num3 = Mathf.RoundToInt(this.mScroll);
		if (num2 + num3 > this.mTotalLines)
		{
			num3 = Mathf.Max(0, this.mTotalLines - num2);
			this.mScroll = (float)num3;
		}
		if (this.style == UITextList.Style.Chat)
		{
			num3 = Mathf.Max(0, this.mTotalLines - num2 - num3);
		}
		string text = string.Empty;
		int i = 0;
		int count = this.mParagraphs.Count;
		while (i < count)
		{
			UITextList.Paragraph paragraph = this.mParagraphs[i];
			int j = 0;
			int num4 = paragraph.lines.Length;
			while (j < num4)
			{
				string text2 = paragraph.lines[j];
				if (num3 > 0)
				{
					num3--;
				}
				else
				{
					if (text.Length > 0)
					{
						text += "\n";
					}
					text += text2;
					num++;
					if (num >= num2)
					{
						break;
					}
				}
				j++;
			}
			if (num >= num2)
			{
				break;
			}
			i++;
		}
		this.textLabel.text = text;
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00032248 File Offset: 0x00030448
	private void OnScroll(float val)
	{
		if (this.mSelected && this.supportScrollWheel)
		{
			val *= ((this.style != UITextList.Style.Chat) ? (-10f) : 10f);
			this.mScroll = Mathf.Max(0f, this.mScroll + val);
			this.UpdateVisibleText();
		}
	}

	// Token: 0x0400080C RID: 2060
	public UITextList.Style style;

	// Token: 0x0400080D RID: 2061
	public UILabel textLabel;

	// Token: 0x0400080E RID: 2062
	public float maxWidth;

	// Token: 0x0400080F RID: 2063
	public float maxHeight;

	// Token: 0x04000810 RID: 2064
	public int maxEntries = 50;

	// Token: 0x04000811 RID: 2065
	public bool supportScrollWheel = true;

	// Token: 0x04000812 RID: 2066
	protected char[] mSeparator = new char[] { '\n' };

	// Token: 0x04000813 RID: 2067
	protected List<UITextList.Paragraph> mParagraphs = new List<UITextList.Paragraph>();

	// Token: 0x04000814 RID: 2068
	protected float mScroll;

	// Token: 0x04000815 RID: 2069
	protected bool mSelected;

	// Token: 0x04000816 RID: 2070
	protected int mTotalLines;

	// Token: 0x0200021C RID: 540
	public enum Style
	{
		// Token: 0x04000C37 RID: 3127
		Text,
		// Token: 0x04000C38 RID: 3128
		Chat
	}

	// Token: 0x0200021D RID: 541
	protected class Paragraph
	{
		// Token: 0x04000C39 RID: 3129
		public string text;

		// Token: 0x04000C3A RID: 3130
		public string[] lines;
	}
}
