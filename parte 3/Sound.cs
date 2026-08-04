using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class Sound
{
	// Token: 0x17000093 RID: 147
	// (set) Token: 0x060005FA RID: 1530 RVA: 0x0001E106 File Offset: 0x0001C306
	public bool loop
	{
		set
		{
			this.audioSource.loop = value;
		}
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x0001E114 File Offset: 0x0001C314
	public Sound(So manager)
	{
		this._manager = manager;
		this.gameObject = new GameObject();
		this.gameObject.name = "empty";
		this.gameObject.transform.parent = manager.transform;
		this.audioSource = this.gameObject.AddComponent<AudioSource>();
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0001E177 File Offset: 0x0001C377
	public void destroySelf()
	{
		this._manager.removeSound(this);
		Object.Destroy(this.gameObject);
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0001E190 File Offset: 0x0001C390
	public void stop()
	{
		this.audioSource.Stop();
		this.destroySelf();
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0001E1A3 File Offset: 0x0001C3A3
	public IEnumerator fadeOutAndStop(float duration)
	{
		return this.audioSource.fadeOut(duration, delegate
		{
			this.stop();
		});
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x0001E1C0 File Offset: 0x0001C3C0
	public IEnumerator playAudioClip(AudioClip audioClip, AudioRolloffMode rolloff, float minVolume, float maxVolume, float minPitch, float maxPitch, Vector3 position)
	{
		this.gameObject.name = audioClip.name;
		this.audioSource.clip = audioClip;
		this.audioSource.volume = Random.Range(minVolume, maxVolume);
		this.audioSource.pitch = Random.Range(minPitch, maxPitch);
		return this.play(rolloff, minVolume, maxVolume, minPitch, maxPitch, position);
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0001E221 File Offset: 0x0001C421
	public IEnumerator play(AudioRolloffMode rolloff, float minVolume, float maxVolume, float minPitch, float maxPitch, Vector3 position)
	{
		this.available = false;
		this.gameObject.transform.position = position;
		this.audioSource.rolloffMode = rolloff;
		this.audioSource.volume = Random.Range(minVolume, maxVolume);
		this.audioSource.pitch = Random.Range(minPitch, maxPitch);
		this.audioSource.GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(this.audioSource.clip.length + 0.1f);
		if (this.destroyAfterPlay)
		{
			this.destroySelf();
		}
		this.available = true;
		yield break;
	}

	// Token: 0x040004F8 RID: 1272
	private So _manager;

	// Token: 0x040004F9 RID: 1273
	public AudioSource audioSource;

	// Token: 0x040004FA RID: 1274
	public GameObject gameObject;

	// Token: 0x040004FB RID: 1275
	public bool available = true;

	// Token: 0x040004FC RID: 1276
	public bool destroyAfterPlay;
}
