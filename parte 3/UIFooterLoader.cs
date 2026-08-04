using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class UIFooterLoader : MonoBehaviour
{
	// Token: 0x060007BD RID: 1981 RVA: 0x00028484 File Offset: 0x00026684
	private void Awake()
	{
		GameObject gameObject = NGUITools.AddChild(base.gameObject, this.FooterPrefab);
		this._footerHandler = gameObject.GetComponent<UIFootherHandler>();
		switch (this.selectedButton)
		{
		case 1:
			Object.Destroy(this._footerHandler.Button1.GetComponent<BoxCollider>());
			this._footerHandler.Fill1.color = this.selectedColor;
			return;
		case 2:
			Object.Destroy(this._footerHandler.Button2.GetComponent<BoxCollider>());
			this._footerHandler.Fill2.color = this.selectedColor;
			return;
		case 3:
			Object.Destroy(this._footerHandler.Button3.GetComponent<BoxCollider>());
			this._footerHandler.Fill3.color = this.selectedColor;
			return;
		default:
			Debug.Log("No button was selected in the footer?", this);
			return;
		}
	}

	// Token: 0x040006A6 RID: 1702
	public GameObject FooterPrefab;

	// Token: 0x040006A7 RID: 1703
	private UIFootherHandler _footerHandler;

	// Token: 0x040006A8 RID: 1704
	public int selectedButton;

	// Token: 0x040006A9 RID: 1705
	private Color selectedColor = new Color(0.6627451f, 0.6627451f, 0.6627451f, 1f);
}
