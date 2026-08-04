using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020000FB RID: 251
[AddComponentMenu("NGUI/Interaction/Checkbox")]
public class UICheckbox : MonoBehaviour
{
	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06000729 RID: 1833 RVA: 0x00023B9F File Offset: 0x00021D9F
	// (set) Token: 0x0600072A RID: 1834 RVA: 0x00023BA7 File Offset: 0x00021DA7
	public bool isChecked
	{
		get
		{
			return this.mChecked;
		}
		set
		{
			if (this.radioButtonRoot == null || value || this.optionCanBeNone || !this.mStarted)
			{
				this.Set(value);
			}
		}
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00023BD0 File Offset: 0x00021DD0
	private void Awake()
	{
		this.mTrans = base.transform;
		if (this.checkSprite != null)
		{
			this.checkSprite.alpha = ((!this.startsChecked) ? 0f : 1f);
		}
		if (this.option)
		{
			this.option = false;
			if (this.radioButtonRoot == null)
			{
				this.radioButtonRoot = this.mTrans.parent;
			}
		}
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x00023C44 File Offset: 0x00021E44
	private void Start()
	{
		if (this.eventReceiver == null)
		{
			this.eventReceiver = base.gameObject;
		}
		this.mChecked = !this.startsChecked;
		this.mStarted = true;
		this.Set(this.startsChecked);
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x00023C82 File Offset: 0x00021E82
	private void OnClick()
	{
		if (base.enabled)
		{
			this.isChecked = !this.isChecked;
		}
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x00023C9C File Offset: 0x00021E9C
	private void Set(bool state)
	{
		if (!this.mStarted)
		{
			this.mChecked = state;
			this.startsChecked = state;
			if (this.checkSprite != null)
			{
				this.checkSprite.alpha = ((!state) ? 0f : 1f);
				return;
			}
		}
		else
		{
			if (this.mChecked == state)
			{
				return;
			}
			if (this.radioButtonRoot != null && state)
			{
				UICheckbox[] componentsInChildren = this.radioButtonRoot.GetComponentsInChildren<UICheckbox>(true);
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					UICheckbox uicheckbox = componentsInChildren[i];
					if (uicheckbox != this && uicheckbox.radioButtonRoot == this.radioButtonRoot)
					{
						uicheckbox.Set(false);
					}
					i++;
				}
			}
			this.mChecked = state;
			if (this.checkSprite != null)
			{
				Color color = this.checkSprite.color;
				color.a = ((!this.mChecked) ? 0f : 1f);
				TweenColor.Begin(this.checkSprite.gameObject, 0.2f, color);
			}
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
			{
				UICheckbox.current = this;
				this.eventReceiver.SendMessage(this.functionName, this.mChecked, SendMessageOptions.DontRequireReceiver);
			}
			if (this.checkAnimation != null)
			{
				ActiveAnimation.Play(this.checkAnimation, state ? Direction.Forward : Direction.Reverse);
			}
		}
	}

	// Token: 0x04000630 RID: 1584
	public static UICheckbox current;

	// Token: 0x04000631 RID: 1585
	public UISprite checkSprite;

	// Token: 0x04000632 RID: 1586
	public Animation checkAnimation;

	// Token: 0x04000633 RID: 1587
	public GameObject eventReceiver;

	// Token: 0x04000634 RID: 1588
	public string functionName = "OnActivate";

	// Token: 0x04000635 RID: 1589
	public bool startsChecked = true;

	// Token: 0x04000636 RID: 1590
	public Transform radioButtonRoot;

	// Token: 0x04000637 RID: 1591
	public bool optionCanBeNone;

	// Token: 0x04000638 RID: 1592
	[SerializeField]
	[HideInInspector]
	private bool option;

	// Token: 0x04000639 RID: 1593
	private bool mChecked = true;

	// Token: 0x0400063A RID: 1594
	private bool mStarted;

	// Token: 0x0400063B RID: 1595
	private Transform mTrans;
}
