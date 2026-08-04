using System;
using UnityEngine;

// Token: 0x020000EC RID: 236
[AddComponentMenu("GUI/Interaction/Change Screen Button")]
public class UIButtonChangeScreen : UIBasicButton
{
	// Token: 0x060006B5 RID: 1717 RVA: 0x000212F4 File Offset: 0x0001F4F4
	private void Awake()
	{
		if (this.screenChangeType == UIButtonChangeScreen.ScreenChangeType.PushScreen)
		{
			this.functionName = "PushScreen";
			return;
		}
		if (this.screenChangeType == UIButtonChangeScreen.ScreenChangeType.SwitchScreen)
		{
			this.functionName = "SwitchScreen";
			return;
		}
		if (this.screenChangeType == UIButtonChangeScreen.ScreenChangeType.BackToPrevious)
		{
			this.functionName = "BackToPrevious";
			return;
		}
		if (this.screenChangeType == UIButtonChangeScreen.ScreenChangeType.QueuePopup)
		{
			this.functionName = "QueuePopup";
			return;
		}
		if (this.screenChangeType == UIButtonChangeScreen.ScreenChangeType.ClosePopup)
		{
			this.functionName = "ClosePopup";
		}
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x00021368 File Offset: 0x0001F568
	protected override void Send()
	{
		if (base.enabled && base.gameObject.active)
		{
			if (string.IsNullOrEmpty(this.ScreenNameToOpen) && (this.screenChangeType == UIButtonChangeScreen.ScreenChangeType.PushScreen || this.screenChangeType == UIButtonChangeScreen.ScreenChangeType.SwitchScreen || this.screenChangeType == UIButtonChangeScreen.ScreenChangeType.QueuePopup))
			{
				Debug.LogError(base.name + " tried to send an empty Change Screen message");
			}
			if (this.target == null)
			{
				this.target = MessageCenter.Instance.gameObject;
			}
			Transform[] componentsInChildren = this.target.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x040005B1 RID: 1457
	private GameObject target;

	// Token: 0x040005B2 RID: 1458
	private string functionName = string.Empty;

	// Token: 0x040005B3 RID: 1459
	public UIButtonChangeScreen.ScreenChangeType screenChangeType;

	// Token: 0x040005B4 RID: 1460
	public string ScreenNameToOpen;

	// Token: 0x020001F8 RID: 504
	public enum ScreenChangeType
	{
		// Token: 0x04000BAE RID: 2990
		PushScreen,
		// Token: 0x04000BAF RID: 2991
		SwitchScreen,
		// Token: 0x04000BB0 RID: 2992
		BackToPrevious,
		// Token: 0x04000BB1 RID: 2993
		QueuePopup,
		// Token: 0x04000BB2 RID: 2994
		ClosePopup
	}
}
