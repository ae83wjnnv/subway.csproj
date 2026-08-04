using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200013F RID: 319
[AddComponentMenu("Game/UI/Tooltip")]
public class UITooltip : MonoBehaviour
{
	// Token: 0x06000957 RID: 2391 RVA: 0x00032D98 File Offset: 0x00030F98
	private void Awake()
	{
		UITooltip.mInstance = this;
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x00032DA0 File Offset: 0x00030FA0
	private void OnDestroy()
	{
		UITooltip.mInstance = null;
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x00032DA8 File Offset: 0x00030FA8
	private void Start()
	{
		this.mTrans = base.transform;
		this.mWidgets = base.GetComponentsInChildren<UIWidget>();
		this.mPos = this.mTrans.localPosition;
		this.mSize = this.mTrans.localScale;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.SetAlpha(0f);
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x00032E20 File Offset: 0x00031020
	private void Update()
	{
		if (this.mCurrent != this.mTarget)
		{
			this.mCurrent = Mathf.Lerp(this.mCurrent, this.mTarget, Time.deltaTime * this.appearSpeed);
			if (Mathf.Abs(this.mCurrent - this.mTarget) < 0.001f)
			{
				this.mCurrent = this.mTarget;
			}
			this.SetAlpha(this.mCurrent * this.mCurrent);
			if (this.scalingTransitions)
			{
				Vector3 vector = this.mSize * 0.25f;
				vector.y = 0f - vector.y;
				Vector3 vector2 = Vector3.one * (1.5f - this.mCurrent * 0.5f);
				Vector3 vector3 = Vector3.Lerp(this.mPos - vector, this.mPos, this.mCurrent);
				vector3 = NGUIMath.ApplyHalfPixelOffset(vector3);
				this.mTrans.localPosition = vector3;
				this.mTrans.localScale = vector2;
			}
		}
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x00032F20 File Offset: 0x00031120
	private void SetAlpha(float val)
	{
		int i = 0;
		int num = this.mWidgets.Length;
		while (i < num)
		{
			UIWidget uiwidget = this.mWidgets[i];
			Color color = uiwidget.color;
			color.a = val;
			uiwidget.color = color;
			i++;
		}
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x00032F60 File Offset: 0x00031160
	private void SetText(string tooltipText)
	{
		if (this.text != null && !string.IsNullOrEmpty(tooltipText))
		{
			this.mTarget = 1f;
			if (this.text != null)
			{
				this.text.text = tooltipText;
			}
			this.mPos = Input.mousePosition;
			if (this.background != null)
			{
				Transform transform = this.background.transform;
				Transform transform2 = this.text.transform;
				Vector3 localPosition = transform2.localPosition;
				Vector3 localScale = transform2.localScale;
				this.mSize = this.text.relativeSize;
				this.mSize.x = this.mSize.x * localScale.x;
				this.mSize.y = this.mSize.y * localScale.y;
				this.mSize.x = this.mSize.x + (this.background.border.x + this.background.border.z + (localPosition.x - this.background.border.x) * 2f);
				this.mSize.y = this.mSize.y + (this.background.border.y + this.background.border.w + (0f - localPosition.y - this.background.border.y) * 2f);
				this.mSize.z = 1f;
				transform.localScale = this.mSize;
			}
			if (this.uiCamera != null)
			{
				this.mPos.x = Mathf.Clamp01(this.mPos.x / (float)Screen.width);
				this.mPos.y = Mathf.Clamp01(this.mPos.y / (float)Screen.height);
				float num = this.uiCamera.orthographicSize / this.mTrans.parent.lossyScale.y;
				float num2 = (float)Screen.height * 0.5f / num;
				Vector2 vector = new Vector2(num2 * this.mSize.x / (float)Screen.width, num2 * this.mSize.y / (float)Screen.height);
				this.mPos.x = Mathf.Min(this.mPos.x, 1f - vector.x);
				this.mPos.y = Mathf.Max(this.mPos.y, vector.y);
				this.mTrans.position = this.uiCamera.ViewportToWorldPoint(this.mPos);
				this.mPos = this.mTrans.localPosition;
			}
			else
			{
				if (this.mPos.x + this.mSize.x > (float)Screen.width)
				{
					this.mPos.x = (float)Screen.width - this.mSize.x;
				}
				if (this.mPos.y - this.mSize.y < 0f)
				{
					this.mPos.y = this.mSize.y;
				}
				this.mPos.x = this.mPos.x - (float)Screen.width * 0.5f;
				this.mPos.y = this.mPos.y - (float)Screen.height * 0.5f;
			}
			this.mTrans.localPosition = NGUIMath.ApplyHalfPixelOffset(this.mPos);
			return;
		}
		this.mTarget = 0f;
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x000332E3 File Offset: 0x000314E3
	public static void ShowText(string tooltipText)
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.SetText(tooltipText);
		}
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x00033300 File Offset: 0x00031500
	public static void ShowItem(InvGameItem item)
	{
		if (item != null)
		{
			InvBaseItem baseItem = item.baseItem;
			if (baseItem != null)
			{
				string text = string.Concat(new string[]
				{
					"[",
					NGUITools.EncodeColor(item.color),
					"]",
					item.name,
					"[-]\n"
				});
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"[AFAFAF]Level ",
					item.itemLevel.ToString(),
					" ",
					baseItem.slot.ToString()
				});
				List<InvStat> list = item.CalculateStats();
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					InvStat invStat = list[i];
					if (invStat.amount != 0)
					{
						text = ((invStat.amount >= 0) ? (text + "\n[00FF00]+" + invStat.amount.ToString()) : (text + "\n[FF0000]" + invStat.amount.ToString()));
						if (invStat.modifier == InvStat.Modifier.Percent)
						{
							text += "%";
						}
						text = text + " " + invStat.id.ToString();
						text += "[-]";
					}
					i++;
				}
				if (!string.IsNullOrEmpty(baseItem.description))
				{
					text = text + "\n[FF9900]" + baseItem.description;
				}
				UITooltip.ShowText(text);
				return;
			}
		}
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.mTarget = 0f;
		}
	}

	// Token: 0x04000825 RID: 2085
	private static UITooltip mInstance;

	// Token: 0x04000826 RID: 2086
	public Camera uiCamera;

	// Token: 0x04000827 RID: 2087
	public UILabel text;

	// Token: 0x04000828 RID: 2088
	public UISlicedSprite background;

	// Token: 0x04000829 RID: 2089
	public float appearSpeed = 10f;

	// Token: 0x0400082A RID: 2090
	public bool scalingTransitions = true;

	// Token: 0x0400082B RID: 2091
	private Transform mTrans;

	// Token: 0x0400082C RID: 2092
	private float mTarget;

	// Token: 0x0400082D RID: 2093
	private float mCurrent;

	// Token: 0x0400082E RID: 2094
	private Vector3 mPos;

	// Token: 0x0400082F RID: 2095
	private Vector3 mSize;

	// Token: 0x04000830 RID: 2096
	private UIWidget[] mWidgets;
}
