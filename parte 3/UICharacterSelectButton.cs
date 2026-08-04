using System;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class UICharacterSelectButton : MonoBehaviour
{
	// Token: 0x06000722 RID: 1826 RVA: 0x00023900 File Offset: 0x00021B00
	private void OnEnable()
	{
		this.OnChangedCurrentlyShownModel();
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x00023908 File Offset: 0x00021B08
	private void Awake()
	{
		UIModelController instance = UIModelController.Instance;
		instance.OnChangedCurrentlyShown = (Action)Delegate.Combine(instance.OnChangedCurrentlyShown, new Action(this.OnChangedCurrentlyShownModel));
		this.col = base.GetComponent<BoxCollider>();
		this.OnChangedCurrentlyShownModel();
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00023942 File Offset: 0x00021B42
	private void OnDestroy()
	{
		UIModelController instance = UIModelController.Instance;
		instance.OnChangedCurrentlyShown = (Action)Delegate.Remove(instance.OnChangedCurrentlyShown, new Action(this.OnChangedCurrentlyShownModel));
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x0002396C File Offset: 0x00021B6C
	private void OnChangedCurrentlyShownModel()
	{
		CharacterModels.ModelType currentlyShownModel = (CharacterModels.ModelType)UIModelController.Instance.currentlyShownModel;
		CharacterModels.Model model = CharacterModels.modelData[currentlyShownModel];
		if (PlayerInfo.Instance.currentCharacter == UIModelController.Instance.currentlyShownModel)
		{
			this.showAndEnable();
			this.fillSprite.spriteName = this.fillSelected;
			this.label.text = this.textSelected;
			this.col.enabled = false;
			return;
		}
		if (PlayerInfo.Instance.IsCollectionComplete(currentlyShownModel))
		{
			this.showAndEnable();
			this.fillSprite.spriteName = this.fillSelect;
			this.label.text = this.textSelect;
			this.col.enabled = true;
			return;
		}
		if (model.UnlockType == CharacterModels.UnlockType.tokens)
		{
			this.showAndEnable();
			this.fillSprite.spriteName = this.fillNotAvailable;
			this.label.text = string.Format(this.textNotAvailable, PlayerInfo.Instance.GetCollectedTokens(currentlyShownModel), model.Price);
			this.col.enabled = false;
			return;
		}
		this.hideAndDisable();
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x00023A88 File Offset: 0x00021C88
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

	// Token: 0x06000727 RID: 1831 RVA: 0x00023AE0 File Offset: 0x00021CE0
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

	// Token: 0x04000625 RID: 1573
	public UISlicedSprite fillSprite;

	// Token: 0x04000626 RID: 1574
	public UILabel label;

	// Token: 0x04000627 RID: 1575
	private string fillSelect = "button_fill_select";

	// Token: 0x04000628 RID: 1576
	private string fillSelected = "button_fill_selected";

	// Token: 0x04000629 RID: 1577
	private string fillNotAvailable = "button_fill_info";

	// Token: 0x0400062A RID: 1578
	private string textSelect = "SELECT";

	// Token: 0x0400062B RID: 1579
	private string textSelected = "SELECTED";

	// Token: 0x0400062C RID: 1580
	private string textBuy = "BUY\n{0} COINS";

	// Token: 0x0400062D RID: 1581
	private string textNotAvailable = "COLLECT ALL\n{0}/{1}";

	// Token: 0x0400062E RID: 1582
	private bool isEnabled = true;

	// Token: 0x0400062F RID: 1583
	private BoxCollider col;
}
