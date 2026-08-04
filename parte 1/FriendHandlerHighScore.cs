using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class FriendHandlerHighScore : MonoBehaviour
{
	// Token: 0x0600032A RID: 810 RVA: 0x0000E550 File Offset: 0x0000C750
	private void Awake()
	{
		this._grid = base.GetComponent<UIGrid>();
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0000E55E File Offset: 0x0000C75E
	public void LoadHighScore()
	{
		this.LoadFriends();
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0000E568 File Offset: 0x0000C768
	private void LoadFriends()
	{
		base.transform.parent.GetComponent<UIPanel>().widgetsAreStatic = false;
		if (this._grid == null)
		{
			this._grid = base.GetComponent<UIGrid>();
		}
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			NGUITools.SetActive(transform.gameObject, false);
			Object.Destroy(transform.gameObject);
		}
		Friend[] array = SocialManager.instance.FriendsSortedByScore();
		Transform transform2 = base.transform;
		bool flag = false;
		int num = 1;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = NGUITools.AddChild(base.gameObject, this.FriendPrefab);
			gameObject.name = string.Format("{0:000000}{1}", array[i].score, num);
			FriendHelperHighScore friendHelperHighScore = gameObject.GetComponent<FriendHelperHighScore>();
			if (!flag && PlayerInfo.Instance.highestScore >= array[i].score)
			{
				transform2 = gameObject.transform;
				friendHelperHighScore.InitLocalUser(num, num % 2 == 0);
				num++;
				flag = true;
				gameObject = NGUITools.AddChild(base.gameObject, this.FriendPrefab);
				friendHelperHighScore = gameObject.GetComponent<FriendHelperHighScore>();
				gameObject.name = string.Format("{0:000000}{1}", array[i].score, num);
			}
			friendHelperHighScore.InitFriend(array[i], num, num % 2 == 0);
			num++;
		}
		if (!flag)
		{
			GameObject gameObject2 = NGUITools.AddChild(base.gameObject, this.FriendPrefab);
			FriendHelperHighScore component = gameObject2.GetComponent<FriendHelperHighScore>();
			transform2 = gameObject2.transform;
			component.InitLocalUser(num, num % 2 == 0);
			num++;
		}
		UIPanel component2 = this._grid.transform.parent.GetComponent<UIPanel>();
		Vector3 localPosition = this._grid.transform.parent.localPosition;
		component2.clipRange = this.defaultPanelClipping;
		this._grid.sorted = false;
		this._grid.repositionNow = true;
		this._grid.Reposition();
		component2.transform.localPosition = new Vector3(localPosition.x, 0f - transform2.localPosition.y, localPosition.z);
		component2.clipRange = new Vector4(this.defaultPanelClipping.x, this.defaultPanelClipping.y + transform2.localPosition.y, this.defaultPanelClipping.z, this.defaultPanelClipping.w);
		component2.GetComponent<UIDraggablePanel>().RestrictWithinBounds(true);
		base.gameObject.BroadcastMessage("CreatePanel", SendMessageOptions.DontRequireReceiver);
		base.StartCoroutine(this.SetStatic());
	}

	// Token: 0x0600032D RID: 813 RVA: 0x0000E834 File Offset: 0x0000CA34
	private IEnumerator SetStatic()
	{
		yield return null;
		base.transform.parent.GetComponent<UIPanel>().widgetsAreStatic = true;
		yield break;
	}

	// Token: 0x04000268 RID: 616
	public GameObject FriendPrefab;

	// Token: 0x04000269 RID: 617
	private Color myColor = new Color(0.06666667f, 0.39607844f, 0.6156863f, 1f);

	// Token: 0x0400026A RID: 618
	private UIGrid _grid;

	// Token: 0x0400026B RID: 619
	private Vector4 defaultPanelClipping = new Vector4(-5.5f, 240f, 277f, 300f);
}
