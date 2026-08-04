using System;
using UnityEngine;

// Token: 0x0200011A RID: 282
[AddComponentMenu("NGUI/UI/Label")]
[ExecuteInEditMode]
public class UILabel : UIWidget
{
	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x0600081B RID: 2075 RVA: 0x0002A240 File Offset: 0x00028440
	// (set) Token: 0x0600081C RID: 2076 RVA: 0x0002A2D0 File Offset: 0x000284D0
	private bool hasChanged
	{
		get
		{
			return this.mShouldBeProcessed || this.mLastText != this.text || this.mLastWidth != this.mMaxLineWidth || this.mLastEncoding != this.mEncoding || this.mLastMulti != this.mMultiline || this.mLastPass != this.mPassword || this.mLastShow != this.mShowLastChar || this.mLastEffect != this.mEffectStyle || this.mLastColor != this.mEffectColor;
		}
		set
		{
			if (value)
			{
				this.mChanged = true;
				this.mShouldBeProcessed = true;
				return;
			}
			this.mShouldBeProcessed = false;
			this.mLastText = this.text;
			this.mLastWidth = this.mMaxLineWidth;
			this.mLastEncoding = this.mEncoding;
			this.mLastMulti = this.mMultiline;
			this.mLastPass = this.mPassword;
			this.mLastShow = this.mShowLastChar;
			this.mLastEffect = this.mEffectStyle;
			this.mLastColor = this.mEffectColor;
		}
	}

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x0600081D RID: 2077 RVA: 0x0002A356 File Offset: 0x00028556
	// (set) Token: 0x0600081E RID: 2078 RVA: 0x0002A360 File Offset: 0x00028560
	public UIFont font
	{
		get
		{
			return this.mFont;
		}
		set
		{
			if (this.mFont != value)
			{
				this.mFont = value;
				this.material = ((!(this.mFont != null)) ? null : this.mFont.material);
				this.mChanged = true;
				this.hasChanged = true;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x0600081F RID: 2079 RVA: 0x0002A3B8 File Offset: 0x000285B8
	// (set) Token: 0x06000820 RID: 2080 RVA: 0x0002A3C0 File Offset: 0x000285C0
	public string text
	{
		get
		{
			return this.mText;
		}
		set
		{
			if (value != null && this.mText != value)
			{
				this.mText = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x06000821 RID: 2081 RVA: 0x0002A3E1 File Offset: 0x000285E1
	// (set) Token: 0x06000822 RID: 2082 RVA: 0x0002A3E9 File Offset: 0x000285E9
	public bool supportEncoding
	{
		get
		{
			return this.mEncoding;
		}
		set
		{
			if (this.mEncoding != value)
			{
				this.mEncoding = value;
				this.hasChanged = true;
				if (value)
				{
					this.mPassword = false;
				}
			}
		}
	}

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x06000823 RID: 2083 RVA: 0x0002A40C File Offset: 0x0002860C
	// (set) Token: 0x06000824 RID: 2084 RVA: 0x0002A414 File Offset: 0x00028614
	public int lineWidth
	{
		get
		{
			return this.mMaxLineWidth;
		}
		set
		{
			if (this.mMaxLineWidth != value)
			{
				this.mMaxLineWidth = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x06000825 RID: 2085 RVA: 0x0002A42D File Offset: 0x0002862D
	// (set) Token: 0x06000826 RID: 2086 RVA: 0x0002A435 File Offset: 0x00028635
	public bool multiLine
	{
		get
		{
			return this.mMultiline;
		}
		set
		{
			if (this.mMultiline != value)
			{
				this.mMultiline = value;
				this.hasChanged = true;
				if (value)
				{
					this.mPassword = false;
				}
			}
		}
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x06000827 RID: 2087 RVA: 0x0002A458 File Offset: 0x00028658
	// (set) Token: 0x06000828 RID: 2088 RVA: 0x0002A460 File Offset: 0x00028660
	public bool password
	{
		get
		{
			return this.mPassword;
		}
		set
		{
			if (this.mPassword != value)
			{
				this.mPassword = value;
				this.mMultiline = false;
				this.mEncoding = false;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x06000829 RID: 2089 RVA: 0x0002A487 File Offset: 0x00028687
	// (set) Token: 0x0600082A RID: 2090 RVA: 0x0002A48F File Offset: 0x0002868F
	public bool showLastPasswordChar
	{
		get
		{
			return this.mShowLastChar;
		}
		set
		{
			if (this.mShowLastChar != value)
			{
				this.mShowLastChar = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x0600082B RID: 2091 RVA: 0x0002A4A8 File Offset: 0x000286A8
	// (set) Token: 0x0600082C RID: 2092 RVA: 0x0002A4B0 File Offset: 0x000286B0
	public UILabel.Effect effectStyle
	{
		get
		{
			return this.mEffectStyle;
		}
		set
		{
			if (this.mEffectStyle != value)
			{
				this.mEffectStyle = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x0600082D RID: 2093 RVA: 0x0002A4C9 File Offset: 0x000286C9
	// (set) Token: 0x0600082E RID: 2094 RVA: 0x0002A4D1 File Offset: 0x000286D1
	public Color effectColor
	{
		get
		{
			return this.mEffectColor;
		}
		set
		{
			if (this.mEffectColor != value)
			{
				this.mEffectColor = value;
				if (this.mEffectStyle != UILabel.Effect.None)
				{
					this.hasChanged = true;
				}
			}
		}
	}

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x0600082F RID: 2095 RVA: 0x0002A4F8 File Offset: 0x000286F8
	public string processedText
	{
		get
		{
			if (this.mLastScale != base.cachedTransform.localScale)
			{
				this.mLastScale = base.cachedTransform.localScale;
				this.mShouldBeProcessed = true;
			}
			if (this.hasChanged)
			{
				this.ProcessText();
			}
			return this.mProcessedText;
		}
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x06000830 RID: 2096 RVA: 0x0002A54C File Offset: 0x0002874C
	public override Material material
	{
		get
		{
			Material material = base.material;
			if (material == null)
			{
				material = (this.material = ((!(this.mFont != null)) ? null : this.mFont.material));
			}
			return material;
		}
	}

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x06000831 RID: 2097 RVA: 0x0002A590 File Offset: 0x00028790
	public override Vector2 relativeSize
	{
		get
		{
			if (this.mFont == null)
			{
				return Vector3.one;
			}
			if (this.hasChanged)
			{
				this.ProcessText();
			}
			return this.mSize;
		}
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x0002A5C4 File Offset: 0x000287C4
	protected override void OnStart()
	{
		if (this.mLineWidth > 0f)
		{
			this.mMaxLineWidth = Mathf.RoundToInt(this.mLineWidth);
			this.mLineWidth = 0f;
		}
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x0002A5EF File Offset: 0x000287EF
	public override void MarkAsChanged()
	{
		this.hasChanged = true;
		base.MarkAsChanged();
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x0002A600 File Offset: 0x00028800
	private void ProcessText()
	{
		this.mChanged = true;
		this.hasChanged = false;
		this.mLastText = this.mText;
		this.mProcessedText = this.mText.Replace("\\n", "\n");
		if (this.mPassword)
		{
			this.mProcessedText = this.mFont.WrapText(this.mProcessedText, 100000f, false, false);
			string text = string.Empty;
			if (this.mShowLastChar)
			{
				int i = 1;
				int length = this.mProcessedText.Length;
				while (i < length)
				{
					text += "*";
					i++;
				}
				if (this.mProcessedText.Length > 0)
				{
					text += this.mProcessedText[this.mProcessedText.Length - 1].ToString();
				}
			}
			else
			{
				int j = 0;
				int length2 = this.mProcessedText.Length;
				while (j < length2)
				{
					text += "*";
					j++;
				}
			}
			this.mProcessedText = text;
		}
		else if (this.mMaxLineWidth > 0)
		{
			this.mProcessedText = this.mFont.WrapText(this.mProcessedText, (float)this.mMaxLineWidth / base.cachedTransform.localScale.x, this.mMultiline, this.mEncoding);
		}
		else if (!this.mMultiline)
		{
			this.mProcessedText = this.mFont.WrapText(this.mProcessedText, 100000f, false, this.mEncoding);
		}
		this.mSize = (string.IsNullOrEmpty(this.mProcessedText) ? Vector2.one : this.mFont.CalculatePrintedSize(this.mProcessedText, this.mEncoding));
		float x = base.cachedTransform.localScale.x;
		this.mSize.x = Mathf.Max(this.mSize.x, (!(this.mFont != null) || x <= 1f) ? 1f : ((float)this.lineWidth / x));
		this.mSize.y = Mathf.Max(this.mSize.y, 1f);
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0002A824 File Offset: 0x00028A24
	public void MakePositionPerfect()
	{
		float num = ((!(this.font.atlas != null)) ? 1f : this.font.atlas.pixelSize);
		Vector3 localScale = base.cachedTransform.localScale;
		if (this.mFont.size == Mathf.RoundToInt(localScale.x / num) && this.mFont.size == Mathf.RoundToInt(localScale.y / num) && base.cachedTransform.localRotation == Quaternion.identity)
		{
			Vector2 vector = this.relativeSize * localScale.x;
			int num2 = Mathf.RoundToInt(vector.x / num);
			int num3 = Mathf.RoundToInt(vector.y / num);
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.x = (float)Mathf.FloorToInt(localPosition.x / num);
			localPosition.y = (float)Mathf.CeilToInt(localPosition.y / num);
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			if (num2 % 2 == 1 && (base.pivot == UIWidget.Pivot.Top || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Bottom))
			{
				localPosition.x += 0.5f;
			}
			if (num3 % 2 == 1 && (base.pivot == UIWidget.Pivot.Left || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Right))
			{
				localPosition.y -= 0.5f;
			}
			localPosition.x *= num;
			localPosition.y *= num;
			if (base.cachedTransform.localPosition != localPosition)
			{
				base.cachedTransform.localPosition = localPosition;
			}
		}
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0002A9C8 File Offset: 0x00028BC8
	public override void MakePixelPerfect()
	{
		if (this.mFont != null)
		{
			float num = ((!(this.font.atlas != null)) ? 1f : this.font.atlas.pixelSize);
			Vector3 localScale = base.cachedTransform.localScale;
			localScale.x = (float)this.mFont.size * num;
			localScale.y = localScale.x;
			localScale.z = 1f;
			Vector2 vector = this.relativeSize * localScale.x;
			int num2 = Mathf.RoundToInt(vector.x / num);
			int num3 = Mathf.RoundToInt(vector.y / num);
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.x = (float)Mathf.FloorToInt(localPosition.x / num);
			localPosition.y = (float)Mathf.CeilToInt(localPosition.y / num);
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			if (base.cachedTransform.localRotation == Quaternion.identity)
			{
				if (num2 % 2 == 1 && (base.pivot == UIWidget.Pivot.Top || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Bottom))
				{
					localPosition.x += 0.5f;
				}
				if (num3 % 2 == 1 && (base.pivot == UIWidget.Pivot.Left || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Right))
				{
					localPosition.y -= 0.5f;
				}
			}
			localPosition.x *= num;
			localPosition.y *= num;
			base.cachedTransform.localPosition = localPosition;
			base.cachedTransform.localScale = localScale;
			return;
		}
		base.MakePixelPerfect();
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x0002AB78 File Offset: 0x00028D78
	private void ApplyShadow(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color> cols, int start, int end, float x, float y)
	{
		Color color = this.mEffectColor;
		color.a *= base.color.a;
		for (int i = start; i < end; i++)
		{
			verts.Add(verts.buffer[i]);
			uvs.Add(uvs.buffer[i]);
			cols.Add(cols.buffer[i]);
			Vector3 vector = verts.buffer[i];
			vector.x += x;
			vector.y += y;
			verts.buffer[i] = vector;
			cols.buffer[i] = color;
		}
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x0002AC28 File Offset: 0x00028E28
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color> cols)
	{
		if (this.mFont == null)
		{
			return;
		}
		this.MakePositionPerfect();
		UIWidget.Pivot pivot = base.pivot;
		int num = verts.size;
		switch (pivot)
		{
		case UIWidget.Pivot.TopLeft:
		case UIWidget.Pivot.Left:
		case UIWidget.Pivot.BottomLeft:
			this.mFont.Print(this.processedText, base.color, verts, uvs, cols, this.mEncoding, UIFont.Alignment.Left, 0);
			goto IL_00F1;
		case UIWidget.Pivot.TopRight:
		case UIWidget.Pivot.Right:
		case UIWidget.Pivot.BottomRight:
			this.mFont.Print(this.processedText, base.color, verts, uvs, cols, this.mEncoding, UIFont.Alignment.Right, Mathf.RoundToInt(this.relativeSize.x * (float)this.mFont.size));
			goto IL_00F1;
		}
		this.mFont.Print(this.processedText, base.color, verts, uvs, cols, this.mEncoding, UIFont.Alignment.Center, Mathf.RoundToInt(this.relativeSize.x * (float)this.mFont.size));
		IL_00F1:
		if (this.effectStyle == UILabel.Effect.None)
		{
			return;
		}
		Vector3 localScale = base.cachedTransform.localScale;
		if (localScale.x != 0f && localScale.y != 0f)
		{
			int num2 = verts.size;
			float num3 = 1f / (float)this.mFont.size;
			this.ApplyShadow(verts, uvs, cols, num, num2, num3, 0f - num3);
			if (this.effectStyle == UILabel.Effect.Outline)
			{
				num = num2;
				num2 = verts.size;
				this.ApplyShadow(verts, uvs, cols, num, num2, 0f - num3, num3);
				num = num2;
				num2 = verts.size;
				this.ApplyShadow(verts, uvs, cols, num, num2, num3, num3);
				num = num2;
				num2 = verts.size;
				this.ApplyShadow(verts, uvs, cols, num, num2, 0f - num3, 0f - num3);
			}
		}
	}

	// Token: 0x04000718 RID: 1816
	[SerializeField]
	[HideInInspector]
	private UIFont mFont;

	// Token: 0x04000719 RID: 1817
	[HideInInspector]
	[SerializeField]
	private string mText = string.Empty;

	// Token: 0x0400071A RID: 1818
	[HideInInspector]
	[SerializeField]
	private int mMaxLineWidth;

	// Token: 0x0400071B RID: 1819
	[HideInInspector]
	[SerializeField]
	private bool mEncoding = true;

	// Token: 0x0400071C RID: 1820
	[SerializeField]
	[HideInInspector]
	private bool mMultiline = true;

	// Token: 0x0400071D RID: 1821
	[SerializeField]
	[HideInInspector]
	private bool mPassword;

	// Token: 0x0400071E RID: 1822
	[HideInInspector]
	[SerializeField]
	private bool mShowLastChar;

	// Token: 0x0400071F RID: 1823
	[HideInInspector]
	[SerializeField]
	private UILabel.Effect mEffectStyle;

	// Token: 0x04000720 RID: 1824
	[HideInInspector]
	[SerializeField]
	private Color mEffectColor = Color.black;

	// Token: 0x04000721 RID: 1825
	[SerializeField]
	[HideInInspector]
	private float mLineWidth;

	// Token: 0x04000722 RID: 1826
	private bool mShouldBeProcessed = true;

	// Token: 0x04000723 RID: 1827
	private string mProcessedText;

	// Token: 0x04000724 RID: 1828
	private Vector3 mLastScale = Vector3.one;

	// Token: 0x04000725 RID: 1829
	private string mLastText = string.Empty;

	// Token: 0x04000726 RID: 1830
	private int mLastWidth;

	// Token: 0x04000727 RID: 1831
	private bool mLastEncoding = true;

	// Token: 0x04000728 RID: 1832
	private bool mLastMulti = true;

	// Token: 0x04000729 RID: 1833
	private bool mLastPass;

	// Token: 0x0400072A RID: 1834
	private bool mLastShow;

	// Token: 0x0400072B RID: 1835
	private UILabel.Effect mLastEffect;

	// Token: 0x0400072C RID: 1836
	private Color mLastColor = Color.black;

	// Token: 0x0400072D RID: 1837
	private Vector3 mSize = Vector3.zero;

	// Token: 0x0200020E RID: 526
	public enum Effect
	{
		// Token: 0x04000BFA RID: 3066
		None,
		// Token: 0x04000BFB RID: 3067
		Shadow,
		// Token: 0x04000BFC RID: 3068
		Outline
	}
}
