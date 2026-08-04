using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class AnimationSoundPlayer : MonoBehaviour
{
	// Token: 0x06000156 RID: 342 RVA: 0x0000539C File Offset: 0x0000359C
	private void Start()
	{
		if (AnimationSoundPlayer.nodesInitialized.IndexOf(base.name) != -1)
		{
			return;
		}
		AnimationSoundPlayer.nodesInitialized.Add(base.name);
		this.animationEvents = new AnimationEvent[this.AudioClips.Count];
		int num = 0;
		foreach (KeyFrameAudio keyFrameAudio in this.AudioClips)
		{
			AnimationEvent animationEvent = new AnimationEvent();
			animationEvent.messageOptions = SendMessageOptions.RequireReceiver;
			animationEvent.time = (float)keyFrameAudio.KeyFrame / this.TargetAnimation[keyFrameAudio.clip].clip.frameRate;
			animationEvent.intParameter = num;
			animationEvent.functionName = "PlayKeyframeAnimation";
			this.TargetAnimation[keyFrameAudio.clip].clip.AddEvent(animationEvent);
			num++;
		}
	}

	// Token: 0x06000157 RID: 343 RVA: 0x0000548C File Offset: 0x0000368C
	public virtual void PlayKeyframeAnimation(int soundIndex)
	{
		KeyFrameAudio keyFrameAudio = this.AudioClips[soundIndex];
		if (keyFrameAudio.Callback != null)
		{
			keyFrameAudio.Callback(keyFrameAudio);
			return;
		}
		So.Instance.playSound(this.AudioClips[soundIndex].Audio);
	}

	// Token: 0x04000077 RID: 119
	public Animation TargetAnimation;

	// Token: 0x04000078 RID: 120
	public List<KeyFrameAudio> AudioClips;

	// Token: 0x04000079 RID: 121
	private bool addedListeners;

	// Token: 0x0400007A RID: 122
	private static List<string> nodesInitialized = new List<string>();

	// Token: 0x0400007B RID: 123
	private AnimationEvent[] animationEvents;
}
