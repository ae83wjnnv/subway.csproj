using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
[AddComponentMenu("NGUI/Examples/Drag & Drop Surface")]
public class DragDropSurface : MonoBehaviour
{
	// Token: 0x060002C2 RID: 706 RVA: 0x0000C004 File Offset: 0x0000A204
	private void OnDrop(GameObject go)
	{
		DragDropItem component = go.GetComponent<DragDropItem>();
		if (component != null)
		{
			Transform transform = NGUITools.AddChild(base.gameObject, component.prefab).transform;
			transform.position = UICamera.lastHit.point;
			if (this.rotatePlacedObject)
			{
				transform.rotation = Quaternion.LookRotation(UICamera.lastHit.normal) * Quaternion.Euler(90f, 0f, 0f);
			}
			Object.Destroy(go);
		}
	}

	// Token: 0x04000200 RID: 512
	public bool rotatePlacedObject;
}
