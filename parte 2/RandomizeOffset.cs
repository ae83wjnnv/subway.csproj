using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class RandomizeOffset : MonoBehaviour
{
	// Token: 0x0600053C RID: 1340 RVA: 0x000193CC File Offset: 0x000175CC
	public void ChooseRandomOffset()
	{
		List<float> list = new List<float>();
		if (this.randomOffsets.left)
		{
			list.Add(-20f);
		}
		if (this.randomOffsets.mid)
		{
			list.Add(0f);
		}
		if (this.randomOffsets.right)
		{
			list.Add(20f);
		}
		float[] array = list.ToArray();
		if (array.Length != 0)
		{
			Vector3 localPosition = base.transform.localPosition;
			localPosition.x = array[Random.Range(0, array.Length)];
			base.transform.localPosition = localPosition;
		}
	}

	// Token: 0x04000467 RID: 1127
	public RandomizeOffset.RandomOffsets randomOffsets;

	// Token: 0x020001CC RID: 460
	[Serializable]
	public class RandomOffsets
	{
		// Token: 0x04000AE0 RID: 2784
		public bool left = true;

		// Token: 0x04000AE1 RID: 2785
		public bool mid = true;

		// Token: 0x04000AE2 RID: 2786
		public bool right = true;
	}
}
