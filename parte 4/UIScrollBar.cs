using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Scroll Bar")]
public class UIScrollBar : MonoBehaviour
{
	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0002F33C File Offset: 0x0002D53C
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0002F35E File Offset: 0x0002D55E
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = NGUITools.FindCameraForLayer(base.gameObject.layer);
			}
			return this.mCam;
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0002F38A File Offset: 0x0002D58A
	// (set) Token: 0x060008D8 RID: 2264 RVA: 0x0002F392 File Offset: 0x0002D592
	public UISprite background
	{
		get
		{
			return this.mBG;
		}
		set
		{
			if (this.mBG != value)
			{
				this.mBG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0002F3B0 File Offset: 0x0002D5B0
	// (set) Token: 0x060008DA RID: 2266 RVA: 0x0002F3B8 File Offset: 0x0002D5B8
	public UISprite foreground
	{
		get
		{
			return this.mFG;
		}
		set
		{
			if (this.mFG != value)
			{
				this.mFG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x060008DB RID: 2267 RVA: 0x0002F3D6 File Offset: 0x0002D5D6
	// (set) Token: 0x060008DC RID: 2268 RVA: 0x0002F3E0 File Offset: 0x0002D5E0
	public UIScrollBar.Direction direction
	{
		get
		{
			return this.mDir;
		}
		set
		{
			if (this.mDir == value)
			{
				return;
			}
			this.mDir = value;
			this.mIsDirty = true;
			if (!(this.mBG != null))
			{
				return;
			}
			Transform cachedTransform = this.mBG.cachedTransform;
			Vector3 localScale = cachedTransform.localScale;
			if ((this.mDir == UIScrollBar.Direction.Vertical && localScale.x > localScale.y) || (this.mDir == UIScrollBar.Direction.Horizontal && localScale.x < localScale.y))
			{
				float x = localScale.x;
				localScale.x = localScale.y;
				localScale.y = x;
				cachedTransform.localScale = localScale;
				this.ForceUpdate();
				if (this.mBG.GetComponent<Collider>() != null)
				{
					NGUITools.AddWidgetCollider(this.mBG.gameObject);
				}
				if (this.mFG.GetComponent<Collider>() != null)
				{
					NGUITools.AddWidgetCollider(this.mFG.gameObject);
				}
			}
		}
	}

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x060008DD RID: 2269 RVA: 0x0002F4C5 File Offset: 0x0002D6C5
	// (set) Token: 0x060008DE RID: 2270 RVA: 0x0002F4CD File Offset: 0x0002D6CD
	public bool inverted
	{
		get
		{
			return this.mInverted;
		}
		set
		{
			if (this.mInverted != value)
			{
				this.mInverted = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x060008DF RID: 2271 RVA: 0x0002F4E6 File Offset: 0x0002D6E6
	// (set) Token: 0x060008E0 RID: 2272 RVA: 0x0002F4F0 File Offset: 0x0002D6F0
	public float scrollValue
	{
		get
		{
			return this.mScroll;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mScroll != num)
			{
				this.mScroll = num;
				this.mIsDirty = true;
				if (this.onChange != null)
				{
					this.onChange(this);
				}
			}
		}
	}

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0002F52F File Offset: 0x0002D72F
	// (set) Token: 0x060008E2 RID: 2274 RVA: 0x0002F538 File Offset: 0x0002D738
	public float barSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mSize != num)
			{
				this.mSize = num;
				this.mIsDirty = true;
				if (this.onChange != null)
				{
					this.onChange(this);
				}
			}
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0002F577 File Offset: 0x0002D777
	// (set) Token: 0x060008E4 RID: 2276 RVA: 0x0002F5B4 File Offset: 0x0002D7B4
	public float alpha
	{
		get
		{
			if (this.mFG != null)
			{
				return this.mFG.alpha;
			}
			if (this.mBG != null)
			{
				return this.mBG.alpha;
			}
			return 0f;
		}
		set
		{
			if (this.mFG != null)
			{
				this.mFG.alpha = value;
				this.mFG.gameObject.active = this.mFG.alpha > 0.001f;
			}
			if (this.mBG != null)
			{
				this.mBG.alpha = value;
				this.mBG.gameObject.active = this.mBG.alpha > 0.001f;
			}
		}
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x0002F63C File Offset: 0x0002D83C
	private void CenterOnPos(Vector2 localPos)
	{
		if (!(this.mBG == null) && !(this.mFG == null))
		{
			Bounds bounds = NGUIMath.CalculateRelativeInnerBounds(this.cachedTransform, this.mBG);
			Bounds bounds2 = NGUIMath.CalculateRelativeInnerBounds(this.cachedTransform, this.mFG);
			if (this.mDir == UIScrollBar.Direction.Horizontal)
			{
				float num = bounds.size.x - bounds2.size.x;
				float num2 = num * 0.5f;
				float num3 = bounds.center.x - num2;
				float num4 = ((num <= 0f) ? 0f : ((localPos.x - num3) / num));
				this.scrollValue = ((!this.mInverted) ? num4 : (1f - num4));
				return;
			}
			float num5 = bounds.size.y - bounds2.size.y;
			float num6 = num5 * 0.5f;
			float num7 = bounds.center.y - num6;
			float num8 = ((num5 <= 0f) ? 0f : (1f - (localPos.y - num7) / num5));
			this.scrollValue = ((!this.mInverted) ? num8 : (1f - num8));
		}
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x0002F774 File Offset: 0x0002D974
	private void Reposition(Vector2 screenPos)
	{
		Transform cachedTransform = this.cachedTransform;
		Plane plane = new Plane(cachedTransform.rotation * Vector3.back, cachedTransform.position);
		Ray ray = this.cachedCamera.ScreenPointToRay(screenPos);
		float num;
		if (plane.Raycast(ray, out num))
		{
			this.CenterOnPos(cachedTransform.InverseTransformPoint(ray.GetPoint(num)));
		}
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x0002F7DC File Offset: 0x0002D9DC
	private void OnPressBackground(GameObject go, bool isPressed)
	{
		this.mCam = UICamera.currentCamera;
		this.Reposition(UICamera.lastTouchPosition);
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x0002F7F4 File Offset: 0x0002D9F4
	private void OnDragBackground(GameObject go, Vector2 delta)
	{
		this.mCam = UICamera.currentCamera;
		this.Reposition(UICamera.lastTouchPosition);
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x0002F80C File Offset: 0x0002DA0C
	private void OnPressForeground(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			this.mCam = UICamera.currentCamera;
			Bounds bounds = NGUIMath.CalculateAbsoluteWidgetBounds(this.mFG.cachedTransform);
			this.mScreenPos = this.mCam.WorldToScreenPoint(bounds.center);
		}
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x0002F855 File Offset: 0x0002DA55
	private void OnDragForeground(GameObject go, Vector2 delta)
	{
		this.mCam = UICamera.currentCamera;
		this.Reposition(this.mScreenPos + UICamera.currentTouch.totalDelta);
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x0002F880 File Offset: 0x0002DA80
	private void Start()
	{
		if (this.background != null && this.background.GetComponent<Collider>() != null)
		{
			UIEventListener uieventListener = UIEventListener.Get(this.background.gameObject);
			uieventListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener.onPress, new UIEventListener.BoolDelegate(this.OnPressBackground));
			uieventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener.onDrag, new UIEventListener.VectorDelegate(this.OnDragBackground));
		}
		if (this.foreground != null && this.foreground.GetComponent<Collider>() != null)
		{
			UIEventListener uieventListener2 = UIEventListener.Get(this.foreground.gameObject);
			uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPressForeground));
			uieventListener2.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener2.onDrag, new UIEventListener.VectorDelegate(this.OnDragForeground));
		}
		this.ForceUpdate();
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x0002F97B File Offset: 0x0002DB7B
	private void Update()
	{
		if (this.mIsDirty)
		{
			this.ForceUpdate();
		}
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x0002F98C File Offset: 0x0002DB8C
	public void ForceUpdate()
	{
		this.mIsDirty = false;
		if (!(this.mBG != null) || !(this.mFG != null))
		{
			return;
		}
		this.mSize = Mathf.Clamp01(this.mSize);
		this.mScroll = Mathf.Clamp01(this.mScroll);
		Vector4 border = this.mBG.border;
		Vector4 border2 = this.mFG.border;
		Vector2 vector = new Vector2(Mathf.Max(0f, this.mBG.cachedTransform.localScale.x - border.x - border.z), Mathf.Max(0f, this.mBG.cachedTransform.localScale.y - border.y - border.w));
		float num = ((!this.mInverted) ? this.mScroll : (1f - this.mScroll));
		if (this.mDir == UIScrollBar.Direction.Horizontal)
		{
			Vector2 vector2 = new Vector2(vector.x * this.mSize, vector.y);
			this.mFG.pivot = UIWidget.Pivot.Left;
			this.mBG.pivot = UIWidget.Pivot.Left;
			this.mBG.cachedTransform.localPosition = Vector3.zero;
			this.mFG.cachedTransform.localPosition = new Vector3(border.x - border2.x + (vector.x - vector2.x) * num, 0f, 0f);
			this.mFG.cachedTransform.localScale = new Vector3(vector2.x + border2.x + border2.z, vector2.y + border2.y + border2.w, 1f);
			if (num < 0.999f && num > 0.001f)
			{
				this.mFG.MakePixelPerfect();
				return;
			}
		}
		else
		{
			Vector2 vector3 = new Vector2(vector.x, vector.y * this.mSize);
			this.mFG.pivot = UIWidget.Pivot.Top;
			this.mBG.pivot = UIWidget.Pivot.Top;
			this.mBG.cachedTransform.localPosition = Vector3.zero;
			this.mFG.cachedTransform.localPosition = new Vector3(0f, 0f - border.y + border2.y - (vector.y - vector3.y) * num, 0f);
			this.mFG.cachedTransform.localScale = new Vector3(vector3.x + border2.x + border2.z, vector3.y + border2.y + border2.w, 1f);
			if (num < 0.999f && num > 0.001f)
			{
				this.mFG.MakePixelPerfect();
			}
		}
	}

	// Token: 0x040007C1 RID: 1985
	[SerializeField]
	[HideInInspector]
	private UISprite mBG;

	// Token: 0x040007C2 RID: 1986
	[HideInInspector]
	[SerializeField]
	private UISprite mFG;

	// Token: 0x040007C3 RID: 1987
	[HideInInspector]
	[SerializeField]
	private UIScrollBar.Direction mDir;

	// Token: 0x040007C4 RID: 1988
	[HideInInspector]
	[SerializeField]
	private bool mInverted;

	// Token: 0x040007C5 RID: 1989
	[SerializeField]
	[HideInInspector]
	private float mScroll;

	// Token: 0x040007C6 RID: 1990
	[SerializeField]
	[HideInInspector]
	private float mSize = 1f;

	// Token: 0x040007C7 RID: 1991
	private Transform mTrans;

	// Token: 0x040007C8 RID: 1992
	private bool mIsDirty;

	// Token: 0x040007C9 RID: 1993
	private Camera mCam;

	// Token: 0x040007CA RID: 1994
	private Vector2 mScreenPos = Vector2.zero;

	// Token: 0x040007CB RID: 1995
	public UIScrollBar.OnScrollBarChange onChange;

	// Token: 0x02000218 RID: 536
	public enum Direction
	{
		// Token: 0x04000C2B RID: 3115
		Horizontal,
		// Token: 0x04000C2C RID: 3116
		Vertical
	}

	// Token: 0x02000219 RID: 537
	// (Invoke) Token: 0x06000C88 RID: 3208
	public delegate void OnScrollBarChange(UIScrollBar sb);
}
