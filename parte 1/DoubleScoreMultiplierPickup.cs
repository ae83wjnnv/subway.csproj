using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class DoubleScoreMultiplierPickup : MonoBehaviour
{
	// Token: 0x060002B3 RID: 691 RVA: 0x0000BDBA File Offset: 0x00009FBA
	public void Awake()
	{
		this.game = Game.Instance;
		Pickup component = base.GetComponent<Pickup>();
		component.OnPickup = (Pickup.OnPickupDelegate)Delegate.Combine(component.OnPickup, new Pickup.OnPickupDelegate(this.OnPickup));
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0000BDEE File Offset: 0x00009FEE
	private void OnPickup(CharacterPickupParticles particles)
	{
		this.game.Modifiers.Add(this.game.Modifiers.DoubleScoreMultiplier);
		GameStats.Instance.doubleMultiplierPickups++;
		particles.PickedUpPowerUp();
	}

	// Token: 0x040001F7 RID: 503
	private Game game;
}
