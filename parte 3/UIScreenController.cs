using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class UIScreenController : MonoBehaviour
{
	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0002E1DE File Offset: 0x0002C3DE
	public static UIScreenController Instance
	{
		get
		{
			UIScreenController uiscreenController;
			if ((uiscreenController = UIScreenController._instance) == null)
			{
				uiscreenController = (UIScreenController._instance = Object.FindObjectOfType(typeof(UIScreenController)) as UIScreenController);
			}
			return uiscreenController;
		}
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x0002E204 File Offset: 0x0002C404
	private void Awake()
	{
		Missions instance = Missions.Instance;
		instance.onMissionComplete = (Missions.MissionCompleteHandler)Delegate.Combine(instance.onMissionComplete, new Missions.MissionCompleteHandler(this.OnMissionCompleted));
		Missions instance2 = Missions.Instance;
		instance2.onMissionSetComplete = (Missions.MissionSetCompleteHandler)Delegate.Combine(instance2.onMissionSetComplete, new Missions.MissionSetCompleteHandler(this.OnMissionSetCompleted));
		PlayerInfo instance3 = PlayerInfo.Instance;
		instance3.OnPickedUpLetter = (Action)Delegate.Combine(instance3.OnPickedUpLetter, new Action(this.OnLetterPickedUp));
		PlayerInfo instance4 = PlayerInfo.Instance;
		instance4.OnTokenCollected = (Action<CharacterModels.ModelType>)Delegate.Combine(instance4.OnTokenCollected, new Action<CharacterModels.ModelType>(this.OnTokenPickUp));
		ChartBoost.didDismissInterstitial = (Action)Delegate.Combine(ChartBoost.didDismissInterstitial, new Action(this.OnChartBoostDidDismissInterstitial));
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x0002E2CC File Offset: 0x0002C4CC
	private void onDestroy()
	{
		Missions instance = Missions.Instance;
		instance.onMissionComplete = (Missions.MissionCompleteHandler)Delegate.Remove(instance.onMissionComplete, new Missions.MissionCompleteHandler(this.OnMissionCompleted));
		Missions instance2 = Missions.Instance;
		instance2.onMissionSetComplete = (Missions.MissionSetCompleteHandler)Delegate.Remove(instance2.onMissionSetComplete, new Missions.MissionSetCompleteHandler(this.OnMissionSetCompleted));
		PlayerInfo instance3 = PlayerInfo.Instance;
		instance3.OnPickedUpLetter = (Action)Delegate.Remove(instance3.OnPickedUpLetter, new Action(this.OnLetterPickedUp));
		PlayerInfo instance4 = PlayerInfo.Instance;
		instance4.OnTokenCollected = (Action<CharacterModels.ModelType>)Delegate.Remove(instance4.OnTokenCollected, new Action<CharacterModels.ModelType>(this.OnTokenPickUp));
		ChartBoost.didDismissInterstitial = (Action)Delegate.Remove(ChartBoost.didDismissInterstitial, new Action(this.OnChartBoostDidDismissInterstitial));
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x0002E391 File Offset: 0x0002C591
	private void Start()
	{
		this.HideInAppPurchaseOverlay();
		if (this.LoadMenuOnStart)
		{
			this.ShowMainMenu();
		}
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x0002E3A7 File Offset: 0x0002C5A7
	private void OnApplicationPause(bool paused)
	{
		if (paused)
		{
			if (this._screenStack.Count > 0 && this._screenStack.Peek() == "IngameUI")
			{
				this.PushScreen(null, "PauseUI");
			}
			PlayerInfo.Instance.Save();
		}
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x0002E3E8 File Offset: 0x0002C5E8
	public void FacebookLogIn(bool loggedIn)
	{
		if (this._screenStack.Count <= 0)
		{
			return;
		}
		if (this._screenStack.Peek() == "FriendsUI_offline")
		{
			if (loggedIn)
			{
				this._SwitchScreen("FriendsUI_online");
				return;
			}
		}
		else if (this._screenStack.Peek() == "FriendsUI_online")
		{
			if (loggedIn)
			{
				this._SwitchScreen("FriendsUI_online");
				return;
			}
		}
		else if (this._screenStack.Peek() == "LeaderboardUI_online")
		{
			if (loggedIn)
			{
				this._SwitchScreen("LeaderboardUI_online");
				return;
			}
		}
		else if (this._screenStack.Peek() == "LeaderboardUI_offline")
		{
			if (loggedIn)
			{
				this._SwitchScreen("LeaderboardUI_online");
				return;
			}
		}
		else if (this._screenStack.Peek() == "GameoverUI" && loggedIn)
		{
			this._cachedScreens["GameoverUI"].GetComponent<UIGameOverHelper>().FacebookLoggedIn();
		}
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x0002E4D8 File Offset: 0x0002C6D8
	public void ShowMainMenu()
	{
		string text = "FrontUI";
		this._ActivateScreen(text);
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x0002E4F4 File Offset: 0x0002C6F4
	public void GameOverTriggered()
	{
		Missions.Instance.inRun = false;
		string text = "GameoverUI";
		this._ActivateScreen(text);
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x0002E519 File Offset: 0x0002C719
	public void QueueMessage(string message)
	{
		Debug.Log("Showing message: " + message);
		this._QueueMessage(message);
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0002E532 File Offset: 0x0002C732
	public void GoToMainMenuFromGame(GameObject sender)
	{
		if (Game.Instance != null)
		{
			Missions.Instance.inRun = false;
			Game.Instance.StartTopMenu();
			Game.Instance.TriggerPause(false);
		}
		this._ActivateScreen("FrontUI");
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x0002E56C File Offset: 0x0002C76C
	public void PushScreen(GameObject sender)
	{
		this.PushScreen(sender, string.Empty);
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0002E57C File Offset: 0x0002C77C
	public void PushScreen(GameObject sender, string screenOverride = "")
	{
		string text = string.Empty;
		text = ((!(screenOverride != string.Empty)) ? sender.GetComponent<UIButtonChangeScreen>().ScreenNameToOpen : screenOverride);
		if (this._screenNamesWithOnlineVersion.Contains(text))
		{
			text = ((!SocialManager.instance.consolidatedFriendsCompleted) ? (text + "_offline") : (text + "_online"));
		}
		this._ActivateScreen(text);
		if (text == "IngameUI")
		{
			this._cachedScreens[text].GetComponent<UIIngameUpdater>().TriggerInGameUI();
			if (!UIIngameUpdater.isCountingDown())
			{
				Missions.Instance.inRun = true;
				return;
			}
		}
		else if (text == "PauseUI" && Game.Instance != null)
		{
			Game.Instance.TriggerPause(true);
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0002E640 File Offset: 0x0002C840
	public void SwitchScreen(GameObject sender)
	{
		this.SwitchScreen(sender, string.Empty);
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x0002E650 File Offset: 0x0002C850
	public void SwitchScreen(GameObject sender, string screenOverride = "")
	{
		string text = string.Empty;
		text = ((!(screenOverride != string.Empty)) ? sender.GetComponent<UIButtonChangeScreen>().ScreenNameToOpen : screenOverride);
		if (this._screenNamesWithOnlineVersion.Contains(text) && this._screenNamesWithOnlineVersion.Contains(text))
		{
			text = ((!SocialManager.instance.consolidatedFriendsCompleted) ? (text + "_offline") : (text + "_online"));
		}
		this._SwitchScreen(text);
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x0002E6C7 File Offset: 0x0002C8C7
	public void BackToPrevious()
	{
		this._BackToPreviousScreen();
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x0002E6D0 File Offset: 0x0002C8D0
	public void QueuePopup(GameObject sender)
	{
		string screenNameToOpen = sender.GetComponent<UIButtonChangeScreen>().ScreenNameToOpen;
		this._QueuePopup(screenNameToOpen);
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0002E6F0 File Offset: 0x0002C8F0
	public void QueueMysteryBox()
	{
		string text = string.Empty;
		if (this._popupQueue.Count > 0)
		{
			Debug.Log("Peeking at popup queue: " + this._popupQueue.Peek());
			text = this._popupQueue.Peek();
			this._RemovePopup();
		}
		this._QueuePopup("MysteryBoxPopup");
		if (text != string.Empty)
		{
			this._QueuePopup(text);
			Debug.Log("Queueing " + text);
		}
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0002E76C File Offset: 0x0002C96C
	public void ClosePopup()
	{
		this._RemovePopup();
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0002E774 File Offset: 0x0002C974
	public void SpawnCollectText(Vector3 startPosition, string text)
	{
		UILabel uilabel = NGUITools.AddWidget<UILabel>(this.superPopupAnchor);
		uilabel.text = text;
		uilabel.transform.position = new Vector3(startPosition.x, startPosition.y, uilabel.cachedTransform.position.z);
		uilabel.font = this.FloatingTextFont;
		uilabel.color = new Color(0.98039216f, 0.7764706f, 0.23529412f, 0f);
		uilabel.cachedTransform.localScale = new Vector3(17f, 17f, 1f);
		base.StartCoroutine(this.AnimateCollectText(uilabel));
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x0002E818 File Offset: 0x0002CA18
	private IEnumerator AnimateCollectText(UILabel collectText)
	{
		Vector3 localPosition = collectText.transform.localPosition;
		Vector3 toLocalPosition = new Vector3(localPosition.x, localPosition.y + 50f, localPosition.z);
		yield return base.StartCoroutine(this.AnimateAlpha(collectText, 0.1f, 1f));
		base.StartCoroutine(this.MoveTransform(collectText.cachedTransform, 1f, toLocalPosition));
		yield return new WaitForSeconds(0.8f);
		base.StartCoroutine(this.AnimateAlpha(collectText, 0.2f, 0f));
		yield return new WaitForSeconds(0.25f);
		Object.Destroy(collectText.gameObject);
		yield break;
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x0002E82E File Offset: 0x0002CA2E
	private IEnumerator AnimateAlpha(UILabel label, float duration, float toAlpha)
	{
		float fromAlpha = label.alpha;
		float factor2 = 0f;
		while (factor2 < 1f)
		{
			factor2 += Time.deltaTime / duration;
			factor2 = Mathf.Clamp01(factor2);
			label.alpha = Mathf.Lerp(fromAlpha, toAlpha, factor2);
			yield return null;
		}
		yield break;
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x0002E84B File Offset: 0x0002CA4B
	private IEnumerator MoveTransform(Transform trans, float duration, Vector3 toPos)
	{
		Vector3 fromPos = trans.localPosition;
		float factor2 = 0f;
		while (factor2 < 1f)
		{
			factor2 += Time.deltaTime / duration;
			factor2 = Mathf.Clamp01(factor2);
			trans.localPosition = Vector3.Lerp(fromPos, toPos, factor2);
			yield return null;
		}
		yield break;
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x0002E868 File Offset: 0x0002CA68
	private void _ActivateScreen(string screenName)
	{
		if (this._cachedScreens.ContainsKey(screenName))
		{
			GameObject gameObject = this._cachedScreens[screenName];
			gameObject.SetActiveRecursively(true);
			gameObject.BroadcastMessage("CreatePanel", SendMessageOptions.DontRequireReceiver);
			if (this._screenStack.Contains(screenName))
			{
				while (this._screenStack.Contains(screenName))
				{
					if (!(this._screenStack.Peek() != screenName))
					{
						break;
					}
					string text = this._screenStack.Pop();
					this._cachedScreens[text].SetActiveRecursively(false);
				}
			}
			else
			{
				this._cachedScreens[this._screenStack.Peek()].SetActiveRecursively(false);
				this._screenStack.Push(screenName);
			}
		}
		else
		{
			GameObject gameObject2 = Resources.Load("Prefabs/Screens/" + screenName, typeof(GameObject)) as GameObject;
			GameObject gameObject3 = NGUITools.AddChild(this.overlayAnchor, gameObject2);
			this._cachedScreens.Add(screenName, gameObject3);
			if (this._screenStack.Count > 0)
			{
				this._cachedScreens[this._screenStack.Peek()].SetActiveRecursively(false);
			}
			this._screenStack.Push(screenName);
		}
		UIModelController.Instance.ClearModels();
		if (screenName != null)
		{
			if (!(screenName == "GameoverUI"))
			{
				if (!(screenName == "CharacterUI"))
				{
					if (screenName == "FriendsUI_online" || screenName == "LeaderboardUI_online")
					{
						Debug.Log("Getting an online screen!");
						this._cachedScreens[screenName].GetComponent<UISocialScreen>().ReloadFriends();
					}
				}
				else
				{
					UIModelController.Instance.ActivateCharacterModel();
				}
			}
			else
			{
				UIModelController.Instance.ActivateGameOverModel();
				this._cachedScreens[screenName].GetComponent<UIGameOverHelper>().SetupBeforeMysteryBox();
				if (PlayerInfo.Instance.mysteryBoxesToUnlock != 0)
				{
					this._QueuePopup("MysteryBoxPopup");
				}
				else
				{
					this._cachedScreens[screenName].GetComponent<UIGameOverHelper>().SetupAfterMysteryBox();
				}
			}
		}
		this.SetBackground(!this._screenNamesWithoutBackground.Contains(screenName));
		Action<bool> onChangedScreen = this.OnChangedScreen;
		if (onChangedScreen != null)
		{
			onChangedScreen(screenName == "FrontUI");
		}
		this.ScreenDidChange(screenName);
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x0002EA90 File Offset: 0x0002CC90
	private void _SwitchScreen(string screenName)
	{
		string text = this._screenStack.Pop();
		this._cachedScreens[text].SetActiveRecursively(false);
		this._ActivateScreen(screenName);
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x0002EAC4 File Offset: 0x0002CCC4
	private void _BackToPreviousScreen()
	{
		if (this._screenStack.Count > 1)
		{
			string text = this._screenStack.Pop();
			this._cachedScreens[text].SetActiveRecursively(false);
			text = this._screenStack.Peek();
			this._cachedScreens[text].SetActiveRecursively(true);
			this.SetBackground(!this._screenNamesWithoutBackground.Contains(text));
			this.ScreenDidChange(text);
			return;
		}
		Debug.LogError("Tried to remove the only screen in the stack. You dun goofed.", this);
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0002EB44 File Offset: 0x0002CD44
	private void ScreenDidChange(string newScreenName)
	{
		this.messageHelper.SetTemporaryHidden(newScreenName != "IngameUI");
		Flurry.LogEvent("UI Screen " + newScreenName);
		if (PlayerInfo.Instance.inAppPurchaseCount <= 0)
		{
			if (UIScreenController.CHARTBOOST_ALLOWED_SCREENS.Contains(newScreenName))
			{
				DateTime dateTime;
				if (PlayerPrefs.HasKey("cb_alwnxt_ticks"))
				{
					long ticks;
					if (!long.TryParse(PlayerPrefs.GetString("cb_alwnxt_ticks"), out ticks))
					{
						ticks = DateTime.Now.Ticks;
					}
					dateTime = new DateTime(ticks);
				}
				else
				{
					dateTime = DateTime.Now + new TimeSpan(24, 0, 0);
					PlayerPrefs.SetString("cb_alwnxt_ticks", dateTime.Ticks.ToString());
				}
				DateTime now = DateTime.Now;
				bool flag = false;
				if (now >= dateTime)
				{
					flag = true;
				}
				if (flag || ChartBoost.isInitialized)
				{
					if (!ChartBoost.isInitialized)
					{
						ChartBoost.InitAndStartSession("4fa7c657f77659a92b000006", "e96e642c8ff398581d031532eda34c4617ba11fe");
					}
					ChartBoost.SetShouldRequestInterstitial(flag);
					ChartBoost.SetShouldDisplayInterstitial(true);
					ChartBoost.ShowInterstitial();
					return;
				}
			}
			else if (ChartBoost.isInitialized)
			{
				ChartBoost.SetShouldRequestInterstitial(false);
				ChartBoost.SetShouldDisplayInterstitial(false);
				return;
			}
		}
		else if (ChartBoost.isInitialized)
		{
			ChartBoost.SetShouldRequestInterstitial(false);
			ChartBoost.SetShouldDisplayInterstitial(false);
		}
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0002EC68 File Offset: 0x0002CE68
	private void OnChartBoostDidDismissInterstitial()
	{
		PlayerPrefs.SetString("cb_alwnxt_ticks", (DateTime.Now + new TimeSpan(0, 0, 0)).Ticks.ToString());
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x0002ECA1 File Offset: 0x0002CEA1
	private void _QueuePopup(string name)
	{
		this._popupQueue.Enqueue(name);
		if (!this._popupActive)
		{
			this._ActivateNextPopup();
		}
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x0002ECC0 File Offset: 0x0002CEC0
	private void _ActivateNextPopup()
	{
		if (this._popupQueue.Count > 0)
		{
			this._PauseAnimations(true, this.MenuElements3D.transform);
			NGUITools.SetActive(this.MenuElements3D, false);
			string text = this._popupQueue.Peek();
			if (!this._cachedScreens.ContainsKey(text))
			{
				GameObject gameObject = Resources.Load("Prefabs/Popups/" + text, typeof(GameObject)) as GameObject;
				GameObject gameObject2 = NGUITools.AddChild(this.popupAnchor, gameObject);
				this._cachedScreens.Add(text, gameObject2);
			}
			this._cachedScreens[text].SetActiveRecursively(true);
			if (text == "MysteryBoxPopup")
			{
				if (this.mainCamera == null)
				{
					this.mainCamera = Camera.main;
				}
				this.mainCamera.enabled = false;
				this._cachedScreens[text].GetComponent<MysteryBoxHandler>().SetupMysteryBoxScreen();
			}
			else if (text == "BragPopup")
			{
				this._cachedScreens[text].GetComponent<BragPopupHandler>().SetupBragPopup();
			}
			this._cachedScreens[text].BroadcastMessage("CreatePanel", SendMessageOptions.DontRequireReceiver);
			this._popupActive = true;
			Action<bool> onChangedScreen = this.OnChangedScreen;
			if (onChangedScreen != null)
			{
				onChangedScreen(false);
				return;
			}
		}
		else
		{
			NGUITools.SetActive(this.MenuElements3D, true);
			this._PauseAnimations(false, this.MenuElements3D.transform);
		}
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x0002EE20 File Offset: 0x0002D020
	private void _PauseAnimations(bool pause, Transform trans)
	{
		foreach (object obj in trans)
		{
			Transform transform = (Transform)obj;
			this._PauseAnimations(pause, transform);
		}
		if (trans.GetComponent<CharacterModel>() != null)
		{
			if (pause)
			{
				trans.GetComponent<CharacterModel>().StopIdleAnimations();
				return;
			}
			trans.GetComponent<CharacterModel>().StartIdleAnimations();
		}
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
	private void _RemovePopup()
	{
		if (this._popupQueue.Count < 1)
		{
			return;
		}
		string text = this._popupQueue.Dequeue();
		this._cachedScreens[text].SetActiveRecursively(false);
		this._popupActive = false;
		Action<bool> onChangedScreen = this.OnChangedScreen;
		if (onChangedScreen != null)
		{
			onChangedScreen(true);
		}
		this._ActivateNextPopup();
		if (text == "MysteryBoxPopup")
		{
			if (this.mainCamera != null)
			{
				this.mainCamera.enabled = true;
			}
			if (this._screenStack.Peek() == "GameoverUI")
			{
				this._cachedScreens[this._screenStack.Peek()].GetComponent<UIGameOverHelper>().SetupAfterMysteryBox();
			}
		}
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x0002EF58 File Offset: 0x0002D158
	private void SetBackground(bool state)
	{
		string text = "NotebookPanel";
		if (state)
		{
			if (!this._cachedScreens.ContainsKey(text))
			{
				GameObject gameObject = Resources.Load("Prefabs/Screens/" + text, typeof(GameObject)) as GameObject;
				GameObject gameObject2 = NGUITools.AddChild(this.backgroundAnchor, gameObject);
				this._cachedScreens.Add(text, gameObject2);
			}
			this._cachedScreens[text].SetActiveRecursively(true);
			this._cachedScreens[text].BroadcastMessage("CreatePanel", SendMessageOptions.DontRequireReceiver);
			return;
		}
		if (this._cachedScreens.ContainsKey(text))
		{
			this._cachedScreens[text].SetActiveRecursively(false);
		}
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0002F000 File Offset: 0x0002D200
	private void OnMissionCompleted(string message)
	{
		Debug.Log(message);
		this.QueueSlideIn(UIScreenController.SlideInType.Mission, message);
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x0002F010 File Offset: 0x0002D210
	private void OnMissionSetCompleted()
	{
		Debug.Log("Mission Set Completed, increase multiplier");
		this.QueueSlideIn(UIScreenController.SlideInType.MissionSet, string.Empty);
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0002F028 File Offset: 0x0002D228
	private void OnLetterPickedUp()
	{
		this.QueueSlideIn(UIScreenController.SlideInType.Letters, string.Empty);
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x0002F038 File Offset: 0x0002D238
	private void OnTokenPickUp(CharacterModels.ModelType type)
	{
		CharacterModels.Model model = CharacterModels.modelData[type];
		if (PlayerInfo.Instance.GetCollectedTokens(type) == model.Price)
		{
			this.QueueSlideIn(UIScreenController.SlideInType.Character, model.ModelName);
			Debug.Log("Queue character slidein");
			Flurry.LogEventWithAParameter("Character unlocked", "Id", type.ToString());
		}
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x0002F098 File Offset: 0x0002D298
	public void QueueSlideIn(UIScreenController.SlideInType type, string payload = "")
	{
		UIScreenController.SlideIn slideIn = new UIScreenController.SlideIn();
		slideIn.type = type;
		slideIn.payload = payload;
		this._slideInQueue.Enqueue(slideIn);
		if (!this.slideInActive)
		{
			this._ShowSlideIn();
		}
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x0002F0D3 File Offset: 0x0002D2D3
	public void ReadyForNextSlide()
	{
		this.slideInActive = false;
		if (!this.slideInActive)
		{
			this._ShowSlideIn();
		}
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x0002F0EC File Offset: 0x0002D2EC
	private void _ShowSlideIn()
	{
		if (this._slideInQueue.Count > 0)
		{
			UIScreenController.SlideIn slideIn = this._slideInQueue.Dequeue();
			if (slideIn.type == UIScreenController.SlideInType.Mission)
			{
				So.Instance.playSound(this.slideInFanfare);
				this.missionSlideIn.SetupSlideInMission(slideIn.payload);
			}
			else if (slideIn.type == UIScreenController.SlideInType.MissionSet)
			{
				So.Instance.playSound(this.slideInFanfare);
				this.missionSetSlideIn.SetupSlideInMissionSet(PlayerInfo.Instance.rawMultiplier);
			}
			else if (slideIn.type == UIScreenController.SlideInType.Letters)
			{
				So.Instance.playSound(this.slideInSound);
				this.lettersSlideIn.SetupLetters();
			}
			else if (slideIn.type == UIScreenController.SlideInType.Character)
			{
				So.Instance.playSound(this.slideInSound);
				this.characterSlideIn.SetupSlideInCharacter(slideIn.payload);
			}
			else if (slideIn.type == UIScreenController.SlideInType.LettersComplete)
			{
				So.Instance.playSound(this.slideInFanfare);
				this.lettersCompleteSlideIn.SetupSlideIn();
			}
			this.slideInActive = true;
		}
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x0002F1F6 File Offset: 0x0002D3F6
	public void ReadyForNextMessage()
	{
		this.messageShowing = false;
		this._ShowNextMessage();
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0002F205 File Offset: 0x0002D405
	private void _QueueMessage(string message)
	{
		this._messageQueue.Enqueue(message);
		if (!this.messageShowing)
		{
			this._ShowNextMessage();
		}
		if (!this.slideInActive)
		{
			this._ShowSlideIn();
		}
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0002F230 File Offset: 0x0002D430
	private void _ShowNextMessage()
	{
		if (this._messageQueue.Count > 0)
		{
			string text = this._messageQueue.Dequeue();
			this.messageHelper.ShowMessage(text);
			this.messageShowing = true;
		}
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x0002F26A File Offset: 0x0002D46A
	public void ShowInAppPurchaseOverlay()
	{
		this.inAppPurchaseOverlay.SetActiveRecursively(true);
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x0002F278 File Offset: 0x0002D478
	public void HideInAppPurchaseOverlay()
	{
		this.inAppPurchaseOverlay.SetActiveRecursively(false);
	}

	// Token: 0x0400079B RID: 1947
	private const string CHARTBOOST_APPID = "4fa7c657f77659a92b000006";

	// Token: 0x0400079C RID: 1948
	private const string CHARTBOOST_APPSIGNATURE = "e96e642c8ff398581d031532eda34c4617ba11fe";

	// Token: 0x0400079D RID: 1949
	private const string CHARTBOOST_ALLOWNEXT_TICKS_KEY = "cb_alwnxt_ticks";

	// Token: 0x0400079E RID: 1950
	private const int CHARTBOOST_FIRSTTIME_DELAY_HOURS = 24;

	// Token: 0x0400079F RID: 1951
	private const int CHARTBOOST_FIRSTTIME_DELAY_MINUTES = 0;

	// Token: 0x040007A0 RID: 1952
	private const int CHARTBOOST_DELAY_HOURS = 0;

	// Token: 0x040007A1 RID: 1953
	private const int CHARTBOOST_DELAY_MINUTES = 0;

	// Token: 0x040007A2 RID: 1954
	private const string INGAMEUI_SCREENNAME = "IngameUI";

	// Token: 0x040007A3 RID: 1955
	private static UIScreenController _instance;

	// Token: 0x040007A4 RID: 1956
	public GameObject backgroundAnchor;

	// Token: 0x040007A5 RID: 1957
	public GameObject overlayAnchor;

	// Token: 0x040007A6 RID: 1958
	public GameObject popupAnchor;

	// Token: 0x040007A7 RID: 1959
	public GameObject superPopupAnchor;

	// Token: 0x040007A8 RID: 1960
	public GameObject MenuElements3D;

	// Token: 0x040007A9 RID: 1961
	public bool LoadMenuOnStart;

	// Token: 0x040007AA RID: 1962
	public UIFont FloatingTextFont;

	// Token: 0x040007AB RID: 1963
	private Camera mainCamera;

	// Token: 0x040007AC RID: 1964
	public Action<bool> OnChangedScreen;

	// Token: 0x040007AD RID: 1965
	private static readonly List<string> CHARTBOOST_ALLOWED_SCREENS = new List<string> { "FrontUI" };

	// Token: 0x040007AE RID: 1966
	private Dictionary<string, GameObject> _cachedScreens = new Dictionary<string, GameObject>();

	// Token: 0x040007AF RID: 1967
	private Stack<string> _screenStack = new Stack<string>(15);

	// Token: 0x040007B0 RID: 1968
	private Queue<string> _popupQueue = new Queue<string>(15);

	// Token: 0x040007B1 RID: 1969
	private bool _popupActive;

	// Token: 0x040007B2 RID: 1970
	private List<string> _screenNamesWithoutBackground = new List<string> { "FrontUI", "IngameUI", "MysteryBoxUI" };

	// Token: 0x040007B3 RID: 1971
	private List<string> _screenNamesWithOnlineVersion = new List<string> { "LeaderboardUI", "FriendsUI" };

	// Token: 0x040007B4 RID: 1972
	public UISlideInMissionHelper missionSlideIn;

	// Token: 0x040007B5 RID: 1973
	public UISlideInMissionSetHelper missionSetSlideIn;

	// Token: 0x040007B6 RID: 1974
	public UISlideInLettersHelper lettersSlideIn;

	// Token: 0x040007B7 RID: 1975
	public UISlideInCharacterUnlock characterSlideIn;

	// Token: 0x040007B8 RID: 1976
	public UISlideIn lettersCompleteSlideIn;

	// Token: 0x040007B9 RID: 1977
	private Queue<UIScreenController.SlideIn> _slideInQueue = new Queue<UIScreenController.SlideIn>(15);

	// Token: 0x040007BA RID: 1978
	private bool slideInActive;

	// Token: 0x040007BB RID: 1979
	public AudioClipInfo slideInSound;

	// Token: 0x040007BC RID: 1980
	public AudioClipInfo slideInFanfare;

	// Token: 0x040007BD RID: 1981
	public UIMessageHelper messageHelper;

	// Token: 0x040007BE RID: 1982
	private Queue<string> _messageQueue = new Queue<string>();

	// Token: 0x040007BF RID: 1983
	private bool messageShowing;

	// Token: 0x040007C0 RID: 1984
	public GameObject inAppPurchaseOverlay;

	// Token: 0x02000213 RID: 531
	public enum SlideInType
	{
		// Token: 0x04000C10 RID: 3088
		Mission,
		// Token: 0x04000C11 RID: 3089
		MissionSet,
		// Token: 0x04000C12 RID: 3090
		Letters,
		// Token: 0x04000C13 RID: 3091
		Character,
		// Token: 0x04000C14 RID: 3092
		LettersComplete
	}

	// Token: 0x02000214 RID: 532
	private class SlideIn
	{
		// Token: 0x04000C15 RID: 3093
		public UIScreenController.SlideInType type;

		// Token: 0x04000C16 RID: 3094
		public string payload = string.Empty;
	}
}
