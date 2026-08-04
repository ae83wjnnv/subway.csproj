using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000147 RID: 327
public class UpgradeScreenSetup : MonoBehaviour
{
	// Token: 0x060009B4 RID: 2484 RVA: 0x00035180 File Offset: 0x00033380
	private void Awake()
	{
		this._table = base.GetComponent<UITable>();
		this._parentDragPanel = NGUITools.FindInParents<UIDraggablePanel>(base.transform.parent.gameObject);
		Missions instance = Missions.Instance;
		instance.onMissionComplete = (Missions.MissionCompleteHandler)Delegate.Combine(instance.onMissionComplete, new Missions.MissionCompleteHandler(this.OnMissionComplete));
		Missions instance2 = Missions.Instance;
		instance2.onMissionSetComplete = (Missions.MissionSetCompleteHandler)Delegate.Combine(instance2.onMissionSetComplete, new Missions.MissionSetCompleteHandler(this.OnMissionSetComplete));
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x00035200 File Offset: 0x00033400
	private void OnDestroy()
	{
		Missions instance = Missions.Instance;
		instance.onMissionComplete = (Missions.MissionCompleteHandler)Delegate.Remove(instance.onMissionComplete, new Missions.MissionCompleteHandler(this.OnMissionComplete));
		Missions instance2 = Missions.Instance;
		instance2.onMissionSetComplete = (Missions.MissionSetCompleteHandler)Delegate.Remove(instance2.onMissionSetComplete, new Missions.MissionSetCompleteHandler(this.OnMissionSetComplete));
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x00035259 File Offset: 0x00033459
	private void OnEnable()
	{
		this._table.repositionNow = true;
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x00035267 File Offset: 0x00033467
	private void Start()
	{
		this.FillTable();
		if (!AdColony.isInitialized)
		{
			AdColony.Init("app2568a30bc18f470288d36d", "vz714b7567808540889e4a44");
		}
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x00035288 File Offset: 0x00033488
	private void FillTable()
	{
		int num = 0;
		UILabel uilabel = NGUITools.AddWidget<UILabel>(base.gameObject);
		uilabel.font = this.headerFont;
		uilabel.text = "Single Use";
		uilabel.color = new Color(0f, 0.2901961f, 0.5019608f, 1f);
		uilabel.name = string.Format("{0:000}", num);
		uilabel.supportEncoding = false;
		uilabel.multiLine = false;
		uilabel.MakePixelPerfect();
		if (DeviceInfo.isHighres)
		{
			uilabel.gameObject.transform.localScale = new Vector3(uilabel.gameObject.transform.localScale.x / 2f, uilabel.gameObject.transform.localScale.y / 2f, uilabel.gameObject.transform.localScale.z);
		}
		num++;
		GameObject gameObject = NGUITools.AddChild(base.gameObject, this.ConsumablePrefab);
		gameObject.name = string.Format("{0:000}", num);
		gameObject.GetComponent<UpgradeHelper>().InitSingle(PowerupType.hoverboard);
		gameObject.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
		NGUITools.AddWidgetCollider(gameObject);
		this.cachedUpgradeHelpers.Add(gameObject.GetComponent<UpgradeHelper>());
		num++;
		gameObject = NGUITools.AddChild(base.gameObject, this.ConsumablePrefab);
		gameObject.name = string.Format("{0:000}", num);
		gameObject.GetComponent<UpgradeHelper>().InitSingle(PowerupType.mysterybox);
		gameObject.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
		NGUITools.AddWidgetCollider(gameObject);
		this.cachedUpgradeHelpers.Add(gameObject.GetComponent<UpgradeHelper>());
		num++;
		gameObject = NGUITools.AddChild(base.gameObject, this.ConsumablePrefab);
		gameObject.name = string.Format("{0:000}", num);
		gameObject.GetComponent<UpgradeHelper>().InitSingle(PowerupType.headstart500);
		gameObject.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
		NGUITools.AddWidgetCollider(gameObject);
		this.cachedUpgradeHelpers.Add(gameObject.GetComponent<UpgradeHelper>());
		num++;
		gameObject = NGUITools.AddChild(base.gameObject, this.ConsumablePrefab);
		gameObject.name = string.Format("{0:000}", num);
		gameObject.GetComponent<UpgradeHelper>().InitSingle(PowerupType.headstart2000);
		gameObject.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
		NGUITools.AddWidgetCollider(gameObject);
		this.cachedUpgradeHelpers.Add(gameObject.GetComponent<UpgradeHelper>());
		num++;
		if (Missions.Instance.HasMoreMissions())
		{
			string text = string.Format("{0:000}", num);
			this.skipMissionNames[0] = text;
			if (!Missions.Instance.GetMissionInfo(0).complete)
			{
				gameObject = NGUITools.AddChild(base.gameObject, this.ConsumablePrefab);
				gameObject.name = string.Format("{0:000}", num);
				gameObject.GetComponent<UpgradeHelper>().InitSingle(PowerupType.skipmission1);
				gameObject.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
				NGUITools.AddWidgetCollider(gameObject);
				this.skipMissions[0] = gameObject;
				this.cachedUpgradeHelpers.Add(gameObject.GetComponent<UpgradeHelper>());
			}
			num++;
			text = string.Format("{0:000}", num);
			this.skipMissionNames[1] = text;
			if (!Missions.Instance.GetMissionInfo(1).complete)
			{
				gameObject = NGUITools.AddChild(base.gameObject, this.ConsumablePrefab);
				gameObject.name = string.Format("{0:000}", num);
				gameObject.GetComponent<UpgradeHelper>().InitSingle(PowerupType.skipmission2);
				gameObject.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
				NGUITools.AddWidgetCollider(gameObject);
				this.skipMissions[1] = gameObject;
				this.cachedUpgradeHelpers.Add(gameObject.GetComponent<UpgradeHelper>());
			}
			num++;
			text = string.Format("{0:000}", num);
			this.skipMissionNames[2] = text;
			if (!Missions.Instance.GetMissionInfo(2).complete)
			{
				gameObject = NGUITools.AddChild(base.gameObject, this.ConsumablePrefab);
				gameObject.name = string.Format("{0:000}", num);
				gameObject.GetComponent<UpgradeHelper>().InitSingle(PowerupType.skipmission3);
				gameObject.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
				NGUITools.AddWidgetCollider(gameObject);
				this.skipMissions[2] = gameObject;
				this.cachedUpgradeHelpers.Add(gameObject.GetComponent<UpgradeHelper>());
			}
			num++;
		}
		UILabel uilabel2 = NGUITools.AddWidget<UILabel>(base.gameObject);
		uilabel2.font = this.headerFont;
		uilabel2.text = "Upgrades";
		uilabel2.color = new Color(0f, 0.2901961f, 0.5019608f, 1f);
		uilabel2.name = string.Format("{0:000}", num);
		uilabel2.supportEncoding = false;
		uilabel2.multiLine = false;
		uilabel2.MakePixelPerfect();
		if (DeviceInfo.isHighres)
		{
			uilabel2.gameObject.transform.localScale = new Vector3(uilabel2.gameObject.transform.localScale.x / 2f, uilabel2.gameObject.transform.localScale.y / 2f, uilabel2.gameObject.transform.localScale.z);
		}
		num++;
		gameObject = NGUITools.AddChild(base.gameObject, this.PermanentPrefab);
		gameObject.name = string.Format("{0:000}", num);
		gameObject.GetComponent<UpgradeHelper>().InitPermanent(PowerupType.jetpack);
		gameObject.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
		NGUITools.AddWidgetCollider(gameObject);
		this.cachedUpgradeHelpers.Add(gameObject.GetComponent<UpgradeHelper>());
		num++;
		gameObject = NGUITools.AddChild(base.gameObject, this.PermanentPrefab);
		gameObject.name = string.Format("{0:000}", num);
		gameObject.GetComponent<UpgradeHelper>().InitPermanent(PowerupType.supersneakers);
		gameObject.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
		NGUITools.AddWidgetCollider(gameObject);
		this.cachedUpgradeHelpers.Add(gameObject.GetComponent<UpgradeHelper>());
		num++;
		gameObject = NGUITools.AddChild(base.gameObject, this.PermanentPrefab);
		gameObject.name = string.Format("{0:000}", num);
		gameObject.GetComponent<UpgradeHelper>().InitPermanent(PowerupType.coinmagnet);
		gameObject.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
		NGUITools.AddWidgetCollider(gameObject);
		this.cachedUpgradeHelpers.Add(gameObject.GetComponent<UpgradeHelper>());
		num++;
		gameObject = NGUITools.AddChild(base.gameObject, this.PermanentPrefab);
		gameObject.name = string.Format("{0:000}", num);
		gameObject.GetComponent<UpgradeHelper>().InitPermanent(PowerupType.doubleMultiplier);
		gameObject.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
		NGUITools.AddWidgetCollider(gameObject);
		this.cachedUpgradeHelpers.Add(gameObject.GetComponent<UpgradeHelper>());
		num++;
		base.gameObject.BroadcastMessage("CreatePanel", SendMessageOptions.DontRequireReceiver);
		this._table.gameObject.transform.localPosition = new Vector3(this._table.gameObject.transform.localPosition.x, 0f, this._table.gameObject.transform.localPosition.z);
		this._table.sorted = true;
		this._table.repositionNow = true;
		this._table.Reposition();
		this._parentDragPanel.RestrictWithinBounds(true);
		this._parentDragPanel.ResetPosition();
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x000359C8 File Offset: 0x00033BC8
	private void OnMissionComplete(string payload)
	{
		for (int i = 0; i < this.skipMissions.Length; i++)
		{
			if (this.skipMissions[i] != null && Missions.Instance.GetMissionInfo(i).complete)
			{
				this.cachedUpgradeHelpers.Remove(this.skipMissions[i].GetComponent<UpgradeHelper>());
				NGUITools.SetActive(this.skipMissions[i], false);
				Object.Destroy(this.skipMissions[i]);
			}
		}
		this._table.repositionNow = true;
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x00035A4C File Offset: 0x00033C4C
	private void OnMissionSetComplete()
	{
		for (int i = 0; i < this.skipMissions.Length; i++)
		{
			if (this.skipMissions[i] != null)
			{
				this.cachedUpgradeHelpers.Remove(this.skipMissions[i].GetComponent<UpgradeHelper>());
				Object.Destroy(this.skipMissions[i]);
			}
		}
		if (Missions.Instance.HasMoreMissions())
		{
			bool active = base.gameObject.active;
			if (!Missions.Instance.GetMissionInfo(0).complete)
			{
				GameObject gameObject = NGUITools.AddChild(base.gameObject, this.ConsumablePrefab);
				gameObject.name = this.skipMissionNames[0];
				gameObject.GetComponent<UpgradeHelper>().InitSingle(PowerupType.skipmission1);
				gameObject.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
				NGUITools.AddWidgetCollider(gameObject);
				this.skipMissions[0] = gameObject;
				NGUITools.SetActive(gameObject, active);
				this.cachedUpgradeHelpers.Add(gameObject.GetComponent<UpgradeHelper>());
			}
			if (!Missions.Instance.GetMissionInfo(1).complete)
			{
				GameObject gameObject2 = NGUITools.AddChild(base.gameObject, this.ConsumablePrefab);
				gameObject2.name = this.skipMissionNames[1];
				gameObject2.GetComponent<UpgradeHelper>().InitSingle(PowerupType.skipmission2);
				gameObject2.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
				NGUITools.AddWidgetCollider(gameObject2);
				this.skipMissions[1] = gameObject2;
				NGUITools.SetActive(gameObject2, active);
				this.cachedUpgradeHelpers.Add(gameObject2.GetComponent<UpgradeHelper>());
			}
			if (!Missions.Instance.GetMissionInfo(2).complete)
			{
				GameObject gameObject3 = NGUITools.AddChild(base.gameObject, this.ConsumablePrefab);
				gameObject3.name = this.skipMissionNames[2];
				gameObject3.GetComponent<UpgradeHelper>().InitSingle(PowerupType.skipmission3);
				gameObject3.GetComponent<UIDragPanelContents>().draggablePanel = this._parentDragPanel;
				NGUITools.AddWidgetCollider(gameObject3);
				this.skipMissions[2] = gameObject3;
				NGUITools.SetActive(gameObject3, active);
				this.cachedUpgradeHelpers.Add(gameObject3.GetComponent<UpgradeHelper>());
			}
		}
		if (base.gameObject.active)
		{
			this._table.repositionNow = true;
		}
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x00035C46 File Offset: 0x00033E46
	private IEnumerator SetStatic()
	{
		yield return null;
		base.transform.parent.GetComponent<UIPanel>().widgetsAreStatic = true;
		yield break;
	}

	// Token: 0x0400086C RID: 2156
	public GameObject ConsumablePrefab;

	// Token: 0x0400086D RID: 2157
	public GameObject PermanentPrefab;

	// Token: 0x0400086E RID: 2158
	public UIFont headerFont;

	// Token: 0x0400086F RID: 2159
	private UITable _table;

	// Token: 0x04000870 RID: 2160
	private UIDraggablePanel _parentDragPanel;

	// Token: 0x04000871 RID: 2161
	private GameObject[] skipMissions = new GameObject[3];

	// Token: 0x04000872 RID: 2162
	private string[] skipMissionNames = new string[3];

	// Token: 0x04000873 RID: 2163
	public List<UpgradeHelper> cachedUpgradeHelpers = new List<UpgradeHelper>(11);
}
