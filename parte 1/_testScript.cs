using System;
using UnityEngine;

// Token: 0x02000151 RID: 337
public class _testScript : MonoBehaviour
{
	// Token: 0x060009DD RID: 2525 RVA: 0x0003664B File Offset: 0x0003484B
	private void Start()
	{
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x00036650 File Offset: 0x00034850
	private void OnEnable()
	{
		Debug.Log("Enabled");
		Missions instance = Missions.Instance;
		instance.onMissionComplete = (Missions.MissionCompleteHandler)Delegate.Combine(instance.onMissionComplete, new Missions.MissionCompleteHandler(this.OnMissionComplete));
		Missions instance2 = Missions.Instance;
		instance2.onMissionSetComplete = (Missions.MissionSetCompleteHandler)Delegate.Combine(instance2.onMissionSetComplete, new Missions.MissionSetCompleteHandler(this.OnMissionSetComplete));
		PlayerInfo instance3 = PlayerInfo.Instance;
		instance3.onCoinsChanged = (Action)Delegate.Combine(instance3.onCoinsChanged, new Action(this.OnCoinsChanged));
		PlayerInfo instance4 = PlayerInfo.Instance;
		instance4.onScoreMultiplierChanged = (Action)Delegate.Combine(instance4.onScoreMultiplierChanged, new Action(this.OnScoreMultiplierChanged));
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x00036700 File Offset: 0x00034900
	private void OnDisable()
	{
		Debug.Log("Disabled");
		Missions instance = Missions.Instance;
		instance.onMissionComplete = (Missions.MissionCompleteHandler)Delegate.Remove(instance.onMissionComplete, new Missions.MissionCompleteHandler(this.OnMissionComplete));
		Missions instance2 = Missions.Instance;
		instance2.onMissionSetComplete = (Missions.MissionSetCompleteHandler)Delegate.Remove(instance2.onMissionSetComplete, new Missions.MissionSetCompleteHandler(this.OnMissionSetComplete));
		PlayerInfo instance3 = PlayerInfo.Instance;
		instance3.onCoinsChanged = (Action)Delegate.Remove(instance3.onCoinsChanged, new Action(this.OnCoinsChanged));
		PlayerInfo instance4 = PlayerInfo.Instance;
		instance4.onScoreMultiplierChanged = (Action)Delegate.Remove(instance4.onScoreMultiplierChanged, new Action(this.OnScoreMultiplierChanged));
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x000367AF File Offset: 0x000349AF
	private void OnMissionComplete(string msg)
	{
		Debug.Log("OnMissionComplete: " + msg);
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x000367C4 File Offset: 0x000349C4
	private void OnMissionSetComplete()
	{
		Debug.Log("OnMissionSetComplete, new mission set is " + PlayerInfo.Instance.currentMissionSet.ToString() + ", new missions:");
		foreach (MissionInfo missionInfo2 in Missions.Instance.GetMissionInfo())
		{
			Debug.Log("Mission " + missionInfo2.complete.ToString() + " " + string.Format(missionInfo2.template.description, missionInfo2.mission.goal, missionInfo2.progress));
		}
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x00036860 File Offset: 0x00034A60
	private void OnCoinsChanged()
	{
		Debug.Log("OnCoinsChanged: " + PlayerInfo.Instance.amountOfCoins.ToString());
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x00036890 File Offset: 0x00034A90
	private void OnScoreMultiplierChanged()
	{
		Debug.Log("OnScoreMultiplierChanged: " + PlayerInfo.Instance.scoreMultiplier.ToString());
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x000368C0 File Offset: 0x00034AC0
	private void Update()
	{
		Missions instance = Missions.Instance;
		PlayerInfo instance2 = PlayerInfo.Instance;
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Debug.Log("Next mission set");
			if (instance.currentMissionSet + 1 < instance.missionSetCount)
			{
				Missions missions = instance;
				int i = missions.currentMissionSet;
				missions.currentMissionSet = i + 1;
				Debug.Log("Current Mission Set set to " + instance.currentMissionSet.ToString());
				foreach (MissionInfo missionInfo in Missions.Instance.GetMissionInfo())
				{
					Debug.Log("Mission " + missionInfo.complete.ToString() + " " + string.Format(missionInfo.template.description, missionInfo.mission.goal, Mathf.Max(0, missionInfo.mission.goal - missionInfo.progress)));
				}
				return;
			}
			Debug.Log("Current mission set is already at max " + instance.currentMissionSet.ToString());
			return;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Debug.Log("Previous mission set");
			if (instance.currentMissionSet > 0)
			{
				Missions missions2 = instance;
				int i = missions2.currentMissionSet;
				missions2.currentMissionSet = i - 1;
				Debug.Log("Current Mission Set set to " + instance.currentMissionSet.ToString());
				foreach (MissionInfo missionInfo2 in Missions.Instance.GetMissionInfo())
				{
					Debug.Log("Mission " + missionInfo2.complete.ToString() + " " + string.Format(missionInfo2.template.description, missionInfo2.mission.goal, Mathf.Max(0, missionInfo2.mission.goal - missionInfo2.progress)));
				}
				return;
			}
			Debug.Log("Current mission set is already at min 0");
			return;
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				Debug.Log("Get mission info (menu)");
				foreach (MissionInfo missionInfo3 in Missions.Instance.GetMissionInfo())
				{
					Debug.Log("tempMission " + missionInfo3.complete.ToString() + " " + string.Format(missionInfo3.template.description, missionInfo3.mission.goal, Mathf.Max(0, missionInfo3.mission.goal - missionInfo3.progress)));
				}
				return;
			}
			if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				Missions.Instance.inRun = true;
				Debug.Log("inRun true");
				return;
			}
			if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				Missions.Instance.inRun = false;
				Debug.Log("inRun false");
				return;
			}
			if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				instance2.amountOfCoins += 5;
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.EarnCoin, 5);
				return;
			}
			if (Input.GetKeyDown(KeyCode.Alpha8))
			{
				instance2.doubleScore = !instance2.doubleScore;
				return;
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.BuyMysterybox, 1);
				Debug.Log("Missions.Instance.MissionTarget.BuyMysterybox");
				return;
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.CollectCoinPouch, 1);
				Debug.Log("Missions.Instance.MissionTarget.CollectCoinPouch");
				return;
			}
			if (Input.GetKeyDown(KeyCode.E))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.BeatFriends, 1);
				Debug.Log("Missions.Instance.MissionTarget.BeatFriends");
				return;
			}
			if (Input.GetKeyDown(KeyCode.R))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.DodgeBarriers, 1);
				Debug.Log("Missions.Instance.MissionTarget.Characters");
				return;
			}
			if (Input.GetKeyDown(KeyCode.T))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.CoinsWithMagnet, 1);
				Debug.Log("Missions.Instance.MissionTarget.CoinsWithMagnet");
				return;
			}
			if (Input.GetKeyDown(KeyCode.Y))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.CrashTrains, 1);
				Debug.Log("Missions.Instance.MissionTarget.CrashTrains");
				return;
			}
			if (Input.GetKeyDown(KeyCode.U))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.DailyQuests, 1);
				Debug.Log("Missions.Instance.MissionTarget.DailyQuests");
				return;
			}
			if (Input.GetKeyDown(KeyCode.I))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.DieToTrain, 1);
				Debug.Log("Missions.Instance.MissionTarget.DieToTrain");
				return;
			}
			if (Input.GetKeyDown(KeyCode.O))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.RollUnderBarriers, 1);
				Debug.Log("Missions.Instance.MissionTarget.DodgeBarriers");
				return;
			}
			if (Input.GetKeyDown(KeyCode.P))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.EarnCoin, 1);
				Debug.Log("Missions.Instance.MissionTarget.EarnCoin");
				return;
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Headstart, 1);
				Debug.Log("Missions.Instance.MissionTarget.Headstart");
				return;
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Jetpack, 1);
				Debug.Log("Missions.Instance.MissionTarget.Jetpack");
				return;
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Jump, 1);
				Debug.Log("Missions.Instance.MissionTarget.Jump");
				return;
			}
			if (Input.GetKeyDown(KeyCode.F))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.JumpBarriers, 1);
				Debug.Log("Missions.Instance.MissionTarget.JumpBarriers");
				return;
			}
			if (Input.GetKeyDown(KeyCode.G))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.JumpTrain, 1);
				Debug.Log("Missions.Instance.MissionTarget.JumpTrain");
				return;
			}
			if (Input.GetKeyDown(KeyCode.H))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Letters, 1);
				Debug.Log("Missions.Instance.MissionTarget.Letters");
				return;
			}
			if (Input.GetKeyDown(KeyCode.J))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Magnets, 1);
				Debug.Log("Missions.Instance.MissionTarget.Magnets");
				return;
			}
			if (Input.GetKeyDown(KeyCode.K))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.MysteryBoxes, 1);
				Debug.Log("Missions.Instance.MissionTarget.MysteryBoxes");
				return;
			}
			if (Input.GetKeyDown(KeyCode.L))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Powerups, 1);
				Debug.Log("Missions.Instance.MissionTarget.Powerups");
				return;
			}
			if (Input.GetKeyDown(KeyCode.Z))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Roll, 1);
				Debug.Log("Missions.Instance.MissionTarget.Roll");
				return;
			}
			if (Input.GetKeyDown(KeyCode.X))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.RollCenter, 1);
				Debug.Log("Missions.Instance.MissionTarget.RollCenter");
				return;
			}
			if (Input.GetKeyDown(KeyCode.C))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.RollLeft, 1);
				Debug.Log("Missions.Instance.MissionTarget.RollLeft");
				return;
			}
			if (Input.GetKeyDown(KeyCode.V))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.RollRight, 1);
				Debug.Log("Missions.Instance.MissionTarget.RollRight");
				return;
			}
			if (Input.GetKeyDown(KeyCode.B))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Score, 3);
				Debug.Log("Missions.Instance.MissionTarget.Score");
				return;
			}
			if (Input.GetKeyDown(KeyCode.N))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.SpendCoin, 1);
				Debug.Log("Missions.Instance.MissionTarget.ScoreBetween");
				return;
			}
			if (Input.GetKeyDown(KeyCode.M))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.SuperSneakers, 1);
				Debug.Log("Missions.Instance.MissionTarget.SuperSneekers");
				return;
			}
			if (Input.GetKeyDown(KeyCode.Comma))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.TimeDeath, (int)Time.time);
				Debug.Log("Missions.Instance.MissionTarget.TimeDeath");
				return;
			}
			if (Input.GetKeyDown(KeyCode.Period))
			{
				Missions.Instance.PlayerDidThis(Missions.MissionTarget.Tokens, 1);
				Debug.Log("Missions.Instance.MissionTarget.Tokens");
			}
			return;
		}
	}
}
