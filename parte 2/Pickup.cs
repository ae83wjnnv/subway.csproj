using System;
using UnityEngine;

// Token: 0x020000A5 RID: 165
public class Pickup : MonoBehaviour
{
	// Token: 0x060004E4 RID: 1252 RVA: 0x00017A9B File Offset: 0x00015C9B
	public void NotifyPickup(CharacterPickupParticles pickupParticles)
	{
		if (this.OnPickup != null)
		{
			this.OnPickup(pickupParticles);
		}
	}

	// Token: 0x04000420 RID: 1056
	public Pickup.OnPickupDelegate OnPickup;

	// Token: 0x04000421 RID: 1057
	public bool CanBeSpawned = true;

	// Token: 0x020001C9 RID: 457
	// (Invoke) Token: 0x06000B99 RID: 2969
	public delegate void OnPickupDelegate(CharacterPickupParticles pickupParticles);
}
