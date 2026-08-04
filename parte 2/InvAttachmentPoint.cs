using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
[AddComponentMenu("NGUI/Examples/Item Attachment Point")]
public class InvAttachmentPoint : MonoBehaviour
{
	// Token: 0x060003F6 RID: 1014 RVA: 0x000119E4 File Offset: 0x0000FBE4
	public GameObject Attach(GameObject prefab)
	{
		if (this.mPrefab != prefab)
		{
			this.mPrefab = prefab;
			if (this.mChild != null)
			{
				Object.Destroy(this.mChild);
			}
			if (this.mPrefab != null)
			{
				Transform transform = base.transform;
				this.mChild = Object.Instantiate<GameObject>(this.mPrefab, transform.position, transform.rotation);
				Transform transform2 = this.mChild.transform;
				transform2.parent = transform;
				transform2.localPosition = Vector3.zero;
				transform2.localRotation = Quaternion.identity;
				transform2.localScale = Vector3.one;
			}
		}
		return this.mChild;
	}

	// Token: 0x04000340 RID: 832
	public InvBaseItem.Slot slot;

	// Token: 0x04000341 RID: 833
	private GameObject mPrefab;

	// Token: 0x04000342 RID: 834
	private GameObject mChild;
}
