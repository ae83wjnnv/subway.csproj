using System;
using UnityEngine;

// Token: 0x0200010E RID: 270
[AddComponentMenu("NGUI/Interaction/Forward Events")]
public class UIForwardEvents : MonoBehaviour
{
	// Token: 0x060007C0 RID: 1984 RVA: 0x0002858D File Offset: 0x0002678D
	private void OnHover(bool isOver)
	{
		if (this.onHover && this.target != null)
		{
			this.target.SendMessage("OnHover", isOver, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x000285BC File Offset: 0x000267BC
	private void OnPress(bool pressed)
	{
		if (this.onPress && this.target != null)
		{
			this.target.SendMessage("OnPress", pressed, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x000285EB File Offset: 0x000267EB
	private void OnClick()
	{
		if (this.onClick && this.target != null)
		{
			this.target.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x00028614 File Offset: 0x00026814
	private void OnDoubleClick()
	{
		if (this.onDoubleClick && this.target != null)
		{
			this.target.SendMessage("OnDoubleClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0002863D File Offset: 0x0002683D
	private void OnSelect(bool selected)
	{
		if (this.onSelect && this.target != null)
		{
			this.target.SendMessage("OnSelect", selected, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x0002866C File Offset: 0x0002686C
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag && this.target != null)
		{
			this.target.SendMessage("OnDrag", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x0002869B File Offset: 0x0002689B
	private void OnDrop(GameObject go)
	{
		if (this.onDrop && this.target != null)
		{
			this.target.SendMessage("OnDrop", go, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x000286C5 File Offset: 0x000268C5
	private void OnInput(string text)
	{
		if (this.onInput && this.target != null)
		{
			this.target.SendMessage("OnInput", text, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x000286EF File Offset: 0x000268EF
	private void OnSubmit()
	{
		if (this.onSubmit && this.target != null)
		{
			this.target.SendMessage("OnSubmit", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x040006B0 RID: 1712
	public GameObject target;

	// Token: 0x040006B1 RID: 1713
	public bool onHover;

	// Token: 0x040006B2 RID: 1714
	public bool onPress;

	// Token: 0x040006B3 RID: 1715
	public bool onClick;

	// Token: 0x040006B4 RID: 1716
	public bool onDoubleClick;

	// Token: 0x040006B5 RID: 1717
	public bool onSelect;

	// Token: 0x040006B6 RID: 1718
	public bool onDrag;

	// Token: 0x040006B7 RID: 1719
	public bool onDrop;

	// Token: 0x040006B8 RID: 1720
	public bool onInput;

	// Token: 0x040006B9 RID: 1721
	public bool onSubmit;
}
