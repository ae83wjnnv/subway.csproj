using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A8 RID: 168
[AddComponentMenu("NGUI/Examples/Play Idle Animations")]
public class PlayIdleAnimations : MonoBehaviour
{
	// Token: 0x060004F2 RID: 1266 RVA: 0x00017D28 File Offset: 0x00015F28
	private void Start()
	{
		this.mAnim = base.GetComponentInChildren<Animation>();
		if (this.mAnim == null)
		{
			Debug.LogWarning(NGUITools.GetHierarchy(base.gameObject) + " has no Animation component");
			Object.Destroy(this);
			return;
		}
		foreach (object obj in this.mAnim)
		{
			AnimationState animationState = (AnimationState)obj;
			if (animationState.clip.name == "idle")
			{
				animationState.layer = 0;
				this.mIdle = animationState.clip;
				this.mAnim.Play(this.mIdle.name);
			}
			else if (animationState.clip.name.StartsWith("idle"))
			{
				animationState.layer = 1;
				this.mBreaks.Add(animationState.clip);
			}
		}
		if (this.mBreaks.Count == 0)
		{
			Object.Destroy(this);
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x00017E40 File Offset: 0x00016040
	private void Update()
	{
		if (this.mNextBreak >= Time.time)
		{
			return;
		}
		if (this.mBreaks.Count == 1)
		{
			AnimationClip animationClip = this.mBreaks[0];
			this.mNextBreak = Time.time + animationClip.length + Random.Range(5f, 15f);
			this.mAnim.CrossFade(animationClip.name);
			return;
		}
		int num = Random.Range(0, this.mBreaks.Count - 1);
		if (this.mLastIndex == num)
		{
			num++;
			if (num >= this.mBreaks.Count)
			{
				num = 0;
			}
		}
		this.mLastIndex = num;
		AnimationClip animationClip2 = this.mBreaks[num];
		this.mNextBreak = Time.time + animationClip2.length + Random.Range(2f, 8f);
		this.mAnim.CrossFade(animationClip2.name);
	}

	// Token: 0x0400042A RID: 1066
	private Animation mAnim;

	// Token: 0x0400042B RID: 1067
	private AnimationClip mIdle;

	// Token: 0x0400042C RID: 1068
	private List<AnimationClip> mBreaks = new List<AnimationClip>();

	// Token: 0x0400042D RID: 1069
	private float mNextBreak;

	// Token: 0x0400042E RID: 1070
	private int mLastIndex;
}
