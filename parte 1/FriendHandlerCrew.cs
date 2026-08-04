using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class FriendHandlerCrew : MonoBehaviour
{
	// Token: 0x06000327 RID: 807 RVA: 0x0000E368 File Offset: 0x0000C568
	private void Awake()
	{
		this._grid = base.GetComponent<UIGrid>();
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0000E378 File Offset: 0x0000C578
	public void InitCrew()
	{
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
		Friend[] array = SocialManager.instance.FriendsSortedByCash();
		Debug.Log("number of friends: " + array.Length.ToString());
		int num = -1;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = NGUITools.AddChild(base.gameObject, this.FriendPrefab);
			gameObject.name = string.Format("{0:000000}", i);
			gameObject.GetComponent<FriendHelperCrew>().InitFriend(array[i], i % 2 == 0);
			num = i;
		}
		if (SocialManager.instance.facebookIsLoggedIn)
		{
			NGUITools.AddChild(base.gameObject, this.InvitePrefab).name = "invite";
		}
		if (num == -1 && !SocialManager.instance.facebookIsLoggedIn)
		{
			this.NoFriends.alpha = 1f;
			this.NoFriends.gameObject.active = true;
		}
		else
		{
			this.NoFriends.alpha = 0f;
			this.NoFriends.gameObject.active = false;
		}
		this.CrewHeader.text = "Friends (" + (num + 1).ToString() + ")";
		this._grid.sorted = false;
		this._grid.repositionNow = true;
		this._grid.Reposition();
		base.gameObject.BroadcastMessage("CreatePanel", SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x04000263 RID: 611
	public GameObject FriendPrefab;

	// Token: 0x04000264 RID: 612
	public GameObject InvitePrefab;

	// Token: 0x04000265 RID: 613
	public UILabel CrewHeader;

	// Token: 0x04000266 RID: 614
	public UILabel NoFriends;

	// Token: 0x04000267 RID: 615
	private UIGrid _grid;
}
