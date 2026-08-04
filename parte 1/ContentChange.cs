using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class ContentChange : MonoBehaviour
{
	// Token: 0x0600026B RID: 619 RVA: 0x0000AD9F File Offset: 0x00008F9F
	private void Start()
	{
		this.foldedOut = false;
		this.ContentActivation(false);
		this._table = NGUITools.FindInParents<UITable>(base.gameObject);
		this._table.repositionNow = true;
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0000ADCC File Offset: 0x00008FCC
	private void OnEnable()
	{
		this.ContentActivation(this.foldedOut);
		if (this._table != null)
		{
			this._table.repositionNow = true;
		}
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0000ADF4 File Offset: 0x00008FF4
	public void TriggerContent()
	{
		if (this.foldedOut)
		{
			this.ContentActivation(this.foldedOut);
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0000AE0A File Offset: 0x0000900A
	public void FoldClicked()
	{
		if (!this.foldedOut)
		{
			this.foldedOut = true;
			return;
		}
		this.foldedOut = false;
		this.ContentActivation(false);
	}

	// Token: 0x0600026F RID: 623 RVA: 0x0000AE2C File Offset: 0x0000902C
	private void ContentActivation(bool active)
	{
		this.descriptionText.active = active;
		if (this.button != null)
		{
			NGUITools.SetActive(this.button, active);
		}
		NGUITools.SetActive(this.openButton, !active);
		NGUITools.AddWidgetCollider(base.transform.parent.gameObject);
		if (this.button != null)
		{
			NGUITools.AddWidgetCollider(this.button);
			BoxCollider boxCollider = this.button.GetComponent<Collider>() as BoxCollider;
			boxCollider.size = new Vector3(70f, 50f, boxCollider.size.z);
		}
	}

	// Token: 0x040001AC RID: 428
	private bool foldedOut;

	// Token: 0x040001AD RID: 429
	public GameObject descriptionText;

	// Token: 0x040001AE RID: 430
	public GameObject button;

	// Token: 0x040001AF RID: 431
	public GameObject openButton;

	// Token: 0x040001B0 RID: 432
	private UITable _table;
}
