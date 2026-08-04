using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
[Serializable]
public class AudioLoopInfo
{
	// Token: 0x04000086 RID: 134
	public AudioClip[] clips;

	// Token: 0x04000087 RID: 135
	public float minStartTime = 0.4f;

	// Token: 0x04000088 RID: 136
	public float maxStartTime = 3f;

	// Token: 0x04000089 RID: 137
	public float minStartPitch;

	// Token: 0x0400008A RID: 138
	public float maxStartPitch = 0.5f;

	// Token: 0x0400008B RID: 139
	public float minStartVolume;

	// Token: 0x0400008C RID: 140
	public float maxStartVolume;

	// Token: 0x0400008D RID: 141
	public float minTargetPitch = 0.8f;

	// Token: 0x0400008E RID: 142
	public float maxTargetPitch = 1.1f;

	// Token: 0x0400008F RID: 143
	public float minTargetVolume = 0.5f;

	// Token: 0x04000090 RID: 144
	public float maxTargatVolume = 0.7f;

	// Token: 0x04000091 RID: 145
	public float minStopTime = 0.4f;

	// Token: 0x04000092 RID: 146
	public float maxStopTime = 3f;

	// Token: 0x04000093 RID: 147
	public float minStopPitch;

	// Token: 0x04000094 RID: 148
	public float maxStopPitch = 0.5f;

	// Token: 0x04000095 RID: 149
	public AudioRolloffMode Rollof = AudioRolloffMode.Linear;
}
