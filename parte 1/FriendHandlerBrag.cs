using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class FriendHandlerBrag : MonoBehaviour
{
	// Token: 0x17000028 RID: 40
	// (get) Token: 0x0600031F RID: 799 RVA: 0x0000DE6A File Offset: 0x0000C06A
	public List<Friend> bragList
	{
		get
		{
			if (this._bragList != null)
			{
				return this._bragList;
			}
			return null;
		}
	}

	// Token: 0x06000320 RID: 800 RVA: 0x0000DE7C File Offset: 0x0000C07C
	private void Awake()
	{
		this._grid = base.GetComponent<UIGrid>();
		NGUITools.SetActive(this.gettingLabel.gameObject, false);
	}

	// Token: 0x06000321 RID: 801 RVA: 0x0000DE9C File Offset: 0x0000C09C
	public void ShowGettingReadyLabel()
	{
		NGUITools.SetActive(this.gettingLabel.gameObject, true);
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			NGUITools.SetActive(transform.gameObject, false);
			Object.Destroy(transform.gameObject);
		}
	}

	// Token: 0x06000322 RID: 802 RVA: 0x0000DF14 File Offset: 0x0000C114
	public void ShowBragList()
	{
		Transform transform = base.transform.parent;
		if (this._grid == null)
		{
			this._grid = base.GetComponent<UIGrid>();
		}
		foreach (object obj in this._grid.transform)
		{
			Transform transform2 = (Transform)obj;
			NGUITools.SetActive(transform2.gameObject, false);
			Object.Destroy(transform2.gameObject);
		}
		NGUITools.SetActive(this.gettingLabel.gameObject, false);
		Friend[] array = SocialManager.instance.FriendsSortedByScore();
		bool flag = false;
		int num = 1;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = NGUITools.AddChild(base.gameObject, this.friendBragPrefab);
			gameObject.name = string.Format("{0:000000}{1}", array[i].score, num);
			FriendHelperBrag friendHelperBrag = gameObject.GetComponent<FriendHelperBrag>();
			if (!flag && PlayerInfo.Instance.highestScore >= array[i].score)
			{
				transform = gameObject.transform;
				friendHelperBrag.InitLocalUser(num, num % 2 == 0);
				num++;
				flag = true;
				this.playerHelper = friendHelperBrag;
				gameObject = NGUITools.AddChild(base.gameObject, this.friendBragPrefab);
				gameObject.name = string.Format("{0:000000}", array[i].score);
				friendHelperBrag = gameObject.GetComponent<FriendHelperBrag>();
			}
			bool flag2 = array[i].score <= PlayerInfo.Instance.highestScore && array[i].score > PlayerInfo.Instance.oldHighestScore;
			friendHelperBrag.InitFriend(array[i], num, flag2, num % 2 == 0);
			if (flag2)
			{
				this.AddBragFriend(array[i]);
			}
			num++;
		}
		if (!flag)
		{
			GameObject gameObject2 = NGUITools.AddChild(base.gameObject, this.friendBragPrefab);
			FriendHelperBrag friendHelperBrag2 = (this.playerHelper = gameObject2.GetComponent<FriendHelperBrag>());
			transform = gameObject2.transform;
			friendHelperBrag2.InitLocalUser(num, num % 2 == 0);
			num++;
		}
		if (this.bragList.Count == 0)
		{
			this.bragButton.DisableButton();
		}
		else
		{
			this.bragButton.EnableButton();
			this.playerHelper.SetRankMovement(true);
		}
		UIPanel component = this._grid.transform.parent.GetComponent<UIPanel>();
		Vector3 zero = Vector3.zero;
		component.transform.localPosition = zero;
		Vector3 vector = zero;
		component.clipRange = this.defaultPanelClipping;
		this._grid.sorted = false;
		this._grid.repositionNow = true;
		this._grid.Reposition();
		component.transform.localPosition = new Vector3(vector.x, 0f - transform.localPosition.y, vector.z);
		component.clipRange = new Vector4(this.defaultPanelClipping.x, this.defaultPanelClipping.y + transform.localPosition.y, this.defaultPanelClipping.z, this.defaultPanelClipping.w);
		component.GetComponent<UIDraggablePanel>().RestrictWithinBounds(true);
		base.gameObject.BroadcastMessage("CreatePanel", SendMessageOptions.DontRequireReceiver);
		if (Settings.optionAutoMessage)
		{
			SocialManager.instance.BragNotify(PlayerInfo.Instance.oldHighestScore, this.bragList);
			this.bragNotifyDone = true;
		}
	}

	// Token: 0x06000323 RID: 803 RVA: 0x0000E278 File Offset: 0x0000C478
	public void AddBragFriend(Friend friend)
	{
		if (!this._bragList.Contains(friend))
		{
			this._bragList.Add(friend);
			if (!this.bragButton.buttonEnabled)
			{
				this.bragButton.EnableButton();
			}
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x0000E2AC File Offset: 0x0000C4AC
	public void RemoveBragFriend(Friend friend)
	{
		if (this._bragList.Contains(friend))
		{
			this._bragList.Remove(friend);
			if (this._bragList.Count == 0)
			{
				this.bragButton.DisableButton();
			}
		}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x0000E2E1 File Offset: 0x0000C4E1
	public void CompletedBrag()
	{
		this.bragButton.DisableButton();
		base.gameObject.BroadcastMessage("CompletedBragging", SendMessageOptions.DontRequireReceiver);
		this._bragList.Clear();
	}

	// Token: 0x04000259 RID: 601
	public GameObject friendBragPrefab;

	// Token: 0x0400025A RID: 602
	public UILabel gettingLabel;

	// Token: 0x0400025B RID: 603
	public BragButtonHelper bragButton;

	// Token: 0x0400025C RID: 604
	private FriendHelperBrag playerHelper;

	// Token: 0x0400025D RID: 605
	private Color myColor = new Color(0.06666667f, 0.39607844f, 0.6156863f, 1f);

	// Token: 0x0400025E RID: 606
	private UIGrid _grid;

	// Token: 0x0400025F RID: 607
	private List<Friend> _bragList = new List<Friend>();

	// Token: 0x04000260 RID: 608
	private Vector4 defaultPanelClipping = new Vector4(0f, 152f, 295.5f, 121f);

	// Token: 0x04000261 RID: 609
	[HideInInspector]
	public bool bragNotifyDone;

	// Token: 0x04000262 RID: 610
	[HideInInspector]
	public bool bragFacebookDone;
}
