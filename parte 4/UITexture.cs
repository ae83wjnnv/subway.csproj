using System;
using UnityEngine;

// Token: 0x0200013B RID: 315
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Texture")]
public class UITexture : UIWidget
{
	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06000948 RID: 2376 RVA: 0x000322CF File Offset: 0x000304CF
	public override bool keepMaterial
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x000322D4 File Offset: 0x000304D4
	public override void MakePixelPerfect()
	{
		Texture mainTexture = base.mainTexture;
		if (mainTexture != null)
		{
			Vector3 localScale = base.cachedTransform.localScale;
			localScale.x = (float)mainTexture.width;
			localScale.y = (float)mainTexture.height;
			localScale.z = 1f;
			base.cachedTransform.localScale = localScale;
		}
		base.MakePixelPerfect();
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x00032338 File Offset: 0x00030538
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color> cols)
	{
		verts.Add(new Vector3(1f, 0f, 0f));
		verts.Add(new Vector3(1f, -1f, 0f));
		verts.Add(new Vector3(0f, -1f, 0f));
		verts.Add(new Vector3(0f, 0f, 0f));
		uvs.Add(Vector2.one);
		uvs.Add(new Vector2(1f, 0f));
		uvs.Add(Vector2.zero);
		uvs.Add(new Vector2(0f, 1f));
		cols.Add(base.color);
		cols.Add(base.color);
		cols.Add(base.color);
		cols.Add(base.color);
	}
}
