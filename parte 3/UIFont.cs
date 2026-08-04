using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x0200010B RID: 267
[AddComponentMenu("NGUI/UI/Font")]
[ExecuteInEditMode]
public class UIFont : MonoBehaviour
{
	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x0600079F RID: 1951 RVA: 0x000273C3 File Offset: 0x000255C3
	public BMFont bmFont
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.bmFont;
			}
			return this.mFont;
		}
	}

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x060007A0 RID: 1952 RVA: 0x000273E5 File Offset: 0x000255E5
	public int texWidth
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.texWidth;
			}
			if (this.mFont != null)
			{
				return this.mFont.texWidth;
			}
			return 1;
		}
	}

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00027416 File Offset: 0x00025616
	public int texHeight
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.texHeight;
			}
			if (this.mFont != null)
			{
				return this.mFont.texHeight;
			}
			return 1;
		}
	}

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00027447 File Offset: 0x00025647
	// (set) Token: 0x060007A3 RID: 1955 RVA: 0x0002746C File Offset: 0x0002566C
	public UIAtlas atlas
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.atlas;
			}
			return this.mAtlas;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.atlas = value;
				return;
			}
			if (!(this.mAtlas != value))
			{
				return;
			}
			if (value == null)
			{
				if (this.mAtlas != null)
				{
					this.mMat = this.mAtlas.spriteMaterial;
				}
				if (this.sprite != null)
				{
					this.mUVRect = this.uvRect;
				}
			}
			this.mAtlas = value;
			this.MarkAsDirty();
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x060007A4 RID: 1956 RVA: 0x000274EC File Offset: 0x000256EC
	// (set) Token: 0x060007A5 RID: 1957 RVA: 0x00027528 File Offset: 0x00025728
	public Material material
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.material;
			}
			if (this.mAtlas != null)
			{
				return this.mAtlas.spriteMaterial;
			}
			return this.mMat;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.material = value;
				return;
			}
			if (this.mAtlas == null && this.mMat != value)
			{
				this.mMat = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0002757C File Offset: 0x0002577C
	public Texture2D texture
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.texture;
			}
			Material material = this.material;
			if (material != null)
			{
				return material.mainTexture as Texture2D;
			}
			return null;
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x060007A7 RID: 1959 RVA: 0x000275C0 File Offset: 0x000257C0
	// (set) Token: 0x060007A8 RID: 1960 RVA: 0x00027729 File Offset: 0x00025929
	public Rect uvRect
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.uvRect;
			}
			if (this.mAtlas != null && this.mSprite == null && this.sprite != null)
			{
				Texture texture = this.mAtlas.texture;
				if (texture != null)
				{
					this.mUVRect = this.mSprite.outer;
					if (this.mAtlas.coordinates == UIAtlas.Coordinates.Pixels)
					{
						this.mUVRect = NGUIMath.ConvertToTexCoords(this.mUVRect, texture.width, texture.height);
					}
					if (this.mSprite.hasPadding)
					{
						Rect rect = this.mUVRect;
						this.mUVRect.xMin = rect.xMin - this.mSprite.paddingLeft * rect.width;
						this.mUVRect.yMin = rect.yMin - this.mSprite.paddingBottom * rect.height;
						this.mUVRect.xMax = rect.xMax + this.mSprite.paddingRight * rect.width;
						this.mUVRect.yMax = rect.yMax + this.mSprite.paddingTop * rect.height;
					}
					if (this.mSprite.hasPadding)
					{
						this.Trim();
					}
				}
			}
			return this.mUVRect;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.uvRect = value;
				return;
			}
			if (this.sprite == null && this.mUVRect != value)
			{
				this.mUVRect = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00027769 File Offset: 0x00025969
	// (set) Token: 0x060007AA RID: 1962 RVA: 0x00027790 File Offset: 0x00025990
	public string spriteName
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.spriteName;
			}
			return this.mFont.spriteName;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteName = value;
				return;
			}
			if (this.mFont.spriteName != value)
			{
				this.mFont.spriteName = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x060007AB RID: 1963 RVA: 0x000277DD File Offset: 0x000259DD
	// (set) Token: 0x060007AC RID: 1964 RVA: 0x000277FF File Offset: 0x000259FF
	public int horizontalSpacing
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.horizontalSpacing;
			}
			return this.mSpacingX;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.horizontalSpacing = value;
				return;
			}
			if (this.mSpacingX != value)
			{
				this.mSpacingX = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x060007AD RID: 1965 RVA: 0x00027832 File Offset: 0x00025A32
	// (set) Token: 0x060007AE RID: 1966 RVA: 0x00027854 File Offset: 0x00025A54
	public int verticalSpacing
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.verticalSpacing;
			}
			return this.mSpacingY;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.verticalSpacing = value;
				return;
			}
			if (this.mSpacingY != value)
			{
				this.mSpacingY = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x060007AF RID: 1967 RVA: 0x00027887 File Offset: 0x00025A87
	public int size
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.size;
			}
			return this.mFont.charSize;
		}
	}

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x060007B0 RID: 1968 RVA: 0x000278B0 File Offset: 0x00025AB0
	public UIAtlas.Sprite sprite
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.sprite;
			}
			if (!this.mSpriteSet)
			{
				this.mSprite = null;
			}
			if (this.mSprite == null && this.mAtlas != null && !string.IsNullOrEmpty(this.mFont.spriteName))
			{
				this.mSprite = this.mAtlas.GetSprite(this.mFont.spriteName);
				if (this.mSprite == null)
				{
					this.mSprite = this.mAtlas.GetSprite(base.name);
				}
				this.mSpriteSet = true;
				if (this.mSprite == null)
				{
					Debug.LogError("Can't find the sprite '" + this.mFont.spriteName + "' in UIAtlas on " + NGUITools.GetHierarchy(this.mAtlas.gameObject));
					this.mFont.spriteName = null;
				}
			}
			return this.mSprite;
		}
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x060007B1 RID: 1969 RVA: 0x000279A2 File Offset: 0x00025BA2
	// (set) Token: 0x060007B2 RID: 1970 RVA: 0x000279AC File Offset: 0x00025BAC
	public UIFont replacement
	{
		get
		{
			return this.mReplacement;
		}
		set
		{
			UIFont uifont = value;
			if (uifont == this)
			{
				uifont = null;
			}
			if (this.mReplacement != uifont)
			{
				if (uifont != null && uifont.replacement == this)
				{
					uifont.replacement = null;
				}
				if (this.mReplacement != null)
				{
					this.MarkAsDirty();
				}
				this.mReplacement = uifont;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x00027A14 File Offset: 0x00025C14
	private void Trim()
	{
		Texture texture = this.mAtlas.texture;
		if (texture != null && this.mSprite != null)
		{
			Rect rect = NGUIMath.ConvertToPixels(this.mUVRect, this.texture.width, this.texture.height, true);
			Rect rect2 = ((this.mAtlas.coordinates != UIAtlas.Coordinates.TexCoords) ? this.mSprite.outer : NGUIMath.ConvertToPixels(this.mSprite.outer, texture.width, texture.height, true));
			int num = Mathf.RoundToInt(rect2.xMin - rect.xMin);
			int num2 = Mathf.RoundToInt(rect2.yMin - rect.yMin);
			int num3 = Mathf.RoundToInt(rect2.xMax - rect.xMin);
			int num4 = Mathf.RoundToInt(rect2.yMax - rect.yMin);
			this.mFont.Trim(num, num2, num3, num4);
		}
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00027B09 File Offset: 0x00025D09
	private bool References(UIFont font)
	{
		return !(font == null) && (font == this || (this.mReplacement != null && this.mReplacement.References(font)));
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00027B3D File Offset: 0x00025D3D
	public static bool CheckIfRelated(UIFont a, UIFont b)
	{
		return !(a == null) && !(b == null) && (a == b || a.References(b) || b.References(a));
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x00027B70 File Offset: 0x00025D70
	public void MarkAsDirty()
	{
		this.mSprite = null;
		UILabel[] array = NGUITools.FindActive<UILabel>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UILabel uilabel = array[i];
			if (uilabel.enabled && uilabel.gameObject.active && UIFont.CheckIfRelated(this, uilabel.font))
			{
				UIFont font = uilabel.font;
				uilabel.font = null;
				uilabel.font = font;
			}
			i++;
		}
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x00027BD8 File Offset: 0x00025DD8
	public Vector2 CalculatePrintedSize(string text, bool encoding)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.CalculatePrintedSize(text, encoding);
		}
		Vector2 zero = Vector2.zero;
		if (this.mFont != null && this.mFont.isValid && !string.IsNullOrEmpty(text))
		{
			if (encoding)
			{
				text = NGUITools.StripSymbols(text);
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = this.mFont.charSize + this.mSpacingY;
			int i = 0;
			int length = text.Length;
			while (i < length)
			{
				char c = text[i];
				if (c == '\n')
				{
					if (num2 > num)
					{
						num = num2;
					}
					num2 = 0;
					num3 += num5;
					num4 = 0;
				}
				else if (c < ' ')
				{
					num4 = 0;
				}
				else
				{
					BMGlyph glyph = this.mFont.GetGlyph((int)c);
					if (glyph != null)
					{
						num2 += this.mSpacingX + ((num4 == 0) ? glyph.advance : (glyph.advance + glyph.GetKerning(num4)));
						num4 = (int)c;
					}
				}
				i++;
			}
			float num6 = ((this.mFont.charSize <= 0) ? 1f : (1f / (float)this.mFont.charSize));
			zero.x = num6 * (float)((num2 <= num) ? num : num2);
			zero.y = num6 * (float)(num3 + num5);
		}
		return zero;
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x00027D28 File Offset: 0x00025F28
	private static void EndLine(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && s[num] == ' ')
		{
			s[num] = '\n';
			return;
		}
		s.Append('\n');
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x00027D64 File Offset: 0x00025F64
	public string WrapText(string text, float maxWidth, bool multiline, bool encoding)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.WrapText(text, maxWidth, multiline, encoding);
		}
		int num = Mathf.RoundToInt(maxWidth * (float)this.size);
		if (num < 1)
		{
			return text;
		}
		StringBuilder stringBuilder = new StringBuilder();
		int length = text.Length;
		int num2 = num;
		int num3 = 0;
		int num4 = 0;
		int i = 0;
		bool flag = true;
		while (i < length)
		{
			char c = text[i];
			if (c == '\n')
			{
				if (!multiline)
				{
					break;
				}
				num2 = num;
				if (num4 < i)
				{
					stringBuilder.Append(text.Substring(num4, i - num4 + 1));
				}
				else
				{
					stringBuilder.Append(c);
				}
				flag = true;
				num4 = i + 1;
				num3 = 0;
			}
			else
			{
				if (c == ' ' && num3 != 32 && num4 < i)
				{
					stringBuilder.Append(text.Substring(num4, i - num4 + 1));
					flag = false;
					num4 = i + 1;
					num3 = (int)c;
				}
				if (encoding && c == '[' && i + 2 < length)
				{
					if (text[i + 1] == '-' && text[i + 2] == ']')
					{
						i += 2;
						goto IL_0201;
					}
					if (i + 7 < length && text[i + 7] == ']')
					{
						i += 7;
						goto IL_0201;
					}
				}
				BMGlyph glyph = this.mFont.GetGlyph((int)c);
				if (glyph != null)
				{
					int num5 = this.mSpacingX + ((num3 == 0) ? glyph.advance : (glyph.advance + glyph.GetKerning(num3)));
					num2 -= num5;
					if (num2 < 0)
					{
						if (flag || !multiline)
						{
							stringBuilder.Append(text.Substring(num4, Mathf.Max(0, i - num4)));
							if (!multiline)
							{
								num4 = i;
								break;
							}
							UIFont.EndLine(ref stringBuilder);
							flag = true;
							if (c == ' ')
							{
								num4 = i + 1;
								num2 = num;
							}
							else
							{
								num4 = i;
								num2 = num - num5;
							}
							num3 = 0;
						}
						else
						{
							while (num4 < length && text[num4] == ' ')
							{
								num4++;
							}
							flag = true;
							num2 = num;
							i = num4 - 1;
							num3 = 0;
							if (!multiline)
							{
								break;
							}
							UIFont.EndLine(ref stringBuilder);
						}
					}
					else
					{
						num3 = (int)c;
					}
				}
			}
			IL_0201:
			i++;
		}
		if (num4 < i)
		{
			stringBuilder.Append(text.Substring(num4, i - num4));
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x00027FA0 File Offset: 0x000261A0
	private void Align(BetterList<Vector3> verts, int indexOffset, UIFont.Alignment alignment, int x, int lineWidth)
	{
		if (alignment != UIFont.Alignment.Left && this.mFont.charSize > 0)
		{
			float num = ((alignment != UIFont.Alignment.Right) ? ((float)(lineWidth - x) * 0.5f) : ((float)(lineWidth - x)));
			num = (float)Mathf.RoundToInt(num);
			if (num < 0f)
			{
				num = 0f;
			}
			num /= (float)this.mFont.charSize;
			for (int i = indexOffset; i < verts.size; i++)
			{
				Vector3 vector = verts.buffer[i];
				vector.x += num;
				verts.buffer[i] = vector;
			}
		}
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x00028038 File Offset: 0x00026238
	public void Print(string text, Color color, BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color> cols, bool encoding, UIFont.Alignment alignment, int lineWidth)
	{
		if (this.mReplacement != null)
		{
			this.mReplacement.Print(text, color, verts, uvs, cols, encoding, alignment, lineWidth);
			return;
		}
		if (this.mFont == null || text == null)
		{
			return;
		}
		if (!this.mFont.isValid)
		{
			Debug.LogError("Attempting to print using an invalid font!");
			return;
		}
		this.mColors.Clear();
		this.mColors.Add(color);
		Vector2 vector = ((this.mFont.charSize <= 0) ? Vector2.one : new Vector2(1f / (float)this.mFont.charSize, 1f / (float)this.mFont.charSize));
		int num = verts.size;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = this.mFont.charSize + this.mSpacingY;
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		Vector2 zero3 = Vector2.zero;
		Vector2 zero4 = Vector2.zero;
		float num7 = this.uvRect.width / (float)this.mFont.texWidth;
		float num8 = this.mUVRect.height / (float)this.mFont.texHeight;
		int i = 0;
		int length = text.Length;
		while (i < length)
		{
			char c = text[i];
			if (c == '\n')
			{
				if (num3 > num2)
				{
					num2 = num3;
				}
				if (alignment != UIFont.Alignment.Left)
				{
					this.Align(verts, num, alignment, num3, lineWidth);
					num = verts.size;
				}
				num3 = 0;
				num4 += num6;
				num5 = 0;
			}
			else if (c < ' ')
			{
				num5 = 0;
			}
			else
			{
				if (encoding && c == '[')
				{
					int num9 = NGUITools.ParseSymbol(text, i, this.mColors);
					if (num9 > 0)
					{
						color = this.mColors[this.mColors.Count - 1];
						i += num9 - 1;
						goto IL_03D1;
					}
				}
				BMGlyph glyph = this.mFont.GetGlyph((int)c);
				if (glyph != null)
				{
					if (num5 != 0)
					{
						num3 += glyph.GetKerning(num5);
					}
					if (c != ' ')
					{
						zero.x = vector.x * (float)(num3 + glyph.offsetX);
						zero.y = (0f - vector.y) * (float)(num4 + glyph.offsetY);
						zero2.x = zero.x + vector.x * (float)glyph.width;
						zero2.y = zero.y - vector.y * (float)glyph.height;
						zero3.x = this.mUVRect.xMin + num7 * (float)glyph.x;
						zero3.y = this.mUVRect.yMax - num8 * (float)glyph.y;
						zero4.x = zero3.x + num7 * (float)glyph.width;
						zero4.y = zero3.y - num8 * (float)glyph.height;
						verts.Add(new Vector3(zero2.x, zero.y));
						verts.Add(new Vector3(zero2.x, zero2.y));
						verts.Add(new Vector3(zero.x, zero2.y));
						verts.Add(new Vector3(zero.x, zero.y));
						uvs.Add(new Vector2(zero4.x, zero3.y));
						uvs.Add(new Vector2(zero4.x, zero4.y));
						uvs.Add(new Vector2(zero3.x, zero4.y));
						uvs.Add(new Vector2(zero3.x, zero3.y));
						cols.Add(color);
						cols.Add(color);
						cols.Add(color);
						cols.Add(color);
					}
					num3 += this.mSpacingX + glyph.advance;
					num5 = (int)c;
				}
			}
			IL_03D1:
			i++;
		}
		if (alignment != UIFont.Alignment.Left && num < verts.size)
		{
			this.Align(verts, num, alignment, num3, lineWidth);
			num = verts.size;
		}
	}

	// Token: 0x0400069C RID: 1692
	[HideInInspector]
	[SerializeField]
	private Material mMat;

	// Token: 0x0400069D RID: 1693
	[SerializeField]
	[HideInInspector]
	private Rect mUVRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x0400069E RID: 1694
	[HideInInspector]
	[SerializeField]
	private BMFont mFont = new BMFont();

	// Token: 0x0400069F RID: 1695
	[HideInInspector]
	[SerializeField]
	private int mSpacingX;

	// Token: 0x040006A0 RID: 1696
	[SerializeField]
	[HideInInspector]
	private int mSpacingY;

	// Token: 0x040006A1 RID: 1697
	[SerializeField]
	[HideInInspector]
	private UIAtlas mAtlas;

	// Token: 0x040006A2 RID: 1698
	[HideInInspector]
	[SerializeField]
	private UIFont mReplacement;

	// Token: 0x040006A3 RID: 1699
	private UIAtlas.Sprite mSprite;

	// Token: 0x040006A4 RID: 1700
	private bool mSpriteSet;

	// Token: 0x040006A5 RID: 1701
	private List<Color> mColors = new List<Color>();

	// Token: 0x0200020A RID: 522
	public enum Alignment
	{
		// Token: 0x04000BEA RID: 3050
		Left,
		// Token: 0x04000BEB RID: 3051
		Center,
		// Token: 0x04000BEC RID: 3052
		Right
	}
}
