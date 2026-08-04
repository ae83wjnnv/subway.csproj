using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class DynamicBgMusic : MonoBehaviour
{
	// Token: 0x060002C4 RID: 708 RVA: 0x0000C08C File Offset: 0x0000A28C
	private void Awake()
	{
		this.masterSource = base.gameObject.AddComponent<AudioSource>();
		this.masterSource.loop = true;
		this.masterSource.clip = this.masterClip;
		this.masterSource.volume = this.masterSourceVolume;
		this.masterSource.Play();
		for (int i = 0; i < this.audioSources.Length; i++)
		{
			this.audioSources[i] = base.gameObject.AddComponent<AudioSource>();
			this.audioSources[i].clip = this.audioClips[Random.Range(0, this.audioClips.Length)];
			this.audioSources[i].loop = true;
			if (Random.Range(0, 1) == 0)
			{
				this.audioSources[i].volume = 0f;
			}
			else
			{
				this.audioSources[i].volume = this.masterSourceVolume;
			}
		}
		for (int j = 0; j < this.audioSources.Length; j++)
		{
			base.StartCoroutine(this.LoopFader(j));
		}
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0000C190 File Offset: 0x0000A390
	private AudioClip FindNotYetPlayingLoop()
	{
		AudioClip audioClip;
		bool flag;
		do
		{
			audioClip = this.audioClips[Random.Range(0, this.audioClips.Length)];
			flag = true;
			for (int i = 0; i < this.audioSources.Length; i++)
			{
				if (audioClip == this.audioSources[i].clip)
				{
					flag = false;
					break;
				}
			}
		}
		while (!flag);
		return audioClip;
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0000C1E5 File Offset: 0x0000A3E5
	private IEnumerator LoopFader(int audioSourceID)
	{
		for (;;)
		{
			this.audioSources[audioSourceID].clip = this.FindNotYetPlayingLoop();
			this.audioSources[audioSourceID].time = this.masterSource.time;
			this.audioSources[audioSourceID].Play();
			float counter2 = 0f;
			float startFade2 = this.audioSources[audioSourceID].volume;
			float fadeSpeed2 = 1f / Random.Range(this.minFadeTime, this.maxFadeTime);
			float targetVolume2 = Random.Range(this.minPlayingVolume, this.maxPlayingVolume);
			while (counter2 < 1f)
			{
				float num = Mathf.Lerp(startFade2, targetVolume2, counter2);
				this.audioSources[audioSourceID].volume = num;
				counter2 += Time.deltaTime * fadeSpeed2;
				yield return 0;
			}
			this.audioSources[audioSourceID].volume = targetVolume2;
			yield return new WaitForSeconds(Random.Range(this.minPlayingTime, this.maxPlayingTime));
			counter2 = 0f;
			startFade2 = this.audioSources[audioSourceID].volume;
			fadeSpeed2 = 1f / Random.Range(this.minFadeTime, this.maxFadeTime);
			targetVolume2 = 0f;
			while (counter2 < 1f)
			{
				float num2 = Mathf.Lerp(startFade2, targetVolume2, counter2);
				this.audioSources[audioSourceID].volume = num2;
				counter2 += Time.deltaTime * fadeSpeed2;
				yield return 0;
			}
			this.audioSources[audioSourceID].volume = targetVolume2;
			yield return new WaitForSeconds(Random.Range(this.minPauseTime, this.maxPauseTime));
		}
		yield break;
	}

	// Token: 0x04000201 RID: 513
	public AudioClip[] audioClips;

	// Token: 0x04000202 RID: 514
	public AudioClip masterClip;

	// Token: 0x04000203 RID: 515
	private AudioSource masterSource;

	// Token: 0x04000204 RID: 516
	private AudioSource[] audioSources = new AudioSource[5];

	// Token: 0x04000205 RID: 517
	public float minFadeTime = 8f;

	// Token: 0x04000206 RID: 518
	public float maxFadeTime = 16f;

	// Token: 0x04000207 RID: 519
	public float minPlayingTime = 4f;

	// Token: 0x04000208 RID: 520
	public float maxPlayingTime = 10f;

	// Token: 0x04000209 RID: 521
	public float minPauseTime;

	// Token: 0x0400020A RID: 522
	public float maxPauseTime = 4f;

	// Token: 0x0400020B RID: 523
	public float minPlayingVolume = 0.3f;

	// Token: 0x0400020C RID: 524
	public float maxPlayingVolume = 0.6f;

	// Token: 0x0400020D RID: 525
	public float masterSourceVolume = 0.2f;
}
