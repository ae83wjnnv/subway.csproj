using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class ResolutionSetup : MonoBehaviour
{
	// Token: 0x06000546 RID: 1350 RVA: 0x000195C0 File Offset: 0x000177C0
	private void Awake()
	{
		if (this.lowResAtlasses.Length != this.highResAtlasses.Length)
		{
			Debug.LogError("Low res and high res atlasses do not fit!");
			return;
		}
		if (this.lowResFonts.Length != this.highResFonts.Length)
		{
			Debug.LogError("Low res and high res fonts do not fit!");
			return;
		}
		if (DeviceInfo.isHighres)
		{
			for (int i = 0; i < this.usedFonts.Length; i++)
			{
				this.usedFonts[i].replacement = this.highResFonts[i];
			}
			for (int j = 0; j < this.usedAtlasses.Length; j++)
			{
				this.usedAtlasses[j].replacement = this.highResAtlasses[j];
			}
			return;
		}
		for (int k = 0; k < this.usedFonts.Length; k++)
		{
			this.usedFonts[k].replacement = this.lowResFonts[k];
		}
		for (int l = 0; l < this.usedAtlasses.Length; l++)
		{
			this.usedAtlasses[l].replacement = this.lowResAtlasses[l];
		}
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x000196B0 File Offset: 0x000178B0
	private void OnDisable()
	{
		if (DeviceInfo.isHighres)
		{
			for (int i = 0; i < this.usedFonts.Length; i++)
			{
				this.usedFonts[i].replacement = this.lowResFonts[i];
			}
			for (int j = 0; j < this.highResAtlasses.Length; j++)
			{
				this.usedAtlasses[j].replacement = this.lowResAtlasses[j];
			}
			return;
		}
		Debug.Log("Is low res, no atlas change");
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00019720 File Offset: 0x00017920
	public void SwitchFontResolution()
	{
		UILabel[] array = Resources.FindObjectsOfTypeAll(typeof(UILabel)) as UILabel[];
		this.allLabels = new List<UILabel>();
		this.allModifiedLabelsOldFonts = new List<UIFont>();
		foreach (UILabel uilabel in array)
		{
			for (int j = 0; j < this.lowResFonts.Length; j++)
			{
				if (uilabel.font == this.lowResFonts[j])
				{
					Debug.Log("Switching to high res font now!");
					this.allLabels.Add(uilabel);
					this.allModifiedLabelsOldFonts.Add(this.lowResFonts[j]);
					uilabel.font = this.highResFonts[j];
					break;
				}
			}
		}
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x000197CC File Offset: 0x000179CC
	public void ResetFontResolution()
	{
		Debug.Log("Resetting fonts");
		for (int i = 0; i < this.allLabels.Count; i++)
		{
			this.allLabels[i].font = this.allModifiedLabelsOldFonts[i];
		}
	}

	// Token: 0x04000469 RID: 1129
	public UIAtlas[] usedAtlasses;

	// Token: 0x0400046A RID: 1130
	public UIAtlas[] lowResAtlasses;

	// Token: 0x0400046B RID: 1131
	public UIAtlas[] highResAtlasses;

	// Token: 0x0400046C RID: 1132
	public UIFont[] usedFonts;

	// Token: 0x0400046D RID: 1133
	public UIFont[] lowResFonts;

	// Token: 0x0400046E RID: 1134
	public UIFont[] highResFonts;

	// Token: 0x0400046F RID: 1135
	private List<UILabel> allLabels;

	// Token: 0x04000470 RID: 1136
	private List<UIFont> allModifiedLabelsOldFonts;
}
