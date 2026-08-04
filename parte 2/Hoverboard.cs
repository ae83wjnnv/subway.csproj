using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class Hoverboard : CharacterModifier
{
	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060003C3 RID: 963 RVA: 0x00010F87 File Offset: 0x0000F187
	public override bool ShouldPauseInJetpack
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060003C4 RID: 964 RVA: 0x00010F8A File Offset: 0x0000F18A
	public static Hoverboard Instance
	{
		get
		{
			Hoverboard hoverboard;
			if ((hoverboard = Hoverboard.instance) == null)
			{
				hoverboard = (Hoverboard.instance = Object.FindObjectOfType(typeof(Hoverboard)) as Hoverboard);
			}
			return hoverboard;
		}
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00010FAF File Offset: 0x0000F1AF
	public void Awake()
	{
		this.character = Character.Instance;
		this.track = Track.Instance;
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x00010FC8 File Offset: 0x0000F1C8
	public override void Reset()
	{
		this.character.immuneToCriticalHit = false;
		this.character.characterController.enabled = true;
		this.character.characterCollider.enabled = true;
		this.powerupMesh.active = false;
		this.isActive = false;
		Time.timeScale = 1f;
		this.character.hoverboardCrashParticleSystem.gameObject.SetActiveRecursively(false);
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x00011036 File Offset: 0x0000F236
	public override IEnumerator Begin()
	{
		float num = Time.time - this.lastEndActivationTime;
		if (!this.isAllowed || num < this.WaitForParticlesDelay + PlayerInfo.Instance.GetHoverBoardCoolDown())
		{
			yield break;
		}
		GameStats gameStats = GameStats.Instance;
		int usePowerups = gameStats.usePowerups;
		gameStats.usePowerups = usePowerups + 1;
		PlayerInfo.Instance.UseUpgrade(PowerupType.hoverboard);
		Missions.Instance.PlayerDidThis(Missions.MissionTarget.HoverBoard, 1);
		this.Paused = false;
		this.character.Stumble = false;
		this.isActive = true;
		this.character.ChangeAnimations();
		this.character.characterAnimation.CrossFade("h_skate_on", 0.06f);
		this.character.characterAnimation.CrossFadeQueued("h_run", 0.2f);
		So.Instance.playSound(this.StartSound);
		this.character.CharacterPickupParticleSystem.PickedUpDefaultPowerUp();
		this.character.immuneToCriticalHit = true;
		this.stop = CharacterModifier.StopSignal.DONT_STOP;
		this.Powerup = GameStats.Instance.TriggerPowerup(PowerupType.hoverboard);
		this.duration = this.Powerup.timeLeft;
		this.powerupMesh.active = true;
		while (this.Powerup.timeLeft > 0f && this.stop == CharacterModifier.StopSignal.DONT_STOP)
		{
			yield return 0;
		}
		if (this.stop == CharacterModifier.StopSignal.DONT_STOP)
		{
			Missions.Instance.PlayerDidThis(Missions.MissionTarget.HoverBoardExpire, 1);
		}
		this.powerupMesh.active = false;
		this.character.immuneToCriticalHit = false;
		this.isActive = false;
		this.character.ChangeAnimations();
		this.lastEndActivationTime = Time.time;
		if (this.stop != CharacterModifier.StopSignal.STOP)
		{
			yield break;
		}
		this.isActive = false;
		this.character.immuneToCriticalHit = false;
		this.character.hoverboardCrashParticleSystem.gameObject.SetActiveRecursively(true);
		this.character.hoverboardCrashParticleSystem.Play();
		this.PlayCrashSound();
		float timeLeft = this.WaitForParticlesDelay;
		while (timeLeft > 0f)
		{
			timeLeft -= Time.deltaTime;
			yield return 0;
		}
		this.track.LayEmptyChunks(this.character.z, this.RemoveObstaclesDistance * Game.Instance.NormalizedGameSpeed);
		this.character.jumping = true;
		this.character.falling = false;
		this.character.verticalSpeed = this.character.CalculateJumpVerticalSpeed(10f);
		this.character.characterAnimation.CrossFade(this.character.animations.jump, 0.05f);
		float num2 = this.slowMotionDistance * Game.Instance.NormalizedGameSpeed;
		float newCoolDownDist = this.cooldownDstance * Game.Instance.NormalizedGameSpeed;
		float distanceLeft = num2;
		bool didStopCooldown = false;
		while (distanceLeft > 0f)
		{
			distanceLeft -= Game.Instance.currentLevelSpeed * Time.deltaTime;
			newCoolDownDist -= Game.Instance.currentLevelSpeed * Time.deltaTime;
			if (newCoolDownDist < 0f && !didStopCooldown)
			{
				this.character.immuneToCriticalHit = false;
				didStopCooldown = true;
			}
			yield return 0;
		}
		this.character.hoverboardCrashParticleSystem.gameObject.SetActiveRecursively(false);
		yield break;
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x00011045 File Offset: 0x0000F245
	public void PlayCrashSound()
	{
		So.Instance.playSound(this.CrashSound);
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00011058 File Offset: 0x0000F258
	public override void Pause()
	{
		this.powerupMesh.active = false;
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00011066 File Offset: 0x0000F266
	public override void Resume()
	{
		this.powerupMesh.active = true;
	}

	// Token: 0x0400030F RID: 783
	private float duration;

	// Token: 0x04000310 RID: 784
	public float cooldownDstance = 50f;

	// Token: 0x04000311 RID: 785
	public float slowMotionDistance = 90f;

	// Token: 0x04000312 RID: 786
	public float slowDownToScale = 0.3f;

	// Token: 0x04000313 RID: 787
	public bool isAllowed = true;

	// Token: 0x04000314 RID: 788
	public GameObject powerupMesh;

	// Token: 0x04000315 RID: 789
	public float WaitForParticlesDelay;

	// Token: 0x04000316 RID: 790
	public float RemoveObstaclesDistance = 250f;

	// Token: 0x04000317 RID: 791
	private Game game;

	// Token: 0x04000318 RID: 792
	private Character character;

	// Token: 0x04000319 RID: 793
	private Track track;

	// Token: 0x0400031A RID: 794
	private float lastEndActivationTime;

	// Token: 0x0400031B RID: 795
	[HideInInspector]
	public bool isActive;

	// Token: 0x0400031C RID: 796
	public AudioClipInfo CrashSound;

	// Token: 0x0400031D RID: 797
	public AudioClipInfo StartSound;

	// Token: 0x0400031E RID: 798
	public ActivePowerup Powerup;

	// Token: 0x0400031F RID: 799
	private static Hoverboard instance;
}
