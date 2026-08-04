using System;
using UnityEngine;

// Token: 0x0200011F RID: 287
public class UIMysteryBoxButton : UIBasicButton
{
	// Token: 0x06000850 RID: 2128 RVA: 0x0002B880 File Offset: 0x00029A80
	protected override void Send()
	{
		MysteryBoxReward mysteryBoxReward = MysteryBox.Roll();
		Debug.Log("Reward: " + mysteryBoxReward.amount.ToString() + "x" + mysteryBoxReward.type.ToString());
	}
}
