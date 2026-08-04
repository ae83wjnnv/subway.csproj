using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class Character : MonoBehaviour
{
	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060001B0 RID: 432 RVA: 0x000069F1 File Offset: 0x00004BF1
	// (set) Token: 0x060001B1 RID: 433 RVA: 0x000069F9 File Offset: 0x00004BF9
	public bool Stumble
	{
		get
		{
			return this.stumble;
		}
		set
		{
			if (value)
			{
				this.StartStumble();
			}
			else
			{
				this.StopStumble();
			}
			this.stumble = value;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x060001B2 RID: 434 RVA: 0x00006A13 File Offset: 0x00004C13
	public static Character Instance
	{
		get
		{
			Character character;
			if ((character = Character.instance) == null)
			{
				character = (Character.instance = Object.FindObjectOfType(typeof(Character)) as Character);
			}
			return character;
		}
	}

	// Token: 0x1400001A RID: 26
	// (add) Token: 0x060001B3 RID: 435 RVA: 0x00006A38 File Offset: 0x00004C38
	// (remove) Token: 0x060001B4 RID: 436 RVA: 0x00006A70 File Offset: 0x00004C70
	public event Character.OnGroundedDelegate OnGrounded;

	// Token: 0x060001B5 RID: 437 RVA: 0x00006AA8 File Offset: 0x00004CA8
	public void Awake()
	{
		this.game = Game.Instance;
		Variable<bool> isInGame2 = this.game.isInGame;
		isInGame2.OnChange = (Variable<bool>.OnChangeDelegate)Delegate.Combine(isInGame2.OnChange, new Variable<bool>.OnChangeDelegate(delegate(bool isInGame)
		{
			if (!isInGame)
			{
				base.StopAllCoroutines();
				this.immuneToCriticalHit = false;
				this.characterController.enabled = true;
				this.stopColliding = false;
			}
		}));
		this.track = Track.Instance;
		this.characterController = Game.Charactercontroller;
		this.hoverboard = Hoverboard.Instance;
		this.running = Running.Instance;
		this.superSneakers = this.FindObject<SuperSneakers>();
		this.characterModel = base.GetComponentInChildren<CharacterModel>();
		this.characterCamera = CharacterCamera.Instance;
		this.guard = FollowingGuard.Instance;
		this.CharacterPickupParticleSystem = base.GetComponentInChildren<CharacterPickupParticles>();
		this.characterColliderTrigger = this.characterCollider.GetComponent<OnTriggerObject>();
		OnTriggerObject onTriggerObject = this.characterColliderTrigger;
		onTriggerObject.OnEnter = (OnTriggerObject.OnEnterDelegate)Delegate.Combine(onTriggerObject.OnEnter, new OnTriggerObject.OnEnterDelegate(this.OnCharacterColliderEnter));
		OnTriggerObject onTriggerObject2 = this.characterColliderTrigger;
		onTriggerObject2.OnExit = (OnTriggerObject.OnExitDelegate)Delegate.Combine(onTriggerObject2.OnExit, new OnTriggerObject.OnExitDelegate(this.OnCharacterColliderExit));
		this.characterAnimation["caught"].layer = 4;
		this.characterAnimation["caught"].enabled = false;
		this.characterAnimation["caught2"].layer = 4;
		this.characterAnimation["caught2"].enabled = false;
		this.characterControllerCenter = this.characterController.center;
		this.characterControllerHeight = this.characterController.height;
		this.characterColliderCenter = this.characterCollider.center;
		this.characterColliderHeight = this.characterCollider.height;
		this.stats = GameStats.Instance;
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00006C58 File Offset: 0x00004E58
	public void Restart()
	{
		this.trackIndex = this.initialTrackIndex;
		this.trackIndexTarget = this.initialTrackIndex;
		this.x = this.track.GetTrackX(this.trackIndex);
		this.trackIndexPosition = (float)this.trackIndex;
		this.characterModel.ResetBlink();
		this.z = 0f;
		this.trackMovement = 0;
		this.trackMovementNext = 0;
		this.characterController.transform.position = this.track.GetPosition(this.x, this.z) + Vector3.up * 5f;
		this.characterController.Move(-5f * Vector3.up);
		this.verticalSpeed = 0f;
		this.jumpHeight = this.jumpHeightNormal;
		this.inAirJump = false;
		this.lastGroundedY = 0f;
		this.guard.Restart(false);
		this.Stumble = false;
		this.startedJumpFromGround = false;
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x00006D5E File Offset: 0x00004F5E
	public void ChangeTrack(int movement, float duration)
	{
		this.stats.trackChanges++;
		if (this.trackMovement != movement)
		{
			this.ForceChangeTrack(movement, duration);
			return;
		}
		this.trackMovementNext = movement;
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00006D8C File Offset: 0x00004F8C
	public void ForceChangeTrack(int movement, float duration)
	{
		this.trackMovement = movement;
		this.trackMovementNext = 0;
		base.StopAllCoroutines();
		base.StartCoroutine(this.ChangeTrackCoroutine(movement, duration));
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x00006DB1 File Offset: 0x00004FB1
	private IEnumerator ChangeTrackCoroutine(int move, float duration)
	{
		this.trackMovement = move;
		this.trackMovementNext = 0;
		int newTrackIndex = this.trackIndexTarget + move;
		float num = Mathf.Abs((float)newTrackIndex - this.trackIndexPosition);
		float trackIndexPositionBegin = this.trackIndexPosition;
		float startX = this.x;
		float endX = this.track.GetTrackX(newTrackIndex);
		float dir = Mathf.Sign((float)(newTrackIndex - this.trackIndexTarget));
		float startRotation = this.characterRotation;
		if (this.characterController.isGrounded)
		{
			string text = ((dir >= 0f) ? this.animations.dodgeRight : this.animations.dodgeLeft);
			this.characterAnimation["dodgeRight"].speed = Game.Instance.NormalizedGameSpeed;
			this.characterAnimation["dodgeLeft"].speed = Game.Instance.NormalizedGameSpeed;
			this.characterAnimation.CrossFade(text, 0.02f);
		}
		this.characterAnimation.CrossFadeQueued(this.animations.run, (!this.game.Modifiers.IsActive(this.game.Modifiers.Hoverboard)) ? 0.02f : 0.4f);
		if (newTrackIndex < 0 || newTrackIndex >= this.track.numberOfTracks)
		{
			this.NotifyStumble(this.StumbleSideSound, "side");
			if (!this.game.Modifiers.IsActive(this.game.Modifiers.Hoverboard) && !this.game.IsInJetpackMode)
			{
				this.characterAnimation.CrossFade((dir >= 0f) ? "stumbleOffRight" : "stumbleOffLeft", 0.2f);
			}
			this.characterAnimation.CrossFadeQueued(this.animations.run, (!this.game.Modifiers.IsActive(this.game.Modifiers.Hoverboard)) ? 0.02f : 0.4f);
			yield break;
		}
		if (move < 0)
		{
			if (this.game.Modifiers.IsActive(this.game.Modifiers.Hoverboard))
			{
				So.Instance.playSound(this.H_Left);
			}
			else
			{
				So.Instance.playSound(this.DodgeLeft);
			}
		}
		else if (this.game.Modifiers.IsActive(this.game.Modifiers.Hoverboard))
		{
			So.Instance.playSound(this.H_Right);
		}
		else
		{
			So.Instance.playSound(this.DodgeRight);
		}
		this.trackIndexTarget = newTrackIndex;
		yield return base.StartCoroutine(pTween.To(num * duration, delegate(float t)
		{
			this.trackIndexPosition = Mathf.Lerp(trackIndexPositionBegin, (float)newTrackIndex, t);
			this.x = Mathf.Lerp(startX, endX, t);
			this.characterRotation = pMath.Bell(t) * dir * this.characterAngle + Mathf.Lerp(startRotation, 0f, t);
			this.characterRoot.localRotation = Quaternion.Euler(0f, this.characterRotation, 0f);
		}));
		this.trackIndex = newTrackIndex;
		this.trackMovement = 0;
		if (this.trackMovementNext != 0)
		{
			base.StartCoroutine(this.ChangeTrackCoroutine(this.trackMovementNext, duration));
		}
		yield break;
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00006DD0 File Offset: 0x00004FD0
	public void SetBackToCheckPoint(float zoomTime)
	{
		float lastCheckPoint = this.track.GetLastCheckPoint(this.z);
		this.trackIndex = this.initialTrackIndex;
		this.trackIndexTarget = this.initialTrackIndex;
		float trackX = this.track.GetTrackX(this.trackIndex);
		this.trackIndexPosition = (float)this.trackIndex;
		this.trackMovement = 0;
		this.trackMovementNext = 0;
		base.StartCoroutine(this.MoveCharacterToPosition(trackX, lastCheckPoint, zoomTime));
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00006E44 File Offset: 0x00005044
	private IEnumerator MoveCharacterToPosition(float newX, float newZ, float time)
	{
		float oldX = this.x;
		float oldZ = this.z;
		this.game.ChangeState(null);
		this.immuneToCriticalHit = true;
		this.stopColliding = true;
		this.characterController.enabled = false;
		this.characterAnimation.CrossFade(this.animations.run, time);
		float newX2 = newX;
		float newZ2 = newZ;
		yield return base.StartCoroutine(pTween.To(time, delegate(float t)
		{
			this.x = Mathf.SmoothStep(oldX, newX2, t);
			this.z = Mathf.SmoothStep(oldZ, newZ2, t);
		}));
		this.immuneToCriticalHit = false;
		this.characterController.enabled = true;
		this.characterAnimation.Play(this.animations.run);
		this.stopColliding = false;
		this.game.ChangeState(this.game.Running);
		yield break;
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00006E68 File Offset: 0x00005068
	private Character.ObstacleTypes ObstacleTagToType(string tag)
	{
		if (tag != null)
		{
			if (tag == "JumpTrain")
			{
				return Character.ObstacleTypes.jumpTrain;
			}
			if (tag == "RollBarrier")
			{
				return Character.ObstacleTypes.rollBarrier;
			}
			if (tag == "JumpBarrier")
			{
				return Character.ObstacleTypes.jumpBarrier;
			}
		}
		return Character.ObstacleTypes.none;
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00006EA0 File Offset: 0x000050A0
	private void OnCharacterColliderExit(Collider collider)
	{
		Character.ObstacleTypes obstacleTypes = this.ObstacleTagToType(collider.tag);
		if (obstacleTypes == this.lastObstacleTriggerType && this.lastObstacleTriggerTrackInex == this.trackIndex)
		{
			switch (obstacleTypes)
			{
			case Character.ObstacleTypes.jumpTrain:
			{
				GameStats gameStats = this.stats;
				int num = gameStats.jumpsOverTrains;
				gameStats.jumpsOverTrains = num + 1;
				return;
			}
			case Character.ObstacleTypes.rollBarrier:
			{
				GameStats gameStats2 = this.stats;
				int num = gameStats2.dodgeBarrier;
				gameStats2.dodgeBarrier = num + 1;
				break;
			}
			case Character.ObstacleTypes.jumpBarrier:
			{
				GameStats gameStats3 = this.stats;
				int num = gameStats3.jumpBarrier;
				gameStats3.jumpBarrier = num + 1;
				return;
			}
			default:
				return;
			}
		}
	}

	// Token: 0x060001BE RID: 446 RVA: 0x00006F28 File Offset: 0x00005128
	private void OnCharacterColliderEnter(Collider collider)
	{
		if (collider.name == "Character")
		{
			return;
		}
		if (this.stopColliding || collider.gameObject.layer == 16)
		{
			return;
		}
		Pickup componentInChildren = collider.GetComponentInChildren<Pickup>();
		if (componentInChildren != null)
		{
			this.NotifyPickup(componentInChildren);
			return;
		}
		if (collider.gameObject.layer == 0)
		{
			if (collider.isTrigger && this.characterController.isGrounded && this.OnGrounded != null)
			{
				this.OnGrounded();
			}
			if (collider.isTrigger)
			{
				Character.ObstacleTypes obstacleTypes = this.ObstacleTagToType(collider.tag);
				if (obstacleTypes != Character.ObstacleTypes.none)
				{
					this.lastObstacleTriggerType = obstacleTypes;
					this.lastObstacleTriggerTrackInex = this.trackIndex;
				}
			}
			return;
		}
		if (collider.isTrigger)
		{
			this.characterAnimation.CrossFade(this.animations.stumble, 0.05f);
			this.characterAnimation.CrossFadeQueued(this.animations.run, 0.5f);
			if (collider.name == "bush")
			{
				this.NotifyStumble(this.StumbleBushSound, collider.name);
				return;
			}
			this.NotifyStumble(this.StumbleSound, collider.name);
			return;
		}
		else
		{
			this.lastHitTag = collider.tag;
			Character.ImpactX impactX = this.GetImpactX(collider);
			Character.ImpactY impactY = this.GetImpactY(collider);
			Character.ImpactZ impactZ = this.GetImpactZ(collider);
			int num = (((collider.bounds.min.x + collider.bounds.max.x) / 2f > base.transform.position.x) ? 1 : (-1));
			bool flag = this.trackMovement == num;
			bool flag2 = this.characterCollider.bounds.center.z < collider.bounds.min.z;
			bool flag3 = impactZ == Character.ImpactZ.Before && !flag2 && flag;
			if (impactZ == Character.ImpactZ.Middle || flag3)
			{
				if (this.trackMovement != 0)
				{
					float num2 = 0.5f;
					if (this.track.IsRunningOnTutorialTrack)
					{
						num2 = 0.2f;
					}
					this.ChangeTrack(-this.trackMovement, num2);
				}
				if (impactX == Character.ImpactX.Left)
				{
					this.characterAnimation.Play(this.animations.stumbleLeftSide);
					this.characterAnimation.PlayQueued(this.animations.run);
					this.NotifyStumble(this.StumbleSound, collider.name);
					return;
				}
				if (impactX != Character.ImpactX.Right)
				{
					return;
				}
				this.characterAnimation.Play(this.animations.stumbleRightSide);
				this.characterAnimation.PlayQueued(this.animations.run);
				this.NotifyStumble(this.StumbleSound, collider.name);
				return;
			}
			else
			{
				if (impactX == Character.ImpactX.Middle)
				{
					bool flag4 = true;
					if (!this.immuneToCriticalHit)
					{
						if (impactY == Character.ImpactY.Lower)
						{
							this.characterAnimation.CrossFade(this.animations.stumble, 0.05f);
							this.characterAnimation.CrossFadeQueued(this.animations.run, 0.5f);
							flag4 = false;
							this.verticalSpeed = this.CalculateJumpVerticalSpeed(8f);
							this.NotifyStumble(this.StumbleSound, collider.name);
						}
						else if (collider.gameObject.CompareTag("HitMovingTrain"))
						{
							this.HitByTrainSequence();
						}
						else if (impactY == Character.ImpactY.Middle)
						{
							this.characterAnimation.CrossFade(this.animations.hitMid, 0.07f);
						}
						else
						{
							this.characterAnimation.CrossFade(this.animations.hitUpper, 0.07f);
						}
					}
					if (flag4)
					{
						this.NotifyCriticalHit();
					}
					return;
				}
				if (impactZ == Character.ImpactZ.Before && flag)
				{
					if (collider.gameObject.CompareTag("HitMovingTrain"))
					{
						this.HitByTrainSequence();
						this.NotifyCriticalHit();
					}
					else if (collider.gameObject.layer == 13)
					{
						this.characterAnimation.CrossFade(this.animations.stumble, 0.05f);
						this.characterAnimation.CrossFadeQueued(this.animations.run, 0.5f);
					}
					else
					{
						this.ForceChangeTrack(-this.trackMovement, 0.5f);
					}
				}
				else if (collider.gameObject.layer == 13)
				{
					this.ForceChangeTrack(-this.trackMovement, 0.5f);
				}
				if (impactX != Character.ImpactX.Left)
				{
					if (impactX == Character.ImpactX.Right)
					{
						this.characterAnimation.Play(this.animations.stumbleRightCorner);
						this.characterAnimation.PlayQueued(this.animations.run);
					}
				}
				else
				{
					this.characterAnimation.Play(this.animations.stumbleLeftCorner);
					this.characterAnimation.PlayQueued(this.animations.run);
				}
				this.NotifyStumble(this.StumbleSound, collider.name);
				return;
			}
		}
	}

	// Token: 0x060001BF RID: 447 RVA: 0x000073CC File Offset: 0x000055CC
	private void HitByTrainSequence()
	{
		if (!this.hoverboard.isActive)
		{
			this.characterAnimation.Play(this.animations.hitMoving);
			Vector3 currentPos = base.transform.position;
			Vector3 camPos = this.characterCamera.transform.position;
			base.StartCoroutine(pTween.To(0.5f, delegate(float t)
			{
				this.transform.position = Vector3.Lerp(currentPos, new Vector3(camPos.x, camPos.y - 33f, currentPos.z), t);
			}));
		}
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00007450 File Offset: 0x00005650
	private Character.ImpactX GetImpactX(Collider collider)
	{
		Bounds bounds = this.characterCollider.bounds;
		Bounds bounds2 = collider.bounds;
		float num = Mathf.Max(bounds.min.x, bounds2.min.x);
		float num2 = Mathf.Min(bounds.max.x, bounds2.max.x);
		float num3 = (num + num2) * 0.5f;
		float num4 = (num3 - bounds2.min.x) / bounds2.size.x;
		float num5 = num3 - bounds2.min.x;
		if ((double)num5 > (double)bounds2.size.x - (double)this.ColliderTrackWidth * 0.33)
		{
			return Character.ImpactX.Right;
		}
		if ((double)num5 < (double)this.ColliderTrackWidth * 0.33)
		{
			return Character.ImpactX.Left;
		}
		return Character.ImpactX.Middle;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000751C File Offset: 0x0000571C
	private Character.ImpactZ GetImpactZ(Collider collider)
	{
		Vector3 position = base.transform.position;
		Bounds bounds = collider.bounds;
		if (position.z > bounds.max.z - ((bounds.max.z - bounds.min.z <= 30f) ? ((bounds.max.z - bounds.min.z) * 0.5f) : this.stumbleCornerTolerance))
		{
			return Character.ImpactZ.After;
		}
		if (position.z < bounds.min.z + this.stumbleCornerTolerance)
		{
			return Character.ImpactZ.Before;
		}
		return Character.ImpactZ.Middle;
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x000075B8 File Offset: 0x000057B8
	private Character.ImpactY GetImpactY(Collider collider)
	{
		Bounds bounds = this.characterCollider.bounds;
		Bounds bounds2 = collider.bounds;
		float num = Mathf.Max(bounds.min.y, bounds2.min.y);
		float num2 = Mathf.Min(bounds.max.y, bounds2.max.y);
		float num3 = ((num + num2) * 0.5f - bounds.min.y) / bounds.size.y;
		if (num3 < 0.33f)
		{
			return Character.ImpactY.Lower;
		}
		if (num3 < 0.66f)
		{
			return Character.ImpactY.Middle;
		}
		return Character.ImpactY.Upper;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000764C File Offset: 0x0000584C
	public void Update()
	{
		if (this.roll != null)
		{
			this.roll.MoveNext();
		}
		Vector3 position = base.transform.position;
		if (position.y < 0f)
		{
			position.y = 1f;
			base.transform.position = position;
			Debug.Log("Character y-position has been clamped to avoid fallthrough.");
		}
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x000076A8 File Offset: 0x000058A8
	public float GetTrackX()
	{
		return this.track.GetPosition(this.track.GetTrackX(this.trackIndex), 0f).x;
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x000076D0 File Offset: 0x000058D0
	public void Jump()
	{
		this.fallAnim = true;
		if (this.hoverboard.isActive)
		{
			this.animations.SetRandomHoverJump();
		}
		else
		{
			this.animations.SetRandomJump();
		}
		bool flag = this.falling && !this.jumping && this.verticalSpeed < 0f && this.verticalSpeed > this.verticalSpeed_jumpTolerance;
		if (this.characterController.isGrounded || flag)
		{
			this.jumping = true;
			this.falling = false;
			this.shadow.active = false;
			this.verticalSpeed = this.CalculateJumpVerticalSpeed(this.jumpHeight);
			this.characterAnimation.CrossFade(this.animations.jump, 0.05f);
			if (this.IsRunningOnGround())
			{
				this.startedJumpFromGround = true;
				this.trainJump = false;
				this.trainJumpSampleZ = this.z + this.trainJumpSampleLength;
			}
			if (this.OnJump != null)
			{
				this.OnJump();
			}
			GameStats gameStats = this.stats;
			int jumps = gameStats.jumps;
			gameStats.jumps = jumps + 1;
			return;
		}
		if (this.verticalSpeed < 0f)
		{
			this.inAirJump = true;
		}
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x000077F7 File Offset: 0x000059F7
	private bool IsRunningOnGround()
	{
		return this.running.currentRunPosition == Running.RunPositions.ground;
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00007807 File Offset: 0x00005A07
	public void CheckInAirJump()
	{
		if (this.characterController.isGrounded && this.inAirJump)
		{
			this.Jump();
			this.inAirJump = false;
		}
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000782C File Offset: 0x00005A2C
	public void Roll()
	{
		if (this.roll == null)
		{
			this.roll = this.BeginRoll();
			GameStats gameStats = this.stats;
			int num = gameStats.rolls;
			gameStats.rolls = num + 1;
			if (this.trackIndex == 0)
			{
				GameStats gameStats2 = this.stats;
				num = gameStats2.rollsLeftTrack;
				gameStats2.rollsLeftTrack = num + 1;
			}
			if (this.trackIndex == 1)
			{
				GameStats gameStats3 = this.stats;
				num = gameStats3.rollsCenterTrack;
				gameStats3.rollsCenterTrack = num + 1;
			}
			if (this.trackIndex == 2)
			{
				GameStats gameStats4 = this.stats;
				num = gameStats4.rollsRightTrack;
				gameStats4.rollsRightTrack = num + 1;
			}
		}
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x000078BC File Offset: 0x00005ABC
	public void ApplyGravity()
	{
		if (this.verticalSpeed < 0f && this.characterController.isGrounded)
		{
			if (this.startedJumpFromGround && this.trainJump && this.IsRunningOnGround())
			{
				GameStats gameStats = this.stats;
				int jumpsOverTrains = gameStats.jumpsOverTrains;
				gameStats.jumpsOverTrains = jumpsOverTrains + 1;
			}
			if (this.running.currentRunPosition != Running.RunPositions.air)
			{
				this.startedJumpFromGround = false;
			}
			this.verticalSpeed = 0f;
			if (this.jumping || this.falling)
			{
				this.jumping = false;
				this.falling = false;
				if (this.OnGrounded != null)
				{
					this.OnGrounded();
				}
				if (this.roll == null)
				{
					this.shadow.active = true;
					this.SetRunAnim();
					if (this.fallAnim)
					{
						this.characterAnimation.CrossFade(this.animations.landing, 0.05f);
						this.characterAnimation.CrossFadeQueued(this.animations.run, 0.1f);
					}
					else
					{
						this.fallAnim = true;
						this.characterAnimation.CrossFade(this.animations.run, 0.1f);
					}
				}
			}
		}
		else if (this.startedJumpFromGround && this.trainJumpSampleZ < this.z)
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(new Ray(base.transform.position, -Vector3.up), out raycastHit))
			{
				Debug.DrawRay(base.transform.position, -Vector3.up * raycastHit.distance, Color.red, 1000f);
				if (raycastHit.collider.CompareTag("HitMovingTrain") || raycastHit.collider.CompareTag("HitTrain"))
				{
					this.trainJump = true;
				}
			}
			this.trainJumpSampleZ += this.trainJumpSampleLength;
		}
		this.verticalSpeed -= this.gravity * Time.deltaTime;
		if (this.characterController.isGrounded)
		{
			return;
		}
		this.shadow.active = false;
		if (!this.falling && this.verticalSpeed < this.verticalFallSpeedLimit && this.roll == null)
		{
			this.falling = true;
			if (this.fallAnim)
			{
				this.characterAnimation.CrossFade(this.animations.hangtime, 0.2f);
			}
		}
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00007B1C File Offset: 0x00005D1C
	public void MoveWithGravity()
	{
		if (this.characterController.enabled)
		{
			this.verticalSpeed -= this.gravity * Time.deltaTime;
			if (this.verticalSpeed > 0f)
			{
				this.verticalSpeed = 0f;
			}
			Vector3 vector = this.verticalSpeed * Time.deltaTime * Vector3.up;
			this.characterController.Move(vector);
		}
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00007B8C File Offset: 0x00005D8C
	public void MoveForward()
	{
		Vector3 position = base.transform.position;
		float num = this.z + this.game.currentSpeed * Time.deltaTime;
		Vector3 vector = this.verticalSpeed * Time.deltaTime * Vector3.up;
		Vector3 position2 = this.track.GetPosition(this.x, num);
		Vector3 vector2 = new Vector3(position.x, 0f, position.z);
		Vector3 vector3 = position2 - vector2;
		if (this.characterController.enabled)
		{
			this.characterController.Move(vector + vector3);
		}
		else
		{
			this.characterController.transform.position = this.characterController.transform.position + vector3;
		}
		this.z = base.transform.position.z;
		if (this.characterController.isGrounded)
		{
			this.lastGroundedY = position.y;
		}
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00007C80 File Offset: 0x00005E80
	private IEnumerator BeginRoll()
	{
		this.characterAnimation.CrossFade(this.animations.roll, 0.1f);
		this.SetRunAnim();
		this.fallAnim = false;
		this.characterAnimation.CrossFadeQueued(this.animations.run, (!this.game.Modifiers.IsActive(this.game.Modifiers.Hoverboard)) ? 0f : 0.2f);
		this.characterController.height = 4f;
		this.characterController.center = new Vector3(0f, 2f, this.characterControllerCenter.z);
		this.characterCollider.height = 4f;
		this.characterCollider.center = new Vector3(0f, 4f, this.characterColliderCenter.z);
		this.verticalSpeed = 0f - this.CalculateJumpVerticalSpeed(this.jumpHeight);
		float endTime = Time.time + this.characterAnimation[this.animations.roll].length;
		while (Time.time < endTime)
		{
			yield return 0;
			if (!this.characterAnimation[this.animations.roll].enabled)
			{
				break;
			}
		}
		if (this.characterController.enabled)
		{
			this.characterController.Move(Vector3.up * 2f);
		}
		this.characterController.center = this.characterControllerCenter;
		this.characterController.height = this.characterControllerHeight;
		this.characterCollider.center = this.characterColliderCenter;
		this.characterCollider.height = this.characterColliderHeight;
		if (this.characterController.enabled)
		{
			this.characterController.Move(Vector3.down * 2f);
		}
		this.roll = null;
		yield break;
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00007C8F File Offset: 0x00005E8F
	public float CalculateJumpVerticalSpeed(float jumpHeight)
	{
		return Mathf.Sqrt(2f * jumpHeight * this.gravity);
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00007CA4 File Offset: 0x00005EA4
	public float CalculateJumpVerticalSpeed()
	{
		return this.CalculateJumpVerticalSpeed(this.jumpHeight);
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00007CB2 File Offset: 0x00005EB2
	public float JumpLength(float speed, float jumpHeight)
	{
		return speed * 2f * this.CalculateJumpVerticalSpeed(jumpHeight) / this.gravity;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x00007CCA File Offset: 0x00005ECA
	private void StartStumble()
	{
		this.guard.CatchUp();
		this.guard.StartCoroutine(this.StumbleDecay());
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00007CE9 File Offset: 0x00005EE9
	private void StopStumble()
	{
		this.guard.ResetCatchUp();
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x00007CF6 File Offset: 0x00005EF6
	private IEnumerator StumbleDecay()
	{
		yield return new WaitForSeconds(this.stumbleDecayTime);
		this.stumble = false;
		this.StopStumble();
		yield break;
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x00007D08 File Offset: 0x00005F08
	private void NotifyStumble(AudioClipInfo sound, string nameOfCollider)
	{
		if (this.game.IsInJetpackMode)
		{
			return;
		}
		So.Instance.playSound(sound);
		if (this.track.IsRunningOnTutorialTrack)
		{
			return;
		}
		if (this.OnStumble != null)
		{
			this.OnStumble();
			if (nameOfCollider != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(nameOfCollider);
				if (num <= 1884404243U)
				{
					if (num > 947118415U)
					{
						if (num != 1845197635U)
						{
							if (num != 1884404243U)
							{
								goto IL_01C1;
							}
							if (!(nameOfCollider == "powerbox"))
							{
								goto IL_01C1;
							}
						}
						else if (!(nameOfCollider == "bush"))
						{
							goto IL_01C1;
						}
						Missions.Instance.PlayerDidThis(Missions.MissionTarget.BumpBush, 1);
						goto IL_01CE;
					}
					if (num != 398089822U)
					{
						if (num == 947118415U)
						{
							if (nameOfCollider == "blocker_standard")
							{
								Missions.Instance.PlayerDidThis(Missions.MissionTarget.BumpBarrier, 1);
								goto IL_01CE;
							}
						}
					}
					else if (nameOfCollider == "side")
					{
						goto IL_01CE;
					}
				}
				else if (num <= 2324613926U)
				{
					if (num != 2305333821U)
					{
						if (num == 2324613926U)
						{
							if (nameOfCollider == "blocker_jump")
							{
								Missions.Instance.PlayerDidThis(Missions.MissionTarget.BumpBarrier, 1);
								goto IL_01CE;
							}
						}
					}
					else if (nameOfCollider == "collider")
					{
						goto IL_01CE;
					}
				}
				else if (num != 2597905607U)
				{
					if (num != 2880217895U)
					{
						if (num == 3366026289U)
						{
							if (nameOfCollider == "blocker_roll")
							{
								Missions.Instance.PlayerDidThis(Missions.MissionTarget.BumpBarrier, 1);
								goto IL_01CE;
							}
						}
					}
					else if (nameOfCollider == "collider stumble")
					{
						Missions.Instance.PlayerDidThis(Missions.MissionTarget.BumpTrain, 1);
						goto IL_01CE;
					}
				}
				else if (nameOfCollider == "lightSignal")
				{
					Missions.Instance.PlayerDidThis(Missions.MissionTarget.BumpLightSignal, 1);
					goto IL_01CE;
				}
			}
			IL_01C1:
			Missions.Instance.PlayerDidThis(Missions.MissionTarget.BumpTrain, 1);
		}
		IL_01CE:
		this.Stumble = false;
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00007EEC File Offset: 0x000060EC
	private void NotifyCriticalHit()
	{
		if (this.OnCriticalHit != null)
		{
			this.OnCriticalHit();
			string text = this.lastHitTag;
			if (text != null)
			{
				int num;
				if (text == "HitTrain")
				{
					GameStats gameStats = this.stats;
					num = gameStats.trainHit;
					gameStats.trainHit = num + 1;
					return;
				}
				if (text == "HitBarrier")
				{
					GameStats gameStats2 = this.stats;
					num = gameStats2.barrierHit;
					gameStats2.barrierHit = num + 1;
					return;
				}
				if (!(text == "HitMovingTrain"))
				{
					return;
				}
				GameStats gameStats3 = this.stats;
				num = gameStats3.movingTrainHit;
				gameStats3.movingTrainHit = num + 1;
			}
		}
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00007F7F File Offset: 0x0000617F
	public void NotifyPickup(Pickup pickup)
	{
		pickup.NotifyPickup(this.CharacterPickupParticleSystem);
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00007F90 File Offset: 0x00006190
	public void ChangeAnimations()
	{
		if (this.game.isDead)
		{
			return;
		}
		if (this.hoverboard.isActive)
		{
			this.animations.run = "h_run";
			this.animations.roll = "h_roll";
			this.animations.dodgeLeft = "h_left";
			this.animations.dodgeRight = "h_right";
		}
		else
		{
			if (this.superSneakers.isActive)
			{
				this.animations.run = "superRun";
				this.animations.landing = "landing";
			}
			else
			{
				this.animations.SetRandomRun();
			}
			this.animations.roll = "roll";
			this.animations.dodgeLeft = "dodgeLeft";
			this.animations.dodgeRight = "dodgeRight";
			this.animations.SetRandomJump();
		}
		if (this.characterController.isGrounded)
		{
			this.characterAnimation.CrossFade(this.animations.run);
		}
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x00008094 File Offset: 0x00006294
	public void SetAnimations()
	{
		this.animations.run = "run";
		this.animations.runAnimations = new string[] { "run", "run2", "run3", "run4_long" };
		this.animations.landAnimations = new string[] { "landing", "landing", "landing", "landing3" };
		this.animations.jumpAnimations = new string[] { "jump", "jump", "jump_salto", "jump2", "jump3" };
		this.animations.hangtimeAnimations = new string[] { "hangtime", "hangtime", "hangtime2", "hangtime3" };
		this.animations.grindAnimations = new string[] { "h_Grind1", "h_Grind2", "h_Grind3" };
		this.animations.grindLandAnimations = new string[] { "landing_grind1", "landing_grind2", "landing_grind3" };
		this.animations.hoverAnimations = new string[] { "h_run" };
		this.animations.hoverLandAnimations = new string[] { "h_landing" };
		this.animations.hoverJumpAnimations = new string[]
		{
			"h_jump", "h_jump2_kickflip", "h_jump3_180", "h_jump4_360flip", "h_jump5_Impossible", "h_jump6_nollie", "h_jump7_heelflip", "h_jump8_pop shuvit", "h_jump9_fs360", "h_jump10_heel360",
			"h_jump11_fs salto"
		};
		this.animations.hoverHangtimeAnimations = new string[]
		{
			"h_hangtime", "h_jump2_kickflip", "h_jump3_180", "h_jump4_360flip", "h_jump5_Impossible", "h_jump6_nollie", "h_jump7_heelflip", "h_jump8_pop shuvit", "h_jump9_fs360", "h_jump10_heel360",
			"h_jump11_fs salto"
		};
		this.animations.run = "run";
		this.animations.jump = "jump";
		this.animations.hangtime = "hangtime";
		this.animations.landing = "landing";
		this.animations.roll = "roll";
		this.animations.dodgeLeft = "dodgeLeft";
		this.animations.dodgeRight = "dodgeRight";
		this.animations.hitMid = "death_bounce";
		this.animations.hitUpper = "death_upper";
		this.animations.hitLower = "death_lower";
		this.animations.hitMoving = "death_movingTrain";
		this.animations.stumble = "stumble_low";
		this.animations.stumbleDeath = "caught";
		this.animations.stumbleLeftSide = "stumbleSideLeft";
		this.animations.stumbleRightSide = "stumbleSideRight";
		this.animations.stumbleLeftCorner = "stumbleCornerLeft";
		this.animations.stumbleRightCorner = "stumbleCornerRight";
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x000083EC File Offset: 0x000065EC
	private void SetRunAnim()
	{
		if (!this.hoverboard.isActive)
		{
			if (!this.superSneakers.isActive)
			{
				this.animations.SetRandomRun();
			}
			return;
		}
		if (base.transform.position.y > 20f)
		{
			this.animations.SetRandomGrind();
			return;
		}
		this.animations.SetRandomHover();
	}

	// Token: 0x040000E6 RID: 230
	public int initialTrackIndex = 1;

	// Token: 0x040000E7 RID: 231
	public CapsuleCollider characterCollider;

	// Token: 0x040000E8 RID: 232
	public OnTriggerObject coinMagnetCollider;

	// Token: 0x040000E9 RID: 233
	public Character.OnStumbleDelegate OnStumble;

	// Token: 0x040000EA RID: 234
	public Character.OnCriticalHitDelegate OnCriticalHit;

	// Token: 0x040000EB RID: 235
	public Character.OnJumpDelegate OnJump;

	// Token: 0x040000EC RID: 236
	public Transform characterRoot;

	// Token: 0x040000ED RID: 237
	public float characterAngle = 45f;

	// Token: 0x040000EE RID: 238
	public Animation characterAnimation;

	// Token: 0x040000EF RID: 239
	public GameObject shadow;

	// Token: 0x040000F0 RID: 240
	public ParticleSystem hoverboardCrashParticleSystem;

	// Token: 0x040000F1 RID: 241
	public bool fallAnim;

	// Token: 0x040000F2 RID: 242
	private Vector3 characterControllerCenter;

	// Token: 0x040000F3 RID: 243
	private float characterControllerHeight;

	// Token: 0x040000F4 RID: 244
	private Vector3 characterColliderCenter;

	// Token: 0x040000F5 RID: 245
	private float characterColliderHeight;

	// Token: 0x040000F6 RID: 246
	public CharacterPickupParticles CharacterPickupParticleSystem;

	// Token: 0x040000F7 RID: 247
	public float ColliderTrackWidth = 17f;

	// Token: 0x040000F8 RID: 248
	public Animation guardAnimation;

	// Token: 0x040000F9 RID: 249
	[HideInInspector]
	public CharacterController characterController;

	// Token: 0x040000FA RID: 250
	[HideInInspector]
	public OnTriggerObject characterColliderTrigger;

	// Token: 0x040000FB RID: 251
	[HideInInspector]
	public CharacterModel characterModel;

	// Token: 0x040000FC RID: 252
	[HideInInspector]
	public CharacterCamera characterCamera;

	// Token: 0x040000FD RID: 253
	[HideInInspector]
	public Hoverboard hoverboard;

	// Token: 0x040000FE RID: 254
	[HideInInspector]
	public SuperSneakers superSneakers;

	// Token: 0x040000FF RID: 255
	[HideInInspector]
	public Running running;

	// Token: 0x04000100 RID: 256
	public GameObject sprayCanModel;

	// Token: 0x04000101 RID: 257
	[HideInInspector]
	public bool immuneToCriticalHit;

	// Token: 0x04000102 RID: 258
	[HideInInspector]
	public int trackIndex;

	// Token: 0x04000103 RID: 259
	[HideInInspector]
	public float x;

	// Token: 0x04000104 RID: 260
	public float z;

	// Token: 0x04000105 RID: 261
	public float verticalSpeed;

	// Token: 0x04000106 RID: 262
	[HideInInspector]
	public float lastGroundedY;

	// Token: 0x04000107 RID: 263
	private int trackMovement;

	// Token: 0x04000108 RID: 264
	private int trackMovementNext;

	// Token: 0x04000109 RID: 265
	private float characterRotation;

	// Token: 0x0400010A RID: 266
	private int trackIndexTarget;

	// Token: 0x0400010B RID: 267
	private float trackIndexPosition;

	// Token: 0x0400010C RID: 268
	private Game game;

	// Token: 0x0400010D RID: 269
	private Track track;

	// Token: 0x0400010E RID: 270
	[HideInInspector]
	public Character.Animations animations;

	// Token: 0x0400010F RID: 271
	[HideInInspector]
	public float jumpHeight;

	// Token: 0x04000110 RID: 272
	public float gravity = 200f;

	// Token: 0x04000111 RID: 273
	public float jumpHeightNormal = 20f;

	// Token: 0x04000112 RID: 274
	public float jumpHeightSuperSneakers = 40f;

	// Token: 0x04000113 RID: 275
	public float verticalFallSpeedLimit = -1f;

	// Token: 0x04000114 RID: 276
	public float stumbleCornerTolerance = 15f;

	// Token: 0x04000115 RID: 277
	[HideInInspector]
	public bool stumble;

	// Token: 0x04000116 RID: 278
	public float stumbleDecayTime = 5f;

	// Token: 0x04000117 RID: 279
	private IEnumerator roll;

	// Token: 0x04000118 RID: 280
	[HideInInspector]
	public bool jumping;

	// Token: 0x04000119 RID: 281
	[HideInInspector]
	public bool falling;

	// Token: 0x0400011A RID: 282
	private bool inAirJump;

	// Token: 0x0400011B RID: 283
	private string lastHitTag;

	// Token: 0x0400011C RID: 284
	[HideInInspector]
	public bool stopColliding;

	// Token: 0x0400011D RID: 285
	private GameStats stats;

	// Token: 0x0400011E RID: 286
	private FollowingGuard guard;

	// Token: 0x0400011F RID: 287
	private AnimationState runState;

	// Token: 0x04000120 RID: 288
	private int grindSwitch = 1;

	// Token: 0x04000121 RID: 289
	private static Character instance;

	// Token: 0x04000122 RID: 290
	private bool startedJumpFromGround;

	// Token: 0x04000123 RID: 291
	private float trainJumpSampleZ;

	// Token: 0x04000124 RID: 292
	private float trainJumpSampleLength = 10f;

	// Token: 0x04000125 RID: 293
	private bool trainJump;

	// Token: 0x04000126 RID: 294
	private float verticalSpeed_jumpTolerance = -30f;

	// Token: 0x04000127 RID: 295
	private Character.ObstacleTypes lastObstacleTriggerType;

	// Token: 0x04000128 RID: 296
	private int lastObstacleTriggerTrackInex;

	// Token: 0x04000129 RID: 297
	public AudioClipInfo DodgeLeft;

	// Token: 0x0400012A RID: 298
	public AudioClipInfo DodgeRight;

	// Token: 0x0400012B RID: 299
	public AudioClipInfo H_Left;

	// Token: 0x0400012C RID: 300
	public AudioClipInfo H_Right;

	// Token: 0x0400012D RID: 301
	public AudioClipInfo StumbleSound;

	// Token: 0x0400012E RID: 302
	public AudioClipInfo StumbleBushSound;

	// Token: 0x0400012F RID: 303
	public AudioClipInfo StumbleSideSound;

	// Token: 0x02000163 RID: 355
	public struct Animations
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x00037628 File Offset: 0x00035828
		public void SetRandomGrind()
		{
			if (this.grindAnimations.Length == 0 || this.grindLandAnimations.Length != this.grindAnimations.Length)
			{
				Debug.Log("animation arrays should be same length if paired; also not null");
				return;
			}
			int num = Random.Range(0, this.grindAnimations.Length);
			this.run = this.grindAnimations[num];
			this.landing = this.grindLandAnimations[num];
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00037688 File Offset: 0x00035888
		public void SetRandomRun()
		{
			if (this.runAnimations.Length == 0 || this.runAnimations.Length != this.landAnimations.Length)
			{
				Debug.Log("animation arrays should be same length if paired; also not null");
				return;
			}
			int num = Random.Range(0, this.runAnimations.Length);
			this.run = this.runAnimations[num];
			this.landing = this.landAnimations[num];
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000376E8 File Offset: 0x000358E8
		public void SetRandomHover()
		{
			if (this.hoverAnimations.Length == 0 || this.hoverAnimations.Length != this.hoverLandAnimations.Length)
			{
				Debug.Log("animation arrays should be same length if paired; also not null");
				return;
			}
			int num = Random.Range(0, this.hoverAnimations.Length);
			this.run = this.hoverAnimations[num];
			this.landing = this.hoverLandAnimations[num];
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00037748 File Offset: 0x00035948
		public void SetRandomJump()
		{
			if (this.jumpAnimations.Length == 0 || this.hangtimeAnimations.Length == 0)
			{
				Debug.Log("animation array is null");
				return;
			}
			int num = Random.Range(0, this.jumpAnimations.Length);
			int num2 = Random.Range(0, this.hangtimeAnimations.Length);
			this.jump = this.jumpAnimations[num];
			this.hangtime = this.hangtimeAnimations[num2];
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000377AC File Offset: 0x000359AC
		public void SetRandomHoverJump()
		{
			if (this.hoverJumpAnimations.Length == 0 || this.hoverJumpAnimations.Length != this.hoverHangtimeAnimations.Length)
			{
				Debug.Log("animation arrays should be same length if paired; also not null");
				return;
			}
			int num = Random.Range(0, this.hoverJumpAnimations.Length);
			this.jump = this.hoverJumpAnimations[num];
			this.hangtime = this.hoverHangtimeAnimations[num];
		}

		// Token: 0x040008C4 RID: 2244
		public string[] runAnimations;

		// Token: 0x040008C5 RID: 2245
		public string[] landAnimations;

		// Token: 0x040008C6 RID: 2246
		public string[] jumpAnimations;

		// Token: 0x040008C7 RID: 2247
		public string[] hangtimeAnimations;

		// Token: 0x040008C8 RID: 2248
		public string[] grindAnimations;

		// Token: 0x040008C9 RID: 2249
		public string[] grindLandAnimations;

		// Token: 0x040008CA RID: 2250
		public string[] hoverAnimations;

		// Token: 0x040008CB RID: 2251
		public string[] hoverLandAnimations;

		// Token: 0x040008CC RID: 2252
		public string[] hoverJumpAnimations;

		// Token: 0x040008CD RID: 2253
		public string[] hoverHangtimeAnimations;

		// Token: 0x040008CE RID: 2254
		public string jump;

		// Token: 0x040008CF RID: 2255
		public string run;

		// Token: 0x040008D0 RID: 2256
		public string landing;

		// Token: 0x040008D1 RID: 2257
		public string hangtime;

		// Token: 0x040008D2 RID: 2258
		public string roll;

		// Token: 0x040008D3 RID: 2259
		public string dodgeLeft;

		// Token: 0x040008D4 RID: 2260
		public string dodgeRight;

		// Token: 0x040008D5 RID: 2261
		public string hitMid;

		// Token: 0x040008D6 RID: 2262
		public string hitUpper;

		// Token: 0x040008D7 RID: 2263
		public string hitLower;

		// Token: 0x040008D8 RID: 2264
		public string hitMoving;

		// Token: 0x040008D9 RID: 2265
		public string stumble;

		// Token: 0x040008DA RID: 2266
		public string stumbleDeath;

		// Token: 0x040008DB RID: 2267
		public string stumbleLeftSide;

		// Token: 0x040008DC RID: 2268
		public string stumbleRightSide;

		// Token: 0x040008DD RID: 2269
		public string stumbleLeftCorner;

		// Token: 0x040008DE RID: 2270
		public string stumbleRightCorner;
	}

	// Token: 0x02000164 RID: 356
	private enum ObstacleTypes
	{
		// Token: 0x040008E0 RID: 2272
		jumpTrain,
		// Token: 0x040008E1 RID: 2273
		rollBarrier,
		// Token: 0x040008E2 RID: 2274
		jumpBarrier,
		// Token: 0x040008E3 RID: 2275
		none
	}

	// Token: 0x02000165 RID: 357
	private enum ImpactX
	{
		// Token: 0x040008E5 RID: 2277
		Left,
		// Token: 0x040008E6 RID: 2278
		Middle,
		// Token: 0x040008E7 RID: 2279
		Right
	}

	// Token: 0x02000166 RID: 358
	private enum ImpactY
	{
		// Token: 0x040008E9 RID: 2281
		Upper,
		// Token: 0x040008EA RID: 2282
		Middle,
		// Token: 0x040008EB RID: 2283
		Lower
	}

	// Token: 0x02000167 RID: 359
	private enum ImpactZ
	{
		// Token: 0x040008ED RID: 2285
		Before,
		// Token: 0x040008EE RID: 2286
		Middle,
		// Token: 0x040008EF RID: 2287
		After
	}

	// Token: 0x02000168 RID: 360
	// (Invoke) Token: 0x06000A1C RID: 2588
	public delegate void OnStumbleDelegate();

	// Token: 0x02000169 RID: 361
	// (Invoke) Token: 0x06000A20 RID: 2592
	public delegate void OnCriticalHitDelegate();

	// Token: 0x0200016A RID: 362
	// (Invoke) Token: 0x06000A24 RID: 2596
	public delegate void OnJumpDelegate();

	// Token: 0x0200016B RID: 363
	// (Invoke) Token: 0x06000A28 RID: 2600
	public delegate void OnGroundedDelegate();
}
