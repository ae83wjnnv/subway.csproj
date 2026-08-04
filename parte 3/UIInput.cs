using System;
using UnityEngine;

// Token: 0x02000116 RID: 278
[AddComponentMenu("NGUI/UI/Input (Basic)")]
public class UIInput : MonoBehaviour
{
	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x060007FD RID: 2045 RVA: 0x00029828 File Offset: 0x00027A28
	// (set) Token: 0x060007FE RID: 2046 RVA: 0x00029830 File Offset: 0x00027A30
	public string text
	{
		get
		{
			return this.mText;
		}
		set
		{
			this.mText = value;
			if (this.label != null)
			{
				if (string.IsNullOrEmpty(value))
				{
					value = this.mDefaultText;
				}
				this.label.supportEncoding = false;
				this.label.text = ((!this.selected) ? value : (value + this.caratChar));
				this.label.showLastPasswordChar = this.selected;
				this.label.color = ((!this.selected && !(value != this.mDefaultText)) ? this.mDefaultColor : this.activeColor);
			}
		}
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x060007FF RID: 2047 RVA: 0x000298D0 File Offset: 0x00027AD0
	// (set) Token: 0x06000800 RID: 2048 RVA: 0x000298E2 File Offset: 0x00027AE2
	public bool selected
	{
		get
		{
			return UICamera.selectedObject == base.gameObject;
		}
		set
		{
			if (!value && UICamera.selectedObject == base.gameObject)
			{
				UICamera.selectedObject = null;
				return;
			}
			if (value)
			{
				UICamera.selectedObject = base.gameObject;
			}
		}
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x00029910 File Offset: 0x00027B10
	protected void Init()
	{
		if (this.label == null)
		{
			this.label = base.GetComponentInChildren<UILabel>();
		}
		if (this.label != null)
		{
			this.mDefaultText = this.label.text;
			this.mDefaultColor = this.label.color;
			this.label.supportEncoding = false;
		}
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00029973 File Offset: 0x00027B73
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x0002997B File Offset: 0x00027B7B
	private void OnEnable()
	{
		if (UICamera.IsHighlighted(base.gameObject))
		{
			this.OnSelect(true);
		}
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x00029991 File Offset: 0x00027B91
	private void OnDisable()
	{
		if (UICamera.IsHighlighted(base.gameObject))
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x000299A8 File Offset: 0x00027BA8
	private void OnSelect(bool isSelected)
	{
		if (!(this.label != null) || !base.enabled || !base.gameObject.active)
		{
			return;
		}
		if (isSelected)
		{
			this.mText = ((!(this.label.text == this.mDefaultText)) ? this.label.text : string.Empty);
			this.label.color = this.activeColor;
			if (this.isPassword)
			{
				this.label.password = true;
			}
			Input.imeCompositionMode = IMECompositionMode.On;
			Transform cachedTransform = this.label.cachedTransform;
			Vector3 vector = this.label.pivotOffset;
			vector.y += this.label.relativeSize.y;
			vector = cachedTransform.TransformPoint(vector);
			Input.compositionCursorPos = UICamera.currentCamera.WorldToScreenPoint(vector);
			this.UpdateLabel();
			return;
		}
		if (string.IsNullOrEmpty(this.mText))
		{
			this.label.text = this.mDefaultText;
			this.label.color = this.mDefaultColor;
			if (this.isPassword)
			{
				this.label.password = false;
			}
		}
		else
		{
			this.label.text = this.mText;
		}
		this.label.showLastPasswordChar = false;
		Input.imeCompositionMode = IMECompositionMode.Off;
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x00029AFC File Offset: 0x00027CFC
	private void Update()
	{
		if (this.mLastIME != Input.compositionString)
		{
			this.mLastIME = Input.compositionString;
			this.UpdateLabel();
		}
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x00029B24 File Offset: 0x00027D24
	private void OnInput(string input)
	{
		if (!this.selected || !base.enabled || !base.gameObject.active || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return;
		}
		int i = 0;
		int length = input.Length;
		while (i < length)
		{
			char c = input[i];
			if (c == '\b')
			{
				if (this.mText.Length > 0)
				{
					this.mText = this.mText.Substring(0, this.mText.Length - 1);
				}
			}
			else
			{
				if (c == '\r' || c == '\n')
				{
					UIInput.current = this;
					if (this.eventReceiver == null)
					{
						this.eventReceiver = base.gameObject;
					}
					this.eventReceiver.SendMessage(this.functionName, SendMessageOptions.DontRequireReceiver);
					UIInput.current = null;
					this.selected = false;
					return;
				}
				if (c >= ' ')
				{
					this.mText += c.ToString();
				}
			}
			i++;
		}
		this.UpdateLabel();
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00029C24 File Offset: 0x00027E24
	private void UpdateLabel()
	{
		if (this.maxChars > 0 && this.mText.Length > this.maxChars)
		{
			this.mText = this.mText.Substring(0, this.maxChars);
		}
		if (this.label.font != null)
		{
			string text = ((!this.selected) ? this.mText : (this.mText + Input.compositionString + this.caratChar));
			text = this.label.font.WrapText(text, (float)this.label.lineWidth / this.label.cachedTransform.localScale.x, true, false);
			if (!this.label.multiLine)
			{
				string[] array = text.Split(new char[] { '\n' });
				text = ((array.Length == 0) ? string.Empty : array[array.Length - 1]);
			}
			this.label.text = text;
			this.label.showLastPasswordChar = this.selected;
		}
	}

	// Token: 0x040006FA RID: 1786
	public static UIInput current;

	// Token: 0x040006FB RID: 1787
	public UILabel label;

	// Token: 0x040006FC RID: 1788
	public int maxChars;

	// Token: 0x040006FD RID: 1789
	public string caratChar = "|";

	// Token: 0x040006FE RID: 1790
	public bool isPassword;

	// Token: 0x040006FF RID: 1791
	public Color activeColor = Color.white;

	// Token: 0x04000700 RID: 1792
	public GameObject eventReceiver;

	// Token: 0x04000701 RID: 1793
	public string functionName = "OnSubmit";

	// Token: 0x04000702 RID: 1794
	private string mText = string.Empty;

	// Token: 0x04000703 RID: 1795
	private string mDefaultText = string.Empty;

	// Token: 0x04000704 RID: 1796
	private Color mDefaultColor = Color.white;

	// Token: 0x04000705 RID: 1797
	private string mLastIME = string.Empty;
}
