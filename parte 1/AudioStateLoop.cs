using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class AudioStateLoop : MonoBehaviour
{
	// Token: 0x0600015F RID: 351 RVA: 0x000055D8 File Offset: 0x000037D8
	private void Awake()
	{
		this.jetpackSource = base.gameObject.AddComponent<AudioSource>();
		this.jetpackSource.clip = this.jetpackLoop;
		this.jetpackSource.volume = this.jetpackVolume;
		this.jetpackSource.loop = true;
		this.jetpackSource.playOnAwake = false;
		this.magnetSource = base.gameObject.AddComponent<AudioSource>();
		this.magnetSource.clip = this.magnetLoop;
		this.magnetSource.volume = this.magnetVolume;
		this.magnetSource.loop = true;
		this.magnetSource.playOnAwake = false;
		this.mysterySource = base.gameObject.AddComponent<AudioSource>();
		this.mysterySource.clip = this.mysteryBoxOpenSound;
		this.mysterySource.volume = this.mysteryVolume;
		this.magnetSource.playOnAwake = false;
		this.musicPlayer.volume = this.ingameMusicVolume;
		this.musicPlayer.bypassEffects = true;
		this.musicPlayer.Play();
	}

	// Token: 0x06000160 RID: 352 RVA: 0x000056E2 File Offset: 0x000038E2
	public void PlayMysteryBoxOpenSound()
	{
		this.mysterySource.Play();
		base.StartCoroutine(this.MusicFader());
	}

	// Token: 0x06000161 RID: 353 RVA: 0x000056FC File Offset: 0x000038FC
	private IEnumerator MusicFader()
	{
		float counter2 = 0f;
		float startFade = this.musicPlayer.volume;
		float fadeSpeed2 = 1f / this.fadeDownTime;
		while (counter2 < 1f)
		{
			this.musicPlayer.volume = Mathf.Lerp(startFade, 0f, counter2);
			counter2 += Time.deltaTime * fadeSpeed2;
			yield return 0;
		}
		this.musicPlayer.volume = 0f;
		yield return new WaitForSeconds(this.pauseTime);
		counter2 = 0f;
		fadeSpeed2 = 1f / this.fadeUpTime;
		while (counter2 < 1f)
		{
			this.musicPlayer.volume = Mathf.Lerp(0f, this.menuMusicVolume, counter2);
			counter2 += Time.deltaTime * fadeSpeed2;
			yield return 0;
		}
		this.musicPlayer.volume = this.menuMusicVolume;
		yield break;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0000570C File Offset: 0x0000390C
	public void ChangeLoop(AudioState audioState)
	{
		switch (audioState)
		{
		case AudioState.Menu:
			if (this.hasPlayedIntro)
			{
				this.musicPlayer.bypassEffects = false;
				this.musicPlayer.volume = this.menuMusicVolume;
				return;
			}
			break;
		case AudioState.Ingame:
			if (this.hasPlayedIntro)
			{
				this.musicPlayer.timeSamples = 0;
			}
			else
			{
				this.hasPlayedIntro = true;
			}
			this.musicPlayer.bypassEffects = true;
			this.musicPlayer.volume = this.ingameMusicVolume;
			return;
		case AudioState.Jetpack:
			this.PlayLoop(this.jetpackSource, this.jetpackMaxPitch, this.jetpackMaxPitch);
			return;
		case AudioState.JetpackStop:
			this.StopLoop(this.jetpackSource);
			return;
		case AudioState.Magnet:
			this.PlayLoop(this.magnetSource, this.magnetMinPitch, this.magnetMaxPitch);
			return;
		case AudioState.MagnetStop:
			this.StopLoop(this.magnetSource);
			break;
		default:
			return;
		}
	}

	// Token: 0x06000163 RID: 355 RVA: 0x000057E7 File Offset: 0x000039E7
	public void PlayLoop(AudioSource audioSource)
	{
		audioSource.pitch = Random.Range(0.8f, 1.1f);
		audioSource.Play();
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00005804 File Offset: 0x00003A04
	public void PlayLoop(AudioSource audioSource, float minPitch, float maxPitch)
	{
		audioSource.pitch = Random.Range(minPitch, maxPitch);
		audioSource.Play();
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00005819 File Offset: 0x00003A19
	public void StopLoop(AudioSource audioSource)
	{
		audioSource.Stop();
	}

	// Token: 0x0400009D RID: 157
	public AudioSource musicPlayer;

	// Token: 0x0400009E RID: 158
	public float menuMusicVolume = 0.3f;

	// Token: 0x0400009F RID: 159
	public float ingameMusicVolume = 0.4f;

	// Token: 0x040000A0 RID: 160
	public AudioClip jetpackLoop;

	// Token: 0x040000A1 RID: 161
	public float jetpackVolume = 0.4f;

	// Token: 0x040000A2 RID: 162
	public float jetpackMinPitch = 0.8f;

	// Token: 0x040000A3 RID: 163
	public float jetpackMaxPitch = 1.1f;

	// Token: 0x040000A4 RID: 164
	public AudioClip magnetLoop;

	// Token: 0x040000A5 RID: 165
	public float magnetVolume = 0.4f;

	// Token: 0x040000A6 RID: 166
	public float magnetMinPitch = 0.8f;

	// Token: 0x040000A7 RID: 167
	public float magnetMaxPitch = 1.1f;

	// Token: 0x040000A8 RID: 168
	public AudioClip mysteryBoxOpenSound;

	// Token: 0x040000A9 RID: 169
	public float mysteryVolume = 1f;

	// Token: 0x040000AA RID: 170
	private AudioSource jetpackSource;

	// Token: 0x040000AB RID: 171
	private AudioSource magnetSource;

	// Token: 0x040000AC RID: 172
	private AudioSource mysterySource;

	// Token: 0x040000AD RID: 173
	private bool hasPlayedIntro;

	// Token: 0x040000AE RID: 174
	public float fadeDownTime = 0.5f;

	// Token: 0x040000AF RID: 175
	public float pauseTime = 3f;

	// Token: 0x040000B0 RID: 176
	public float fadeUpTime = 4f;
}
