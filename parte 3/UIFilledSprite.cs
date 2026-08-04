using System;
using UnityEngine;

// Token: 0x0200010A RID: 266
[AddComponentMenu("NGUI/UI/Sprite (Filled)")]
[ExecuteInEditMode]
public class UIFilledSprite : UISprite
{
	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06000795 RID: 1941 RVA: 0x00026525 File Offset: 0x00024725
	// (set) Token: 0x06000796 RID: 1942 RVA: 0x0002652D File Offset: 0x0002472D
	public UIFilledSprite.FillDirection fillDirection
	{
		get
		{
			return this.mFillDirection;
		}
		set
		{
			if (this.mFillDirection != value)
			{
				this.mFillDirection = value;
				this.mChanged = true;
			}
		}
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x06000797 RID: 1943 RVA: 0x00026546 File Offset: 0x00024746
	// (set) Token: 0x06000798 RID: 1944 RVA: 0x00026550 File Offset: 0x00024750
	public float fillAmount
	{
		get
		{
			return this.mFillAmount;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mFillAmount != num)
			{
				this.mFillAmount = num;
				this.mChanged = true;
			}
		}
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000799 RID: 1945 RVA: 0x0002657B File Offset: 0x0002477B
	// (set) Token: 0x0600079A RID: 1946 RVA: 0x00026583 File Offset: 0x00024783
	public bool invert
	{
		get
		{
			return this.mInvert;
		}
		set
		{
			if (this.mInvert != value)
			{
				this.mInvert = value;
				this.mChanged = true;
			}
		}
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x0002659C File Offset: 0x0002479C
	private bool AdjustRadial(Vector2[] xy, Vector2[] uv, float fill, bool invert)
	{
		if (fill < 0.001f)
		{
			return false;
		}
		if (!invert && fill > 0.999f)
		{
			return true;
		}
		float num = Mathf.Clamp01(fill);
		if (!invert)
		{
			num = 1f - num;
		}
		num *= 1.5707964f;
		float num2 = Mathf.Sin(num);
		float num3 = Mathf.Cos(num);
		if (num2 > num3)
		{
			num3 *= 1f / num2;
			num2 = 1f;
			if (!invert)
			{
				xy[0].y = Mathf.Lerp(xy[2].y, xy[0].y, num3);
				xy[3].y = xy[0].y;
				uv[0].y = Mathf.Lerp(uv[2].y, uv[0].y, num3);
				uv[3].y = uv[0].y;
			}
		}
		else if (num3 > num2)
		{
			num2 *= 1f / num3;
			num3 = 1f;
			if (invert)
			{
				xy[0].x = Mathf.Lerp(xy[2].x, xy[0].x, num2);
				xy[1].x = xy[0].x;
				uv[0].x = Mathf.Lerp(uv[2].x, uv[0].x, num2);
				uv[1].x = uv[0].x;
			}
		}
		else
		{
			num2 = 1f;
			num3 = 1f;
		}
		if (invert)
		{
			xy[1].y = Mathf.Lerp(xy[2].y, xy[0].y, num3);
			uv[1].y = Mathf.Lerp(uv[2].y, uv[0].y, num3);
		}
		else
		{
			xy[3].x = Mathf.Lerp(xy[2].x, xy[0].x, num2);
			uv[3].x = Mathf.Lerp(uv[2].x, uv[0].x, num2);
		}
		return true;
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x000267F0 File Offset: 0x000249F0
	private void Rotate(Vector2[] v, int offset)
	{
		for (int i = 0; i < offset; i++)
		{
			Vector2 vector = new Vector2(v[3].x, v[3].y);
			v[3].x = v[2].y;
			v[3].y = v[2].x;
			v[2].x = v[1].y;
			v[2].y = v[1].x;
			v[1].x = v[0].y;
			v[1].y = v[0].x;
			v[0].x = vector.y;
			v[0].y = vector.x;
		}
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x000268E4 File Offset: 0x00024AE4
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color> cols)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 1f;
		float num4 = -1f;
		float num5 = this.mOuterUV.xMin;
		float num6 = this.mOuterUV.yMin;
		float num7 = this.mOuterUV.xMax;
		float num8 = this.mOuterUV.yMax;
		if (this.mFillDirection == UIFilledSprite.FillDirection.Horizontal || this.mFillDirection == UIFilledSprite.FillDirection.Vertical)
		{
			float num9 = (num7 - num5) * this.mFillAmount;
			float num10 = (num8 - num6) * this.mFillAmount;
			if (this.fillDirection == UIFilledSprite.FillDirection.Horizontal)
			{
				if (this.mInvert)
				{
					num = 1f - this.mFillAmount;
					num5 = num7 - num9;
				}
				else
				{
					num3 *= this.mFillAmount;
					num7 = num5 + num9;
				}
			}
			else if (this.fillDirection == UIFilledSprite.FillDirection.Vertical)
			{
				if (this.mInvert)
				{
					num4 *= this.mFillAmount;
					num6 = num8 - num10;
				}
				else
				{
					num2 = 0f - (1f - this.mFillAmount);
					num8 = num6 + num10;
				}
			}
		}
		Vector2[] array = new Vector2[4];
		Vector2[] array2 = new Vector2[4];
		array[0] = new Vector2(num3, num2);
		array[1] = new Vector2(num3, num4);
		array[2] = new Vector2(num, num4);
		array[3] = new Vector2(num, num2);
		array2[0] = new Vector2(num7, num8);
		array2[1] = new Vector2(num7, num6);
		array2[2] = new Vector2(num5, num6);
		array2[3] = new Vector2(num5, num8);
		if (this.fillDirection == UIFilledSprite.FillDirection.Radial90)
		{
			if (!this.AdjustRadial(array, array2, this.mFillAmount, this.mInvert))
			{
				return;
			}
		}
		else
		{
			if (this.fillDirection == UIFilledSprite.FillDirection.Radial180)
			{
				Vector2[] array3 = new Vector2[4];
				Vector2[] array4 = new Vector2[4];
				for (int i = 0; i < 2; i++)
				{
					array3[0] = new Vector2(0f, 0f);
					array3[1] = new Vector2(0f, 1f);
					array3[2] = new Vector2(1f, 1f);
					array3[3] = new Vector2(1f, 0f);
					array4[0] = new Vector2(0f, 0f);
					array4[1] = new Vector2(0f, 1f);
					array4[2] = new Vector2(1f, 1f);
					array4[3] = new Vector2(1f, 0f);
					if (this.mInvert)
					{
						if (i > 0)
						{
							this.Rotate(array3, i);
							this.Rotate(array4, i);
						}
					}
					else if (i < 1)
					{
						this.Rotate(array3, 1 - i);
						this.Rotate(array4, 1 - i);
					}
					float num11;
					float num12;
					if (i == 1)
					{
						num11 = ((!this.mInvert) ? 1f : 0.5f);
						num12 = ((!this.mInvert) ? 0.5f : 1f);
					}
					else
					{
						num11 = ((!this.mInvert) ? 0.5f : 1f);
						num12 = ((!this.mInvert) ? 1f : 0.5f);
					}
					array3[1].y = Mathf.Lerp(num11, num12, array3[1].y);
					array3[2].y = Mathf.Lerp(num11, num12, array3[2].y);
					array4[1].y = Mathf.Lerp(num11, num12, array4[1].y);
					array4[2].y = Mathf.Lerp(num11, num12, array4[2].y);
					float num13 = this.mFillAmount * 2f - (float)i;
					bool flag = i % 2 == 1;
					if (this.AdjustRadial(array3, array4, num13, !flag))
					{
						if (this.mInvert)
						{
							flag = !flag;
						}
						if (flag)
						{
							for (int j = 0; j < 4; j++)
							{
								num11 = Mathf.Lerp(array[0].x, array[2].x, array3[j].x);
								num12 = Mathf.Lerp(array[0].y, array[2].y, array3[j].y);
								float num14 = Mathf.Lerp(array2[0].x, array2[2].x, array4[j].x);
								float num15 = Mathf.Lerp(array2[0].y, array2[2].y, array4[j].y);
								verts.Add(new Vector3(num11, num12, 0f));
								uvs.Add(new Vector2(num14, num15));
								cols.Add(base.color);
							}
						}
						else
						{
							for (int k = 3; k > -1; k--)
							{
								num11 = Mathf.Lerp(array[0].x, array[2].x, array3[k].x);
								num12 = Mathf.Lerp(array[0].y, array[2].y, array3[k].y);
								float num16 = Mathf.Lerp(array2[0].x, array2[2].x, array4[k].x);
								float num17 = Mathf.Lerp(array2[0].y, array2[2].y, array4[k].y);
								verts.Add(new Vector3(num11, num12, 0f));
								uvs.Add(new Vector2(num16, num17));
								cols.Add(base.color);
							}
						}
					}
				}
				return;
			}
			if (this.fillDirection == UIFilledSprite.FillDirection.Radial360)
			{
				float[] array5 = new float[]
				{
					0.5f, 1f, 0f, 0.5f, 0.5f, 1f, 0.5f, 1f, 0f, 0.5f,
					0.5f, 1f, 0f, 0.5f, 0f, 0.5f
				};
				Vector2[] array6 = new Vector2[4];
				Vector2[] array7 = new Vector2[4];
				for (int l = 0; l < 4; l++)
				{
					array6[0] = new Vector2(0f, 0f);
					array6[1] = new Vector2(0f, 1f);
					array6[2] = new Vector2(1f, 1f);
					array6[3] = new Vector2(1f, 0f);
					array7[0] = new Vector2(0f, 0f);
					array7[1] = new Vector2(0f, 1f);
					array7[2] = new Vector2(1f, 1f);
					array7[3] = new Vector2(1f, 0f);
					if (this.mInvert)
					{
						if (l > 0)
						{
							this.Rotate(array6, l);
							this.Rotate(array7, l);
						}
					}
					else if (l < 3)
					{
						this.Rotate(array6, 3 - l);
						this.Rotate(array7, 3 - l);
					}
					for (int m = 0; m < 4; m++)
					{
						int num18 = ((!this.mInvert) ? (l * 4) : ((3 - l) * 4));
						float num19 = array5[num18];
						float num20 = array5[num18 + 1];
						float num21 = array5[num18 + 2];
						float num22 = array5[num18 + 3];
						array6[m].x = Mathf.Lerp(num19, num20, array6[m].x);
						array6[m].y = Mathf.Lerp(num21, num22, array6[m].y);
						array7[m].x = Mathf.Lerp(num19, num20, array7[m].x);
						array7[m].y = Mathf.Lerp(num21, num22, array7[m].y);
					}
					float num23 = this.mFillAmount * 4f - (float)l;
					bool flag2 = l % 2 == 1;
					if (this.AdjustRadial(array6, array7, num23, !flag2))
					{
						if (this.mInvert)
						{
							flag2 = !flag2;
						}
						if (flag2)
						{
							for (int n = 0; n < 4; n++)
							{
								float num24 = Mathf.Lerp(array[0].x, array[2].x, array6[n].x);
								float num25 = Mathf.Lerp(array[0].y, array[2].y, array6[n].y);
								float num26 = Mathf.Lerp(array2[0].x, array2[2].x, array7[n].x);
								float num27 = Mathf.Lerp(array2[0].y, array2[2].y, array7[n].y);
								verts.Add(new Vector3(num24, num25, 0f));
								uvs.Add(new Vector2(num26, num27));
								cols.Add(base.color);
							}
						}
						else
						{
							for (int num28 = 3; num28 > -1; num28--)
							{
								float num29 = Mathf.Lerp(array[0].x, array[2].x, array6[num28].x);
								float num30 = Mathf.Lerp(array[0].y, array[2].y, array6[num28].y);
								float num31 = Mathf.Lerp(array2[0].x, array2[2].x, array7[num28].x);
								float num32 = Mathf.Lerp(array2[0].y, array2[2].y, array7[num28].y);
								verts.Add(new Vector3(num29, num30, 0f));
								uvs.Add(new Vector2(num31, num32));
								cols.Add(base.color);
							}
						}
					}
				}
				return;
			}
		}
		for (int num33 = 0; num33 < 4; num33++)
		{
			verts.Add(array[num33]);
			uvs.Add(array2[num33]);
			cols.Add(base.color);
		}
	}

	// Token: 0x04000699 RID: 1689
	[HideInInspector]
	[SerializeField]
	private UIFilledSprite.FillDirection mFillDirection = UIFilledSprite.FillDirection.Radial360;

	// Token: 0x0400069A RID: 1690
	[SerializeField]
	[HideInInspector]
	private float mFillAmount = 1f;

	// Token: 0x0400069B RID: 1691
	[SerializeField]
	[HideInInspector]
	private bool mInvert;

	// Token: 0x02000209 RID: 521
	public enum FillDirection
	{
		// Token: 0x04000BE4 RID: 3044
		Horizontal,
		// Token: 0x04000BE5 RID: 3045
		Vertical,
		// Token: 0x04000BE6 RID: 3046
		Radial90,
		// Token: 0x04000BE7 RID: 3047
		Radial180,
		// Token: 0x04000BE8 RID: 3048
		Radial360
	}
}
