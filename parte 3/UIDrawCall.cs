using System;
using UnityEngine;

// Token: 0x02000106 RID: 262
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Draw Call")]
public class UIDrawCall : MonoBehaviour
{
	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000771 RID: 1905 RVA: 0x00025C64 File Offset: 0x00023E64
	// (set) Token: 0x06000772 RID: 1906 RVA: 0x00025C6C File Offset: 0x00023E6C
	public bool depthPass
	{
		get
		{
			return this.mDepthPass;
		}
		set
		{
			if (this.mDepthPass != value)
			{
				this.mDepthPass = value;
				this.mReset = true;
			}
		}
	}

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000773 RID: 1907 RVA: 0x00025C85 File Offset: 0x00023E85
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000774 RID: 1908 RVA: 0x00025CA7 File Offset: 0x00023EA7
	// (set) Token: 0x06000775 RID: 1909 RVA: 0x00025CAF File Offset: 0x00023EAF
	public Material material
	{
		get
		{
			return this.mSharedMat;
		}
		set
		{
			this.mSharedMat = value;
		}
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000776 RID: 1910 RVA: 0x00025CB8 File Offset: 0x00023EB8
	public int triangles
	{
		get
		{
			return this.mMesh.vertexCount >> 1;
		}
	}

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000777 RID: 1911 RVA: 0x00025CC7 File Offset: 0x00023EC7
	// (set) Token: 0x06000778 RID: 1912 RVA: 0x00025CCF File Offset: 0x00023ECF
	public UIDrawCall.Clipping clipping
	{
		get
		{
			return this.mClipping;
		}
		set
		{
			if (this.mClipping != value)
			{
				this.mClipping = value;
				this.mReset = true;
			}
		}
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000779 RID: 1913 RVA: 0x00025CE8 File Offset: 0x00023EE8
	// (set) Token: 0x0600077A RID: 1914 RVA: 0x00025CF0 File Offset: 0x00023EF0
	public Vector4 clipRange
	{
		get
		{
			return this.mClipRange;
		}
		set
		{
			this.mClipRange = value;
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x0600077B RID: 1915 RVA: 0x00025CF9 File Offset: 0x00023EF9
	// (set) Token: 0x0600077C RID: 1916 RVA: 0x00025D01 File Offset: 0x00023F01
	public Vector2 clipSoftness
	{
		get
		{
			return this.mClipSoft;
		}
		set
		{
			this.mClipSoft = value;
		}
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x00025D0C File Offset: 0x00023F0C
	private void UpdateMaterials()
	{
		if (this.mClipping != UIDrawCall.Clipping.None)
		{
			Shader shader = null;
			if (this.mClipping != UIDrawCall.Clipping.None)
			{
				string text = this.mSharedMat.shader.name;
				text = text.Replace(" (HardClip)", string.Empty);
				text = text.Replace(" (AlphaClip)", string.Empty);
				text = text.Replace(" (SoftClip)", string.Empty);
				if (this.mClipping == UIDrawCall.Clipping.HardClip)
				{
					shader = Shader.Find(text + " (HardClip)");
				}
				else if (this.mClipping == UIDrawCall.Clipping.AlphaClip)
				{
					shader = Shader.Find(text + " (AlphaClip)");
				}
				else if (this.mClipping == UIDrawCall.Clipping.SoftClip)
				{
					shader = Shader.Find(text + " (SoftClip)");
				}
				if (shader == null)
				{
					this.mClipping = UIDrawCall.Clipping.None;
				}
			}
			if (shader != null)
			{
				this.mClippedMat = new Material(this.mSharedMat);
				this.mClippedMat.shader = shader;
			}
		}
		else if (this.mClippedMat != null)
		{
			NGUITools.Destroy(this.mClippedMat);
			this.mClippedMat = null;
		}
		if (this.mDepthPass)
		{
			if (this.mDepthMat == null)
			{
				Shader shader2 = Shader.Find("Depth");
				this.mDepthMat = new Material(shader2);
				this.mDepthMat.mainTexture = this.mSharedMat.mainTexture;
			}
		}
		else if (this.mDepthMat != null)
		{
			NGUITools.Destroy(this.mDepthMat);
			this.mDepthMat = null;
		}
		Material material = ((!(this.mClippedMat != null)) ? this.mSharedMat : this.mClippedMat);
		if (this.mDepthMat != null)
		{
			if (this.mRen.sharedMaterials == null || this.mRen.sharedMaterials.Length != 2 || !(this.mRen.sharedMaterials[1] == material))
			{
				this.mRen.sharedMaterials = new Material[] { this.mDepthMat, material };
				return;
			}
		}
		else if (this.mRen.sharedMaterial != material)
		{
			this.mRen.sharedMaterials = new Material[] { material };
		}
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x00025F28 File Offset: 0x00024128
	public void Set(BetterList<Vector3> verts, BetterList<Vector3> norms, BetterList<Vector4> tans, BetterList<Vector2> uvs, BetterList<Color> cols)
	{
		int size = verts.size;
		if (size <= 0 || size != uvs.size || size != cols.size || size % 4 != 0)
		{
			if (this.mMesh != null)
			{
				this.mMesh.Clear();
			}
			Debug.LogError("UIWidgets must fill the buffer with 4 vertices per quad. Found " + size.ToString());
			return;
		}
		int num = 0;
		int num2 = (size >> 1) * 3;
		if (this.mIndices == null || this.mIndices.Length != num2)
		{
			this.mIndices = new int[num2];
			for (int i = 0; i < size; i += 4)
			{
				this.mIndices[num++] = i;
				this.mIndices[num++] = i + 1;
				this.mIndices[num++] = i + 2;
				this.mIndices[num++] = i + 2;
				this.mIndices[num++] = i + 3;
				this.mIndices[num++] = i;
			}
		}
		if (this.mFilter == null)
		{
			this.mFilter = base.gameObject.GetComponent<MeshFilter>();
		}
		if (this.mFilter == null)
		{
			this.mFilter = base.gameObject.AddComponent<MeshFilter>();
		}
		if (this.mRen == null)
		{
			this.mRen = base.gameObject.GetComponent<MeshRenderer>();
		}
		if (this.mRen == null)
		{
			this.mRen = base.gameObject.AddComponent<MeshRenderer>();
			this.UpdateMaterials();
		}
		if (verts.size < 65000)
		{
			if (this.mMesh == null)
			{
				this.mMesh = new Mesh();
				this.mMesh.name = "UIDrawCall for " + this.mSharedMat.name;
			}
			else
			{
				this.mMesh.Clear();
			}
			this.mMesh.vertices = verts.ToArray();
			if (norms != null)
			{
				this.mMesh.normals = norms.ToArray();
			}
			if (tans != null)
			{
				this.mMesh.tangents = tans.ToArray();
			}
			this.mMesh.uv = uvs.ToArray();
			this.mMesh.colors = cols.ToArray();
			this.mMesh.triangles = this.mIndices;
			this.mMesh.RecalculateBounds();
			this.mFilter.mesh = this.mMesh;
			return;
		}
		if (this.mMesh != null)
		{
			this.mMesh.Clear();
		}
		Debug.LogError("Too many vertices on one panel: " + verts.size.ToString());
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x000261B4 File Offset: 0x000243B4
	private void OnWillRenderObject()
	{
		if (this.mReset)
		{
			this.mReset = false;
			this.UpdateMaterials();
		}
		if (this.mClippedMat != null)
		{
			this.mClippedMat.mainTextureOffset = new Vector2((0f - this.mClipRange.x) / this.mClipRange.z, (0f - this.mClipRange.y) / this.mClipRange.w);
			this.mClippedMat.mainTextureScale = new Vector2(1f / this.mClipRange.z, 1f / this.mClipRange.w);
			Vector2 vector = new Vector2(1000f, 1000f);
			if (this.mClipSoft.x > 0f)
			{
				vector.x = this.mClipRange.z / this.mClipSoft.x;
			}
			if (this.mClipSoft.y > 0f)
			{
				vector.y = this.mClipRange.w / this.mClipSoft.y;
			}
			this.mClippedMat.SetVector("_ClipSharpness", vector);
		}
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x000262EA File Offset: 0x000244EA
	private void OnDestroy()
	{
		NGUITools.DestroyImmediate(this.mMesh);
		NGUITools.DestroyImmediate(this.mClippedMat);
		NGUITools.DestroyImmediate(this.mDepthMat);
	}

	// Token: 0x0400067C RID: 1660
	private Transform mTrans;

	// Token: 0x0400067D RID: 1661
	private Material mSharedMat;

	// Token: 0x0400067E RID: 1662
	private Mesh mMesh;

	// Token: 0x0400067F RID: 1663
	private MeshFilter mFilter;

	// Token: 0x04000680 RID: 1664
	private MeshRenderer mRen;

	// Token: 0x04000681 RID: 1665
	private UIDrawCall.Clipping mClipping;

	// Token: 0x04000682 RID: 1666
	private Vector4 mClipRange;

	// Token: 0x04000683 RID: 1667
	private Vector2 mClipSoft;

	// Token: 0x04000684 RID: 1668
	private Material mClippedMat;

	// Token: 0x04000685 RID: 1669
	private Material mDepthMat;

	// Token: 0x04000686 RID: 1670
	private int[] mIndices;

	// Token: 0x04000687 RID: 1671
	private bool mDepthPass;

	// Token: 0x04000688 RID: 1672
	private bool mReset = true;

	// Token: 0x02000202 RID: 514
	public enum Clipping
	{
		// Token: 0x04000BDF RID: 3039
		None,
		// Token: 0x04000BE0 RID: 3040
		HardClip,
		// Token: 0x04000BE1 RID: 3041
		AlphaClip,
		// Token: 0x04000BE2 RID: 3042
		SoftClip
	}
}
