using System;
using UnityEngine;

// Token: 0x02000131 RID: 305
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Slider")]
public class UISlider : IgnoreTimeScale
{
	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06000906 RID: 2310 RVA: 0x000307B6 File Offset: 0x0002E9B6
	// (set) Token: 0x06000907 RID: 2311 RVA: 0x000307BE File Offset: 0x0002E9BE
	public float sliderValue
	{
		get
		{
			return this.mStepValue;
		}
		set
		{
			this.Set(value, false);
		}
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x000307C8 File Offset: 0x0002E9C8
	private void Init()
	{
		this.mInitDone = true;
		if (this.foreground != null)
		{
			this.mFGWidget = this.foreground.GetComponent<UIWidget>();
			this.mFGFilled = ((!(this.mFGWidget != null)) ? null : (this.mFGWidget as UIFilledSprite));
			this.mFGTrans = this.foreground.transform;
			if (this.fullSize == Vector2.zero)
			{
				this.fullSize = this.foreground.localScale;
				return;
			}
		}
		else if (this.mCol != null)
		{
			if (this.fullSize == Vector2.zero)
			{
				this.fullSize = this.mCol.size;
				return;
			}
		}
		else
		{
			Debug.LogWarning("UISlider expected to find a foreground object or a box collider to work with", this);
		}
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00030899 File Offset: 0x0002EA99
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mCol = base.GetComponent<Collider>() as BoxCollider;
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x000308B8 File Offset: 0x0002EAB8
	private void Start()
	{
		this.Init();
		if (Application.isPlaying && this.thumb != null && this.thumb.GetComponent<Collider>() != null)
		{
			UIEventListener uieventListener = UIEventListener.Get(this.thumb.gameObject);
			uieventListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener.onPress, new UIEventListener.BoolDelegate(this.OnPressThumb));
			uieventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener.onDrag, new UIEventListener.VectorDelegate(this.OnDragThumb));
		}
		this.Set(this.rawValue, true);
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x00030953 File Offset: 0x0002EB53
	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			this.UpdateDrag();
		}
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0003095E File Offset: 0x0002EB5E
	private void OnDrag(Vector2 delta)
	{
		this.UpdateDrag();
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x00030966 File Offset: 0x0002EB66
	private void OnPressThumb(GameObject go, bool pressed)
	{
		if (pressed)
		{
			this.UpdateDrag();
		}
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x00030971 File Offset: 0x0002EB71
	private void OnDragThumb(GameObject go, Vector2 delta)
	{
		this.UpdateDrag();
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x0003097C File Offset: 0x0002EB7C
	private void OnKey(KeyCode key)
	{
		float num = (((float)this.numberOfSteps <= 1f) ? 0.125f : (1f / (float)(this.numberOfSteps - 1)));
		if (this.direction == UISlider.Direction.Horizontal)
		{
			if (key == KeyCode.RightArrow)
			{
				this.Set(this.rawValue + num, false);
				return;
			}
			if (key == KeyCode.LeftArrow)
			{
				this.Set(this.rawValue - num, false);
				return;
			}
		}
		else if (key != KeyCode.UpArrow)
		{
			if (key == KeyCode.DownArrow)
			{
				this.Set(this.rawValue - num, false);
				return;
			}
		}
		else
		{
			this.Set(this.rawValue + num, false);
		}
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x00030A18 File Offset: 0x0002EC18
	private void UpdateDrag()
	{
		if (!(this.mCol == null) && !(UICamera.currentCamera == null) && UICamera.currentTouch != null)
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
			Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
			float num;
			if (new Plane(this.mTrans.rotation * Vector3.back, this.mTrans.position).Raycast(ray, out num))
			{
				Vector3 vector = this.mTrans.localPosition + this.mCol.center - this.mCol.extents;
				Vector3 vector2 = this.mTrans.localPosition - vector;
				Vector3 vector3 = this.mTrans.InverseTransformPoint(ray.GetPoint(num)) + vector2;
				this.Set((this.direction != UISlider.Direction.Horizontal) ? (vector3.y / this.mCol.size.y) : (vector3.x / this.mCol.size.x), false);
			}
		}
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00030B48 File Offset: 0x0002ED48
	private void Set(float input, bool force)
	{
		if (!this.mInitDone)
		{
			this.Init();
		}
		float num = Mathf.Clamp01(input);
		if (num < 0.001f)
		{
			num = 0f;
		}
		this.rawValue = num;
		if (this.numberOfSteps > 1)
		{
			num = Mathf.Round(num * (float)(this.numberOfSteps - 1)) / (float)(this.numberOfSteps - 1);
		}
		if (!force && this.mStepValue == num)
		{
			return;
		}
		this.mStepValue = num;
		Vector3 vector = this.fullSize;
		if (this.direction == UISlider.Direction.Horizontal)
		{
			vector.x *= this.mStepValue;
		}
		else
		{
			vector.y *= this.mStepValue;
		}
		if (this.mFGFilled != null)
		{
			this.mFGFilled.fillAmount = this.mStepValue;
		}
		else if (this.foreground != null)
		{
			this.mFGTrans.localScale = vector;
			if (this.mFGWidget != null)
			{
				if (num > 0.001f)
				{
					this.mFGWidget.enabled = true;
					this.mFGWidget.MarkAsChanged();
				}
				else
				{
					this.mFGWidget.enabled = false;
				}
			}
		}
		if (this.thumb != null)
		{
			Vector3 localPosition = this.thumb.localPosition;
			if (this.mFGFilled != null)
			{
				if (this.mFGFilled.fillDirection == UIFilledSprite.FillDirection.Horizontal)
				{
					localPosition.x = ((!this.mFGFilled.invert) ? vector.x : (this.fullSize.x - vector.x));
				}
				else if (this.mFGFilled.fillDirection == UIFilledSprite.FillDirection.Vertical)
				{
					localPosition.y = ((!this.mFGFilled.invert) ? vector.y : (this.fullSize.y - vector.y));
				}
			}
			else if (this.direction == UISlider.Direction.Horizontal)
			{
				localPosition.x = vector.x;
			}
			else
			{
				localPosition.y = vector.y;
			}
			this.thumb.localPosition = localPosition;
		}
		if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName) && Application.isPlaying)
		{
			UISlider.current = this;
			this.eventReceiver.SendMessage(this.functionName, this.mStepValue, SendMessageOptions.DontRequireReceiver);
			UISlider.current = null;
		}
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x00030D86 File Offset: 0x0002EF86
	public void ForceUpdate()
	{
		this.Set(this.rawValue, true);
	}

	// Token: 0x040007D9 RID: 2009
	public static UISlider current;

	// Token: 0x040007DA RID: 2010
	public Transform foreground;

	// Token: 0x040007DB RID: 2011
	public Transform thumb;

	// Token: 0x040007DC RID: 2012
	public UISlider.Direction direction;

	// Token: 0x040007DD RID: 2013
	public Vector2 fullSize = Vector2.zero;

	// Token: 0x040007DE RID: 2014
	public GameObject eventReceiver;

	// Token: 0x040007DF RID: 2015
	public string functionName = "OnSliderChange";

	// Token: 0x040007E0 RID: 2016
	public int numberOfSteps;

	// Token: 0x040007E1 RID: 2017
	[SerializeField]
	[HideInInspector]
	private float rawValue = 1f;

	// Token: 0x040007E2 RID: 2018
	private float mStepValue = 1f;

	// Token: 0x040007E3 RID: 2019
	private BoxCollider mCol;

	// Token: 0x040007E4 RID: 2020
	private Transform mTrans;

	// Token: 0x040007E5 RID: 2021
	private Transform mFGTrans;

	// Token: 0x040007E6 RID: 2022
	private UIWidget mFGWidget;

	// Token: 0x040007E7 RID: 2023
	private UIFilledSprite mFGFilled;

	// Token: 0x040007E8 RID: 2024
	private bool mInitDone;

	// Token: 0x0200021A RID: 538
	public enum Direction
	{
		// Token: 0x04000C2E RID: 3118
		Horizontal,
		// Token: 0x04000C2F RID: 3119
		Vertical
	}
}
