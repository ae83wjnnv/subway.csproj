using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
[RequireComponent(typeof(Pickup))]
public class CoinMagnetPickup : MonoBehaviour
{
	// Token: 0x0600024F RID: 591 RVA: 0x0000A320 File Offset: 0x00008520
	private void Awake()
	{
		this.game = Game.Instance;
		this.pickup = base.GetComponent<Pickup>();
		Pickup pickup = this.pickup;
		pickup.OnPickup = (Pickup.OnPickupDelegate)Delegate.Combine(pickup.OnPickup, new Pickup.OnPickupDelegate(this.OnPickup));
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000A360 File Offset: 0x00008560
	private void OnPickup(CharacterPickupParticles particles)
	{
		this.game.Modifiers.Add(this.game.Modifiers.CoinMagnet);
		GameStats instance = GameStats.Instance;
		int coinMagnetsPickups = instance.coinMagnetsPickups;
		instance.coinMagnetsPickups = coinMagnetsPickups + 1;
		particles.PickedUpPowerUp();
	}

	// Token: 0x04000194 RID: 404
	private Game game;

	// Token: 0x04000195 RID: 405
	private Pickup pickup;
}
