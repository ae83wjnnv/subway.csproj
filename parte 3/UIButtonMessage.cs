using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
[AddComponentMenu("NGUI/Interaction/Button Message")]
public class UIButtonMessage : MonoBehaviour
{
	// Token: 0x060006C7 RID: 1735 RVA: 0x000218C3 File Offset: 0x0001FAC3
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x000218CC File Offset: 0x0001FACC
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x000218EF File Offset: 0x0001FAEF
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if ((isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOut))
			{
				this.Send();
			}
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x0002191E File Offset: 0x0001FB1E
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonMessage.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonMessage.Trigger.OnRelease)))
		{
			this.Send();
		}
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x00021946 File Offset: 0x0001FB46
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnClick)
		{
			this.Send();
		}
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x0002195E File Offset: 0x0001FB5E
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnDoubleClick)
		{
			this.Send();
		}
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x00021978 File Offset: 0x0001FB78
	private void Send()
	{
		if (!base.gameObject.active || string.IsNullOrEmpty(this.functionName))
		{
			return;
		}
		if (this.target == null)
		{
			this.target = base.gameObject;
		}
		if (this.includeChildren)
		{
			Transform[] componentsInChildren = this.target.GetComponentsInChildren<Transform>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				componentsInChildren[i].gameObject.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
				i++;
			}
			return;
		}
		this.target.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x040005C4 RID: 1476
	public GameObject target;

	// Token: 0x040005C5 RID: 1477
	public string functionName;

	// Token: 0x040005C6 RID: 1478
	public UIButtonMessage.Trigger trigger;

	// Token: 0x040005C7 RID: 1479
	public bool includeChildren;

	// Token: 0x040005C8 RID: 1480
	private bool mStarted;

	// Token: 0x040005C9 RID: 1481
	private bool mHighlighted;

	// Token: 0x020001FA RID: 506
	public enum Trigger
	{
		// Token: 0x04000BB8 RID: 3000
		OnClick,
		// Token: 0x04000BB9 RID: 3001
		OnMouseOver,
		// Token: 0x04000BBA RID: 3002
		OnMouseOut,
		// Token: 0x04000BBB RID: 3003
		OnPress,
		// Token: 0x04000BBC RID: 3004
		OnRelease,
		// Token: 0x04000BBD RID: 3005
		OnDoubleClick
	}
}
