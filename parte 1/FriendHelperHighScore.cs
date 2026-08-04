using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class FriendHelperHighScore : MonoBehaviour
{
	// Token: 0x0600033B RID: 827 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
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
		this.friendName.text = ranking.ToString() + ". " + SocialManager.instance.localUserName;
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
		this._initialized = true;
		this._isLocalUser = true;
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
	public void InitFriend(Friend friend, int ranking, bool backgroundActive = false)
	{
		this._friend = friend;
		if (!backgroundActive)
		{
			this.friendBackground.alpha = 0f;
		}
		else
		{
			this.friendBackground.alpha = 0.1f;
		}
		this.friendName.text = ranking.ToString() + ". " + friend.name;
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
		this._initialized = true;
	}

	// Token: 0x0600033D RID: 829 RVA: 0x0000F190 File Offset: 0x0000D390
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

	// Token: 0x04000289 RID: 649
	private Color localPlayerColor = new Color(0.06666667f, 0.39607844f, 0.6156863f, 1f);

	// Token: 0x0400028A RID: 650
	public UILabel friendName;

	// Token: 0x0400028B RID: 651
	public UILabel friendScore;

	// Token: 0x0400028C RID: 652
	public UITexture friendPicture;

	// Token: 0x0400028D RID: 653
	public UISlicedSprite friendBackground;

	// Token: 0x0400028E RID: 654
	public Texture2D dummyImage;

	// Token: 0x0400028F RID: 655
	private bool _imageSet;

	// Token: 0x04000290 RID: 656
	private bool _isLocalUser;

	// Token: 0x04000291 RID: 657
	private Friend _friend;

	// Token: 0x04000292 RID: 658
	private bool _initialized;
}
