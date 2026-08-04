using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms;

// Token: 0x020000C6 RID: 198
public class SocialManager : MonoBehaviour
{
	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0001C072 File Offset: 0x0001A272
	public FacebookProfile facebookProfile
	{
		get
		{
			return this._fbProfile;
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001C07A File Offset: 0x0001A27A
	public Texture2D localUserImage
	{
		get
		{
			if (this.facebookProfile != null)
			{
				return this.facebookProfile.image;
			}
			if (Social.localUser != null)
			{
				return Social.localUser.image;
			}
			Debug.LogError("Local user not initialized");
			return null;
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001C0AD File Offset: 0x0001A2AD
	public string localUserName
	{
		get
		{
			if (this.facebookProfile != null)
			{
				return this.facebookProfile.name;
			}
			if (Social.localUser != null)
			{
				return Social.localUser.userName;
			}
			Debug.LogError("Local user not initialized");
			return null;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001C0E0 File Offset: 0x0001A2E0
	public static SocialManager instance
	{
		get
		{
			if (SocialManager._instance == null)
			{
				GameObject gameObject = new GameObject();
				Object.DontDestroyOnLoad(gameObject);
				gameObject.AddComponent<SocialNetworkingManager>();
				SocialManager._instance = gameObject.AddComponent<SocialManager>();
			}
			return SocialManager._instance;
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001C110 File Offset: 0x0001A310
	public bool facebookIsLoggedIn
	{
		get
		{
			return FacebookBinding.isLoggedIn();
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001C117 File Offset: 0x0001A317
	public bool twitterIsLoggedIn
	{
		get
		{
			return TwitterBinding.isLoggedIn();
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001C11E File Offset: 0x0001A31E
	public bool doneLoggingIn
	{
		get
		{
			return this._doneLoggingIn;
		}
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001C126 File Offset: 0x0001A326
	public bool consolidatedFriendsCompleted
	{
		get
		{
			return this._consolidatedFriendsCompleted;
		}
	}

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0001C12E File Offset: 0x0001A32E
	public bool dirty
	{
		get
		{
			return this._dirty;
		}
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x0001C138 File Offset: 0x0001A338
	public Friend[] FriendsSortedByScore()
	{
		if (this._friends != null)
		{
			Friend[] array = this._friends.ToArray();
			Array.Sort<Friend>(array, (Friend x, Friend y) => y.score - x.score);
			return array;
		}
		return new Friend[0];
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x0001C184 File Offset: 0x0001A384
	public Friend[] FriendsSortedByCash()
	{
		if (this._friends != null)
		{
			Debug.Log("Friends was NOT null");
			Friend[] array = this._friends.ToArray();
			Array.Sort<Friend>(array, (Friend x, Friend y) => y.gamesToCashIn - x.gamesToCashIn);
			return array;
		}
		Debug.Log("Friends was null");
		return new Friend[0];
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x0001C1E4 File Offset: 0x0001A3E4
	private void InitPushNotifications()
	{
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x0001C1E6 File Offset: 0x0001A3E6
	private void InitGameCenter()
	{
		this._gameCenterAuthenticationComplete = false;
		Social.localUser.Authenticate(delegate(bool authenticated)
		{
			if (authenticated)
			{
				Social.localUser.LoadFriends(delegate(bool friendsLoaded)
				{
					if (friendsLoaded)
					{
						IUserProfile[] friends = Social.localUser.friends;
						this._gcFriends = new Dictionary<string, IUserProfile>(friends.Length);
						foreach (IUserProfile userProfile in friends)
						{
							this._gcFriends[userProfile.id] = userProfile;
						}
					}
					else
					{
						this._gcFriends = null;
					}
					this._gameCenterFriendListRequestComplete = true;
					this.Invalidate();
				});
				Flurry.LogGameCenterLogin();
			}
			else if (this._friends != null)
			{
				this._friends.RemoveAll((Friend item) => item.gcProfile != null && item.fbProfile == null);
				this._friends.ForEach(delegate(Friend item)
				{
					item.gcProfile = null;
				});
			}
			this._gameCenterAuthenticationComplete = true;
		});
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x0001C205 File Offset: 0x0001A405
	public void FacebookLogin(Action<bool> onComplete)
	{
		base.StartCoroutine(this.FacebookLoginCoroutine(onComplete));
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x0001C215 File Offset: 0x0001A415
	public void FacebookLogout()
	{
		FacebookBinding.logout();
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x0001C21C File Offset: 0x0001A41C
	private IEnumerator FacebookLoginCoroutine(Action<bool> onComplete)
	{
		if (!FacebookBinding.isLoggedIn())
		{
			this._fbCurrentRequest = SocialManager.FacebookCurrentRequest.LoggingIn;
			FacebookBinding.login();
			while (this._fbCurrentRequest != SocialManager.FacebookCurrentRequest.None)
			{
				if (this._fbCurrentRequest == SocialManager.FacebookCurrentRequest.Error)
				{
					if (onComplete != null)
					{
						onComplete(false);
					}
					yield break;
				}
				yield return null;
			}
		}
		this._fbCurrentRequest = SocialManager.FacebookCurrentRequest.GettingUserInfo;
		Hashtable hashtable = new Hashtable();
		hashtable["fields"] = "id,name,first_name";
		FacebookBinding.graphRequest("me", "GET", hashtable);
		while (this._fbCurrentRequest != SocialManager.FacebookCurrentRequest.None)
		{
			if (this._fbCurrentRequest == SocialManager.FacebookCurrentRequest.Error)
			{
				if (onComplete != null)
				{
					onComplete(false);
				}
				yield break;
			}
			yield return null;
		}
		base.StartCoroutine(this.DownloadFacebookPicture(this._fbProfile));
		this._fbCurrentRequest = SocialManager.FacebookCurrentRequest.GettingFriends;
		Hashtable hashtable2 = new Hashtable();
		hashtable2["fields"] = "id,name,first_name";
		FacebookBinding.graphRequest("me/friends", "GET", hashtable2);
		while (this._fbCurrentRequest != SocialManager.FacebookCurrentRequest.None)
		{
			if (this._fbCurrentRequest == SocialManager.FacebookCurrentRequest.Error)
			{
				if (onComplete != null)
				{
					onComplete(false);
				}
				yield break;
			}
			yield return null;
		}
		if (SocialManager.debugGUI)
		{
			this._debugFacebookFriends = new Dictionary<string, FacebookProfile>(this._fbFriends.Count);
			foreach (Hashtable hashtable3 in this._fbFriends.Values)
			{
				FacebookProfile facebookProfile = new FacebookProfile
				{
					id = (string)hashtable3["id"],
					name = (string)hashtable3["first_name"],
					fullName = (string)hashtable3["name"]
				};
				this._debugFacebookFriends[facebookProfile.id] = facebookProfile;
			}
			base.StartCoroutine(this.DownloadFacebookPictures(this._debugFacebookFriends));
		}
		this._fbReady = true;
		this.Invalidate();
		if (onComplete != null)
		{
			onComplete(true);
		}
		yield break;
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x0001C232 File Offset: 0x0001A432
	private void TwitterLogin(Action<bool> onComplete)
	{
		base.StartCoroutine(this.TwitterLoginCoroutine(onComplete));
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x0001C242 File Offset: 0x0001A442
	public void TwitterLogout()
	{
		TwitterBinding.logout();
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x0001C249 File Offset: 0x0001A449
	private IEnumerator TwitterLoginCoroutine(Action<bool> onComplete)
	{
		if (!TwitterBinding.isLoggedIn())
		{
			this._twitterCurrentRequest = SocialManager.TwitterCurrentRequest.LoggingIn;
			TwitterBinding.showOauthLoginDialog();
			while (this._twitterCurrentRequest != SocialManager.TwitterCurrentRequest.None)
			{
				if (this._twitterCurrentRequest == SocialManager.TwitterCurrentRequest.Error)
				{
					if (onComplete != null)
					{
						onComplete(false);
					}
					yield break;
				}
				yield return null;
			}
		}
		this._twitterReady = true;
		if (onComplete != null)
		{
			onComplete(true);
		}
		yield break;
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x0001C260 File Offset: 0x0001A460
	private void Invalidate()
	{
		if (this._gameCenterAuthenticationComplete && (!Social.localUser.authenticated || this._gameCenterFriendListRequestComplete) && (!this.facebookIsLoggedIn || this._fbReady))
		{
			this._doneLoggingIn = true;
			this.RegisterUser(delegate(bool success)
			{
				Debug.Log((!success) ? "Register user failed" : "Register user succeeded");
			});
			this.ConsolidateFriends(delegate(bool success)
			{
				Debug.Log((!success) ? "Consolidate friends failed" : "Consolidate friends succeeded");
				this._consolidatedFriendsCompleted = true;
			});
		}
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x0001C2D7 File Offset: 0x0001A4D7
	public void CollectFriendReward(Friend friend)
	{
		friend.status.gamesCashedIn = friend.games;
		this._dirty = true;
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x0001C2F4 File Offset: 0x0001A4F4
	public int CashIn(Friend friend, int max)
	{
		int num = friend.games - friend.status.gamesCashedIn;
		if (num > 0)
		{
			friend.status.gamesCashedIn = friend.games;
			this._dirty = true;
			return Mathf.Max(num, max);
		}
		return 0;
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0001C33C File Offset: 0x0001A53C
	public int CashInAll(int maxPerFriend)
	{
		int num = 0;
		foreach (Friend friend in this._friends)
		{
			num += this.CashIn(friend, maxPerFriend);
		}
		return num;
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x0001C398 File Offset: 0x0001A598
	public void WriteTo(Stream stream)
	{
		BinaryWriter binaryWriter = new BinaryWriter(stream);
		binaryWriter.Write(1);
		if (this._friendStatus != null)
		{
			binaryWriter.Write(this._friendStatus.Count);
			foreach (KeyValuePair<string, Friend.Status> keyValuePair in this._friendStatus)
			{
				binaryWriter.Write(keyValuePair.Key);
				binaryWriter.Write(keyValuePair.Value.gamesCashedIn);
				binaryWriter.Write(keyValuePair.Value.lastPokeTime.ToBinary());
			}
			return;
		}
		binaryWriter.Write(0);
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x0001C44C File Offset: 0x0001A64C
	public void ReadFrom(Stream stream)
	{
		BinaryReader binaryReader = new BinaryReader(stream);
		if (binaryReader.ReadByte() == 1)
		{
			int num = binaryReader.ReadInt32();
			this._friendStatus = new Dictionary<string, Friend.Status>(num);
			for (int i = 0; i < num; i++)
			{
				string text = binaryReader.ReadString();
				if (!string.IsNullOrEmpty(text))
				{
					Friend.Status status = new Friend.Status();
					status.gamesCashedIn = binaryReader.ReadInt32();
					status.lastPokeTime = DateTime.FromBinary(binaryReader.ReadInt64());
					this._friendStatus[text] = status;
				}
			}
			return;
		}
		throw new IOException("Unsupported playerdata file version");
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0001C4D6 File Offset: 0x0001A6D6
	private static string GetSaveDataPath()
	{
		return Application.persistentDataPath + "/socialdata";
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x0001C4E8 File Offset: 0x0001A6E8
	private static bool ArraysAreEqual<T>(T[] a, T[] b)
	{
		if (a == null && b == null)
		{
			return true;
		}
		if (a.Length != b.Length)
		{
			return false;
		}
		for (int i = 0; i < a.Length; i++)
		{
			if (!object.Equals(a[i], b[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x0001C538 File Offset: 0x0001A738
	public void Load()
	{
		try
		{
			SocialManager.GetSaveDataPath();
			MemoryStream memoryStream = new MemoryStream(FileUtil.Load(SocialManager.GetSaveDataPath(), "resxrctrv7tgv7gb8h9h9u0909kllfmolkjnhghgjjkhjghg"));
			this.ReadFrom(memoryStream);
			memoryStream.Close();
			this._dirty = false;
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Could not load data: " + ex.Message);
		}
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x0001C5A0 File Offset: 0x0001A7A0
	public bool Save()
	{
		try
		{
			MemoryStream memoryStream = new MemoryStream(8192);
			this.WriteTo(memoryStream);
			byte[] buffer = memoryStream.GetBuffer();
			FileUtil.Save(SocialManager.GetSaveDataPath(), "resxrctrv7tgv7gb8h9h9u0909kllfmolkjnhghgjjkhjghg", buffer, 0, (int)memoryStream.Length);
			memoryStream.Close();
			this._dirty = false;
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogWarning(string.Concat(new string[]
			{
				"Error saving social data: ",
				ex.GetType().Name,
				": ",
				ex.Message,
				"\n",
				ex.StackTrace
			}));
		}
		return false;
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x0001C650 File Offset: 0x0001A850
	private void Awake()
	{
		this.Load();
		FacebookBinding.init("254616967963463");
		this.InitPushNotifications();
		if (this.facebookIsLoggedIn)
		{
			this.FacebookLogin(null);
		}
		this.InitGameCenter();
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x0001C67D File Offset: 0x0001A87D
	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			this.Save();
			return;
		}
		if (this.facebookIsLoggedIn)
		{
			this.FacebookLogin(null);
		}
		this.InitGameCenter();
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0001C6A0 File Offset: 0x0001A8A0
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

	// Token: 0x060005C1 RID: 1473 RVA: 0x0001C858 File Offset: 0x0001AA58
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

	// Token: 0x060005C2 RID: 1474 RVA: 0x0001CA0E File Offset: 0x0001AC0E
	private void twitterLogin()
	{
		Debug.Log("Successfully logged in to Twitter");
		if (this._twitterCurrentRequest == SocialManager.TwitterCurrentRequest.LoggingIn)
		{
			this._twitterCurrentRequest = SocialManager.TwitterCurrentRequest.None;
			return;
		}
		Debug.LogWarning("Received twitter login message, but we are not in that state");
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x0001CA35 File Offset: 0x0001AC35
	private void twitterLoginFailed(string error)
	{
		Debug.Log("Twitter login failed: " + error);
		if (this._twitterCurrentRequest == SocialManager.TwitterCurrentRequest.LoggingIn)
		{
			this._twitterCurrentRequest = SocialManager.TwitterCurrentRequest.Error;
			return;
		}
		Debug.LogWarning("Received twitter login failed message, but we are not in that state");
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x0001CA62 File Offset: 0x0001AC62
	private void twitterPost()
	{
		Debug.Log("Successfully posted to Twitter");
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0001CA6E File Offset: 0x0001AC6E
	private void twitterPostFailed(string error)
	{
		Debug.Log("Twitter post failed: " + error);
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x0001CA80 File Offset: 0x0001AC80
	private void twitterHomeTimelineFailed(string error)
	{
		Debug.Log("Twitter HomeTimeline failed: " + error);
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x0001CA94 File Offset: 0x0001AC94
	private void twitterHomeTimelineReceived(ArrayList result)
	{
		Debug.Log("received home timeline with tweet count: " + result.Count.ToString());
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x0001CABE File Offset: 0x0001ACBE
	private void twitterRequestDidFailEvent(string error)
	{
		Debug.Log("twitterRequestDidFailEvent: " + error);
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x0001CAD0 File Offset: 0x0001ACD0
	private void twitterRequestDidFinishEvent(object result)
	{
		if (result != null)
		{
			Debug.Log("twitterRequestDidFinishEvent: " + result.GetType().ToString());
			return;
		}
		Debug.Log("twitterRequestDidFinishEvent with no data");
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x0001CAFA File Offset: 0x0001ACFA
	private void facebookLogin()
	{
		Debug.Log("Successfully logged in to Facebook");
		if (this._fbCurrentRequest == SocialManager.FacebookCurrentRequest.LoggingIn)
		{
			this._fbCurrentRequest = SocialManager.FacebookCurrentRequest.None;
			Flurry.LogFacebookLogin();
			return;
		}
		Debug.LogWarning("Received facebook login message, but we are not in that state");
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x0001CB26 File Offset: 0x0001AD26
	private void facebookLoginFailed(string error)
	{
		Debug.Log("Facebook login failed: " + error);
		if (this._fbCurrentRequest == SocialManager.FacebookCurrentRequest.LoggingIn)
		{
			this._fbCurrentRequest = SocialManager.FacebookCurrentRequest.Error;
			return;
		}
		Debug.LogWarning("Received facebook login failed message, but we are not in that state");
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x0001CB54 File Offset: 0x0001AD54
	private void facebookDidLogoutEvent()
	{
		Debug.Log("facebookDidLogoutEvent");
		if (this._friends != null)
		{
			this._friends.RemoveAll((Friend item) => item.gcProfile == null && item.fbProfile != null);
			this._friends.ForEach(delegate(Friend item)
			{
				item.fbProfile = null;
			});
		}
		this._fbFriends = null;
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x0001CBCF File Offset: 0x0001ADCF
	private void facebookDidExtendTokenEvent(DateTime newExpiry)
	{
		Debug.Log("facebookDidExtendTokenEvent: " + newExpiry.ToString());
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0001CBE7 File Offset: 0x0001ADE7
	private void facebookSessionInvalidatedEvent()
	{
		Debug.Log("facebookSessionInvalidatedEvent");
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x0001CBF3 File Offset: 0x0001ADF3
	private void facebookReceivedUsername(string username)
	{
		Debug.Log("Facebook logged in users name: " + username);
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x0001CC05 File Offset: 0x0001AE05
	private void facebookUsernameRequestFailed(string error)
	{
		Debug.Log("Facebook failed to receive username: " + error);
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x0001CC17 File Offset: 0x0001AE17
	private void facebookPost()
	{
		Debug.Log("Successfully posted to Facebook");
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0001CC23 File Offset: 0x0001AE23
	private void facebookPostFailed(string error)
	{
		Debug.Log("Facebook post failed: " + error);
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x0001CC38 File Offset: 0x0001AE38
	private void facebookReceivedFriends(ArrayList result)
	{
		Debug.Log("received total friends: " + result.Count.ToString());
		if (this._fbCurrentRequest == SocialManager.FacebookCurrentRequest.GettingFriends)
		{
			this._fbCurrentRequest = SocialManager.FacebookCurrentRequest.None;
		}
		this._fbFriends = new Dictionary<string, Hashtable>(result.Count);
		foreach (object obj in result)
		{
			Hashtable hashtable = (Hashtable)obj;
			if (hashtable.ContainsKey("id"))
			{
				this._fbFriends[(string)hashtable["id"]] = hashtable;
			}
			else
			{
				Debug.LogError("Unexpected format of FaceBook Friend");
			}
		}
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0001CCF8 File Offset: 0x0001AEF8
	private void facebookFriendRequestFailed(string error)
	{
		Debug.Log("FacebookFriendRequestFailed: " + error);
		this._fbFriends = null;
		if (this._fbCurrentRequest == SocialManager.FacebookCurrentRequest.GettingFriends)
		{
			this._fbCurrentRequest = SocialManager.FacebookCurrentRequest.Error;
		}
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x0001CD21 File Offset: 0x0001AF21
	private void facebokDialogCompleted()
	{
		Debug.Log("facebokDialogCompleted");
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x0001CD2D File Offset: 0x0001AF2D
	private void facebookDialogCompletedWithUrl(string url)
	{
		Debug.Log("facebookDialogCompletedWithUrl: " + url);
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x0001CD3F File Offset: 0x0001AF3F
	private void facebookDialogDidntComplete()
	{
		Debug.Log("facebookDialogDidntComplete");
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x0001CD4B File Offset: 0x0001AF4B
	private void facebookDialogFailed(string error)
	{
		Debug.Log("facebookDialogFailed: " + error);
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x0001CD60 File Offset: 0x0001AF60
	private void facebookReceivedCustomRequest(object obj)
	{
		Debug.Log("facebookReceivedCustomRequest");
		if (this._fbCurrentRequest == SocialManager.FacebookCurrentRequest.GettingUserInfo)
		{
			this._fbProfile = new FacebookProfile();
			Hashtable hashtable = (Hashtable)obj;
			this._fbProfile.id = (string)hashtable["id"];
			this._fbProfile.name = (string)hashtable["first_name"];
			this._fbProfile.fullName = (string)hashtable["name"];
			this._fbCurrentRequest = SocialManager.FacebookCurrentRequest.None;
		}
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0001CDEA File Offset: 0x0001AFEA
	private void facebookCustomRequestFailed(string error)
	{
		Debug.Log("facebookCustomRequestFailed failed: " + error);
		if (this._fbCurrentRequest == SocialManager.FacebookCurrentRequest.GettingUserInfo)
		{
			this._fbCurrentRequest = SocialManager.FacebookCurrentRequest.Error;
		}
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0001CE0C File Offset: 0x0001B00C
	private static string GetRandomIdentifier()
	{
		return ((!Application.isEditor) ? SystemInfo.deviceUniqueIdentifier : "0000000000000000000000000000000000000000") + Random.Range(0, int.MaxValue).ToString();
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x0001CE44 File Offset: 0x0001B044
	private static string GetChecksum(string data)
	{
		return SocialManager.GetSHA1Hash(data + "resxrctrv7tgv7gb8h9h9u0909kllfmolkjnhghgjjkhjghg");
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0001CE56 File Offset: 0x0001B056
	private static string GetChecksum(params string[] data)
	{
		return SocialManager.GetChecksum(string.Join(null, data));
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x0001CE64 File Offset: 0x0001B064
	private static string GetSHA1Hash(string unhashed)
	{
		byte[] array = SHA1.Create().ComputeHash(Encoding.Default.GetBytes(unhashed));
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			stringBuilder.Append(array[i].ToString("x2"));
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x0001CEB9 File Offset: 0x0001B0B9
	private static IEnumerator WWWRequestCoroutine(SocialManager.WWWComplete onWWWComplete, string relativeUrl, object cookie, params string[] postItems)
	{
		string text = "http://hoodrunner.kiloo.com" + relativeUrl;
		string randomIdentifier = SocialManager.GetRandomIdentifier();
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 1; i < postItems.Length; i += 2)
		{
			stringBuilder.Append(postItems[i]);
		}
		string checksum = SocialManager.GetChecksum(randomIdentifier + stringBuilder.ToString());
		WWWForm wwwform = new WWWForm();
		wwwform.AddField("identifier", randomIdentifier);
		wwwform.AddField("checksum", checksum);
		StringBuilder sb = new StringBuilder();
		sb.Append("WWWRequest(").Append(text).Append(")\n");
		for (int j = 0; j < postItems.Length; j += 2)
		{
			sb.Append("Adding post data: \"").Append(postItems[j]).Append("\" = \"")
				.Append(postItems[j + 1])
				.Append("\"\n");
			wwwform.AddField(postItems[j], postItems[j + 1]);
		}
		WWW www = new WWW(text, wwwform);
		yield return www;
		if (www.text != null)
		{
			sb.Append("Text: \"").Append(www.text).Append("\"\n");
		}
		if (www.error != null)
		{
			sb.Append("Error: \"").Append(www.error).Append("\"\n");
		}
		Debug.Log(sb.ToString());
		if (onWWWComplete == null)
		{
			yield break;
		}
		if (www.error != null)
		{
			onWWWComplete(SocialManager.WWWRequestResult.Error, null, cookie);
			yield break;
		}
		string text2 = null;
		int num = www.text.IndexOf("<result>", StringComparison.OrdinalIgnoreCase);
		if (num >= 0)
		{
			num += 8;
			int num2 = www.text.IndexOf("</result>", num, StringComparison.OrdinalIgnoreCase);
			if (num2 > num)
			{
				text2 = www.text.Substring(num, num2 - num);
			}
			else if (num2 == num)
			{
				text2 = string.Empty;
			}
		}
		onWWWComplete((text2 == null) ? SocialManager.WWWRequestResult.Error : SocialManager.WWWRequestResult.Success, text2, cookie);
		yield break;
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x0001CEE0 File Offset: 0x0001B0E0
	private static string ByteArrayToHex(byte[] barray)
	{
		char[] array = new char[barray.Length * 2];
		for (int i = 0; i < barray.Length; i++)
		{
			byte b = (byte)(barray[i] >> 4);
			array[i * 2] = (char)((b <= 9) ? (b + 48) : (b + 55));
			b = barray[i] & 15;
			array[i * 2 + 1] = (char)((b <= 9) ? (b + 48) : (b + 55));
		}
		return new string(array);
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x0001CF47 File Offset: 0x0001B147
	private static string GetBundleVersion()
	{
		return DeviceUtility.GetBundleVersion();
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0001CF4E File Offset: 0x0001B14E
	private void RegisterUser(Action<bool> registerUserCompleted)
	{
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x0001CF50 File Offset: 0x0001B150
	private void WWWRegisterUserCompleted(SocialManager.WWWRequestResult result, string output, object cookie)
	{
		bool flag = false;
		if (result == SocialManager.WWWRequestResult.Success)
		{
			Dictionary<string, string> dictionary = StringUtility.ParseProperties(output);
			if (dictionary.ContainsKey("userid"))
			{
				string text = dictionary["userid"];
				string text2 = dictionary["score"];
				string text3 = dictionary["meters"];
				string text4 = dictionary["games"];
				string text5 = dictionary["checksum"];
				string checksum = SocialManager.GetChecksum(new string[] { text, text2, text3, text4 });
				if (string.Compare(text5, checksum, true) == 0)
				{
					try
					{
						int num = int.Parse(text);
						int num2 = int.Parse(text2);
						int num3 = int.Parse(text3);
						this._userid = num;
						PlayerInfo.Instance.highestScore = num2;
						PlayerInfo.Instance.highestMeters = num3;
						flag = true;
						goto IL_00DD;
					}
					catch (Exception)
					{
						Debug.LogError("Error parsing output data from register user");
						goto IL_00DD;
					}
				}
				Debug.LogError("Output data from register user corrupted or tampered with");
			}
		}
		IL_00DD:
		if (cookie != null)
		{
			((Action<bool>)cookie)(flag);
		}
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0001D05C File Offset: 0x0001B25C
	private void ConsolidateFriends(Action<bool> consolidateFriendsCompleted)
	{
		string text;
		if (this._fbFriends != null)
		{
			string[] array = new string[this._fbFriends.Count];
			this._fbFriends.Keys.CopyTo(array, 0);
			text = string.Join(";", array);
		}
		else
		{
			text = string.Empty;
		}
		string text2;
		if (this._gcFriends != null)
		{
			string[] array2 = new string[this._gcFriends.Count];
			this._gcFriends.Keys.CopyTo(array2, 0);
			text2 = string.Join(";", array2);
		}
		else
		{
			text2 = string.Empty;
		}
		if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
		{
			consolidateFriendsCompleted(true);
			return;
		}
		base.StartCoroutine(SocialManager.WWWRequestCoroutine(new SocialManager.WWWComplete(this.WWWConsolidateFriendsCompleted), "/friends.php", consolidateFriendsCompleted, new string[] { "fblist", text, "gclist", text2 }));
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x0001D13C File Offset: 0x0001B33C
	private static string[][] ParseSets(string setsString)
	{
		string[] array = new string[] { ");(" };
		string[] array2 = setsString.Split(array, StringSplitOptions.RemoveEmptyEntries);
		if (array2.Length != 0)
		{
			if (array2[0][0] == '(')
			{
				array2[0] = array2[0].Substring(1);
			}
			int num = array2.Length - 1;
			int num2 = array2[num].Length - 1;
			if (array2[num][num2] == ')')
			{
				array2[num] = array2[num].Remove(num2);
			}
			string[][] array3 = new string[array2.Length][];
			for (int i = 0; i < array2.Length; i++)
			{
				array3[i] = array2[i].Split(new char[] { ';' });
			}
			return array3;
		}
		return new string[0][];
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
	private void WWWConsolidateFriendsCompleted(SocialManager.WWWRequestResult result, string output, object cookie)
	{
		bool flag = false;
		if (result == SocialManager.WWWRequestResult.Success)
		{
			Dictionary<string, string> dictionary = StringUtility.ParseProperties(output);
			Debug.Log("Parse properties");
			if (dictionary.ContainsKey("friendslist"))
			{
				Debug.Log("props contain friendslist");
				string text = dictionary["friendslist"];
				string text2 = dictionary["checksum"];
				string checksum = SocialManager.GetChecksum(text);
				if (string.Compare(text2, checksum, true) == 0)
				{
					Debug.Log("Checksum fits");
					if (string.IsNullOrEmpty(text))
					{
						this._friends = null;
						Debug.Log("no friends");
					}
					else
					{
						Debug.Log("Friends exist");
						string[][] array = SocialManager.ParseSets(text);
						this._friends = new List<Friend>(array.Length);
						string[][] array2 = array;
						int i = 0;
						while (i < array2.Length)
						{
							string[] array3 = array2[i];
							Debug.Log("foreach friend");
							if (array3.Length == 6 && (array3[1].Length > 0 || array3[2].Length > 0))
							{
								try
								{
									Debug.Log("Trying to create friend");
									Friend friend = new Friend();
									friend.userid = int.Parse(array3[0]);
									string text3 = array3[1];
									if (text3.Length > 0)
									{
										Debug.Log("gcid length > 0: " + text3);
										friend.gcProfile = this._gcFriends[text3];
									}
									string text4 = array3[2];
									if (text4.Length > 0)
									{
										Debug.Log("fbid > 0");
										if (this._fbProfiles == null)
										{
											this._fbProfiles = new Dictionary<string, FacebookProfile>();
										}
										FacebookProfile facebookProfile;
										if (this._fbProfiles.ContainsKey(text4))
										{
											facebookProfile = this._fbProfiles[text4];
										}
										else
										{
											Hashtable hashtable = this._fbFriends[text4];
											facebookProfile = new FacebookProfile();
											facebookProfile.id = text4;
											facebookProfile.name = (string)hashtable["first_name"];
											facebookProfile.fullName = (string)hashtable["name"];
											this._fbProfiles[text4] = facebookProfile;
										}
										friend.fbProfile = facebookProfile;
									}
									friend.score = int.Parse(array3[3]);
									friend.meters = int.Parse(array3[4]);
									friend.games = int.Parse(array3[5]);
									if (this._friendStatus == null)
									{
										this._friendStatus = new Dictionary<string, Friend.Status>();
									}
									Friend.Status status;
									if (friend.fbProfile != null && this._friendStatus.ContainsKey(friend.fbProfile.id))
									{
										Debug.Log("found fb");
										status = this._friendStatus[friend.fbProfile.id];
									}
									else if (friend.gcProfile != null && this._friendStatus.ContainsKey(friend.gcProfile.id))
									{
										Debug.Log("found gc");
										status = this._friendStatus[friend.gcProfile.id];
									}
									else
									{
										Debug.Log("creating new");
										status = new Friend.Status();
										status.gamesCashedIn = friend.games;
										string text5 = ((friend.fbProfile == null) ? friend.gcProfile.id : friend.fbProfile.id);
										this._friendStatus[text5] = status;
										this._dirty = true;
									}
									friend.status = status;
									this._friends.Add(friend);
									goto IL_0379;
								}
								catch (Exception ex)
								{
									Debug.LogError("Friend parse error " + ex.ToString());
									goto IL_0379;
								}
								goto IL_0359;
							}
							goto IL_0359;
							IL_0379:
							i++;
							continue;
							IL_0359:
							Debug.LogError("Malformed friend: (" + string.Join(";", array3) + ")");
							goto IL_0379;
						}
						if (this._fbProfiles != null)
						{
							base.StartCoroutine(this.DownloadFacebookPictures(this._fbProfiles));
						}
					}
					Debug.Log("success");
					flag = true;
				}
				else
				{
					Debug.LogError("Consolidated friend data corrupted");
				}
			}
		}
		if (cookie != null)
		{
			((Action<bool>)cookie)(flag);
		}
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
	public void ReportScore(int newScore, int newMeters)
	{
		if (this._userid > 0)
		{
			base.StartCoroutine(SocialManager.WWWRequestCoroutine(null, "/report.php", null, new string[]
			{
				"userid",
				this._userid.ToString(),
				"score",
				newScore.ToString(),
				"meters",
				newMeters.ToString()
			}));
			if (Social.localUser.authenticated)
			{
				Social.ReportScore((long)newScore, "com.kiloo.subwaysurfers.ScoreLeaderboard", new Action<bool>(this.GameCenterCallBack));
				return;
			}
			Debug.Log("Game Center localuser was not authenticated");
		}
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0001D67B File Offset: 0x0001B87B
	private void GameCenterCallBack(bool success)
	{
		Debug.Log("Game center score report " + ((!success) ? "failure" : "success"));
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0001D69C File Offset: 0x0001B89C
	public void UpdateFriendScores(Action<bool> updateFriendsScoresCompleted)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (Friend friend in this._friends)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(';');
			}
			stringBuilder.Append(friend.userid);
		}
		string text = stringBuilder.ToString();
		base.StartCoroutine(SocialManager.WWWRequestCoroutine(new SocialManager.WWWComplete(this.WWWUpdateFriendScoresCompleted), "/scores.php", updateFriendsScoresCompleted, new string[] { "idlist", text }));
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x0001D744 File Offset: 0x0001B944
	private void WWWUpdateFriendScoresCompleted(SocialManager.WWWRequestResult result, string output, object cookie)
	{
		bool flag = false;
		if (result == SocialManager.WWWRequestResult.Success)
		{
			Dictionary<string, string> dictionary = StringUtility.ParseProperties(output);
			if (dictionary.ContainsKey("scores"))
			{
				string text = dictionary["scores"];
				string text2 = dictionary["checksum"];
				string checksum = SocialManager.GetChecksum(text);
				if (string.Compare(text2, checksum, true) == 0)
				{
					try
					{
						string[][] array = SocialManager.ParseSets(text);
						for (int i = 0; i < array.Length; i++)
						{
							string[] array2 = array[i];
							if (array2.Length != 4)
							{
								Debug.LogError("UpdateFriendScores: Malformed score (" + string.Join(";", array2) + ")");
								throw new Exception();
							}
							int userid = int.Parse(array2[0]);
							Friend friend = this._friends.Find((Friend f) => f.userid == userid);
							if (friend != null)
							{
								int num = int.Parse(array2[1]);
								int num2 = int.Parse(array2[2]);
								int num3 = int.Parse(array2[3]);
								friend.score = num;
								friend.meters = num2;
								friend.games = num3;
							}
							else
							{
								Debug.LogWarning("UpdateFriendScores: Unexpected friend user id");
							}
						}
						flag = true;
						goto IL_0138;
					}
					catch (Exception)
					{
						Debug.LogError("UpdateFriendScores: Error parsing output data");
						goto IL_0138;
					}
				}
				Debug.LogError("UpdateFriendScores: Output data corrupt");
			}
		}
		IL_0138:
		if (cookie != null)
		{
			((Action<bool>)cookie)(flag);
		}
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x0001D8A8 File Offset: 0x0001BAA8
	public void Poke(Friend friend)
	{
		string text = ((friend.fbProfile != null) ? this._fbProfile.fullName : ((!Social.localUser.authenticated) ? string.Empty : Social.localUser.userName));
		base.StartCoroutine(SocialManager.WWWRequestCoroutine(null, "/poke.php", null, new string[]
		{
			"friend",
			friend.userid.ToString(),
			"name",
			text
		}));
		friend.status.lastPokeTime = DateTime.UtcNow;
		this._dirty = true;
		Flurry.LogGenericSocialAction();
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x0001D93F File Offset: 0x0001BB3F
	public void SetPokeFirstTime(Friend friend)
	{
		friend.status.lastPokeTime = DateTime.UtcNow;
		this._dirty = true;
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x0001D958 File Offset: 0x0001BB58
	public void BragNotify(int oldScore, List<Friend> friends)
	{
		if (friends == null)
		{
			return;
		}
		int count = friends.Count;
		StringBuilder stringBuilder = new StringBuilder(count * 8);
		StringBuilder stringBuilder2 = new StringBuilder(count * 2);
		foreach (Friend friend in friends)
		{
			int relation = friend.relation;
			int userid = friend.userid;
			if (relation != 0 && userid != 0)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(';');
					stringBuilder2.Append(';');
				}
				stringBuilder.Append(userid);
				stringBuilder2.Append(relation);
			}
		}
		if (stringBuilder.Length > 0)
		{
			base.StartCoroutine(SocialManager.WWWRequestCoroutine(null, "/brag.php", null, new string[]
			{
				"oldscore",
				oldScore.ToString(),
				"newscore",
				PlayerInfo.Instance.highestScore.ToString(),
				"useridlist",
				stringBuilder.ToString(),
				"relationlist",
				stringBuilder2.ToString(),
				"fbname",
				(this._fbProfile == null) ? string.Empty : this._fbProfile.name,
				"gcname",
				(!Social.localUser.authenticated) ? string.Empty : Social.localUser.userName
			}));
			Flurry.LogGenericSocialAction();
		}
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x0001DAC8 File Offset: 0x0001BCC8
	private static string GetDeviceTypeString()
	{
		return "iDevice";
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x0001DAD0 File Offset: 0x0001BCD0
	public void RecommendAppFacebook()
	{
		if (this.facebookIsLoggedIn)
		{
			FacebookBinding.showPostMessageDialogWithOptions(new Hashtable
			{
				{ "link", "http://redirect.kiloo.com/subwayapp.php" },
				{ "picture", "http://hoodrunner.kiloo.com/fblogo.png" },
				{ "name", "Subway Surfers" },
				{ "caption", "Dodge the trains! Help Jake, Tricky and Fresh escape." },
				{ "description", "Try out Subway Surfers for free on iOS!" }
			});
			return;
		}
		Debug.LogError("Not logged in to facebook");
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x0001DB4C File Offset: 0x0001BD4C
	public void BragFacebook(List<Friend> friends)
	{
		if (this.facebookIsLoggedIn)
		{
			List<Friend> list = null;
			if (friends != null)
			{
				list = new List<Friend>(friends.Count);
				foreach (Friend friend in friends)
				{
					if (friend.fbProfile != null && friend.score < PlayerInfo.Instance.highestScore)
					{
						list.Add(friend);
					}
				}
				list.Sort((Friend x, Friend y) => y.score - x.score);
			}
			string text = ((list == null || list.Count == 0) ? string.Concat(new string[]
			{
				"I just scored ",
				PlayerInfo.Instance.highestScore.ToString(),
				" points dodging trains in Subway Surfers on my ",
				SocialManager.GetDeviceTypeString(),
				". Check it out!"
			}) : ((list.Count == 1) ? string.Concat(new string[]
			{
				"I just scored ",
				PlayerInfo.Instance.highestScore.ToString(),
				" points in Subway Surfers on my ",
				SocialManager.GetDeviceTypeString(),
				" and beat ",
				list[0].fbProfile.fullName
			}) : ((list.Count == 2) ? string.Concat(new string[]
			{
				"I just scored ",
				PlayerInfo.Instance.highestScore.ToString(),
				" points in Subway Surfers on my ",
				SocialManager.GetDeviceTypeString(),
				" and beat ",
				list[0].fbProfile.fullName,
				" and ",
				list[1].fbProfile.fullName
			}) : ((list.Count != 3) ? string.Concat(new string[]
			{
				"I just scored ",
				PlayerInfo.Instance.highestScore.ToString(),
				" points in Subway Surfers on my ",
				SocialManager.GetDeviceTypeString(),
				" and beat ",
				list[0].fbProfile.fullName,
				", ",
				list[1].fbProfile.fullName,
				" and ",
				(list.Count - 2).ToString(),
				" others"
			}) : string.Concat(new string[]
			{
				"I just scored ",
				PlayerInfo.Instance.highestScore.ToString(),
				" points in Subway Surfers on my ",
				SocialManager.GetDeviceTypeString(),
				" and beat ",
				list[0].fbProfile.fullName,
				", ",
				list[1].fbProfile.fullName,
				" and ",
				list[2].fbProfile.fullName
			})))));
			FacebookBinding.showPostMessageDialogWithOptions(new Hashtable
			{
				{ "link", "http://redirect.kiloo.com/subwayapp.php" },
				{ "picture", "http://hoodrunner.kiloo.com/fblogo.png" },
				{ "name", "Subway Surfers" },
				{ "caption", "New Subway Surfers High Score" },
				{ "description", text }
			});
			Flurry.LogGenericSocialAction();
			return;
		}
		Debug.LogError("Not logged in to facebook");
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x0001DED0 File Offset: 0x0001C0D0
	public void BragTwitter(int oldScore)
	{
		if (TwitterBinding.isLoggedIn())
		{
			TwitterBinding.showTweetComposer(string.Concat(new string[]
			{
				"I just scored ",
				PlayerInfo.Instance.highestScore.ToString(),
				" points dodging trains in Subway Surfers on my ",
				SocialManager.GetDeviceTypeString(),
				". Check it out: http://redirect.kiloo.com/subwayapp.php"
			}), null);
			Flurry.LogGenericSocialAction();
		}
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x0001DF2F File Offset: 0x0001C12F
	private IEnumerator DownloadFacebookPicture(FacebookProfile profile)
	{
		if (profile == null)
		{
			Debug.LogError("facebook profile was null in DownloadFacebookPictures!");
			yield break;
		}
		string text = "http://graph.facebook.com/" + profile.id + "/picture?type=square";
		Debug.Log(string.Concat(new string[] { "www getting facebook image for ", profile.name, " at \"", text, "\"" }));
		WWW www = new WWW(text);
		yield return www;
		if (www.error != null)
		{
			Debug.LogWarning("www failed getting image for " + profile.name + ": " + www.error);
		}
		Texture2D texture = www.texture;
		if (texture == null || (texture.width == 8 && texture.height == 8))
		{
			Debug.LogWarning("www done but no image for " + profile.name);
			yield break;
		}
		profile.image = texture;
		Debug.Log(string.Concat(new string[]
		{
			"www done, got image for ",
			profile.name,
			" (width=",
			profile.image.width.ToString(),
			", height=",
			profile.image.height.ToString(),
			")"
		}));
		yield break;
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x0001DF3E File Offset: 0x0001C13E
	private IEnumerator DownloadFacebookPictures(Dictionary<string, FacebookProfile> fbProfiles)
	{
		List<FacebookProfile> list = new List<FacebookProfile>(fbProfiles.Count);
		foreach (FacebookProfile facebookProfile in fbProfiles.Values)
		{
			if (facebookProfile.image == null)
			{
				list.Add(facebookProfile);
			}
		}
		foreach (FacebookProfile facebookProfile2 in list)
		{
			yield return base.StartCoroutine(this.DownloadFacebookPicture(facebookProfile2));
		}
		List<FacebookProfile>.Enumerator enumerator2 = default(List<FacebookProfile>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x040004D5 RID: 1237
	private const byte VERSION = 1;

	// Token: 0x040004D6 RID: 1238
	private const string FACEBOOK_APPID = "254616967963463";

	// Token: 0x040004D7 RID: 1239
	private const string TWITTER_CONSUMER_KEY = "VKV2NMbj7YIEGblD97ZFSw";

	// Token: 0x040004D8 RID: 1240
	private const string TWITTER_CONSUMER_SECRET = "z1Wy3GXYL4XS9z9a2YbE4KWF3T0ynAFBwwwxZSYDI";

	// Token: 0x040004D9 RID: 1241
	private const bool DEBUG_SET_DEBUG_POST_FIELD = false;

	// Token: 0x040004DA RID: 1242
	private const string BASE_URL = "http://hoodrunner.kiloo.com";

	// Token: 0x040004DB RID: 1243
	private const string REGISTER_DEVICE_URL = "/register.php";

	// Token: 0x040004DC RID: 1244
	private const string REPORT_SCORE_URL = "/report.php";

	// Token: 0x040004DD RID: 1245
	private const string CONSOLIDATE_FRIENDS_URL = "/friends.php";

	// Token: 0x040004DE RID: 1246
	private const string UPDATE_FRIEND_SCORES_URL = "/scores.php";

	// Token: 0x040004DF RID: 1247
	private const string POKE_URL = "/poke.php";

	// Token: 0x040004E0 RID: 1248
	private const string BRAG_URL = "/brag.php";

	// Token: 0x040004E1 RID: 1249
	private const string SECRET = "resxrctrv7tgv7gb8h9h9u0909kllfmolkjnhghgjjkhjghg";

	// Token: 0x040004E2 RID: 1250
	private static SocialManager _instance;

	// Token: 0x040004E3 RID: 1251
	private int _userid;

	// Token: 0x040004E4 RID: 1252
	private FacebookProfile _fbProfile;

	// Token: 0x040004E5 RID: 1253
	private List<Friend> _friends;

	// Token: 0x040004E6 RID: 1254
	private Dictionary<string, Hashtable> _fbFriends;

	// Token: 0x040004E7 RID: 1255
	private Dictionary<string, IUserProfile> _gcFriends;

	// Token: 0x040004E8 RID: 1256
	private bool _gameCenterAuthenticationComplete;

	// Token: 0x040004E9 RID: 1257
	private bool _gameCenterFriendListRequestComplete;

	// Token: 0x040004EA RID: 1258
	private bool _fbReady;

	// Token: 0x040004EB RID: 1259
	private bool _twitterReady;

	// Token: 0x040004EC RID: 1260
	private SocialManager.FacebookCurrentRequest _fbCurrentRequest;

	// Token: 0x040004ED RID: 1261
	private SocialManager.TwitterCurrentRequest _twitterCurrentRequest;

	// Token: 0x040004EE RID: 1262
	private bool _doneLoggingIn;

	// Token: 0x040004EF RID: 1263
	private bool _consolidatedFriendsCompleted;

	// Token: 0x040004F0 RID: 1264
	private Dictionary<string, Friend.Status> _friendStatus;

	// Token: 0x040004F1 RID: 1265
	private bool _dirty;

	// Token: 0x040004F2 RID: 1266
	private Dictionary<string, FacebookProfile> _fbProfiles;

	// Token: 0x040004F3 RID: 1267
	public static bool debugGUI;

	// Token: 0x040004F4 RID: 1268
	private Dictionary<string, FacebookProfile> _debugFacebookFriends;

	// Token: 0x040004F5 RID: 1269
	private Vector2 _debugGCScrollPosition = new Vector2(0f, 0f);

	// Token: 0x040004F6 RID: 1270
	private Vector2 _debugFBScrollPosition = new Vector2(0f, 0f);

	// Token: 0x020001D2 RID: 466
	private enum FacebookCurrentRequest
	{
		// Token: 0x04000B02 RID: 2818
		None,
		// Token: 0x04000B03 RID: 2819
		Error,
		// Token: 0x04000B04 RID: 2820
		LoggingIn,
		// Token: 0x04000B05 RID: 2821
		GettingUserInfo,
		// Token: 0x04000B06 RID: 2822
		GettingFriends
	}

	// Token: 0x020001D3 RID: 467
	private enum TwitterCurrentRequest
	{
		// Token: 0x04000B08 RID: 2824
		None,
		// Token: 0x04000B09 RID: 2825
		Error,
		// Token: 0x04000B0A RID: 2826
		LoggingIn
	}

	// Token: 0x020001D4 RID: 468
	private enum WWWRequestResult
	{
		// Token: 0x04000B0C RID: 2828
		Success,
		// Token: 0x04000B0D RID: 2829
		Error
	}

	// Token: 0x020001D5 RID: 469
	// (Invoke) Token: 0x06000BBC RID: 3004
	private delegate void WWWComplete(SocialManager.WWWRequestResult result, string output, object cookie);
}
