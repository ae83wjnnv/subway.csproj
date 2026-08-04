using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000010 RID: 16
[RequireComponent(typeof(Animation))]
[AddComponentMenu("NGUI/Internal/Active Animation")]
public class ActiveAnimation : IgnoreTimeScale
{
	// Token: 0x06000147 RID: 327 RVA: 0x00004E90 File Offset: 0x00003090
	public void Reset()
	{
		if (!(this.mAnim != null))
		{
			return;
		}
		foreach (object obj in this.mAnim)
		{
			AnimationState animationState = (AnimationState)obj;
			if (this.mLastDirection == Direction.Reverse)
			{
				animationState.time = animationState.length;
			}
			else if (this.mLastDirection == Direction.Forward)
			{
				animationState.time = 0f;
			}
		}
	}

	// Token: 0x06000148 RID: 328 RVA: 0x00004F1C File Offset: 0x0000311C
	private void Update()
	{
		float num = base.UpdateRealTimeDelta();
		if (this.mAnim != null)
		{
			bool flag = false;
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				float num2 = animationState.speed * num;
				animationState.time += num2;
				if (num2 < 0f)
				{
					if (animationState.time > 0f)
					{
						flag = true;
					}
					else
					{
						animationState.time = 0f;
					}
				}
				else if (animationState.time < animationState.length)
				{
					flag = true;
				}
				else
				{
					animationState.time = animationState.length;
				}
			}
			this.mAnim.enabled = true;
			this.mAnim.Sample();
			this.mAnim.enabled = false;
			if (flag)
			{
				return;
			}
			if (this.mNotify)
			{
				this.mNotify = false;
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
				}
				if (this.mDisableDirection != Direction.Toggle && this.mLastDirection == this.mDisableDirection)
				{
					NGUITools.SetActive(base.gameObject, false);
				}
			}
		}
		base.enabled = false;
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00005078 File Offset: 0x00003278
	private void Play(string clipName, Direction playDirection)
	{
		if (!(this.mAnim != null))
		{
			return;
		}
		this.mAnim.enabled = false;
		if (playDirection == Direction.Toggle)
		{
			playDirection = ((this.mLastDirection != Direction.Forward) ? Direction.Forward : Direction.Reverse);
		}
		if (string.IsNullOrEmpty(clipName))
		{
			if (!this.mAnim.isPlaying)
			{
				this.mAnim.Play();
			}
		}
		else if (!this.mAnim.IsPlaying(clipName))
		{
			this.mAnim.Play(clipName);
		}
		foreach (object obj in this.mAnim)
		{
			AnimationState animationState = (AnimationState)obj;
			if (string.IsNullOrEmpty(clipName) || animationState.name == clipName)
			{
				float num = Mathf.Abs(animationState.speed);
				animationState.speed = num * (float)playDirection;
				if (playDirection == Direction.Reverse && animationState.time == 0f)
				{
					animationState.time = animationState.length;
				}
				else if (playDirection == Direction.Forward && animationState.time == animationState.length)
				{
					animationState.time = 0f;
				}
			}
		}
		this.mLastDirection = playDirection;
		this.mNotify = true;
	}

	// Token: 0x0600014A RID: 330 RVA: 0x000051AC File Offset: 0x000033AC
	public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection, EnableCondition enableBeforePlay, DisableCondition disableCondition)
	{
		if (!anim.gameObject.active)
		{
			if (enableBeforePlay != EnableCondition.EnableThenPlay)
			{
				return null;
			}
			NGUITools.SetActive(anim.gameObject, true);
		}
		ActiveAnimation activeAnimation = anim.GetComponent<ActiveAnimation>();
		if (activeAnimation != null)
		{
			activeAnimation.enabled = true;
		}
		else
		{
			activeAnimation = anim.gameObject.AddComponent<ActiveAnimation>();
		}
		activeAnimation.mAnim = anim;
		activeAnimation.mDisableDirection = (Direction)disableCondition;
		activeAnimation.Play(clipName, playDirection);
		return activeAnimation;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x00005215 File Offset: 0x00003415
	public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection)
	{
		return ActiveAnimation.Play(anim, clipName, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00005221 File Offset: 0x00003421
	public static ActiveAnimation Play(Animation anim, Direction playDirection)
	{
		return ActiveAnimation.Play(anim, null, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
	}

	// Token: 0x04000068 RID: 104
	public GameObject eventReceiver;

	// Token: 0x04000069 RID: 105
	public string callWhenFinished;

	// Token: 0x0400006A RID: 106
	private Animation mAnim;

	// Token: 0x0400006B RID: 107
	private Direction mLastDirection;

	// Token: 0x0400006C RID: 108
	private Direction mDisableDirection;

	// Token: 0x0400006D RID: 109
	private bool mNotify;
}
