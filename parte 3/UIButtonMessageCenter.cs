using System;
using UnityEngine;

// Token: 0x020000F1 RID: 241
[AddComponentMenu("GUI/Interaction/Button Message Center")]
public class UIButtonMessageCenter : UIBasicButton
{
	// Token: 0x060006CF RID: 1743 RVA: 0x00021A1C File Offset: 0x0001FC1C
	protected override void Send()
	{
		if (!base.enabled || !base.gameObject.active || string.IsNullOrEmpty(this.functionName))
		{
			return;
		}
		if (this.target == null)
		{
			if (MessageCenter.IsInstanced)
			{
				this.target = MessageCenter.Instance.gameObject;
			}
			else
			{
				Debug.LogError("MessageCenter called but not instanced.");
			}
		}
		Transform[] componentsInChildren = this.target.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x040005CA RID: 1482
	private GameObject target;

	// Token: 0x040005CB RID: 1483
	public string functionName;
}
