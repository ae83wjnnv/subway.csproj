using System;

// Token: 0x02000085 RID: 133
[Serializable]
public class KeyFrameAudio : KeyFrameAction
{
	// Token: 0x0400038A RID: 906
	public AudioKeyFrameType audioKeyFrameType;

	// Token: 0x0400038B RID: 907
	public AudioClipInfo Audio;

	// Token: 0x0400038C RID: 908
	public KeyFrameAudio.ExtraKeyframeCall Callback;

	// Token: 0x020001B4 RID: 436
	// (Invoke) Token: 0x06000B3B RID: 2875
	public delegate void ExtraKeyframeCall(KeyFrameAudio info);
}
