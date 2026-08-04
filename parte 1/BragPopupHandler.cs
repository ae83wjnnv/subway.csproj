using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class BragPopupHandler : MonoBehaviour
{
	// Token: 0x0600019A RID: 410 RVA: 0x00006358 File Offset: 0x00004558
	public void SetupBragPopup()
	{
		if (this._bragHandler == null)
		{
			this._bragHandler = UIScreenController.Instance.overlayAnchor.GetComponentInChildren(typeof(FriendHandlerBrag)) as FriendHandlerBrag;
		}
		if (this._bragHandler.bragNotifyDone)
		{
			if (this.SendMessageButton.gameObject.GetComponent<Collider>() != null)
			{
				Object.Destroy(this.SendMessageButton.gameObject.GetComponent<Collider>());
			}
			this.SendMessageButton.icon.alpha = 0.5f;
			this.SendMessageButton.GetComponent<UIButtonColor>().defaultColor = new Color(1f, 1f, 1f, 0.5f);
			this.SendMessageButton.line1.text = "Message auto-sent";
			this.SendMessageButton.line2.text = "to friends you passed";
			this.SendMessageButton.line3.text = "Disable in settings";
		}
		else
		{
			NGUITools.AddWidgetCollider(this.SendMessageButton.gameObject);
			this.SendMessageButton.GetComponent<UIButtonColor>().defaultColor = new Color(1f, 1f, 1f, 1f);
			this.SendMessageButton.icon.alpha = 1f;
			this.SendMessageButton.line1.text = "Send message";
			this.SendMessageButton.line2.text = "to selected friends (" + this._bragHandler.bragList.Count.ToString() + ")";
			this.SendMessageButton.line3.text = "Tap friend's name to deselect";
		}
		NGUITools.AddWidgetCollider(this.FacebookBragButton.gameObject);
		this.FacebookBragButton.icon.alpha = 1f;
		this.SetupFacebookButtonTexts();
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00006534 File Offset: 0x00004734
	private void SendMessageButtonClicked()
	{
		if (SocialManager.instance != null)
		{
			SocialManager.instance.BragNotify(PlayerInfo.Instance.oldHighestScore, this._bragHandler.bragList);
		}
		if (this.SendMessageButton.gameObject.GetComponent<Collider>() != null)
		{
			Object.Destroy(this.SendMessageButton.gameObject.GetComponent<Collider>());
		}
		this.SendMessageButton.GetComponent<UIButtonColor>().defaultColor = new Color(1f, 1f, 1f, 0.5f);
		this.SendMessageButton.icon.alpha = 0.5f;
		this.SendMessageButton.line1.text = "Message sent";
		this._bragHandler.bragNotifyDone = true;
		this.CheckIfCompleted();
	}

	// Token: 0x0600019C RID: 412 RVA: 0x00006600 File Offset: 0x00004800
	private void FacebookBragButtonClicked()
	{
		if (SocialManager.instance.facebookIsLoggedIn)
		{
			if (SocialManager.instance != null)
			{
				SocialManager.instance.BragFacebook(this._bragHandler.bragList);
			}
			if (this.FacebookBragButton.gameObject.GetComponent<Collider>() != null)
			{
				Object.Destroy(this.FacebookBragButton.gameObject.GetComponent<Collider>());
			}
			this.FacebookBragButton.icon.alpha = 0.5f;
			this.SendMessageButton.GetComponent<UIButtonColor>().defaultColor = new Color(1f, 1f, 1f, 0.5f);
			this._bragHandler.bragFacebookDone = true;
		}
		else
		{
			SocialManager.instance.FacebookLogin(new Action<bool>(this.FacebookLoggedIn));
		}
		this.SetupFacebookButtonTexts();
		this.CheckIfCompleted();
	}

	// Token: 0x0600019D RID: 413 RVA: 0x000066D9 File Offset: 0x000048D9
	private void CheckIfCompleted()
	{
		if (this._bragHandler.bragFacebookDone && this._bragHandler.bragNotifyDone)
		{
			UIScreenController.Instance.ClosePopup();
			this._bragHandler.CompletedBrag();
		}
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0000670A File Offset: 0x0000490A
	private void FacebookLoggedIn(bool loggedIn)
	{
		if (loggedIn)
		{
			this.SetupFacebookButtonTexts();
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00006718 File Offset: 0x00004918
	private void SetupFacebookButtonTexts()
	{
		if (!SocialManager.instance.facebookIsLoggedIn)
		{
			this.FacebookBragButton.line1.text = "Log in to Facebook";
			this.FacebookBragButton.line2.text = "to tell your friends about Subway Surfers!";
			return;
		}
		if (this._bragHandler.bragFacebookDone)
		{
			this.FacebookBragButton.line1.text = "Posted to your wall";
			this.FacebookBragButton.line2.text = "told your friends about Subway Surfers!";
			return;
		}
		this.FacebookBragButton.line1.text = "Post to your wall";
		this.FacebookBragButton.line2.text = "tell your friends about Subway Surfers!";
	}

	// Token: 0x040000D7 RID: 215
	public BragSendHelper SendMessageButton;

	// Token: 0x040000D8 RID: 216
	public BragSendHelper FacebookBragButton;

	// Token: 0x040000D9 RID: 217
	private FriendHandlerBrag _bragHandler;

	// Token: 0x040000DA RID: 218
	private Color localPlayerColor = new Color(1f, 0.85882354f, 0f, 1f);
}
