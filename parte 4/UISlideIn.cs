using System;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class UISlideIn : IgnoreTimeScale
{
	// Token: 0x060008F7 RID: 2295 RVA: 0x00030591 File Offset: 0x0002E791
	protected virtual void Start()
	{
		base.transform.localPosition = this.posOff;
		base.gameObject.SetActiveRecursively(false);
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x000305B0 File Offset: 0x0002E7B0
	public void SetupSlideIn()
	{
		base.gameObject.SetActiveRecursively(true);
		this.SlideIn();
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x000305C4 File Offset: 0x0002E7C4
	protected virtual void SlideIn()
	{
		SpringPosition.Begin(base.gameObject, this.posOn, 10f).ignoreTimeScale = true;
		this._slideOutTimer = 3f;
		this._readyForNextTimer = 1f;
		this._triggerSlideOut = true;
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x000305FF File Offset: 0x0002E7FF
	protected virtual void SlideOut()
	{
		SpringPosition.Begin(base.gameObject, this.posOff, 10f).ignoreTimeScale = true;
		this._triggerReadyForNext = true;
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00030624 File Offset: 0x0002E824
	protected virtual void ReadyForNewMessage()
	{
		base.gameObject.SetActiveRecursively(false);
		UIScreenController.Instance.ReadyForNextSlide();
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x0003063C File Offset: 0x0002E83C
	private void Update()
	{
		float num = base.UpdateRealTimeDelta();
		if (this._triggerSlideOut)
		{
			this._slideOutTimer -= num;
			if (this._slideOutTimer <= 0f)
			{
				this.SlideOut();
				this._triggerSlideOut = false;
			}
		}
		if (this._triggerReadyForNext)
		{
			this._readyForNextTimer -= num;
			if (this._readyForNextTimer <= 0f)
			{
				this._triggerReadyForNext = false;
				this.ReadyForNewMessage();
			}
		}
	}

	// Token: 0x040007D0 RID: 2000
	protected Vector3 posOff = new Vector3(0f, 65f, 0f);

	// Token: 0x040007D1 RID: 2001
	protected Vector3 posOn = new Vector3(0f, -5f, 0f);

	// Token: 0x040007D2 RID: 2002
	private bool _triggerSlideOut;

	// Token: 0x040007D3 RID: 2003
	private float _slideOutTimer = 3f;

	// Token: 0x040007D4 RID: 2004
	private bool _triggerReadyForNext;

	// Token: 0x040007D5 RID: 2005
	private float _readyForNextTimer = 1f;
}
