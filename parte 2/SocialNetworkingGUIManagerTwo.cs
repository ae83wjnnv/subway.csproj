using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class SocialNetworkingGUIManagerTwo : MonoBehaviour
{
	// Token: 0x060000DC RID: 220 RVA: 0x00003D64 File Offset: 0x00001F64
	private void Start()
	{
		SocialNetworkingManager.facebookReceivedFriends += delegate(ArrayList result)
		{
			ResultLogger.logArraylist(result);
		};
		SocialNetworkingManager.facebookReceivedCustomRequest += delegate(object result)
		{
			ResultLogger.logObject(result);
		};
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00003DBC File Offset: 0x00001FBC
	private void OnGUI()
	{
		float num = 5f;
		float num2 = 5f;
		float num3 = (float)((Screen.width < 960 && Screen.height < 960) ? 160 : 320);
		float num4 = (float)((Screen.width < 960 && Screen.height < 960) ? 30 : 70);
		float num5 = num4 + 10f;
		if (GUI.Button(new Rect(num2, num, num3, num4), "Post Message"))
		{
			FacebookBinding.postMessage("im posting this from Unity: " + Time.deltaTime.ToString());
		}
		if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Post Message & More"))
		{
			FacebookBinding.postMessageWithLinkAndLinkToImage("link post from Unity: " + Time.deltaTime.ToString(), "http://prime31.com", "Prime31 Studios", "http://prime31.com/assets/images/prime31logo.png", "Prime31 Logo");
		}
		if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Get Friends"))
		{
			Hashtable hashtable = new Hashtable();
			hashtable["fields"] = "id,name,picture";
			FacebookBinding.graphRequest("me/friends", "GET", hashtable);
		}
		if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Dialog With Options"))
		{
			FacebookBinding.showPostMessageDialogWithOptions("http://prime31.com", "Prime31 Studios", string.Empty, string.Empty);
		}
		if (GUI.Button(new Rect(num2, num += num5, num3, num4), "Custom Feed Dialog"))
		{
			FacebookBinding.showPostMessageDialogWithOptions(new Hashtable
			{
				{ "link", "http://hoodrunner.kiloo.com" },
				{ "picture", "http://prime31.com/assets/images/prime31logo.png" },
				{ "name", "Hood Runner" },
				{ "caption", "New Hood Runner Score" },
				{ "message", "In your face @[671337364:Jeppe]" },
				{ "description", "@[1692528651:Lars] just beat the score of @[671337364:Jeppe] in Hood Runner" }
			});
		}
		if (GUI.Button(new Rect(num2, num + num5 * 2f, num3, num4), "Back"))
		{
			Application.LoadLevel("SocialNetworkingtestScene");
		}
		float num6 = (float)Screen.width - num3 - 5f;
		num = 5f;
		if (GUI.Button(new Rect(num6, num, num3, num4), "Graph Request (me)"))
		{
			FacebookBinding.graphRequest("me", "GET", new Hashtable());
		}
		if (GUI.Button(new Rect(num6, num += num5, num3, num4), "Post Score"))
		{
			FacebookBinding.graphRequest("me/scores", "GET", new Hashtable { { "score", "2500" } });
		}
		if (GUI.Button(new Rect(num6, num += num5, num3, num4), "Custom REST Request"))
		{
			FacebookBinding.restRequest("fql.query", "POST", new Hashtable { { "query", "SELECT uid,name FROM user WHERE uid=4" } });
		}
		if (GUI.Button(new Rect(num6, num + num5, num3, num4), "Custom Dialog"))
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string> { { "message", "Check out this great app!" } };
			FacebookBinding.showDialog("apprequests", dictionary);
		}
	}
}
