using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class CharacterModel : MonoBehaviour
{
	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060001E0 RID: 480 RVA: 0x000085E3 File Offset: 0x000067E3
	public string[] ModelNames
	{
		get
		{
			return this.modelNames;
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060001E1 RID: 481 RVA: 0x000085EB File Offset: 0x000067EB
	// (set) Token: 0x060001E2 RID: 482 RVA: 0x000085F4 File Offset: 0x000067F4
	public Color OverlayColor
	{
		get
		{
			return this.overlayColor;
		}
		set
		{
			this.overlayColor = value;
			for (int i = 0; i < this.models.Length; i++)
			{
				this.models[i].sharedMaterial.SetColor("_OverlayColor", this.overlayColor);
			}
		}
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x00008638 File Offset: 0x00006838
	public void Awake()
	{
		this.models = base.GetComponentsInChildren<SkinnedMeshRenderer>();
		this.modelNames = new string[this.models.Length];
		this.modelLookupTable = new Dictionary<string, SkinnedMeshRenderer>();
		for (int i = 0; i < this.models.Length; i++)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = this.models[i];
			string name = skinnedMeshRenderer.gameObject.name;
			this.modelNames[i] = name;
			this.modelLookupTable.Add(name, skinnedMeshRenderer);
		}
		CharacterModels.Model model = CharacterModels.modelData[(CharacterModels.ModelType)PlayerInfo.Instance.currentCharacter];
		if (PlayerInfo.Instance.GetCollectedTokens((CharacterModels.ModelType)PlayerInfo.Instance.currentCharacter) < model.Price)
		{
			Debug.Log("Resetting to jake/slick because of likely exploit");
			PlayerInfo.Instance.currentCharacter = 0;
			PlayerInfo.Instance.Save();
		}
		this.ChangeCharacterModel(((CharacterModels.ModelType)PlayerInfo.Instance.currentCharacter).ToString());
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x0000871C File Offset: 0x0000691C
	public void ChangeCharacterModel(string name)
	{
		this.StopIdleAnimations();
		SkinnedMeshRenderer skinnedMeshRenderer;
		if (this.modelLookupTable.TryGetValue(name, out skinnedMeshRenderer))
		{
			for (int i = 0; i < this.models.Length; i++)
			{
				SkinnedMeshRenderer skinnedMeshRenderer2 = this.models[i];
				skinnedMeshRenderer2.enabled = skinnedMeshRenderer2 == skinnedMeshRenderer;
			}
		}
		else
		{
			Debug.LogWarning(string.Format("Could not find character by the name: {0}", name));
		}
		this.model = skinnedMeshRenderer;
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x00008780 File Offset: 0x00006980
	public void HideAllPowerups()
	{
		this.meshHoverboard.enabled = false;
		this.meshJetpack.enabled = false;
		this.meshSuperSneaker.enabled = false;
		this.meshCoinMagnet.enabled = false;
		this.meshSprayCan.enabled = this.model.name == "slick";
		ParticleSystem[] componentsInChildren = this.meshSprayCan.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enableEmission = false;
		}
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x00008800 File Offset: 0x00006A00
	public void StartBlink()
	{
		this.blinking = true;
		base.StartCoroutine(this.Blink());
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x00008816 File Offset: 0x00006A16
	private IEnumerator Blink()
	{
		while (this.blinking)
		{
			this.OverlayColor = pMath.Square(Time.time * this.blinkFrequency) * Color.white;
			yield return 0;
		}
		this.OverlayColor = Color.black;
		yield break;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x00008825 File Offset: 0x00006A25
	public void StopBlink()
	{
		this.blinking = false;
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000882E File Offset: 0x00006A2E
	public void ResetBlink()
	{
		this.OverlayColor = Color.black;
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000883C File Offset: 0x00006A3C
	public void StartIdleAnimations()
	{
		if (!(this.model == null))
		{
			AvatarAnimations component = this.model.GetComponent<AvatarAnimations>();
			if (component != null)
			{
				component.StartIdleAnimations();
			}
		}
	}

	// Token: 0x060001EB RID: 491 RVA: 0x00008874 File Offset: 0x00006A74
	public void StopIdleAnimations()
	{
		if (!(this.model == null))
		{
			AvatarAnimations component = this.model.GetComponent<AvatarAnimations>();
			if (component != null)
			{
				component.StopIdleAnimations();
			}
		}
	}

	// Token: 0x060001EC RID: 492 RVA: 0x000088AC File Offset: 0x00006AAC
	public void PauseIdleAnimations()
	{
		if (!(this.model == null))
		{
			AvatarAnimations component = this.model.GetComponent<AvatarAnimations>();
			if (component != null)
			{
				component.PauseIdleAnimations();
			}
		}
	}

	// Token: 0x060001ED RID: 493 RVA: 0x000088E4 File Offset: 0x00006AE4
	public void ResumeIdleAnimations()
	{
		if (!(this.model == null))
		{
			AvatarAnimations component = this.model.GetComponent<AvatarAnimations>();
			if (component != null)
			{
				component.ResumeIdleAnimations();
			}
		}
	}

	// Token: 0x04000135 RID: 309
	public SkinnedMeshRenderer model;

	// Token: 0x04000136 RID: 310
	public SkinnedMeshRenderer meshSuperSneaker;

	// Token: 0x04000137 RID: 311
	public MeshRenderer meshHoverboard;

	// Token: 0x04000138 RID: 312
	public MeshRenderer meshCoinMagnet;

	// Token: 0x04000139 RID: 313
	public MeshRenderer meshJetpack;

	// Token: 0x0400013A RID: 314
	public MeshRenderer meshSprayCan;

	// Token: 0x0400013B RID: 315
	private SkinnedMeshRenderer[] models;

	// Token: 0x0400013C RID: 316
	private Dictionary<string, SkinnedMeshRenderer> modelLookupTable;

	// Token: 0x0400013D RID: 317
	private string[] modelNames;

	// Token: 0x0400013E RID: 318
	private Color overlayColor = Color.black;

	// Token: 0x0400013F RID: 319
	private bool blinking;

	// Token: 0x04000140 RID: 320
	public float blinkFrequency = 1.5f;
}
