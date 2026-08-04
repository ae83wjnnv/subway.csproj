using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class So : MonoBehaviour
{
	// Token: 0x06000597 RID: 1431 RVA: 0x0001BDD4 File Offset: 0x00019FD4
	private void Awake()
	{
		if (So.Instance != null)
		{
			Object.Destroy(this);
			return;
		}
		So.Instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
		this._soundList = new List<Sound>(this.initialCapacity);
		for (int i = 0; i < this.initialCapacity; i++)
		{
			this._soundList.Add(new Sound(this));
		}
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0001BE3C File Offset: 0x0001A03C
	private void OnApplicationPause(bool didPause)
	{
		if (didPause)
		{
			if (this._bgSound != null)
			{
				this._audioWasPlaying = true;
				this._audioTime = this._bgSound.audioSource.time;
				return;
			}
		}
		else if (this._audioWasPlaying)
		{
			this._audioWasPlaying = false;
			this._bgSound.audioSource.time = this._audioTime;
			this._bgSound.audioSource.Play();
		}
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0001BEA7 File Offset: 0x0001A0A7
	public void playBGMusic(AudioClip audioClip, float volume, bool loop)
	{
		if (this._bgSound == null)
		{
			this._bgSound = new Sound(this);
		}
		this._bgSound.loop = loop;
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x0001BEC9 File Offset: 0x0001A0C9
	public void beginPlaySound(AudioClipInfo audioClip)
	{
		base.StartCoroutine(this.playSoundAsync(audioClip));
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x0001BED9 File Offset: 0x0001A0D9
	private IEnumerator playSoundAsync(AudioClipInfo audioClip)
	{
		yield return 0;
		this.playSound(audioClip);
		yield break;
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
	public Sound playSound(AudioClipInfo audioClip)
	{
		if (audioClip.Clip == null)
		{
			return null;
		}
		return this.playSound(audioClip.Clip, audioClip.Rollof, audioClip.minVolume, audioClip.maxVolume, audioClip.minPitch, audioClip.maxPitch, base.transform.position);
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0001BF42 File Offset: 0x0001A142
	public Sound playSound(AudioClip audioClip, AudioRolloffMode rolloff, float volume, Vector3 position)
	{
		return this.playSound(audioClip, rolloff, volume, volume, 1f, 1f, position);
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0001BF5C File Offset: 0x0001A15C
	public Sound playSound(AudioClip audioClip, AudioRolloffMode rolloff, float minVolume, float maxVolume, float minPitch, float maxPitch, Vector3 position)
	{
		Sound sound = null;
		bool flag = false;
		bool flag2 = false;
		foreach (Sound sound2 in this._soundList)
		{
			if (sound2.available)
			{
				if (sound2.gameObject.name == audioClip.name)
				{
					sound = sound2;
					flag = true;
					break;
				}
				if (!flag2)
				{
					if (sound2.gameObject.name == "empty")
					{
						flag2 = true;
					}
					sound = sound2;
				}
			}
		}
		if (sound == null)
		{
			sound = this._soundList[0];
			this._soundList.Add(sound);
		}
		if (flag)
		{
			base.StartCoroutine(sound.play(rolloff, minVolume, maxVolume, minPitch, maxPitch, position));
		}
		else
		{
			base.StartCoroutine(sound.playAudioClip(audioClip, rolloff, minVolume, maxVolume, minPitch, maxPitch, position));
		}
		return sound;
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0001C04C File Offset: 0x0001A24C
	public void removeSound(Sound s)
	{
		this._soundList.Remove(s);
	}

	// Token: 0x040004CE RID: 1230
	public static So Instance;

	// Token: 0x040004CF RID: 1231
	public int initialCapacity = 5;

	// Token: 0x040004D0 RID: 1232
	public int maxCapacity = 50;

	// Token: 0x040004D1 RID: 1233
	private List<Sound> _soundList;

	// Token: 0x040004D2 RID: 1234
	private Sound _bgSound;

	// Token: 0x040004D3 RID: 1235
	private bool _audioWasPlaying;

	// Token: 0x040004D4 RID: 1236
	private float _audioTime;
}
