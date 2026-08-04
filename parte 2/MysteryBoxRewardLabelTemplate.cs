using System;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class MysteryBoxRewardLabelTemplate : MonoBehaviour
{
	// Token: 0x17000067 RID: 103
	// (get) Token: 0x06000493 RID: 1171 RVA: 0x00015EEE File Offset: 0x000140EE
	// (set) Token: 0x06000494 RID: 1172 RVA: 0x00015EFB File Offset: 0x000140FB
	public float Alpha
	{
		get
		{
			return this.label.alpha;
		}
		set
		{
			this.label.alpha = Mathf.Clamp01(value);
		}
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x00015F0E File Offset: 0x0001410E
	public void SetupPowerup(PowerupType powerup, int amount)
	{
		this.Alpha = 0f;
		this.label.text = this._GetPowerupLabel(powerup, amount);
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00015F2E File Offset: 0x0001412E
	public void SetupCoins(int amount)
	{
		this.Alpha = 0f;
		this.label.text = this._GetCoinsLabel(amount);
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00015F4D File Offset: 0x0001414D
	public void UpdateCoins(int amount)
	{
		this.label.text = this._GetCoinsLabel(amount);
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x00015F61 File Offset: 0x00014161
	public void SetupToken(CharacterModels.ModelType tokenType)
	{
		this.Alpha = 0f;
		this.label.text = this._GetTokenLabel(tokenType);
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x00015F80 File Offset: 0x00014180
	private string _GetPowerupLabel(PowerupType type, int amount)
	{
		return string.Empty + amount.ToString() + "x " + Upgrades.upgrades[type].name;
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x00015FAD File Offset: 0x000141AD
	private string _GetTokenLabel(CharacterModels.ModelType modelType)
	{
		return string.Empty + "1x " + CharacterModels.modelData[modelType].TokenName;
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00015FCE File Offset: 0x000141CE
	private string _GetCoinsLabel(int amount)
	{
		return amount.ToString() + " Coins";
	}

	// Token: 0x04000406 RID: 1030
	public UILabel label;
}
