using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CB RID: 203
public class SpawnPointManager
{
	// Token: 0x17000094 RID: 148
	// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001E327 File Offset: 0x0001C527
	public static SpawnPointManager Instance
	{
		get
		{
			SpawnPointManager spawnPointManager;
			if ((spawnPointManager = SpawnPointManager.instance) == null)
			{
				spawnPointManager = (SpawnPointManager.instance = new SpawnPointManager());
			}
			return spawnPointManager;
		}
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0001E340 File Offset: 0x0001C540
	public SpawnPointManager()
	{
		float distancePerMeter = Game.Instance.distancePerMeter;
		Upgrade upgrade = Upgrades.upgrades[PowerupType.letters];
		this.dailyLetter = new SpawnPointManager.PickupType();
		this.dailyLetter.spawnDistanceMin *= distancePerMeter;
		this.dailyLetter.spawnProbability = upgrade.spawnProbability;
		this.dailyLetter.ExtractGameObject = (SpawnPoint spawnPoint) => spawnPoint.dailyLetter;
		Upgrade upgrade2 = Upgrades.upgrades[PowerupType.doubleMultiplier];
		this.doubleScoreMultiplier = new SpawnPointManager.PickupType();
		this.doubleScoreMultiplier.spawnDistanceMin = (float)upgrade2.minimumMeters * distancePerMeter;
		this.doubleScoreMultiplier.spawnProbability = upgrade2.spawnProbability;
		this.doubleScoreMultiplier.ExtractGameObject = (SpawnPoint spawnPoint) => spawnPoint.doubleScoreMultiplier;
		Upgrade upgrade3 = Upgrades.upgrades[PowerupType.jetpack];
		this.jetpackPickup = new SpawnPointManager.PickupType();
		this.jetpackPickup.spawnDistanceMin = (float)upgrade3.minimumMeters * distancePerMeter;
		this.jetpackPickup.spawnProbability = upgrade3.spawnProbability;
		this.jetpackPickup.ExtractGameObject = (SpawnPoint spawnPoint) => spawnPoint.jetpackPickup;
		Upgrade upgrade4 = Upgrades.upgrades[PowerupType.supersneakers];
		this.jumpBooster = new SpawnPointManager.PickupType();
		this.jumpBooster.spawnDistanceMin = (float)upgrade4.minimumMeters * distancePerMeter;
		this.jumpBooster.spawnProbability = upgrade4.spawnProbability;
		this.jumpBooster.ExtractGameObject = (SpawnPoint spawnPoint) => spawnPoint.jumpBooster;
		Upgrade upgrade5 = Upgrades.upgrades[PowerupType.coinmagnet];
		this.magnetBooster = new SpawnPointManager.PickupType();
		this.magnetBooster.spawnDistanceMin = (float)upgrade5.minimumMeters * distancePerMeter;
		this.magnetBooster.spawnProbability = upgrade5.spawnProbability;
		this.magnetBooster.ExtractGameObject = (SpawnPoint spawnPoint) => spawnPoint.magnetBooster;
		Upgrade upgrade6 = Upgrades.upgrades[PowerupType.mysterybox];
		this.mysteryBox = new SpawnPointManager.PickupType();
		this.mysteryBox.spawnDistanceMin = (float)upgrade6.minimumMeters * distancePerMeter;
		this.mysteryBox.spawnProbability = upgrade6.spawnProbability;
		this.mysteryBox.ExtractGameObject = (SpawnPoint spawnPoint) => spawnPoint.mysteryBox;
		this.pickups = new SpawnPointManager.PickupType[] { this.dailyLetter, this.doubleScoreMultiplier, this.jetpackPickup, this.jumpBooster, this.magnetBooster, this.mysteryBox };
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x0001E610 File Offset: 0x0001C810
	public void PerformSelection(SpawnPoint spawnPoint, List<GameObject> objectsToVisit)
	{
		float z = spawnPoint.transform.position.z;
		SpawnPointManager.PickupType pickupType = null;
		if (z > this.spawnZ)
		{
			List<SpawnPointManager.PickupType> list = new List<SpawnPointManager.PickupType>(this.pickups).FindAll((SpawnPointManager.PickupType p) => p.spawnZ < z);
			if (list.Count > 0)
			{
				float[] array = new float[list.Count];
				float num = 0f;
				for (int i = 0; i < list.Count; i++)
				{
					num = (array[i] = num + list[i].spawnProbability);
				}
				float num2 = Random.Range(0f, num);
				for (int j = 0; j < array.Length; j++)
				{
					if (num2 < array[j])
					{
						pickupType = list[j];
						pickupType.spawnZ = z + pickupType.spawnDistanceMin;
						break;
					}
				}
				this.spawnZ = z + this.spawnSpacing;
			}
		}
		for (int k = 0; k < this.pickups.Length; k++)
		{
			SpawnPointManager.PickupType pickupType2 = this.pickups[k];
			GameObject gameObject = pickupType2.ExtractGameObject(spawnPoint);
			if (pickupType2 == pickupType)
			{
				objectsToVisit.Add(gameObject);
			}
			else
			{
				gameObject.SetActiveRecursively(false);
			}
		}
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0001E754 File Offset: 0x0001C954
	public void Restart()
	{
		float distancePerMeter = Game.Instance.distancePerMeter;
		this.spawnZ = Upgrades.UpgradeFirstSpawnMeters * distancePerMeter;
		this.spawnSpacing = Upgrades.UpgradeSpawnSpacingMeters * distancePerMeter;
		SpawnPointManager.PickupType[] array = this.pickups;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].spawnZ = float.MinValue;
		}
	}

	// Token: 0x04000509 RID: 1289
	private static SpawnPointManager instance;

	// Token: 0x0400050A RID: 1290
	private SpawnPointManager.PickupType dailyLetter;

	// Token: 0x0400050B RID: 1291
	private SpawnPointManager.PickupType doubleScoreMultiplier;

	// Token: 0x0400050C RID: 1292
	private SpawnPointManager.PickupType jetpackPickup;

	// Token: 0x0400050D RID: 1293
	private SpawnPointManager.PickupType jumpBooster;

	// Token: 0x0400050E RID: 1294
	private SpawnPointManager.PickupType magnetBooster;

	// Token: 0x0400050F RID: 1295
	private SpawnPointManager.PickupType mysteryBox;

	// Token: 0x04000510 RID: 1296
	private SpawnPointManager.PickupType[] pickups;

	// Token: 0x04000511 RID: 1297
	private float spawnZ;

	// Token: 0x04000512 RID: 1298
	private float spawnSpacing;

	// Token: 0x04000513 RID: 1299
	private float totalProbability;

	// Token: 0x04000514 RID: 1300
	private float[] accumulatedProbability;

	// Token: 0x020001DE RID: 478
	private class PickupType
	{
		// Token: 0x04000B3A RID: 2874
		public Func<SpawnPoint, GameObject> ExtractGameObject;

		// Token: 0x04000B3B RID: 2875
		public float spawnProbability;

		// Token: 0x04000B3C RID: 2876
		public float spawnDistanceMin;

		// Token: 0x04000B3D RID: 2877
		public float spawnZ;
	}
}
