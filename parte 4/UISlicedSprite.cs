using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
[AddComponentMenu("NGUI/UI/Sprite (Sliced)")]
[ExecuteInEditMode]
public class UISlicedSprite : UISprite
{
	// Token: 0x170000FB RID: 251
	// (get) Token: 0x060008EF RID: 2287 RVA: 0x0002FC71 File Offset: 0x0002DE71
	public Rect innerUV
	{
		get
		{
			this.UpdateUVs(false);
			return this.mInnerUV;
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0002FC80 File Offset: 0x0002DE80
	// (set) Token: 0x060008F1 RID: 2289 RVA: 0x0002FC88 File Offset: 0x0002DE88
	public bool fillCenter
	{
		get
		{
			return this.mFillCenter;
		}
		set
		{
			if (this.mFillCenter != value)
			{
				this.mFillCenter = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0002FCA0 File Offset: 0x0002DEA0
	public override Vector4 border
	{
		get
		{
			UIAtlas.Sprite sprite = base.sprite;
			if (sprite == null)
			{
				return Vector2.zero;
			}
			Rect rect = sprite.outer;
			Rect rect2 = sprite.inner;
			Texture mainTexture = base.mainTexture;
			if (base.atlas.coordinates == UIAtlas.Coordinates.TexCoords && mainTexture != null)
			{
				rect = NGUIMath.ConvertToPixels(rect, mainTexture.width, mainTexture.height, true);
				rect2 = NGUIMath.ConvertToPixels(rect2, mainTexture.width, mainTexture.height, true);
			}
			return new Vector4(rect2.xMin - rect.xMin, rect2.yMin - rect.yMin, rect.xMax - rect2.xMax, rect.yMax - rect2.yMax) * base.atlas.pixelSize;
		}
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x0002FD68 File Offset: 0x0002DF68
	public override void UpdateUVs(bool force)
	{
		if (base.cachedTransform.localScale != this.mScale)
		{
			this.mScale = base.cachedTransform.localScale;
			this.mChanged = true;
		}
		if (base.sprite == null || (!force && !(this.mInner != this.mSprite.inner) && !(this.mOuter != this.mSprite.outer)))
		{
			return;
		}
		Texture mainTexture = base.mainTexture;
		if (mainTexture != null)
		{
			this.mInner = this.mSprite.inner;
			this.mOuter = this.mSprite.outer;
			this.mInnerUV = this.mInner;
			this.mOuterUV = this.mOuter;
			if (base.atlas.coordinates == UIAtlas.Coordinates.Pixels)
			{
				this.mOuterUV = NGUIMath.ConvertToTexCoords(this.mOuterUV, mainTexture.width, mainTexture.height);
				this.mInnerUV = NGUIMath.ConvertToTexCoords(this.mInnerUV, mainTexture.width, mainTexture.height);
			}
		}
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x0002FE78 File Offset: 0x0002E078
	public override void MakePixelPerfect()
	{
		Vector3 localPosition = base.cachedTransform.localPosition;
		localPosition.x = (float)Mathf.RoundToInt(localPosition.x);
		localPosition.y = (float)Mathf.RoundToInt(localPosition.y);
		localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
		base.cachedTransform.localPosition = localPosition;
		Vector3 localScale = base.cachedTransform.localScale;
		localScale.x = (float)(Mathf.RoundToInt(localScale.x * 0.5f) << 1);
		localScale.y = (float)(Mathf.RoundToInt(localScale.y * 0.5f) << 1);
		localScale.z = 1f;
		base.cachedTransform.localScale = localScale;
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x0002FF30 File Offset: 0x0002E130
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color> cols)
	{
		if (this.mOuterUV == this.mInnerUV)
		{
			base.OnFill(verts, uvs, cols);
			return;
		}
		Vector2[] array = new Vector2[4];
		Vector2[] array2 = new Vector2[4];
		Texture mainTexture = base.mainTexture;
		array[0] = Vector2.zero;
		array[1] = Vector2.zero;
		array[2] = new Vector2(1f, -1f);
		array[3] = new Vector2(1f, -1f);
		if (mainTexture != null)
		{
			float pixelSize = base.atlas.pixelSize;
			float num = (this.mInnerUV.xMin - this.mOuterUV.xMin) * pixelSize;
			float num2 = (this.mOuterUV.xMax - this.mInnerUV.xMax) * pixelSize;
			float num3 = (this.mInnerUV.yMax - this.mOuterUV.yMax) * pixelSize;
			float num4 = (this.mOuterUV.yMin - this.mInnerUV.yMin) * pixelSize;
			Vector3 localScale = base.cachedTransform.localScale;
			localScale.x = Mathf.Max(0f, localScale.x);
			localScale.y = Mathf.Max(0f, localScale.y);
			Vector2 vector = new Vector2(localScale.x / (float)mainTexture.width, localScale.y / (float)mainTexture.height);
			Vector2 vector2 = new Vector2(num / vector.x, num3 / vector.y);
			Vector2 vector3 = new Vector2(num2 / vector.x, num4 / vector.y);
			UIWidget.Pivot pivot = base.pivot;
			if (pivot == UIWidget.Pivot.Right || pivot == UIWidget.Pivot.TopRight || pivot == UIWidget.Pivot.BottomRight)
			{
				array[0].x = Mathf.Min(0f, 1f - (vector3.x + vector2.x));
				array[1].x = array[0].x + vector2.x;
				array[2].x = array[0].x + Mathf.Max(vector2.x, 1f - vector3.x);
				array[3].x = array[0].x + Mathf.Max(vector2.x + vector3.x, 1f);
			}
			else
			{
				array[1].x = vector2.x;
				array[2].x = Mathf.Max(vector2.x, 1f - vector3.x);
				array[3].x = Mathf.Max(vector2.x + vector3.x, 1f);
			}
			if (pivot == UIWidget.Pivot.Bottom || pivot == UIWidget.Pivot.BottomLeft || pivot == UIWidget.Pivot.BottomRight)
			{
				array[0].y = Mathf.Max(0f, -1f - (vector3.y + vector2.y));
				array[1].y = array[0].y + vector2.y;
				array[2].y = array[0].y + Mathf.Min(vector2.y, -1f - vector3.y);
				array[3].y = array[0].y + Mathf.Min(vector2.y + vector3.y, -1f);
			}
			else
			{
				array[1].y = vector2.y;
				array[2].y = Mathf.Min(vector2.y, -1f - vector3.y);
				array[3].y = Mathf.Min(vector2.y + vector3.y, -1f);
			}
			array2[0] = new Vector2(this.mOuterUV.xMin, this.mOuterUV.yMax);
			array2[1] = new Vector2(this.mInnerUV.xMin, this.mInnerUV.yMax);
			array2[2] = new Vector2(this.mInnerUV.xMax, this.mInnerUV.yMin);
			array2[3] = new Vector2(this.mOuterUV.xMax, this.mOuterUV.yMin);
		}
		else
		{
			for (int i = 0; i < 4; i++)
			{
				array2[i] = Vector2.zero;
			}
		}
		for (int j = 0; j < 3; j++)
		{
			int num5 = j + 1;
			for (int k = 0; k < 3; k++)
			{
				if (this.mFillCenter || j != 1 || k != 1)
				{
					int num6 = k + 1;
					verts.Add(new Vector3(array[num5].x, array[k].y, 0f));
					verts.Add(new Vector3(array[num5].x, array[num6].y, 0f));
					verts.Add(new Vector3(array[j].x, array[num6].y, 0f));
					verts.Add(new Vector3(array[j].x, array[k].y, 0f));
					uvs.Add(new Vector2(array2[num5].x, array2[k].y));
					uvs.Add(new Vector2(array2[num5].x, array2[num6].y));
					uvs.Add(new Vector2(array2[j].x, array2[num6].y));
					uvs.Add(new Vector2(array2[j].x, array2[k].y));
					cols.Add(base.color);
					cols.Add(base.color);
					cols.Add(base.color);
					cols.Add(base.color);
				}
			}
		}
	}

	// Token: 0x040007CC RID: 1996
	[HideInInspector]
	[SerializeField]
	private bool mFillCenter = true;

	// Token: 0x040007CD RID: 1997
	protected Rect mInner;

	// Token: 0x040007CE RID: 1998
	protected Rect mInnerUV;

	// Token: 0x040007CF RID: 1999
	protected Vector3 mScale = Vector3.one;
}
