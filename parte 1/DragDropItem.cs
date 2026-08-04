using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
[AddComponentMenu("NGUI/Examples/Drag & Drop Item")]
public class DragDropItem : MonoBehaviour
{
	// Token: 0x060002BA RID: 698 RVA: 0x0000BE90 File Offset: 0x0000A090
	private void UpdateTable()
	{
		UITable uitable = NGUITools.FindInParents<UITable>(base.gameObject);
		if (uitable != null)
		{
			uitable.repositionNow = true;
		}
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0000BEBC File Offset: 0x0000A0BC
	private void Drop()
	{
		Collider collider = UICamera.lastHit.collider;
		DragDropContainer dragDropContainer = ((!(collider != null)) ? null : collider.gameObject.GetComponent<DragDropContainer>());
		if (dragDropContainer != null)
		{
			this.mTrans.parent = dragDropContainer.transform;
		}
		else
		{
			this.mTrans.parent = this.mParent;
		}
		this.UpdateTable();
		base.BroadcastMessage("CheckParent", SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x060002BC RID: 700 RVA: 0x0000BF2B File Offset: 0x0000A12B
	private void Awake()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0000BF3C File Offset: 0x0000A13C
	private void OnDrag(Vector2 delta)
	{
		if (UICamera.currentTouchID == -1)
		{
			if (!this.mIsDragging)
			{
				this.mIsDragging = true;
				this.mParent = this.mTrans.parent;
				this.mTrans.parent = DragDropRoot.root;
				this.mTrans.BroadcastMessage("CheckParent", SendMessageOptions.DontRequireReceiver);
				return;
			}
			this.mTrans.localPosition += delta;
		}
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
	private void OnPress(bool isPressed)
	{
		this.mIsDragging = false;
		Collider component = base.GetComponent<Collider>();
		if (component != null)
		{
			component.enabled = !isPressed;
		}
		if (!isPressed)
		{
			this.Drop();
		}
	}

	// Token: 0x040001FB RID: 507
	public GameObject prefab;

	// Token: 0x040001FC RID: 508
	private Transform mTrans;

	// Token: 0x040001FD RID: 509
	private bool mIsDragging;

	// Token: 0x040001FE RID: 510
	private Transform mParent;
}
