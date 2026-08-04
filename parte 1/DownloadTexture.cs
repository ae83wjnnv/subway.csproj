using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200004D RID: 77
[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
	// Token: 0x060002B6 RID: 694 RVA: 0x0000BE30 File Offset: 0x0000A030
	private IEnumerator Start()
	{
		WWW www = new WWW(this.url);
		yield return www;
		this.mTex = www.texture;
		if (this.mTex != null)
		{
			UITexture component = base.GetComponent<UITexture>();
			if (component.material == null)
			{
				Shader shader = Shader.Find("Unlit/Transparent Colored");
				this.mMat = new Material(shader);
				component.material = this.mMat;
			}
			component.material.mainTexture = this.mTex;
			component.MakePixelPerfect();
		}
		www.Dispose();
		yield break;
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0000BE3F File Offset: 0x0000A03F
	private void OnDestroy()
	{
		if (this.mMat != null)
		{
			Object.Destroy(this.mMat);
		}
		if (this.mTex != null)
		{
			Object.Destroy(this.mTex);
		}
	}

	// Token: 0x040001F8 RID: 504
	public string url = "http://www.tasharen.com/misc/logo.png";

	// Token: 0x040001F9 RID: 505
	private Material mMat;

	// Token: 0x040001FA RID: 506
	private Texture2D mTex;
}
