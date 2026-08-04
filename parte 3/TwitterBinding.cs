using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class TwitterBinding
{
	// Token: 0x0600012C RID: 300
	[DllImport("__Internal")]
	private static extern void _twitterInit(string consumerKey, string consumerSecret);

	// Token: 0x0600012D RID: 301 RVA: 0x00004DA2 File Offset: 0x00002FA2
	public static void init(string consumerKey, string consumerSecret)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			TwitterBinding._twitterInit(consumerKey, consumerSecret);
		}
	}

	// Token: 0x0600012E RID: 302
	[DllImport("__Internal")]
	private static extern bool _twitterIsLoggedIn();

	// Token: 0x0600012F RID: 303 RVA: 0x00004DB3 File Offset: 0x00002FB3
	public static bool isLoggedIn()
	{
		return Application.platform == RuntimePlatform.IPhonePlayer && TwitterBinding._twitterIsLoggedIn();
	}

	// Token: 0x06000130 RID: 304
	[DllImport("__Internal")]
	private static extern string _twitterLoggedInUsername();

	// Token: 0x06000131 RID: 305 RVA: 0x00004DC4 File Offset: 0x00002FC4
	public static string loggedInUsername()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return TwitterBinding._twitterLoggedInUsername();
		}
		return string.Empty;
	}

	// Token: 0x06000132 RID: 306
	[DllImport("__Internal")]
	private static extern void _twitterLogin(string username, string password);

	// Token: 0x06000133 RID: 307 RVA: 0x00004DD9 File Offset: 0x00002FD9
	public static void login(string username, string password)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			TwitterBinding._twitterLogin(username, password);
		}
	}

	// Token: 0x06000134 RID: 308
	[DllImport("__Internal")]
	private static extern void _twitterShowOauthLoginDialog();

	// Token: 0x06000135 RID: 309 RVA: 0x00004DEA File Offset: 0x00002FEA
	public static void showOauthLoginDialog()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			TwitterBinding._twitterShowOauthLoginDialog();
		}
	}

	// Token: 0x06000136 RID: 310
	[DllImport("__Internal")]
	private static extern void _twitterLogout();

	// Token: 0x06000137 RID: 311 RVA: 0x00004DF9 File Offset: 0x00002FF9
	public static void logout()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			TwitterBinding._twitterLogout();
		}
	}

	// Token: 0x06000138 RID: 312
	[DllImport("__Internal")]
	private static extern void _twitterPostStatusUpdate(string status);

	// Token: 0x06000139 RID: 313 RVA: 0x00004E08 File Offset: 0x00003008
	public static void postStatusUpdate(string status)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			TwitterBinding._twitterPostStatusUpdate(status);
		}
	}

	// Token: 0x0600013A RID: 314
	[DllImport("__Internal")]
	private static extern void _twitterPostStatusUpdateWithImage(string status, string imagePath);

	// Token: 0x0600013B RID: 315 RVA: 0x00004E18 File Offset: 0x00003018
	public static void postStatusUpdate(string status, string pathToImage)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			TwitterBinding._twitterPostStatusUpdateWithImage(status, pathToImage);
		}
	}

	// Token: 0x0600013C RID: 316
	[DllImport("__Internal")]
	private static extern void _twitterGetHomeTimeline();

	// Token: 0x0600013D RID: 317 RVA: 0x00004E29 File Offset: 0x00003029
	public static void getHomeTimeline()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			TwitterBinding._twitterGetHomeTimeline();
		}
	}

	// Token: 0x0600013E RID: 318
	[DllImport("__Internal")]
	private static extern void _twitterPerformRequest(string methodType, string path, string parameters);

	// Token: 0x0600013F RID: 319 RVA: 0x00004E38 File Offset: 0x00003038
	public static void performRequest(string methodType, string path, Dictionary<string, string> parameters)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			TwitterBinding._twitterPerformRequest(methodType, path, (parameters == null) ? null : parameters.toJson());
		}
	}

	// Token: 0x06000140 RID: 320
	[DllImport("__Internal")]
	private static extern bool _twitterIsTweetSheetSupported();

	// Token: 0x06000141 RID: 321 RVA: 0x00004E55 File Offset: 0x00003055
	public static bool isTweetSheetSupported()
	{
		return Application.platform == RuntimePlatform.IPhonePlayer && TwitterBinding._twitterIsTweetSheetSupported();
	}

	// Token: 0x06000142 RID: 322
	[DllImport("__Internal")]
	private static extern bool _twitterCanUserTweet();

	// Token: 0x06000143 RID: 323 RVA: 0x00004E66 File Offset: 0x00003066
	public static bool canUserTweet()
	{
		return Application.platform == RuntimePlatform.IPhonePlayer && TwitterBinding._twitterCanUserTweet();
	}

	// Token: 0x06000144 RID: 324
	[DllImport("__Internal")]
	private static extern void _twitterShowTweetComposer(string status, string imagePath);

	// Token: 0x06000145 RID: 325 RVA: 0x00004E77 File Offset: 0x00003077
	public static void showTweetComposer(string status, string pathToImage)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			TwitterBinding._twitterShowTweetComposer(status, pathToImage);
		}
	}
}
