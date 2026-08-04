using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class Cloud : MonoBehaviour
{
	// Token: 0x0600021F RID: 543 RVA: 0x0000950B File Offset: 0x0000770B
	private void Awake()
	{
		this.cameraTransform = Camera.main.transform;
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000951D File Offset: 0x0000771D
	private void OnBecameVisible()
	{
		base.enabled = true;
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00009526 File Offset: 0x00007726
	private void Update()
	{
		base.transform.rotation = this.cameraTransform.rotation;
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000953E File Offset: 0x0000773E
	private void OnBecameInvisible()
	{
		base.enabled = false;
	}

	// Token: 0x0400015A RID: 346
	private Transform cameraTransform;
}
