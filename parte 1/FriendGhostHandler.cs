using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class FriendGhostHandler : MonoBehaviour
{
	// Token: 0x06000309 RID: 777 RVA: 0x0000D6B8 File Offset: 0x0000B8B8
	private void Init()
	{
		Game instance = Game.Instance;
		instance.OnGameStarted = (Action)Delegate.Combine(instance.OnGameStarted, new Action(this.NewGame));
		Game instance2 = Game.Instance;
		instance2.OnGameEnded = (Action)Delegate.Combine(instance2.OnGameEnded, new Action(this.GameOver));
		this.inited = true;
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0000D718 File Offset: 0x0000B918
	private void OnDestroy()
	{
		Game instance = Game.Instance;
		instance.OnGameStarted = (Action)Delegate.Remove(instance.OnGameStarted, new Action(this.NewGame));
		Game instance2 = Game.Instance;
		instance2.OnGameEnded = (Action)Delegate.Remove(instance2.OnGameEnded, new Action(this.GameOver));
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0000D771 File Offset: 0x0000B971
	private void Awake()
	{
		this.Init();
		this.NewGame();
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0000D780 File Offset: 0x0000B980
	public void NewGame()
	{
		if (this._gameRunning)
		{
			return;
		}
		this._gameRunning = true;
		this._localUserInserted = false;
		this._currentThreshold = 0;
		GameStats.Instance.ResetScore();
		if (SocialManager.instance.consolidatedFriendsCompleted)
		{
			this.friendsDescending = SocialManager.instance.FriendsSortedByScore();
			if (this.friendsDescending != null)
			{
				this._currentFriend = this.friendsDescending.Length - 1;
			}
		}
		this.helper.NewGame();
		this.SetNewFriend();
		this.helper.AnimateIn();
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0000D805 File Offset: 0x0000BA05
	private void Update()
	{
		if (!this.helper.animatingNow && this._gameRunning && !this.helper.noFriendsLeftToGhost && GameStats.Instance.score > this._currentThreshold)
		{
			this.PassThreshold();
		}
	}

	// Token: 0x0600030E RID: 782 RVA: 0x0000D841 File Offset: 0x0000BA41
	private void PassThreshold()
	{
		this.helper.AnimateOut();
	}

	// Token: 0x0600030F RID: 783 RVA: 0x0000D84E File Offset: 0x0000BA4E
	public void FinishedAnimatingOut()
	{
		if (this._gameRunning)
		{
			this.SetNewFriend();
		}
	}

	// Token: 0x06000310 RID: 784 RVA: 0x0000D860 File Offset: 0x0000BA60
	public void SetNewFriend()
	{
		Debug.Log("CurrentPlayer: " + this._currentFriend.ToString());
		if (!this._gameRunning)
		{
			return;
		}
		bool flag = false;
		if (this.friendsDescending != null || this._currentFriend != -1)
		{
			int num = -1;
			for (int i = this._currentFriend; i >= 0; i--)
			{
				if (this.friendsDescending[i].score > GameStats.Instance.score && this.friendsDescending[i].score > PlayerInfo.Instance.highestScore)
				{
					num = i;
					break;
				}
			}
			this._currentFriend = num;
			if (this._currentFriend == -1)
			{
				if (PlayerInfo.Instance.highestScore > GameStats.Instance.score && this.InsertLocalUser())
				{
					flag = true;
				}
			}
			else if (PlayerInfo.Instance.highestScore < this.friendsDescending[this._currentFriend].score)
			{
				if (this.InsertLocalUser())
				{
					flag = true;
				}
				else if (this.InsertFriend())
				{
					flag = true;
				}
			}
			else if (this.InsertFriend())
			{
				flag = true;
			}
		}
		else if (!this._localUserInserted && this.InsertLocalUser())
		{
			flag = true;
		}
		if (flag)
		{
			this.helper.AnimateIn();
			return;
		}
		this.helper.NoFriendsLeft();
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0000D98F File Offset: 0x0000BB8F
	public void GameOver()
	{
		this._gameRunning = false;
		this.helper.GameOver();
	}

	// Token: 0x06000312 RID: 786 RVA: 0x0000D9A4 File Offset: 0x0000BBA4
	private bool InsertLocalUser()
	{
		if (PlayerInfo.Instance.highestScore > GameStats.Instance.score && !this._localUserInserted)
		{
			if (SocialManager.instance.localUserImage != null && SocialManager.instance.consolidatedFriendsCompleted)
			{
				this.helper.picture.material.mainTexture = SocialManager.instance.localUserImage;
			}
			else
			{
				this.helper.picture.material.mainTexture = this.dummyImage;
			}
			this.helper.points.text = PlayerInfo.Instance.highestScore.ToString();
			this.helper.points.color = this.localPlayerColor;
			this._localUserInserted = true;
			this._currentThreshold = PlayerInfo.Instance.highestScore;
			Debug.Log("Inserted local player: " + PlayerInfo.Instance.highestScore.ToString());
			return true;
		}
		return false;
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0000DAA4 File Offset: 0x0000BCA4
	private bool InsertFriend()
	{
		int currentFriend = this._currentFriend;
		if (currentFriend < 0 || currentFriend >= this.friendsDescending.Length)
		{
			Debug.LogError("Tried to insert a friend outside the array");
			return false;
		}
		Missions.Instance.PlayerDidThis(Missions.MissionTarget.BeatFriends, 1);
		Debug.Log("Inserted friend: " + currentFriend.ToString());
		Friend friend = this.friendsDescending[currentFriend];
		if (friend.score > GameStats.Instance.score)
		{
			if (friend.image != null)
			{
				this.helper.picture.material.mainTexture = friend.image;
			}
			else
			{
				this.helper.picture.material.mainTexture = this.dummyImage;
			}
			this.helper.points.text = friend.score.ToString();
			this.helper.points.color = this.friendColor;
			this._currentThreshold = friend.score;
			return true;
		}
		return false;
	}

	// Token: 0x0400023E RID: 574
	public FriendGhostHelper helper;

	// Token: 0x0400023F RID: 575
	public Texture dummyImage;

	// Token: 0x04000240 RID: 576
	private Friend[] friendsDescending;

	// Token: 0x04000241 RID: 577
	private bool _localUserInserted;

	// Token: 0x04000242 RID: 578
	private bool inited;

	// Token: 0x04000243 RID: 579
	private bool _gameRunning;

	// Token: 0x04000244 RID: 580
	private int _currentThreshold;

	// Token: 0x04000245 RID: 581
	private int _currentFriend = -1;

	// Token: 0x04000246 RID: 582
	private Color localPlayerColor = new Color(1f, 0.85882354f, 0f, 1f);

	// Token: 0x04000247 RID: 583
	private Color friendColor = Color.white;
}
