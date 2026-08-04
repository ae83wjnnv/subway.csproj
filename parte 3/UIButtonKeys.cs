using System;
using UnityEngine;

// Token: 0x020000EF RID: 239
[AddComponentMenu("NGUI/Interaction/Button Keys")]
[RequireComponent(typeof(Collider))]
public class UIButtonKeys : MonoBehaviour
{
	// Token: 0x060006C3 RID: 1731 RVA: 0x00021676 File Offset: 0x0001F876
	private void Start()
	{
		if (this.startsSelected && (UICamera.selectedObject == null || !UICamera.selectedObject.active))
		{
			UICamera.selectedObject = base.gameObject;
		}
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x000216A4 File Offset: 0x0001F8A4
	private void OnKey(KeyCode key)
	{
		if (!base.enabled || !base.gameObject.active)
		{
			return;
		}
		if (key != KeyCode.Tab)
		{
			switch (key)
			{
			case KeyCode.UpArrow:
				if (this.selectOnUp != null)
				{
					UICamera.selectedObject = this.selectOnUp.gameObject;
					return;
				}
				break;
			case KeyCode.DownArrow:
				if (this.selectOnDown != null)
				{
					UICamera.selectedObject = this.selectOnDown.gameObject;
					return;
				}
				break;
			case KeyCode.RightArrow:
				if (this.selectOnRight != null)
				{
					UICamera.selectedObject = this.selectOnRight.gameObject;
					return;
				}
				break;
			case KeyCode.LeftArrow:
				if (this.selectOnLeft != null)
				{
					UICamera.selectedObject = this.selectOnLeft.gameObject;
					return;
				}
				break;
			default:
				return;
			}
		}
		else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			if (this.selectOnLeft != null)
			{
				UICamera.selectedObject = this.selectOnLeft.gameObject;
				return;
			}
			if (this.selectOnUp != null)
			{
				UICamera.selectedObject = this.selectOnUp.gameObject;
				return;
			}
			if (this.selectOnDown != null)
			{
				UICamera.selectedObject = this.selectOnDown.gameObject;
				return;
			}
			if (this.selectOnRight != null)
			{
				UICamera.selectedObject = this.selectOnRight.gameObject;
				return;
			}
		}
		else
		{
			if (this.selectOnRight != null)
			{
				UICamera.selectedObject = this.selectOnRight.gameObject;
				return;
			}
			if (this.selectOnDown != null)
			{
				UICamera.selectedObject = this.selectOnDown.gameObject;
				return;
			}
			if (this.selectOnUp != null)
			{
				UICamera.selectedObject = this.selectOnUp.gameObject;
				return;
			}
			if (this.selectOnRight != null)
			{
				UICamera.selectedObject = this.selectOnRight.gameObject;
			}
		}
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x00021886 File Offset: 0x0001FA86
	private void OnClick()
	{
		if (base.enabled && base.gameObject.active && this.selectOnClick != null)
		{
			UICamera.selectedObject = this.selectOnClick.gameObject;
		}
	}

	// Token: 0x040005BE RID: 1470
	public bool startsSelected;

	// Token: 0x040005BF RID: 1471
	public UIButtonKeys selectOnClick;

	// Token: 0x040005C0 RID: 1472
	public UIButtonKeys selectOnUp;

	// Token: 0x040005C1 RID: 1473
	public UIButtonKeys selectOnDown;

	// Token: 0x040005C2 RID: 1474
	public UIButtonKeys selectOnLeft;

	// Token: 0x040005C3 RID: 1475
	public UIButtonKeys selectOnRight;
}
