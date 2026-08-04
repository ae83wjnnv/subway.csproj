using System;
using UnityEngine;

// Token: 0x02000073 RID: 115
[AddComponentMenu("NGUI/Internal/Ignore TimeScale Behaviour")]
public class IgnoreTimeScale : MonoBehaviour
{
	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060003CC RID: 972 RVA: 0x000110AF File Offset: 0x0000F2AF
	public float realTimeDelta
	{
		get
		{
			return this.mDelta;
		}
	}

	// Token: 0x060003CD RID: 973 RVA: 0x000110B7 File Offset: 0x0000F2B7
	private void OnEnable()
	{
		this.mTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060003CE RID: 974 RVA: 0x000110C4 File Offset: 0x0000F2C4
	private void Start()
	{
		this.mTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060003CF RID: 975 RVA: 0x000110D4 File Offset: 0x0000F2D4
	protected float UpdateRealTimeDelta()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.mTime;
		this.mActual += Mathf.Max(0f, num);
		this.mDelta = 0.001f * Mathf.Round(this.mActual * 1000f);
		this.mActual -= this.mDelta;
		this.mTime = realtimeSinceStartup;
		return this.mDelta;
	}

	// Token: 0x04000320 RID: 800
	private float mTime;

	// Token: 0x04000321 RID: 801
	private float mActual;

	// Token: 0x04000322 RID: 802
	private float mDelta;
}
