using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class DailyLetterPickup : MonoBehaviour
{
	// Token: 0x1700001E RID: 30
	// (set) Token: 0x06000289 RID: 649 RVA: 0x0000B660 File Offset: 0x00009860
	public char Letter
	{
		set
		{
			this.letter = value;
			if (this.HasDailyLetter)
			{
				int num = (int)(this.letter - 'A');
				if (num < this.Letters.Count && num >= 0)
				{
					this.LetterMesh.mesh = this.Letters[num];
				}
			}
			this.SetVisible(this.HasDailyLetter);
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600028A RID: 650 RVA: 0x0000B6BB File Offset: 0x000098BB
	private bool HasDailyLetter
	{
		get
		{
			return this.letter > '\0';
		}
	}

	// Token: 0x0600028B RID: 651 RVA: 0x0000B6C8 File Offset: 0x000098C8
	private void Awake()
	{
		this.pickup = base.GetComponent<Pickup>();
		Pickup pickup = this.pickup;
		pickup.OnPickup = (Pickup.OnPickupDelegate)Delegate.Combine(pickup.OnPickup, new Pickup.OnPickupDelegate(this.OnPickup));
		DailyLetterPickupManager.Instance.InitializePickup(this);
		TrackObject trackObject = base.GetComponent<TrackObject>() ?? base.gameObject.AddComponent<TrackObject>();
		trackObject.OnActivate = (TrackObject.OnActivateDelegate)Delegate.Combine(trackObject.OnActivate, new TrackObject.OnActivateDelegate(this.OnActivate));
		trackObject.OnDeactivate = (TrackObject.OnDeactivateDelegate)Delegate.Combine(trackObject.OnDeactivate, new TrackObject.OnDeactivateDelegate(this.OnDeactivate));
	}

	// Token: 0x0600028C RID: 652 RVA: 0x0000B76B File Offset: 0x0000996B
	private void OnActivate()
	{
		this.SetVisible(this.HasDailyLetter);
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0000B779 File Offset: 0x00009979
	private void OnDeactivate()
	{
		this.SetVisible(false);
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0000B782 File Offset: 0x00009982
	private void SetVisible(bool visible)
	{
		this.pickupCollider.enabled = visible;
		this.meshRenderer.enabled = visible;
		if (this.glow != null)
		{
			this.glow.SetVisible(visible);
		}
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0000B7B6 File Offset: 0x000099B6
	private void OnPickup(CharacterPickupParticles particles)
	{
		base.StartCoroutine(this.PickupCoroutine(particles));
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0000B7C6 File Offset: 0x000099C6
	private IEnumerator PickupCoroutine(CharacterPickupParticles particles)
	{
		this.SetVisible(false);
		GameStats.Instance.AddScoreForPickup(PowerupType.letters);
		PlayerInfo.Instance.PickedupLetter(this.letter);
		particles.PickedUpPowerUp();
		GameStats instance = GameStats.Instance;
		int letterPickups = instance.letterPickups;
		instance.letterPickups = letterPickups + 1;
		yield return new WaitForSeconds(2f);
		DailyLetterPickupManager.Instance.UpdateLetter();
		yield break;
	}

	// Token: 0x040001CB RID: 459
	public Collider pickupCollider;

	// Token: 0x040001CC RID: 460
	public MeshRenderer meshRenderer;

	// Token: 0x040001CD RID: 461
	public Glow glow;

	// Token: 0x040001CE RID: 462
	public MeshFilter LetterMesh;

	// Token: 0x040001CF RID: 463
	public List<Mesh> Letters;

	// Token: 0x040001D0 RID: 464
	public bool shouldSpawnParticles;

	// Token: 0x040001D1 RID: 465
	private Pickup pickup;

	// Token: 0x040001D2 RID: 466
	private char letter;
}
