using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020000F3 RID: 243
[AddComponentMenu("NGUI/Interaction/Button Play Animation")]
public class UIButtonPlayAnimation : MonoBehaviour
{
	// Token: 0x060006D8 RID: 1752 RVA: 0x00021C51 File Offset: 0x0001FE51
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x00021C5A File Offset: 0x0001FE5A
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x00021C7D File Offset: 0x0001FE7D
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (this.trigger == Trigger.OnHover || (this.trigger == Trigger.OnHoverTrue && isOver) || (this.trigger == Trigger.OnHoverFalse && !isOver))
			{
				this.Play(isOver);
			}
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00021CB7 File Offset: 0x0001FEB7
	private void OnPress(bool isPressed)
	{
		if (base.enabled && (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed)))
		{
			this.Play(isPressed);
		}
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00021CEA File Offset: 0x0001FEEA
	private void OnClick()
	{
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x00021D04 File Offset: 0x0001FF04
	private void Play(bool forward)
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<Animation>();
		}
		if (this.target != null)
		{
			int num = (int)(Direction.Toggle - this.playDirection);
			Direction direction = (Direction)((!forward) ? num : ((int)this.playDirection));
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.target, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished);
			if (this.resetOnPlay)
			{
				activeAnimation.Reset();
			}
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
			{
				activeAnimation.eventReceiver = this.eventReceiver;
				activeAnimation.callWhenFinished = this.callWhenFinished;
			}
		}
	}

	// Token: 0x040005D4 RID: 1492
	public Animation target;

	// Token: 0x040005D5 RID: 1493
	public string clipName;

	// Token: 0x040005D6 RID: 1494
	public Trigger trigger;

	// Token: 0x040005D7 RID: 1495
	public Direction playDirection = Direction.Forward;

	// Token: 0x040005D8 RID: 1496
	public bool resetOnPlay;

	// Token: 0x040005D9 RID: 1497
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x040005DA RID: 1498
	public DisableCondition disableWhenFinished;

	// Token: 0x040005DB RID: 1499
	public GameObject eventReceiver;

	// Token: 0x040005DC RID: 1500
	public string callWhenFinished;

	// Token: 0x040005DD RID: 1501
	private bool mStarted;

	// Token: 0x040005DE RID: 1502
	private bool mHighlighted;
}
