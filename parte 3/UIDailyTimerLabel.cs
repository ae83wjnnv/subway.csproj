using System;
using UnityEngine;

// Token: 0x02000100 RID: 256
[RequireComponent(typeof(UILabel))]
public class UIDailyTimerLabel : MonoBehaviour
{
	// Token: 0x0600073E RID: 1854 RVA: 0x00024120 File Offset: 0x00022320
	private void OnEnable()
	{
		if (this.label == null)
		{
			this.label = base.GetComponent<UILabel>();
			this.baseText = this.label.text;
		}
		if (PlayerInfo.Instance.isDailyWordComplete())
		{
			this.label.text = "Next Challenge in";
			return;
		}
		this.label.text = this.baseText;
	}

	// Token: 0x04000647 RID: 1607
	private UILabel label;

	// Token: 0x04000648 RID: 1608
	private string baseText;
}
