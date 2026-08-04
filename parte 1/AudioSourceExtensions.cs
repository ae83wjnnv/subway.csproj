using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000018 RID: 24
public static class AudioSourceExtensions
{
	// Token: 0x0600015E RID: 350 RVA: 0x000055B8 File Offset: 0x000037B8
	public static IEnumerator fadeOut(this AudioSource audioSource, float duration, Action onComplete)
	{
		float startingVolume = audioSource.volume;
		while (audioSource.volume > 0f)
		{
			audioSource.volume -= Time.deltaTime * startingVolume / duration;
			yield return null;
		}
		if (onComplete != null)
		{
			onComplete();
		}
		yield break;
	}
}
