using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class SuperSneakers : CharacterModifier
{
	// Token: 0x17000095 RID: 149
	// (get) Token: 0x0600062B RID: 1579 RVA: 0x0001F070 File Offset: 0x0001D270
	public override bool ShouldPauseInJetpack
	{
		get
		{
			return true;
		}
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0001F074 File Offset: 0x0001D274
	public void Awake()
	{
		this.character = Character.Instance;
		this.characterAnimation = this.character.characterAnimation;
		this.objects = Object.FindObjectsOfType(typeof(SuperSneakersGroup)) as SuperSneakersGroup[];
		this.characterController = this.character.characterController;
		this.game = Game.Instance;
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x0001F0D3 File Offset: 0x0001D2D3
	public override void Reset()
	{
		this.ratio = 0f;
		this.Paused = false;
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0001F0E7 File Offset: 0x0001D2E7
	public override IEnumerator Begin()
	{
		GameStats instance = GameStats.Instance;
		int i = instance.usePowerups;
		instance.usePowerups = i + 1;
		this.Paused = false;
		this.character.Stumble = false;
		this.isActive = true;
		this.powerupMesh.active = true;
		this.character.ChangeAnimations();
		this.Powerup = GameStats.Instance.TriggerPowerup(PowerupType.supersneakers);
		float duration = this.Powerup.timeLeft;
		this.coinMagnetCollider.OnEnter = new OnTriggerObject.OnEnterDelegate(this.CoinHit);
		this.coinMagnetCollider.GetComponent<Collider>().enabled = true;
		this.character.jumpHeight = this.character.jumpHeightSuperSneakers;
		this.stop = CharacterModifier.StopSignal.DONT_STOP;
		SuperSneakersGroup[] array = this.objects;
		for (i = 0; i < array.Length; i++)
		{
			array[i].GroupActive = true;
		}
		while (this.Powerup.timeLeft > 0f && this.stop == CharacterModifier.StopSignal.DONT_STOP)
		{
			yield return 0;
			this.ratio = this.Powerup.timeLeft / duration;
		}
		this.coinMagnetCollider.GetComponent<Collider>().enabled = false;
		OnTriggerObject onTriggerObject = this.coinMagnetCollider;
		onTriggerObject.OnEnter = (OnTriggerObject.OnEnterDelegate)Delegate.Remove(onTriggerObject.OnEnter, new OnTriggerObject.OnEnterDelegate(this.CoinHit));
		this.ratio = 0f;
		array = this.objects;
		for (i = 0; i < array.Length; i++)
		{
			array[i].GroupActive = false;
		}
		this.character.jumpHeight = this.character.jumpHeightNormal;
		this.powerupMesh.active = false;
		this.isActive = false;
		this.character.ChangeAnimations();
		yield break;
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0001F0F8 File Offset: 0x0001D2F8
	public void CoinHit(Collider collider)
	{
		Coin component = collider.GetComponent<Coin>();
		if (component != null)
		{
			component.GetComponent<Collider>().enabled = false;
			base.StartCoroutine(this.Pull(component));
		}
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0001F12F File Offset: 0x0001D32F
	private IEnumerator Pull(Coin coin)
	{
		Transform pivot = coin.pivot.transform;
		Vector3 position = pivot.position;
		float magnitude = (position - this.characterController.transform.position).magnitude;
		yield return base.StartCoroutine(pTween.To(magnitude / (this.pullSpeed * this.game.NormalizedGameSpeed), delegate(float t)
		{
			pivot.position = Vector3.Lerp(position, this.powerupMesh.transform.position, t * t);
		}));
		Pickup component = coin.GetComponent<Pickup>();
		this.character.NotifyPickup(component);
		yield break;
	}

	// Token: 0x0400052A RID: 1322
	private float duration;

	// Token: 0x0400052B RID: 1323
	public GameObject powerupMesh;

	// Token: 0x0400052C RID: 1324
	private Animation characterAnimation;

	// Token: 0x0400052D RID: 1325
	[HideInInspector]
	public bool isActive;

	// Token: 0x0400052E RID: 1326
	public OnTriggerObject coinMagnetCollider;

	// Token: 0x0400052F RID: 1327
	public float pullSpeed = 200f;

	// Token: 0x04000530 RID: 1328
	private CharacterController characterController;

	// Token: 0x04000531 RID: 1329
	private float ratio;

	// Token: 0x04000532 RID: 1330
	private Character character;

	// Token: 0x04000533 RID: 1331
	private SuperSneakersGroup[] objects;

	// Token: 0x04000534 RID: 1332
	private Game game;

	// Token: 0x04000535 RID: 1333
	public ActivePowerup Powerup;
}
