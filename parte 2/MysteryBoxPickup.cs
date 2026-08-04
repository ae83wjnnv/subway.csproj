using System;
using UnityEngine;

// Token: 0x0200009B RID: 155
public class MysteryBoxPickup : MonoBehaviour
{
	// Token: 0x0600048F RID: 1167 RVA: 0x00015E63 File Offset: 0x00014063
	private void Awake()
	{
		this.game = Game.Instance;
		Pickup component = base.GetComponent<Pickup>();
		component.OnPickup = (Pickup.OnPickupDelegate)Delegate.Combine(component.OnPickup, new Pickup.OnPickupDelegate(this.OnPickup));
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x00015E98 File Offset: 0x00014098
	private void OnPickup(CharacterPickupParticles particles)
	{
		PlayerInfo instance = PlayerInfo.Instance;
		int num = instance.mysteryBoxesToUnlock;
		instance.mysteryBoxesToUnlock = num + 1;
		GameStats instance2 = GameStats.Instance;
		num = instance2.mysteryBoxPickups;
		instance2.mysteryBoxPickups = num + 1;
		particles.PickedUpPowerUp();
		GameStats.Instance.AddScoreForPickup(PowerupType.mysterybox);
	}

	// Token: 0x04000401 RID: 1025
	private Game game;
}
