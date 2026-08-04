using System;
using UnityEngine;

// Token: 0x0200011E RID: 286
public class UIModelController : MonoBehaviour
{
	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06000846 RID: 2118 RVA: 0x0002B4A3 File Offset: 0x000296A3
	public int currentlyShownModel
	{
		get
		{
			return this._currentlyShownModel;
		}
	}

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x06000847 RID: 2119 RVA: 0x0002B4AB File Offset: 0x000296AB
	public static UIModelController Instance
	{
		get
		{
			UIModelController uimodelController;
			if ((uimodelController = UIModelController._instance) == null)
			{
				uimodelController = (UIModelController._instance = Object.FindObjectOfType(typeof(UIModelController)) as UIModelController);
			}
			return uimodelController;
		}
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x0002B4D0 File Offset: 0x000296D0
	public void ActivateGameOverModel()
	{
		this._ActivateModel((CharacterModels.ModelType)PlayerInfo.Instance.currentCharacter, UIModelController.ModelScreen.GameOver);
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x0002B4E4 File Offset: 0x000296E4
	public void SelectCurrentModel()
	{
		PlayerInfo.Instance.currentCharacter = this._currentlyShownModel;
		PlayerInfo.Instance.Save();
		if (Game.Instance != null)
		{
			CharacterModel characterModel = Game.Instance.Character.characterModel;
			CharacterModels.ModelType currentlyShownModel = (CharacterModels.ModelType)this._currentlyShownModel;
			characterModel.ChangeCharacterModel(currentlyShownModel.ToString());
		}
		Action onChangedCurrentlyShown = this.OnChangedCurrentlyShown;
		if (onChangedCurrentlyShown != null)
		{
			onChangedCurrentlyShown();
		}
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x0002B550 File Offset: 0x00029750
	public void ActivateCharacterModel()
	{
		this._currentlyShownModel = PlayerInfo.Instance.currentCharacter;
		this._ActivateModel((CharacterModels.ModelType)this._currentlyShownModel, UIModelController.ModelScreen.Character);
		Action onChangedCurrentlyShown = this.OnChangedCurrentlyShown;
		if (onChangedCurrentlyShown != null)
		{
			onChangedCurrentlyShown();
		}
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x0002B58C File Offset: 0x0002978C
	public void ChangeModelRight()
	{
		this._currentlyShownModel++;
		this._currentlyShownModel %= this._numberOfModels;
		this._ActivateModel((CharacterModels.ModelType)this._currentlyShownModel, UIModelController.ModelScreen.Character);
		Action onChangedCurrentlyShown = this.OnChangedCurrentlyShown;
		if (onChangedCurrentlyShown != null)
		{
			onChangedCurrentlyShown();
		}
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0002B5D8 File Offset: 0x000297D8
	public void ChangeModelLeft()
	{
		this._currentlyShownModel--;
		if (this._currentlyShownModel < 0)
		{
			this._currentlyShownModel = this._numberOfModels - 1;
		}
		this._ActivateModel((CharacterModels.ModelType)this._currentlyShownModel, UIModelController.ModelScreen.Character);
		Action onChangedCurrentlyShown = this.OnChangedCurrentlyShown;
		if (onChangedCurrentlyShown != null)
		{
			onChangedCurrentlyShown();
		}
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x0002B628 File Offset: 0x00029828
	private void _ActivateModel(CharacterModels.ModelType name, UIModelController.ModelScreen screen)
	{
		this.ClearModels();
		if (screen == UIModelController.ModelScreen.Character)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.ModelPrefab);
			gameObject.transform.parent = this.CharacterAnchor.transform;
			gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
			Utility.SetLayerRecursively(gameObject.transform, this.CharacterAnchor.layer);
			gameObject.transform.localScale = new Vector3(21f, 21f, 21f);
			gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
			CharacterModel component = gameObject.GetComponent<CharacterModel>();
			component.ChangeCharacterModel(name.ToString());
			component.HideAllPowerups();
			component.StartIdleAnimations();
			return;
		}
		if (screen != UIModelController.ModelScreen.GameOver)
		{
			return;
		}
		GameObject gameObject2 = Object.Instantiate<GameObject>(this.ModelPrefab);
		gameObject2.transform.parent = this.GameOverAnchor.transform;
		gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
		Utility.SetLayerRecursively(gameObject2.transform, this.GameOverAnchor.layer);
		gameObject2.transform.localScale = new Vector3(18f, 18f, 18f);
		gameObject2.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		CharacterModel component2 = gameObject2.GetComponent<CharacterModel>();
		component2.ChangeCharacterModel(name.ToString());
		component2.HideAllPowerups();
		component2.StartIdleAnimations();
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x0002B7C0 File Offset: 0x000299C0
	public void ClearModels()
	{
		foreach (object obj in this.CharacterAnchor.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		foreach (object obj2 in this.GameOverAnchor.transform)
		{
			Object.Destroy(((Transform)obj2).gameObject);
		}
	}

	// Token: 0x04000742 RID: 1858
	public GameObject CharacterAnchor;

	// Token: 0x04000743 RID: 1859
	public GameObject GameOverAnchor;

	// Token: 0x04000744 RID: 1860
	public GameObject MysteryBoxAnchor;

	// Token: 0x04000745 RID: 1861
	public GameObject ModelPrefab;

	// Token: 0x04000746 RID: 1862
	public Action OnChangedCurrentlyShown;

	// Token: 0x04000747 RID: 1863
	private int _currentlyShownModel;

	// Token: 0x04000748 RID: 1864
	private int _numberOfModels = 8;

	// Token: 0x04000749 RID: 1865
	private static UIModelController _instance;

	// Token: 0x02000210 RID: 528
	public enum ModelScreen
	{
		// Token: 0x04000C05 RID: 3077
		Character,
		// Token: 0x04000C06 RID: 3078
		GameOver
	}
}
