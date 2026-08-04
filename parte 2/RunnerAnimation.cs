using System;
using UnityEngine;

// Token: 0x020000BA RID: 186
public class RunnerAnimation : MonoBehaviour
{
	// Token: 0x06000560 RID: 1376 RVA: 0x0001B1EE File Offset: 0x000193EE
	public void SetAnimationSpeedEvent(AnimationEvent animEvent)
	{
		if (!RunnerAnimation.addedListeners)
		{
			RunnerAnimation.addedListeners = true;
			animEvent.animationState.speed = 1f + (Game.Instance.NormalizedGameSpeed - 1f) * this.AnimationSpeedUpFactor;
		}
	}

	// Token: 0x0400049A RID: 1178
	private static bool addedListeners;

	// Token: 0x0400049B RID: 1179
	public float AnimationSpeedUpFactor = 0.5f;
}
