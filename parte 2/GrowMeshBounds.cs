using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class GrowMeshBounds : MonoBehaviour
{
	// Token: 0x060003BD RID: 957 RVA: 0x00010E7C File Offset: 0x0000F07C
	private void Awake()
	{
		Mesh sharedMesh = base.GetComponent<MeshFilter>().sharedMesh;
		sharedMesh.RecalculateBounds();
		if (!GrowMeshBounds.grownMeshes.Contains(sharedMesh))
		{
			sharedMesh.bounds = new Bounds(sharedMesh.bounds.center, sharedMesh.bounds.extents * this.growFactor);
			GrowMeshBounds.grownMeshes.Add(sharedMesh);
			return;
		}
		Debug.Log(sharedMesh.name + " allready grown.");
	}

	// Token: 0x04000309 RID: 777
	public float growFactor = 5f;

	// Token: 0x0400030A RID: 778
	private static HashSet<Mesh> grownMeshes = new HashSet<Mesh>();
}
