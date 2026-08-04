using System;
using System.Collections.Generic;

// Token: 0x0200001E RID: 30
[Serializable]
public class BMGlyph
{
	// Token: 0x06000187 RID: 391 RVA: 0x00005EBC File Offset: 0x000040BC
	public int GetKerning(int previousChar)
	{
		if (this.kerning != null)
		{
			int i = 0;
			int count = this.kerning.Count;
			while (i < count)
			{
				BMGlyph.Kerning kerning = this.kerning[i];
				if (kerning.previousChar == previousChar)
				{
					return kerning.amount;
				}
				i++;
			}
		}
		return 0;
	}

	// Token: 0x06000188 RID: 392 RVA: 0x00005F08 File Offset: 0x00004108
	public void SetKerning(int previousChar, int amount)
	{
		if (this.kerning == null)
		{
			this.kerning = new List<BMGlyph.Kerning>();
		}
		for (int i = 0; i < this.kerning.Count; i++)
		{
			if (this.kerning[i].previousChar == previousChar)
			{
				BMGlyph.Kerning kerning = this.kerning[i];
				kerning.amount = amount;
				this.kerning[i] = kerning;
				return;
			}
		}
		BMGlyph.Kerning kerning2 = default(BMGlyph.Kerning);
		kerning2.previousChar = previousChar;
		kerning2.amount = amount;
		this.kerning.Add(kerning2);
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00005F9C File Offset: 0x0000419C
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		int num = this.x + this.width;
		int num2 = this.y + this.height;
		if (this.x < xMin)
		{
			int num3 = xMin - this.x;
			this.x += num3;
			this.width -= num3;
			this.offsetX += num3;
		}
		if (this.y < yMin)
		{
			int num4 = yMin - this.y;
			this.y += num4;
			this.height -= num4;
			this.offsetY += num4;
		}
		if (num > xMax)
		{
			this.width -= num - xMax;
		}
		if (num2 > yMax)
		{
			this.height -= num2 - yMax;
		}
	}

	// Token: 0x040000C8 RID: 200
	public int index;

	// Token: 0x040000C9 RID: 201
	public int x;

	// Token: 0x040000CA RID: 202
	public int y;

	// Token: 0x040000CB RID: 203
	public int width;

	// Token: 0x040000CC RID: 204
	public int height;

	// Token: 0x040000CD RID: 205
	public int offsetX;

	// Token: 0x040000CE RID: 206
	public int offsetY;

	// Token: 0x040000CF RID: 207
	public int advance;

	// Token: 0x040000D0 RID: 208
	public List<BMGlyph.Kerning> kerning;

	// Token: 0x02000161 RID: 353
	public struct Kerning
	{
		// Token: 0x040008BE RID: 2238
		public int previousChar;

		// Token: 0x040008BF RID: 2239
		public int amount;
	}
}
