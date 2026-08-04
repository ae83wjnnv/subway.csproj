using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000123 RID: 291
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Popup List")]
public class UIPopupList : MonoBehaviour
{
	// Token: 0x170000ED RID: 237
	// (get) Token: 0x06000881 RID: 2177 RVA: 0x0002CF1A File Offset: 0x0002B11A
	public bool isOpen
	{
		get
		{
			return this.mChild != null;
		}
	}

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x06000882 RID: 2178 RVA: 0x0002CF28 File Offset: 0x0002B128
	// (set) Token: 0x06000883 RID: 2179 RVA: 0x0002CF30 File Offset: 0x0002B130
	public string selection
	{
		get
		{
			return this.mSelectedItem;
		}
		set
		{
			if (this.mSelectedItem != value)
			{
				this.mSelectedItem = value;
				if (this.textLabel != null)
				{
					this.textLabel.text = ((!this.isLocalized || !(Localization.instance != null)) ? value : Localization.instance.Get(value));
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName) && Application.isPlaying)
				{
					this.eventReceiver.SendMessage(this.functionName, this.mSelectedItem, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x06000884 RID: 2180 RVA: 0x0002CFC8 File Offset: 0x0002B1C8
	// (set) Token: 0x06000885 RID: 2181 RVA: 0x0002CFF0 File Offset: 0x0002B1F0
	private bool handleEvents
	{
		get
		{
			UIButtonKeys component = base.GetComponent<UIButtonKeys>();
			return component == null || !component.enabled;
		}
		set
		{
			UIButtonKeys component = base.GetComponent<UIButtonKeys>();
			if (component != null)
			{
				component.enabled = !value;
			}
		}
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x0002D018 File Offset: 0x0002B218
	private void Start()
	{
		if (string.IsNullOrEmpty(this.mSelectedItem))
		{
			if (this.items.Count > 0)
			{
				this.selection = this.items[0];
				return;
			}
		}
		else
		{
			string text = this.mSelectedItem;
			this.mSelectedItem = null;
			this.selection = text;
		}
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x0002D068 File Offset: 0x0002B268
	private void OnLocalize(Localization loc)
	{
		if (this.isLocalized && this.textLabel != null)
		{
			this.textLabel.text = loc.Get(this.mSelectedItem);
		}
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x0002D098 File Offset: 0x0002B298
	private void Highlight(UILabel lbl, bool instant)
	{
		if (!(this.mHighlight != null))
		{
			return;
		}
		TweenPosition component = lbl.GetComponent<TweenPosition>();
		if (!(component != null) || !component.enabled)
		{
			this.mHighlightedLabel = lbl;
			UIAtlas.Sprite sprite = this.mHighlight.sprite;
			float num = sprite.inner.xMin - sprite.outer.xMin;
			float num2 = sprite.inner.yMin - sprite.outer.yMin;
			Vector3 vector = lbl.cachedTransform.localPosition + new Vector3(0f - num, num2, 0f);
			if (instant || !this.isAnimated)
			{
				this.mHighlight.cachedTransform.localPosition = vector;
				return;
			}
			TweenPosition.Begin(this.mHighlight.gameObject, 0.1f, vector).method = UITweener.Method.EaseOut;
		}
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x0002D174 File Offset: 0x0002B374
	private void OnItemHover(GameObject go, bool isOver)
	{
		if (isOver)
		{
			UILabel component = go.GetComponent<UILabel>();
			this.Highlight(component, false);
		}
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x0002D194 File Offset: 0x0002B394
	private void Select(UILabel lbl, bool instant)
	{
		this.Highlight(lbl, instant);
		UIEventListener component = lbl.gameObject.GetComponent<UIEventListener>();
		this.selection = component.parameter as string;
		UIButtonSound[] components = base.GetComponents<UIButtonSound>();
		int i = 0;
		int num = components.Length;
		while (i < num)
		{
			UIButtonSound uibuttonSound = components[i];
			if (uibuttonSound.trigger == UIButtonSound.Trigger.OnClick)
			{
				NGUITools.PlaySound(uibuttonSound.audioClip, uibuttonSound.volume, 1f);
			}
			i++;
		}
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x0002D204 File Offset: 0x0002B404
	private void OnItemPress(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			this.Select(go.GetComponent<UILabel>(), true);
		}
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x0002D218 File Offset: 0x0002B418
	private void OnKey(KeyCode key)
	{
		if (!base.enabled || !base.gameObject.active || !this.handleEvents)
		{
			return;
		}
		int num = this.mLabelList.IndexOf(this.mHighlightedLabel);
		if (key != KeyCode.Escape)
		{
			if (key != KeyCode.UpArrow)
			{
				if (key != KeyCode.DownArrow)
				{
					return;
				}
				if (num + 1 < this.mLabelList.Count)
				{
					this.Select(this.mLabelList[num + 1], false);
					return;
				}
			}
			else if (num > 0)
			{
				this.Select(this.mLabelList[num - 1], false);
				return;
			}
		}
		else
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x0002D2B8 File Offset: 0x0002B4B8
	private void OnSelect(bool isSelected)
	{
		if (isSelected || !(this.mChild != null))
		{
			return;
		}
		this.mLabelList.Clear();
		this.handleEvents = false;
		if (this.isAnimated)
		{
			UIWidget[] componentsInChildren = this.mChild.GetComponentsInChildren<UIWidget>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				UIWidget uiwidget = componentsInChildren[i];
				Color color = uiwidget.color;
				color.a = 0f;
				TweenColor.Begin(uiwidget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
				i++;
			}
			Collider[] componentsInChildren2 = this.mChild.GetComponentsInChildren<Collider>();
			int j = 0;
			int num2 = componentsInChildren2.Length;
			while (j < num2)
			{
				componentsInChildren2[j].enabled = false;
				j++;
			}
			UpdateManager.AddDestroy(this.mChild, 0.15f);
		}
		else
		{
			Object.Destroy(this.mChild);
		}
		this.mBackground = null;
		this.mHighlight = null;
		this.mChild = null;
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x0002D39C File Offset: 0x0002B59C
	private void AnimateColor(UIWidget widget)
	{
		Color color = widget.color;
		widget.color = new Color(color.r, color.g, color.b, 0f);
		TweenColor.Begin(widget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x0002D3EC File Offset: 0x0002B5EC
	private void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
	{
		Vector3 localPosition = widget.cachedTransform.localPosition;
		Vector3 vector = ((!placeAbove) ? new Vector3(localPosition.x, 0f, localPosition.z) : new Vector3(localPosition.x, bottom, localPosition.z));
		widget.cachedTransform.localPosition = vector;
		TweenPosition.Begin(widget.gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x0002D458 File Offset: 0x0002B658
	private void AnimateScale(UIWidget widget, bool placeAbove, float bottom)
	{
		GameObject gameObject = widget.gameObject;
		Transform cachedTransform = widget.cachedTransform;
		float num = (float)this.font.size * this.textScale + this.mBgBorder * 2f;
		Vector3 localScale = cachedTransform.localScale;
		cachedTransform.localScale = new Vector3(localScale.x, num, localScale.z);
		TweenScale.Begin(gameObject, 0.15f, localScale).method = UITweener.Method.EaseOut;
		if (placeAbove)
		{
			Vector3 localPosition = cachedTransform.localPosition;
			cachedTransform.localPosition = new Vector3(localPosition.x, localPosition.y - localScale.y + num, localPosition.z);
			TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
		}
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x0002D50C File Offset: 0x0002B70C
	private void Animate(UIWidget widget, bool placeAbove, float bottom)
	{
		this.AnimateColor(widget);
		this.AnimatePosition(widget, placeAbove, bottom);
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x0002D520 File Offset: 0x0002B720
	private void OnClick()
	{
		if (this.mChild == null && this.atlas != null && this.font != null && this.items.Count > 1)
		{
			this.mLabelList.Clear();
			this.handleEvents = true;
			if (this.mPanel == null)
			{
				this.mPanel = UIPanel.Find(base.transform, true);
			}
			Transform transform = base.transform;
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform.parent, transform);
			this.mChild = new GameObject("Drop-down List");
			this.mChild.layer = base.gameObject.layer;
			Transform transform2 = this.mChild.transform;
			transform2.parent = transform.parent;
			transform2.localPosition = bounds.min;
			transform2.localRotation = Quaternion.identity;
			transform2.localScale = Vector3.one;
			this.mBackground = NGUITools.AddSprite(this.mChild, this.atlas, this.backgroundSprite);
			this.mBackground.pivot = UIWidget.Pivot.TopLeft;
			this.mBackground.depth = NGUITools.CalculateNextDepth(this.mPanel.gameObject);
			this.mBackground.color = this.backgroundColor;
			Vector4 border = this.mBackground.border;
			this.mBgBorder = border.y;
			this.mBackground.cachedTransform.localPosition = new Vector3(0f, border.y, 0f);
			this.mHighlight = NGUITools.AddSprite(this.mChild, this.atlas, this.highlightSprite);
			this.mHighlight.pivot = UIWidget.Pivot.TopLeft;
			this.mHighlight.color = this.highlightColor;
			UIAtlas.Sprite sprite = this.mHighlight.sprite;
			float num = sprite.inner.yMin - sprite.outer.yMin;
			float num2 = (float)this.font.size * this.textScale;
			float num3 = 0f;
			float num4 = 0f - this.padding.y;
			List<UILabel> list = new List<UILabel>();
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				string text = this.items[i];
				UILabel uilabel = NGUITools.AddWidget<UILabel>(this.mChild);
				uilabel.pivot = UIWidget.Pivot.TopLeft;
				uilabel.font = this.font;
				uilabel.text = ((!this.isLocalized || !(Localization.instance != null)) ? text : Localization.instance.Get(text));
				uilabel.color = this.textColor;
				uilabel.cachedTransform.localPosition = new Vector3(border.x, num4, 0f);
				uilabel.MakePixelPerfect();
				if (this.textScale != 1f)
				{
					Vector3 localScale = uilabel.cachedTransform.localScale;
					uilabel.cachedTransform.localScale = localScale * this.textScale;
				}
				list.Add(uilabel);
				num4 -= num2;
				num4 -= this.padding.y;
				num3 = Mathf.Max(num3, uilabel.relativeSize.x * num2);
				UIEventListener uieventListener = UIEventListener.Get(uilabel.gameObject);
				uieventListener.onHover = new UIEventListener.BoolDelegate(this.OnItemHover);
				uieventListener.onPress = new UIEventListener.BoolDelegate(this.OnItemPress);
				uieventListener.parameter = text;
				if (this.mSelectedItem == text)
				{
					this.Highlight(uilabel, true);
				}
				this.mLabelList.Add(uilabel);
				i++;
			}
			num3 = Mathf.Max(num3, bounds.size.x - border.x * 2f);
			Vector3 vector = new Vector3(num3 * 0.5f / num2, -0.5f, 0f);
			Vector3 vector2 = new Vector3(num3 / num2, (num2 + this.padding.y) / num2, 1f);
			int j = 0;
			int count2 = list.Count;
			while (j < count2)
			{
				BoxCollider boxCollider = NGUITools.AddWidgetCollider(list[j].gameObject);
				vector.z = boxCollider.center.z;
				boxCollider.center = vector;
				boxCollider.size = vector2;
				j++;
			}
			num3 += border.x * 2f;
			num4 -= border.y;
			this.mBackground.cachedTransform.localScale = new Vector3(num3, 0f - num4 + border.y, 1f);
			this.mHighlight.cachedTransform.localScale = new Vector3(num3 - border.x * 2f + (sprite.inner.xMin - sprite.outer.xMin) * 2f, num2 + num * 2f, 1f);
			bool flag = this.position == UIPopupList.Position.Above;
			if (this.position == UIPopupList.Position.Auto)
			{
				UICamera uicamera = UICamera.FindCameraForLayer(base.gameObject.layer);
				if (uicamera != null)
				{
					flag = uicamera.cachedCamera.WorldToViewportPoint(transform.position).y < 0.5f;
				}
			}
			if (this.isAnimated)
			{
				float num5 = num4 + num2;
				this.Animate(this.mHighlight, flag, num5);
				int k = 0;
				int count3 = list.Count;
				while (k < count3)
				{
					this.Animate(list[k], flag, num5);
					k++;
				}
				this.AnimateColor(this.mBackground);
				this.AnimateScale(this.mBackground, flag, num5);
			}
			if (flag)
			{
				transform2.localPosition = new Vector3(bounds.min.x, bounds.max.y - num4 - border.y, bounds.min.z);
				return;
			}
		}
		else
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x04000776 RID: 1910
	private const float animSpeed = 0.15f;

	// Token: 0x04000777 RID: 1911
	public UIAtlas atlas;

	// Token: 0x04000778 RID: 1912
	public UIFont font;

	// Token: 0x04000779 RID: 1913
	public UILabel textLabel;

	// Token: 0x0400077A RID: 1914
	public string backgroundSprite;

	// Token: 0x0400077B RID: 1915
	public string highlightSprite;

	// Token: 0x0400077C RID: 1916
	public UIPopupList.Position position;

	// Token: 0x0400077D RID: 1917
	public List<string> items = new List<string>();

	// Token: 0x0400077E RID: 1918
	public Vector2 padding = new Vector3(4f, 4f);

	// Token: 0x0400077F RID: 1919
	public float textScale = 1f;

	// Token: 0x04000780 RID: 1920
	public Color textColor = Color.white;

	// Token: 0x04000781 RID: 1921
	public Color backgroundColor = Color.white;

	// Token: 0x04000782 RID: 1922
	public Color highlightColor = new Color(0.59607846f, 1f, 0.2f, 1f);

	// Token: 0x04000783 RID: 1923
	public bool isAnimated = true;

	// Token: 0x04000784 RID: 1924
	public bool isLocalized;

	// Token: 0x04000785 RID: 1925
	public GameObject eventReceiver;

	// Token: 0x04000786 RID: 1926
	public string functionName = "OnSelectionChange";

	// Token: 0x04000787 RID: 1927
	[SerializeField]
	[HideInInspector]
	private string mSelectedItem;

	// Token: 0x04000788 RID: 1928
	private UIPanel mPanel;

	// Token: 0x04000789 RID: 1929
	private GameObject mChild;

	// Token: 0x0400078A RID: 1930
	private UISprite mBackground;

	// Token: 0x0400078B RID: 1931
	private UISprite mHighlight;

	// Token: 0x0400078C RID: 1932
	private UILabel mHighlightedLabel;

	// Token: 0x0400078D RID: 1933
	private List<UILabel> mLabelList = new List<UILabel>();

	// Token: 0x0400078E RID: 1934
	private float mBgBorder;

	// Token: 0x02000212 RID: 530
	public enum Position
	{
		// Token: 0x04000C0C RID: 3084
		Auto,
		// Token: 0x04000C0D RID: 3085
		Above,
		// Token: 0x04000C0E RID: 3086
		Below
	}
}
