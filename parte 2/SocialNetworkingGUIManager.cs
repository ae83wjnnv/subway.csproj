using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class SocialNetworkingGUIManager : MonoBehaviour
{
	// Token: 0x060000D9 RID: 217 RVA: 0x0000395C File Offset: 0x00001B5C
	private void Start()
	{
		SocialNetworkingManager.twitterHomeTimelineReceived += delegate(ArrayList result)
		{
			ResultLogger.logArraylist(result);
		};
		SocialNetworkingManager.facebookReceivedCustomRequest += delegate(object result)
		{
			ResultLogger.logObject(result);
		};
		ScreenCapture.CaptureScreenshot(this.screenshotFilename);
	}

	// Token: 0x060000DA RID: 218 RVA: 0x000039BC File Offset: 0x00001BBC
	private void OnGUI()
	{
		float num = 5f;
		float num2 = 5f;
		float num3 = (float)((Screen.width < 960 && Screen.height < 960) ? 160 : 320);
		float num4 = (float)((Screen.width < 960 && Screen.height < 960) ? 30 : 70);
		float num5 = num4 + 10f;
		if (GUI.Button(new Rect(num2, num, num3, num4), "Initialize"))
		{
			FacebookBinding.init("242042899220023");
		}
		if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Is Logged In?"))
		{
			Debug.Log("Facebook is logged in: " + FacebookBinding.isLoggedIn().ToString());
		}
		if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Login"))
		{
			FacebookBinding.login();
		}
		if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Logout"))
		{
			FacebookBinding.logout();
		}
		if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Get User's Name"))
		{
			FacebookBinding.getLoggedinUsersName();
		}
		if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Post Image"))
		{
			FacebookBinding.postImage(Application.persistentDataPath + "/" + this.screenshotFilename, "im an image posted from iOS");
		}
		if (GUI.Button(new Rect(num2, num + num5 * 2f, num3, num4), "More Facebook..."))
		{
			Application.LoadLevel("SocialNetworkingtestSceneTwo");
		}
		num2 = (float)Screen.width - num3 - 5f;
		num = 5f;
		if (GUI.Button(new Rect(num2, num, num3, num4), "Initialize"))
		{
			TwitterBinding.init("VKV2NMbj7YIEGblD97ZFSw", "z1Wy3GXYL4XS9z9a2YbE4KWF3T0ynAFBwwwxZSYDI");
		}
		if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Is Logged In?"))
		{
			Debug.Log("Twitter is logged in: " + TwitterBinding.isLoggedIn().ToString());
		}
		if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Logged in Username"))
		{
			string text = TwitterBinding.loggedInUsername();
			Debug.Log("Twitter username: " + text);
		}
		if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Login with Oauth"))
		{
			TwitterBinding.showOauthLoginDialog();
		}
		if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Logout"))
		{
			TwitterBinding.logout();
		}
		if (!this.useTweetSheet)
		{
			if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Post Status Update"))
			{
				TwitterBinding.postStatusUpdate("im posting this from Unity: " + Time.deltaTime.ToString());
			}
			if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Post Status Update + Image"))
			{
				string text2 = Application.persistentDataPath + "/" + this.screenshotFilename;
				TwitterBinding.postStatusUpdate("I'm posting this from Unity with a fancy image: " + Time.deltaTime.ToString(), text2);
			}
		}
		else
		{
			if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Can User Tweet?"))
			{
				Debug.Log("Can the user tweet using the tweet sheet? " + TwitterBinding.canUserTweet().ToString());
			}
			if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Post Status Update + Image"))
			{
				TwitterBinding.showTweetComposer("I just scored 999999 points dodging trains in Subway Surfers on my iPhone. Check it out: http://redirect.kiloo.com/subwayapp.php", null);
			}
		}
		if (GUI.Button(new Rect(num2, num + num5, num3, num4), "Custom Request"))
		{
			TwitterBinding.performRequest("POST", "/statuses/update.json", new Dictionary<string, string> { { "status", "word up with a boogie boogie update" } });
		}
	}

	// Token: 0x0400004D RID: 77
	public bool useTweetSheet;

	// Token: 0x0400004E RID: 78
	private string screenshotFilename = "someScreeny.png";
}
