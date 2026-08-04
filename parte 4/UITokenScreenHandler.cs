using System;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class UITokenScreenHandler : MonoBehaviour
{
	// Token: 0x06000954 RID: 2388 RVA: 0x00032B08 File Offset: 0x00030D08
	private void Awake()
	{
		PlayerInfo instance = PlayerInfo.Instance;
		instance.OnTokenCollected = (Action<CharacterModels.ModelType>)Delegate.Combine(instance.OnTokenCollected, new Action<CharacterModels.ModelType>(this.UpdateTokens));
		this.UpdateTokens(CharacterModels.ModelType.yutani);
		this.trickyNameLabel.text = CharacterModels.modelData[CharacterModels.ModelType.tricky].TokenName;
		this.spikeNameLabel.text = CharacterModels.modelData[CharacterModels.ModelType.spike].TokenName;
		this.yutaniNameLabel.text = CharacterModels.modelData[CharacterModels.ModelType.yutani].TokenName;
		this.freshNameLabel.text = CharacterModels.modelData[CharacterModels.ModelType.fresh].TokenName;
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x00032BB0 File Offset: 0x00030DB0
	private void UpdateTokens(CharacterModels.ModelType type = CharacterModels.ModelType.yutani)
	{
		int collectedTokens = PlayerInfo.Instance.GetCollectedTokens(CharacterModels.ModelType.tricky);
		int price = CharacterModels.modelData[CharacterModels.ModelType.tricky].Price;
		this.trickyProgress.sliderValue = (float)collectedTokens / (float)price;
		if (collectedTokens < price)
		{
			this.trickyLabel.text = collectedTokens.ToString() + "/" + price.ToString();
		}
		else
		{
			this.trickyLabel.text = string.Empty + collectedTokens.ToString();
		}
		int collectedTokens2 = PlayerInfo.Instance.GetCollectedTokens(CharacterModels.ModelType.spike);
		int price2 = CharacterModels.modelData[CharacterModels.ModelType.spike].Price;
		this.spikeProgress.sliderValue = (float)collectedTokens2 / (float)price2;
		if (collectedTokens2 < price2)
		{
			this.spikeLabel.text = collectedTokens2.ToString() + "/" + price2.ToString();
		}
		else
		{
			this.spikeLabel.text = string.Empty + collectedTokens2.ToString();
		}
		int collectedTokens3 = PlayerInfo.Instance.GetCollectedTokens(CharacterModels.ModelType.yutani);
		int price3 = CharacterModels.modelData[CharacterModels.ModelType.yutani].Price;
		this.yutaniProgress.sliderValue = (float)collectedTokens3 / (float)price3;
		if (collectedTokens3 < price3)
		{
			this.yutaniLabel.text = collectedTokens3.ToString() + "/" + price3.ToString();
		}
		else
		{
			this.yutaniLabel.text = string.Empty + collectedTokens3.ToString();
		}
		int collectedTokens4 = PlayerInfo.Instance.GetCollectedTokens(CharacterModels.ModelType.fresh);
		int price4 = CharacterModels.modelData[CharacterModels.ModelType.fresh].Price;
		this.freshProgress.sliderValue = (float)collectedTokens4 / (float)price4;
		if (collectedTokens4 < price4)
		{
			this.freshLabel.text = collectedTokens4.ToString() + "/" + price4.ToString();
			return;
		}
		this.freshLabel.text = string.Empty + collectedTokens4.ToString();
	}

	// Token: 0x04000819 RID: 2073
	public UISlider trickyProgress;

	// Token: 0x0400081A RID: 2074
	public UILabel trickyLabel;

	// Token: 0x0400081B RID: 2075
	public UISlider spikeProgress;

	// Token: 0x0400081C RID: 2076
	public UILabel spikeLabel;

	// Token: 0x0400081D RID: 2077
	public UISlider yutaniProgress;

	// Token: 0x0400081E RID: 2078
	public UILabel yutaniLabel;

	// Token: 0x0400081F RID: 2079
	public UISlider freshProgress;

	// Token: 0x04000820 RID: 2080
	public UILabel freshLabel;

	// Token: 0x04000821 RID: 2081
	public UILabel trickyNameLabel;

	// Token: 0x04000822 RID: 2082
	public UILabel spikeNameLabel;

	// Token: 0x04000823 RID: 2083
	public UILabel yutaniNameLabel;

	// Token: 0x04000824 RID: 2084
	public UILabel freshNameLabel;
}
