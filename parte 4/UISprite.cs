using System;
using UnityEngine;

// Token: 0x02000134 RID: 308
[AddComponentMenu("NGUI/UI/Sprite (Basic)")]
[ExecuteInEditMode]
public class UISprite : UIWidget
{
	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06000919 RID: 2329 RVA: 0x00030FA4 File Offset: 0x0002F1A4
	public Rect outerUV
	{
		get
		{
			this.UpdateUVs(false);
			return this.mOuterUV;
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x0600091A RID: 2330 RVA: 0x00030FB3 File Offset: 0x0002F1B3
	// (set) Token: 0x0600091B RID: 2331 RVA: 0x00030FBC File Offset: 0x0002F1BC
	public UIAtlas atlas
	{
		get
		{
			return this.mAtlas;
		}
		set
		{
			if (this.mAtlas != value)
			{
				this.mAtlas = value;
				this.mSpriteSet = false;
				this.mSprite = null;
				this.material = ((!(this.mAtlas != null)) ? null : this.mAtlas.spriteMaterial);
				if (string.IsNullOrEmpty(this.mSpriteName) && this.mAtlas != null && this.mAtlas.spriteList.Count > 0)
				{
					this.sprite = this.mAtlas.spriteList[0];
					this.mSpriteName = this.mSprite.name;
				}
				if (!string.IsNullOrEmpty(this.mSpriteName))
				{
					string text = this.mSpriteName;
					this.mSpriteName = string.Empty;
					this.spriteName = text;
					this.mChanged = true;
					this.UpdateUVs(true);
				}
			}
		}
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x0600091C RID: 2332 RVA: 0x0003109B File Offset: 0x0002F29B
	// (set) Token: 0x0600091D RID: 2333 RVA: 0x000310A4 File Offset: 0x0002F2A4
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(this.mSpriteName))
				{
					this.mSpriteName = string.Empty;
					this.mSprite = null;
					this.mChanged = true;
					return;
				}
			}
			else if (this.mSpriteName != value)
			{
				this.mSpriteName = value;
				this.mSprite = null;
				this.mChanged = true;
				if (this.mSprite != null)
				{
					this.UpdateUVs(true);
				}
			}
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x0600091E RID: 2334 RVA: 0x00031114 File Offset: 0x0002F314
	// (set) Token: 0x0600091F RID: 2335 RVA: 0x000311D2 File Offset: 0x0002F3D2
	public UIAtlas.Sprite sprite
	{
		get
		{
			if (!this.mSpriteSet)
			{
				this.mSprite = null;
			}
			if (this.mSprite == null && this.mAtlas != null)
			{
				if (!string.IsNullOrEmpty(this.mSpriteName))
				{
					this.sprite = this.mAtlas.GetSprite(this.mSpriteName);
				}
				if (this.mSprite == null && this.mAtlas.spriteList.Count > 0)
				{
					this.sprite = this.mAtlas.spriteList[0];
					this.mSpriteName = this.mSprite.name;
				}
				if (this.mSprite != null)
				{
					this.material = this.mAtlas.spriteMaterial;
				}
			}
			return this.mSprite;
		}
		set
		{
			this.mSprite = value;
			this.mSpriteSet = true;
			this.material = ((this.mSprite == null || !(this.mAtlas != null)) ? null : this.mAtlas.spriteMaterial);
		}
	}

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x06000920 RID: 2336 RVA: 0x0003120C File Offset: 0x0002F40C
	public override Vector2 pivotOffset
	{
		get
		{
			Vector2 zero = Vector2.zero;
			if (this.sprite != null)
			{
				UIWidget.Pivot pivot = base.pivot;
				switch (pivot)
				{
				case UIWidget.Pivot.Top:
				case UIWidget.Pivot.Center:
				case UIWidget.Pivot.Bottom:
					zero.x = (-1f - this.mSprite.paddingRight + this.mSprite.paddingLeft) * 0.5f;
					goto IL_009A;
				case UIWidget.Pivot.TopRight:
				case UIWidget.Pivot.Right:
				case UIWidget.Pivot.BottomRight:
					zero.x = -1f - this.mSprite.paddingRight;
					goto IL_009A;
				}
				zero.x = this.mSprite.paddingLeft;
				IL_009A:
				if (pivot - UIWidget.Pivot.Left > 2)
				{
					if (pivot - UIWidget.Pivot.BottomLeft > 2)
					{
						zero.y = 0f - this.mSprite.paddingTop;
					}
					else
					{
						zero.y = 1f + this.mSprite.paddingBottom;
					}
				}
				else
				{
					zero.y = (1f + this.mSprite.paddingBottom - this.mSprite.paddingTop) * 0.5f;
				}
			}
			return zero;
		}
	}

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06000921 RID: 2337 RVA: 0x00031320 File Offset: 0x0002F520
	public override Material material
	{
		get
		{
			Material material = base.material;
			if (material == null)
			{
				material = ((!(this.mAtlas != null)) ? null : this.mAtlas.spriteMaterial);
				this.mSprite = null;
				this.material = material;
				if (material != null)
				{
					this.UpdateUVs(true);
				}
			}
			return material;
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06000922 RID: 2338 RVA: 0x00031379 File Offset: 0x0002F579
	public virtual Vector4 border
	{
		get
		{
			return Vector4.zero;
		}
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x00031380 File Offset: 0x0002F580
	public virtual void UpdateUVs(bool force)
	{
		if (this.sprite == null || (!force && !(this.mOuter != this.mSprite.outer)))
		{
			return;
		}
		Texture mainTexture = base.mainTexture;
		if (mainTexture != null)
		{
			this.mOuter = this.mSprite.outer;
			this.mOuterUV = this.mOuter;
			if (this.mAtlas.coordinates == UIAtlas.Coordinates.Pixels)
			{
				this.mOuterUV = NGUIMath.ConvertToTexCoords(this.mOuterUV, mainTexture.width, mainTexture.height);
			}
			this.mChanged = true;
		}
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x00031410 File Offset: 0x0002F610
	public override void MakePixelPerfect()
	{
		if (this.sprite != null)
		{
			Texture mainTexture = base.mainTexture;
			Vector3 localScale = base.cachedTransform.localScale;
			if (mainTexture != null)
			{
				Rect rect = NGUIMath.ConvertToPixels(this.outerUV, mainTexture.width, mainTexture.height, true);
				float pixelSize = this.atlas.pixelSize;
				localScale.x = (float)Mathf.RoundToInt(rect.width * pixelSize);
				localScale.y = (float)Mathf.RoundToInt(rect.height * pixelSize);
				localScale.z = 1f;
				base.cachedTransform.localScale = localScale;
			}
			int num = Mathf.RoundToInt(localScale.x * (1f + this.mSprite.paddingLeft + this.mSprite.paddingRight));
			int num2 = Mathf.RoundToInt(localScale.y * (1f + this.mSprite.paddingTop + this.mSprite.paddingBottom));
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			if (num % 2 == 1 && (base.pivot == UIWidget.Pivot.Top || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Bottom))
			{
				localPosition.x = Mathf.Floor(localPosition.x) + 0.5f;
			}
			else
			{
				localPosition.x = Mathf.Round(localPosition.x);
			}
			if (num2 % 2 == 1 && (base.pivot == UIWidget.Pivot.Left || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Right))
			{
				localPosition.y = Mathf.Ceil(localPosition.y) - 0.5f;
			}
			else
			{
				localPosition.y = Mathf.Round(localPosition.y);
			}
			base.cachedTransform.localPosition = localPosition;
		}
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x000315C8 File Offset: 0x0002F7C8
	protected override void OnStart()
	{
		if (this.mAtlas != null)
		{
			this.UpdateUVs(true);
		}
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x000315DF File Offset: 0x0002F7DF
	public override bool OnUpdate()
	{
		if (this.mLastName != this.mSpriteName)
		{
			this.mSprite = null;
			this.mChanged = true;
			this.mLastName = this.mSpriteName;
			this.UpdateUVs(false);
			return true;
		}
		this.UpdateUVs(false);
		return false;
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x00031620 File Offset: 0x0002F820
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color> cols)
	{
		Vector2 vector = new Vector2(this.mOuterUV.xMin, this.mOuterUV.yMin);
		Vector2 vector2 = new Vector2(this.mOuterUV.xMax, this.mOuterUV.yMax);
		verts.Add(new Vector3(1f, 0f, 0f));
		verts.Add(new Vector3(1f, -1f, 0f));
		verts.Add(new Vector3(0f, -1f, 0f));
		verts.Add(new Vector3(0f, 0f, 0f));
		uvs.Add(vector2);
		uvs.Add(new Vector2(vector2.x, vector.y));
		uvs.Add(vector);
		uvs.Add(new Vector2(vector.x, vector2.y));
		cols.Add(base.color);
		cols.Add(base.color);
		cols.Add(base.color);
		cols.Add(base.color);
	}

	// Token: 0x040007F0 RID: 2032
	[SerializeField]
	[HideInInspector]
	private UIAtlas mAtlas;

	// Token: 0x040007F1 RID: 2033
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x040007F2 RID: 2034
	protected UIAtlas.Sprite mSprite;

	// Token: 0x040007F3 RID: 2035
	protected Rect mOuter;

	// Token: 0x040007F4 RID: 2036
	protected Rect mOuterUV;

	// Token: 0x040007F5 RID: 2037
	private bool mSpriteSet;

	// Token: 0x040007F6 RID: 2038
	private string mLastName = string.Empty;
}
