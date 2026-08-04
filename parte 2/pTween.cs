using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000153 RID: 339
public class pTween
{
	// Token: 0x060009EA RID: 2538 RVA: 0x00036FBC File Offset: 0x000351BC
	public static IEnumerator To(float duration, float startValue, float endValue, Action<float> callback)
	{
		float time = Time.time;
		float end = time + duration;
		float durationInv = 1f / duration;
		float startMulDurationInv = time / duration;
		for (float num = Time.time; num < end; num = Time.time)
		{
			callback(Mathf.Lerp(startValue, endValue, num * durationInv - startMulDurationInv));
			yield return 0;
		}
		callback(endValue);
		yield break;
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x00036FE0 File Offset: 0x000351E0
	public static IEnumerator RealtimeTo(float duration, float startValue, float endValue, Action<float> callback)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float end = realtimeSinceStartup + duration;
		float durationInv = 1f / duration;
		float startMulDurationInv = realtimeSinceStartup / duration;
		for (float num = Time.realtimeSinceStartup; num < end; num = Time.realtimeSinceStartup)
		{
			callback(Mathf.Lerp(startValue, endValue, num * durationInv - startMulDurationInv));
			yield return 0;
		}
		callback(endValue);
		yield break;
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x00037004 File Offset: 0x00035204
	public static IEnumerator To(float duration, Action<float> callback)
	{
		return pTween.To(duration, 0f, 1f, callback);
	}
}
