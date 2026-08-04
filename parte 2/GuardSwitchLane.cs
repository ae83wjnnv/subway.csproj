using System;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class GuardSwitchLane : MonoBehaviour
{
	// Token: 0x060003C0 RID: 960 RVA: 0x00010F1B File Offset: 0x0000F11B
	private void Start()
	{
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00010F20 File Offset: 0x0000F120
	private void Update()
	{
		Vector3 position = this.character.transform.position;
		base.gameObject.transform.position = new Vector3(position.x, position.y, base.gameObject.transform.position.z);
	}

	// Token: 0x0400030B RID: 779
	private SmoothDampFloat smoothGuardX;

	// Token: 0x0400030C RID: 780
	public float guardXSmoothTime = 1f;

	// Token: 0x0400030D RID: 781
	public GameObject character;

	// Token: 0x0400030E RID: 782
	private Vector3 initPos;
}
