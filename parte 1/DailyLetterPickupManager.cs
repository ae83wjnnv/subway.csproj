using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class DailyLetterPickupManager : MonoBehaviour
{
	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000292 RID: 658 RVA: 0x0000B7E4 File Offset: 0x000099E4
	public static DailyLetterPickupManager Instance
	{
		get
		{
			if (DailyLetterPickupManager.instance == null)
			{
				DailyLetterPickupManager.instance = new GameObject("DailyLetterPickupManager").AddComponent<DailyLetterPickupManager>();
			}
			return DailyLetterPickupManager.instance;
		}
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0000B80C File Offset: 0x00009A0C
	private void NotifyPickups()
	{
		foreach (DailyLetterPickup dailyLetterPickup in this.pickups)
		{
			dailyLetterPickup.Letter = this.letter;
		}
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0000B864 File Offset: 0x00009A64
	public void InitializePickup(DailyLetterPickup pickup)
	{
		this.pickups.Add(pickup);
		pickup.Letter = this.letter;
	}

	// Token: 0x06000295 RID: 661 RVA: 0x0000B87F File Offset: 0x00009A7F
	public void UpdateLetter()
	{
		this.letter = PlayerInfo.Instance.GetNewDailyLetter();
		this.NotifyPickups();
	}

	// Token: 0x040001D3 RID: 467
	public const char NO_LETTER = '\0';

	// Token: 0x040001D4 RID: 468
	private char letter;

	// Token: 0x040001D5 RID: 469
	private HashSet<DailyLetterPickup> pickups = new HashSet<DailyLetterPickup>();

	// Token: 0x040001D6 RID: 470
	private static DailyLetterPickupManager instance;
}
