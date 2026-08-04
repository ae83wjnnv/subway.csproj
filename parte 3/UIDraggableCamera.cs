using System;
using UnityEngine;

// Token: 0x02000104 RID: 260
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Interaction/Draggable Camera")]
public class UIDraggableCamera : IgnoreTimeScale
{
	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06000751 RID: 1873 RVA: 0x00024922 File Offset: 0x00022B22
	// (set) Token: 0x06000752 RID: 1874 RVA: 0x0002492A File Offset: 0x00022B2A
	public Vector2 currentMomentum
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

	// Token: 0x06000753 RID: 1875 RVA: 0x00024934 File Offset: 0x00022B34
	private void Awake()
	{
		this.mCam = base.GetComponent<Camera>();
		this.mTrans = base.transform;
		if (this.rootForBounds == null)
		{
			Debug.LogError(NGUITools.GetHierarchy(base.gameObject) + " needs the 'Root For Bounds' parameter to be set", this);
			base.enabled = false;
		}
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0002498C File Offset: 0x00022B8C
	private Vector3 CalculateConstrainOffset()
	{
		if (this.rootForBounds == null || this.rootForBounds.childCount == 0)
		{
			return Vector3.zero;
		}
		Vector3 vector = new Vector3(this.mCam.rect.xMin * (float)Screen.width, this.mCam.rect.yMin * (float)Screen.height, 0f);
		Vector3 vector2 = new Vector3(this.mCam.rect.xMax * (float)Screen.width, this.mCam.rect.yMax * (float)Screen.height, 0f);
		vector = this.mCam.ScreenToWorldPoint(vector);
		vector2 = this.mCam.ScreenToWorldPoint(vector2);
		Vector2 vector3 = new Vector2(this.mBounds.min.x, this.mBounds.min.y);
		Vector2 vector4 = new Vector2(this.mBounds.max.x, this.mBounds.max.y);
		return NGUIMath.ConstrainRect(vector3, vector4, vector, vector2);
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00024AB8 File Offset: 0x00022CB8
	public bool ConstrainToBounds(bool immediate)
	{
		if (this.mTrans != null && this.rootForBounds != null)
		{
			Vector3 vector = this.CalculateConstrainOffset();
			if (vector.magnitude > 0f)
			{
				if (immediate)
				{
					this.mTrans.position -= vector;
				}
				else
				{
					SpringPosition springPosition = SpringPosition.Begin(base.gameObject, this.mTrans.position - vector, 13f);
					springPosition.ignoreTimeScale = true;
					springPosition.worldSpace = true;
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x00024B44 File Offset: 0x00022D44
	public void Press(bool isPressed)
	{
		if (!(this.rootForBounds != null))
		{
			return;
		}
		this.mPressed = isPressed;
		if (isPressed)
		{
			this.mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(this.rootForBounds);
			this.mMomentum = Vector2.zero;
			this.mScroll = 0f;
			SpringPosition component = base.GetComponent<SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
				return;
			}
		}
		else if (this.dragEffect == UIDragObject.DragEffect.MomentumAndSpring)
		{
			this.ConstrainToBounds(false);
		}
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00024BBC File Offset: 0x00022DBC
	public void Drag(Vector2 delta)
	{
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
		Vector2 vector = Vector2.Scale(delta, -this.scale);
		this.mTrans.localPosition += vector;
		this.mMomentum = Vector2.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
		if (this.dragEffect != UIDragObject.DragEffect.MomentumAndSpring && this.ConstrainToBounds(true))
		{
			this.mMomentum = Vector2.zero;
			this.mScroll = 0f;
		}
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00024C60 File Offset: 0x00022E60
	public void Scroll(float delta)
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

	// Token: 0x06000759 RID: 1881 RVA: 0x00024CB8 File Offset: 0x00022EB8
	private void Update()
	{
		float num = base.UpdateRealTimeDelta();
		if (this.mPressed)
		{
			SpringPosition component = base.GetComponent<SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
			this.mScroll = 0f;
			return;
		}
		this.mMomentum += this.scale * (this.mScroll * 20f);
		this.mScroll = NGUIMath.SpringLerp(this.mScroll, 0f, 20f, num);
		if (this.mMomentum.magnitude > 0.01f)
		{
			this.mTrans.localPosition += NGUIMath.SpringDampen(ref this.mMomentum, 9f, num);
			this.mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(this.rootForBounds);
			if (!this.ConstrainToBounds(this.dragEffect == UIDragObject.DragEffect.None))
			{
				SpringPosition component2 = base.GetComponent<SpringPosition>();
				if (component2 != null)
				{
					component2.enabled = false;
					return;
				}
			}
		}
		else
		{
			this.mScroll = 0f;
		}
	}

	// Token: 0x0400065A RID: 1626
	public Transform rootForBounds;

	// Token: 0x0400065B RID: 1627
	public Vector2 scale = Vector2.one;

	// Token: 0x0400065C RID: 1628
	public float scrollWheelFactor;

	// Token: 0x0400065D RID: 1629
	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	// Token: 0x0400065E RID: 1630
	public float momentumAmount = 35f;

	// Token: 0x0400065F RID: 1631
	private Camera mCam;

	// Token: 0x04000660 RID: 1632
	private Transform mTrans;

	// Token: 0x04000661 RID: 1633
	private bool mPressed;

	// Token: 0x04000662 RID: 1634
	private Vector2 mMomentum = Vector2.zero;

	// Token: 0x04000663 RID: 1635
	private Bounds mBounds;

	// Token: 0x04000664 RID: 1636
	private float mScroll;
}
