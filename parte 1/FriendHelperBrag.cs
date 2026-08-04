using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class FriendHelperBrag : MonoBehaviour
{
	// Token: 0x0600032F RID: 815 RVA: 0x0000E895 File Offset: 0x0000CA95
	private void Start()
	{
		this._bragHandler = base.transform.parent.GetComponent<FriendHandlerBrag>();
	}

	// Token: 0x06000330 RID: 816 RVA: 0x0000E8B0 File Offset: 0x0000CAB0
	public void InitLocalUser(int ranking, bool backgroundActive = false)
	{
		if (!backgroundActive)
		{
			this.friendBackground.alpha = 0f;
		}
		else
		{
			this.friendBackground.alpha = 0.1f;
		}
		this._braggable = false;
		this.friendRank.text = ranking.ToString();
		this.friendRank.color = this.localPlayerColor;
		this.friendName.text = SocialManager.instance.localUserName;
		this.friendName.color = this.localPlayerColor;
		this.friendScore.text = PlayerInfo.Instance.highestScore.ToString();
		this.friendScore.color = this.localPlayerColor;
		this.friendPicture.material = new Material(Shader.Find("Unlit/Transparent Colored"));
		if (SocialManager.instance.localUserImage != null)
		{
			this.friendPicture.material.mainTexture = SocialManager.instance.localUserImage;
			this._imageSet = true;
		}
		else
		{
			this.friendPicture.material.mainTexture = this.dummyImage;
		}
		this.rankMovementIcon.spriteName = this.rankSame;
		this._initialized = true;
		this._isLocalUser = true;
	}

	// Token: 0x06000331 RID: 817 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
	public void SetRankMovement(bool passedFriend)
	{
		if (passedFriend)
		{
			this.rankMovementIcon.spriteName = this.rankUp;
		}
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0000E9FC File Offset: 0x0000CBFC
	public void InitFriend(Friend friend, int ranking, bool braggable = false, bool backgroundActive = false)
	{
		this._friend = friend;
		this._braggable = braggable;
		if (!backgroundActive)
		{
			this.friendBackground.alpha = 0f;
		}
		else
		{
			this.friendBackground.alpha = 0.1f;
		}
		this.friendRank.text = ranking.ToString();
		this.friendName.text = friend.name;
		this.friendScore.text = friend.score.ToString();
		this.friendPicture.material = new Material(Shader.Find("Unlit/Transparent Colored"));
		if (this._friend.image != null)
		{
			this.friendPicture.material.mainTexture = friend.image;
			this._imageSet = true;
		}
		else
		{
			this.friendPicture.material.mainTexture = this.dummyImage;
		}
		if (this._braggable)
		{
			NGUITools.AddWidgetCollider(base.gameObject);
			this.rankMovementIcon.spriteName = this.rankDown;
			this.bragActive = true;
		}
		else
		{
			this.rankMovementIcon.spriteName = this.rankSame;
		}
		this._initialized = true;
	}

	// Token: 0x06000333 RID: 819 RVA: 0x0000EB20 File Offset: 0x0000CD20
	private void OnClick()
	{
		if (this._braggable)
		{
			if (this.bragActive)
			{
				this.bragActive = false;
				this._bragHandler.RemoveBragFriend(this._friend);
				this.rankMovementIcon.alpha = 0.3f;
				return;
			}
			this.bragActive = true;
			this._bragHandler.AddBragFriend(this._friend);
			this.rankMovementIcon.alpha = 1f;
		}
	}

	// Token: 0x06000334 RID: 820 RVA: 0x0000EB8E File Offset: 0x0000CD8E
	private void CompletedBragging()
	{
		if (this._braggable && base.gameObject.GetComponent<Collider>() != null)
		{
			Object.Destroy(base.gameObject.GetComponent<Collider>());
		}
	}

	// Token: 0x06000335 RID: 821 RVA: 0x0000EBBC File Offset: 0x0000CDBC
	private void Update()
	{
		if (!this._initialized || this._imageSet)
		{
			return;
		}
		if (this._isLocalUser)
		{
			if (SocialManager.instance.localUserImage != null)
			{
				this.friendPicture.material.mainTexture = SocialManager.instance.localUserImage;
				this._imageSet = true;
				return;
			}
		}
		else if (this._friend.image != null)
		{
			this.friendPicture.material.mainTexture = this._friend.image;
			this._imageSet = true;
		}
	}

	// Token: 0x0400026C RID: 620
	private Color localPlayerColor = new Color(0.06666667f, 0.39607844f, 0.6156863f, 1f);

	// Token: 0x0400026D RID: 621
	public UILabel friendRank;

	// Token: 0x0400026E RID: 622
	public UILabel friendName;

	// Token: 0x0400026F RID: 623
	public UILabel friendScore;

	// Token: 0x04000270 RID: 624
	public UITexture friendPicture;

	// Token: 0x04000271 RID: 625
	public UISlicedSprite friendBackground;

	// Token: 0x04000272 RID: 626
	public UISprite rankMovementIcon;

	// Token: 0x04000273 RID: 627
	private string rankUp = "icon_rank_up";

	// Token: 0x04000274 RID: 628
	private string rankSame = "icon_rank_same";

	// Token: 0x04000275 RID: 629
	private string rankDown = "icon_rank_down";

	// Token: 0x04000276 RID: 630
	private bool bragActive;

	// Token: 0x04000277 RID: 631
	public Texture2D dummyImage;

	// Token: 0x04000278 RID: 632
	private bool _imageSet;

	// Token: 0x04000279 RID: 633
	private bool _isLocalUser;

	// Token: 0x0400027A RID: 634
	private Friend _friend;

	// Token: 0x0400027B RID: 635
	private bool _initialized;

	// Token: 0x0400027C RID: 636
	private bool _braggable;

	// Token: 0x0400027D RID: 637
	private FriendHandlerBrag _bragHandler;
}
