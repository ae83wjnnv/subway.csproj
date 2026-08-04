using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000135 RID: 309
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/UI/Sprite Animation")]
[ExecuteInEditMode]
public class UISpriteAnimation : MonoBehaviour
{
	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06000929 RID: 2345 RVA: 0x0003174E File Offset: 0x0002F94E
	// (set) Token: 0x0600092A RID: 2346 RVA: 0x00031756 File Offset: 0x0002F956
	public int framesPerSecond
	{
		get
		{
			return this.mFPS;
		}
		set
		{
			this.mFPS = value;
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x0600092B RID: 2347 RVA: 0x0003175F File Offset: 0x0002F95F
	// (set) Token: 0x0600092C RID: 2348 RVA: 0x00031767 File Offset: 0x0002F967
	public string namePrefix
	{
		get
		{
			return this.mPrefix;
		}
		set
		{
			if (this.mPrefix != value)
			{
				this.mPrefix = value;
				this.RebuildSpriteList();
			}
		}
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x00031784 File Offset: 0x0002F984
	private void Start()
	{
		this.RebuildSpriteList();
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x0003178C File Offset: 0x0002F98C
	private void Update()
	{
		if (this.mSpriteNames.Count <= 1 || !Application.isPlaying)
		{
			return;
		}
		this.mDelta += Time.deltaTime;
		float num = (((float)this.mFPS <= 0f) ? 0f : (1f / (float)this.mFPS));
		if (num < this.mDelta)
		{
			this.mDelta = ((num <= 0f) ? 0f : (this.mDelta - num));
			int num2 = this.mIndex + 1;
			this.mIndex = num2;
			if (num2 >= this.mSpriteNames.Count)
			{
				this.mIndex = 0;
			}
			this.mSprite.spriteName = this.mSpriteNames[this.mIndex];
			this.mSprite.MakePixelPerfect();
		}
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x00031858 File Offset: 0x0002FA58
	private void RebuildSpriteList()
	{
		if (this.mSprite == null)
		{
			this.mSprite = base.GetComponent<UISprite>();
		}
		this.mSpriteNames.Clear();
		if (!(this.mSprite != null) || !(this.mSprite.atlas != null))
		{
			return;
		}
		List<UIAtlas.Sprite> spriteList = this.mSprite.atlas.spriteList;
		int i = 0;
		int count = spriteList.Count;
		while (i < count)
		{
			UIAtlas.Sprite sprite = spriteList[i];
			if (string.IsNullOrEmpty(this.mPrefix) || sprite.name.StartsWith(this.mPrefix))
			{
				this.mSpriteNames.Add(sprite.name);
			}
			i++;
		}
		this.mSpriteNames.Sort();
	}

	// Token: 0x040007F7 RID: 2039
	[HideInInspector]
	[SerializeField]
	private int mFPS = 30;

	// Token: 0x040007F8 RID: 2040
	[HideInInspector]
	[SerializeField]
	private string mPrefix = string.Empty;

	// Token: 0x040007F9 RID: 2041
	private UISprite mSprite;

	// Token: 0x040007FA RID: 2042
	private float mDelta;

	// Token: 0x040007FB RID: 2043
	private int mIndex;

	// Token: 0x040007FC RID: 2044
	private List<string> mSpriteNames = new List<string>();
}
