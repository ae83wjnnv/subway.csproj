using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001D RID: 29
[Serializable]
public class BMFont
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000174 RID: 372 RVA: 0x00005C67 File Offset: 0x00003E67
	public bool isValid
	{
		get
		{
			return this.mSaved.Count > 0 || this.LegacyCheck();
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000175 RID: 373 RVA: 0x00005C7F File Offset: 0x00003E7F
	// (set) Token: 0x06000176 RID: 374 RVA: 0x00005C87 File Offset: 0x00003E87
	public int charSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			this.mSize = value;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000177 RID: 375 RVA: 0x00005C90 File Offset: 0x00003E90
	// (set) Token: 0x06000178 RID: 376 RVA: 0x00005C98 File Offset: 0x00003E98
	public int baseOffset
	{
		get
		{
			return this.mBase;
		}
		set
		{
			this.mBase = value;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000179 RID: 377 RVA: 0x00005CA1 File Offset: 0x00003EA1
	// (set) Token: 0x0600017A RID: 378 RVA: 0x00005CA9 File Offset: 0x00003EA9
	public int texWidth
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			this.mWidth = value;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600017B RID: 379 RVA: 0x00005CB2 File Offset: 0x00003EB2
	// (set) Token: 0x0600017C RID: 380 RVA: 0x00005CBA File Offset: 0x00003EBA
	public int texHeight
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			this.mHeight = value;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600017D RID: 381 RVA: 0x00005CC3 File Offset: 0x00003EC3
	public int glyphCount
	{
		get
		{
			if (!this.isValid)
			{
				return 0;
			}
			return this.mSaved.Count;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600017E RID: 382 RVA: 0x00005CDA File Offset: 0x00003EDA
	// (set) Token: 0x0600017F RID: 383 RVA: 0x00005CE2 File Offset: 0x00003EE2
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			this.mSpriteName = value;
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00005CEC File Offset: 0x00003EEC
	public bool LegacyCheck()
	{
		if (this.mGlyphs != null && this.mGlyphs.Length != 0)
		{
			int i = 0;
			int num = this.mGlyphs.Length;
			while (i < num)
			{
				BMGlyph bmglyph = this.mGlyphs[i];
				if (bmglyph != null)
				{
					bmglyph.index = i;
					this.mSaved.Add(bmglyph);
					this.mDict.Add(i, bmglyph);
				}
				i++;
			}
			this.mGlyphs = null;
			return true;
		}
		return false;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x00005D55 File Offset: 0x00003F55
	private int GetArraySize(int index)
	{
		if (index < 256)
		{
			return 256;
		}
		if (index < 65536)
		{
			return 65536;
		}
		if (index < 262144)
		{
			return 262144;
		}
		return 0;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00005D84 File Offset: 0x00003F84
	public BMGlyph GetGlyph(int index, bool createIfMissing)
	{
		BMGlyph bmglyph = null;
		if (this.mDict.Count == 0)
		{
			if (this.mSaved.Count == 0)
			{
				this.LegacyCheck();
			}
			else
			{
				int i = 0;
				int count = this.mSaved.Count;
				while (i < count)
				{
					BMGlyph bmglyph2 = this.mSaved[i];
					this.mDict.Add(bmglyph2.index, bmglyph2);
					i++;
				}
			}
		}
		if (!this.mDict.TryGetValue(index, out bmglyph) && createIfMissing)
		{
			bmglyph = new BMGlyph();
			bmglyph.index = index;
			this.mSaved.Add(bmglyph);
			this.mDict.Add(index, bmglyph);
		}
		return bmglyph;
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00005E29 File Offset: 0x00004029
	public BMGlyph GetGlyph(int index)
	{
		return this.GetGlyph(index, false);
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00005E33 File Offset: 0x00004033
	public void Clear()
	{
		this.mGlyphs = null;
		this.mDict.Clear();
		this.mSaved.Clear();
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00005E54 File Offset: 0x00004054
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		if (!this.isValid)
		{
			return;
		}
		int i = 0;
		int count = this.mSaved.Count;
		while (i < count)
		{
			BMGlyph bmglyph = this.mSaved[i];
			if (bmglyph != null)
			{
				bmglyph.Trim(xMin, yMin, xMax, yMax);
			}
			i++;
		}
	}

	// Token: 0x040000C0 RID: 192
	[SerializeField]
	[HideInInspector]
	private BMGlyph[] mGlyphs;

	// Token: 0x040000C1 RID: 193
	[SerializeField]
	[HideInInspector]
	private int mSize;

	// Token: 0x040000C2 RID: 194
	[SerializeField]
	[HideInInspector]
	private int mBase;

	// Token: 0x040000C3 RID: 195
	[SerializeField]
	[HideInInspector]
	private int mWidth;

	// Token: 0x040000C4 RID: 196
	[HideInInspector]
	[SerializeField]
	private int mHeight;

	// Token: 0x040000C5 RID: 197
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x040000C6 RID: 198
	[HideInInspector]
	[SerializeField]
	private List<BMGlyph> mSaved = new List<BMGlyph>();

	// Token: 0x040000C7 RID: 199
	private Dictionary<int, BMGlyph> mDict = new Dictionary<int, BMGlyph>();
}
