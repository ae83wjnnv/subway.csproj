using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class FacebookBinding
{
	// Token: 0x0600004F RID: 79
	[DllImport("__Internal")]
	private static extern void _facebookInit(string applicationId);

	// Token: 0x06000050 RID: 80 RVA: 0x00002325 File Offset: 0x00000525
	public static void init(string applicationId)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookInit(applicationId);
		}
	}

	// Token: 0x06000051 RID: 81
	[DllImport("__Internal")]
	private static extern bool _facebookIsLoggedIn();

	// Token: 0x06000052 RID: 82 RVA: 0x00002335 File Offset: 0x00000535
	public static bool isLoggedIn()
	{
		return Application.platform == RuntimePlatform.IPhonePlayer && FacebookBinding._facebookIsLoggedIn();
	}

	// Token: 0x06000053 RID: 83
	[DllImport("__Internal")]
	private static extern string _facebookGetFacebookAccessToken();

	// Token: 0x06000054 RID: 84 RVA: 0x00002346 File Offset: 0x00000546
	public static string getFacebookAccessToken()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return FacebookBinding._facebookGetFacebookAccessToken();
		}
		return string.Empty;
	}

	// Token: 0x06000055 RID: 85
	[DllImport("__Internal")]
	private static extern void _facebookExtendAccessToken();

	// Token: 0x06000056 RID: 86 RVA: 0x0000235B File Offset: 0x0000055B
	public static void extendAccessToken()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookExtendAccessToken();
		}
	}

	// Token: 0x06000057 RID: 87
	[DllImport("__Internal")]
	private static extern void _facebookLogin();

	// Token: 0x06000058 RID: 88 RVA: 0x0000236A File Offset: 0x0000056A
	public static void login()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookLogin();
		}
	}

	// Token: 0x06000059 RID: 89
	[DllImport("__Internal")]
	private static extern void _facebookLoginWithRequestedPermissions(string perms);

	// Token: 0x0600005A RID: 90 RVA: 0x00002379 File Offset: 0x00000579
	public static void loginWithRequestedPermissions(string[] permissions)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookLoginWithRequestedPermissions(string.Join(",", permissions));
		}
	}

	// Token: 0x0600005B RID: 91
	[DllImport("__Internal")]
	private static extern void _facebookLogout();

	// Token: 0x0600005C RID: 92 RVA: 0x00002393 File Offset: 0x00000593
	public static void logout()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookLogout();
		}
	}

	// Token: 0x0600005D RID: 93
	[DllImport("__Internal")]
	private static extern void _facebookGetLoggedinUsersName();

	// Token: 0x0600005E RID: 94 RVA: 0x000023A2 File Offset: 0x000005A2
	public static void getLoggedinUsersName()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookGetLoggedinUsersName();
		}
	}

	// Token: 0x0600005F RID: 95
	[DllImport("__Internal")]
	private static extern void _facebookPostMessage(string message);

	// Token: 0x06000060 RID: 96 RVA: 0x000023B1 File Offset: 0x000005B1
	public static void postMessage(string message)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookPostMessage(message);
		}
	}

	// Token: 0x06000061 RID: 97
	[DllImport("__Internal")]
	private static extern void _facebookPostMessageWithLink(string message, string link, string linkName);

	// Token: 0x06000062 RID: 98 RVA: 0x000023C1 File Offset: 0x000005C1
	public static void postMessageWithLink(string message, string link, string linkName)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookPostMessageWithLink(message, link, linkName);
		}
	}

	// Token: 0x06000063 RID: 99
	[DllImport("__Internal")]
	private static extern void _facebookPostMessageWithLinkAndLinkToImage(string message, string link, string linkName, string linkToImage, string caption);

	// Token: 0x06000064 RID: 100 RVA: 0x000023D3 File Offset: 0x000005D3
	public static void postMessageWithLinkAndLinkToImage(string message, string link, string linkName, string linkToImage, string caption)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookPostMessageWithLinkAndLinkToImage(message, link, linkName, linkToImage, caption);
		}
	}

	// Token: 0x06000065 RID: 101
	[DllImport("__Internal")]
	private static extern void _facebookPostImage(string pathToImage, string caption);

	// Token: 0x06000066 RID: 102 RVA: 0x000023E8 File Offset: 0x000005E8
	public static void postImage(string pathToImage, string caption)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookPostImage(pathToImage, caption);
		}
	}

	// Token: 0x06000067 RID: 103
	[DllImport("__Internal")]
	private static extern void _facebookPostImageInAlbum(string pathToImage, string caption, string albumId);

	// Token: 0x06000068 RID: 104 RVA: 0x000023F9 File Offset: 0x000005F9
	public static void postImage(string pathToImage, string caption, string albumId)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookPostImageInAlbum(pathToImage, caption, albumId);
		}
	}

	// Token: 0x06000069 RID: 105
	[DllImport("__Internal")]
	private static extern void _facebookGetFriends();

	// Token: 0x0600006A RID: 106 RVA: 0x0000240B File Offset: 0x0000060B
	public static void getFriends()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookGetFriends();
		}
	}

	// Token: 0x0600006B RID: 107
	[DllImport("__Internal")]
	private static extern void _facebookShowPostMessageDialog();

	// Token: 0x0600006C RID: 108 RVA: 0x0000241A File Offset: 0x0000061A
	public static void showPostMessageDialog()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookShowPostMessageDialog();
		}
	}

	// Token: 0x0600006D RID: 109
	[DllImport("__Internal")]
	private static extern void _facebookShowPostMessageDialogWithOptions(string link, string linkName, string linkToImage, string caption);

	// Token: 0x0600006E RID: 110 RVA: 0x00002429 File Offset: 0x00000629
	public static void showPostMessageDialogWithOptions(string link, string linkName, string linkToImage, string caption)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookShowPostMessageDialogWithOptions(link, linkName, linkToImage, caption);
		}
	}

	// Token: 0x0600006F RID: 111
	[DllImport("__Internal")]
	private static extern void _facebookShowPostMessageDialogWithCustomOptions(string json);

	// Token: 0x06000070 RID: 112 RVA: 0x0000243C File Offset: 0x0000063C
	public static void showPostMessageDialogWithOptions(Hashtable options)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookShowPostMessageDialogWithCustomOptions(MiniJSON.jsonEncode(options));
		}
	}

	// Token: 0x06000071 RID: 113
	[DllImport("__Internal")]
	private static extern void _facebookShowDialog(string dialogType, string json);

	// Token: 0x06000072 RID: 114 RVA: 0x00002451 File Offset: 0x00000651
	public static void showDialog(string dialogType, Dictionary<string, string> options)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FacebookBinding._facebookShowDialog(dialogType, MiniJSON.jsonEncode(options));
		}
	}

	// Token: 0x06000073 RID: 115
	[DllImport("__Internal")]
	private static extern void _facebookGraphRequest(string graphPath, string httpMethod, string jsonDict);

	// Token: 0x06000074 RID: 116 RVA: 0x00002468 File Offset: 0x00000668
	public static void graphRequest(string graphPath, string httpMethod, Hashtable keyValueHash)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string text = MiniJSON.jsonEncode(keyValueHash);
			if (text != null)
			{
				FacebookBinding._facebookGraphRequest(graphPath, httpMethod, text);
			}
		}
	}

	// Token: 0x06000075 RID: 117
	[DllImport("__Internal")]
	private static extern void _facebookRestRequest(string restMethod, string httpMethod, string jsonDict);

	// Token: 0x06000076 RID: 118 RVA: 0x00002490 File Offset: 0x00000690
	public static void restRequest(string restMethod, string httpMethod, Hashtable keyValueHash)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string text = MiniJSON.jsonEncode(keyValueHash);
			if (text != null)
			{
				FacebookBinding._facebookRestRequest(restMethod, httpMethod, text);
			}
		}
	}
}
