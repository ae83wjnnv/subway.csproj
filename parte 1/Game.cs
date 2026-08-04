using System;
using System.Collections;
using Sensors;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class Game : MonoBehaviour
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000343 RID: 835 RVA: 0x0000F282 File Offset: 0x0000D482
	public bool isPaused
	{
		get
		{
			return this._paused;
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000344 RID: 836 RVA: 0x0000F28C File Offset: 0x0000D48C
	private float Medium
	{
		get
		{
			float num = 0f;
			for (int i = 0; i < 200; i++)
			{
				num += this.yValues[i];
			}
			return num * this.oneOverLengthOYValues;
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000345 RID: 837 RVA: 0x0000F2C2 File Offset: 0x0000D4C2
	public Character Character
	{
		get
		{
			return this.character;
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000346 RID: 838 RVA: 0x0000F2CA File Offset: 0x0000D4CA
	public CharacterState CharacterState
	{
		get
		{
			return this.characterState;
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000347 RID: 839 RVA: 0x0000F2D2 File Offset: 0x0000D4D2
	public CharacterModifierCollection Modifiers
	{
		get
		{
			return this.modifiers;
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000348 RID: 840 RVA: 0x0000F2DA File Offset: 0x0000D4DA
	public Running Running
	{
		get
		{
			return this.running;
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000349 RID: 841 RVA: 0x0000F2E2 File Offset: 0x0000D4E2
	public Jetpack Jetpack
	{
		get
		{
			return this.jetpack;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x0600034A RID: 842 RVA: 0x0000F2EA File Offset: 0x0000D4EA
	public bool IsInJetpackMode
	{
		get
		{
			return this.characterState == this.Jetpack;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x0600034B RID: 843 RVA: 0x0000F2FD File Offset: 0x0000D4FD
	public bool HasSuperSneakers
	{
		get
		{
			return this.modifiers.SuperSneakes.isActive;
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x0600034C RID: 844 RVA: 0x0000F30F File Offset: 0x0000D50F
	public float NormalizedGameSpeed
	{
		get
		{
			return this.currentSpeed / this.speed.min;
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x0600034D RID: 845 RVA: 0x0000F323 File Offset: 0x0000D523
	public static Game Instance
	{
		get
		{
			Game game;
			if ((game = Game.instance) == null)
			{
				game = (Game.instance = Object.FindObjectOfType(typeof(Game)) as Game);
			}
			return game;
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x0600034E RID: 846 RVA: 0x0000F348 File Offset: 0x0000D548
	public static CharacterController Charactercontroller
	{
		get
		{
			CharacterController characterController;
			if ((characterController = Game.characterController) == null)
			{
				characterController = (Game.characterController = Object.FindObjectOfType(typeof(CharacterController)) as CharacterController);
			}
			return characterController;
		}
	}

	// Token: 0x0600034F RID: 847 RVA: 0x0000F370 File Offset: 0x0000D570
	public Game()
	{
		this.isInGame = new Variable<bool>(false);
	}

	// Token: 0x06000350 RID: 848 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
	public void TriggerPause(bool pauseGame)
	{
		this._paused = pauseGame;
		if (pauseGame)
		{
			this.ingameTouchDetection = false;
			Time.timeScale = 0f;
		}
		else
		{
			this.ingameTouchDetection = true;
			Time.timeScale = 1f;
		}
		if (this.OnPauseChange != null)
		{
			this.OnPauseChange(this._paused);
		}
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0000F43C File Offset: 0x0000D63C
	public void StartNewRun()
	{
		this.isInGame.Value = true;
		this.ChangeState(null, this.Intro());
		Action onGameStarted = this.OnGameStarted;
		if (onGameStarted != null)
		{
			onGameStarted();
		}
	}

	// Token: 0x06000352 RID: 850 RVA: 0x0000F474 File Offset: 0x0000D674
	public void Awake()
	{
		Game.HasLoaded = true;
		this.character = Character.Instance;
		this.characterAnimation = this.character.characterAnimation;
		this.guardAnimation = this.character.guardAnimation;
		this.track = Track.Instance;
		this.characterCamera = CharacterCamera.Instance;
		this.characterCameraTransform = this.characterCamera.transform;
		this.distort = this.FindObject<Distort>();
		this.running = Running.Instance;
		this.jetpack = Jetpack.Instance;
		this.enemies = FollowingGuard.Instance;
		this.modifiers = new CharacterModifierCollection();
		Character character = this.character;
		character.OnStumble = (Character.OnStumbleDelegate)Delegate.Combine(character.OnStumble, new Character.OnStumbleDelegate(this.OnStumble));
		Character character2 = this.character;
		character2.OnCriticalHit = (Character.OnCriticalHitDelegate)Delegate.Combine(character2.OnCriticalHit, new Character.OnCriticalHitDelegate(this.OnCriticalHit));
		this.currentLevelSpeed = this.Speed(0f);
		this.player = PlayerInfo.Instance;
		this.stats = GameStats.Instance;
		this.character.SetAnimations();
		this._testStats = base.GetComponent<TestStats>();
		this.awakeDone = true;
	}

	// Token: 0x06000353 RID: 851 RVA: 0x0000F5A6 File Offset: 0x0000D7A6
	public void Start()
	{
		this.track.Restart();
		this.currentThread = this.GameIntro();
		this.currentThread.MoveNext();
	}

	// Token: 0x06000354 RID: 852 RVA: 0x0000F5CC File Offset: 0x0000D7CC
	public void Update()
	{
		Accelerometer.raw(out this.accelData);
		this.accelReadingX = Mathf.Lerp(this.accelReadingX, (float)this.accelData.x, 0.01f);
		float num = Time.time - this.startTime;
		this.currentLevelSpeed = this.Speed(num);
		this.currentThread.MoveNext();
		if (this.characterState != null)
		{
			this.modifiers.Update();
		}
		GameStats.Instance.UpdatePowerupTimes(Time.deltaTime);
	}

	// Token: 0x06000355 RID: 853 RVA: 0x0000F654 File Offset: 0x0000D854
	public void LayTrackChunks()
	{
		this.track.LayTrackChunks(this.character.z);
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0000F66C File Offset: 0x0000D86C
	private void HandleDebugControls()
	{
		this.DebugTimeControl();
		if (Input.GetKeyDown(KeyCode.S))
		{
			this.modifiers.Add(this.modifiers.SuperSneakes);
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			this.ActivateJetpack();
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			this.modifiers.Hoverboard.Begin();
		}
		if (Input.GetKeyDown(KeyCode.M))
		{
			this.modifiers.Add(this.modifiers.CoinMagnet);
		}
		Input.GetKeyDown(KeyCode.H);
		if (this.characterState != null)
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				this.characterState.HandleSwipe(SwipeDir.Up);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				this.characterState.HandleSwipe(SwipeDir.Down);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				this.characterState.HandleSwipe(SwipeDir.Left);
			}
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				this.characterState.HandleSwipe(SwipeDir.Right);
			}
		}
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0000F75C File Offset: 0x0000D95C
	public void UpdateMeters()
	{
		this.stats.meters = (float)Mathf.RoundToInt(this.character.z / this.distancePerMeter);
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0000F784 File Offset: 0x0000D984
	public float CalcTime(float z)
	{
		if (z <= this.Position(this.speed.rampUpDuration))
		{
			float num = this.speed.min * this.speed.min + 2f * ((this.speed.max - this.speed.min) / this.speed.rampUpDuration) * z;
			return (0f - this.speed.min + Mathf.Sqrt(num)) / ((this.speed.max - this.speed.min) / this.speed.rampUpDuration);
		}
		return (z - this.Position(this.speed.rampUpDuration)) * 1f / this.speed.max + this.speed.rampUpDuration;
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0000F85A File Offset: 0x0000DA5A
	public void ChangeState(CharacterState state)
	{
		this.characterState = state;
		if (state != null)
		{
			this.currentThread = state.Begin();
		}
	}

	// Token: 0x0600035A RID: 858 RVA: 0x0000F878 File Offset: 0x0000DA78
	public void ChangeState(CharacterState state, IEnumerator thread)
	{
		this.characterState = state;
		this.currentThread = thread;
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0000F888 File Offset: 0x0000DA88
	public void ActivateJetpack()
	{
		if (this.characterState != this.Jetpack)
		{
			this.ChangeState(this.Jetpack);
		}
	}

	// Token: 0x0600035C RID: 860 RVA: 0x0000F8AC File Offset: 0x0000DAAC
	private float Speed(float t)
	{
		if (t < this.speed.rampUpDuration)
		{
			return t * (this.speed.max - this.speed.min) / this.speed.rampUpDuration + this.speed.min;
		}
		return this.speed.max;
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0000F904 File Offset: 0x0000DB04
	private float Position(float t)
	{
		if (t < this.speed.rampUpDuration)
		{
			return 0.5f * ((this.speed.max - this.speed.min) / this.speed.rampUpDuration) * t * t + this.speed.min * t;
		}
		return (t - this.speed.rampUpDuration) * this.speed.max + 0.5f * (this.speed.max - this.speed.min) * this.speed.rampUpDuration + this.speed.min * this.speed.rampUpDuration;
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0000F9B8 File Offset: 0x0000DBB8
	public void Die()
	{
		if (this.modifiers.IsActive(this.modifiers.Hoverboard))
		{
			this.enemies.MuteProximityLoop();
			this.enemies.ResetCatchUp();
			this.character.stumble = false;
			this.enemies.Restart(false);
			this.modifiers.Hoverboard.Stop = CharacterModifier.StopSignal.STOP;
			GameStats.Instance.RemoveHoverBoardPowerup();
			return;
		}
		if (this.track.IsRunningOnTutorialTrack)
		{
			if (!this.goingBackToCheckpoint)
			{
				base.StartCoroutine(this.BackToCheckPointSequence());
			}
			return;
		}
		GameStats.Instance.ClearPowerups();
		this.isDead = true;
		MovingTrain.ActivateAutoPilot();
		MovingCoin.ActivateAutoPilot();
		if (this.enemies.isShowing)
		{
			if (this.characterAnimation["death_movingTrain"].enabled)
			{
				this.enemies.HitByTrainSequence();
			}
			else
			{
				this.enemies.CatchPlayer(this.character.x - this.character.GetTrackX());
			}
		}
		this.stats.duration = this.GetDuration();
		Missions.Instance.PlayerDidThis(Missions.MissionTarget.TimeDeath, Mathf.FloorToInt(GameStats.Instance.duration));
		this.enemies.enabled = false;
		if (this.OnGameOver != null)
		{
			this.OnGameOver(this.stats);
		}
		Action onGameEnded = this.OnGameEnded;
		if (onGameEnded != null)
		{
			onGameEnded();
		}
		base.StopAllCoroutines();
		this.ChangeState(null, this.SwitchToDieStateWhenGrounded());
	}

	// Token: 0x0600035F RID: 863 RVA: 0x0000FB2C File Offset: 0x0000DD2C
	public float GetDuration()
	{
		return Time.time - this.startTime;
	}

	// Token: 0x06000360 RID: 864 RVA: 0x0000FB3A File Offset: 0x0000DD3A
	private IEnumerator SwitchToDieStateWhenGrounded()
	{
		while (!this.character.characterController.isGrounded)
		{
			this.character.MoveWithGravity();
			yield return 0;
		}
		this.ChangeState(null, this.DieSequence());
		yield break;
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0000FB49 File Offset: 0x0000DD49
	public void OnCriticalHit()
	{
		if (this.characterState != null)
		{
			So.Instance.playSound(this.DieSound);
			this.characterState.HandleCriticalHit();
		}
	}

	// Token: 0x06000362 RID: 866 RVA: 0x0000FB75 File Offset: 0x0000DD75
	private IEnumerator StumbleDeathSequence()
	{
		this.currentSpeed = this.speed.min;
		yield return new WaitForSeconds(0.2f);
		if (this.characterState != this.Jetpack)
		{
			this.characterAnimation.CrossFade("stumbleFall", 0.2f);
			this.characterState.HandleCriticalHit();
		}
		yield break;
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0000FB84 File Offset: 0x0000DD84
	public void OnStumble()
	{
		if (this.character.Stumble && this.characterState != null)
		{
			base.StartCoroutine(this.StumbleDeathSequence());
		}
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0000FBAE File Offset: 0x0000DDAE
	public void StartJetpack()
	{
		this.Jetpack.headStart = false;
		this.Jetpack.powerType = PowerupType.jetpack;
		this.ChangeState(this.Jetpack);
	}

	// Token: 0x06000365 RID: 869 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
	public void PickupJetpack()
	{
		Game.Instance.StartJetpack();
		GameStats gameStats = GameStats.Instance;
		int jetpackPickups = gameStats.jetpackPickups;
		gameStats.jetpackPickups = jetpackPickups + 1;
	}

	// Token: 0x06000366 RID: 870 RVA: 0x0000FBFF File Offset: 0x0000DDFF
	public void StartTopMenu()
	{
		this.ChangeState(null, this.TopMenu());
	}

	// Token: 0x06000367 RID: 871 RVA: 0x0000FC10 File Offset: 0x0000DE10
	public void StartHeadStart2000()
	{
		float powerupDuration = PlayerInfo.Instance.GetPowerupDuration(PowerupType.headstart2000);
		this.Jetpack.headStart = true;
		this.Jetpack.powerType = PowerupType.headstart2000;
		this.Jetpack.headStartDistance = powerupDuration * this.distancePerMeter;
		this.Jetpack.headStartSpeed = 1000f;
		this.ChangeState(this.Jetpack);
		PlayerInfo.Instance.UseUpgrade(PowerupType.headstart2000);
		Missions.Instance.PlayerDidThis(Missions.MissionTarget.Headstart, 1);
	}

	// Token: 0x06000368 RID: 872 RVA: 0x0000FC88 File Offset: 0x0000DE88
	public void StartHeadStart500()
	{
		float powerupDuration = PlayerInfo.Instance.GetPowerupDuration(PowerupType.headstart500);
		this.Jetpack.headStart = true;
		this.Jetpack.powerType = PowerupType.headstart500;
		this.Jetpack.headStartDistance = powerupDuration * this.distancePerMeter;
		this.Jetpack.headStartSpeed = 1000f;
		this.ChangeState(this.Jetpack);
		PlayerInfo.Instance.UseUpgrade(PowerupType.headstart500);
		Missions.Instance.PlayerDidThis(Missions.MissionTarget.Headstart, 1);
	}

	// Token: 0x06000369 RID: 873 RVA: 0x0000FD00 File Offset: 0x0000DF00
	private IEnumerator DieSequence()
	{
		float wait = Time.time + 2f;
		while (Time.time < wait - 1.5f)
		{
			yield return 0;
		}
		while (Time.time < wait && !Input.GetMouseButtonUp(0))
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.touches[0];
				if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
				{
					break;
				}
			}
			yield return 0;
		}
		this.ingameTouchDetection = false;
		UIScreenController.Instance.GameOverTriggered();
		this.ChangeState(null, this.TopMenu());
		yield break;
	}

	// Token: 0x0600036A RID: 874 RVA: 0x0000FD10 File Offset: 0x0000DF10
	private void StageMenuSequence()
	{
		this.characterAnimation[this.character.animations.stumbleDeath].enabled = false;
		this.enemies.enabled = false;
		this.enemies.ShowEnemies(false);
		this.enemies.StopAllCoroutines();
		this.character.StopAllCoroutines();
		this.character.transform.position = Vector3.zero + new Vector3(0f, 0.8f, 0f);
		this.characterAnimation.transform.rotation = Quaternion.identity;
		this.character.sprayCanModel.GetComponent<Renderer>().enabled = true;
		ParticleSystem[] componentsInChildren = this.character.sprayCanModel.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enableEmission = false;
		}
		this.characterCamera.enabled = false;
		this.characterCamera.GetComponent<Camera>().fieldOfView = this.Running.cameraFOV;
		this.characterCameraTransform.localPosition = this.character.transform.position + this.Running.cameraOffset + Vector3.up * 0.8f;
		this.characterCameraTransform.localRotation = Quaternion.Euler(21.50143f, 0f, 0f);
		this.characterAnimation.Play("idlePaint");
	}

	// Token: 0x0600036B RID: 875 RVA: 0x0000FE83 File Offset: 0x0000E083
	private IEnumerator GameIntro()
	{
		this.ingameTouchDetection = true;
		this.StageMenuSequence();
		this.characterCamera.transform.parent.GetComponent<Animation>().Play("introPan");
		float stopTime = Time.time + this.introAnimation.GetComponent<Animation>()["introPan"].length;
		while (Time.time < stopTime && !Input.GetMouseButtonUp(0))
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.touches[0];
				if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
				{
					break;
				}
			}
			yield return 0;
		}
		this.ingameTouchDetection = false;
		UIScreenController.Instance.ShowMainMenu();
		this.ChangeState(null, this.TopMenu());
		yield break;
	}

	// Token: 0x0600036C RID: 876 RVA: 0x0000FE92 File Offset: 0x0000E092
	private IEnumerator TopMenu()
	{
		this.isInGame.Value = false;
		this.audioStateLoop.ChangeLoop(AudioState.Menu);
		this.enemies.MuteProximityLoop();
		this.track.DeactivateTrackChunks();
		this.modifiers.StopWithNoEnding();
		this.modifiers.Update();
		GameStats.Instance.ClearPowerups();
		this.jetpack.coinsManager.ReleaseCoins();
		this.distort.Reset();
		this.enemies.ShowEnemies(false);
		this.StageMenuSequence();
		this.characterCamera.transform.parent.GetComponent<Animation>().CrossFade("menuIdle", 0.1f);
		if (this.OnTopMenu != null)
		{
			this.OnTopMenu();
		}
		yield return null;
		yield break;
	}

	// Token: 0x0600036D RID: 877 RVA: 0x0000FEA1 File Offset: 0x0000E0A1
	private IEnumerator Intro()
	{
		this.stats.Reset();
		this.audioStateLoop.ChangeLoop(AudioState.Ingame);
		this.enemies.MuteProximityLoop();
		this.isDead = false;
		this.ingameTouchDetection = true;
		this.character.CharacterPickupParticleSystem.CoinEFX.transform.localPosition = Vector3.zero;
		ParticleSystem[] componentsInChildren = this.character.sprayCanModel.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enableEmission = false;
		}
		this.StageMenuSequence();
		this.enemies.ShowEnemies(true);
		this.enemies.PlayIntro();
		this.currentLevelSpeed = this.Speed(0f);
		this.startTime = Time.time;
		this.character.Restart();
		SpawnPointManager.Instance.Restart();
		this.track.Restart();
		this.track.LayTrackChunks(0f);
		this.characterCamera.transform.parent.GetComponent<Animation>().CrossFade("startPan", 0.2f);
		this.characterAnimation.CrossFade("introRun", 0.2f);
		IEnumerator cameraMovement = pTween.To(this.characterAnimation.GetComponent<Animation>()["introRun"].length, delegate
		{
		});
		while (cameraMovement.MoveNext())
		{
			yield return 0;
		}
		this.stats.Reset();
		this.character.sprayCanModel.GetComponent<Renderer>().enabled = false;
		this.enemies.enabled = true;
		if (this.track.IsRunningOnTutorialTrack)
		{
			this.enemies.ResetCatchUp();
			this.character.stumble = false;
		}
		this.isReadyForHeadStart = true;
		this.ChangeState(this.Running);
		yield return 0;
		yield break;
	}

	// Token: 0x0600036E RID: 878 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
	private bool HandleTap()
	{
		bool flag = false;
		if (Time.time < this.lastTapTime + this.swipe.doubleTapDuration && this.characterState != null)
		{
			this.characterState.HandleDoubleTap();
			flag = true;
		}
		this.lastTapTime = Time.time;
		return flag;
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0000FF00 File Offset: 0x0000E100
	public void HandleControls()
	{
		if (this._paused)
		{
			return;
		}
		this.HandleDebugControls();
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			this.currentSwipe = new Swipe();
			this.currentSwipe.start = Input.mousePosition;
			this.currentSwipe.startTime = Time.time;
		}
		if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse0)) && this.currentSwipe != null)
		{
			this.currentSwipe.end = Input.mousePosition;
			this.currentSwipe.endTime = Time.time;
			SwipeDir swipeDir = this.AnalyzeSwipe(this.currentSwipe);
			if (swipeDir != SwipeDir.None)
			{
				if (this.characterState != null)
				{
					this.characterState.HandleSwipe(swipeDir);
				}
				this.currentSwipe = null;
			}
		}
		if (Input.GetKeyUp(KeyCode.Mouse0) && this.currentSwipe != null)
		{
			this.currentSwipe.end = Input.mousePosition;
			this.currentSwipe.endTime = Time.time;
			if (this.AnalyzeSwipe(this.currentSwipe) == SwipeDir.None && this.characterState != null)
			{
				this.HandleTap();
			}
		}
		if (Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];
			if (touch.phase == TouchPhase.Began)
			{
				this.currentSwipe = new Swipe();
				this.currentSwipe.start = touch.position;
				this.currentSwipe.startTime = Time.time;
			}
			if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && this.currentSwipe != null)
			{
				this.currentSwipe.endTime = Time.time;
				this.currentSwipe.end = touch.position;
				SwipeDir swipeDir2 = this.AnalyzeSwipe(this.currentSwipe);
				if (swipeDir2 != SwipeDir.None)
				{
					if (this.characterState != null)
					{
						this.characterState.HandleSwipe(swipeDir2);
					}
					this.currentSwipe = null;
				}
			}
			if (touch.phase == TouchPhase.Ended && this.currentSwipe != null)
			{
				this.currentSwipe.endTime = Time.time;
				this.currentSwipe.end = touch.position;
				if (this.AnalyzeSwipe(this.currentSwipe) == SwipeDir.None && this.characterState != null)
				{
					this.HandleTap();
				}
			}
		}
		this.FillYValues();
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00010154 File Offset: 0x0000E354
	private void DebugTimeControl()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Time.timeScale = 0f;
			this.PrintTimeScale();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Time.timeScale = Mathf.Clamp01(Time.timeScale - 0.1f);
			this.PrintTimeScale();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			Time.timeScale += 0.1f;
			this.PrintTimeScale();
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			Time.timeScale = 1f;
			this.PrintTimeScale();
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			Time.timeScale = Mathf.Clamp01(Time.timeScale * 0.9f);
			this.PrintTimeScale();
		}
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			Time.timeScale *= 1.1111112f;
			this.PrintTimeScale();
		}
	}

	// Token: 0x06000371 RID: 881 RVA: 0x0001021C File Offset: 0x0000E41C
	private SwipeDir AnalyzeTilt()
	{
		if (this.ignorTimeStampX <= Time.time)
		{
			float num = this.oldX - (float)this.accelData.x;
			if (Mathf.Abs(num) > 0.1f)
			{
				if (num < 0f)
				{
					if ((float)this.accelData.x >= 0f)
					{
						this.characterState.HandleSwipe(SwipeDir.Right);
					}
				}
				else if ((float)this.accelData.x <= 0f)
				{
					this.characterState.HandleSwipe(SwipeDir.Left);
				}
				this.ignorTimeStampX = Time.time + 0.2f + 0f * Mathf.Abs(num);
			}
		}
		this.oldX = (float)this.accelData.x;
		this.oldY = (float)this.accelData.y;
		this.sumAccelDataX = (float)this.accelData.x;
		this.sumAccelDataY = (float)this.accelData.y;
		if (this.accelData.y < (double)this.Medium - 0.1)
		{
			Debug.Log("Moving Up: " + this.accelData.y.ToString());
			this.characterState.HandleSwipe(SwipeDir.Up);
			return SwipeDir.Up;
		}
		if (this.accelData.y > (double)this.Medium + 0.1)
		{
			Debug.Log("Moving Down: " + this.accelData.y.ToString());
			this.characterState.HandleSwipe(SwipeDir.Down);
			return SwipeDir.Down;
		}
		return SwipeDir.None;
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0001039F File Offset: 0x0000E59F
	private void FixedUpdate()
	{
		this.yValues[this.yValueCounter] = (float)this.accelData.y;
		this.yValueCounter++;
		if (this.yValueCounter >= 200)
		{
			this.yValueCounter = 0;
		}
	}

	// Token: 0x06000373 RID: 883 RVA: 0x000103DC File Offset: 0x0000E5DC
	private void FillYValues()
	{
		if (!this.filled)
		{
			this.filled = true;
			for (int i = 0; i < 200; i++)
			{
				this.yValues[i] = (float)this.accelData.y;
			}
			this.filled = true;
		}
	}

	// Token: 0x06000374 RID: 884 RVA: 0x00010424 File Offset: 0x0000E624
	private void PrintTimeScale()
	{
		Debug.Log("Time scale = " + Time.timeScale.ToString());
	}

	// Token: 0x06000375 RID: 885 RVA: 0x00010450 File Offset: 0x0000E650
	private SwipeDir AnalyzeSwipe(Swipe swipe)
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(new Vector3(swipe.start.x, swipe.start.y, 2f));
		if (Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(swipe.end.x, swipe.end.y, 2f)), vector) < this.swipe.distanceMin)
		{
			return SwipeDir.None;
		}
		Vector3 vector2 = swipe.end - swipe.start;
		SwipeDir swipeDir = SwipeDir.None;
		float num = 0f;
		float num2 = Vector3.Dot(vector2, Vector3.up);
		if (num2 > num)
		{
			num = num2;
			swipeDir = SwipeDir.Up;
		}
		num2 = Vector3.Dot(vector2, Vector3.down);
		if (num2 > num)
		{
			num = num2;
			swipeDir = SwipeDir.Down;
		}
		num2 = Vector3.Dot(vector2, Vector3.left);
		if (num2 > num)
		{
			num = num2;
			swipeDir = SwipeDir.Left;
		}
		num2 = Vector3.Dot(vector2, Vector3.right);
		if (num2 > num)
		{
			swipeDir = SwipeDir.Right;
		}
		return swipeDir;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00010530 File Offset: 0x0000E730
	private IEnumerator BackToCheckPointSequence()
	{
		this.goingBackToCheckpoint = true;
		this.ChangeState(null);
		yield return new WaitForSeconds(this.backToCheckpointDelayTime);
		this.character.SetBackToCheckPoint(this.backToCheckpointZoomTime);
		yield return new WaitForSeconds(this.backToCheckpointZoomTime);
		this.goingBackToCheckpoint = false;
		yield break;
	}

	// Token: 0x04000297 RID: 663
	private const float tiltCooldown = 0.3f;

	// Token: 0x04000298 RID: 664
	private const int lengthOYValues = 200;

	// Token: 0x04000299 RID: 665
	[HideInInspector]
	public bool isDead;

	// Token: 0x0400029A RID: 666
	public bool ingameTouchDetection = true;

	// Token: 0x0400029B RID: 667
	private AccelData accelData;

	// Token: 0x0400029C RID: 668
	private bool acc;

	// Token: 0x0400029D RID: 669
	private float accelReadingX;

	// Token: 0x0400029E RID: 670
	private float currentTiltCooldown;

	// Token: 0x0400029F RID: 671
	private bool tiltCooldownActive;

	// Token: 0x040002A0 RID: 672
	[HideInInspector]
	public float currentSpeed;

	// Token: 0x040002A1 RID: 673
	public float currentLevelSpeed = 30f;

	// Token: 0x040002A2 RID: 674
	public float distancePerMeter = 8f;

	// Token: 0x040002A3 RID: 675
	public Game.SwipeInfo swipe;

	// Token: 0x040002A4 RID: 676
	public Game.SpeedInfo speed;

	// Token: 0x040002A5 RID: 677
	public float backToCheckpointDelayTime = 0.7f;

	// Token: 0x040002A6 RID: 678
	public float backToCheckpointZoomTime = 1f;

	// Token: 0x040002A7 RID: 679
	private bool goingBackToCheckpoint;

	// Token: 0x040002A8 RID: 680
	public Transform introAnimation;

	// Token: 0x040002A9 RID: 681
	private IEnumerator currentThread;

	// Token: 0x040002AA RID: 682
	private CharacterState characterState;

	// Token: 0x040002AB RID: 683
	[HideInInspector]
	public CharacterModifierCollection modifiers;

	// Token: 0x040002AC RID: 684
	private Swipe currentSwipe;

	// Token: 0x040002AD RID: 685
	private float lastTapTime = float.MinValue;

	// Token: 0x040002AE RID: 686
	public static bool HasLoaded;

	// Token: 0x040002AF RID: 687
	private static CharacterController characterController;

	// Token: 0x040002B0 RID: 688
	public Character character;

	// Token: 0x040002B1 RID: 689
	public Animation characterAnimation;

	// Token: 0x040002B2 RID: 690
	public Animation guardAnimation;

	// Token: 0x040002B3 RID: 691
	public Track track;

	// Token: 0x040002B4 RID: 692
	private CharacterCamera characterCamera;

	// Token: 0x040002B5 RID: 693
	private Transform characterCameraTransform;

	// Token: 0x040002B6 RID: 694
	private Distort distort;

	// Token: 0x040002B7 RID: 695
	private FollowingGuard enemies;

	// Token: 0x040002B8 RID: 696
	public Running running;

	// Token: 0x040002B9 RID: 697
	private Jetpack jetpack;

	// Token: 0x040002BA RID: 698
	private static Game instance;

	// Token: 0x040002BB RID: 699
	private float startTime;

	// Token: 0x040002BC RID: 700
	private float currentRunTime;

	// Token: 0x040002BD RID: 701
	private PlayerInfo player;

	// Token: 0x040002BE RID: 702
	private GameStats stats;

	// Token: 0x040002BF RID: 703
	public Action OnGameStarted;

	// Token: 0x040002C0 RID: 704
	public Action OnGameEnded;

	// Token: 0x040002C1 RID: 705
	public Game.OnGameOverDelegate OnGameOver;

	// Token: 0x040002C2 RID: 706
	public Game.OnPauseChangeDelegate OnPauseChange;

	// Token: 0x040002C3 RID: 707
	public Game.OnTopMenuDelegate OnTopMenu;

	// Token: 0x040002C4 RID: 708
	public Variable<bool> isInGame;

	// Token: 0x040002C5 RID: 709
	private TestStats _testStats;

	// Token: 0x040002C6 RID: 710
	public AudioStateLoop audioStateLoop;

	// Token: 0x040002C7 RID: 711
	public AudioClipInfo DieSound;

	// Token: 0x040002C8 RID: 712
	public bool awakeDone;

	// Token: 0x040002C9 RID: 713
	public bool isReadyForHeadStart;

	// Token: 0x040002CA RID: 714
	private bool _paused;

	// Token: 0x040002CB RID: 715
	private float sumAccelDataX;

	// Token: 0x040002CC RID: 716
	private float sumAccelDataY;

	// Token: 0x040002CD RID: 717
	private float oldSumAccelDataX;

	// Token: 0x040002CE RID: 718
	private float ignorTimeStampX;

	// Token: 0x040002CF RID: 719
	private float oldX;

	// Token: 0x040002D0 RID: 720
	private float oldY;

	// Token: 0x040002D1 RID: 721
	private float oldSumAccelDataY;

	// Token: 0x040002D2 RID: 722
	private float ignorTimeStampY;

	// Token: 0x040002D3 RID: 723
	private float timeStamp;

	// Token: 0x040002D4 RID: 724
	private bool filled;

	// Token: 0x040002D5 RID: 725
	private float oneOverLengthOYValues = 0.005f;

	// Token: 0x040002D6 RID: 726
	public float[] yValues = new float[200];

	// Token: 0x040002D7 RID: 727
	private int yValueCounter;

	// Token: 0x0200019A RID: 410
	[Serializable]
	public class SwipeInfo
	{
		// Token: 0x040009AB RID: 2475
		public float distanceMin = 0.1f;

		// Token: 0x040009AC RID: 2476
		public float doubleTapDuration = 0.3f;
	}

	// Token: 0x0200019B RID: 411
	[Serializable]
	public class SpeedInfo
	{
		// Token: 0x040009AD RID: 2477
		public float min = 30f;

		// Token: 0x040009AE RID: 2478
		public float max = 70f;

		// Token: 0x040009AF RID: 2479
		public float rampUpDuration = 200f;
	}

	// Token: 0x0200019C RID: 412
	// (Invoke) Token: 0x06000ADB RID: 2779
	public delegate void OnGameOverDelegate(GameStats gameStats);

	// Token: 0x0200019D RID: 413
	// (Invoke) Token: 0x06000ADF RID: 2783
	public delegate void OnPauseChangeDelegate(bool pause);

	// Token: 0x0200019E RID: 414
	// (Invoke) Token: 0x06000AE3 RID: 2787
	public delegate void OnTopMenuDelegate();
}
