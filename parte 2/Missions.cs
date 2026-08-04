using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000093 RID: 147
public class Missions
{
	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06000454 RID: 1108 RVA: 0x000130F8 File Offset: 0x000112F8
	private MissionTemplate[] currentMissionTemplates
	{
		get
		{
			int currentMissionSet = PlayerInfo.Instance.currentMissionSet;
			if (this._currentMissionSetLoaded != currentMissionSet)
			{
				Mission[] array = Missions.missions[currentMissionSet];
				this._currentMissionTemplates = new MissionTemplate[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this._currentMissionTemplates[i] = Missions.missionTemplates[array[i].type];
				}
				this._currentMissionSetLoaded = currentMissionSet;
			}
			return this._currentMissionTemplates;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000455 RID: 1109 RVA: 0x0001316C File Offset: 0x0001136C
	// (set) Token: 0x06000456 RID: 1110 RVA: 0x00013178 File Offset: 0x00011378
	public bool inRun
	{
		get
		{
			return this._currentRunProgress != null;
		}
		set
		{
			if (value)
			{
				this._currentRunProgress = new int[Missions.missions[PlayerInfo.Instance.currentMissionSet].Length];
				return;
			}
			if (this._currentRunProgress == null)
			{
				return;
			}
			PlayerInfo instance = PlayerInfo.Instance;
			MissionTemplate[] currentMissionTemplates = this.currentMissionTemplates;
			Mission[] array = Missions.missions[instance.currentMissionSet];
			for (int i = 0; i < array.Length; i++)
			{
				if (currentMissionTemplates[i].singleRun && currentMissionTemplates[i].completeIfLess && this._currentRunProgress[i] < array[i].goal)
				{
					instance.SetCurrentMissionProgress(i, array[i].goal);
					this.MissionComplete(i);
				}
			}
			this._currentRunProgress = null;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000457 RID: 1111 RVA: 0x0001322A File Offset: 0x0001142A
	// (set) Token: 0x06000458 RID: 1112 RVA: 0x00013236 File Offset: 0x00011436
	public int currentMissionSet
	{
		get
		{
			return PlayerInfo.Instance.currentMissionSet;
		}
		set
		{
			if (value != PlayerInfo.Instance.currentMissionSet)
			{
				PlayerInfo.Instance.InitCurrentMissionSet(value, Missions.missions[value].Length);
			}
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000459 RID: 1113 RVA: 0x00013259 File Offset: 0x00011459
	public int missionSetCount
	{
		get
		{
			return Missions.missions.Length - 1;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x0600045A RID: 1114 RVA: 0x00013264 File Offset: 0x00011464
	public static Missions Instance
	{
		get
		{
			Missions missions;
			if ((missions = Missions._instance) == null)
			{
				missions = (Missions._instance = new Missions());
			}
			return missions;
		}
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x0001327A File Offset: 0x0001147A
	private Missions()
	{
		if (PlayerInfo.Instance.currentMissionSet == -1)
		{
			PlayerInfo.Instance.InitCurrentMissionSet(0, Missions.missions[0].Length);
		}
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x000132AA File Offset: 0x000114AA
	public void SkipMission(int missionNumber)
	{
		PlayerInfo.Instance.SetCurrentMissionProgress(missionNumber, Missions.missions[PlayerInfo.Instance.currentMissionSet][missionNumber].goal);
		this.MissionComplete(missionNumber);
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x000132DC File Offset: 0x000114DC
	private void MissionComplete(int mission)
	{
		PlayerInfo instance = PlayerInfo.Instance;
		int currentMissionSet = instance.currentMissionSet;
		MissionTemplate[] currentMissionTemplates = this.currentMissionTemplates;
		Mission[] array = Missions.missions[currentMissionSet];
		Missions.MissionCompleteHandler missionCompleteHandler = this.onMissionComplete;
		if (missionCompleteHandler != null)
		{
			if (array[mission].goal == 1)
			{
			}
			missionCompleteHandler(string.Format(currentMissionTemplates[mission].ultraShortDescription, array[mission].goal));
		}
		bool flag = true;
		for (int i = 0; i < this.currentMissionTemplates.Length; i++)
		{
			if (instance.GetCurrentMissionProgress(i) < array[i].goal)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			Flurry.LogEventWithAParameter("Mission Set completed", "Mission Set", instance.currentMissionSet.ToString());
			int num = instance.currentMissionSet + 1;
			int num2 = ((num >= this.missionSetCount) ? 3 : Missions.missions[num].Length);
			instance.InitCurrentMissionSet(num, num2);
			Missions.MissionSetCompleteHandler missionSetCompleteHandler = this.onMissionSetComplete;
			if (missionSetCompleteHandler != null)
			{
				missionSetCompleteHandler();
			}
		}
		instance.Save();
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x000133FC File Offset: 0x000115FC
	public void PlayerDidThis(Missions.MissionTarget myTask, int magnitude = 1)
	{
		if (!this.HasMoreMissions())
		{
			return;
		}
		PlayerInfo instance = PlayerInfo.Instance;
		int currentMissionSet = instance.currentMissionSet;
		MissionTemplate[] currentMissionTemplates = this.currentMissionTemplates;
		if (currentMissionTemplates == null)
		{
			Debug.LogError("currentTemplates == null");
		}
		Mission[] array = Missions.missions[currentMissionSet];
		int i = 0;
		while (i < currentMissionTemplates.Length)
		{
			if ((!currentMissionTemplates[i].singleRun || this.inRun) && currentMissionTemplates[i].missionTarget == myTask)
			{
				int num = instance.GetCurrentMissionProgress(i);
				if (currentMissionTemplates[i].singleRun && this.inRun && num < array[i].goal && this._currentRunProgress != null)
				{
					num = this._currentRunProgress[i];
				}
				int num2 = num + magnitude;
				if (currentMissionTemplates[i].completeIfLess)
				{
					if (num2 > array[i].goal)
					{
						if (!currentMissionTemplates[i].singleRun || !this.inRun)
						{
							instance.SetCurrentMissionProgress(i, num2);
							return;
						}
						if (this._currentRunProgress != null)
						{
							this._currentRunProgress[i] = num2;
							return;
						}
						break;
					}
					else
					{
						if (currentMissionTemplates[i].singleRun && this._currentRunProgress != null)
						{
							this._currentRunProgress[i] = num2;
						}
						instance.SetCurrentMissionProgress(i, array[i].goal);
						if (num > array[i].goal)
						{
							this.MissionComplete(i);
							return;
						}
						break;
					}
				}
				else if (num2 < array[i].goal)
				{
					if (!currentMissionTemplates[i].singleRun)
					{
						instance.SetCurrentMissionProgress(i, num2);
						return;
					}
					if (this._currentRunProgress != null)
					{
						this._currentRunProgress[i] = num2;
						return;
					}
					break;
				}
				else
				{
					instance.SetCurrentMissionProgress(i, array[i].goal);
					if (num < array[i].goal)
					{
						this.MissionComplete(i);
						return;
					}
					break;
				}
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x000135EC File Offset: 0x000117EC
	public MissionInfo[] GetMissionInfo()
	{
		PlayerInfo instance = PlayerInfo.Instance;
		int currentMissionSet = instance.currentMissionSet;
		if (currentMissionSet >= this.missionSetCount)
		{
			return new MissionInfo[0];
		}
		MissionTemplate[] currentMissionTemplates = this.currentMissionTemplates;
		Mission[] array = Missions.missions[currentMissionSet];
		MissionInfo[] array2 = new MissionInfo[currentMissionTemplates.Length];
		for (int i = 0; i < array2.Length; i++)
		{
			int num = instance.GetCurrentMissionProgress(i);
			bool flag = num >= array[i].goal;
			if (!flag && currentMissionTemplates[i].singleRun && this.inRun)
			{
				num = this._currentRunProgress[i];
			}
			else
			{
				Game.Instance.GetDuration();
			}
			array2[i] = new MissionInfo(array[i], currentMissionTemplates[i], num, flag);
		}
		return array2;
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x000136C0 File Offset: 0x000118C0
	public MissionInfo GetMissionInfo(int missonNumber)
	{
		PlayerInfo instance = PlayerInfo.Instance;
		int currentMissionSet = instance.currentMissionSet;
		MissionTemplate[] currentMissionTemplates = this.currentMissionTemplates;
		Mission[] array = Missions.missions[currentMissionSet];
		int num = instance.GetCurrentMissionProgress(missonNumber);
		bool flag = num >= array[missonNumber].goal;
		if (!flag && currentMissionTemplates[missonNumber].singleRun && this.inRun)
		{
			num = this._currentRunProgress[missonNumber];
		}
		return new MissionInfo(array[missonNumber], currentMissionTemplates[missonNumber], num, flag);
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x0001373C File Offset: 0x0001193C
	public bool HasMoreMissions()
	{
		return this.missionSetCount > this.currentMissionSet;
	}

	// Token: 0x040003B8 RID: 952
	private int _currentMissionSetLoaded = -1;

	// Token: 0x040003B9 RID: 953
	private MissionTemplate[] _currentMissionTemplates;

	// Token: 0x040003BA RID: 954
	private int[] _currentRunProgress;

	// Token: 0x040003BB RID: 955
	public Missions.MissionSetCompleteHandler onMissionSetComplete;

	// Token: 0x040003BC RID: 956
	public Missions.MissionCompleteHandler onMissionComplete;

	// Token: 0x040003BD RID: 957
	private static readonly Mission[][] missions = new Mission[][]
	{
		new Mission[]
		{
			new Mission(Missions.MissionType.EarnCoin, 500),
			new Mission(Missions.MissionType.ScoreSingleRun, 1000),
			new Mission(Missions.MissionType.Powerups, 2)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.EarnCoinSingleRun, 200),
			new Mission(Missions.MissionType.Jump, 20),
			new Mission(Missions.MissionType.SuperSneakers, 2)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.Tokens, 2),
			new Mission(Missions.MissionType.Roll, 30),
			new Mission(Missions.MissionType.SpendCoin, 2000)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.DailyQuests, 1),
			new Mission(Missions.MissionType.DodgeBarriers, 20),
			new Mission(Missions.MissionType.ScoreSingleRun, 6000)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.EarnCoin, 2500),
			new Mission(Missions.MissionType.JumpSingleRun, 30),
			new Mission(Missions.MissionType.BuyMysterybox, 1)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.HoverBoard, 1),
			new Mission(Missions.MissionType.Magnets, 5),
			new Mission(Missions.MissionType.BumpBarrier, 4)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.Jetpack, 2),
			new Mission(Missions.MissionType.BeatFriends, 1),
			new Mission(Missions.MissionType.Headstart, 1)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.BumpTrainsSingleRun, 8),
			new Mission(Missions.MissionType.CoinsWithMagnet, 40),
			new Mission(Missions.MissionType.TimeDeath, 10)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.HoverBoardExpire, 1),
			new Mission(Missions.MissionType.MysteryBoxes, 2),
			new Mission(Missions.MissionType.RollSingleRun, 30)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.ScoreSingleRun, 20000),
			new Mission(Missions.MissionType.Powerups, 12),
			new Mission(Missions.MissionType.JumpTrain, 2)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.NoCoinsBeforeScore, 4000),
			new Mission(Missions.MissionType.RollCenter, 50),
			new Mission(Missions.MissionType.EarnCoin, 5000)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.DailyQuests, 2),
			new Mission(Missions.MissionType.DodgeBarriers, 40),
			new Mission(Missions.MissionType.SuperSneakers, 5)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.BumpBush, 4),
			new Mission(Missions.MissionType.CoinsWithMagnet, 160),
			new Mission(Missions.MissionType.MagnetsSingleRun, 2)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.MysteryBoxes, 4),
			new Mission(Missions.MissionType.RollSingleRun, 40),
			new Mission(Missions.MissionType.EarnCoinSingleRun, 400)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.ScoreCumulative, 100000),
			new Mission(Missions.MissionType.Jetpack, 5),
			new Mission(Missions.MissionType.BumpLightSignal, 12)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.DailyQuests, 3),
			new Mission(Missions.MissionType.SuperSneakersSingleRun, 3),
			new Mission(Missions.MissionType.JumpTrain, 4)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.ScoreSingleRun, 50000),
			new Mission(Missions.MissionType.SpendCoin, 4000),
			new Mission(Missions.MissionType.Magnets, 15)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.JetpackSingleRun, 2),
			new Mission(Missions.MissionType.BumpTrainsSingleRun, 12),
			new Mission(Missions.MissionType.CrashTrains, 20)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.HoverBoard, 5),
			new Mission(Missions.MissionType.MagnetsSingleRun, 3),
			new Mission(Missions.MissionType.Tokens, 5)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.ScoreCumulative, 250000),
			new Mission(Missions.MissionType.JumpSingleRun, 40),
			new Mission(Missions.MissionType.Powerups, 25)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.BumpBarrier, 15),
			new Mission(Missions.MissionType.BuyMysterybox, 3),
			new Mission(Missions.MissionType.CoinsWithMagnet, 240)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.DodgeBarriers, 80),
			new Mission(Missions.MissionType.SpendCoin, 8000),
			new Mission(Missions.MissionType.SuperSneakersSingleRun, 4)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.Headstart, 8),
			new Mission(Missions.MissionType.JumpTrain, 10),
			new Mission(Missions.MissionType.Jetpack, 15)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.MysteryBoxes, 8),
			new Mission(Missions.MissionType.RollCenter, 200),
			new Mission(Missions.MissionType.DailyQuests, 4)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.EarnCoin, 15000),
			new Mission(Missions.MissionType.ScoreSingleRun, 120000),
			new Mission(Missions.MissionType.JumpTrainSingleRun, 3)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.RollSingleRun, 50),
			new Mission(Missions.MissionType.ScoreCumulative, 500000),
			new Mission(Missions.MissionType.NoCoinsBeforeScore, 12000)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.Tokens, 10),
			new Mission(Missions.MissionType.BuyMysterybox, 6),
			new Mission(Missions.MissionType.BumpLightSignal, 20)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.JumpSingleRun, 50),
			new Mission(Missions.MissionType.HoverBoard, 12),
			new Mission(Missions.MissionType.HoverBoardExpire, 4)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.EarnCoinSingleRun, 750),
			new Mission(Missions.MissionType.BumpBarrier, 25),
			new Mission(Missions.MissionType.ScoreSingleRun, 250000)
		},
		new Mission[]
		{
			new Mission(Missions.MissionType.none, 1),
			new Mission(Missions.MissionType.none, 1),
			new Mission(Missions.MissionType.none, 1)
		}
	};

	// Token: 0x040003BE RID: 958
	private static readonly Dictionary<Missions.MissionType, MissionTemplate> missionTemplates = new Dictionary<Missions.MissionType, MissionTemplate>
	{
		{
			Missions.MissionType.EarnCoin,
			new MissionTemplate
			{
				descriptionSingle = "Collect {0} coin. {1} left",
				description = "Collect {0} coins. {1} left",
				ultraShortDescriptionSingle = "Collect {0} coin",
				ultraShortDescription = "Collect {0} coins",
				missionTarget = Missions.MissionTarget.EarnCoin
			}
		},
		{
			Missions.MissionType.EarnCoinSingleRun,
			new MissionTemplate
			{
				descriptionSingle = "Collect {0} coin in a one run. {1} left",
				description = "Collect {0} coins in a one run. {1} left",
				ultraShortDescriptionSingle = "Collect {0} coin",
				ultraShortDescription = "Collect {0} coins",
				missionTarget = Missions.MissionTarget.EarnCoin,
				singleRun = true
			}
		},
		{
			Missions.MissionType.SpendCoin,
			new MissionTemplate
			{
				descriptionSingle = "Spend {0} coin. {1} left",
				description = "Spend {0} coins. {1} left",
				ultraShortDescriptionSingle = "Spend {0} coin",
				ultraShortDescription = "Spend {0} coins",
				missionTarget = Missions.MissionTarget.SpendCoin
			}
		},
		{
			Missions.MissionType.ScoreCumulative,
			new MissionTemplate
			{
				descriptionSingle = "Collect {0} point. {1} left",
				description = "Collect {0} points. {1} left",
				ultraShortDescriptionSingle = "Collect {0} point",
				ultraShortDescription = "Collect {0} points",
				missionTarget = Missions.MissionTarget.Score
			}
		},
		{
			Missions.MissionType.JumpTrain,
			new MissionTemplate
			{
				descriptionSingle = "Jump over {0} train. {1} left",
				description = "Jump over {0} trains. {1} left",
				ultraShortDescriptionSingle = "Jump {0} train",
				ultraShortDescription = "Jump {0} trains",
				missionTarget = Missions.MissionTarget.JumpTrain
			}
		},
		{
			Missions.MissionType.JumpTrainSingleRun,
			new MissionTemplate
			{
				descriptionSingle = "Jump over {0} train in one run. {1} left",
				description = "Jump over {0} trains in one run. {1} left",
				ultraShortDescriptionSingle = "Jump {0} train",
				ultraShortDescription = "Jump {0} trains",
				missionTarget = Missions.MissionTarget.JumpTrain,
				singleRun = true
			}
		},
		{
			Missions.MissionType.Jump,
			new MissionTemplate
			{
				descriptionSingle = "Jump {0} time. {1} left",
				description = "Jump {0} times. {1} left",
				ultraShortDescriptionSingle = "Jump {0} time",
				ultraShortDescription = "Jump {0} times",
				missionTarget = Missions.MissionTarget.Jump
			}
		},
		{
			Missions.MissionType.JumpSingleRun,
			new MissionTemplate
			{
				descriptionSingle = "Jump {0} time in one run. {1} left",
				description = "Jump {0} times in one run. {1} left",
				ultraShortDescriptionSingle = "Jump {0} time",
				ultraShortDescription = "Jump {0} times",
				missionTarget = Missions.MissionTarget.Jump,
				singleRun = true
			}
		},
		{
			Missions.MissionType.Roll,
			new MissionTemplate
			{
				descriptionSingle = "Roll {0} time. {1} left",
				description = "Roll {0} times in total. {1} left",
				ultraShortDescriptionSingle = "Roll {0} time",
				ultraShortDescription = "Roll {0} times",
				missionTarget = Missions.MissionTarget.Roll
			}
		},
		{
			Missions.MissionType.RollSingleRun,
			new MissionTemplate
			{
				descriptionSingle = "Roll {0} time in a single run. {1} left",
				description = "Roll {0} times in a single run. {1} left",
				ultraShortDescriptionSingle = "Roll {0} time",
				ultraShortDescription = "Roll {0} times",
				missionTarget = Missions.MissionTarget.Roll,
				singleRun = true
			}
		},
		{
			Missions.MissionType.RollLeft,
			new MissionTemplate
			{
				descriptionSingle = "Roll {0} time in left lane. {1} left",
				description = "Roll {0} times in left lane. {1} left",
				ultraShortDescriptionSingle = "Roll {0} time",
				ultraShortDescription = "Roll {0} times",
				missionTarget = Missions.MissionTarget.RollLeft
			}
		},
		{
			Missions.MissionType.RollCenter,
			new MissionTemplate
			{
				descriptionSingle = "Roll {0} time in center lane. {1} left",
				description = "Roll {0} times in center lane. {1} left",
				ultraShortDescriptionSingle = "Roll {0} time",
				ultraShortDescription = "Roll {0} times",
				missionTarget = Missions.MissionTarget.RollCenter
			}
		},
		{
			Missions.MissionType.RollRight,
			new MissionTemplate
			{
				descriptionSingle = "Roll {0} time in right lane. {1} left",
				description = "Roll {0} times in right lane. {1} left",
				ultraShortDescriptionSingle = "Roll {0} time",
				ultraShortDescription = "Roll {0} times",
				missionTarget = Missions.MissionTarget.RollRight
			}
		},
		{
			Missions.MissionType.RollUnderBarriers,
			new MissionTemplate
			{
				descriptionSingle = "Roll under {0} barrier. {1} left",
				description = "Roll under {0} barriers. {1} left",
				ultraShortDescriptionSingle = "Roll under {0} barrier",
				ultraShortDescription = "Roll under {0} barriers",
				missionTarget = Missions.MissionTarget.RollUnderBarriers
			}
		},
		{
			Missions.MissionType.JumpBarriers,
			new MissionTemplate
			{
				descriptionSingle = "Jump over {0} barrier. {1} left",
				description = "Jump over {0} barriers. {1} left",
				ultraShortDescriptionSingle = "Jump {0} barrier",
				ultraShortDescription = "Jump {0} barriers",
				missionTarget = Missions.MissionTarget.JumpBarriers
			}
		},
		{
			Missions.MissionType.DieToTrain,
			new MissionTemplate
			{
				descriptionSingle = "Get run over by {0} train. {1} left",
				description = "Get run over by {0} trains. {1} left",
				ultraShortDescriptionSingle = "Get run over {0} time",
				ultraShortDescription = "Get run over {0} times",
				missionTarget = Missions.MissionTarget.DieToTrain
			}
		},
		{
			Missions.MissionType.Jetpack,
			new MissionTemplate
			{
				descriptionSingle = "Pick up {0} Jetpack. {1} left",
				description = "Pick up {0} Jetpacks. {1} left",
				ultraShortDescriptionSingle = "Pick up {0} Jetpack",
				ultraShortDescription = "Pick up {0} Jetpacks",
				missionTarget = Missions.MissionTarget.Jetpack
			}
		},
		{
			Missions.MissionType.JetpackSingleRun,
			new MissionTemplate
			{
				descriptionSingle = "Pick up {0} Jetpack in one run. {1} left",
				description = "Pick up {0} Jetpacks in one run. {1} left",
				ultraShortDescriptionSingle = "Pick up {0} Jetpack",
				ultraShortDescription = "Pick up {0} Jetpacks",
				missionTarget = Missions.MissionTarget.Jetpack,
				singleRun = true
			}
		},
		{
			Missions.MissionType.SuperSneakers,
			new MissionTemplate
			{
				descriptionSingle = "Pick up {0} Super Sneaker. {1} left",
				description = "Pick up {0} Super Sneakers. {1} left",
				ultraShortDescriptionSingle = "Pick up {0} Sneaker",
				ultraShortDescription = "Pick up {0} Sneakers",
				missionTarget = Missions.MissionTarget.SuperSneakers
			}
		},
		{
			Missions.MissionType.SuperSneakersSingleRun,
			new MissionTemplate
			{
				descriptionSingle = "Pick up {0} Super Sneaker in one run. {1} left",
				description = "Pick up {0} Super Sneakers in one run. {1} left",
				ultraShortDescriptionSingle = "Pick up {0} Sneaker",
				ultraShortDescription = "Pick up {0} Sneakers",
				missionTarget = Missions.MissionTarget.SuperSneakers,
				singleRun = true
			}
		},
		{
			Missions.MissionType.Letters,
			new MissionTemplate
			{
				descriptionSingle = "Pick up {0} Daily Letter. {1} left",
				description = "Pick up {0} Daily Letters. {1} left",
				ultraShortDescriptionSingle = "Pick up {0} Letter",
				ultraShortDescription = "Pick up {0} Letters",
				missionTarget = Missions.MissionTarget.Letters
			}
		},
		{
			Missions.MissionType.Magnets,
			new MissionTemplate
			{
				descriptionSingle = "Pick up {0} Coin Magnet. {1} left",
				description = "Pick up {0} Coin Magnets. {1} left",
				ultraShortDescriptionSingle = "Pick up {0} Magnet",
				ultraShortDescription = "Pick up {0} Magnets",
				missionTarget = Missions.MissionTarget.Magnets
			}
		},
		{
			Missions.MissionType.MagnetsSingleRun,
			new MissionTemplate
			{
				descriptionSingle = "Pick up {0} Magnet in one run. {1} left",
				description = "Pick up {0} Magnets in one run. {1} left",
				ultraShortDescriptionSingle = "Pick up {0} Magnet",
				ultraShortDescription = "Pick up {0} Magnets",
				missionTarget = Missions.MissionTarget.Magnets,
				singleRun = true
			}
		},
		{
			Missions.MissionType.BeatFriends,
			new MissionTemplate
			{
				descriptionSingle = "Beat {0} friend. {1} left",
				description = "Beat {0} friends. {1} left",
				ultraShortDescriptionSingle = "Beat {0} friend",
				ultraShortDescription = "Beat {0} friends",
				missionTarget = Missions.MissionTarget.BeatFriends
			}
		},
		{
			Missions.MissionType.BeatFriendsSingleRun,
			new MissionTemplate
			{
				descriptionSingle = "Beat {0} friend in one run. {1} left",
				description = "Beat {0} friends in one run. {1} left",
				ultraShortDescriptionSingle = "Beat {0} friend",
				ultraShortDescription = "Beat {0} friends",
				missionTarget = Missions.MissionTarget.BeatFriends,
				singleRun = true
			}
		},
		{
			Missions.MissionType.DailyQuests,
			new MissionTemplate
			{
				descriptionSingle = "Complete {0} Daily Challenge. {1} left",
				description = "Complete {0} Daily Challenges. {1} left",
				ultraShortDescriptionSingle = "{0} Daily Challenge",
				ultraShortDescription = "{0} Daily Challenges",
				missionTarget = Missions.MissionTarget.DailyQuests
			}
		},
		{
			Missions.MissionType.Tokens,
			new MissionTemplate
			{
				descriptionSingle = "Get {0} character token. {1} left",
				description = "Get {0} character tokens. {1} left",
				ultraShortDescriptionSingle = "Get {0} token",
				ultraShortDescription = "Get {0} tokens",
				missionTarget = Missions.MissionTarget.Tokens
			}
		},
		{
			Missions.MissionType.DodgeBarriers,
			new MissionTemplate
			{
				descriptionSingle = "Dodge {0} barrier. {1} left",
				description = "Dodge {0} barriers. {1} left",
				ultraShortDescriptionSingle = "Dodge {0} barrier",
				ultraShortDescription = "Dodge {0} barriers",
				missionTarget = Missions.MissionTarget.DodgeBarriers
			}
		},
		{
			Missions.MissionType.CrashBarriers,
			new MissionTemplate
			{
				descriptionSingle = "Crash into {0} barrier. {1} left",
				description = "Crash into {0} barriers. {1} left",
				ultraShortDescriptionSingle = "Crash into {0} barrier",
				ultraShortDescription = "Crash into {0} barriers",
				missionTarget = Missions.MissionTarget.CrashBarriers
			}
		},
		{
			Missions.MissionType.CrashBarriersSingleRun,
			new MissionTemplate
			{
				descriptionSingle = "Crash into {0} barrier in one run. {1} left",
				description = "Crash into {0} barriers in one run. {1} left",
				ultraShortDescriptionSingle = "Crash into {0} barrier",
				ultraShortDescription = "Crash into {0} barriers",
				missionTarget = Missions.MissionTarget.CrashBarriers,
				singleRun = true
			}
		},
		{
			Missions.MissionType.CrashTrains,
			new MissionTemplate
			{
				descriptionSingle = "Crash into {0} train. {1} left",
				description = "Crash into {0} trains. {1} left",
				ultraShortDescriptionSingle = "Crash into {0} train",
				ultraShortDescription = "Crash into {0} trains",
				missionTarget = Missions.MissionTarget.CrashTrains
			}
		},
		{
			Missions.MissionType.BumpTrainsSingleRun,
			new MissionTemplate
			{
				descriptionSingle = "Bump into {0} train in one run. {1} left",
				description = "Bump into {0} trains in one run. {1} left",
				ultraShortDescriptionSingle = "Bump into {0} train",
				ultraShortDescription = "Bump into {0} trains",
				missionTarget = Missions.MissionTarget.BumpTrain,
				singleRun = true
			}
		},
		{
			Missions.MissionType.Powerups,
			new MissionTemplate
			{
				descriptionSingle = "Pickup {0} powerup. {1} left",
				description = "Pickup {0} powerups. {1} left",
				ultraShortDescriptionSingle = "Pickup {0} powerup",
				ultraShortDescription = "Pickup {0} powerups",
				missionTarget = Missions.MissionTarget.Powerups
			}
		},
		{
			Missions.MissionType.Headstart,
			new MissionTemplate
			{
				descriptionSingle = "Use {0} Headstart. {1} left",
				description = "Use {0} Headstarts. {1} left",
				ultraShortDescriptionSingle = "Use {0} Headstart",
				ultraShortDescription = "Use {0} Headstarts",
				missionTarget = Missions.MissionTarget.Headstart
			}
		},
		{
			Missions.MissionType.CoinsWithMagnet,
			new MissionTemplate
			{
				descriptionSingle = "Pickup {0} coin with a Magnet. {1} left",
				description = "Pickup {0} coins with a Magnet. {1} left",
				ultraShortDescriptionSingle = "{0} coin with Magnet",
				ultraShortDescription = "{0} coins with Magnet",
				missionTarget = Missions.MissionTarget.CoinsWithMagnet
			}
		},
		{
			Missions.MissionType.BuyMysterybox,
			new MissionTemplate
			{
				descriptionSingle = "Buy {0} Mystery box. {1} left",
				description = "Buy {0} Mystery boxes. {1} left",
				ultraShortDescriptionSingle = "Buy {0} Mystery box",
				ultraShortDescription = "Buy {0} Mystery boxes",
				missionTarget = Missions.MissionTarget.BuyMysterybox
			}
		},
		{
			Missions.MissionType.CollectCoinPouch,
			new MissionTemplate
			{
				descriptionSingle = "Pickup {0} coin pouch. {1} left",
				description = "Pickup {0} coin pouches in total. {1} left",
				ultraShortDescriptionSingle = "{0} coin pouche",
				ultraShortDescription = "{0} coin pouches",
				missionTarget = Missions.MissionTarget.CollectCoinPouch
			}
		},
		{
			Missions.MissionType.TimeDeath,
			new MissionTemplate
			{
				descriptionSingle = "Get caught in first {0} second of run. Ran {1} sec",
				description = "Get caught in first {0} seconds of run. Ran {1} sec",
				ultraShortDescriptionSingle = "Caught in {0} sec",
				ultraShortDescription = "Caught in {0} sec",
				missionTarget = Missions.MissionTarget.TimeDeath,
				singleRun = true,
				completeIfLess = true
			}
		},
		{
			Missions.MissionType.MysteryBoxes,
			new MissionTemplate
			{
				descriptionSingle = "Pickup {0} Mystery boxe. {1} left",
				description = "Pickup {0} Mystery boxes. {1} left",
				ultraShortDescriptionSingle = "{0} Mystery box",
				ultraShortDescription = "{0} Mystery boxes",
				missionTarget = Missions.MissionTarget.MysteryBoxes
			}
		},
		{
			Missions.MissionType.BumpBarrier,
			new MissionTemplate
			{
				descriptionSingle = "Stumble into {0} Barrier. {1} left",
				description = "Stumble into {0} Barriers. {1} left",
				ultraShortDescriptionSingle = "Stumble {0} Barrier",
				ultraShortDescription = "Stumble {0} Barriers",
				missionTarget = Missions.MissionTarget.BumpBarrier
			}
		},
		{
			Missions.MissionType.BumpLightSignal,
			new MissionTemplate
			{
				descriptionSingle = "Bump into {0} Light Signal. {1} left",
				description = "Bump into {0} Light Signals. {1} left",
				ultraShortDescriptionSingle = "{0} Light Signal",
				ultraShortDescription = "{0} Light Signal",
				missionTarget = Missions.MissionTarget.BumpLightSignal
			}
		},
		{
			Missions.MissionType.BumpBush,
			new MissionTemplate
			{
				descriptionSingle = "Bump {0} bush. {1} left",
				description = "Bump {0} bushes. {1} left",
				ultraShortDescriptionSingle = "Bump {0} Bush",
				ultraShortDescription = "Bump {0} Bushes",
				missionTarget = Missions.MissionTarget.BumpBush
			}
		},
		{
			Missions.MissionType.BumpTrain,
			new MissionTemplate
			{
				descriptionSingle = "Bump {0} train. {1} left",
				description = "Bump {0} trains. {1} left",
				ultraShortDescriptionSingle = "Bump {0} train",
				ultraShortDescription = "Bump {0} trains",
				missionTarget = Missions.MissionTarget.BumpTrain
			}
		},
		{
			Missions.MissionType.HoverBoard,
			new MissionTemplate
			{
				descriptionSingle = "Use {0} Hoverboard. {1} left",
				description = "Use {0} Hoverboards. {1} left",
				ultraShortDescriptionSingle = "Use {0} Hoverboard",
				ultraShortDescription = "Use {0} Hoverboards",
				missionTarget = Missions.MissionTarget.HoverBoard
			}
		},
		{
			Missions.MissionType.HoverBoardExpire,
			new MissionTemplate
			{
				descriptionSingle = "Use {0} Hoverboard without crashing. {1} left",
				description = "Use {0} Hoverboards without crashing. {1} left",
				ultraShortDescriptionSingle = "{0} Hoverboard no crash",
				ultraShortDescription = "{0} Hoverboards no crash",
				missionTarget = Missions.MissionTarget.HoverBoardExpire
			}
		},
		{
			Missions.MissionType.ScoreSingleRun,
			new MissionTemplate
			{
				descriptionSingle = "Score {0} point in single run. {1} left",
				description = "Score {0} points in single run. {1} left",
				ultraShortDescriptionSingle = "{0} point one run",
				ultraShortDescription = "{0} points one run",
				missionTarget = Missions.MissionTarget.Score,
				singleRun = true
			}
		},
		{
			Missions.MissionType.NoCoinsBeforeScore,
			new MissionTemplate
			{
				descriptionSingle = "Collect {0} point without collecting coins. {1} left",
				description = "Collect {0} points without collecting coins. {1} left",
				ultraShortDescriptionSingle = "{0} Point no coins",
				ultraShortDescription = "{0} Points no coins",
				missionTarget = Missions.MissionTarget.NoCoinsBeforeScore,
				singleRun = true
			}
		},
		{
			Missions.MissionType.none,
			new MissionTemplate
			{
				descriptionSingle = " ",
				description = " ",
				ultraShortDescriptionSingle = " ",
				ultraShortDescription = " "
			}
		}
	};

	// Token: 0x040003BF RID: 959
	private static Missions _instance;

	// Token: 0x020001B7 RID: 439
	public enum MissionTarget
	{
		// Token: 0x04000A22 RID: 2594
		none,
		// Token: 0x04000A23 RID: 2595
		EarnCoin,
		// Token: 0x04000A24 RID: 2596
		SpendCoin,
		// Token: 0x04000A25 RID: 2597
		Score,
		// Token: 0x04000A26 RID: 2598
		JumpTrain,
		// Token: 0x04000A27 RID: 2599
		Jump,
		// Token: 0x04000A28 RID: 2600
		Roll,
		// Token: 0x04000A29 RID: 2601
		RollLeft,
		// Token: 0x04000A2A RID: 2602
		RollCenter,
		// Token: 0x04000A2B RID: 2603
		RollRight,
		// Token: 0x04000A2C RID: 2604
		RollUnderBarriers,
		// Token: 0x04000A2D RID: 2605
		JumpBarriers,
		// Token: 0x04000A2E RID: 2606
		DieToTrain,
		// Token: 0x04000A2F RID: 2607
		Jetpack,
		// Token: 0x04000A30 RID: 2608
		SuperSneakers,
		// Token: 0x04000A31 RID: 2609
		Letters,
		// Token: 0x04000A32 RID: 2610
		Magnets,
		// Token: 0x04000A33 RID: 2611
		MysteryBoxes,
		// Token: 0x04000A34 RID: 2612
		BeatFriends,
		// Token: 0x04000A35 RID: 2613
		DailyQuests,
		// Token: 0x04000A36 RID: 2614
		Tokens,
		// Token: 0x04000A37 RID: 2615
		DodgeBarriers,
		// Token: 0x04000A38 RID: 2616
		CrashBarriers,
		// Token: 0x04000A39 RID: 2617
		CrashTrains,
		// Token: 0x04000A3A RID: 2618
		Powerups,
		// Token: 0x04000A3B RID: 2619
		Headstart,
		// Token: 0x04000A3C RID: 2620
		CoinsWithMagnet,
		// Token: 0x04000A3D RID: 2621
		BuyMysterybox,
		// Token: 0x04000A3E RID: 2622
		CollectCoinPouch,
		// Token: 0x04000A3F RID: 2623
		TimeDeath,
		// Token: 0x04000A40 RID: 2624
		BumpTrain,
		// Token: 0x04000A41 RID: 2625
		BumpBush,
		// Token: 0x04000A42 RID: 2626
		BumpLightSignal,
		// Token: 0x04000A43 RID: 2627
		BumpBarrier,
		// Token: 0x04000A44 RID: 2628
		HoverBoard,
		// Token: 0x04000A45 RID: 2629
		HoverBoardExpire,
		// Token: 0x04000A46 RID: 2630
		NoCoinsBeforeScore
	}

	// Token: 0x020001B8 RID: 440
	public enum MissionType
	{
		// Token: 0x04000A48 RID: 2632
		none,
		// Token: 0x04000A49 RID: 2633
		EarnCoin,
		// Token: 0x04000A4A RID: 2634
		EarnCoinSingleRun,
		// Token: 0x04000A4B RID: 2635
		SpendCoin,
		// Token: 0x04000A4C RID: 2636
		ScoreCumulative,
		// Token: 0x04000A4D RID: 2637
		JumpTrain,
		// Token: 0x04000A4E RID: 2638
		JumpTrainSingleRun,
		// Token: 0x04000A4F RID: 2639
		Jump,
		// Token: 0x04000A50 RID: 2640
		JumpSingleRun,
		// Token: 0x04000A51 RID: 2641
		Roll,
		// Token: 0x04000A52 RID: 2642
		RollSingleRun,
		// Token: 0x04000A53 RID: 2643
		RollLeft,
		// Token: 0x04000A54 RID: 2644
		RollCenter,
		// Token: 0x04000A55 RID: 2645
		RollRight,
		// Token: 0x04000A56 RID: 2646
		RollUnderBarriers,
		// Token: 0x04000A57 RID: 2647
		JumpBarriers,
		// Token: 0x04000A58 RID: 2648
		DieToTrain,
		// Token: 0x04000A59 RID: 2649
		Jetpack,
		// Token: 0x04000A5A RID: 2650
		JetpackSingleRun,
		// Token: 0x04000A5B RID: 2651
		SuperSneakers,
		// Token: 0x04000A5C RID: 2652
		SuperSneakersSingleRun,
		// Token: 0x04000A5D RID: 2653
		Letters,
		// Token: 0x04000A5E RID: 2654
		Magnets,
		// Token: 0x04000A5F RID: 2655
		MagnetsSingleRun,
		// Token: 0x04000A60 RID: 2656
		MysteryBoxes,
		// Token: 0x04000A61 RID: 2657
		BeatFriends,
		// Token: 0x04000A62 RID: 2658
		BeatFriendsSingleRun,
		// Token: 0x04000A63 RID: 2659
		DailyQuests,
		// Token: 0x04000A64 RID: 2660
		Tokens,
		// Token: 0x04000A65 RID: 2661
		DodgeBarriers,
		// Token: 0x04000A66 RID: 2662
		CrashBarriers,
		// Token: 0x04000A67 RID: 2663
		CrashBarriersSingleRun,
		// Token: 0x04000A68 RID: 2664
		CrashTrains,
		// Token: 0x04000A69 RID: 2665
		BumpTrainsSingleRun,
		// Token: 0x04000A6A RID: 2666
		Powerups,
		// Token: 0x04000A6B RID: 2667
		Headstart,
		// Token: 0x04000A6C RID: 2668
		CoinsWithMagnet,
		// Token: 0x04000A6D RID: 2669
		BuyMysterybox,
		// Token: 0x04000A6E RID: 2670
		CollectCoinPouch,
		// Token: 0x04000A6F RID: 2671
		TimeDeath,
		// Token: 0x04000A70 RID: 2672
		BumpTrain,
		// Token: 0x04000A71 RID: 2673
		BumpBush,
		// Token: 0x04000A72 RID: 2674
		BumpLightSignal,
		// Token: 0x04000A73 RID: 2675
		BumpBarrier,
		// Token: 0x04000A74 RID: 2676
		ScoreSingleRun,
		// Token: 0x04000A75 RID: 2677
		HoverBoard,
		// Token: 0x04000A76 RID: 2678
		HoverBoardExpire,
		// Token: 0x04000A77 RID: 2679
		NoCoinsBeforeScore
	}

	// Token: 0x020001B9 RID: 441
	// (Invoke) Token: 0x06000B47 RID: 2887
	public delegate void MissionSetCompleteHandler();

	// Token: 0x020001BA RID: 442
	// (Invoke) Token: 0x06000B4B RID: 2891
	public delegate void MissionCompleteHandler(string msg);
}
