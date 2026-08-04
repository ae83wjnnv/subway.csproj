using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class Running : CharacterState
{
	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06000562 RID: 1378 RVA: 0x0001B238 File Offset: 0x00019438
	public static Running Instance
	{
		get
		{
			Running running;
			if ((running = Running.instance) == null)
			{
				running = (Running.instance = Object.FindObjectOfType(typeof(Running)) as Running);
			}
			return running;
		}
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0001B260 File Offset: 0x00019460
	public void Awake()
	{
		this.game = Game.Instance;
		this.track = Track.Instance;
		this.character = Character.Instance;
		this.characterTransform = this.character.transform;
		this.characterController = this.character.characterController;
		this.characterCamera = CharacterCamera.Instance;
		this.characterCameraTransform = this.characterCamera.transform;
		this.characterAnimation = this.character.characterAnimation;
		this.characterAnimation["stumbleCornerLeft"].AddMixingTransform(this.spineAnimation);
		this.characterAnimation["stumbleCornerLeft"].layer = 2;
		this.characterAnimation["stumbleCornerLeft"].weight = 1f;
		this.characterAnimation["stumbleCornerRight"].AddMixingTransform(this.spineAnimation);
		this.characterAnimation["stumbleCornerRight"].layer = 2;
		this.characterAnimation["stumbleCornerRight"].weight = 1f;
		Character character = this.character;
		character.OnStumble = (Character.OnStumbleDelegate)Delegate.Combine(character.OnStumble, new Character.OnStumbleDelegate(this.character.characterCamera.Shake));
		this.character.OnGrounded += this.UpdateGroundTag;
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0001B3BB File Offset: 0x000195BB
	public override IEnumerator Begin()
	{
		bool transitionFromJetpack = this.characterTransform.position.y > 70f;
		this.character.characterCollider.enabled = true;
		this.character.characterCamera.enabled = true;
		SmoothDampVector3 currentCameraOffset = new SmoothDampVector3(this.cameraOffset, this.cameraOffsetSmoothDuration)
		{
			Target = this.cameraOffset
		};
		SmoothDampFloat smoothCameraX = new SmoothDampFloat(this.characterCameraTransform.position.x, this.smoothCameraXDuration);
		SmoothDampFloat currentCameraAimOffset = new SmoothDampFloat(this.cameraAimOffset, this.cameraOffsetSmoothDuration);
		this.offsetDeltaCurve = new Curve();
		this.character.lastGroundedY = this.character.transform.position.y;
		AnimationEvent animationEvent = new AnimationEvent
		{
			functionName = "SetAnimationSpeedEvent",
			time = 0.1f,
			messageOptions = SendMessageOptions.RequireReceiver
		};
		this.characterAnimation["run"].clip.AddEvent(animationEvent);
		this.characterAnimation["run2"].clip.AddEvent(animationEvent);
		this.characterAnimation["run3"].clip.AddEvent(animationEvent);
		this.characterAnimation["run4_long"].clip.AddEvent(animationEvent);
		ParticleSystem[] componentsInChildren = this.character.sprayCanModel.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enableEmission = false;
		}
		float transitionTimeMax = 2f;
		float transitionTime = 0f;
		Vector3 cameraPositionStart = this.characterCamera.position;
		Vector3 cameraTargetStart = this.characterCamera.target;
		this.character.fallAnim = false;
		this.characterController.Move(Vector3.down * 2f);
		this.character.fallAnim = true;
		this.character.ChangeAnimations();
		SmoothDampFloat y = new SmoothDampFloat(this.characterTransform.position.y, this.ySmoothDuration);
		for (;;)
		{
			this.game.LayTrackChunks();
			this.game.currentSpeed = this.game.currentLevelSpeed;
			this.game.HandleControls();
			this.character.ApplyGravity();
			this.character.MoveForward();
			Vector3 position = this.character.transform.position;
			if (this.game.Modifiers.IsActive(this.game.Modifiers.SuperSneakes))
			{
				y.Target = 0.5f * (this.character.lastGroundedY + position.y);
			}
			else if (this.characterController.isGrounded)
			{
				y.Target = this.character.lastGroundedY;
			}
			else if (position.y < this.character.lastGroundedY)
			{
				y.Target = position.y;
			}
			Vector3 vector = this.offsetDeltaCurve.Evaluate(this.character.z - this.tunnelStartZ);
			smoothCameraX.Target = position.x * 0.75f;
			Vector3 vector2 = new Vector3(smoothCameraX.Value, y.Value, position.z) + currentCameraOffset.Value + vector;
			Vector3 vector3 = new Vector3(smoothCameraX.Value, 0f, position.z) + Vector3.up * (y.Value + currentCameraAimOffset.Value) + vector * 0.5f;
			if (transitionFromJetpack)
			{
				float num = Mathf.Clamp01(transitionTime / transitionTimeMax);
				float num2 = this.transitionFromJetpackCurve.Evaluate(num);
				this.character.characterCamera.position = Vector3.Lerp(cameraPositionStart, vector2, num2);
				this.character.characterCamera.target = Vector3.Lerp(cameraTargetStart, vector3, num2);
				if (num == 1f)
				{
					transitionFromJetpack = false;
				}
				transitionTime += Time.deltaTime;
			}
			else
			{
				this.character.characterCamera.position = vector2;
				this.character.characterCamera.target = vector3;
			}
			this.character.CheckInAirJump();
			y.Update();
			currentCameraOffset.Update();
			currentCameraAimOffset.Update();
			smoothCameraX.Update();
			this.game.UpdateMeters();
			this.UpdateInAirRunPosition();
			this.UpdateRunStateMeters();
			yield return 0;
		}
		yield break;
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0001B3CC File Offset: 0x000195CC
	private void UpdateRunStateMeters()
	{
		float num = this.game.currentSpeed * Time.deltaTime;
		GameStats gameStats = GameStats.Instance;
		if (this.currentRunPosition != Running.RunPositions.air)
		{
			if (this.character.trackIndex == 0)
			{
				gameStats.metersRunLeftTrack += num;
			}
			if (this.character.trackIndex == 1)
			{
				gameStats.metersRunCenterTrack += num;
			}
			if (this.character.trackIndex == 2)
			{
				gameStats.metersRunRightTrack += num;
			}
		}
		if (this.currentRunPosition == Running.RunPositions.ground)
		{
			GameStats.Instance.metersRunGround += num;
		}
		if (this.currentRunPosition == Running.RunPositions.air)
		{
			GameStats.Instance.metersFly += num;
		}
		if (this.currentRunPosition == Running.RunPositions.station)
		{
			GameStats.Instance.metersRunStation += num;
		}
		if (this.currentRunPosition == Running.RunPositions.train)
		{
			GameStats.Instance.metersRunTrain += num;
		}
		if (this.currentRunPosition == Running.RunPositions.movingTrain)
		{
			GameStats.Instance.metersRunTrain += num;
		}
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0001B4D3 File Offset: 0x000196D3
	private void UpdateInAirRunPosition()
	{
		if (!this.characterController.isGrounded)
		{
			this.currentRunPosition = Running.RunPositions.air;
		}
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x0001B4EC File Offset: 0x000196EC
	private void UpdateGroundTag()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(this.character.characterRoot.position, -Vector3.up), out raycastHit))
		{
			string tag = raycastHit.collider.tag;
			if (tag != null)
			{
				if (tag == "Ground")
				{
					this.currentRunPosition = Running.RunPositions.ground;
					return;
				}
				if (tag == "HitTrain")
				{
					this.currentRunPosition = Running.RunPositions.train;
					return;
				}
				if (tag == "HitMovingTrain")
				{
					this.currentRunPosition = Running.RunPositions.movingTrain;
					return;
				}
				if (!(tag == "Station"))
				{
					return;
				}
				this.currentRunPosition = Running.RunPositions.station;
			}
		}
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0001B585 File Offset: 0x00019785
	public override void HandleCriticalHit()
	{
		this.character.characterCamera.Shake();
		this.game.Die();
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0001B5A4 File Offset: 0x000197A4
	public override void HandleSwipe(SwipeDir swipeDir)
	{
		switch (swipeDir)
		{
		case SwipeDir.Up:
			this.character.Jump();
			return;
		case SwipeDir.Down:
			this.character.Roll();
			break;
		case SwipeDir.Left:
			this.character.ChangeTrack(-1, this.characterChangeTrackLength / this.game.currentSpeed);
			return;
		case SwipeDir.Right:
			this.character.ChangeTrack(1, this.characterChangeTrackLength / this.game.currentSpeed);
			return;
		case SwipeDir.None:
			break;
		default:
			return;
		}
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0001B624 File Offset: 0x00019824
	public override void HandleDoubleTap()
	{
		if (PlayerInfo.Instance.GetUpgradeAmount(PowerupType.hoverboard) > 0 && !this.game.modifiers.IsActive(this.game.modifiers.Hoverboard))
		{
			this.game.Modifiers.Add(this.game.Modifiers.Hoverboard);
		}
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0001B684 File Offset: 0x00019884
	public void StartTunnel(float tunnelLength)
	{
		this.tunnelStartZ = this.character.z;
		this.offsetDeltaCurve = new Curve();
		this.offsetDeltaCurve.AddKey(0f, Vector3.zero, -Vector3.up, -Vector3.up);
		this.offsetDeltaCurve.AddKey(tunnelLength / 2f, Vector3.up * this.tunnelDelta);
		this.offsetDeltaCurve.AddKey(tunnelLength, Vector3.zero, Vector3.up * 0.001f, Vector3.up * 0.001f);
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0001B727 File Offset: 0x00019927
	public void EndTunnel()
	{
	}

	// Token: 0x0400049C RID: 1180
	public float slowDownDuration = 0.625f;

	// Token: 0x0400049D RID: 1181
	public float slowDownRatio = 1f;

	// Token: 0x0400049E RID: 1182
	public float tunnelDelta = -20f;

	// Token: 0x0400049F RID: 1183
	public Vector3 cameraOffset = new Vector3(0f, 33f, -33f);

	// Token: 0x040004A0 RID: 1184
	public float cameraOffsetSmoothDuration = 0.5f;

	// Token: 0x040004A1 RID: 1185
	public float cameraAimOffset = 20f;

	// Token: 0x040004A2 RID: 1186
	public float cameraFOV = 60f;

	// Token: 0x040004A3 RID: 1187
	public float smoothCameraXDuration = 0.05f;

	// Token: 0x040004A4 RID: 1188
	public float ySmoothDuration = 0.1f;

	// Token: 0x040004A5 RID: 1189
	public float characterChangeTrackLength = 30f;

	// Token: 0x040004A6 RID: 1190
	public AnimationCurve transitionFromJetpackCurve;

	// Token: 0x040004A7 RID: 1191
	private float tunnelStartZ;

	// Token: 0x040004A8 RID: 1192
	private Curve offsetDeltaCurve;

	// Token: 0x040004A9 RID: 1193
	private Game game;

	// Token: 0x040004AA RID: 1194
	private Character character;

	// Token: 0x040004AB RID: 1195
	private Transform characterTransform;

	// Token: 0x040004AC RID: 1196
	private CharacterController characterController;

	// Token: 0x040004AD RID: 1197
	private CharacterCamera characterCamera;

	// Token: 0x040004AE RID: 1198
	private Transform characterCameraTransform;

	// Token: 0x040004AF RID: 1199
	private Animation characterAnimation;

	// Token: 0x040004B0 RID: 1200
	public Transform spineAnimation;

	// Token: 0x040004B1 RID: 1201
	private Track track;

	// Token: 0x040004B2 RID: 1202
	public Running.RunPositions currentRunPosition;

	// Token: 0x040004B3 RID: 1203
	private static Running instance;

	// Token: 0x020001CE RID: 462
	public enum RunPositions
	{
		// Token: 0x04000AE9 RID: 2793
		ground,
		// Token: 0x04000AEA RID: 2794
		station,
		// Token: 0x04000AEB RID: 2795
		train,
		// Token: 0x04000AEC RID: 2796
		movingTrain,
		// Token: 0x04000AED RID: 2797
		air
	}
}
