using System;
using UnityEngine;

// Token: 0x02000083 RID: 131
public class JetpackPickup : MonoBehaviour
{
	// Token: 0x06000427 RID: 1063 RVA: 0x00012753 File Offset: 0x00010953
	private void Awake()
	{
		this.game = Game.Instance;
		Pickup component = base.GetComponent<Pickup>();
		component.OnPickup = (Pickup.OnPickupDelegate)Delegate.Combine(component.OnPickup, new Pickup.OnPickupDelegate(this.OnPickup));
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x00012787 File Offset: 0x00010987
	private void OnPickup(CharacterPickupParticles particles)
	{
		this.game.PickupJetpack();
		particles.PickedUpPowerUp();
	}

	// Token: 0x04000386 RID: 902
	private Game game;
}
