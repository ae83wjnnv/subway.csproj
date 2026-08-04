using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class CoinMagnet : CharacterModifier
{
	// Token: 0x06000249 RID: 585 RVA: 0x0000A1DC File Offset: 0x000083DC
	private void Awake()
	{
		this.character = Character.Instance;
		this.characterController = this.character.characterController;
		this.coinEFX = this.character.CharacterPickupParticleSystem.CoinEFX.transform;
		this.characterAnimation = this.character.characterAnimation;
		this.characterAnimation["hold_magnet"].AddMixingTransform(this.shoulder);
		this.characterAnimation["hold_magnet"].layer = 3;
		this.characterAnimation["hold_magnet"].weight = 0.9f;
		this.characterAnimation["hold_magnet"].enabled = false;
		this.game = Game.Instance;
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0000A29D File Offset: 0x0000849D
	public override void Reset()
	{
		this.ratio = 0f;
		this.Paused = false;
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0000A2B1 File Offset: 0x000084B1
	public override IEnumerator Begin()
	{
		GameStats instance = GameStats.Instance;
		int usePowerups = instance.usePowerups;
		instance.usePowerups = usePowerups + 1;
		this.Paused = false;
		this.audioStateLoop.ChangeLoop(AudioState.Magnet);
		this.character.Stumble = false;
		this.powerupMesh.active = true;
		this.characterAnimation["hold_magnet"].enabled = true;
		this.characterAnimation.Play("hold_magnet");
		this.Powerup = GameStats.Instance.TriggerPowerup(PowerupType.coinmagnet);
		this.duration = this.Powerup.timeLeft;
		this.coinMagnetCollider.OnEnter = new OnTriggerObject.OnEnterDelegate(this.CoinHit);
		this.coinMagnetCollider.GetComponent<Collider>().enabled = true;
		base.enabled = true;
		this.stop = CharacterModifier.StopSignal.DONT_STOP;
		while (this.Powerup.timeLeft > 0f && this.stop == CharacterModifier.StopSignal.DONT_STOP)
		{
			yield return 0;
			this.ratio = this.Powerup.timeLeft / this.duration;
		}
		this.coinMagnetCollider.GetComponent<Collider>().enabled = false;
		base.enabled = false;
		this.powerupMesh.active = false;
		this.coinEFX.localPosition = Vector3.zero;
		this.characterAnimation["hold_magnet"].enabled = false;
		this.audioStateLoop.ChangeLoop(AudioState.MagnetStop);
		yield break;
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000A2C0 File Offset: 0x000084C0
	public void CoinHit(Collider collider)
	{
		Coin component = collider.GetComponent<Coin>();
		if (component != null)
		{
			component.GetComponent<Collider>().enabled = false;
			base.StartCoroutine(this.Pull(component));
		}
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000A2F7 File Offset: 0x000084F7
	private IEnumerator Pull(Coin coin)
	{
		Transform pivot = coin.pivot.transform;
		Vector3 position = pivot.position;
		float magnitude = (position - this.characterController.transform.position).magnitude;
		yield return base.StartCoroutine(pTween.To(magnitude / (this.pullSpeed * this.game.NormalizedGameSpeed), delegate(float t)
		{
			pivot.position = Vector3.Lerp(position, this.powerupMesh.transform.position, t * t);
		}));
		this.coinEFX.position = this.powerupMesh.transform.position;
		Pickup component = coin.GetComponent<Pickup>();
		this.character.NotifyPickup(component);
		GameStats instance = GameStats.Instance;
		int coinsCoinMagnet = instance.coinsCoinMagnet;
		instance.coinsCoinMagnet = coinsCoinMagnet + 1;
		yield break;
	}

	// Token: 0x04000187 RID: 391
	private float duration;

	// Token: 0x04000188 RID: 392
	public OnTriggerObject coinMagnetCollider;

	// Token: 0x04000189 RID: 393
	public float pullSpeed = 150f;

	// Token: 0x0400018A RID: 394
	public GameObject powerupMesh;

	// Token: 0x0400018B RID: 395
	private CharacterController characterController;

	// Token: 0x0400018C RID: 396
	private Animation characterAnimation;

	// Token: 0x0400018D RID: 397
	private Character character;

	// Token: 0x0400018E RID: 398
	private Transform coinEFX;

	// Token: 0x0400018F RID: 399
	public Transform shoulder;

	// Token: 0x04000190 RID: 400
	private float ratio;

	// Token: 0x04000191 RID: 401
	private Game game;

	// Token: 0x04000192 RID: 402
	public AudioStateLoop audioStateLoop;

	// Token: 0x04000193 RID: 403
	public ActivePowerup Powerup;
}
