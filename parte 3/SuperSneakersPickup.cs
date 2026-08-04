using System;
using UnityEngine;

// Token: 0x020000D7 RID: 215
public class SuperSneakersPickup : MonoBehaviour
{
	// Token: 0x06000633 RID: 1587 RVA: 0x0001F160 File Offset: 0x0001D360
	private void Awake()
	{
		this.game = Game.Instance;
		Pickup component = base.GetComponent<Pickup>();
		component.OnPickup = (Pickup.OnPickupDelegate)Delegate.Combine(component.OnPickup, new Pickup.OnPickupDelegate(this.OnPickup));
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x0001F194 File Offset: 0x0001D394
	private void OnPickup(CharacterPickupParticles particles)
	{
		this.game.Modifiers.Add(this.game.Modifiers.SuperSneakes);
		GameStats instance = GameStats.Instance;
		int superSneakerPickups = instance.superSneakerPickups;
		instance.superSneakerPickups = superSneakerPickups + 1;
		particles.PickedUpPowerUp();
	}

	// Token: 0x04000536 RID: 1334
	private Game game;
}
