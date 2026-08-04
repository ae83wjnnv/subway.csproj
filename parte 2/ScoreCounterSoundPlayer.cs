using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000BC RID: 188
public class ScoreCounterSoundPlayer : MonoBehaviour
{
	// Token: 0x0600056E RID: 1390 RVA: 0x0001B7BC File Offset: 0x000199BC
	private void Awake()
	{
		this.scoreSource = base.gameObject.AddComponent<AudioSource>();
		this.scoreSource.volume = this.volume;
		this.scoreSource.clip = this.scoreSound;
		this.scoreSource.playOnAwake = false;
		this.scoreSource.spatialBlend = 0f;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0001B818 File Offset: 0x00019A18
	public void PlayCoinSound(float countFactor)
	{
		this.count = countFactor;
		if (!this.playScore)
		{
			this.playScore = true;
			base.StartCoroutine(this.ScoreSoundTimer());
		}
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x0001B83D File Offset: 0x00019A3D
	private IEnumerator ScoreSoundTimer()
	{
		while (this.playScore)
		{
			this.scoreSource.pitch = Mathf.Pow(2f, this.count);
			this.scoreSource.Play();
			yield return new WaitForSeconds(this.stepDelay);
		}
		this.scoreSource.pitch = 2f;
		this.scoreSource.Play();
		yield break;
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x0001B84C File Offset: 0x00019A4C
	public void StopScoreSound()
	{
		this.playScore = false;
	}

	// Token: 0x040004B4 RID: 1204
	public AudioClip scoreSound;

	// Token: 0x040004B5 RID: 1205
	public float volume = 0.3f;

	// Token: 0x040004B6 RID: 1206
	public float stepDelay = 0.0625f;

	// Token: 0x040004B7 RID: 1207
	private float count;

	// Token: 0x040004B8 RID: 1208
	private AudioSource scoreSource;

	// Token: 0x040004B9 RID: 1209
	private bool playScore;
}
