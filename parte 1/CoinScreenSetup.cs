using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class CoinScreenSetup : MonoBehaviour
{
	// Token: 0x0600025E RID: 606 RVA: 0x0000A712 File Offset: 0x00008912
	private void Awake()
	{
		this._table = base.GetComponent<UITable>();
		this._parentDragPanel = NGUITools.FindInParents<UIDraggablePanel>(base.transform.parent.gameObject);
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000A73B File Offset: 0x0000893B
	private void Start()
	{
		this.FillTable();
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0000A743 File Offset: 0x00008943
	public void RefreshCurrencyEarners()
	{
		this.FillTable();
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0000A74C File Offset: 0x0000894C
	private void FillTable()
	{
		base.transform.parent.GetComponent<UIPanel>().widgetsAreStatic = false;
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			NGUITools.SetActive(transform.gameObject, false);
			Object.Destroy(transform.gameObject);
		}
		int num = 0;
		UILabel uilabel = NGUITools.AddWidget<UILabel>(base.gameObject);
		uilabel.font = this.headerFont;
		uilabel.text = "Coin Shop";
		uilabel.color = new Color(0f, 0.2901961f, 0.5019608f, 1f);
		uilabel.name = string.Format("{0:000}", num);
		uilabel.MakePixelPerfect();
		if (DeviceInfo.isHighres)
		{
			uilabel.gameObject.transform.localScale = new Vector3(uilabel.gameObject.transform.localScale.x / 2f, uilabel.gameObject.transform.localScale.y / 2f, uilabel.gameObject.transform.localScale.z);
		}
		num++;
		GameObject gameObject = NGUITools.AddChild(base.gameObject, this.coinPrefab);
		gameObject.name = string.Format("{0:000}", num);
		gameObject.GetComponent<CoinButtonHelper>().Init(InAppData.inAppTier1);
		gameObject.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
		NGUITools.AddWidgetCollider(gameObject);
		num++;
		GameObject gameObject2 = NGUITools.AddChild(base.gameObject, this.coinPrefab);
		gameObject2.name = string.Format("{0:000}", num);
		gameObject2.GetComponent<CoinButtonHelper>().Init(InAppData.inAppTier2);
		gameObject2.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
		NGUITools.AddWidgetCollider(gameObject2);
		num++;
		GameObject gameObject3 = NGUITools.AddChild(base.gameObject, this.coinPrefab);
		gameObject3.name = string.Format("{0:000}", num);
		gameObject3.GetComponent<CoinButtonHelper>().Init(InAppData.inAppTier3);
		gameObject3.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
		NGUITools.AddWidgetCollider(gameObject3);
		num++;
		UILabel uilabel2 = NGUITools.AddWidget<UILabel>(base.gameObject);
		uilabel2.font = this.headerFont;
		uilabel2.text = "Earn Coins";
		uilabel2.color = new Color(0f, 0.2901961f, 0.5019608f, 1f);
		uilabel2.name = string.Format("{0:000}", num);
		uilabel2.MakePixelPerfect();
		if (DeviceInfo.isHighres)
		{
			uilabel2.gameObject.transform.localScale = new Vector3(uilabel2.gameObject.transform.localScale.x / 2f, uilabel2.gameObject.transform.localScale.y / 2f, uilabel2.gameObject.transform.localScale.z);
		}
		num++;
		for (int i = 0; i < EarnCurrencyInfo.profiles.Length; i++)
		{
			if (EarnCurrencyInfo.ShouldShowInGUI(i))
			{
				EarnCurrencyInfo.EarnCurrencyProfile earnCurrencyProfile = EarnCurrencyInfo.profiles[i];
				GameObject gameObject4 = NGUITools.AddChild(base.gameObject, this.coinEarnerPrefab);
				gameObject4.name = string.Format("{0:000}", num);
				string text = string.Format(earnCurrencyProfile.desc, earnCurrencyProfile.amountOfCoins);
				gameObject4.GetComponent<CoinEarnerButtonHelper>().Init(i, earnCurrencyProfile.title, text, earnCurrencyProfile.iconName);
				gameObject4.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
				NGUITools.AddWidgetCollider(gameObject4);
				num++;
			}
		}
		base.gameObject.BroadcastMessage("CreatePanel", SendMessageOptions.DontRequireReceiver);
		this._table.gameObject.transform.localPosition = new Vector3(this._table.gameObject.transform.localPosition.x, 0f, this._table.gameObject.transform.localPosition.z);
		this._table.sorted = true;
		this._table.repositionNow = true;
		this._table.Reposition();
		this._parentDragPanel.RestrictWithinBounds(true);
		base.StartCoroutine(this.SetStatic());
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000ABA4 File Offset: 0x00008DA4
	private IEnumerator SetStatic()
	{
		yield return null;
		base.transform.parent.GetComponent<UIPanel>().widgetsAreStatic = true;
		yield break;
	}

	// Token: 0x040001A0 RID: 416
	public GameObject coinPrefab;

	// Token: 0x040001A1 RID: 417
	public GameObject coinEarnerPrefab;

	// Token: 0x040001A2 RID: 418
	public UIFont headerFont;

	// Token: 0x040001A3 RID: 419
	private UITable _table;

	// Token: 0x040001A4 RID: 420
	private UIDraggablePanel _parentDragPanel;
}
