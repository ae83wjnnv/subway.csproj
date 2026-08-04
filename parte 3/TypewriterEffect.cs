using System;
using UnityEngine;

// Token: 0x020000E7 RID: 231
[AddComponentMenu("NGUI/Examples/Typewriter Effect")]
[RequireComponent(typeof(UILabel))]
public class TypewriterEffect : MonoBehaviour
{
	// Token: 0x06000694 RID: 1684 RVA: 0x00020834 File Offset: 0x0001EA34
	private void Update()
	{
		if (this.mLabel == null)
		{
			this.mLabel = base.GetComponent<UILabel>();
			this.mText = this.mLabel.font.WrapText(this.mLabel.text, (float)this.mLabel.lineWidth / this.mLabel.cachedTransform.localScale.x, true, true);
		}
		if (this.mOffset < this.mText.Length)
		{
			if (this.mNextChar <= Time.time)
			{
				this.charsPerSecond = Mathf.Max(1, this.charsPerSecond);
				float num = 1f / (float)this.charsPerSecond;
				char c = this.mText[this.mOffset];
				if (c == '.' || c == '\n' || c == '!' || c == '?')
				{
					num *= 4f;
				}
				this.mNextChar = Time.time + num;
				UILabel uilabel = this.mLabel;
				string text = this.mText;
				int num2 = 0;
				int num3 = this.mOffset + 1;
				this.mOffset = num3;
				uilabel.text = text.Substring(num2, num3);
				return;
			}
		}
		else
		{
			Object.Destroy(this);
		}
	}

	// Token: 0x0400059E RID: 1438
	public int charsPerSecond = 40;

	// Token: 0x0400059F RID: 1439
	private UILabel mLabel;

	// Token: 0x040005A0 RID: 1440
	private string mText;

	// Token: 0x040005A1 RID: 1441
	private int mOffset;

	// Token: 0x040005A2 RID: 1442
	private float mNextChar;
}
