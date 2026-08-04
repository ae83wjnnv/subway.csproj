using System;
using System.Collections;

// Token: 0x0200004B RID: 75
public class DoubleScoreMultiplier : CharacterModifier
{
	// Token: 0x060002B0 RID: 688 RVA: 0x0000BD96 File Offset: 0x00009F96
	public override IEnumerator Begin()
	{
		GameStats instance = GameStats.Instance;
		int usePowerups = instance.usePowerups;
		instance.usePowerups = usePowerups + 1;
		this.Paused = false;
		this.stop = CharacterModifier.StopSignal.DONT_STOP;
		this.Powerup = GameStats.Instance.TriggerPowerup(PowerupType.doubleMultiplier);
		float duration = this.Powerup.timeLeft;
		PlayerInfo.Instance.doubleScore = true;
		while (this.Powerup.timeLeft > 0f && this.stop == CharacterModifier.StopSignal.DONT_STOP)
		{
			this.ratio = this.Powerup.timeLeft / duration;
			yield return 0;
		}
		PlayerInfo.Instance.doubleScore = false;
		yield break;
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0000BDA5 File Offset: 0x00009FA5
	public override void Reset()
	{
		PlayerInfo.Instance.doubleScore = false;
	}

	// Token: 0x040001F5 RID: 501
	public float ratio;

	// Token: 0x040001F6 RID: 502
	public ActivePowerup Powerup;
}
