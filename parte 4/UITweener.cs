using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000140 RID: 320
public abstract class UITweener : IgnoreTimeScale
{
	// Token: 0x1700010B RID: 267
	// (get) Token: 0x06000960 RID: 2400 RVA: 0x000334B0 File Offset: 0x000316B0
	public float amountPerDelta
	{
		get
		{
			if (this.mDuration != this.duration)
			{
				this.mDuration = this.duration;
				this.mAmountPerDelta = Mathf.Abs((this.duration <= 0f) ? 1000f : (1f / this.duration));
			}
			return this.mAmountPerDelta;
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06000961 RID: 2401 RVA: 0x00033508 File Offset: 0x00031708
	public float factor
	{
		get
		{
			return this.mFactor;
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06000962 RID: 2402 RVA: 0x00033510 File Offset: 0x00031710
	public Direction direction
	{
		get
		{
			if (this.mAmountPerDelta < 0f)
			{
				return Direction.Reverse;
			}
			return Direction.Forward;
		}
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x00033522 File Offset: 0x00031722
	private void Start()
	{
		this.Update();
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0003352C File Offset: 0x0003172C
	private void Update()
	{
		float num = base.UpdateRealTimeDelta();
		this.mFactor += this.amountPerDelta * num;
		if (this.style == UITweener.Style.Loop)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor -= Mathf.Floor(this.mFactor);
			}
		}
		else if (this.style == UITweener.Style.PingPong)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor = 1f - (this.mFactor - Mathf.Floor(this.mFactor));
				this.mAmountPerDelta = 0f - this.mAmountPerDelta;
			}
			else if (this.mFactor < 0f)
			{
				this.mFactor = 0f - this.mFactor;
				this.mFactor -= Mathf.Floor(this.mFactor);
				this.mAmountPerDelta = 0f - this.mAmountPerDelta;
			}
		}
		float num2 = Mathf.Clamp01(this.mFactor);
		if (this.method == UITweener.Method.EaseIn)
		{
			num2 = 1f - Mathf.Sin(1.5707964f * (1f - num2));
			if (this.steeperCurves)
			{
				num2 *= num2;
			}
		}
		else if (this.method == UITweener.Method.EaseOut)
		{
			num2 = Mathf.Sin(1.5707964f * num2);
			if (this.steeperCurves)
			{
				num2 = 1f - num2;
				num2 = 1f - num2 * num2;
			}
		}
		else if (this.method == UITweener.Method.EaseInOut)
		{
			num2 -= Mathf.Sin(num2 * 6.2831855f) / 6.2831855f;
			if (this.steeperCurves)
			{
				num2 = num2 * 2f - 1f;
				float num3 = Mathf.Sign(num2);
				num2 = 1f - Mathf.Abs(num2);
				num2 = 1f - num2 * num2;
				num2 = num3 * num2 * 0.5f + 0.5f;
			}
		}
		this.OnUpdate(num2);
		if (this.style != UITweener.Style.Once || (this.mFactor <= 1f && this.mFactor >= 0f))
		{
			return;
		}
		this.mFactor = Mathf.Clamp01(this.mFactor);
		if (string.IsNullOrEmpty(this.callWhenFinished))
		{
			base.enabled = false;
			return;
		}
		if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
		{
			this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
		}
		if ((this.mFactor == 1f && this.mAmountPerDelta > 0f) || (this.mFactor == 0f && this.mAmountPerDelta < 0f))
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x000337AC File Offset: 0x000319AC
	public void Play(bool forward)
	{
		this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		if (!forward)
		{
			this.mAmountPerDelta = 0f - this.mAmountPerDelta;
		}
		base.enabled = true;
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x000337DB File Offset: 0x000319DB
	[Obsolete("Use Tweener.Play instead")]
	public void Animate(bool forward)
	{
		this.Play(forward);
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x000337E4 File Offset: 0x000319E4
	public void Reset()
	{
		this.mFactor = ((this.mAmountPerDelta >= 0f) ? 0f : 1f);
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00033805 File Offset: 0x00031A05
	public void Toggle()
	{
		if (this.mFactor > 0f)
		{
			this.mAmountPerDelta = 0f - this.amountPerDelta;
		}
		else
		{
			this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		}
		base.enabled = true;
	}

	// Token: 0x06000969 RID: 2409
	protected abstract void OnUpdate(float factor);

	// Token: 0x0600096A RID: 2410 RVA: 0x00033840 File Offset: 0x00031A40
	public static T Begin<T>(GameObject go, float duration) where T : UITweener
	{
		T t = go.GetComponent<T>();
		if (t == null)
		{
			t = go.AddComponent<T>();
		}
		t.duration = duration;
		t.mFactor = 0f;
		t.style = UITweener.Style.Once;
		t.enabled = true;
		return t;
	}

	// Token: 0x04000831 RID: 2097
	public UITweener.Method method;

	// Token: 0x04000832 RID: 2098
	public UITweener.Style style;

	// Token: 0x04000833 RID: 2099
	public float duration = 1f;

	// Token: 0x04000834 RID: 2100
	public bool steeperCurves;

	// Token: 0x04000835 RID: 2101
	public int tweenGroup;

	// Token: 0x04000836 RID: 2102
	public GameObject eventReceiver;

	// Token: 0x04000837 RID: 2103
	public string callWhenFinished;

	// Token: 0x04000838 RID: 2104
	private float mDuration;

	// Token: 0x04000839 RID: 2105
	private float mAmountPerDelta = 1f;

	// Token: 0x0400083A RID: 2106
	private float mFactor;

	// Token: 0x0200021E RID: 542
	public enum Method
	{
		// Token: 0x04000C3C RID: 3132
		Linear,
		// Token: 0x04000C3D RID: 3133
		EaseIn,
		// Token: 0x04000C3E RID: 3134
		EaseOut,
		// Token: 0x04000C3F RID: 3135
		EaseInOut
	}

	// Token: 0x0200021F RID: 543
	public enum Style
	{
		// Token: 0x04000C41 RID: 3137
		Once,
		// Token: 0x04000C42 RID: 3138
		Loop,
		// Token: 0x04000C43 RID: 3139
		PingPong
	}
}
