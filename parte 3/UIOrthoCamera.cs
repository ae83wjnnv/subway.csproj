using System;
using UnityEngine;

// Token: 0x02000121 RID: 289
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Orthographic Camera")]
[ExecuteInEditMode]
public class UIOrthoCamera : MonoBehaviour
{
	// Token: 0x06000856 RID: 2134 RVA: 0x0002BA66 File Offset: 0x00029C66
	private void Start()
	{
		this.mCam = base.GetComponent<Camera>();
		this.mTrans = base.transform;
		this.mCam.orthographic = true;
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0002BA8C File Offset: 0x00029C8C
	private void Update()
	{
		float num = this.mCam.rect.yMin * (float)Screen.height;
		float num2 = (this.mCam.rect.yMax * (float)Screen.height - num) * 0.5f * this.mTrans.lossyScale.y;
		if (!Mathf.Approximately(this.mCam.orthographicSize, num2))
		{
			this.mCam.orthographicSize = num2;
		}
	}

	// Token: 0x04000753 RID: 1875
	private Camera mCam;

	// Token: 0x04000754 RID: 1876
	private Transform mTrans;
}
