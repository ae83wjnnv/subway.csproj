using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class GameStats
{
	// Token: 0x17000035 RID: 53
	// (get) Token: 0x0600037D RID: 893 RVA: 0x00010690 File Offset: 0x0000E890
	// (set) Token: 0x0600037E RID: 894 RVA: 0x00010698 File Offset: 0x0000E898
	public int coins
	{
		get
		{
			return this._coins;
		}
		set
		{
			this._coins = value;
			Action onCoinsChanged = this.OnCoinsChanged;
			if (onCoinsChanged != null)
			{
				onCoinsChanged();
			}
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.EarnCoin, 1);
			}
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x0600037F RID: 895 RVA: 0x000106CB File Offset: 0x0000E8CB
	// (set) Token: 0x06000380 RID: 896 RVA: 0x000106D3 File Offset: 0x0000E8D3
	public int coinsCoinMagnet
	{
		get
		{
			return this._coinsCoinMagnet;
		}
		set
		{
			this._coinsCoinMagnet = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.CoinsWithMagnet, 1);
			}
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000381 RID: 897 RVA: 0x000106EC File Offset: 0x0000E8EC
	public int score
	{
		get
		{
			return this._score;
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x06000382 RID: 898 RVA: 0x000106F4 File Offset: 0x0000E8F4
	// (set) Token: 0x06000383 RID: 899 RVA: 0x000106FC File Offset: 0x0000E8FC
	public int jumps
	{
		get
		{
			return this._jumps;
		}
		set
		{
			this._jumps = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Jump, 1);
			}
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000384 RID: 900 RVA: 0x00010714 File Offset: 0x0000E914
	// (set) Token: 0x06000385 RID: 901 RVA: 0x0001071C File Offset: 0x0000E91C
	public int jumpsOverTrains
	{
		get
		{
			return this._jumpsOverTrains;
		}
		set
		{
			this._jumpsOverTrains = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.JumpTrain, 1);
			}
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000386 RID: 902 RVA: 0x00010734 File Offset: 0x0000E934
	// (set) Token: 0x06000387 RID: 903 RVA: 0x0001073C File Offset: 0x0000E93C
	public int rolls
	{
		get
		{
			return this._rolls;
		}
		set
		{
			this._rolls = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Roll, 1);
			}
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000388 RID: 904 RVA: 0x00010754 File Offset: 0x0000E954
	// (set) Token: 0x06000389 RID: 905 RVA: 0x0001075C File Offset: 0x0000E95C
	public int rollsLeftTrack
	{
		get
		{
			return this._rollsLeftTrack;
		}
		set
		{
			this._rollsLeftTrack = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.RollLeft, 1);
			}
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x0600038A RID: 906 RVA: 0x00010774 File Offset: 0x0000E974
	// (set) Token: 0x0600038B RID: 907 RVA: 0x0001077C File Offset: 0x0000E97C
	public int rollsCenterTrack
	{
		get
		{
			return this._rollsCenterTrack;
		}
		set
		{
			this._rollsCenterTrack = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.RollCenter, 1);
			}
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x0600038C RID: 908 RVA: 0x00010794 File Offset: 0x0000E994
	// (set) Token: 0x0600038D RID: 909 RVA: 0x0001079C File Offset: 0x0000E99C
	public int rollsRightTrack
	{
		get
		{
			return this._rollsRightTrack;
		}
		set
		{
			this._rollsRightTrack = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.RollRight, 1);
			}
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x0600038E RID: 910 RVA: 0x000107B5 File Offset: 0x0000E9B5
	// (set) Token: 0x0600038F RID: 911 RVA: 0x000107BD File Offset: 0x0000E9BD
	public int dodgeBarrier
	{
		get
		{
			return this._dodgeBarrier;
		}
		set
		{
			this._dodgeBarrier = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.RollUnderBarriers, 1);
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.DodgeBarriers, 1);
			}
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000390 RID: 912 RVA: 0x000107E3 File Offset: 0x0000E9E3
	// (set) Token: 0x06000391 RID: 913 RVA: 0x000107EB File Offset: 0x0000E9EB
	public int jumpBarrier
	{
		get
		{
			return this._jumpBarrier;
		}
		set
		{
			this._jumpBarrier = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.JumpBarriers, 1);
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.DodgeBarriers, 1);
			}
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000392 RID: 914 RVA: 0x00010811 File Offset: 0x0000EA11
	// (set) Token: 0x06000393 RID: 915 RVA: 0x00010819 File Offset: 0x0000EA19
	public int trainHit
	{
		get
		{
			return this._trainHit;
		}
		set
		{
			this._trainHit = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.CrashTrains, 1);
			}
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000394 RID: 916 RVA: 0x00010832 File Offset: 0x0000EA32
	// (set) Token: 0x06000395 RID: 917 RVA: 0x0001083A File Offset: 0x0000EA3A
	public int movingTrainHit
	{
		get
		{
			return this._movingTrainHit;
		}
		set
		{
			this._movingTrainHit = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.DieToTrain, 1);
			}
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000396 RID: 918 RVA: 0x00010853 File Offset: 0x0000EA53
	// (set) Token: 0x06000397 RID: 919 RVA: 0x0001085B File Offset: 0x0000EA5B
	public int barrierHit
	{
		get
		{
			return this._barrierHit;
		}
		set
		{
			this._barrierHit = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.CrashBarriers, 1);
			}
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000398 RID: 920 RVA: 0x00010874 File Offset: 0x0000EA74
	// (set) Token: 0x06000399 RID: 921 RVA: 0x0001087C File Offset: 0x0000EA7C
	public int jetpackPickups
	{
		get
		{
			return this._jetpackPickups;
		}
		set
		{
			this._jetpackPickups = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Jetpack, 1);
			}
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x0600039A RID: 922 RVA: 0x00010895 File Offset: 0x0000EA95
	// (set) Token: 0x0600039B RID: 923 RVA: 0x0001089D File Offset: 0x0000EA9D
	public int superSneakerPickups
	{
		get
		{
			return this._superSneakerPickups;
		}
		set
		{
			this._superSneakerPickups = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.SuperSneakers, 1);
			}
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x0600039C RID: 924 RVA: 0x000108B6 File Offset: 0x0000EAB6
	// (set) Token: 0x0600039D RID: 925 RVA: 0x000108BE File Offset: 0x0000EABE
	public int letterPickups
	{
		get
		{
			return this._letterPickups;
		}
		set
		{
			this._letterPickups = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Letters, 1);
			}
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x0600039E RID: 926 RVA: 0x000108D7 File Offset: 0x0000EAD7
	// (set) Token: 0x0600039F RID: 927 RVA: 0x000108DF File Offset: 0x0000EADF
	public int coinMagnetsPickups
	{
		get
		{
			return this._coinMagnetsPickups;
		}
		set
		{
			this._coinMagnetsPickups = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Magnets, 1);
			}
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x060003A0 RID: 928 RVA: 0x000108F8 File Offset: 0x0000EAF8
	// (set) Token: 0x060003A1 RID: 929 RVA: 0x00010900 File Offset: 0x0000EB00
	public int mysteryBoxPickups
	{
		get
		{
			return this._mysteryBoxPickups;
		}
		set
		{
			this._mysteryBoxPickups = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.MysteryBoxes, 1);
			}
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x060003A2 RID: 930 RVA: 0x00010919 File Offset: 0x0000EB19
	// (set) Token: 0x060003A3 RID: 931 RVA: 0x00010921 File Offset: 0x0000EB21
	public int usePowerups
	{
		get
		{
			return this._usePowerups;
		}
		set
		{
			this._usePowerups = value;
			if (value != 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Powerups, 1);
			}
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060003A4 RID: 932 RVA: 0x0001093A File Offset: 0x0000EB3A
	public static GameStats Instance
	{
		get
		{
			GameStats gameStats;
			if ((gameStats = GameStats.instance) == null)
			{
				gameStats = (GameStats.instance = new GameStats());
			}
			return gameStats;
		}
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00010950 File Offset: 0x0000EB50
	public static int CoinToScoreConversion(int coins)
	{
		return coins * 2 * PlayerInfo.Instance.rawMultiplier;
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00010960 File Offset: 0x0000EB60
	public void CalculateScore()
	{
		if (this._metersLastUsedForScore < this.meters)
		{
			this._meterScore = this.meters - this._metersLastUsedForScore;
			this._metersLastUsedForScore = this.meters;
			int num = (int)(this._meterScore * (float)PlayerInfo.Instance.scoreMultiplier);
			this._score += num;
			Missions.Instance.PlayerDidThis(Missions.MissionTarget.Score, num);
			if (this.coins <= 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.NoCoinsBeforeScore, num);
			}
		}
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x000109E0 File Offset: 0x0000EBE0
	public void AddScoreForPickup(PowerupType type)
	{
		if (type - PowerupType.mysterybox <= 5)
		{
			int num = PlayerInfo.Instance.scoreMultiplier * 50;
			this._score += num;
			Missions.Instance.PlayerDidThis(Missions.MissionTarget.Score, num);
			if (this.coins <= 0)
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.NoCoinsBeforeScore, num);
			}
		}
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00010A31 File Offset: 0x0000EC31
	public void ResetScore()
	{
		this._score = 0;
		this._metersLastUsedForScore = 0f;
		this._meterScore = 0f;
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x00010A50 File Offset: 0x0000EC50
	public ActivePowerup TriggerPowerup(PowerupType type)
	{
		ActivePowerup activePowerup = new ActivePowerup();
		activePowerup.type = type;
		activePowerup.timeActivated = Time.time;
		activePowerup.timeLeft = PlayerInfo.Instance.GetPowerupDuration(type);
		if (type == PowerupType.headstart2000 || type == PowerupType.headstart500)
		{
			activePowerup.timeLeft = 0f;
		}
		for (int i = this._listOfActivePowerups.Count - 1; i >= 0; i--)
		{
			if (this._listOfActivePowerups[i].type == activePowerup.type)
			{
				this._listOfActivePowerups.RemoveAt(i);
				Debug.Log("Removing existing powerup: " + type.ToString());
			}
		}
		this.AddScoreForPickup(type);
		this._listOfActivePowerups.Add(activePowerup);
		return activePowerup;
	}

	// Token: 0x060003AA RID: 938 RVA: 0x00010B06 File Offset: 0x0000ED06
	public List<ActivePowerup> GetActivePowerups()
	{
		return this._listOfActivePowerups;
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00010B10 File Offset: 0x0000ED10
	public void UpdatePowerupTimes(float deltaTime)
	{
		for (int i = this._listOfActivePowerups.Count - 1; i >= 0; i--)
		{
			if (!Game.Instance.IsInJetpackMode || (this._listOfActivePowerups[i].type != PowerupType.hoverboard && this._listOfActivePowerups[i].type != PowerupType.supersneakers))
			{
				this._listOfActivePowerups[i].timeLeft -= deltaTime;
				if (this._listOfActivePowerups[i].timeLeft < 0f && (!Game.Instance.IsInJetpackMode || this._listOfActivePowerups[i].type != PowerupType.jetpack))
				{
					if (this._listOfActivePowerups[i].type == PowerupType.hoverboard)
					{
						float num = Hoverboard.Instance.WaitForParticlesDelay + PlayerInfo.Instance.GetHoverBoardCoolDown();
						if (this._listOfActivePowerups[i].timeLeft > 0f - num)
						{
							goto IL_00EE;
						}
					}
					this._listOfActivePowerups.RemoveAt(i);
				}
			}
			IL_00EE:;
		}
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00010C16 File Offset: 0x0000EE16
	public void ClearPowerups()
	{
		this._listOfActivePowerups.Clear();
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00010C24 File Offset: 0x0000EE24
	public void RemoveHoverBoardPowerup()
	{
		for (int i = this._listOfActivePowerups.Count - 1; i >= 0; i--)
		{
			if (this._listOfActivePowerups[i].type == PowerupType.hoverboard)
			{
				this._listOfActivePowerups[i].timeLeft = 0f;
			}
		}
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00010C74 File Offset: 0x0000EE74
	public void Reset()
	{
		this.duration = 0f;
		this.ResetScore();
		this.coins = 0;
		this.coinsCoinMagnet = 0;
		this.meters = 0f;
		this.metersRunLeftTrack = 0f;
		this.metersRunCenterTrack = 0f;
		this.metersRunRightTrack = 0f;
		this.metersRunGround = 0f;
		this.metersRunTrain = 0f;
		this.metersRunStation = 0f;
		this.metersFly = 0f;
		this.jumps = 0;
		this.jumpsOverTrains = 0;
		this.rolls = 0;
		this.rollsLeftTrack = 0;
		this.rollsCenterTrack = 0;
		this.rollsRightTrack = 0;
		this.trackChanges = 0;
		this.dodgeBarrier = 0;
		this.jumpBarrier = 0;
		this.trainHit = 0;
		this.barrierHit = 0;
		this.jetpackPickups = 0;
		this.superSneakerPickups = 0;
		this.letterPickups = 0;
		this.coinMagnetsPickups = 0;
		this.mysteryBoxPickups = 0;
		this.usePowerups = 0;
		this.doubleMultiplierPickups = 0;
		this.coinsSummerized = new List<KeyValuePair<int, int>>();
	}

	// Token: 0x040002DC RID: 732
	public float duration;

	// Token: 0x040002DD RID: 733
	public Action OnCoinsChanged;

	// Token: 0x040002DE RID: 734
	private int _coins;

	// Token: 0x040002DF RID: 735
	private int _coinsCoinMagnet;

	// Token: 0x040002E0 RID: 736
	private int _score;

	// Token: 0x040002E1 RID: 737
	private float _metersLastUsedForScore;

	// Token: 0x040002E2 RID: 738
	private float _meterScore;

	// Token: 0x040002E3 RID: 739
	private List<ActivePowerup> _listOfActivePowerups = new List<ActivePowerup>();

	// Token: 0x040002E4 RID: 740
	public float meters;

	// Token: 0x040002E5 RID: 741
	public float metersRunLeftTrack;

	// Token: 0x040002E6 RID: 742
	public float metersRunCenterTrack;

	// Token: 0x040002E7 RID: 743
	public float metersRunRightTrack;

	// Token: 0x040002E8 RID: 744
	public float metersFly;

	// Token: 0x040002E9 RID: 745
	public float metersRunGround;

	// Token: 0x040002EA RID: 746
	public float metersRunTrain;

	// Token: 0x040002EB RID: 747
	public float metersRunStation;

	// Token: 0x040002EC RID: 748
	private int _jumps;

	// Token: 0x040002ED RID: 749
	private int _jumpsOverTrains;

	// Token: 0x040002EE RID: 750
	private int _rolls;

	// Token: 0x040002EF RID: 751
	private int _rollsLeftTrack;

	// Token: 0x040002F0 RID: 752
	private int _rollsCenterTrack;

	// Token: 0x040002F1 RID: 753
	private int _rollsRightTrack;

	// Token: 0x040002F2 RID: 754
	public int trackChanges;

	// Token: 0x040002F3 RID: 755
	private int _dodgeBarrier;

	// Token: 0x040002F4 RID: 756
	private int _jumpBarrier;

	// Token: 0x040002F5 RID: 757
	private int _trainHit;

	// Token: 0x040002F6 RID: 758
	private int _movingTrainHit;

	// Token: 0x040002F7 RID: 759
	private int _barrierHit;

	// Token: 0x040002F8 RID: 760
	private int _jetpackPickups;

	// Token: 0x040002F9 RID: 761
	private int _superSneakerPickups;

	// Token: 0x040002FA RID: 762
	private int _letterPickups;

	// Token: 0x040002FB RID: 763
	private int _coinMagnetsPickups;

	// Token: 0x040002FC RID: 764
	private int _mysteryBoxPickups;

	// Token: 0x040002FD RID: 765
	public int doubleMultiplierPickups;

	// Token: 0x040002FE RID: 766
	private int _usePowerups;

	// Token: 0x040002FF RID: 767
	public GameStats.CoinsChangedIngame OnChoinsChangedIngame;

	// Token: 0x04000300 RID: 768
	public List<KeyValuePair<int, int>> coinsSummerized = new List<KeyValuePair<int, int>>();

	// Token: 0x04000301 RID: 769
	private static GameStats instance;

	// Token: 0x020001A8 RID: 424
	// (Invoke) Token: 0x06000B1A RID: 2842
	public delegate void CoinsChangedIngame();
}
