using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class UIGeometry
{
	// Token: 0x170000CF RID: 207
	// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00028980 File Offset: 0x00026B80
	public bool hasVertices
	{
		get
		{
			return this.verts.size > 0;
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00028990 File Offset: 0x00026B90
	public bool hasTransformed
	{
		get
		{
			return this.mRtpVerts != null && this.mRtpVerts.size > 0 && this.mRtpVerts.size == this.verts.size;
		}
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x000289C2 File Offset: 0x00026BC2
	public void Clear()
	{
		this.verts.Clear();
		this.uvs.Clear();
		this.cols.Clear();
		this.mRtpVerts.Clear();
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x000289F0 File Offset: 0x00026BF0
	public void ApplyOffset(Vector3 pivotOffset)
	{
		for (int i = 0; i < this.verts.size; i++)
		{
			this.verts.buffer[i] += pivotOffset;
		}
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x00028A38 File Offset: 0x00026C38
	public void ApplyTransform(Matrix4x4 widgetToPanel, bool normals)
	{
		if (this.verts.size > 0)
		{
			this.mRtpVerts.Clear();
			int i = 0;
			int size = this.verts.size;
			while (i < size)
			{
				this.mRtpVerts.Add(widgetToPanel.MultiplyPoint3x4(this.verts[i]));
				i++;
			}
			this.mRtpNormal = widgetToPanel.MultiplyVector(Vector3.back).normalized;
			Vector3 normalized = widgetToPanel.MultiplyVector(Vector3.right).normalized;
			this.mRtpTan = new Vector4(normalized.x, normalized.y, normalized.z, -1f);
			return;
		}
		this.mRtpVerts.Clear();
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x00028AF4 File Offset: 0x00026CF4
	public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color> c, BetterList<Vector3> n, BetterList<Vector4> t)
	{
		if (this.mRtpVerts == null || this.mRtpVerts.size <= 0)
		{
			return;
		}
		if (n == null)
		{
			for (int i = 0; i < this.mRtpVerts.size; i++)
			{
				v.Add(this.mRtpVerts.buffer[i]);
				u.Add(this.uvs.buffer[i]);
				c.Add(this.cols.buffer[i]);
			}
			return;
		}
		for (int j = 0; j < this.mRtpVerts.size; j++)
		{
			v.Add(this.mRtpVerts.buffer[j]);
			u.Add(this.uvs.buffer[j]);
			c.Add(this.cols.buffer[j]);
			n.Add(this.mRtpNormal);
			t.Add(this.mRtpTan);
		}
	}

	// Token: 0x040006CD RID: 1741
	public BetterList<Vector3> verts = new BetterList<Vector3>();

	// Token: 0x040006CE RID: 1742
	public BetterList<Vector2> uvs = new BetterList<Vector2>();

	// Token: 0x040006CF RID: 1743
	public BetterList<Color> cols = new BetterList<Color>();

	// Token: 0x040006D0 RID: 1744
	private BetterList<Vector3> mRtpVerts = new BetterList<Vector3>();

	// Token: 0x040006D1 RID: 1745
	private Vector3 mRtpNormal;

	// Token: 0x040006D2 RID: 1746
	private Vector4 mRtpTan;
}
