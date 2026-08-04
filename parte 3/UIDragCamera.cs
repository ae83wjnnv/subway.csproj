using System;
using UnityEngine;

// Token: 0x02000101 RID: 257
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
[ExecuteInEditMode]
public class UIDragCamera : IgnoreTimeScale
{
	// Token: 0x06000740 RID: 1856 RVA: 0x00024190 File Offset: 0x00022390
	private void Awake()
	{
		if (this.target != null)
		{
			if (this.draggableCamera == null)
			{
				this.draggableCamera = this.target.GetComponent<UIDraggableCamera>();
				if (this.draggableCamera == null)
				{
					this.draggableCamera = this.target.gameObject.AddComponent<UIDraggableCamera>();
				}
			}
			this.target = null;
			return;
		}
		if (this.draggableCamera == null)
		{
			this.draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(base.gameObject);
		}
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x00024215 File Offset: 0x00022415
	private void OnPress(bool isPressed)
	{
		if (base.enabled && base.gameObject.active && this.draggableCamera != null)
		{
			this.draggableCamera.Press(isPressed);
		}
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x00024246 File Offset: 0x00022446
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && base.gameObject.active && this.draggableCamera != null)
		{
			this.draggableCamera.Drag(delta);
		}
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x00024277 File Offset: 0x00022477
	private void OnScroll(float delta)
	{
		if (base.enabled && base.gameObject.active && this.draggableCamera != null)
		{
			this.draggableCamera.Scroll(delta);
		}
	}

	// Token: 0x04000649 RID: 1609
	public UIDraggableCamera draggableCamera;

	// Token: 0x0400064A RID: 1610
	[SerializeField]
	[HideInInspector]
	private Component target;
}
