using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000082 RID: 130
public class Jetpack : CharacterState
{
	// Token: 0x1700005A RID: 90
	// (get) Token: 0x0600041C RID: 1052 RVA: 0x000124BC File Offset: 0x000106BC
	public override bool PauseActiveModifiers
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x0600041D RID: 1053 RVA: 0x000124BF File Offset: 0x000106BF
	public float LandingZ
	{
		get
		{
			return this.landingZ;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x0600041E RID: 1054 RVA: 0x000124C7 File Offset: 0x000106C7
	public float LandingTime
	{
		get
		{
			return this.landingTime;
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x0600041F RID: 1055 RVA: 0x000124CF File Offset: 0x000106CF
	public static Jetpack Instance
	{
		get
		{
			Jetpack jetpack;
			if ((jetpack = Jetpack.instance) == null)
			{
				jetpack = (Jetpack.instance = Object.FindObjectOfType(typeof(Jetpack)) as Jetpack);
			}
			return jetpack;
		}
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x000124F4 File Offset: 0x000106F4
	public void Awake()
	{
		this.game = Game.Instance;
		this.track = Track.Instance;
		this.character = Character.Instance;
		this.characterController = this.character.characterController;
		this.characterTransform = this.characterController.transform;
		this.characterCamera = CharacterCamera.Instance;
		this.characterCameraTransform = this.characterCamera.transform;
		this.characterAnimation = this.character.characterAnimation;
		this.coinsManager = this.FindObject<InAirCoinsManager>();
		Variable<bool> isInGame2 = this.game.isInGame;
		isInGame2.OnChange = (Variable<bool>.OnChangeDelegate)Delegate.Combine(isInGame2.OnChange, new Variable<bool>.OnChangeDelegate(delegate(bool isInGame)
		{
			if (!isInGame)
			{
				this.TurnOffEffects();
			}
		}));
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x000125A9 File Offset: 0x000107A9
	public override IEnumerator Begin()
	{
		GameStats gameStats = GameStats.Instance;
		int usePowerups = gameStats.usePowerups;
		gameStats.usePowerups = usePowerups + 1;
		this.Powerup = GameStats.Instance.TriggerPowerup(this.powerType);
		this.audioStateLoop.ChangeLoop(AudioState.Jetpack);
		this.game.Modifiers.PauseInJetpackMode();
		this.character.Stumble = false;
		this.powerupMesh.active = true;
		this.character.shadow.active = false;
		this.ChangeAnimations();
		this.characterAnimation.CrossFade(this.character.animations.run);
		Vector3 startCameraOffset = this.characterCameraTransform.position - this.characterTransform.position;
		float startCameraAimOffset = this.game.Running.cameraAimOffset;
		float startY = this.characterTransform.position.y;
		this.characterController.detectCollisions = false;
		this.character.characterCollider.enabled = false;
		this.fuel = 1f;
		new SmoothDampFloat(this.characterTransform.position.y, this.ySmoothDuration).Target = this.flyHeight;
		float jetpackSpeed = ((!this.headStart) ? (this.game.currentLevelSpeed * this.speedup) : this.headStartSpeed);
		float num = ((!this.headStart) ? this.Powerup.timeLeft : (this.headStartDistance / this.headStartSpeed));
		float num2 = jetpackSpeed * num;
		float num3 = jetpackSpeed * this.flyAheadDuration;
		float num4 = num3 + num2;
		num4 = this.track.LayJetpackChunks(this.character.z, num4) - this.stopBeforeLandingChunkDistance * Game.Instance.NormalizedGameSpeed;
		float num5 = num4 / jetpackSpeed;
		float extendedFlyDuration = num5 - this.flyAheadDuration;
		num2 = extendedFlyDuration * jetpackSpeed;
		if (!this.headStart)
		{
			float num6 = num2 - this.coinOffset;
			float num7 = this.character.z + num3 + this.coinOffset;
			this.coinsManager.Spawn(num7, num6, this.flyHeight);
		}
		else
		{
			So.Instance.playSound(this.StartSound);
		}
		this.landingTime = Time.time + num5;
		this.landingZ = this.character.z + num4;
		float cameraZ = this.character.z;
		float startTime2 = Time.time;
		float num8 = (Time.time - startTime2) / this.flyAheadDuration;
		this.JetpackParticles.SetActiveRecursively(true);
		this.game.currentSpeed = jetpackSpeed;
		Vector3 cameraPositionStart = this.characterCamera.position;
		Vector3 cameraTargetStart = this.characterCamera.target;
		Vector3 initRot = this.JetpackParticles.transform.rotation.eulerAngles;
		Vector3 initScale = this.JetpackParticles.transform.localScale;
		Debug.DrawLine(cameraPositionStart, cameraTargetStart, Color.red, 1000f);
		while (num8 < 1f)
		{
			this.game.HandleControls();
			Vector3 vector = Vector3.Lerp(startCameraOffset, this.cameraOffset, Mathf.SmoothStep(0f, 1f, num8));
			float num9 = Mathf.Lerp(startCameraAimOffset, this.cameraAimOffset, Mathf.SmoothStep(0f, 1f, num8));
			float num10 = Mathf.Lerp(0f, 1f, this.fisso.Evaluate(num8));
			this.character.z += jetpackSpeed * Time.deltaTime;
			Vector3 vector2 = this.track.GetPosition(this.character.x, this.character.z) + Vector3.up * (startY + (this.flyHeight - startY) * Mathf.SmoothStep(0f, 1f, num8));
			this.characterTransform.position = vector2;
			cameraZ += ((!this.headStart) ? this.game.currentLevelSpeed : jetpackSpeed) * Time.deltaTime;
			Vector3 vector3 = new Vector3(this.track.GetPosition(this.character.x, this.character.z).x, vector2.y, this.character.z) + vector;
			Vector3 vector4 = vector2 + Vector3.up * num9;
			float num11 = this.flyAhead.cameraMovement.Evaluate(num8);
			Vector3 vector5 = Vector3.Lerp(cameraPositionStart, vector3, num11);
			Vector3 vector6 = Vector3.Lerp(cameraTargetStart, vector4, num11);
			vector5.x = vector3.x;
			vector6.x = vector3.x;
			this.characterCamera.position = vector5;
			this.characterCamera.target = vector6;
			Debug.DrawLine(vector3, vector4, Color.red);
			this.JetpackParticles.transform.rotation = Quaternion.Euler(initRot - new Vector3(num10 * 1f, 0f, 0f));
			this.JetpackParticles.transform.localScale = initScale + new Vector3(0f, 0f, num10 * 2f);
			this.game.UpdateMeters();
			this.game.LayTrackChunks();
			yield return 0;
			num8 = (Time.time - startTime2) / this.flyAheadDuration;
		}
		this.character.characterCollider.enabled = true;
		float width = 200f;
		Debug.DrawLine(Vector3.forward * this.landingZ + Vector3.left * width, Vector3.forward * this.landingZ + -Vector3.left * width, Color.red, 100f);
		Debug.DrawLine(Vector3.forward * this.landingZ + Vector3.left * width, Vector3.forward * this.landingZ + -Vector3.left * width, Color.blue, 100f);
		startTime2 = Time.time;
		num8 = (Time.time - startTime2) / extendedFlyDuration;
		while (num8 < 1f)
		{
			this.game.HandleControls();
			this.character.z += jetpackSpeed * Time.deltaTime;
			this.character.transform.position = this.track.GetPosition(this.character.x, this.character.z) + Vector3.up * this.flyHeight;
			Vector3 position = this.character.transform.position;
			this.characterCamera.position = position + this.cameraOffset;
			this.characterCamera.target = position + Vector3.up * this.cameraAimOffset;
			this.game.UpdateMeters();
			this.game.LayTrackChunks();
			yield return 0;
			num8 = (Time.time - startTime2) / extendedFlyDuration;
			this.fuel = 1f - num8;
			Debug.DrawLine(Vector3.forward * this.character.z + Vector3.left * width, Vector3.forward * this.character.z + -Vector3.left * width, Color.green);
		}
		if (this.OnStop != null)
		{
			this.OnStop();
		}
		this.characterController.detectCollisions = true;
		this.TurnOffEffects();
		this.coinsManager.ReleaseCoins();
		this.game.Modifiers.Resume();
		this.game.ChangeState(this.game.Running);
		this.character.ChangeAnimations();
		yield break;
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x000125B8 File Offset: 0x000107B8
	private void TurnOffEffects()
	{
		this.JetpackParticles.SetActiveRecursively(false);
		this.powerupMesh.active = false;
		this.audioStateLoop.ChangeLoop(AudioState.JetpackStop);
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x000125E0 File Offset: 0x000107E0
	private void ChangeAnimations()
	{
		this.character.animations.run = "jetpack_forward";
		this.character.animations.dodgeLeft = "jetpack_forward";
		this.character.animations.dodgeRight = "jetpack_forward";
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x0001262C File Offset: 0x0001082C
	public override void HandleSwipe(SwipeDir swipeDir)
	{
		switch (swipeDir)
		{
		case SwipeDir.Left:
			this.character.ChangeTrack(-1, this.characterChangeTrackLength / this.game.currentSpeed);
			return;
		case SwipeDir.Right:
			this.character.ChangeTrack(1, this.characterChangeTrackLength / this.game.currentSpeed);
			break;
		case SwipeDir.None:
			break;
		default:
			return;
		}
	}

	// Token: 0x0400035F RID: 863
	public Vector3 cameraOffset = new Vector3(0f, 33f, -33f);

	// Token: 0x04000360 RID: 864
	public float cameraOffsetSmoothDuration = 1f;

	// Token: 0x04000361 RID: 865
	public float cameraAimOffset = 20f;

	// Token: 0x04000362 RID: 866
	public float cameraFOV = 60f;

	// Token: 0x04000363 RID: 867
	public float ySmoothDuration = 0.5f;

	// Token: 0x04000364 RID: 868
	public float speedup = 2f;

	// Token: 0x04000365 RID: 869
	public float flyHeight = 95f;

	// Token: 0x04000366 RID: 870
	public float coinOffset = 200f;

	// Token: 0x04000367 RID: 871
	public float flyAheadDuration = 1.5f;

	// Token: 0x04000368 RID: 872
	private float flyingDuration;

	// Token: 0x04000369 RID: 873
	public float calmDownDuration = 2f;

	// Token: 0x0400036A RID: 874
	public float stopBeforeLandingChunkDistance = 50f;

	// Token: 0x0400036B RID: 875
	public float characterAngle = 45f;

	// Token: 0x0400036C RID: 876
	public float characterChangeTrackLength = 60f;

	// Token: 0x0400036D RID: 877
	public GameObject powerupMesh;

	// Token: 0x0400036E RID: 878
	public AudioClipInfo StartSound;

	// Token: 0x0400036F RID: 879
	public bool headStart;

	// Token: 0x04000370 RID: 880
	public float headStartDistance;

	// Token: 0x04000371 RID: 881
	public float headStartSpeed = 100f;

	// Token: 0x04000372 RID: 882
	public PowerupType powerType;

	// Token: 0x04000373 RID: 883
	public Jetpack.OnStopDelegate OnStop;

	// Token: 0x04000374 RID: 884
	public ActivePowerup Powerup;

	// Token: 0x04000375 RID: 885
	public Jetpack.FlyAheadInfo flyAhead;

	// Token: 0x04000376 RID: 886
	public GameObject JetpackParticles;

	// Token: 0x04000377 RID: 887
	private Game game;

	// Token: 0x04000378 RID: 888
	private Track track;

	// Token: 0x04000379 RID: 889
	private Character character;

	// Token: 0x0400037A RID: 890
	private CharacterController characterController;

	// Token: 0x0400037B RID: 891
	private Transform characterTransform;

	// Token: 0x0400037C RID: 892
	private CharacterCamera characterCamera;

	// Token: 0x0400037D RID: 893
	private Transform characterCameraTransform;

	// Token: 0x0400037E RID: 894
	private Animation characterAnimation;

	// Token: 0x0400037F RID: 895
	public InAirCoinsManager coinsManager;

	// Token: 0x04000380 RID: 896
	private float fuel;

	// Token: 0x04000381 RID: 897
	private float landingZ;

	// Token: 0x04000382 RID: 898
	private float landingTime;

	// Token: 0x04000383 RID: 899
	public AudioStateLoop audioStateLoop;

	// Token: 0x04000384 RID: 900
	public AnimationCurve fisso;

	// Token: 0x04000385 RID: 901
	private static Jetpack instance;

	// Token: 0x020001B1 RID: 433
	[Serializable]
	public class FlyAheadInfo
	{
		// Token: 0x04000A0E RID: 2574
		public AnimationCurve cameraMovement;
	}

	// Token: 0x020001B2 RID: 434
	// (Invoke) Token: 0x06000B31 RID: 2865
	public delegate void OnStopDelegate();
}
