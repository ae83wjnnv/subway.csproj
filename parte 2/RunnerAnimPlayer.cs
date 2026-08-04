using System;

// Token: 0x020000B9 RID: 185
public class RunnerAnimPlayer : AnimationSoundPlayer
{
	// Token: 0x0600055A RID: 1370 RVA: 0x00019C44 File Offset: 0x00017E44
	private void Awake()
	{
		this.game = Game.Instance;
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "run",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 6,
			clip = "run",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "run2",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 6,
			clip = "run2",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "run3",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 6,
			clip = "run3",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "run4_long",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 6,
			clip = "run4_long",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 10,
			clip = "run4_long",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 15,
			clip = "run4_long",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 20,
			clip = "run4_long",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 25,
			clip = "run4_long",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 30,
			clip = "run4_long",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 35,
			clip = "run4_long",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 40,
			clip = "run4_long",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 45,
			clip = "run4_long",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 50,
			clip = "run4_long",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 55,
			clip = "run4_long",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 60,
			clip = "run4_long",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 65,
			clip = "run4_long",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 70,
			clip = "run4_long",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 75,
			clip = "run4_long",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 80,
			clip = "run4_long",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 85,
			clip = "run4_long",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 90,
			clip = "run4_long",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 95,
			clip = "run4_long",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 100,
			clip = "run4_long",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 105,
			clip = "run4_long",
			Audio = this.run__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 110,
			clip = "run4_long",
			Audio = this.run__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "superRun",
			Audio = this.superRun__RightFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 10,
			clip = "superRun",
			Audio = this.superRun__LeftFoot
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "dodgeLeft",
			Audio = this.groundLeft
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "dodgeRight",
			Audio = this.groundRight
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 8,
			clip = "dodgeLeft",
			Audio = this.groundLeft
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 8,
			clip = "dodgeRight",
			Audio = this.groundRight
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "landing",
			Audio = this.landing
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "jump",
			Audio = this.jump,
			Callback = new KeyFrameAudio.ExtraKeyframeCall(this.PlayJumpSound)
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "jump_salto",
			Audio = this.jump,
			Callback = new KeyFrameAudio.ExtraKeyframeCall(this.PlayJumpSound)
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "jump2",
			Audio = this.jump,
			Callback = new KeyFrameAudio.ExtraKeyframeCall(this.PlayJumpSound)
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "jump3",
			Audio = this.jump,
			Callback = new KeyFrameAudio.ExtraKeyframeCall(this.PlayJumpSound)
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "jump",
			Audio = this.groundLeft
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "jump_salto",
			Audio = this.groundRight
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "jump2",
			Audio = this.groundLeft
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "jump3",
			Audio = this.groundLeft
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "roll",
			Audio = this.roll
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "slide_roll2",
			Audio = this.roll
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "roll",
			Audio = this.groundRight
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "slide_roll2",
			Audio = this.groundRight
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "h_landing",
			Audio = this.h_kick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "h_roll",
			Audio = this.h_roll
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 4,
			clip = "h_roll",
			Audio = this.h_miniKick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "h_jump",
			Audio = this.h_kick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 2,
			clip = "h_jump",
			Audio = this.h_long
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "h_jump2_kickflip",
			Audio = this.h_kick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 4,
			clip = "h_jump2_kickflip",
			Audio = this.h_long
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 21,
			clip = "h_jump2_kickflip",
			Audio = this.h_short
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "h_jump3_180",
			Audio = this.h_kick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 2,
			clip = "h_jump3_180",
			Audio = this.h_short
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 8,
			clip = "h_jump3_180",
			Audio = this.h_mid
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "h_jump4_360flip",
			Audio = this.h_kick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 2,
			clip = "h_jump4_360flip",
			Audio = this.h_short
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 8,
			clip = "h_jump4_360flip",
			Audio = this.h_short
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 13,
			clip = "h_jump4_360flip",
			Audio = this.h_miniKick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 19,
			clip = "h_jump4_360flip",
			Audio = this.h_mid
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "h_jump5_Impossible",
			Audio = this.h_kick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 2,
			clip = "h_jump5_Impossible",
			Audio = this.h_mid
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 8,
			clip = "h_jump5_Impossible",
			Audio = this.h_short
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 11,
			clip = "h_jump5_Impossible",
			Audio = this.h_miniKick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 22,
			clip = "h_jump5_Impossible",
			Audio = this.h_mid
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "h_jump6_nollie",
			Audio = this.h_kick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 2,
			clip = "h_jump6_nollie",
			Audio = this.h_long
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 2,
			clip = "h_jump6_nollie",
			Audio = this.h_short
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 21,
			clip = "h_jump6_nollie",
			Audio = this.h_mid
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 3,
			clip = "h_jump7_heelflip",
			Audio = this.h_kick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 2,
			clip = "h_jump7_heelflip",
			Audio = this.h_mid
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 8,
			clip = "h_jump7_heelflip",
			Audio = this.h_short
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 13,
			clip = "h_jump7_heelflip",
			Audio = this.h_miniKick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 18,
			clip = "h_jump7_heelflip",
			Audio = this.h_mid
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "h_jump8_pop shuvit",
			Audio = this.h_kick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 2,
			clip = "h_jump8_pop shuvit",
			Audio = this.h_short
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 4,
			clip = "h_jump8_pop shuvit",
			Audio = this.h_miniKick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 10,
			clip = "h_jump8_pop shuvit",
			Audio = this.h_mid
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 2,
			clip = "h_jump9_fs360",
			Audio = this.h_kick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 3,
			clip = "h_jump9_fs360",
			Audio = this.h_short
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 10,
			clip = "h_jump9_fs360",
			Audio = this.h_short
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 20,
			clip = "h_jump9_fs360",
			Audio = this.h_mid
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			clip = "h_jump10_heel360",
			Audio = this.h_miniKick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 4,
			clip = "h_jump10_heel360",
			Audio = this.h_kick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 5,
			clip = "h_jump10_heel360",
			Audio = this.h_short
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 8,
			clip = "h_jump10_heel360",
			Audio = this.h_mid
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 11,
			clip = "h_jump10_heel360",
			Audio = this.h_short
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 14,
			clip = "h_jump10_heel360",
			Audio = this.h_miniKick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 20,
			clip = "h_jump10_heel360",
			Audio = this.h_mid
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "h_jump11_fs salto",
			Audio = this.h_kick
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 2,
			clip = "h_jump11_fs salto",
			Audio = this.h_long
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 8,
			clip = "h_jump11_fs salto",
			Audio = this.h_short
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "landing_grind1",
			Audio = this.h_landingGrind
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "landing_grind2",
			Audio = this.h_landingGrind
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "landing_grind3",
			Audio = this.h_landingGrind
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 1,
			clip = "idlePaint",
			Audio = this.idlePaintSpray,
			Callback = new KeyFrameAudio.ExtraKeyframeCall(this.PlayIdlePaintSound)
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 28,
			clip = "idlePaint",
			Audio = this.idlePaintShake,
			Callback = new KeyFrameAudio.ExtraKeyframeCall(this.PlayIdlePaintSound)
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 100,
			clip = "idlePaint",
			Audio = this.idlePaintShake,
			Callback = new KeyFrameAudio.ExtraKeyframeCall(this.PlayIdlePaintSound)
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 150,
			clip = "idlePaint",
			Audio = this.idlePaintSprayHigh,
			Callback = new KeyFrameAudio.ExtraKeyframeCall(this.PlayIdlePaintSound)
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 200,
			clip = "idlePaint",
			Audio = this.idlePaintSpray,
			Callback = new KeyFrameAudio.ExtraKeyframeCall(this.PlayIdlePaintSound)
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 237,
			clip = "idlePaint",
			Audio = this.idlePaintSpray,
			Callback = new KeyFrameAudio.ExtraKeyframeCall(this.PlayIdlePaintSound)
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 260,
			clip = "idlePaint",
			Audio = this.idlePaintShake,
			Callback = new KeyFrameAudio.ExtraKeyframeCall(this.PlayIdlePaintSound)
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 310,
			clip = "idlePaint",
			Audio = this.idlePaintSpray,
			Callback = new KeyFrameAudio.ExtraKeyframeCall(this.PlayIdlePaintSound)
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 350,
			clip = "idlePaint",
			Audio = this.idlePaintSpray,
			Callback = new KeyFrameAudio.ExtraKeyframeCall(this.PlayIdlePaintSound)
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 13,
			clip = "death_movingTrain",
			Audio = this.deathMovingTrain
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 8,
			clip = "death_lower",
			Audio = this.deathBodyfall
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 11,
			clip = "death_bounce",
			Audio = this.deathBodyfall
		});
		this.AudioClips.Add(new KeyFrameAudio
		{
			KeyFrame = 2,
			clip = "death_upper",
			Audio = this.deathBodyfall
		});
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0001B13D File Offset: 0x0001933D
	public void PlayOrMutePaintSound(bool doPlay)
	{
		this.playPaintSound = doPlay;
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0001B146 File Offset: 0x00019346
	public void PlayIdlePaintSound(KeyFrameAudio info)
	{
		if (this.playPaintSound)
		{
			So.Instance.playSound(info.Audio);
		}
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0001B161 File Offset: 0x00019361
	public void PlayJumpSound(KeyFrameAudio info)
	{
		if (this.game.HasSuperSneakers)
		{
			So.Instance.playSound(this.superJump);
			return;
		}
		So.Instance.playSound(info.Audio);
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0001B194 File Offset: 0x00019394
	public override void PlayKeyframeAnimation(int soundIndex)
	{
		KeyFrameAudio keyFrameAudio = this.AudioClips[soundIndex];
		if (keyFrameAudio.Callback != null)
		{
			keyFrameAudio.Callback(keyFrameAudio);
			return;
		}
		So.Instance.playSound(this.AudioClips[soundIndex].Audio);
	}

	// Token: 0x0400047E RID: 1150
	private Game game;

	// Token: 0x0400047F RID: 1151
	public AudioClipInfo run__LeftFoot;

	// Token: 0x04000480 RID: 1152
	public AudioClipInfo run__RightFoot;

	// Token: 0x04000481 RID: 1153
	public AudioClipInfo superRun__LeftFoot;

	// Token: 0x04000482 RID: 1154
	public AudioClipInfo superRun__RightFoot;

	// Token: 0x04000483 RID: 1155
	public AudioClipInfo groundLeft;

	// Token: 0x04000484 RID: 1156
	public AudioClipInfo groundRight;

	// Token: 0x04000485 RID: 1157
	public AudioClipInfo groundLeft_super;

	// Token: 0x04000486 RID: 1158
	public AudioClipInfo groundRight_super;

	// Token: 0x04000487 RID: 1159
	public AudioClipInfo jump;

	// Token: 0x04000488 RID: 1160
	public AudioClipInfo roll;

	// Token: 0x04000489 RID: 1161
	public AudioClipInfo landing;

	// Token: 0x0400048A RID: 1162
	public AudioClipInfo h_jump;

	// Token: 0x0400048B RID: 1163
	public AudioClipInfo h_roll;

	// Token: 0x0400048C RID: 1164
	public AudioClipInfo h_kick;

	// Token: 0x0400048D RID: 1165
	public AudioClipInfo h_miniKick;

	// Token: 0x0400048E RID: 1166
	public AudioClipInfo h_long;

	// Token: 0x0400048F RID: 1167
	public AudioClipInfo h_mid;

	// Token: 0x04000490 RID: 1168
	public AudioClipInfo h_short;

	// Token: 0x04000491 RID: 1169
	public AudioClipInfo h_landingGrind;

	// Token: 0x04000492 RID: 1170
	public AudioClipInfo superJump;

	// Token: 0x04000493 RID: 1171
	public AudioClipInfo idlePaintSpray;

	// Token: 0x04000494 RID: 1172
	public AudioClipInfo idlePaintSprayHigh;

	// Token: 0x04000495 RID: 1173
	public AudioClipInfo idlePaintShake;

	// Token: 0x04000496 RID: 1174
	public AudioClipInfo deathMovingTrain;

	// Token: 0x04000497 RID: 1175
	public AudioClipInfo deathBodyfall;

	// Token: 0x04000498 RID: 1176
	public AudioStateLoop audioStateLoop;

	// Token: 0x04000499 RID: 1177
	public bool playPaintSound = true;
}
