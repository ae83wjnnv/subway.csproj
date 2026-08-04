using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000086 RID: 134
public class KeyframeBehaviour : MonoBehaviour
{
	// Token: 0x0600042C RID: 1068 RVA: 0x000127B4 File Offset: 0x000109B4
	private void Start()
	{
		this.animationEvents = new AnimationEvent[this.Actions.Count];
		int num = 0;
		foreach (KeyFrameAction keyFrameAction in this.Actions)
		{
			AnimationEvent animationEvent = new AnimationEvent();
			animationEvent.messageOptions = SendMessageOptions.RequireReceiver;
			animationEvent.time = (float)keyFrameAction.KeyFrame / this.TargetAnimation[keyFrameAction.clip].clip.frameRate;
			animationEvent.intParameter = num;
			animationEvent.functionName = "DoKeyframeAnimation";
			this.TargetAnimation[keyFrameAction.clip].clip.AddEvent(animationEvent);
			num++;
		}
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00012880 File Offset: 0x00010A80
	public void DoKeyframeAnimation(int soundIndex)
	{
		KeyFrameAction info = this.Actions[soundIndex];
		this.TargetObjects.ForEach(delegate(ParticleSystem t)
		{
			t.enableEmission = info.state;
		});
	}

	// Token: 0x0400038D RID: 909
	public Animation TargetAnimation;

	// Token: 0x0400038E RID: 910
	public List<ParticleSystem> TargetObjects;

	// Token: 0x0400038F RID: 911
	public List<KeyFrameAction> Actions;

	// Token: 0x04000390 RID: 912
	private AnimationEvent[] animationEvents;
}
