using System;

// Token: 0x0200006D RID: 109
public class Globals
{
	// Token: 0x060003B0 RID: 944 RVA: 0x00010D9F File Offset: 0x0000EF9F
	public static Friend[] GetDebugFriends(int numberOfFriends = 10)
	{
		return new Friend[numberOfFriends];
	}

	// Token: 0x04000302 RID: 770
	public const bool DEBUG = false;

	// Token: 0x04000303 RID: 771
	public const bool DEBUG_FREE_PURCHASES = false;

	// Token: 0x04000304 RID: 772
	public const bool DEBUG_ALL_CHARS = false;

	// Token: 0x04000305 RID: 773
	public const bool DEBUG_ALWAYS_ONLINE = false;

	// Token: 0x04000306 RID: 774
	public const bool DEBUG_FREE_INAPP_PURCHASE = false;
}
