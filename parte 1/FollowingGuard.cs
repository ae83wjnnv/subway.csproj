using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class FollowingGuard : MonoBehaviour
{
	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000CCCB File Offset: 0x0000AECB
	public static FollowingGuard Instance
	{
		get
		{
			FollowingGuard followingGuard;
			if ((followingGuard = FollowingGuard.instance) == null)
			{
				followingGuard = (FollowingGuard.instance = Object.FindObjectOfType(typeof(FollowingGuard)) as FollowingGuard);
			}
			return followingGuard;
		}
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
	private void Awake()
	{
		this.game = Game.Instance;
		this.character = Character.Instance;
		this.characterTransform = this.character.transform;
		this.enemyRenderers = base.gameObject.GetComponentsInChildren<Renderer>();
		this.enemiesStartPos = new Vector3[this.enemies.Length];
		for (int i = 0; i < this.enemies.Length; i++)
		{
			this.enemiesStartPos[i] = this.enemies[i].position;
		}
		this.x = new SmoothDampFloat(0f, this.xSmoothTime);
		base.GetComponent<AudioSource>().volume = this.guardProximityLoopVolume;
		Game game = this.game;
		game.OnPauseChange = (Game.OnPauseChangeDelegate)Delegate.Combine(game.OnPauseChange, new Game.OnPauseChangeDelegate(this.HandleOnPauseChange));
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
	private void HandleOnPauseChange(bool pause)
	{
		if (pause)
		{
			if (base.GetComponent<AudioSource>().isPlaying)
			{
				base.GetComponent<AudioSource>().Pause();
			}
			this.isPaused = true;
		}
		if (!pause)
		{
			if (this.isPaused)
			{
				base.GetComponent<AudioSource>().Play();
			}
			this.isPaused = false;
		}
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x0000CE10 File Offset: 0x0000B010
	public void Restart(bool closeToCharacter)
	{
		base.StopAllCoroutines();
		this.closeToCharacter = closeToCharacter;
		this.distanceToCharacter = ((!closeToCharacter) ? this.distanceToCharacterMax : this.distanceToCharacterMin);
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x0000CE38 File Offset: 0x0000B038
	public void OnEnable()
	{
		this.lastGroundedSmooth = this.character.lastGroundedY;
		this.lastGroundedVelocity = 0f;
		this.y = this.character.lastGroundedY;
		this.x.Value = this.character.transform.position.x;
		this.distanceToCharacter = this.distanceToCharacterMin;
		this.closeToCharacter = true;
		this.verticalSpeed = 0f;
		bool flag = false;
		this.guardAnimation["Guard_Run"].enabled = flag;
		if (flag)
		{
			this.guardAnimation.Play("Guard_Run");
			this.dogRightAnimation.Play("Dog_Fast Run");
		}
		Character character = this.character;
		character.OnJump = (Character.OnJumpDelegate)Delegate.Combine(character.OnJump, new Character.OnJumpDelegate(this.Jump));
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0000CF14 File Offset: 0x0000B114
	public void OnDisable()
	{
		Character character = this.character;
		character.OnJump = (Character.OnJumpDelegate)Delegate.Remove(character.OnJump, new Character.OnJumpDelegate(this.Jump));
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0000CF3D File Offset: 0x0000B13D
	public void CatchUp()
	{
		this.CatchUp(this.catchUpDuration);
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0000CF4C File Offset: 0x0000B14C
	public void CatchUp(float duration)
	{
		if (!this.closeToCharacter)
		{
			float distanceFrom = this.distanceToCharacter;
			this.ShowEnemies(true);
			base.StopAllCoroutines();
			this.guardAnimation.Play("Guard_grap after");
			this.guardAnimation.PlayQueued("Guard_Run");
			base.GetComponent<AudioSource>().timeSamples = Random.Range(0, base.GetComponent<AudioSource>().timeSamples);
			base.GetComponent<AudioSource>().Play();
			base.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.05f);
			base.StartCoroutine(pTween.To(duration, delegate(float t)
			{
				this.distanceToCharacter = Mathf.SmoothStep(distanceFrom, this.distanceToCharacterMin, t);
			}));
			base.StartCoroutine(pTween.To(duration, delegate(float t)
			{
				base.GetComponent<AudioSource>().volume = Mathf.SmoothStep(0f, this.guardProximityLoopVolume, t);
			}));
			this.closeToCharacter = true;
		}
	}

	// Token: 0x060002EC RID: 748 RVA: 0x0000D026 File Offset: 0x0000B226
	public void ResetCatchUp()
	{
		this.ResetCatchUp(this.resetCatchUpDuration);
	}

	// Token: 0x060002ED RID: 749 RVA: 0x0000D034 File Offset: 0x0000B234
	public void ResetCatchUp(float duration)
	{
		base.StartCoroutine(this.ResetCatchUpCoroutine(duration));
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0000D044 File Offset: 0x0000B244
	public IEnumerator ResetCatchUpCoroutine(float duration)
	{
		if (this.closeToCharacter)
		{
			float distanceFrom = this.distanceToCharacter;
			this.closeToCharacter = false;
			base.StartCoroutine(pTween.To(duration, delegate(float t)
			{
				this.distanceToCharacter = Mathf.SmoothStep(distanceFrom, this.distanceToCharacterMax, t);
			}));
			yield return base.StartCoroutine(pTween.To(duration * 2f, delegate(float t)
			{
				base.GetComponent<AudioSource>().volume = Mathf.SmoothStep(this.guardProximityLoopVolume, 0f, t);
			}));
			base.GetComponent<AudioSource>().Stop();
			if (!this.game.isDead)
			{
				this.ShowEnemies(false);
			}
		}
		yield break;
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0000D05A File Offset: 0x0000B25A
	public void MuteProximityLoop()
	{
		base.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0000D068 File Offset: 0x0000B268
	public void PlayIntro()
	{
		base.gameObject.transform.position = new Vector3(0f, 0f, -10f);
		for (int i = 0; i < this.enemies.Length; i++)
		{
			this.enemies[i].position = this.enemiesStartPos[i];
			this.enemies[i].rotation = Quaternion.Euler(0f, 0f, 0f);
		}
		this.guardAnimation.Play("playIntro");
		this.dogRightAnimation.Play("playIntro");
		this.guardAnimation.CrossFadeQueued("Guard_Run", 0.2f);
		this.dogRightAnimation.CrossFadeQueued("Dog_Fast Run", 0.2f);
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0000D134 File Offset: 0x0000B334
	public void CatchPlayer(float pos)
	{
		base.GetComponent<AudioSource>().Stop();
		base.StopAllCoroutines();
		this.character.characterAnimation.Stop("caught");
		this.character.characterAnimation.Stop("caught2");
		if (pos < 20f)
		{
			this.guardAnimation.CrossFade("catch2", 0.2f);
			this.dogRightAnimation.CrossFade("catch2", 0.2f);
			this.character.animations.stumbleDeath = "caught2";
		}
		else
		{
			this.guardAnimation.CrossFade("catch", 0.2f);
			this.dogRightAnimation.CrossFade("catch", 0.2f);
			this.character.animations.stumbleDeath = "caught";
		}
		this.character.characterAnimation[this.character.animations.stumbleDeath].weight = 0f;
		this.character.characterAnimation[this.character.animations.stumbleDeath].enabled = true;
		float num = 0.68f;
		base.StartCoroutine(pTween.To(num, delegate(float t)
		{
			for (int i = 0; i < this.enemies.Length; i++)
			{
				this.enemies[i].position = Vector3.Lerp(this.enemies[i].position, this.character.transform.position, t);
			}
		}));
		base.StartCoroutine(this.CatchPlayerAnimStarter(num));
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0000D281 File Offset: 0x0000B481
	private IEnumerator CatchPlayerAnimStarter(float delay)
	{
		yield return new WaitForSeconds(delay);
		base.StartCoroutine(pTween.To(0.2f, delegate(float t)
		{
			this.character.characterAnimation[this.character.animations.stumbleDeath].weight = Mathf.Lerp(0f, 1f, t);
		}));
		yield break;
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0000D297 File Offset: 0x0000B497
	public void HitByTrainSequence()
	{
		base.GetComponent<AudioSource>().Stop();
		base.StartCoroutine(this.HitByTrainSequenceCoroutine());
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0000D2B1 File Offset: 0x0000B4B1
	public IEnumerator HitByTrainSequenceCoroutine()
	{
		float num = 0.2f;
		yield return base.StartCoroutine(pTween.To(num, delegate(float t)
		{
			for (int i = 0; i < this.enemies.Length; i++)
			{
				this.enemies[i].position = Vector3.Lerp(this.enemies[i].position, this.character.transform.position, t);
			}
		}));
		this.dogRightAnimation.Play("Dog_death_movingTrain");
		yield return new WaitForSeconds(0.4f);
		Vector3 charPos = this.characterTransform.position;
		base.StartCoroutine(pTween.To(1f, delegate(float t)
		{
			this.characterTransform.position = Vector3.Lerp(charPos, new Vector3(charPos.x, -5f, charPos.z), t);
		}));
		yield return new WaitForSeconds(0.2f);
		this.guardAnimation.Play("Guard_death_movingTrain");
		yield break;
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
	public void ShowEnemies(bool vis)
	{
		this.isShowing = vis;
		Renderer[] array = this.enemyRenderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.active = vis;
		}
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
	public void LateUpdate()
	{
		this.x.Target = this.character.transform.position.x;
		this.x.Update();
		this.lastGroundedSmooth = Mathf.SmoothDamp(this.lastGroundedSmooth, this.character.lastGroundedY, ref this.lastGroundedVelocity, this.lastGroundedSmoothTime);
		if (this.y > this.lastGroundedSmooth)
		{
			this.verticalSpeed -= this.gravity * Time.deltaTime;
		}
		this.y += this.verticalSpeed * Time.deltaTime;
		this.y = Mathf.Max(this.y, this.lastGroundedSmooth);
		Vector3 vector = this.characterTransform.position - Vector3.forward * this.distanceToCharacter;
		vector.y = this.y;
		vector.x = this.x.Value;
		base.transform.position = vector;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x0000D3FB File Offset: 0x0000B5FB
	private void Jump()
	{
		this.Jump(this.distanceToCharacter / this.game.currentSpeed);
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0000D415 File Offset: 0x0000B615
	public void Jump(float delay)
	{
		base.StartCoroutine(this.JumpCoroutine(delay));
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x0000D425 File Offset: 0x0000B625
	private IEnumerator JumpCoroutine(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.guardAnimation.Play("Guard_jump");
		this.guardAnimation.CrossFadeQueued("Guard_Run", 0.2f);
		this.dogRightAnimation.Play("Dog_jump");
		this.dogRightAnimation.CrossFadeQueued("Dog_Fast Run", 0.2f);
		this.verticalSpeed = this.character.CalculateJumpVerticalSpeed() * 0.6f;
		yield break;
	}

	// Token: 0x0400021A RID: 538
	public float distanceToCharacterMin = 10f;

	// Token: 0x0400021B RID: 539
	public float distanceToCharacterMax = 50f;

	// Token: 0x0400021C RID: 540
	public float catchUpDuration = 0.7f;

	// Token: 0x0400021D RID: 541
	public float resetCatchUpDuration = 1.5f;

	// Token: 0x0400021E RID: 542
	public float lastGroundedSmoothTime = 0.3f;

	// Token: 0x0400021F RID: 543
	public float xSmoothTime = 0.1f;

	// Token: 0x04000220 RID: 544
	public float gravity = 200f;

	// Token: 0x04000221 RID: 545
	public bool isShowing;

	// Token: 0x04000222 RID: 546
	public Animation guardAnimation;

	// Token: 0x04000223 RID: 547
	public Animation dogRightAnimation;

	// Token: 0x04000224 RID: 548
	private Renderer[] enemyRenderers;

	// Token: 0x04000225 RID: 549
	public Transform[] enemies;

	// Token: 0x04000226 RID: 550
	private Vector3[] enemiesStartPos;

	// Token: 0x04000227 RID: 551
	private float y;

	// Token: 0x04000228 RID: 552
	private bool closeToCharacter;

	// Token: 0x04000229 RID: 553
	private float distanceToCharacter;

	// Token: 0x0400022A RID: 554
	private float lastGroundedSmooth;

	// Token: 0x0400022B RID: 555
	private float lastGroundedVelocity;

	// Token: 0x0400022C RID: 556
	private SmoothDampFloat x;

	// Token: 0x0400022D RID: 557
	private Game game;

	// Token: 0x0400022E RID: 558
	private Character character;

	// Token: 0x0400022F RID: 559
	private Transform characterTransform;

	// Token: 0x04000230 RID: 560
	private float verticalSpeed;

	// Token: 0x04000231 RID: 561
	public float guardProximityLoopVolume = 0.9f;

	// Token: 0x04000232 RID: 562
	private static FollowingGuard instance;

	// Token: 0x04000233 RID: 563
	private bool isPaused = true;
}
