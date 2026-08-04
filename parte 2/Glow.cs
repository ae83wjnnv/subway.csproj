using System;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class Glow : MonoBehaviour
{
	// Token: 0x060003B2 RID: 946 RVA: 0x00010DAF File Offset: 0x0000EFAF
	public void Awake()
	{
		this.meshRenderer = base.GetComponentInChildren<MeshRenderer>();
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00010DBD File Offset: 0x0000EFBD
	public void SetVisible(bool visible)
	{
		if (this.meshRenderer != null)
		{
			this.meshRenderer.enabled = visible;
		}
		base.enabled = visible;
	}

	// Token: 0x04000307 RID: 775
	private MeshRenderer meshRenderer;
}
