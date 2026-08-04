using System;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public class Rotate : MonoBehaviour
{
	// Token: 0x06000554 RID: 1364 RVA: 0x00019B92 File Offset: 0x00017D92
	private void Start()
	{
		this.m_trans = base.gameObject.transform;
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x00019BA8 File Offset: 0x00017DA8
	private void Update()
	{
		if (this.Rotate_X != 0f || this.Rotate_Y != 0f || this.Rotate_Z != 0f)
		{
			this.m_trans.Rotate(new Vector3(this.Rotate_X * Time.deltaTime, this.Rotate_Y * Time.deltaTime, this.Rotate_Z * Time.deltaTime));
		}
	}

	// Token: 0x0400047A RID: 1146
	private Transform m_trans;

	// Token: 0x0400047B RID: 1147
	public float Rotate_X;

	// Token: 0x0400047C RID: 1148
	public float Rotate_Y;

	// Token: 0x0400047D RID: 1149
	public float Rotate_Z;
}
