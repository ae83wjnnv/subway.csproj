using System;
using UnityEngine;

// Token: 0x02000105 RID: 261
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Interaction/Draggable Panel")]
[ExecuteInEditMode]
public class UIDraggablePanel : IgnoreTimeScale
{
	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x0600075B RID: 1883 RVA: 0x00024DEE File Offset: 0x00022FEE
	public Bounds bounds
	{
		get
		{
			if (!this.mCalculatedBounds)
			{
				this.mCalculatedBounds = true;
				this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mTrans, this.mTrans);
			}
			return this.mBounds;
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x0600075C RID: 1884 RVA: 0x00024E1C File Offset: 0x0002301C
	public bool shouldMoveHorizontally
	{
		get
		{
			float num = this.bounds.size.x;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.x * 2f;
			}
			return num > this.mPanel.clipRange.z;
		}
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x0600075D RID: 1885 RVA: 0x00024E78 File Offset: 0x00023078
	public bool shouldMoveVertically
	{
		get
		{
			float num = this.bounds.size.y;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.y * 2f;
			}
			return num > this.mPanel.clipRange.w;
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x0600075E RID: 1886 RVA: 0x00024ED4 File Offset: 0x000230D4
	private bool shouldMove
	{
		get
		{
			if (!this.disableDragIfFits)
			{
				return true;
			}
			if (this.mPanel == null)
			{
				this.mPanel = base.GetComponent<UIPanel>();
			}
			Vector4 clipRange = this.mPanel.clipRange;
			Bounds bounds = this.bounds;
			float num = clipRange.z * 0.5f;
			float num2 = clipRange.w * 0.5f;
			if (!Mathf.Approximately(this.scale.x, 0f))
			{
				if (bounds.min.x < clipRange.x - num)
				{
					return true;
				}
				if (bounds.max.x > clipRange.x + num)
				{
					return true;
				}
			}
			if (!Mathf.Approximately(this.scale.y, 0f))
			{
				if (bounds.min.y < clipRange.y - num2)
				{
					return true;
				}
				if (bounds.max.y > clipRange.y + num2)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x0600075F RID: 1887 RVA: 0x00024FC1 File Offset: 0x000231C1
	// (set) Token: 0x06000760 RID: 1888 RVA: 0x00024FC9 File Offset: 0x000231C9
	public Vector3 currentMomentum
	{
		get
		{
			return this.mMomentum;
		}
		set
		{
			this.mMomentum = value;
		}
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x00024FD2 File Offset: 0x000231D2
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mPanel = base.GetComponent<UIPanel>();
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00024FEC File Offset: 0x000231EC
	private void Start()
	{
		this.UpdateScrollbars(true);
		if (this.horizontalScrollBar != null)
		{
			UIScrollBar uiscrollBar = this.horizontalScrollBar;
			uiscrollBar.onChange = (UIScrollBar.OnScrollBarChange)Delegate.Combine(uiscrollBar.onChange, new UIScrollBar.OnScrollBarChange(this.OnHorizontalBar));
			this.horizontalScrollBar.alpha = ((this.showScrollBars != UIDraggablePanel.ShowCondition.Always && !this.shouldMoveHorizontally) ? 0f : 1f);
		}
		if (this.verticalScrollBar != null)
		{
			UIScrollBar uiscrollBar2 = this.verticalScrollBar;
			uiscrollBar2.onChange = (UIScrollBar.OnScrollBarChange)Delegate.Combine(uiscrollBar2.onChange, new UIScrollBar.OnScrollBarChange(this.OnVerticalBar));
			this.verticalScrollBar.alpha = ((this.showScrollBars != UIDraggablePanel.ShowCondition.Always && !this.shouldMoveVertically) ? 0f : 1f);
		}
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x000250B8 File Offset: 0x000232B8
	public void RestrictWithinBounds(bool instant)
	{
		Vector3 vector = this.mPanel.CalculateConstrainOffset(this.bounds.min, this.bounds.max);
		if (vector.magnitude <= 0.001f)
		{
			this.DisableSpring();
			return;
		}
		if (!instant && this.dragEffect == UIDraggablePanel.DragEffect.MomentumAndSpring)
		{
			SpringPanel.Begin(this.mPanel.gameObject, this.mTrans.localPosition + vector, 13f);
			return;
		}
		this.MoveRelative(vector);
		this.mMomentum = Vector3.zero;
		this.mScroll = 0f;
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x00025160 File Offset: 0x00023360
	public void DisableSpring()
	{
		SpringPanel component = base.GetComponent<SpringPanel>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x00025184 File Offset: 0x00023384
	public void UpdateScrollbars(bool recalculateBounds)
	{
		if (this.mPanel == null)
		{
			return;
		}
		if (this.horizontalScrollBar != null || this.verticalScrollBar != null)
		{
			if (recalculateBounds)
			{
				this.mCalculatedBounds = false;
				this.mShouldMove = this.shouldMove;
			}
			if (this.horizontalScrollBar != null)
			{
				Bounds bounds = this.bounds;
				Vector3 size = bounds.size;
				if (size.x > 0f)
				{
					Vector4 clipRange = this.mPanel.clipRange;
					float num = clipRange.z * 0.5f;
					float num2 = clipRange.x - num - bounds.min.x;
					float num3 = bounds.max.x - num - clipRange.x;
					if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
					{
						num2 += this.mPanel.clipSoftness.x;
						num3 -= this.mPanel.clipSoftness.x;
					}
					num2 = Mathf.Clamp01(num2 / size.x);
					num3 = Mathf.Clamp01(num3 / size.x);
					float num4 = num2 + num3;
					this.mIgnoreCallbacks = true;
					this.horizontalScrollBar.barSize = 1f - num4;
					this.horizontalScrollBar.scrollValue = ((num4 <= 0.001f) ? 0f : (num2 / num4));
					this.mIgnoreCallbacks = false;
				}
			}
			if (!(this.verticalScrollBar != null))
			{
				return;
			}
			Bounds bounds2 = this.bounds;
			Vector3 size2 = bounds2.size;
			if (size2.y > 0f)
			{
				Vector4 clipRange2 = this.mPanel.clipRange;
				float num5 = clipRange2.w * 0.5f;
				float num6 = clipRange2.y - num5 - bounds2.min.y;
				float num7 = bounds2.max.y - num5 - clipRange2.y;
				if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
				{
					num6 += this.mPanel.clipSoftness.y;
					num7 -= this.mPanel.clipSoftness.y;
				}
				num6 = Mathf.Clamp01(num6 / size2.y);
				num7 = Mathf.Clamp01(num7 / size2.y);
				float num8 = num6 + num7;
				this.mIgnoreCallbacks = true;
				this.verticalScrollBar.barSize = 1f - num8;
				this.verticalScrollBar.scrollValue = ((num8 <= 0.001f) ? 0f : (1f - num6 / num8));
				this.mIgnoreCallbacks = false;
				return;
			}
		}
		else if (recalculateBounds)
		{
			this.mCalculatedBounds = false;
		}
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x00025420 File Offset: 0x00023620
	public void SetDragAmount(float x, float y, bool updateScrollbars)
	{
		this.DisableSpring();
		Bounds bounds = this.bounds;
		if (bounds.min.x == bounds.max.x || bounds.min.y == bounds.max.x)
		{
			return;
		}
		Vector4 clipRange = this.mPanel.clipRange;
		float num = clipRange.z * 0.5f;
		float num2 = clipRange.w * 0.5f;
		float num3 = bounds.min.x + num;
		float num4 = bounds.max.x - num;
		float num5 = bounds.min.y + num2;
		float num6 = bounds.max.y - num2;
		if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
		{
			num3 -= this.mPanel.clipSoftness.x;
			num4 += this.mPanel.clipSoftness.x;
			num5 -= this.mPanel.clipSoftness.y;
			num6 += this.mPanel.clipSoftness.y;
		}
		float num7 = Mathf.Lerp(num3, num4, x);
		float num8 = Mathf.Lerp(num6, num5, y);
		if (!updateScrollbars)
		{
			Vector3 localPosition = this.mTrans.localPosition;
			if (this.scale.x != 0f)
			{
				localPosition.x += clipRange.x - num7;
			}
			if (this.scale.y != 0f)
			{
				localPosition.y += clipRange.y - num8;
			}
			this.mTrans.localPosition = localPosition;
		}
		clipRange.x = num7;
		clipRange.y = num8;
		this.mPanel.clipRange = clipRange;
		if (updateScrollbars)
		{
			this.UpdateScrollbars(false);
		}
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x000255E0 File Offset: 0x000237E0
	public void ResetPosition()
	{
		this.mCalculatedBounds = false;
		this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, false);
		this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, true);
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00025630 File Offset: 0x00023830
	private void OnHorizontalBar(UIScrollBar sb)
	{
		if (!this.mIgnoreCallbacks)
		{
			float num = ((!(this.horizontalScrollBar != null)) ? 0f : this.horizontalScrollBar.scrollValue);
			float num2 = ((!(this.verticalScrollBar != null)) ? 0f : this.verticalScrollBar.scrollValue);
			this.SetDragAmount(num, num2, false);
		}
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x00025690 File Offset: 0x00023890
	private void OnVerticalBar(UIScrollBar sb)
	{
		if (!this.mIgnoreCallbacks)
		{
			float num = ((!(this.horizontalScrollBar != null)) ? 0f : this.horizontalScrollBar.scrollValue);
			float num2 = ((!(this.verticalScrollBar != null)) ? 0f : this.verticalScrollBar.scrollValue);
			this.SetDragAmount(num, num2, false);
		}
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x000256F0 File Offset: 0x000238F0
	private void MoveRelative(Vector3 relative)
	{
		this.mTrans.localPosition += relative;
		Vector4 clipRange = this.mPanel.clipRange;
		clipRange.x -= relative.x;
		clipRange.y -= relative.y;
		this.mPanel.clipRange = clipRange;
		this.UpdateScrollbars(false);
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00025758 File Offset: 0x00023958
	private void MoveAbsolute(Vector3 absolute)
	{
		Vector3 vector = this.mTrans.InverseTransformPoint(absolute);
		Vector3 vector2 = this.mTrans.InverseTransformPoint(Vector3.zero);
		this.MoveRelative(vector - vector2);
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x00025790 File Offset: 0x00023990
	public void Press(bool pressed)
	{
		if (!base.enabled || !base.gameObject.active)
		{
			return;
		}
		this.mTouches += (pressed ? 1 : (-1));
		this.mCalculatedBounds = false;
		this.mShouldMove = this.shouldMove;
		if (this.mShouldMove)
		{
			this.mPressed = pressed;
			if (pressed)
			{
				this.mMomentum = Vector3.zero;
				this.mScroll = 0f;
				this.DisableSpring();
				this.mLastPos = UICamera.lastHit.point;
				this.mPlane = new Plane(this.mTrans.rotation * Vector3.back, this.mLastPos);
				return;
			}
			if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect == UIDraggablePanel.DragEffect.MomentumAndSpring)
			{
				this.RestrictWithinBounds(false);
			}
		}
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00025868 File Offset: 0x00023A68
	public void Drag(Vector2 delta)
	{
		if (!base.enabled || !base.gameObject.active || !this.mShouldMove)
		{
			return;
		}
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
		Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
		float num = 0f;
		if (this.mPlane.Raycast(ray, out num))
		{
			Vector3 point = ray.GetPoint(num);
			Vector3 vector = point - this.mLastPos;
			this.mLastPos = point;
			if (vector.x != 0f || vector.y != 0f)
			{
				vector = this.mTrans.InverseTransformDirection(vector);
				vector.Scale(this.scale);
				vector = this.mTrans.TransformDirection(vector);
			}
			this.mMomentum = Vector3.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
			this.MoveAbsolute(vector);
			if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect != UIDraggablePanel.DragEffect.MomentumAndSpring)
			{
				this.RestrictWithinBounds(false);
			}
		}
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00025990 File Offset: 0x00023B90
	public void Scroll(float delta)
	{
		if (base.enabled && base.gameObject.active)
		{
			this.mShouldMove = this.shouldMove;
			if (Mathf.Sign(this.mScroll) != Mathf.Sign(delta))
			{
				this.mScroll = 0f;
			}
			this.mScroll += delta * this.scrollWheelFactor;
		}
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x000259F4 File Offset: 0x00023BF4
	private void LateUpdate()
	{
		if (this.mPanel.changedLastFrame)
		{
			this.UpdateScrollbars(true);
		}
		if (this.repositionClipping)
		{
			this.repositionClipping = false;
			this.mCalculatedBounds = false;
			this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, true);
		}
		if (!Application.isPlaying)
		{
			return;
		}
		float num = base.UpdateRealTimeDelta();
		if (this.showScrollBars != UIDraggablePanel.ShowCondition.Always)
		{
			bool flag = false;
			bool flag2 = false;
			if (this.showScrollBars != UIDraggablePanel.ShowCondition.WhenDragging || this.mTouches > 0)
			{
				flag = this.shouldMoveVertically;
				flag2 = this.shouldMoveHorizontally;
			}
			if (this.verticalScrollBar)
			{
				float num2 = this.verticalScrollBar.alpha;
				num2 += ((!flag) ? ((0f - num) * 3f) : (num * 6f));
				num2 = Mathf.Clamp01(num2);
				if (this.verticalScrollBar.alpha != num2)
				{
					this.verticalScrollBar.alpha = num2;
				}
			}
			if (this.horizontalScrollBar)
			{
				float num3 = this.horizontalScrollBar.alpha;
				num3 += ((!flag2) ? ((0f - num) * 3f) : (num * 6f));
				num3 = Mathf.Clamp01(num3);
				if (this.horizontalScrollBar.alpha != num3)
				{
					this.horizontalScrollBar.alpha = num3;
				}
			}
		}
		if (this.mShouldMove && !this.mPressed)
		{
			this.mMomentum += this.scale * ((0f - this.mScroll) * 0.05f);
			if (this.mMomentum.magnitude > 0.0001f)
			{
				this.mScroll = NGUIMath.SpringLerp(this.mScroll, 0f, 20f, num);
				Vector3 vector = NGUIMath.SpringDampen(ref this.mMomentum, 9f, num);
				this.MoveAbsolute(vector);
				if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None)
				{
					this.RestrictWithinBounds(false);
				}
				return;
			}
			this.mScroll = 0f;
		}
		else
		{
			this.mScroll = 0f;
		}
		NGUIMath.SpringDampen(ref this.mMomentum, 9f, num);
	}

	// Token: 0x04000665 RID: 1637
	public bool restrictWithinPanel = true;

	// Token: 0x04000666 RID: 1638
	public bool disableDragIfFits;

	// Token: 0x04000667 RID: 1639
	public UIDraggablePanel.DragEffect dragEffect = UIDraggablePanel.DragEffect.MomentumAndSpring;

	// Token: 0x04000668 RID: 1640
	public Vector3 scale = Vector3.one;

	// Token: 0x04000669 RID: 1641
	public float scrollWheelFactor;

	// Token: 0x0400066A RID: 1642
	public float momentumAmount = 35f;

	// Token: 0x0400066B RID: 1643
	public Vector2 relativePositionOnReset = Vector2.zero;

	// Token: 0x0400066C RID: 1644
	public bool repositionClipping;

	// Token: 0x0400066D RID: 1645
	public UIScrollBar horizontalScrollBar;

	// Token: 0x0400066E RID: 1646
	public UIScrollBar verticalScrollBar;

	// Token: 0x0400066F RID: 1647
	public UIDraggablePanel.ShowCondition showScrollBars = UIDraggablePanel.ShowCondition.OnlyIfNeeded;

	// Token: 0x04000670 RID: 1648
	private Transform mTrans;

	// Token: 0x04000671 RID: 1649
	private UIPanel mPanel;

	// Token: 0x04000672 RID: 1650
	private Plane mPlane;

	// Token: 0x04000673 RID: 1651
	private Vector3 mLastPos;

	// Token: 0x04000674 RID: 1652
	private bool mPressed;

	// Token: 0x04000675 RID: 1653
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x04000676 RID: 1654
	private float mScroll;

	// Token: 0x04000677 RID: 1655
	private Bounds mBounds;

	// Token: 0x04000678 RID: 1656
	private bool mCalculatedBounds;

	// Token: 0x04000679 RID: 1657
	private bool mShouldMove;

	// Token: 0x0400067A RID: 1658
	private bool mIgnoreCallbacks;

	// Token: 0x0400067B RID: 1659
	private int mTouches;

	// Token: 0x02000200 RID: 512
	public enum DragEffect
	{
		// Token: 0x04000BD7 RID: 3031
		None,
		// Token: 0x04000BD8 RID: 3032
		Momentum,
		// Token: 0x04000BD9 RID: 3033
		MomentumAndSpring
	}

	// Token: 0x02000201 RID: 513
	public enum ShowCondition
	{
		// Token: 0x04000BDB RID: 3035
		Always,
		// Token: 0x04000BDC RID: 3036
		OnlyIfNeeded,
		// Token: 0x04000BDD RID: 3037
		WhenDragging
	}
}
