using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
[RequireComponent(typeof(Pickup))]
public class Coin : MonoBehaviour
{
	// Token: 0x06000226 RID: 550 RVA: 0x0000962C File Offset: 0x0000782C
	private void Awake()
	{
		this.pivot = base.transform.GetChild(0);
		this.initialPivotPosition = this.pivot.localPosition;
		this.pickup = base.GetComponent<Pickup>();
		Pickup pickup = this.pickup;
		pickup.OnPickup = (Pickup.OnPickupDelegate)Delegate.Combine(pickup.OnPickup, new Pickup.OnPickupDelegate(this.OnPickup));
		TrackObject component = base.GetComponent<TrackObject>();
		component.OnActivate = (TrackObject.OnActivateDelegate)Delegate.Combine(component.OnActivate, new TrackObject.OnActivateDelegate(this.OnActivate));
	}

	// Token: 0x06000227 RID: 551 RVA: 0x000096B8 File Offset: 0x000078B8
	private void OnPickup(CharacterPickupParticles pickupParticles)
	{
		GameStats instance = GameStats.Instance;
		int coins = instance.coins;
		instance.coins = coins + 1;
		pickupParticles.PickedUpCoin(this.pickup);
	}

	// Token: 0x06000228 RID: 552 RVA: 0x000096E5 File Offset: 0x000078E5
	private void OnActivate()
	{
		this.pivot.localPosition = this.initialPivotPosition;
	}

	// Token: 0x04000160 RID: 352
	[HideInInspector]
	public Transform pivot;

	// Token: 0x04000161 RID: 353
	private Vector3 initialPivotPosition;

	// Token: 0x04000162 RID: 354
	private Pickup pickup;
}
