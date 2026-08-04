using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

// Token: 0x0200005D RID: 93
public class Friend
{
	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060002FF RID: 767 RVA: 0x0000D573 File Offset: 0x0000B773
	public string name
	{
		get
		{
			if (this.fbProfile != null)
			{
				return this.fbProfile.name;
			}
			if (this.gcProfile != null)
			{
				return this.gcProfile.userName;
			}
			Debug.LogError("Friend not initialized");
			return null;
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000300 RID: 768 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
	public Texture2D image
	{
		get
		{
			if (this.fbProfile != null)
			{
				return this.fbProfile.image;
			}
			if (this.gcProfile != null)
			{
				return this.gcProfile.image;
			}
			Debug.LogError("Friend not initialized");
			return null;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06000301 RID: 769 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
	public int relation
	{
		get
		{
			int num = 0;
			if (this.fbProfile != null)
			{
				num |= 1;
			}
			if (this.gcProfile != null)
			{
				num |= 2;
			}
			return num;
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x06000302 RID: 770 RVA: 0x0000D608 File Offset: 0x0000B808
	public string id
	{
		get
		{
			if (this.fbProfile != null)
			{
				return this.fbProfile.id;
			}
			if (this.gcProfile != null)
			{
				return this.gcProfile.id;
			}
			return null;
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000303 RID: 771 RVA: 0x0000D633 File Offset: 0x0000B833
	public int gamesToCashIn
	{
		get
		{
			return this.games - this.status.gamesCashedIn;
		}
	}

	// Token: 0x04000234 RID: 564
	public int userid;

	// Token: 0x04000235 RID: 565
	public int score;

	// Token: 0x04000236 RID: 566
	public int meters;

	// Token: 0x04000237 RID: 567
	public int games;

	// Token: 0x04000238 RID: 568
	public IUserProfile gcProfile;

	// Token: 0x04000239 RID: 569
	public FacebookProfile fbProfile;

	// Token: 0x0400023A RID: 570
	public Friend.Status status;

	// Token: 0x02000196 RID: 406
	public class Status
	{
		// Token: 0x0400099C RID: 2460
		public DateTime lastPokeTime = DateTime.MinValue;

		// Token: 0x0400099D RID: 2461
		public int gamesCashedIn;
	}
}
