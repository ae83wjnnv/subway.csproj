using System;
using UnityEngine;

// Token: 0x02000115 RID: 277
public class UIIngameUpdater : IgnoreTimeScale
{
	// Token: 0x060007EF RID: 2031 RVA: 0x00029239 File Offset: 0x00027439
	public static bool isCountingDown()
	{
		return UIIngameUpdater.countingDown;
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x00029240 File Offset: 0x00027440
	public void Awake()
	{
		PlayerInfo instance = PlayerInfo.Instance;
		instance.onScoreMultiplierChanged = (Action)Delegate.Combine(instance.onScoreMultiplierChanged, new Action(this.readMultiplier));
		GameStats instance2 = GameStats.Instance;
		instance2.OnCoinsChanged = (Action)Delegate.Combine(instance2.OnCoinsChanged, new Action(this.OnCoinsChanged));
		Game instance3 = Game.Instance;
		instance3.OnGameStarted = (Action)Delegate.Combine(instance3.OnGameStarted, new Action(this.OnGameStarted));
		this.readMultiplier();
		this.scoreLabel.text = string.Empty + GameStats.Instance.score.ToString();
		this._cachedScoreBGTransform = this.scoreBG.cachedTransform;
		this._cachedCoinBGTransform = this.coinBG.cachedTransform;
		this.countdownStartingLabel.text = string.Empty;
		this.countdownLabel.text = string.Empty;
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00029330 File Offset: 0x00027530
	public void OnDestroy()
	{
		PlayerInfo instance = PlayerInfo.Instance;
		instance.onScoreMultiplierChanged = (Action)Delegate.Remove(instance.onScoreMultiplierChanged, new Action(this.readMultiplier));
		GameStats instance2 = GameStats.Instance;
		instance2.OnCoinsChanged = (Action)Delegate.Remove(instance2.OnCoinsChanged, new Action(this.OnCoinsChanged));
		Game instance3 = Game.Instance;
		instance3.OnGameStarted = (Action)Delegate.Remove(instance3.OnGameStarted, new Action(this.OnGameStarted));
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x000293AF File Offset: 0x000275AF
	private void OnDisable()
	{
		UIIngameUpdater.countingDown = false;
		this.countdownStartingLabel.text = string.Empty;
		this.countdownLabel.text = string.Empty;
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x000293D7 File Offset: 0x000275D7
	public void TriggerInGameUI()
	{
		if (Game.Instance != null)
		{
			if (Game.Instance.isPaused)
			{
				this.countdownSeconds = 3f;
				UIIngameUpdater.countingDown = true;
				return;
			}
		}
		else
		{
			Debug.LogError("You must be running the GUI scene");
		}
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x00029410 File Offset: 0x00027610
	private void readMultiplier()
	{
		this.multiplierLabel.text = "x" + PlayerInfo.Instance.scoreMultiplier.ToString();
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x00029444 File Offset: 0x00027644
	private void Update()
	{
		if (Game.Instance.isReadyForHeadStart && !Game.Instance.track.IsRunningOnTutorialTrack)
		{
			Game.Instance.isReadyForHeadStart = false;
			this.headstartHelper.ShowHeadStart();
		}
		GameStats.Instance.CalculateScore();
		if (this.score != GameStats.Instance.score)
		{
			this.SetScoreLabel();
		}
		if (!UIIngameUpdater.countingDown)
		{
			return;
		}
		float num = base.UpdateRealTimeDelta();
		num *= 1.75f;
		this.countdownSeconds -= num;
		this.countdownStartingLabel.text = "Starting in";
		this.countdownLabel.text = Mathf.CeilToInt(this.countdownSeconds).ToString();
		if (this.cachedCountdownLabelScale == Vector3.zero)
		{
			this.cachedCountdownLabelScale = this.countdownLabel.cachedTransform.localScale;
		}
		this.countdownLabel.cachedTransform.localScale = this.cachedCountdownLabelScale * ((1f - this.countdownSeconds % 1f) * 0.5f + 1f);
		if (this.countdownSeconds < 0f)
		{
			UIIngameUpdater.countingDown = false;
			this.countdownStartingLabel.text = string.Empty;
			this.countdownLabel.text = string.Empty;
			if (Game.Instance != null)
			{
				Game.Instance.TriggerPause(false);
			}
		}
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x000295A8 File Offset: 0x000277A8
	private void OnCoinsChanged()
	{
		this.coinLabel.text = string.Empty + GameStats.Instance.coins.ToString();
		this.ResizeCoinBox();
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x000295E2 File Offset: 0x000277E2
	private void OnGameStarted()
	{
		if (!Game.Instance.isReadyForHeadStart)
		{
			this.headstartHelper.HideHeadStart(true);
		}
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x000295FC File Offset: 0x000277FC
	private void SetScoreLabel()
	{
		this.score = GameStats.Instance.score;
		string text;
		switch (Utility.NumberOfDigits(this.score))
		{
		case 1:
			text = "00000";
			break;
		case 2:
			text = "0000";
			break;
		case 3:
			text = "000";
			break;
		case 4:
			text = "00";
			break;
		case 5:
			text = "0";
			break;
		default:
			text = string.Empty;
			break;
		}
		this.scoreLabel.text = text + this.score.ToString();
		this.ResizeScoreBox();
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x00029694 File Offset: 0x00027894
	private void ResizeScoreBox()
	{
		int length = this.scoreLabel.text.Length;
		float num = 99f;
		if (length > 6)
		{
			num += (float)(13 * (length - 6));
		}
		if (this._cachedScoreBGTransform.localScale.x != num)
		{
			this._cachedScoreBGTransform.localScale = new Vector3(num, this._cachedScoreBGTransform.localScale.y, this._cachedScoreBGTransform.localScale.z);
		}
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x0002970C File Offset: 0x0002790C
	private void ResizeCoinBox()
	{
		int length = this.coinLabel.text.Length;
		float num = 64f;
		if (length > 1)
		{
			num += (float)(13 * (length - 1));
		}
		if (this._cachedCoinBGTransform.localScale.x != num)
		{
			this._cachedCoinBGTransform.localScale = new Vector3(num, this._cachedCoinBGTransform.localScale.y, this._cachedCoinBGTransform.localScale.z);
		}
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00029784 File Offset: 0x00027984
	private void ResizeMultiplierBox()
	{
		int length = this.multiplierLabel.text.Length;
		float num = 50f;
		if (length > 2)
		{
			num += (float)(10 * (length - 2));
		}
		if (this.multiplierBG.transform.localScale.x != num)
		{
			this.multiplierBG.transform.localScale = new Vector3(num, this.multiplierBG.transform.localScale.y, this.multiplierBG.transform.localScale.z);
		}
	}

	// Token: 0x040006EB RID: 1771
	public UILabel scoreLabel;

	// Token: 0x040006EC RID: 1772
	public UILabel multiplierLabel;

	// Token: 0x040006ED RID: 1773
	public UILabel coinLabel;

	// Token: 0x040006EE RID: 1774
	public UISlicedSprite scoreBG;

	// Token: 0x040006EF RID: 1775
	private Transform _cachedScoreBGTransform;

	// Token: 0x040006F0 RID: 1776
	public UISlicedSprite multiplierBG;

	// Token: 0x040006F1 RID: 1777
	public UISlicedSprite coinBG;

	// Token: 0x040006F2 RID: 1778
	private Transform _cachedCoinBGTransform;

	// Token: 0x040006F3 RID: 1779
	public UIHeadStartHelper headstartHelper;

	// Token: 0x040006F4 RID: 1780
	public UILabel countdownStartingLabel;

	// Token: 0x040006F5 RID: 1781
	public UILabel countdownLabel;

	// Token: 0x040006F6 RID: 1782
	private float countdownSeconds;

	// Token: 0x040006F7 RID: 1783
	private static bool countingDown;

	// Token: 0x040006F8 RID: 1784
	private Vector3 cachedCountdownLabelScale = Vector3.zero;

	// Token: 0x040006F9 RID: 1785
	private int score = -1;
}
