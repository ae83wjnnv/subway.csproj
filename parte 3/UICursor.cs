using System;
using UnityEngine;

// Token: 0x020000FE RID: 254
[AddComponentMenu("NGUI/Examples/UI Cursor")]
[RequireComponent(typeof(UISprite))]
public class UICursor : MonoBehaviour
{
	// Token: 0x06000734 RID: 1844 RVA: 0x00023E8A File Offset: 0x0002208A
	private void Awake()
	{
		UICursor.mInstance = this;
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x00023E92 File Offset: 0x00022092
	private void OnDestroy()
	{
		UICursor.mInstance = null;
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x00023E9C File Offset: 0x0002209C
	private void Start()
	{
		this.mTrans = base.transform;
		this.mSprite = base.GetComponentInChildren<UISprite>();
		this.mAtlas = this.mSprite.atlas;
		this.mSpriteName = this.mSprite.spriteName;
		this.mSprite.depth = 100;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x00023F14 File Offset: 0x00022114
	private void Update()
	{
		if (!(this.mSprite.atlas != null))
		{
			return;
		}
		Vector3 mousePosition = Input.mousePosition;
		if (this.uiCamera != null)
		{
			mousePosition.x = Mathf.Clamp01(mousePosition.x / (float)Screen.width);
			mousePosition.y = Mathf.Clamp01(mousePosition.y / (float)Screen.height);
			this.mTrans.position = this.uiCamera.ViewportToWorldPoint(mousePosition);
			if (this.uiCamera.orthographic)
			{
				this.mTrans.localPosition = NGUIMath.ApplyHalfPixelOffset(this.mTrans.localPosition, this.mTrans.localScale);
				return;
			}
		}
		else
		{
			mousePosition.x -= (float)Screen.width * 0.5f;
			mousePosition.y -= (float)Screen.height * 0.5f;
			this.mTrans.localPosition = NGUIMath.ApplyHalfPixelOffset(mousePosition, this.mTrans.localScale);
		}
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x00024010 File Offset: 0x00022210
	public static void Clear()
	{
		UICursor.Set(UICursor.mInstance.mAtlas, UICursor.mInstance.mSpriteName);
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0002402C File Offset: 0x0002222C
	public static void Set(UIAtlas atlas, string sprite)
	{
		if (UICursor.mInstance != null)
		{
			UICursor.mInstance.mSprite.atlas = atlas;
			UICursor.mInstance.mSprite.spriteName = sprite;
			UICursor.mInstance.mSprite.MakePixelPerfect();
			UICursor.mInstance.Update();
		}
	}

	// Token: 0x04000640 RID: 1600
	private static UICursor mInstance;

	// Token: 0x04000641 RID: 1601
	public Camera uiCamera;

	// Token: 0x04000642 RID: 1602
	private Transform mTrans;

	// Token: 0x04000643 RID: 1603
	private UISprite mSprite;

	// Token: 0x04000644 RID: 1604
	private UIAtlas mAtlas;

	// Token: 0x04000645 RID: 1605
	private string mSpriteName;
}
