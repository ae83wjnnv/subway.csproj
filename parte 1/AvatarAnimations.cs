using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class AvatarAnimations : MonoBehaviour
{
	// Token: 0x0600016B RID: 363 RVA: 0x00005965 File Offset: 0x00003B65
	private void Start()
	{
		this.Target = this.FindAnimationInParent(base.gameObject);
		if (this.Target == null)
		{
			Debug.Log(" No animation component for avatar animations");
			return;
		}
		if (this.PlayIdleAnimations)
		{
			this.StartIdleAnimations();
		}
	}

	// Token: 0x0600016C RID: 364 RVA: 0x000059A0 File Offset: 0x00003BA0
	private Animation FindAnimationInParent(GameObject current)
	{
		Animation component = current.GetComponent<Animation>();
		if (component != null)
		{
			return component;
		}
		if (current.transform.parent != null)
		{
			return this.FindAnimationInParent(current.transform.parent.gameObject);
		}
		return null;
	}

	// Token: 0x0600016D RID: 365 RVA: 0x000059EA File Offset: 0x00003BEA
	private void Update()
	{
		if (this.PlayIdleAnimations && this.animationRoutine != null && !this.Paused)
		{
			this.animationRoutine.MoveNext();
		}
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00005A10 File Offset: 0x00003C10
	public void StartIdleAnimations()
	{
		this.PlayIdleAnimations = true;
		this.Paused = false;
		this.Target.AddClip(this.Breath, this.Breath.name);
		foreach (AnimationClip animationClip in this.Idles)
		{
			this.Target.AddClip(animationClip, animationClip.name);
		}
		this.animationRoutine = this.Play();
		this.animationRoutine.MoveNext();
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00005AB0 File Offset: 0x00003CB0
	public void StopIdleAnimations()
	{
		this.PlayIdleAnimations = false;
		this.Target.AddClip(this.Breath, this.Breath.name);
		foreach (AnimationClip animationClip in this.Idles)
		{
			using (IEnumerator enumerator2 = this.Target.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (((AnimationState)enumerator2.Current).clip == animationClip)
					{
						this.Target.RemoveClip(animationClip);
					}
				}
			}
		}
		this.animationRoutine = null;
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00005B80 File Offset: 0x00003D80
	public void PauseIdleAnimations()
	{
		this.Paused = true;
		foreach (object obj in this.Target.GetComponent<Animation>())
		{
			((AnimationState)obj).speed = 0f;
		}
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00005BE8 File Offset: 0x00003DE8
	public void ResumeIdleAnimations()
	{
		this.Paused = false;
		foreach (object obj in this.Target.GetComponent<Animation>())
		{
			((AnimationState)obj).speed = 1f;
		}
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00005C50 File Offset: 0x00003E50
	private IEnumerator Play()
	{
		int index2 = 0;
		List<AnimationClip> list = new List<AnimationClip>();
		Predicate<AnimationClip> <>9__0;
		while (this.PlayIdleAnimations)
		{
			int count = Random.Range(this.MinIdleTimes, this.MaxIdleTimes);
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				this.Target.Play(this.Breath.name);
				this.nextAnimationTime = this.Breath.length;
				while (this.nextAnimationTime > 0f)
				{
					this.nextAnimationTime -= Time.deltaTime;
					yield return 0;
				}
				num = i;
			}
			List<AnimationClip> idles = this.Idles;
			Predicate<AnimationClip> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = (AnimationClip a) => a != this.Idles[index2]);
			}
			list = idles.FindAll(predicate);
			index2 = Random.Range(0, list.Count);
			this.Target.Play(list[index2].name);
			this.nextAnimationTime = list[index2].length;
			while (this.nextAnimationTime > 0f)
			{
				this.nextAnimationTime -= Time.deltaTime;
				yield return 0;
			}
		}
		this.animationRoutine = null;
		yield break;
	}

	// Token: 0x040000B7 RID: 183
	public Animation Target;

	// Token: 0x040000B8 RID: 184
	public bool PlayIdleAnimations;

	// Token: 0x040000B9 RID: 185
	public int MinIdleTimes;

	// Token: 0x040000BA RID: 186
	public int MaxIdleTimes;

	// Token: 0x040000BB RID: 187
	public AnimationClip Breath;

	// Token: 0x040000BC RID: 188
	public List<AnimationClip> Idles;

	// Token: 0x040000BD RID: 189
	public bool Paused;

	// Token: 0x040000BE RID: 190
	private IEnumerator animationRoutine;

	// Token: 0x040000BF RID: 191
	private float nextAnimationTime;
}
