using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000099 RID: 153
public class MysteryBoxHandler : MonoBehaviour
{
	// Token: 0x06000479 RID: 1145 RVA: 0x0001577B File Offset: 0x0001397B
	private void Awake()
	{
		this.audioStateLoop = this.FindObject<AudioStateLoop>();
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x0001578C File Offset: 0x0001398C
	public void SetupMysteryBoxScreen()
	{
		this.boxParent.transform.position = UIModelController.Instance.MysteryBoxAnchor.transform.position;
		int mysteryBoxesToUnlock = PlayerInfo.Instance.mysteryBoxesToUnlock;
		PlayerInfo.Instance.mysteryBoxesToUnlock = 0;
		this.ContinueLabel.alpha = 1f;
		this.ContinueLabel.text = "Tap to open";
		foreach (object obj in this.mainSlot.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		foreach (object obj2 in this.slotSecondBox.transform)
		{
			Object.Destroy(((Transform)obj2).gameObject);
		}
		if (mysteryBoxesToUnlock == 1)
		{
			this.SingleBox();
		}
		else if (mysteryBoxesToUnlock >= 2)
		{
			if (mysteryBoxesToUnlock > 2)
			{
				Debug.LogError("Mysterybox screen setup with " + mysteryBoxesToUnlock.ToString() + " boxes. Should not happen ever.");
			}
			this.TwoBoxes();
		}
		else
		{
			Debug.LogError("Mysterybox screen setup with " + mysteryBoxesToUnlock.ToString() + " boxes. Should not happen ever.");
		}
		if (this._boxMain != null)
		{
			base.StartCoroutine(this.BoxIdleAnimCoroutine(this._boxMain.transform));
		}
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x00015910 File Offset: 0x00013B10
	public void SkipNow()
	{
		if (this._maySetTimeScale)
		{
			Time.timeScale = 3f;
		}
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x00015924 File Offset: 0x00013B24
	public void SingleBox()
	{
		GameObject gameObject = NGUITools.AddChild(this.mainSlot, this.boxPrefab);
		gameObject.transform.localScale = this._boxScale;
		gameObject.transform.localRotation = Quaternion.Euler(this._boxRotation);
		Utility.SetLayerRecursively(gameObject.transform, this.boxParent.layer);
		this._boxMain = gameObject;
		this.GlowEffect.GetComponent<MeshRenderer>().material.SetColor("_MainColor", Color.black);
		this.openButton.GetComponent<Collider>().enabled = true;
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x000159B8 File Offset: 0x00013BB8
	public void TwoBoxes()
	{
		GameObject gameObject = NGUITools.AddChild(this.mainSlot, this.boxPrefab);
		gameObject.transform.localScale = this._boxScale;
		gameObject.transform.localRotation = Quaternion.Euler(this._boxRotation);
		Utility.SetLayerRecursively(gameObject.transform, this.boxParent.layer);
		this._boxMain = gameObject;
		gameObject = NGUITools.AddChild(this.slotSecondBox, this.boxPrefab);
		gameObject.transform.localScale = this._boxScale;
		gameObject.transform.localRotation = Quaternion.Euler(this._boxRotation);
		Utility.SetLayerRecursively(gameObject.transform, this.boxParent.layer);
		this._boxSecond = gameObject;
		this.GlowEffect.GetComponent<MeshRenderer>().material.SetColor("_MainColor", Color.black);
		this.openButton.GetComponent<Collider>().enabled = true;
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x00015AA1 File Offset: 0x00013CA1
	private IEnumerator BoxIdleAnimCoroutine(Transform boxTrans)
	{
		Vector3 baseLocalPos = boxTrans.localPosition;
		this.stopBoxIdleAnim = false;
		float t = 0f;
		Vector3 newLocalPos = baseLocalPos;
		while (!this.stopBoxIdleAnim)
		{
			t += Time.deltaTime;
			newLocalPos.y = baseLocalPos.y + Mathf.Sin(t * 2f) * 10f;
			boxTrans.localPosition = newLocalPos;
			yield return null;
		}
		bool doneResetting = false;
		while (!doneResetting)
		{
			newLocalPos.y = Mathf.MoveTowards(newLocalPos.y, baseLocalPos.y, Time.deltaTime * 20f);
			if (Mathf.Approximately(newLocalPos.y, baseLocalPos.y))
			{
				doneResetting = true;
			}
			boxTrans.localPosition = newLocalPos;
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x00015AB7 File Offset: 0x00013CB7
	private IEnumerator _MoveSecondBoxToFront()
	{
		this._boxMain = this._boxSecond;
		this._boxSecond = null;
		this.anotherBox = false;
		this.GlowEffect.GetComponent<MeshRenderer>().material.SetColor("_MainColor", Color.black);
		this._boxMain.transform.parent = this.mainSlot.transform;
		yield return base.StartCoroutine(this.MoveGameObject(this._boxMain.transform, 1f, Vector3.zero));
		this.openButton.GetComponent<Collider>().enabled = true;
		base.StartCoroutine(this.BoxIdleAnimCoroutine(this._boxMain.transform));
		yield break;
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00015AC6 File Offset: 0x00013CC6
	public void TestDown()
	{
		this.stopBoxIdleAnim = true;
		this._boxMain.GetComponentInChildren<Animation>().Play("down");
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x00015AE8 File Offset: 0x00013CE8
	public void TestUp()
	{
		this.openButton.GetComponent<Collider>().enabled = false;
		if (!this.openingBoxNow)
		{
			MysteryBoxReward mysteryBoxReward = MysteryBox.Roll();
			base.StartCoroutine(this.AnimateAlpha(this.ContinueLabel, 0.5f, 0f));
			base.StartCoroutine(this._ShowReward(mysteryBoxReward, this._boxMain));
			this.openingBoxNow = true;
		}
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x00015B4C File Offset: 0x00013D4C
	private IEnumerator _ShowReward(MysteryBoxReward reward, GameObject box)
	{
		GameObject gameObject = this.ChooseRewardPrefab(reward);
		GameObject rewardGo = NGUITools.AddChild(this.mainSlot, gameObject);
		rewardGo.transform.localScale = this._rewardStartScale;
		rewardGo.transform.localRotation = Quaternion.Euler(this._rewardStartRotation);
		Utility.SetLayerRecursively(rewardGo.transform, this.boxParent.layer);
		this.audioStateLoop.PlayMysteryBoxOpenSound();
		Animation animation = box.GetComponentInChildren<Animation>();
		animation.Play("up");
		while (animation["up"].normalizedTime < 0.5f)
		{
			yield return null;
		}
		this._maySetTimeScale = true;
		if (reward.type == MysteryBoxRewardType.Coins)
		{
			base.StartCoroutine(this.ScaleGameObject(rewardGo.transform, 2f, this._rewardEndScale_coin));
		}
		else
		{
			base.StartCoroutine(this.ScaleGameObject(rewardGo.transform, 2f, this._rewardEndScale));
		}
		base.StartCoroutine(this.MoveGameObject(box.transform, 2f, this._outOfScreenPosition));
		base.StartCoroutine(this.RotateGameObject(rewardGo.transform, 6f, new Vector3(0f, 1500f, 0f)));
		yield return new WaitForSeconds(0.5f);
		base.StartCoroutine(this.AnimateColor(this.GlowEffect.GetComponent<MeshRenderer>().material, 1.5f, Color.white));
		base.StartCoroutine(this.RotateGameObject(this.GlowEffect.transform, 3f, new Vector3(0f, 0f, -270f)));
		yield return new WaitForSeconds(1.3f);
		GameObject labelGo = NGUITools.AddChild(base.gameObject, this.rewardLabelTemplate);
		labelGo.transform.localPosition = this._labelPosition;
		MysteryBoxRewardLabelTemplate template = labelGo.GetComponent<MysteryBoxRewardLabelTemplate>();
		if (reward.type == MysteryBoxRewardType.Coins)
		{
			template.SetupCoins(reward.amount);
		}
		else if (reward.type == MysteryBoxRewardType.powerup)
		{
			template.SetupPowerup(reward.powerupType, reward.amount);
		}
		else if (reward.type == MysteryBoxRewardType.token)
		{
			template.SetupToken(reward.tokenType);
		}
		base.StartCoroutine(this.AnimateAlpha(template, 0.2f, 1f));
		yield return new WaitForSeconds(0.5f);
		if (reward.type == MysteryBoxRewardType.Coins)
		{
			base.StartCoroutine(this.CountUpCoins(reward.amount, template));
		}
		yield return new WaitForSeconds(2.5f);
		base.StartCoroutine(this.AnimateColor(this.GlowEffect.GetComponent<MeshRenderer>().material, 0.5f, Color.black));
		if (reward.type == MysteryBoxRewardType.powerup)
		{
			PlayerInfo.Instance.IncreaseUpgradeAmount(reward.powerupType, reward.amount);
		}
		else if (reward.type == MysteryBoxRewardType.token)
		{
			PlayerInfo.Instance.CollectToken(reward.tokenType, 1);
		}
		PlayerInfo.Instance.Save();
		Flurry.LogEvent("Mystery Box opened");
		if (this._boxSecond != null)
		{
			this.anotherBox = true;
		}
		yield return new WaitForSeconds(2f);
		this.ContinueLabel.text = "Tap to continue";
		base.StartCoroutine(this.AnimateAlpha(this.ContinueLabel, 0.5f, 1f));
		while (!Input.GetMouseButtonUp(0))
		{
			yield return null;
		}
		Time.timeScale = 1f;
		this._maySetTimeScale = false;
		Object.Destroy(rewardGo);
		Object.Destroy(labelGo);
		Object.Destroy(box);
		this._FinishOpening();
		yield break;
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00015B69 File Offset: 0x00013D69
	private void _FinishOpening()
	{
		this.openingBoxNow = false;
		if (this.anotherBox)
		{
			base.StartCoroutine(this._MoveSecondBoxToFront());
			return;
		}
		UIScreenController.Instance.ClosePopup();
		this.openButton.GetComponent<Collider>().enabled = false;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00015BA3 File Offset: 0x00013DA3
	private IEnumerator RotateGameObject(Transform trans, float duration, Vector3 angleToRotate)
	{
		Quaternion fromRotation = trans.localRotation;
		float factor2 = 0f;
		while (factor2 < 1f)
		{
			factor2 += Time.deltaTime / duration;
			factor2 = Mathf.Clamp01(factor2);
			float num = Mathf.Cos(Mathf.Lerp(4.712389f, 6.2831855f, factor2)) * 0.5f + 0.5f;
			trans.localRotation = fromRotation;
			trans.Rotate(angleToRotate * num, Space.World);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00015BC0 File Offset: 0x00013DC0
	private IEnumerator CountUpCoins(int amount, MysteryBoxRewardLabelTemplate rewardTemplate)
	{
		float countFactor = 0f;
		float countTime = Mathf.Lerp(0.3f, 2f, (float)amount / 100f);
		int coinboxFrom = PlayerInfo.Instance.amountOfCoins;
		int coinboxTo = coinboxFrom + amount;
		int rewardLabelTo = 0;
		yield return new WaitForSeconds(0.5f);
		while (countFactor < 1f)
		{
			countFactor += Time.deltaTime / countTime;
			rewardTemplate.UpdateCoins(Mathf.RoundToInt(Mathf.SmoothStep((float)amount, (float)rewardLabelTo, countFactor)));
			this.coinboxLabel.text = string.Empty + Mathf.RoundToInt(Mathf.SmoothStep((float)coinboxFrom, (float)coinboxTo, countFactor)).ToString();
			yield return null;
		}
		Missions.Instance.PlayerDidThis(Missions.MissionTarget.EarnCoin, amount);
		PlayerInfo.Instance.amountOfCoins = coinboxTo;
		PlayerInfo.Instance.Save();
		yield break;
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x00015BDD File Offset: 0x00013DDD
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

	// Token: 0x06000487 RID: 1159 RVA: 0x00015BFA File Offset: 0x00013DFA
	private IEnumerator AnimateAlpha(MysteryBoxRewardLabelTemplate template, float duration, float toAlpha)
	{
		float fromAlpha = template.Alpha;
		float factor2 = 0f;
		while (factor2 < 1f)
		{
			factor2 += Time.deltaTime / duration;
			factor2 = Mathf.Clamp01(factor2);
			template.Alpha = Mathf.Lerp(fromAlpha, toAlpha, factor2);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x00015C17 File Offset: 0x00013E17
	private IEnumerator AnimateColor(UIWidget widget, float duration, Color toColor)
	{
		Color fromColor = widget.color;
		float factor2 = 0f;
		while (factor2 < 1f)
		{
			factor2 += Time.deltaTime / duration;
			factor2 = Mathf.Clamp01(factor2);
			widget.color = Color.Lerp(fromColor, toColor, factor2);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x00015C34 File Offset: 0x00013E34
	private IEnumerator AnimateColor(Material material, float duration, Color toColor)
	{
		Color fromColor = material.GetColor("_MainColor");
		float factor2 = 0f;
		while (factor2 < 1f)
		{
			factor2 += Time.deltaTime / duration;
			factor2 = Mathf.Clamp01(factor2);
			material.SetColor("_MainColor", Color.Lerp(fromColor, toColor, factor2));
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x00015C51 File Offset: 0x00013E51
	private IEnumerator MoveGameObject(Transform trans, float duration, Vector3 toPos)
	{
		Vector3 fromPos = trans.localPosition;
		float factor2 = 0f;
		while (factor2 < 1f)
		{
			factor2 += Time.deltaTime / duration;
			factor2 = Mathf.Clamp01(factor2);
			trans.localPosition = Vector3.Lerp(fromPos, toPos, factor2 * factor2 * 6f);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00015C6E File Offset: 0x00013E6E
	private IEnumerator ScaleGameObject(Transform trans, float duration, Vector3 toScale)
	{
		float factor2 = 0f;
		Vector3 fromScale = trans.localScale;
		while (factor2 < 1f)
		{
			factor2 += Time.deltaTime / duration;
			factor2 = Mathf.Clamp01(factor2);
			float num = Mathf.Cos(Mathf.Lerp(3.1415927f, 6.2831855f, factor2)) * 0.5f + 0.5f;
			trans.localScale = Vector3.Lerp(fromScale, toScale, num);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x00015C8C File Offset: 0x00013E8C
	private GameObject ChooseRewardPrefab(MysteryBoxReward reward)
	{
		GameObject gameObject = this.rewardCoins;
		switch (reward.type)
		{
		case MysteryBoxRewardType.Coins:
			gameObject = this.rewardCoins;
			Missions.Instance.PlayerDidThis(Missions.MissionTarget.CollectCoinPouch, 1);
			break;
		case MysteryBoxRewardType.powerup:
			switch (reward.powerupType)
			{
			case PowerupType.hoverboard:
				gameObject = this.rewardPowerupHoverboard;
				break;
			case PowerupType.headstart500:
				gameObject = this.rewardPowerupHeadstart500;
				break;
			case PowerupType.headstart2000:
				gameObject = this.rewardPowerupHeadstart2000;
				break;
			}
			break;
		case MysteryBoxRewardType.token:
			Missions.Instance.PlayerDidThis(Missions.MissionTarget.Tokens, 1);
			switch (reward.tokenType)
			{
			case CharacterModels.ModelType.tricky:
				gameObject = this.rewardTokenTricky;
				break;
			case CharacterModels.ModelType.fresh:
				gameObject = this.rewardTokenFresh;
				break;
			case CharacterModels.ModelType.spike:
				gameObject = this.rewardTokenSpike;
				break;
			case CharacterModels.ModelType.yutani:
				gameObject = this.rewardTokenYutani;
				break;
			}
			break;
		}
		Debug.Log("Rewardprefab: " + gameObject.name);
		return gameObject;
	}

	// Token: 0x040003D6 RID: 982
	private const string CONTINUELABEL_OPEN = "Tap to open";

	// Token: 0x040003D7 RID: 983
	private const string CONTINUELABEL_CONTINUE = "Tap to continue";

	// Token: 0x040003D8 RID: 984
	private const float BOX_IDLE_ANIM_SPEED = 2f;

	// Token: 0x040003D9 RID: 985
	private const float BOX_IDLE_ANIM_AMOUNT = 10f;

	// Token: 0x040003DA RID: 986
	private const float BOX_IDLE_ANIM_END_SPEED = 20f;

	// Token: 0x040003DB RID: 987
	public GameObject boxParent;

	// Token: 0x040003DC RID: 988
	public GameObject rewardLabelTemplate;

	// Token: 0x040003DD RID: 989
	private Vector3 _outOfScreenPosition = new Vector3(0f, -500f, 0f);

	// Token: 0x040003DE RID: 990
	public GameObject openButton;

	// Token: 0x040003DF RID: 991
	public GameObject continueButton;

	// Token: 0x040003E0 RID: 992
	public UILabel coinboxLabel;

	// Token: 0x040003E1 RID: 993
	public GameObject mainSlot;

	// Token: 0x040003E2 RID: 994
	public GameObject slotSecondBox;

	// Token: 0x040003E3 RID: 995
	public GameObject boxPrefab;

	// Token: 0x040003E4 RID: 996
	public GameObject testRewardPrefab;

	// Token: 0x040003E5 RID: 997
	public GameObject rewardCoins;

	// Token: 0x040003E6 RID: 998
	public GameObject rewardTokenTricky;

	// Token: 0x040003E7 RID: 999
	public GameObject rewardTokenFresh;

	// Token: 0x040003E8 RID: 1000
	public GameObject rewardTokenSpike;

	// Token: 0x040003E9 RID: 1001
	public GameObject rewardTokenYutani;

	// Token: 0x040003EA RID: 1002
	public GameObject rewardPowerupHoverboard;

	// Token: 0x040003EB RID: 1003
	public GameObject rewardPowerupHeadstart500;

	// Token: 0x040003EC RID: 1004
	public GameObject rewardPowerupHeadstart2000;

	// Token: 0x040003ED RID: 1005
	public UILabel ContinueLabel;

	// Token: 0x040003EE RID: 1006
	public AudioStateLoop audioStateLoop;

	// Token: 0x040003EF RID: 1007
	private GameObject _boxMain;

	// Token: 0x040003F0 RID: 1008
	private GameObject _boxSecond;

	// Token: 0x040003F1 RID: 1009
	private bool anotherBox;

	// Token: 0x040003F2 RID: 1010
	private Vector3 _boxScale = new Vector3(1150f, 1150f, 1150f);

	// Token: 0x040003F3 RID: 1011
	private Vector3 _boxRotation = new Vector3(0f, 250f, 20f);

	// Token: 0x040003F4 RID: 1012
	private Vector3 _rewardStartRotation = new Vector3(15f, -10.5f, 0f);

	// Token: 0x040003F5 RID: 1013
	private Vector3 _rewardStartScale = new Vector3(5f, 5f, 5f);

	// Token: 0x040003F6 RID: 1014
	private Vector3 _rewardEndScale_coin = new Vector3(10f, 10f, 10f);

	// Token: 0x040003F7 RID: 1015
	private Vector3 _rewardEndScale = new Vector3(20f, 20f, 20f);

	// Token: 0x040003F8 RID: 1016
	private GameObject _rewardMain;

	// Token: 0x040003F9 RID: 1017
	private UILabel _labelSingle;

	// Token: 0x040003FA RID: 1018
	private UILabel _labelDouble1;

	// Token: 0x040003FB RID: 1019
	private Vector3 _labelPosition = new Vector3(0f, 134f, -5f);

	// Token: 0x040003FC RID: 1020
	private bool _maySetTimeScale;

	// Token: 0x040003FD RID: 1021
	public GameObject GlowEffect;

	// Token: 0x040003FE RID: 1022
	private bool stopBoxIdleAnim;

	// Token: 0x040003FF RID: 1023
	private bool openingBoxNow;
}
