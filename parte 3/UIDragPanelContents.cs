using System;
using UnityEngine;

// Token: 0x02000103 RID: 259
[AddComponentMenu("NGUI/Interaction/Drag Panel Contents")]
[ExecuteInEditMode]
public class UIDragPanelContents : MonoBehaviour
{
	// Token: 0x0600074B RID: 1867 RVA: 0x00024800 File Offset: 0x00022A00
	private void Awake()
	{
		if (!(this.panel != null))
		{
			return;
		}
		if (this.draggablePanel == null)
		{
			this.draggablePanel = this.panel.GetComponent<UIDraggablePanel>();
			if (this.draggablePanel == null)
			{
				this.draggablePanel = this.panel.gameObject.AddComponent<UIDraggablePanel>();
			}
		}
		this.panel = null;
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x00024866 File Offset: 0x00022A66
	private void Start()
	{
		if (this.draggablePanel == null)
		{
			this.draggablePanel = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		}
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x00024887 File Offset: 0x00022A87
	private void OnPress(bool pressed)
	{
		if (base.enabled && base.gameObject.active && this.draggablePanel != null)
		{
			this.draggablePanel.Press(pressed);
		}
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x000248B8 File Offset: 0x00022AB8
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && base.gameObject.active && this.draggablePanel != null)
		{
			this.draggablePanel.Drag(delta);
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x000248E9 File Offset: 0x00022AE9
	private void OnScroll(float delta)
	{
		if (base.enabled && base.gameObject.active && this.draggablePanel != null)
		{
			this.draggablePanel.Scroll(delta);
		}
	}

	// Token: 0x04000658 RID: 1624
	public UIDraggablePanel draggablePanel;

	// Token: 0x04000659 RID: 1625
	[SerializeField]
	[HideInInspector]
	private UIPanel panel;
}
