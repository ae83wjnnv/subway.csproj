using System;
using UnityEngine;

// Token: 0x02000025 RID: 37
public class CameraCulling : MonoBehaviour
{
	// Token: 0x060001AE RID: 430 RVA: 0x000069B2 File Offset: 0x00004BB2
	private void Awake()
	{
		this.distances = new float[32];
		this.distances[LayerMask.NameToLayer("TransparentFX")] = this.distance;
		base.GetComponent<Camera>().layerCullDistances = this.distances;
	}

	// Token: 0x040000E4 RID: 228
	private float[] distances;

	// Token: 0x040000E5 RID: 229
	public float distance;
}
