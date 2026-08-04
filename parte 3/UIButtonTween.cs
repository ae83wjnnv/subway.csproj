using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020000F7 RID: 247
[AddComponentMenu("NGUI/Interaction/Button Tween")]
public class UIButtonTween : MonoBehaviour
{
	// Token: 0x060006F1 RID: 1777 RVA: 0x000221C6 File Offset: 0x000203C6
	private void Start()
	{
		this.mStarted = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x000221E9 File Offset: 0x000203E9
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x0002220C File Offset: 0x0002040C
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

	// Token: 0x060006F4 RID: 1780 RVA: 0x00022246 File Offset: 0x00020446
	private void OnPress(bool isPressed)
	{
		if (base.enabled && (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed)))
		{
			this.Play(isPressed);
		}
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x00022279 File Offset: 0x00020479
	private void OnClick()
	{
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x00022294 File Offset: 0x00020494
	private void Update()
	{
		if (this.disableWhenFinished == DisableCondition.DoNotDisable || this.mTweens == null)
		{
			return;
		}
		bool flag = true;
		bool flag2 = true;
		int i = 0;
		int num = this.mTweens.Length;
		while (i < num)
		{
			UITweener uitweener = this.mTweens[i];
			if (uitweener.enabled)
			{
				flag = false;
				break;
			}
			if (uitweener.direction != (Direction)this.disableWhenFinished)
			{
				flag2 = false;
			}
			i++;
		}
		if (flag)
		{
			if (flag2)
			{
				NGUITools.SetActive(this.tweenTarget, false);
			}
			this.mTweens = null;
		}
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0002230C File Offset: 0x0002050C
	public void Play(bool forward)
	{
		GameObject gameObject = ((!(this.tweenTarget == null)) ? this.tweenTarget : base.gameObject);
		if (!gameObject.active)
		{
			if (this.ifDisabledOnPlay != EnableCondition.EnableThenPlay)
			{
				return;
			}
			NGUITools.SetActive(gameObject, true);
		}
		this.mTweens = ((!this.includeChildren) ? gameObject.GetComponents<UITweener>() : gameObject.GetComponentsInChildren<UITweener>());
		if (this.mTweens.Length == 0)
		{
			if (this.disableWhenFinished != DisableCondition.DoNotDisable)
			{
				NGUITools.SetActive(this.tweenTarget, false);
			}
			return;
		}
		bool flag = false;
		if (this.playDirection == Direction.Reverse)
		{
			forward = !forward;
		}
		int i = 0;
		int num = this.mTweens.Length;
		while (i < num)
		{
			UITweener uitweener = this.mTweens[i];
			if (uitweener.tweenGroup == this.tweenGroup)
			{
				if (!flag && !gameObject.active)
				{
					flag = true;
					NGUITools.SetActive(gameObject, true);
				}
				if (this.playDirection == Direction.Toggle)
				{
					uitweener.Toggle();
				}
				else
				{
					uitweener.Play(forward);
				}
				if (this.resetOnPlay)
				{
					uitweener.Reset();
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					uitweener.eventReceiver = this.eventReceiver;
					uitweener.callWhenFinished = this.callWhenFinished;
				}
			}
			i++;
		}
	}

	// Token: 0x040005F3 RID: 1523
	public GameObject tweenTarget;

	// Token: 0x040005F4 RID: 1524
	public int tweenGroup;

	// Token: 0x040005F5 RID: 1525
	public Trigger trigger;

	// Token: 0x040005F6 RID: 1526
	public Direction playDirection = Direction.Forward;

	// Token: 0x040005F7 RID: 1527
	public bool resetOnPlay;

	// Token: 0x040005F8 RID: 1528
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x040005F9 RID: 1529
	public DisableCondition disableWhenFinished;

	// Token: 0x040005FA RID: 1530
	public bool includeChildren;

	// Token: 0x040005FB RID: 1531
	public GameObject eventReceiver;

	// Token: 0x040005FC RID: 1532
	public string callWhenFinished;

	// Token: 0x040005FD RID: 1533
	private UITweener[] mTweens;

	// Token: 0x040005FE RID: 1534
	private bool mStarted;

	// Token: 0x040005FF RID: 1535
	private bool mHighlighted;
}
