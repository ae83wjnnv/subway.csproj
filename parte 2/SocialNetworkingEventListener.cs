using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class SocialNetworkingEventListener : MonoBehaviour
{
	// Token: 0x060000BD RID: 189 RVA: 0x00003408 File Offset: 0x00001608
	private void OnEnable()
	{
		SocialNetworkingManager.twitterLogin += this.twitterLogin;
		SocialNetworkingManager.twitterLoginFailed += this.twitterLoginFailed;
		SocialNetworkingManager.twitterPost += this.twitterPost;
		SocialNetworkingManager.twitterPostFailed += this.twitterPostFailed;
		SocialNetworkingManager.twitterHomeTimelineReceived += this.twitterHomeTimelineReceived;
		SocialNetworkingManager.twitterHomeTimelineFailed += this.twitterHomeTimelineFailed;
		SocialNetworkingManager.twitterRequestDidFinishEvent += this.twitterRequestDidFinishEvent;
		SocialNetworkingManager.twitterRequestDidFailEvent += this.twitterRequestDidFailEvent;
		SocialNetworkingManager.facebookLogin += this.facebookLogin;
		SocialNetworkingManager.facebookLoginFailed += this.facebookLoginFailed;
		SocialNetworkingManager.facebookDidLogoutEvent += this.facebookDidLogoutEvent;
		SocialNetworkingManager.facebookDidExtendTokenEvent += this.facebookDidExtendTokenEvent;
		SocialNetworkingManager.facebookSessionInvalidatedEvent += this.facebookSessionInvalidatedEvent;
		SocialNetworkingManager.facebookReceivedUsername += this.facebookReceivedUsername;
		SocialNetworkingManager.facebookUsernameRequestFailed += this.facebookUsernameRequestFailed;
		SocialNetworkingManager.facebookPost += this.facebookPost;
		SocialNetworkingManager.facebookPostFailed += this.facebookPostFailed;
		SocialNetworkingManager.facebookReceivedFriends += this.facebookReceivedFriends;
		SocialNetworkingManager.facebookFriendRequestFailed += this.facebookFriendRequestFailed;
		SocialNetworkingManager.facebookDialogCompleted += this.facebokDialogCompleted;
		SocialNetworkingManager.facebookDialogCompletedWithUrl += this.facebookDialogCompletedWithUrl;
		SocialNetworkingManager.facebookDialogDidntComplete += this.facebookDialogDidntComplete;
		SocialNetworkingManager.facebookDialogFailed += this.facebookDialogFailed;
		SocialNetworkingManager.facebookReceivedCustomRequest += this.facebookReceivedCustomRequest;
		SocialNetworkingManager.facebookCustomRequestFailed += this.facebookCustomRequestFailed;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x000035C0 File Offset: 0x000017C0
	private void OnDisable()
	{
		SocialNetworkingManager.twitterLogin -= this.twitterLogin;
		SocialNetworkingManager.twitterLoginFailed -= this.twitterLoginFailed;
		SocialNetworkingManager.twitterPost -= this.twitterPost;
		SocialNetworkingManager.twitterPostFailed -= this.twitterPostFailed;
		SocialNetworkingManager.twitterHomeTimelineReceived -= this.twitterHomeTimelineReceived;
		SocialNetworkingManager.twitterHomeTimelineFailed -= this.twitterHomeTimelineFailed;
		SocialNetworkingManager.twitterRequestDidFinishEvent -= this.twitterRequestDidFinishEvent;
		SocialNetworkingManager.twitterRequestDidFailEvent -= this.twitterRequestDidFailEvent;
		SocialNetworkingManager.facebookLogin -= this.facebookLogin;
		SocialNetworkingManager.facebookLoginFailed -= this.facebookLoginFailed;
		SocialNetworkingManager.facebookDidLogoutEvent -= this.facebookDidLogoutEvent;
		SocialNetworkingManager.facebookDidExtendTokenEvent -= this.facebookDidExtendTokenEvent;
		SocialNetworkingManager.facebookSessionInvalidatedEvent -= this.facebookSessionInvalidatedEvent;
		SocialNetworkingManager.facebookReceivedUsername -= this.facebookReceivedUsername;
		SocialNetworkingManager.facebookUsernameRequestFailed -= this.facebookUsernameRequestFailed;
		SocialNetworkingManager.facebookPost -= this.facebookPost;
		SocialNetworkingManager.facebookPostFailed -= this.facebookPostFailed;
		SocialNetworkingManager.facebookReceivedFriends -= this.facebookReceivedFriends;
		SocialNetworkingManager.facebookFriendRequestFailed += this.facebookFriendRequestFailed;
		SocialNetworkingManager.facebookDialogCompleted -= this.facebokDialogCompleted;
		SocialNetworkingManager.facebookDialogCompletedWithUrl -= this.facebookDialogCompletedWithUrl;
		SocialNetworkingManager.facebookDialogDidntComplete -= this.facebookDialogDidntComplete;
		SocialNetworkingManager.facebookDialogFailed -= this.facebookDialogFailed;
		SocialNetworkingManager.facebookReceivedCustomRequest -= this.facebookReceivedCustomRequest;
		SocialNetworkingManager.facebookCustomRequestFailed -= this.facebookCustomRequestFailed;
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00003776 File Offset: 0x00001976
	private void twitterLogin()
	{
		Debug.Log("Successfully logged in to Twitter");
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00003782 File Offset: 0x00001982
	private void twitterLoginFailed(string error)
	{
		Debug.Log("Twitter login failed: " + error);
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00003794 File Offset: 0x00001994
	private void twitterPost()
	{
		Debug.Log("Successfully posted to Twitter");
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x000037A0 File Offset: 0x000019A0
	private void twitterPostFailed(string error)
	{
		Debug.Log("Twitter post failed: " + error);
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x000037B2 File Offset: 0x000019B2
	private void twitterHomeTimelineFailed(string error)
	{
		Debug.Log("Twitter HomeTimeline failed: " + error);
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x000037C4 File Offset: 0x000019C4
	private void twitterHomeTimelineReceived(ArrayList result)
	{
		Debug.Log("received home timeline with tweet count: " + result.Count.ToString());
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x000037EE File Offset: 0x000019EE
	private void twitterRequestDidFailEvent(string error)
	{
		Debug.Log("twitterRequestDidFailEvent: " + error);
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00003800 File Offset: 0x00001A00
	private void twitterRequestDidFinishEvent(object result)
	{
		if (result != null)
		{
			Debug.Log("twitterRequestDidFinishEvent: " + result.GetType().ToString());
			return;
		}
		Debug.Log("twitterRequestDidFinishEvent with no data");
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x0000382A File Offset: 0x00001A2A
	private void facebookLogin()
	{
		Debug.Log("Successfully logged in to Facebook");
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00003836 File Offset: 0x00001A36
	private void facebookLoginFailed(string error)
	{
		Debug.Log("Facebook login failed: " + error);
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00003848 File Offset: 0x00001A48
	private void facebookDidLogoutEvent()
	{
		Debug.Log("facebookDidLogoutEvent");
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00003854 File Offset: 0x00001A54
	private void facebookDidExtendTokenEvent(DateTime newExpiry)
	{
		Debug.Log("facebookDidExtendTokenEvent: " + newExpiry.ToString());
	}

	// Token: 0x060000CB RID: 203 RVA: 0x0000386C File Offset: 0x00001A6C
	private void facebookSessionInvalidatedEvent()
	{
		Debug.Log("facebookSessionInvalidatedEvent");
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00003878 File Offset: 0x00001A78
	private void facebookReceivedUsername(string username)
	{
		Debug.Log("Facebook logged in users name: " + username);
	}

	// Token: 0x060000CD RID: 205 RVA: 0x0000388A File Offset: 0x00001A8A
	private void facebookUsernameRequestFailed(string error)
	{
		Debug.Log("Facebook failed to receive username: " + error);
	}

	// Token: 0x060000CE RID: 206 RVA: 0x0000389C File Offset: 0x00001A9C
	private void facebookPost()
	{
		Debug.Log("Successfully posted to Facebook");
	}

	// Token: 0x060000CF RID: 207 RVA: 0x000038A8 File Offset: 0x00001AA8
	private void facebookPostFailed(string error)
	{
		Debug.Log("Facebook post failed: " + error);
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x000038BC File Offset: 0x00001ABC
	private void facebookReceivedFriends(ArrayList result)
	{
		Debug.Log("received total friends: " + result.Count.ToString());
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x000038E6 File Offset: 0x00001AE6
	private void facebookFriendRequestFailed(string error)
	{
		Debug.Log("FfacebookFriendRequestFailed: " + error);
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x000038F8 File Offset: 0x00001AF8
	private void facebokDialogCompleted()
	{
		Debug.Log("facebokDialogCompleted");
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00003904 File Offset: 0x00001B04
	private void facebookDialogCompletedWithUrl(string url)
	{
		Debug.Log("facebookDialogCompletedWithUrl: " + url);
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00003916 File Offset: 0x00001B16
	private void facebookDialogDidntComplete()
	{
		Debug.Log("facebookDialogDidntComplete");
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00003922 File Offset: 0x00001B22
	private void facebookDialogFailed(string error)
	{
		Debug.Log("facebookDialogFailed: " + error);
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00003934 File Offset: 0x00001B34
	private void facebookReceivedCustomRequest(object obj)
	{
		Debug.Log("facebookReceivedCustomRequest");
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00003940 File Offset: 0x00001B40
	private void facebookCustomRequestFailed(string error)
	{
		Debug.Log("facebookCustomRequestFailed failed: " + error);
	}
}
