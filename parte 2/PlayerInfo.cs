using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class PlayerInfo
{
	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00017F35 File Offset: 0x00016135
	public bool dirty
	{
		get
		{
			return this._dirty;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00017F3D File Offset: 0x0001613D
	// (set) Token: 0x060004F7 RID: 1271 RVA: 0x00017F48 File Offset: 0x00016148
	public int amountOfCoins
	{
		get
		{
			return this._amountOfCoins;
		}
		set
		{
			if (this._amountOfCoins != value)
			{
				this._amountOfCoins = value;
				this._dirty = true;
				Action action = this.onCoinsChanged;
				if (action != null)
				{
					action();
				}
			}
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00017F7C File Offset: 0x0001617C
	// (set) Token: 0x060004F9 RID: 1273 RVA: 0x00017F84 File Offset: 0x00016184
	public int highestScore
	{
		get
		{
			return this._highestScore;
		}
		set
		{
			if (value > this._highestScore)
			{
				this._oldHighestScore = this._highestScore;
				this._highestScore = value;
				this._dirty = true;
			}
		}
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x060004FA RID: 1274 RVA: 0x00017FA9 File Offset: 0x000161A9
	public int oldHighestScore
	{
		get
		{
			return this._oldHighestScore;
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x060004FB RID: 1275 RVA: 0x00017FB1 File Offset: 0x000161B1
	// (set) Token: 0x060004FC RID: 1276 RVA: 0x00017FB9 File Offset: 0x000161B9
	public int highestMeters
	{
		get
		{
			return this._highestMeters;
		}
		set
		{
			this._highestMeters = value;
			this._dirty = true;
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x060004FD RID: 1277 RVA: 0x00017FC9 File Offset: 0x000161C9
	// (set) Token: 0x060004FE RID: 1278 RVA: 0x00017FD1 File Offset: 0x000161D1
	public int amountOfMysteryBoxesOpened
	{
		get
		{
			return this._amountOfMysteryBoxesOpened;
		}
		set
		{
			this._amountOfMysteryBoxesOpened = value;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x060004FF RID: 1279 RVA: 0x00017FDA File Offset: 0x000161DA
	// (set) Token: 0x06000500 RID: 1280 RVA: 0x00017FE2 File Offset: 0x000161E2
	public int mysteryBoxesToUnlock
	{
		get
		{
			return this._mysteryBoxesToUnlock;
		}
		set
		{
			this._mysteryBoxesToUnlock = value;
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x06000501 RID: 1281 RVA: 0x00017FEB File Offset: 0x000161EB
	public int currentMissionSet
	{
		get
		{
			return this._currentMissionSet;
		}
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000502 RID: 1282 RVA: 0x00017FF3 File Offset: 0x000161F3
	public int lastMissionCompleted
	{
		get
		{
			return this._lastMissionCompleted;
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000503 RID: 1283 RVA: 0x00017FFB File Offset: 0x000161FB
	public int currentMissionSetCount
	{
		get
		{
			return this._currentMissionProgress.Length;
		}
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000504 RID: 1284 RVA: 0x00018008 File Offset: 0x00016208
	public int scoreMultiplier
	{
		get
		{
			int num = this._currentMissionSet + 1;
			if (this.doubleScore)
			{
				num *= 2;
			}
			return num;
		}
	}

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x06000505 RID: 1285 RVA: 0x0001802B File Offset: 0x0001622B
	public int rawMultiplier
	{
		get
		{
			return this._currentMissionSet + 1;
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06000506 RID: 1286 RVA: 0x00018035 File Offset: 0x00016235
	// (set) Token: 0x06000507 RID: 1287 RVA: 0x0001803D File Offset: 0x0001623D
	public int currentCharacter
	{
		get
		{
			return this._currentCharacter;
		}
		set
		{
			if (value != this._currentCharacter)
			{
				this._currentCharacter = value;
				this._dirty = true;
				this.Save();
			}
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06000508 RID: 1288 RVA: 0x0001805C File Offset: 0x0001625C
	public string dailyWord
	{
		get
		{
			return this._dailyWord;
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06000509 RID: 1289 RVA: 0x00018064 File Offset: 0x00016264
	public IntMask dailyWordUnlockedMask
	{
		get
		{
			return this._dailyWordUnlockedMask;
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001806C File Offset: 0x0001626C
	public DateTime dailyWordExpireTime
	{
		get
		{
			return this._dailyWordExpireTime;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x0600050B RID: 1291 RVA: 0x00018074 File Offset: 0x00016274
	public DateTime dailyWordPayedOutTime
	{
		get
		{
			return this._dailyWordPayedOutTime;
		}
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x0600050C RID: 1292 RVA: 0x0001807C File Offset: 0x0001627C
	// (set) Token: 0x0600050D RID: 1293 RVA: 0x00018084 File Offset: 0x00016284
	public bool tutorialCompleted
	{
		get
		{
			return this._tutorialCompleted;
		}
		set
		{
			this._tutorialCompleted = value;
			this._dirty = true;
			this.Save();
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001809A File Offset: 0x0001629A
	// (set) Token: 0x0600050F RID: 1295 RVA: 0x000180A2 File Offset: 0x000162A2
	public int inAppPurchaseCount
	{
		get
		{
			return this._inAppPurchaseCount;
		}
		set
		{
			this._inAppPurchaseCount = value;
			this._dirty = true;
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000510 RID: 1296 RVA: 0x000180B2 File Offset: 0x000162B2
	// (set) Token: 0x06000511 RID: 1297 RVA: 0x000180BA File Offset: 0x000162BA
	public string earnCurrenyData
	{
		get
		{
			return this._earnCurrenyData;
		}
		set
		{
			this._earnCurrenyData = value;
			this._dirty = true;
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000512 RID: 1298 RVA: 0x000180CA File Offset: 0x000162CA
	public float doubleScoreMultiplierDuration
	{
		get
		{
			return this.GetPowerupDuration(PowerupType.doubleMultiplier);
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06000513 RID: 1299 RVA: 0x000180D4 File Offset: 0x000162D4
	// (set) Token: 0x06000514 RID: 1300 RVA: 0x000180DC File Offset: 0x000162DC
	public bool doubleScore
	{
		get
		{
			return this._doubleScore;
		}
		set
		{
			if (value != this._doubleScore)
			{
				this._doubleScore = value;
				Action action = this.onScoreMultiplierChanged;
				if (action != null)
				{
					action();
				}
			}
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000515 RID: 1301 RVA: 0x00018109 File Offset: 0x00016309
	public static PlayerInfo Instance
	{
		get
		{
			PlayerInfo playerInfo;
			if ((playerInfo = PlayerInfo._instance) == null)
			{
				playerInfo = (PlayerInfo._instance = new PlayerInfo());
			}
			return playerInfo;
		}
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00018120 File Offset: 0x00016320
	private PlayerInfo()
	{
		this.Load();
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x000181D1 File Offset: 0x000163D1
	public void BragCompleted()
	{
		this._oldHighestScore = this._highestScore;
		this._dirty = true;
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x000181E6 File Offset: 0x000163E6
	public bool IsCurrentMissionSetInited()
	{
		return false;
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x000181EC File Offset: 0x000163EC
	public void InitCurrentMissionSet(int missionSet, int missionCount)
	{
		if (missionSet != this._currentMissionSet)
		{
			this._currentMissionSet = missionSet;
			this._currentMissionProgress = new int[missionCount];
			for (int i = 0; i < missionCount; i++)
			{
				this._currentMissionProgress[i] = 0;
			}
			this._dirty = true;
			Action action = this.onScoreMultiplierChanged;
			if (action != null)
			{
				action();
			}
		}
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00018244 File Offset: 0x00016444
	public void ReInitCurrentMissionSet(int missionSet, int missionCount)
	{
		this._currentMissionSet = missionSet;
		this._currentMissionProgress = new int[missionCount];
		for (int i = 0; i < missionCount; i++)
		{
			this._currentMissionProgress[i] = 0;
		}
		this._dirty = true;
		Action action = this.onScoreMultiplierChanged;
		if (action != null)
		{
			action();
		}
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00018290 File Offset: 0x00016490
	public int GetCurrentMissionProgress(int mission)
	{
		if (this._currentMissionProgress == null)
		{
			return 0;
		}
		if (mission < this._currentMissionProgress.Length)
		{
			return this._currentMissionProgress[mission];
		}
		return 0;
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x000182B1 File Offset: 0x000164B1
	public void SetCurrentMissionProgress(int mission, int progress)
	{
		if (this._currentMissionProgress[mission] != progress)
		{
			this._currentMissionProgress[mission] = progress;
			this._dirty = true;
		}
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x000182CE File Offset: 0x000164CE
	public bool IncrementCurrentMissionProgress(int mission, int target)
	{
		if (this._currentMissionProgress[mission] < target)
		{
			this._currentMissionProgress[mission]++;
			this._dirty = true;
			return this._currentMissionProgress[mission] == target;
		}
		return false;
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00018300 File Offset: 0x00016500
	public void CollectToken(CharacterModels.ModelType tokenType, int amount = 1)
	{
		this._collectedCharacterTokens[(int)tokenType] += amount;
		this._dirty = true;
		Action<CharacterModels.ModelType> onTokenCollected = this.OnTokenCollected;
		if (onTokenCollected != null)
		{
			onTokenCollected(tokenType);
		}
		this.Save();
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0001833C File Offset: 0x0001653C
	public bool IsCollectionComplete(CharacterModels.ModelType modelType)
	{
		CharacterModels.Model model = CharacterModels.modelData[modelType];
		return model.UnlockType == CharacterModels.UnlockType.free || this._collectedCharacterTokens[(int)modelType] >= model.Price;
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00018372 File Offset: 0x00016572
	public int GetCollectedTokens(CharacterModels.ModelType modelType)
	{
		return this._collectedCharacterTokens[(int)modelType];
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0001837C File Offset: 0x0001657C
	public void InitDailyWord(string word, DateTime expires)
	{
		if (!this._dailyWord.Equals(word) || !this._dailyWordExpireTime.Equals(expires))
		{
			this._dailyWord = word;
			this._dailyWordExpireTime = expires;
			this._dailyWordPayedOutTime = DateTime.UtcNow;
			this._dailyWordUnlockedMask = 0;
			this._dirty = true;
			this.Save();
		}
		DailyLetterPickupManager.Instance.UpdateLetter();
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x000183E4 File Offset: 0x000165E4
	public void PickedupLetter(char letter)
	{
		for (int i = 0; i < this._dailyWord.Length; i++)
		{
			if (this._dailyWord[i] == letter && !this._dailyWordUnlockedMask[i])
			{
				this._dailyWordUnlockedMask[i] = true;
				Action onPickedUpLetter = this.OnPickedUpLetter;
				if (onPickedUpLetter != null)
				{
					onPickedUpLetter();
				}
				this._dirty = true;
				this.Save();
				break;
			}
		}
		if (this.isDailyWordComplete() && this._dailyWordPayedOutTime != this._dailyWordExpireTime)
		{
			Missions.Instance.PlayerDidThis(Missions.MissionTarget.DailyQuests, 1);
			this._mysteryBoxesToUnlock++;
			this._dailyWordPayedOutTime = this._dailyWordExpireTime;
			this._dirty = true;
			this.Save();
			UIScreenController.Instance.QueueSlideIn(UIScreenController.SlideInType.LettersComplete, string.Empty);
			Flurry.LogEventWithAParameter("Daily Challenge completed", "Id", this._dailyWord);
		}
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x000184C4 File Offset: 0x000166C4
	public char GetNewDailyLetter()
	{
		for (int i = 0; i < this._dailyWord.Length; i++)
		{
			if (!this._dailyWordUnlockedMask[i])
			{
				return this._dailyWord[i];
			}
		}
		return '\0';
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00018503 File Offset: 0x00016703
	public bool isDailyWordComplete()
	{
		return (1 << this._dailyWord.Length) - 1 == this._dailyWordUnlockedMask;
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x00018524 File Offset: 0x00016724
	public int GetUpgradeAmount(PowerupType type)
	{
		return this._upgradeAmounts[type];
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x00018532 File Offset: 0x00016732
	public int GetCurrentTier(PowerupType type)
	{
		if (!this._upgradeTiers.ContainsKey(type))
		{
			return 0;
		}
		return this._upgradeTiers[type];
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x00018550 File Offset: 0x00016750
	public float GetPowerupDuration(PowerupType type)
	{
		if (!Upgrades.upgrades.ContainsKey(type))
		{
			Debug.Log("Couldn't find any upgrades of the type: " + type.ToString() + ". Returning 0");
			return 0f;
		}
		return Upgrades.upgrades[type].durations[this.GetCurrentTier(type)];
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x000185AC File Offset: 0x000167AC
	public void IncreasePowerupTier(PowerupType type)
	{
		if (this._upgradeTiers.ContainsKey(type))
		{
			Dictionary<PowerupType, int> upgradeTiers = this._upgradeTiers;
			upgradeTiers[type]++;
			this._dirty = true;
			this.Save();
			return;
		}
		Debug.LogError("Trying to increase tier for a non-tiered upgrade");
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x000185F8 File Offset: 0x000167F8
	public void UseUpgrade(PowerupType type)
	{
		Debug.Log("Used powerup: " + type.ToString());
		if (this._upgradeAmounts.ContainsKey(type))
		{
			Dictionary<PowerupType, int> upgradeAmounts;
			PowerupType powerupType;
			int num = (upgradeAmounts = this._upgradeAmounts)[powerupType = type];
			upgradeAmounts[powerupType] = num - 1;
			this._dirty = true;
			Action action = this.onPowerupAmountChanged;
			if (action != null)
			{
				action();
			}
			this.Save();
		}
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x0001866C File Offset: 0x0001686C
	public void IncreaseUpgradeAmount(PowerupType type, int amount = 1)
	{
		if (this._upgradeAmounts.ContainsKey(type))
		{
			Dictionary<PowerupType, int> upgradeAmounts;
			int num = (upgradeAmounts = this._upgradeAmounts)[type];
			upgradeAmounts[type] = num + amount;
			this._dirty = true;
			Action action = this.onPowerupAmountChanged;
			if (action != null)
			{
				action();
			}
			this.Save();
			return;
		}
		Debug.LogError("Trying to increase upgrade amount for a non-consumable");
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x000186D0 File Offset: 0x000168D0
	public int GetNumberOfAffordableUpgrades()
	{
		int num = 0;
		foreach (KeyValuePair<PowerupType, Upgrade> keyValuePair in Upgrades.upgrades)
		{
			if (keyValuePair.Value.numberOfTiers > 0)
			{
				int num2 = this.GetCurrentTier(keyValuePair.Key) + 1;
				if (num2 < Upgrades.upgrades[keyValuePair.Key].pricesRaw.Length && Upgrades.upgrades[keyValuePair.Key].getPrice(num2) <= this.amountOfCoins)
				{
					num++;
				}
			}
			else if (Upgrades.upgrades[keyValuePair.Key].pricesRaw != null && Upgrades.upgrades[keyValuePair.Key].pricesRaw.Length != 0 && Upgrades.upgrades[keyValuePair.Key].getPrice(0) <= this.amountOfCoins)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x000187D8 File Offset: 0x000169D8
	private static string GetSaveDataPath()
	{
		string text = Application.persistentDataPath + "/playerdata";
		Debug.Log("playerdata save data path: \"" + text + "\"");
		return text;
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x0001880C File Offset: 0x00016A0C
	public void Save()
	{
		try
		{
			MemoryStream memoryStream = new MemoryStream(8192);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(1);
			Dictionary<PlayerInfo.Key, string> dictionary = new Dictionary<PlayerInfo.Key, string>(15);
			dictionary[PlayerInfo.Key.AmountOfCoins] = this._amountOfCoins.ToString();
			dictionary[PlayerInfo.Key.HighestScore] = this._highestScore.ToString();
			dictionary[PlayerInfo.Key.OldHighestScore] = this._oldHighestScore.ToString();
			dictionary[PlayerInfo.Key.DailyWord] = this._dailyWord;
			dictionary[PlayerInfo.Key.DailyWordUnlockMask] = this._dailyWordUnlockedMask.ToString();
			dictionary[PlayerInfo.Key.DailyWordExpireTime] = this._dailyWordExpireTime.ToString();
			dictionary[PlayerInfo.Key.DailyWordPayedOutTime] = this._dailyWordPayedOutTime.ToString();
			dictionary[PlayerInfo.Key.CurrentCharacter] = this._currentCharacter.ToString();
			dictionary[PlayerInfo.Key.CurrentMissionSet] = this._currentMissionSet.ToString();
			dictionary[PlayerInfo.Key.AmountOfMysteryBoxesOpened] = this._amountOfMysteryBoxesOpened.ToString();
			dictionary[PlayerInfo.Key.TutorialCompleted] = this._tutorialCompleted.ToString();
			dictionary[PlayerInfo.Key.InAppPurchaseCount] = this._inAppPurchaseCount.ToString();
			dictionary[PlayerInfo.Key.EarnCurrencyData] = this._earnCurrenyData;
			if (this._currentMissionSet >= 0)
			{
				dictionary[PlayerInfo.Key.CurrentMissionSetProgress] = string.Join(",", Array.ConvertAll<int, string>(this._currentMissionProgress, (int input) => input.ToString()));
			}
			dictionary[PlayerInfo.Key.CollectedCharacterTokens] = string.Join(",", Array.ConvertAll<int, string>(this._collectedCharacterTokens, (int input) => input.ToString()));
			FileUtil.WriteEnumStringDictionary<PlayerInfo.Key>(binaryWriter, dictionary);
			FileUtil.WriteEnumIntDictionary<PowerupType>(binaryWriter, this._upgradeAmounts);
			FileUtil.WriteEnumIntDictionary<PowerupType>(binaryWriter, this._upgradeTiers);
			FileUtil.Save(PlayerInfo.GetSaveDataPath(), "we12rtyuiklhgfdjerKJGHfvghyuhnjiokLJHl145rtyfghjvbn", memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			memoryStream.Close();
			this._dirty = false;
		}
		catch (Exception ex)
		{
			Debug.LogError("Error saving player info: " + ex.ToString());
		}
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x00018A24 File Offset: 0x00016C24
	public void Load()
	{
		try
		{
			MemoryStream memoryStream = new MemoryStream(FileUtil.Load(PlayerInfo.GetSaveDataPath(), "we12rtyuiklhgfdjerKJGHfvghyuhnjiokLJHl145rtyfghjvbn"));
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			binaryReader.ReadInt32();
			Dictionary<PlayerInfo.Key, string> dictionary = FileUtil.ReadEnumStringDictionary<PlayerInfo.Key>(binaryReader);
			this._amountOfCoins = (dictionary.ContainsKey(PlayerInfo.Key.AmountOfCoins) ? int.Parse(dictionary[PlayerInfo.Key.AmountOfCoins]) : 0);
			this._highestScore = (dictionary.ContainsKey(PlayerInfo.Key.HighestScore) ? int.Parse(dictionary[PlayerInfo.Key.HighestScore]) : 0);
			this._oldHighestScore = (dictionary.ContainsKey(PlayerInfo.Key.OldHighestScore) ? int.Parse(dictionary[PlayerInfo.Key.HighestScore]) : 0);
			this._dailyWord = ((!dictionary.ContainsKey(PlayerInfo.Key.DailyWord)) ? string.Empty : dictionary[PlayerInfo.Key.DailyWord]);
			this._dailyWordUnlockedMask = (dictionary.ContainsKey(PlayerInfo.Key.DailyWordUnlockMask) ? int.Parse(dictionary[PlayerInfo.Key.DailyWordUnlockMask]) : 0);
			this._dailyWordExpireTime = ((!dictionary.ContainsKey(PlayerInfo.Key.DailyWordExpireTime)) ? DateTime.UtcNow : DateTime.Parse(dictionary[PlayerInfo.Key.DailyWordExpireTime]));
			this._dailyWordPayedOutTime = ((!dictionary.ContainsKey(PlayerInfo.Key.DailyWordPayedOutTime)) ? DateTime.UtcNow : DateTime.Parse(dictionary[PlayerInfo.Key.DailyWordPayedOutTime]));
			this._currentCharacter = (dictionary.ContainsKey(PlayerInfo.Key.CurrentCharacter) ? int.Parse(dictionary[PlayerInfo.Key.CurrentCharacter]) : 0);
			this._currentMissionSet = ((!dictionary.ContainsKey(PlayerInfo.Key.CurrentMissionSet)) ? (-1) : int.Parse(dictionary[PlayerInfo.Key.CurrentMissionSet]));
			this._amountOfMysteryBoxesOpened = (dictionary.ContainsKey(PlayerInfo.Key.AmountOfMysteryBoxesOpened) ? int.Parse(dictionary[PlayerInfo.Key.AmountOfMysteryBoxesOpened]) : 0);
			this._tutorialCompleted = dictionary.ContainsKey(PlayerInfo.Key.TutorialCompleted) && bool.Parse(dictionary[PlayerInfo.Key.TutorialCompleted]);
			this._inAppPurchaseCount = (dictionary.ContainsKey(PlayerInfo.Key.InAppPurchaseCount) ? int.Parse(dictionary[PlayerInfo.Key.InAppPurchaseCount]) : 0);
			this._earnCurrenyData = ((!dictionary.ContainsKey(PlayerInfo.Key.EarnCurrencyData)) ? string.Empty : dictionary[PlayerInfo.Key.EarnCurrencyData]);
			this._currentMissionProgress = null;
			if (dictionary.ContainsKey(PlayerInfo.Key.CurrentMissionSetProgress))
			{
				string text = dictionary[PlayerInfo.Key.CurrentMissionSetProgress];
				if (!string.IsNullOrEmpty(text))
				{
					this._currentMissionProgress = Array.ConvertAll<string, int>(text.Split(new char[] { ',' }), (string input) => int.Parse(input));
				}
			}
			if (dictionary.ContainsKey(PlayerInfo.Key.CollectedCharacterTokens))
			{
				string text2 = dictionary[PlayerInfo.Key.CollectedCharacterTokens];
				if (!string.IsNullOrEmpty(text2))
				{
					int[] array = Array.ConvertAll<string, int>(text2.Split(new char[] { ',' }), (string input) => int.Parse(input));
					int i = Mathf.Min(array.Length, this._collectedCharacterTokens.Length);
					Array.Copy(array, this._collectedCharacterTokens, i);
					while (i < this._collectedCharacterTokens.Length)
					{
						this._collectedCharacterTokens[i] = 0;
						i++;
					}
				}
			}
			foreach (KeyValuePair<PowerupType, int> keyValuePair in FileUtil.ReadEnumIntDictionary<PowerupType>(binaryReader))
			{
				this._upgradeAmounts[keyValuePair.Key] = keyValuePair.Value;
			}
			foreach (KeyValuePair<PowerupType, int> keyValuePair2 in FileUtil.ReadEnumIntDictionary<PowerupType>(binaryReader))
			{
				this._upgradeTiers[keyValuePair2.Key] = keyValuePair2.Value;
			}
			memoryStream.Close();
			this._dirty = false;
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Error loading player info: " + ex.ToString());
			this.InitNew();
		}
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x00018DF4 File Offset: 0x00016FF4
	public void InitNew()
	{
		this._amountOfCoins = 0;
		this._highestScore = 0;
		this._dailyWord = string.Empty;
		this._dailyWordUnlockedMask = 0;
		this._dailyWordExpireTime = DateTime.UtcNow;
		this._dailyWordPayedOutTime = DateTime.UtcNow;
		this._amountOfMysteryBoxesOpened = 0;
		this._currentCharacter = 0;
		this._currentMissionSet = -1;
		this._currentMissionProgress = null;
		this._tutorialCompleted = false;
		this._inAppPurchaseCount = 0;
		this._earnCurrenyData = string.Empty;
		for (int i = 0; i < this._collectedCharacterTokens.Length; i++)
		{
			this._collectedCharacterTokens[i] = 0;
		}
		Dictionary<PowerupType, int> dictionary = new Dictionary<PowerupType, int>(this._upgradeAmounts.Count);
		foreach (PowerupType powerupType in this._upgradeAmounts.Keys)
		{
			if (powerupType == PowerupType.hoverboard)
			{
				dictionary[powerupType] = 3;
			}
			else
			{
				dictionary[powerupType] = 0;
			}
		}
		this._upgradeAmounts = dictionary;
		dictionary = new Dictionary<PowerupType, int>(this._upgradeTiers.Count);
		foreach (PowerupType powerupType2 in this._upgradeTiers.Keys)
		{
			dictionary[powerupType2] = 0;
		}
		this._upgradeTiers = dictionary;
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x00018F64 File Offset: 0x00017164
	public float GetHoverBoardCoolDown()
	{
		return 5f;
	}

	// Token: 0x0400042F RID: 1071
	private const string SECRET = "we12rtyuiklhgfdjerKJGHfvghyuhnjiokLJHl145rtyfghjvbn";

	// Token: 0x04000430 RID: 1072
	private const int VERSION = 1;

	// Token: 0x04000431 RID: 1073
	private bool _dirty;

	// Token: 0x04000432 RID: 1074
	public Action onCoinsChanged;

	// Token: 0x04000433 RID: 1075
	private int _amountOfCoins;

	// Token: 0x04000434 RID: 1076
	private int _highestScore;

	// Token: 0x04000435 RID: 1077
	private int _oldHighestScore;

	// Token: 0x04000436 RID: 1078
	private int _highestMeters;

	// Token: 0x04000437 RID: 1079
	private int _amountOfMysteryBoxesOpened;

	// Token: 0x04000438 RID: 1080
	private int _mysteryBoxesToUnlock;

	// Token: 0x04000439 RID: 1081
	private int _lastMissionCompleted = -1;

	// Token: 0x0400043A RID: 1082
	private int _currentMissionSet = -1;

	// Token: 0x0400043B RID: 1083
	private int[] _currentMissionProgress;

	// Token: 0x0400043C RID: 1084
	public Action onScoreMultiplierChanged;

	// Token: 0x0400043D RID: 1085
	private int _currentCharacter;

	// Token: 0x0400043E RID: 1086
	public Action<CharacterModels.ModelType> OnTokenCollected;

	// Token: 0x0400043F RID: 1087
	private int[] _collectedCharacterTokens = new int[CharacterModels.modelData.Count];

	// Token: 0x04000440 RID: 1088
	private string _dailyWord = string.Empty;

	// Token: 0x04000441 RID: 1089
	private IntMask _dailyWordUnlockedMask;

	// Token: 0x04000442 RID: 1090
	private DateTime _dailyWordExpireTime;

	// Token: 0x04000443 RID: 1091
	private DateTime _dailyWordPayedOutTime;

	// Token: 0x04000444 RID: 1092
	public Action OnPickedUpLetter;

	// Token: 0x04000445 RID: 1093
	private bool _tutorialCompleted;

	// Token: 0x04000446 RID: 1094
	private int _inAppPurchaseCount;

	// Token: 0x04000447 RID: 1095
	private string _earnCurrenyData = string.Empty;

	// Token: 0x04000448 RID: 1096
	public Action onPowerupAmountChanged;

	// Token: 0x04000449 RID: 1097
	private Dictionary<PowerupType, int> _upgradeAmounts = new Dictionary<PowerupType, int>
	{
		{
			PowerupType.hoverboard,
			0
		},
		{
			PowerupType.headstart500,
			0
		},
		{
			PowerupType.headstart2000,
			0
		},
		{
			PowerupType.mysterybox,
			0
		}
	};

	// Token: 0x0400044A RID: 1098
	private Dictionary<PowerupType, int> _upgradeTiers = new Dictionary<PowerupType, int>
	{
		{
			PowerupType.jetpack,
			0
		},
		{
			PowerupType.supersneakers,
			0
		},
		{
			PowerupType.coinmagnet,
			0
		},
		{
			PowerupType.letters,
			0
		},
		{
			PowerupType.doubleMultiplier,
			4
		}
	};

	// Token: 0x0400044B RID: 1099
	private bool _doubleScore;

	// Token: 0x0400044C RID: 1100
	private static PlayerInfo _instance;

	// Token: 0x020001CA RID: 458
	private enum Key
	{
		// Token: 0x04000ACB RID: 2763
		AmountOfCoins,
		// Token: 0x04000ACC RID: 2764
		OldHighestScore,
		// Token: 0x04000ACD RID: 2765
		HighestScore,
		// Token: 0x04000ACE RID: 2766
		DailyWord,
		// Token: 0x04000ACF RID: 2767
		DailyWordUnlockMask,
		// Token: 0x04000AD0 RID: 2768
		DailyWordExpireTime,
		// Token: 0x04000AD1 RID: 2769
		DailyWordPayedOutTime,
		// Token: 0x04000AD2 RID: 2770
		CurrentCharacter,
		// Token: 0x04000AD3 RID: 2771
		CurrentMissionSet,
		// Token: 0x04000AD4 RID: 2772
		CurrentMissionSetProgress,
		// Token: 0x04000AD5 RID: 2773
		CollectedCharacterTokens,
		// Token: 0x04000AD6 RID: 2774
		AmountOfMysteryBoxesOpened,
		// Token: 0x04000AD7 RID: 2775
		TutorialCompleted,
		// Token: 0x04000AD8 RID: 2776
		InAppPurchaseCount,
		// Token: 0x04000AD9 RID: 2777
		EarnCurrencyData,
		// Token: 0x04000ADA RID: 2778
		Count
	}
}
