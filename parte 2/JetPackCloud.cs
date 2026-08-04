using System;
using UnityEngine;

// Token: 0x02000081 RID: 129
public class JetPackCloud : MonoBehaviour
{
	// Token: 0x06000419 RID: 1049 RVA: 0x00012442 File Offset: 0x00010642
	private void Awake()
	{
		this.material.mainTextureOffset = new Vector2(this.startOffset, 0f);
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00012460 File Offset: 0x00010660
	private void Update()
	{
		float num = this.material.mainTextureOffset.x;
		num = (num + Time.deltaTime * this.scrollSpeed) % 1f;
		this.material.mainTextureOffset = new Vector2(num, 0f);
	}

	// Token: 0x0400035C RID: 860
	public float scrollSpeed = 0.5f;

	// Token: 0x0400035D RID: 861
	public Material material;

	// Token: 0x0400035E RID: 862
	public float startOffset;
}
