using System;
using UnityEngine;

// Token: 0x020000F9 RID: 249
public class UICharacterBuyButton : MonoBehaviour
{
	// Token: 0x06000718 RID: 1816 RVA: 0x000236F6 File Offset: 0x000218F6
	private void OnEnable()
	{
		this.isEnabled = true;
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x000236FF File Offset: 0x000218FF
	private void Awake()
	{
		UIModelController instance = UIModelController.Instance;
		instance.OnChangedCurrentlyShown = (Action)Delegate.Combine(instance.OnChangedCurrentlyShown, new Action(this.OnChangedCurrentlyShownModel));
		this.col = base.GetComponent<BoxCollider>();
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x00023734 File Offset: 0x00021934
	private void OnClick()
	{
		if (!this._purchaseInProgress)
		{
			CharacterModels.ModelType currentlyShownModel = (CharacterModels.ModelType)UIModelController.Instance.currentlyShownModel;
			CharacterModels.Model model = CharacterModels.modelData[currentlyShownModel];
			Debug.Log("Buy: " + currentlyShownModel.ToString());
			PurchaseHandler.Instance.PurchaseCharacter(currentlyShownModel, this);
		}
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x00023788 File Offset: 0x00021988
	private void OnChangedCurrentlyShownModel()
	{
		CharacterModels.ModelType currentlyShownModel = (CharacterModels.ModelType)UIModelController.Instance.currentlyShownModel;
		CharacterModels.Model model = CharacterModels.modelData[currentlyShownModel];
		if (model.UnlockType != CharacterModels.UnlockType.coins)
		{
			this.hideAndDisable();
			return;
		}
		if (PlayerInfo.Instance.IsCollectionComplete(currentlyShownModel))
		{
			this.hideAndDisable();
			return;
		}
		this.showAndEnable();
		int price = model.Price;
		this.label.text = string.Format("UNLOCK FOR\n{0} COINS", price);
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x000237FE File Offset: 0x000219FE
	private void OnDestroy()
	{
		UIModelController instance = UIModelController.Instance;
		instance.OnChangedCurrentlyShown = (Action)Delegate.Remove(instance.OnChangedCurrentlyShown, new Action(this.OnChangedCurrentlyShownModel));
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x00023828 File Offset: 0x00021A28
	private void hideAndDisable()
	{
		if (this.isEnabled)
		{
			for (int i = 0; i < base.transform.childCount; i++)
			{
				base.transform.GetChild(i).gameObject.active = false;
			}
			this.col.enabled = false;
			this.isEnabled = false;
		}
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x00023880 File Offset: 0x00021A80
	private void showAndEnable()
	{
		if (!this.isEnabled)
		{
			for (int i = 0; i < base.transform.childCount; i++)
			{
				base.transform.GetChild(i).gameObject.active = true;
			}
			this.col.enabled = true;
			this.isEnabled = true;
		}
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x000238D5 File Offset: 0x00021AD5
	public void PurchaseSuccessful()
	{
		this._purchaseInProgress = false;
		UIModelController.Instance.SelectCurrentModel();
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x000238E8 File Offset: 0x00021AE8
	public void PurchaseFailure()
	{
		this._purchaseInProgress = false;
	}

	// Token: 0x0400061F RID: 1567
	private const string TEXT_BUY = "UNLOCK FOR\n{0} COINS";

	// Token: 0x04000620 RID: 1568
	public UILabel label;

	// Token: 0x04000621 RID: 1569
	private BoxCollider col;

	// Token: 0x04000622 RID: 1570
	private bool isEnabled = true;

	// Token: 0x04000623 RID: 1571
	private bool _purchaseInProgress;

	// Token: 0x04000624 RID: 1572
	public Action OnChangedCurrentlyShown;
}
