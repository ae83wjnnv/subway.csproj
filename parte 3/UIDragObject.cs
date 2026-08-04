using System;
using UnityEngine;

// Token: 0x02000102 RID: 258
[AddComponentMenu("NGUI/Interaction/Drag Object")]
public class UIDragObject : IgnoreTimeScale
{
	// Token: 0x06000745 RID: 1861 RVA: 0x000242B0 File Offset: 0x000224B0
	private void FindPanel()
	{
		this.mPanel = ((!(this.target != null)) ? null : UIPanel.Find(this.target.transform, false));
		if (this.mPanel == null)
		{
			this.restrictWithinPanel = false;
		}
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x000242F0 File Offset: 0x000224F0
	private void OnPress(bool pressed)
	{
		if (!base.enabled || !base.gameObject.active || !(this.target != null))
		{
			return;
		}
		this.mPressed = pressed;
		if (pressed)
		{
			if (this.restrictWithinPanel && this.mPanel == null)
			{
				this.FindPanel();
			}
			if (this.restrictWithinPanel)
			{
				this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mPanel.cachedTransform, this.target);
			}
			this.mMomentum = Vector3.zero;
			this.mScroll = 0f;
			SpringPosition component = this.target.GetComponent<SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
			this.mLastPos = UICamera.lastHit.point;
			Transform transform = UICamera.currentCamera.transform;
			this.mPlane = new Plane(((!(this.mPanel != null)) ? transform.rotation : this.mPanel.cachedTransform.rotation) * Vector3.back, this.mLastPos);
			return;
		}
		if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect == UIDragObject.DragEffect.MomentumAndSpring)
		{
			this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, false);
		}
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00024434 File Offset: 0x00022634
	private void OnDrag(Vector2 delta)
	{
		if (!base.enabled || !base.gameObject.active || !(this.target != null))
		{
			return;
		}
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
		Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
		float num = 0f;
		if (!this.mPlane.Raycast(ray, out num))
		{
			return;
		}
		Vector3 point = ray.GetPoint(num);
		Vector3 vector = point - this.mLastPos;
		this.mLastPos = point;
		if (vector.x != 0f || vector.y != 0f)
		{
			vector = this.target.InverseTransformDirection(vector);
			vector.Scale(this.scale);
			vector = this.target.TransformDirection(vector);
		}
		this.mMomentum = Vector3.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
		if (this.restrictWithinPanel)
		{
			Vector3 localPosition = this.target.localPosition;
			this.target.position += vector;
			this.mBounds.center = this.mBounds.center + (this.target.localPosition - localPosition);
			if (this.dragEffect != UIDragObject.DragEffect.MomentumAndSpring && this.mPanel.clipping != UIDrawCall.Clipping.None && this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, true))
			{
				this.mMomentum = Vector3.zero;
				this.mScroll = 0f;
				return;
			}
		}
		else
		{
			this.target.position += vector;
		}
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x000245E8 File Offset: 0x000227E8
	private void LateUpdate()
	{
		float num = base.UpdateRealTimeDelta();
		if (this.target == null)
		{
			return;
		}
		if (this.mPressed)
		{
			SpringPosition component = this.target.GetComponent<SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
			this.mScroll = 0f;
		}
		else
		{
			this.mMomentum += this.scale * ((0f - this.mScroll) * 0.05f);
			this.mScroll = NGUIMath.SpringLerp(this.mScroll, 0f, 20f, num);
			if (this.mMomentum.magnitude > 0.0001f)
			{
				if (this.mPanel == null)
				{
					this.FindPanel();
				}
				if (this.mPanel != null)
				{
					this.target.position += NGUIMath.SpringDampen(ref this.mMomentum, 9f, num);
					if (!this.restrictWithinPanel || this.mPanel.clipping == UIDrawCall.Clipping.None)
					{
						return;
					}
					this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mPanel.cachedTransform, this.target);
					if (!this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, this.dragEffect == UIDragObject.DragEffect.None))
					{
						SpringPosition component2 = this.target.GetComponent<SpringPosition>();
						if (component2 != null)
						{
							component2.enabled = false;
						}
					}
					return;
				}
			}
			else
			{
				this.mScroll = 0f;
			}
		}
		NGUIMath.SpringDampen(ref this.mMomentum, 9f, num);
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x00024778 File Offset: 0x00022978
	private void OnScroll(float delta)
	{
		if (base.enabled && base.gameObject.active)
		{
			if (Mathf.Sign(this.mScroll) != Mathf.Sign(delta))
			{
				this.mScroll = 0f;
			}
			this.mScroll += delta * this.scrollWheelFactor;
		}
	}

	// Token: 0x0400064B RID: 1611
	public Transform target;

	// Token: 0x0400064C RID: 1612
	public Vector3 scale = Vector3.one;

	// Token: 0x0400064D RID: 1613
	public float scrollWheelFactor;

	// Token: 0x0400064E RID: 1614
	public bool restrictWithinPanel;

	// Token: 0x0400064F RID: 1615
	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	// Token: 0x04000650 RID: 1616
	public float momentumAmount = 35f;

	// Token: 0x04000651 RID: 1617
	private Plane mPlane;

	// Token: 0x04000652 RID: 1618
	private Vector3 mLastPos;

	// Token: 0x04000653 RID: 1619
	private UIPanel mPanel;

	// Token: 0x04000654 RID: 1620
	private bool mPressed;

	// Token: 0x04000655 RID: 1621
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x04000656 RID: 1622
	private float mScroll;

	// Token: 0x04000657 RID: 1623
	private Bounds mBounds;

	// Token: 0x020001FF RID: 511
	public enum DragEffect
	{
		// Token: 0x04000BD3 RID: 3027
		None,
		// Token: 0x04000BD4 RID: 3028
		Momentum,
		// Token: 0x04000BD5 RID: 3029
		MomentumAndSpring
	}
}
