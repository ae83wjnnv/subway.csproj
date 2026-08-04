using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class SocialNetworkingManager : MonoBehaviour
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x060000DF RID: 223 RVA: 0x000040C4 File Offset: 0x000022C4
	// (remove) Token: 0x060000E0 RID: 224 RVA: 0x000040F8 File Offset: 0x000022F8
	public static event Action twitterLogin;

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x060000E1 RID: 225 RVA: 0x0000412C File Offset: 0x0000232C
	// (remove) Token: 0x060000E2 RID: 226 RVA: 0x00004160 File Offset: 0x00002360
	public static event Action<string> twitterLoginFailed;

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x060000E3 RID: 227 RVA: 0x00004194 File Offset: 0x00002394
	// (remove) Token: 0x060000E4 RID: 228 RVA: 0x000041C8 File Offset: 0x000023C8
	public static event Action twitterPost;

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x060000E5 RID: 229 RVA: 0x000041FC File Offset: 0x000023FC
	// (remove) Token: 0x060000E6 RID: 230 RVA: 0x00004230 File Offset: 0x00002430
	public static event Action<string> twitterPostFailed;

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x060000E7 RID: 231 RVA: 0x00004264 File Offset: 0x00002464
	// (remove) Token: 0x060000E8 RID: 232 RVA: 0x00004298 File Offset: 0x00002498
	public static event Action<ArrayList> twitterHomeTimelineReceived;

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x060000E9 RID: 233 RVA: 0x000042CC File Offset: 0x000024CC
	// (remove) Token: 0x060000EA RID: 234 RVA: 0x00004300 File Offset: 0x00002500
	public static event Action<string> twitterHomeTimelineFailed;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x060000EB RID: 235 RVA: 0x00004334 File Offset: 0x00002534
	// (remove) Token: 0x060000EC RID: 236 RVA: 0x00004368 File Offset: 0x00002568
	public static event Action<object> twitterRequestDidFinishEvent;

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x060000ED RID: 237 RVA: 0x0000439C File Offset: 0x0000259C
	// (remove) Token: 0x060000EE RID: 238 RVA: 0x000043D0 File Offset: 0x000025D0
	public static event Action<string> twitterRequestDidFailEvent;

	// Token: 0x14000009 RID: 9
	// (add) Token: 0x060000EF RID: 239 RVA: 0x00004404 File Offset: 0x00002604
	// (remove) Token: 0x060000F0 RID: 240 RVA: 0x00004438 File Offset: 0x00002638
	public static event Action facebookLogin;

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x060000F1 RID: 241 RVA: 0x0000446C File Offset: 0x0000266C
	// (remove) Token: 0x060000F2 RID: 242 RVA: 0x000044A0 File Offset: 0x000026A0
	public static event Action<string> facebookLoginFailed;

	// Token: 0x1400000B RID: 11
	// (add) Token: 0x060000F3 RID: 243 RVA: 0x000044D4 File Offset: 0x000026D4
	// (remove) Token: 0x060000F4 RID: 244 RVA: 0x00004508 File Offset: 0x00002708
	public static event Action facebookDidLogoutEvent;

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x060000F5 RID: 245 RVA: 0x0000453C File Offset: 0x0000273C
	// (remove) Token: 0x060000F6 RID: 246 RVA: 0x00004570 File Offset: 0x00002770
	public static event Action<DateTime> facebookDidExtendTokenEvent;

	// Token: 0x1400000D RID: 13
	// (add) Token: 0x060000F7 RID: 247 RVA: 0x000045A4 File Offset: 0x000027A4
	// (remove) Token: 0x060000F8 RID: 248 RVA: 0x000045D8 File Offset: 0x000027D8
	public static event Action facebookSessionInvalidatedEvent;

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x060000F9 RID: 249 RVA: 0x0000460C File Offset: 0x0000280C
	// (remove) Token: 0x060000FA RID: 250 RVA: 0x00004640 File Offset: 0x00002840
	public static event Action<string> facebookReceivedUsername;

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x060000FB RID: 251 RVA: 0x00004674 File Offset: 0x00002874
	// (remove) Token: 0x060000FC RID: 252 RVA: 0x000046A8 File Offset: 0x000028A8
	public static event Action<string> facebookUsernameRequestFailed;

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x060000FD RID: 253 RVA: 0x000046DC File Offset: 0x000028DC
	// (remove) Token: 0x060000FE RID: 254 RVA: 0x00004710 File Offset: 0x00002910
	public static event Action facebookPost;

	// Token: 0x14000011 RID: 17
	// (add) Token: 0x060000FF RID: 255 RVA: 0x00004744 File Offset: 0x00002944
	// (remove) Token: 0x06000100 RID: 256 RVA: 0x00004778 File Offset: 0x00002978
	public static event Action<string> facebookPostFailed;

	// Token: 0x14000012 RID: 18
	// (add) Token: 0x06000101 RID: 257 RVA: 0x000047AC File Offset: 0x000029AC
	// (remove) Token: 0x06000102 RID: 258 RVA: 0x000047E0 File Offset: 0x000029E0
	public static event Action<ArrayList> facebookReceivedFriends;

	// Token: 0x14000013 RID: 19
	// (add) Token: 0x06000103 RID: 259 RVA: 0x00004814 File Offset: 0x00002A14
	// (remove) Token: 0x06000104 RID: 260 RVA: 0x00004848 File Offset: 0x00002A48
	public static event Action<string> facebookFriendRequestFailed;

	// Token: 0x14000014 RID: 20
	// (add) Token: 0x06000105 RID: 261 RVA: 0x0000487C File Offset: 0x00002A7C
	// (remove) Token: 0x06000106 RID: 262 RVA: 0x000048B0 File Offset: 0x00002AB0
	public static event Action facebookDialogCompleted;

	// Token: 0x14000015 RID: 21
	// (add) Token: 0x06000107 RID: 263 RVA: 0x000048E4 File Offset: 0x00002AE4
	// (remove) Token: 0x06000108 RID: 264 RVA: 0x00004918 File Offset: 0x00002B18
	public static event Action<string> facebookDialogFailed;

	// Token: 0x14000016 RID: 22
	// (add) Token: 0x06000109 RID: 265 RVA: 0x0000494C File Offset: 0x00002B4C
	// (remove) Token: 0x0600010A RID: 266 RVA: 0x00004980 File Offset: 0x00002B80
	public static event Action facebookDialogDidntComplete;

	// Token: 0x14000017 RID: 23
	// (add) Token: 0x0600010B RID: 267 RVA: 0x000049B4 File Offset: 0x00002BB4
	// (remove) Token: 0x0600010C RID: 268 RVA: 0x000049E8 File Offset: 0x00002BE8
	public static event Action<string> facebookDialogCompletedWithUrl;

	// Token: 0x14000018 RID: 24
	// (add) Token: 0x0600010D RID: 269 RVA: 0x00004A1C File Offset: 0x00002C1C
	// (remove) Token: 0x0600010E RID: 270 RVA: 0x00004A50 File Offset: 0x00002C50
	public static event Action<object> facebookReceivedCustomRequest;

	// Token: 0x14000019 RID: 25
	// (add) Token: 0x0600010F RID: 271 RVA: 0x00004A84 File Offset: 0x00002C84
	// (remove) Token: 0x06000110 RID: 272 RVA: 0x00004AB8 File Offset: 0x00002CB8
	public static event Action<string> facebookCustomRequestFailed;

	// Token: 0x06000111 RID: 273 RVA: 0x00004AEB File Offset: 0x00002CEB
	private void Awake()
	{
		base.gameObject.name = base.GetType().ToString();
		Object.DontDestroyOnLoad(this);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00004B09 File Offset: 0x00002D09
	public void twitterLoginSucceeded(string empty)
	{
		if (SocialNetworkingManager.twitterLogin != null)
		{
			SocialNetworkingManager.twitterLogin();
		}
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00004B1C File Offset: 0x00002D1C
	public void twitterLoginDidFail(string error)
	{
		if (SocialNetworkingManager.twitterLoginFailed != null)
		{
			SocialNetworkingManager.twitterLoginFailed(error);
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00004B30 File Offset: 0x00002D30
	public void twitterPostSucceeded(string empty)
	{
		if (SocialNetworkingManager.twitterPost != null)
		{
			SocialNetworkingManager.twitterPost();
		}
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00004B43 File Offset: 0x00002D43
	public void twitterPostDidFail(string error)
	{
		if (SocialNetworkingManager.twitterPostFailed != null)
		{
			SocialNetworkingManager.twitterPostFailed(error);
		}
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00004B57 File Offset: 0x00002D57
	public void twitterHomeTimelineDidFail(string error)
	{
		if (SocialNetworkingManager.twitterHomeTimelineFailed != null)
		{
			SocialNetworkingManager.twitterHomeTimelineFailed(error);
		}
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00004B6C File Offset: 0x00002D6C
	public void twitterHomeTimelineDidFinish(string results)
	{
		if (SocialNetworkingManager.twitterHomeTimelineReceived != null)
		{
			ArrayList arrayList = (ArrayList)MiniJSON.jsonDecode(results);
			SocialNetworkingManager.twitterHomeTimelineReceived(arrayList);
		}
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00004B97 File Offset: 0x00002D97
	public void twitterRequestDidFinish(string results)
	{
		if (SocialNetworkingManager.twitterRequestDidFinishEvent != null)
		{
			SocialNetworkingManager.twitterRequestDidFinishEvent(MiniJSON.jsonDecode(results));
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00004BB0 File Offset: 0x00002DB0
	public void twitterRequestDidFail(string error)
	{
		if (SocialNetworkingManager.twitterRequestDidFailEvent != null)
		{
			SocialNetworkingManager.twitterRequestDidFailEvent(error);
		}
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00004BC4 File Offset: 0x00002DC4
	public void facebookLoginSucceeded(string empty)
	{
		if (SocialNetworkingManager.facebookLogin != null)
		{
			SocialNetworkingManager.facebookLogin();
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00004BD7 File Offset: 0x00002DD7
	public void facebookLoginDidFail(string error)
	{
		if (SocialNetworkingManager.facebookLoginFailed != null)
		{
			SocialNetworkingManager.facebookLoginFailed(error);
		}
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00004BEB File Offset: 0x00002DEB
	public void facebookDidLogout(string empty)
	{
		if (SocialNetworkingManager.facebookDidLogoutEvent != null)
		{
			SocialNetworkingManager.facebookDidLogoutEvent();
		}
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00004C00 File Offset: 0x00002E00
	public void facebookDidExtendToken(string secondsSinceEpoch)
	{
		if (SocialNetworkingManager.facebookDidExtendTokenEvent != null)
		{
			double num = double.Parse(secondsSinceEpoch);
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(num);
			SocialNetworkingManager.facebookDidExtendTokenEvent(dateTime);
		}
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00004C40 File Offset: 0x00002E40
	public void facebookSessionInvalidated(string empty)
	{
		if (SocialNetworkingManager.facebookSessionInvalidatedEvent != null)
		{
			SocialNetworkingManager.facebookSessionInvalidatedEvent();
		}
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00004C53 File Offset: 0x00002E53
	public void facebookDidReceiveUsername(string username)
	{
		if (SocialNetworkingManager.facebookReceivedUsername != null)
		{
			SocialNetworkingManager.facebookReceivedUsername(username);
		}
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00004C67 File Offset: 0x00002E67
	public void facebookUsernameRequestDidFail(string error)
	{
		if (SocialNetworkingManager.facebookUsernameRequestFailed != null)
		{
			SocialNetworkingManager.facebookUsernameRequestFailed(error);
		}
	}

	// Token: 0x06000121 RID: 289 RVA: 0x00004C7B File Offset: 0x00002E7B
	public void facebookPostSucceeded(string empty)
	{
		if (SocialNetworkingManager.facebookPost != null)
		{
			SocialNetworkingManager.facebookPost();
		}
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00004C8E File Offset: 0x00002E8E
	public void facebookPostDidFail(string error)
	{
		if (SocialNetworkingManager.facebookPostFailed != null)
		{
			SocialNetworkingManager.facebookPostFailed(error);
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00004CA4 File Offset: 0x00002EA4
	public void facebookDidReceiveFriends(string jsonResult)
	{
		if (SocialNetworkingManager.facebookReceivedFriends != null)
		{
			Hashtable hashtable = (Hashtable)MiniJSON.jsonDecode(jsonResult);
			if (hashtable.Contains("data"))
			{
				SocialNetworkingManager.facebookReceivedFriends((ArrayList)hashtable["data"]);
				return;
			}
			SocialNetworkingManager.facebookReceivedFriends(new ArrayList());
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00004CFB File Offset: 0x00002EFB
	public void facebookFriendRequestDidFail(string error)
	{
		if (SocialNetworkingManager.facebookFriendRequestFailed != null)
		{
			SocialNetworkingManager.facebookFriendRequestFailed(error);
		}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00004D0F File Offset: 0x00002F0F
	public void facebookDialogDidComplete(string empty)
	{
		if (SocialNetworkingManager.facebookDialogCompleted != null)
		{
			SocialNetworkingManager.facebookDialogCompleted();
		}
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00004D22 File Offset: 0x00002F22
	public void facebookDialogDidCompleteWithUrl(string url)
	{
		if (SocialNetworkingManager.facebookDialogCompletedWithUrl != null)
		{
			SocialNetworkingManager.facebookDialogCompletedWithUrl(url);
		}
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00004D36 File Offset: 0x00002F36
	public void facebookDialogDidNotComplete(string empty)
	{
		if (SocialNetworkingManager.facebookDialogDidntComplete != null)
		{
			SocialNetworkingManager.facebookDialogDidntComplete();
		}
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00004D49 File Offset: 0x00002F49
	public void facebookDialogDidFailWithError(string error)
	{
		if (SocialNetworkingManager.facebookDialogFailed != null)
		{
			SocialNetworkingManager.facebookDialogFailed(error);
		}
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00004D60 File Offset: 0x00002F60
	public void facebookDidReceiveCustomRequest(string result)
	{
		if (SocialNetworkingManager.facebookReceivedCustomRequest != null)
		{
			object obj = MiniJSON.jsonDecode(result);
			SocialNetworkingManager.facebookReceivedCustomRequest(obj);
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00004D86 File Offset: 0x00002F86
	public void facebookCustomRequestDidFail(string error)
	{
		if (SocialNetworkingManager.facebookCustomRequestFailed != null)
		{
			SocialNetworkingManager.facebookCustomRequestFailed(error);
		}
	}
}
