using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E9 RID: 233
[AddComponentMenu("NGUI/UI/Atlas")]
public class UIAtlas : MonoBehaviour
{
	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x0600069A RID: 1690 RVA: 0x00020C0F File Offset: 0x0001EE0F
	// (set) Token: 0x0600069B RID: 1691 RVA: 0x00020C34 File Offset: 0x0001EE34
	public Material spriteMaterial
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.spriteMaterial;
			}
			return this.material;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteMaterial = value;
				return;
			}
			if (this.material == null)
			{
				this.material = value;
				return;
			}
			this.MarkAsDirty();
			this.material = value;
			this.MarkAsDirty();
		}
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x0600069C RID: 1692 RVA: 0x00020C85 File Offset: 0x0001EE85
	// (set) Token: 0x0600069D RID: 1693 RVA: 0x00020CA7 File Offset: 0x0001EEA7
	public List<UIAtlas.Sprite> spriteList
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.spriteList;
			}
			return this.sprites;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteList = value;
				return;
			}
			this.sprites = value;
		}
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x0600069E RID: 1694 RVA: 0x00020CCB File Offset: 0x0001EECB
	public Texture texture
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.texture;
			}
			if (this.material != null)
			{
				return this.material.mainTexture;
			}
			return null;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x0600069F RID: 1695 RVA: 0x00020D02 File Offset: 0x0001EF02
	// (set) Token: 0x060006A0 RID: 1696 RVA: 0x00020D24 File Offset: 0x0001EF24
	public UIAtlas.Coordinates coordinates
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.coordinates;
			}
			return this.mCoordinates;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.coordinates = value;
				return;
			}
			if (this.mCoordinates == value)
			{
				return;
			}
			if (this.material == null || this.material.mainTexture == null)
			{
				Debug.LogError("Can't switch coordinates until the atlas material has a valid texture");
				return;
			}
			this.mCoordinates = value;
			Texture mainTexture = this.material.mainTexture;
			int i = 0;
			int count = this.sprites.Count;
			while (i < count)
			{
				UIAtlas.Sprite sprite = this.sprites[i];
				if (this.mCoordinates == UIAtlas.Coordinates.TexCoords)
				{
					sprite.outer = NGUIMath.ConvertToTexCoords(sprite.outer, mainTexture.width, mainTexture.height);
					sprite.inner = NGUIMath.ConvertToTexCoords(sprite.inner, mainTexture.width, mainTexture.height);
				}
				else
				{
					sprite.outer = NGUIMath.ConvertToPixels(sprite.outer, mainTexture.width, mainTexture.height, true);
					sprite.inner = NGUIMath.ConvertToPixels(sprite.inner, mainTexture.width, mainTexture.height, true);
				}
				i++;
			}
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00020E41 File Offset: 0x0001F041
	// (set) Token: 0x060006A2 RID: 1698 RVA: 0x00020E64 File Offset: 0x0001F064
	public float pixelSize
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.pixelSize;
			}
			return this.mPixelSize;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.pixelSize = value;
				return;
			}
			float num = Mathf.Clamp(value, 0.25f, 4f);
			if (this.mPixelSize != num)
			{
				this.mPixelSize = num;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00020EB3 File Offset: 0x0001F0B3
	// (set) Token: 0x060006A4 RID: 1700 RVA: 0x00020EBC File Offset: 0x0001F0BC
	public UIAtlas replacement
	{
		get
		{
			return this.mReplacement;
		}
		set
		{
			UIAtlas uiatlas = value;
			if (uiatlas == this)
			{
				uiatlas = null;
			}
			if (this.mReplacement != uiatlas)
			{
				if (uiatlas != null && uiatlas.replacement == this)
				{
					uiatlas.replacement = null;
				}
				if (this.mReplacement != null)
				{
					this.MarkAsDirty();
				}
				this.mReplacement = uiatlas;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x00020F24 File Offset: 0x0001F124
	public UIAtlas.Sprite GetSprite(string name)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetSprite(name);
		}
		if (!string.IsNullOrEmpty(name))
		{
			int i = 0;
			int count = this.sprites.Count;
			while (i < count)
			{
				UIAtlas.Sprite sprite = this.sprites[i];
				if (!string.IsNullOrEmpty(sprite.name) && name == sprite.name)
				{
					return sprite;
				}
				i++;
			}
		}
		else
		{
			Debug.LogWarning("Expected a valid name, found nothing");
		}
		return null;
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x00020FA4 File Offset: 0x0001F1A4
	public List<string> GetListOfSprites()
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetListOfSprites();
		}
		List<string> list = new List<string>();
		int i = 0;
		int count = this.sprites.Count;
		while (i < count)
		{
			UIAtlas.Sprite sprite = this.sprites[i];
			if (sprite != null && !string.IsNullOrEmpty(sprite.name))
			{
				list.Add(sprite.name);
			}
			i++;
		}
		list.Sort();
		return list;
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x00021019 File Offset: 0x0001F219
	private bool References(UIAtlas atlas)
	{
		return !(atlas == null) && (atlas == this || (this.mReplacement != null && this.mReplacement.References(atlas)));
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x0002104D File Offset: 0x0001F24D
	public static bool CheckIfRelated(UIAtlas a, UIAtlas b)
	{
		return !(a == null) && !(b == null) && (a == b || a.References(b) || b.References(a));
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x00021080 File Offset: 0x0001F280
	public void MarkAsDirty()
	{
		UISprite[] array = NGUITools.FindActive<UISprite>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UISprite uisprite = array[i];
			if (UIAtlas.CheckIfRelated(this, uisprite.atlas))
			{
				UIAtlas atlas = uisprite.atlas;
				uisprite.atlas = null;
				uisprite.atlas = atlas;
			}
			i++;
		}
		UIFont[] array2 = Resources.FindObjectsOfTypeAll(typeof(UIFont)) as UIFont[];
		int j = 0;
		int num2 = array2.Length;
		while (j < num2)
		{
			UIFont uifont = array2[j];
			if (UIAtlas.CheckIfRelated(this, uifont.atlas))
			{
				UIAtlas atlas2 = uifont.atlas;
				uifont.atlas = null;
				uifont.atlas = atlas2;
			}
			j++;
		}
		UILabel[] array3 = NGUITools.FindActive<UILabel>();
		int k = 0;
		int num3 = array3.Length;
		while (k < num3)
		{
			UILabel uilabel = array3[k];
			if (uilabel.font != null && UIAtlas.CheckIfRelated(this, uilabel.font.atlas))
			{
				UIFont font = uilabel.font;
				uilabel.font = null;
				uilabel.font = font;
			}
			k++;
		}
	}

	// Token: 0x040005AB RID: 1451
	[SerializeField]
	[HideInInspector]
	private Material material;

	// Token: 0x040005AC RID: 1452
	[HideInInspector]
	[SerializeField]
	private List<UIAtlas.Sprite> sprites = new List<UIAtlas.Sprite>();

	// Token: 0x040005AD RID: 1453
	[SerializeField]
	[HideInInspector]
	private UIAtlas.Coordinates mCoordinates;

	// Token: 0x040005AE RID: 1454
	[SerializeField]
	[HideInInspector]
	private float mPixelSize = 1f;

	// Token: 0x040005AF RID: 1455
	[HideInInspector]
	[SerializeField]
	private UIAtlas mReplacement;

	// Token: 0x020001F5 RID: 501
	[Serializable]
	public class Sprite
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0003E708 File Offset: 0x0003C908
		public bool hasPadding
		{
			get
			{
				return this.paddingLeft != 0f || this.paddingRight != 0f || this.paddingTop != 0f || this.paddingBottom != 0f;
			}
		}

		// Token: 0x04000B9D RID: 2973
		public string name = "Unity Bug";

		// Token: 0x04000B9E RID: 2974
		public Rect outer = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000B9F RID: 2975
		public Rect inner = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000BA0 RID: 2976
		public float paddingLeft;

		// Token: 0x04000BA1 RID: 2977
		public float paddingRight;

		// Token: 0x04000BA2 RID: 2978
		public float paddingTop;

		// Token: 0x04000BA3 RID: 2979
		public float paddingBottom;
	}

	// Token: 0x020001F6 RID: 502
	public enum Coordinates
	{
		// Token: 0x04000BA5 RID: 2981
		Pixels,
		// Token: 0x04000BA6 RID: 2982
		TexCoords
	}
}
