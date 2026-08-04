using System;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class TestStats : MonoBehaviour
{
	// Token: 0x06000637 RID: 1591 RVA: 0x0001F1EB File Offset: 0x0001D3EB
	private void Start()
	{
		Game instance = Game.Instance;
		instance.OnGameOver = (Game.OnGameOverDelegate)Delegate.Combine(instance.OnGameOver, new Game.OnGameOverDelegate(this.OnGameOver));
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x0001F214 File Offset: 0x0001D414
	private void OnGameOver(GameStats stats)
	{
		this.gamesPlayed++;
		this.durationTotal += stats.duration;
		this.durationAvg = this.durationTotal / (float)this.gamesPlayed;
		this.coinsTotal += stats.coins;
		this.coinsAvg = (float)(this.coinsTotal / this.gamesPlayed);
		this.metersTotal += stats.meters;
		this.metersAvg = this.metersTotal / (float)this.gamesPlayed;
		this.jumpsTotal += stats.jumps;
		this.jumpsAvg = (float)(this.jumpsTotal / this.gamesPlayed);
		this.rollsTotal += stats.rolls;
		this.rollsAvg = (float)(this.rollsTotal / this.gamesPlayed);
		this.pickupsTotal += stats.jetpackPickups + stats.superSneakerPickups + stats.letterPickups + stats.coinMagnetsPickups + stats.mysteryBoxPickups;
		this.pickupsAvg = (float)this.pickupsTotal;
		this.trackChangesTotal = stats.trackChanges;
		this.trackChangesAvg = (float)(this.trackChangesTotal / this.gamesPlayed);
	}

	// Token: 0x04000541 RID: 1345
	public int gamesPlayed;

	// Token: 0x04000542 RID: 1346
	public float durationTotal;

	// Token: 0x04000543 RID: 1347
	public float durationAvg = float.NaN;

	// Token: 0x04000544 RID: 1348
	public int coinsTotal;

	// Token: 0x04000545 RID: 1349
	public float coinsAvg = float.NaN;

	// Token: 0x04000546 RID: 1350
	public float metersTotal;

	// Token: 0x04000547 RID: 1351
	public float metersAvg = float.NaN;

	// Token: 0x04000548 RID: 1352
	public int jumpsTotal;

	// Token: 0x04000549 RID: 1353
	public float jumpsAvg = float.NaN;

	// Token: 0x0400054A RID: 1354
	public int rollsTotal;

	// Token: 0x0400054B RID: 1355
	public float rollsAvg = float.NaN;

	// Token: 0x0400054C RID: 1356
	public int pickupsTotal;

	// Token: 0x0400054D RID: 1357
	public float pickupsAvg = float.NaN;

	// Token: 0x0400054E RID: 1358
	public int trackChangesTotal;

	// Token: 0x0400054F RID: 1359
	public float trackChangesAvg = float.NaN;
}
