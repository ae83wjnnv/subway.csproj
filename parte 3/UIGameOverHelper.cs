using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class UIGameOverHelper : MonoBehaviour
{
	// Token: 0x060007CA RID: 1994 RVA: 0x00028720 File Offset: 0x00026920
	private void Awake()
	{
		this.scoreCounterSoundPlayer = base.GetComponent<ScoreCounterSoundPlayer>();
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x0002872E File Offset: 0x0002692E
	private void OnEnable()
	{
		PlayerInfo instance = PlayerInfo.Instance;
		instance.onCoinsChanged = (Action)Delegate.Combine(instance.onCoinsChanged, new Action(this.OnCoinsChanged));
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00028756 File Offset: 0x00026956
	private void OnDisable()
	{
		PlayerInfo instance = PlayerInfo.Instance;
		instance.onCoinsChanged = (Action)Delegate.Remove(instance.onCoinsChanged, new Action(this.OnCoinsChanged));
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00028780 File Offset: 0x00026980
	public void SetupBeforeMysteryBox()
	{
		this.HasBeenSetupAfterAGame = true;
		this.scoreLabel.text = string.Empty + GameStats.Instance.score.ToString();
		this.collectedCoinLabel.text = string.Empty + GameStats.Instance.coins.ToString();
		this.scoreFrom = GameStats.Instance.score;
		this.UpdateNewUpgradesLabel();
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x000287F8 File Offset: 0x000269F8
	private void UpdateNewUpgradesLabel()
	{
		int numberOfAffordableUpgrades = PlayerInfo.Instance.GetNumberOfAffordableUpgrades();
		if (numberOfAffordableUpgrades > 1)
		{
			this.newUpgradesIcon.active = true;
			this.newUpgradesText.gameObject.active = true;
			this.newUpgradesText.text = numberOfAffordableUpgrades.ToString();
			return;
		}
		this.newUpgradesIcon.active = false;
		this.newUpgradesText.gameObject.active = false;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x00028861 File Offset: 0x00026A61
	private void OnCoinsChanged()
	{
		this.UpdateNewUpgradesLabel();
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0002886C File Offset: 0x00026A6C
	public void SetupAfterMysteryBox()
	{
		if (this.HasBeenSetupAfterAGame)
		{
			this.coinBoxSizer.updateAutomatically = false;
			this.collectedCoinsFrom = GameStats.Instance.coins;
			this.collectedCoinsTo = 0;
			this.scoreTo = this.scoreFrom + GameStats.CoinToScoreConversion(this.collectedCoinsFrom);
			this.coinboxFrom = PlayerInfo.Instance.amountOfCoins;
			this.coinboxTo = this.coinboxFrom + GameStats.Instance.coins;
			PlayerInfo.Instance.amountOfCoins = this.coinboxTo;
			PlayerInfo.Instance.highestScore = this.scoreTo;
			if (SocialManager.instance != null)
			{
				SocialManager.instance.ReportScore(PlayerInfo.Instance.highestScore, Mathf.Max(1, Mathf.RoundToInt(GameStats.Instance.meters)));
			}
			PlayerInfo.Instance.Save();
			base.StartCoroutine(this.CountUpCoins());
			this.HasBeenSetupAfterAGame = false;
		}
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x0002895A File Offset: 0x00026B5A
	private void CountUpCompleted()
	{
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x0002895C File Offset: 0x00026B5C
	private IEnumerator CountUpCoins()
	{
		float countFactor = 0f;
		float countTime = Mathf.Lerp(0.3f, 2f, (float)this.collectedCoinsFrom / 100f);
		yield return new WaitForSeconds(0.5f);
		while (countFactor < 1f)
		{
			this.scoreCounterSoundPlayer.PlayCoinSound(countFactor);
			countFactor += Time.deltaTime / countTime;
			this.scoreLabel.text = string.Empty + Mathf.Round(Mathf.SmoothStep((float)this.scoreFrom, (float)this.scoreTo, countFactor)).ToString();
			this.coinboxLabel.text = string.Empty + Mathf.Round(Mathf.SmoothStep((float)this.coinboxFrom, (float)this.coinboxTo, countFactor)).ToString();
			this.collectedCoinLabel.text = string.Empty + Mathf.Round(Mathf.SmoothStep((float)this.collectedCoinsFrom, (float)this.collectedCoinsTo, countFactor)).ToString();
			yield return null;
		}
		this.scoreCounterSoundPlayer.StopScoreSound();
		this.scoreLabel.text = string.Empty + this.scoreTo.ToString();
		this.coinboxLabel.text = string.Empty + this.coinboxTo.ToString();
		this.collectedCoinLabel.text = string.Empty + this.collectedCoinsTo.ToString();
		this.coinBoxSizer.updateAutomatically = true;
		this.CountUpCompleted();
		yield break;
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x0002896B File Offset: 0x00026B6B
	public void FacebookLoggedIn()
	{
	}

	// Token: 0x040006BA RID: 1722
	public UILabel scoreLabel;

	// Token: 0x040006BB RID: 1723
	public UILabel collectedCoinLabel;

	// Token: 0x040006BC RID: 1724
	public UILabel coinboxLabel;

	// Token: 0x040006BD RID: 1725
	public CoinBoxSizer coinBoxSizer;

	// Token: 0x040006BE RID: 1726
	public int coinsToAnimateDebug = 1000;

	// Token: 0x040006BF RID: 1727
	private Friend[] _friends;

	// Token: 0x040006C0 RID: 1728
	private int scoreFrom;

	// Token: 0x040006C1 RID: 1729
	private int scoreTo;

	// Token: 0x040006C2 RID: 1730
	private int coinboxFrom;

	// Token: 0x040006C3 RID: 1731
	private int coinboxTo;

	// Token: 0x040006C4 RID: 1732
	private int collectedCoinsFrom;

	// Token: 0x040006C5 RID: 1733
	private int collectedCoinsTo;

	// Token: 0x040006C6 RID: 1734
	private ScoreCounterSoundPlayer scoreCounterSoundPlayer;

	// Token: 0x040006C7 RID: 1735
	public FriendHandlerBrag bragHandler;

	// Token: 0x040006C8 RID: 1736
	public GameObject OfflineParent;

	// Token: 0x040006C9 RID: 1737
	public GameObject OnlineParent;

	// Token: 0x040006CA RID: 1738
	public GameObject newUpgradesIcon;

	// Token: 0x040006CB RID: 1739
	public UILabel newUpgradesText;

	// Token: 0x040006CC RID: 1740
	[HideInInspector]
	public bool HasBeenSetupAfterAGame;
}
