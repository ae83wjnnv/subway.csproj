using System;
using UnityEngine;

// Token: 0x0200013C RID: 316
public class UITierHelper : MonoBehaviour
{
	// Token: 0x0600094C RID: 2380 RVA: 0x00032428 File Offset: 0x00030628
	public bool ResetTiers()
	{
		foreach (object obj in base.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		this.SetupTiers(this._type);
		return PlayerInfo.Instance.GetCurrentTier(this._type) >= Upgrades.upgrades[this._type].numberOfTiers - 1;
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x000324BC File Offset: 0x000306BC
	public void SetupTiers(PowerupType type)
	{
		this._type = type;
		int numberOfTiers = Upgrades.upgrades[type].numberOfTiers;
		int currentTier = PlayerInfo.Instance.GetCurrentTier(type);
		UISprite uisprite = NGUITools.AddSprite(base.gameObject, this.usedAtlas, "progressbar_background");
		uisprite.name = "0background";
		uisprite.transform.localScale = new Vector3((float)numberOfTiers * 20f - 10f, 16f, 1f);
		uisprite.pivot = UIWidget.Pivot.BottomLeft;
		uisprite.depth = 11;
		for (int i = 0; i < numberOfTiers - 1; i++)
		{
			UISprite uisprite2 = NGUITools.AddSprite(base.gameObject, this.usedAtlas, "progressbar_bar_off");
			uisprite2.name = "slot" + (i + 1).ToString();
			uisprite2.transform.localPosition = new Vector3(5f + (float)(20 * i), 3f, 0f);
			uisprite2.transform.localScale = new Vector3(16f, 10f, 1f);
			uisprite2.pivot = UIWidget.Pivot.BottomLeft;
			uisprite2.depth = 12;
			uisprite2.MakePixelPerfect();
		}
		for (int j = 0; j < currentTier; j++)
		{
			UISprite uisprite3 = NGUITools.AddSprite(base.gameObject, this.usedAtlas, "progressbar_bar_on");
			uisprite3.name = "ActiveSlot" + (j + 1).ToString();
			uisprite3.transform.localPosition = new Vector3(4f + (float)(20 * j), 3f, 0f);
			uisprite3.transform.localScale = new Vector3(18f, 9f, 1f);
			uisprite3.pivot = UIWidget.Pivot.BottomLeft;
			uisprite3.depth = 13;
			uisprite3.MakePixelPerfect();
		}
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x00032684 File Offset: 0x00030884
	private Color getTierColor(int numberOfActiveTiers)
	{
		switch (numberOfActiveTiers)
		{
		case 1:
			return new Color(1f, 0f, 0f, 1f);
		case 2:
			return new Color(1f, 0f, 0f, 1f);
		case 3:
			return new Color(1f, 0f, 0f, 1f);
		case 4:
			return new Color(1f, 0f, 0f, 1f);
		case 5:
			return new Color(1f, 0f, 0f, 1f);
		default:
			return Color.white;
		}
	}

	// Token: 0x04000817 RID: 2071
	public UIAtlas usedAtlas;

	// Token: 0x04000818 RID: 2072
	private PowerupType _type;
}
