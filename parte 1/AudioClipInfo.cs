using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
[Serializable]
public class AudioClipInfo
{
	// Token: 0x0400007C RID: 124
	public AudioClip Clip;

	// Token: 0x0400007D RID: 125
	public float minPitch = 0.8f;

	// Token: 0x0400007E RID: 126
	public float maxPitch = 1.1f;

	// Token: 0x0400007F RID: 127
	public float minVolume = 0.5f;

	// Token: 0x04000080 RID: 128
	public float maxVolume = 0.7f;

	// Token: 0x04000081 RID: 129
	public AudioRolloffMode Rollof = AudioRolloffMode.Linear;
}
