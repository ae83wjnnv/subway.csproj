using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
[AddComponentMenu("NGUI/Examples/Drag & Drop Root")]
public class DragDropRoot : MonoBehaviour
{
	// Token: 0x060002C0 RID: 704 RVA: 0x0000BFEF File Offset: 0x0000A1EF
	private void Awake()
	{
		DragDropRoot.root = base.transform;
	}

	// Token: 0x040001FF RID: 511
	public static Transform root;
}
