using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
public class FriendHelperCrew : MonoBehaviour
{
	// Token: 0x06000337 RID: 823 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
	public void InitFriend(Friend friend, bool backgroundActive = false)
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
		this.friendName.text = friend.name;
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
		GameObject gameObject;
		if (friend.gamesToCashIn >= 50)
		{
			gameObject = NGUITools.AddChild(base.gameObject, this.collectButtonPrefab);
			gameObject.GetComponent<UIButtonMessage>().target = base.gameObject;
			this._collectionIndicator = gameObject;
			this.pokeHelper.DeactivatePoke();
		}
		else
		{
			gameObject = NGUITools.AddChild(base.gameObject, this.progressPrefab);
			FriendProgressHelper component = gameObject.GetComponent<FriendProgressHelper>();
			component.label.text = friend.gamesToCashIn.ToString() + "/ 50 runs";
			component.slider.sliderValue = (float)friend.gamesToCashIn / 50f;
			if ((DateTime.UtcNow - friend.status.lastPokeTime).Days > 0)
			{
				if (friend.status.lastPokeTime == DateTime.MinValue)
				{
					SocialManager.instance.SetPokeFirstTime(friend);
					this.pokeHelper.DeactivatePoke();
				}
				else
				{
					this.pokeHelper.ActivatePoke(friend);
				}
			}
			else
			{
				this.pokeHelper.DeactivatePoke();
			}
		}
		this._collectionIndicator = gameObject;
		this._initialized = true;
	}

	// Token: 0x06000338 RID: 824 RVA: 0x0000EE58 File Offset: 0x0000D058
	public void CollectReward()
	{
		Debug.Log("Collecting reward");
		SocialManager.instance.CollectFriendReward(this._friend);
		int num = Random.Range(50, 350);
		PlayerInfo.Instance.amountOfCoins += num;
		NGUITools.SetActive(this._collectionIndicator, false);
		Object.Destroy(this._collectionIndicator);
		this._collectionIndicator = NGUITools.AddChild(base.gameObject, this.progressPrefab);
		FriendProgressHelper component = this._collectionIndicator.GetComponent<FriendProgressHelper>();
		component.label.text = this._friend.gamesToCashIn.ToString() + "/ 50 runs";
		component.slider.sliderValue = (float)this._friend.gamesToCashIn / 50f;
		UIScreenController.Instance.SpawnCollectText(component.GetCoinPouchGlobalPosition(), num.ToString());
		PlayerInfo.Instance.Save();
		SocialManager.instance.Save();
	}

	// Token: 0x06000339 RID: 825 RVA: 0x0000EF4C File Offset: 0x0000D14C
	private void Update()
	{
		if (this._initialized && !this._imageSet && this._friend.image != null)
		{
			this.friendPicture.material.mainTexture = this._friend.image;
			this._imageSet = true;
		}
	}

	// Token: 0x0400027E RID: 638
	public GameObject collectButtonPrefab;

	// Token: 0x0400027F RID: 639
	public GameObject progressPrefab;

	// Token: 0x04000280 RID: 640
	public UILabel friendName;

	// Token: 0x04000281 RID: 641
	public UITexture friendPicture;

	// Token: 0x04000282 RID: 642
	public UISlicedSprite friendBackground;

	// Token: 0x04000283 RID: 643
	public FriendCrewPokeHelper pokeHelper;

	// Token: 0x04000284 RID: 644
	private GameObject _collectionIndicator;

	// Token: 0x04000285 RID: 645
	public Texture2D dummyImage;

	// Token: 0x04000286 RID: 646
	private bool _imageSet;

	// Token: 0x04000287 RID: 647
	private Friend _friend;

	// Token: 0x04000288 RID: 648
	private bool _initialized;
}
