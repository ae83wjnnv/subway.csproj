using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class TurnTableLoopPlayer : MonoBehaviour
{
	// Token: 0x0600066E RID: 1646 RVA: 0x00020148 File Offset: 0x0001E348
	private void Awake()
	{
		this.audioSource = base.gameObject.AddComponent<AudioSource>();
		this.rewardSource = base.gameObject.AddComponent<AudioSource>();
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0002016C File Offset: 0x0001E36C
	public void StartLoop()
	{
		this.startRoutine = base.StartCoroutine(this.StartLooper(this.audioLoopInfo));
		this.rewardSource.clip = this.audioRewardInfo.Clip;
		this.rewardSource.pitch = Random.Range(this.audioRewardInfo.minPitch, this.audioRewardInfo.maxPitch);
		this.rewardSource.volume = Random.Range(this.audioRewardInfo.minVolume, this.audioRewardInfo.maxVolume);
		this.rewardSource.Play();
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x000201FE File Offset: 0x0001E3FE
	public void StopLoop()
	{
		this.stopRoutine = base.StartCoroutine(this.StopLooper(this.audioLoopInfo));
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x00020218 File Offset: 0x0001E418
	private IEnumerator StartLooper(AudioLoopInfo audioLoopInfo)
	{
		this.audioSource.clip = audioLoopInfo.clips[Random.Range(0, audioLoopInfo.clips.Length)];
		this.audioSource.loop = true;
		this.audioSource.Play();
		float counter = 0f;
		float startFadeVol = Random.Range(audioLoopInfo.minStartVolume, audioLoopInfo.maxStartVolume);
		float startFadePitch = Random.Range(audioLoopInfo.minStartPitch, audioLoopInfo.maxStartPitch);
		float fadeSpeed = 1f / Random.Range(audioLoopInfo.minStartTime, audioLoopInfo.maxStartTime);
		float targetVol = Random.Range(audioLoopInfo.minTargetVolume, audioLoopInfo.maxTargatVolume);
		float targetPitch = Random.Range(audioLoopInfo.minTargetPitch, audioLoopInfo.maxTargetPitch);
		while (counter < 1f)
		{
			float num = Mathf.Lerp(startFadeVol, targetVol, counter);
			this.audioSource.volume = num;
			float num2 = Mathf.Lerp(startFadePitch, targetPitch, counter);
			this.audioSource.pitch = num2;
			counter += Time.deltaTime * fadeSpeed;
			yield return 0;
		}
		this.audioSource.volume = targetVol;
		this.audioSource.pitch = targetPitch;
		this.startRoutine = null;
		yield break;
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0002022E File Offset: 0x0001E42E
	private IEnumerator StopLooper(AudioLoopInfo audioLoopInfo)
	{
		if (this.startRoutine != null)
		{
			base.StopCoroutine("StartLooper");
		}
		this.audioSource.Play();
		float counter = 0f;
		float startFadeVol = this.audioSource.volume;
		float startFadePitch = this.audioSource.pitch;
		float fadeSpeed = 1f / Random.Range(audioLoopInfo.minStopTime, audioLoopInfo.maxStopTime);
		float targetVol = 0f;
		float targetPitch = Random.Range(audioLoopInfo.minStopPitch, audioLoopInfo.maxStopPitch);
		while (counter < 1f)
		{
			float num = Mathf.Lerp(startFadeVol, targetVol, counter);
			this.audioSource.volume = num;
			float num2 = Mathf.Lerp(startFadePitch, targetPitch, counter);
			this.audioSource.pitch = num2;
			counter += Time.deltaTime * fadeSpeed;
			yield return 0;
		}
		this.audioSource.volume = targetVol;
		this.audioSource.pitch = targetPitch;
		this.audioSource.Stop();
		this.stopRoutine = null;
		yield break;
	}

	// Token: 0x04000577 RID: 1399
	private AudioSource audioSource;

	// Token: 0x04000578 RID: 1400
	private AudioSource rewardSource;

	// Token: 0x04000579 RID: 1401
	public AudioLoopInfo audioLoopInfo;

	// Token: 0x0400057A RID: 1402
	public AudioClipInfo audioRewardInfo;

	// Token: 0x0400057B RID: 1403
	private Coroutine startRoutine;

	// Token: 0x0400057C RID: 1404
	private Coroutine stopRoutine;
}
