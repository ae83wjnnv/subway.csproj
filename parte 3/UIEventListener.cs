using System;
using UnityEngine;

// Token: 0x02000109 RID: 265
[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
	// Token: 0x06000788 RID: 1928 RVA: 0x000263DB File Offset: 0x000245DB
	private void OnSubmit()
	{
		if (this.onSubmit != null)
		{
			this.onSubmit(base.gameObject);
		}
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x000263F6 File Offset: 0x000245F6
	private void OnClick()
	{
		if (this.onClick != null)
		{
			this.onClick(base.gameObject);
		}
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x00026411 File Offset: 0x00024611
	private void OnDoubleClick()
	{
		if (this.onDoubleClick != null)
		{
			this.onDoubleClick(base.gameObject);
		}
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x0002642C File Offset: 0x0002462C
	private void OnHover(bool isOver)
	{
		if (this.onHover != null)
		{
			this.onHover(base.gameObject, isOver);
		}
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x00026448 File Offset: 0x00024648
	private void OnPress(bool isPressed)
	{
		if (this.onPress != null)
		{
			this.onPress(base.gameObject, isPressed);
		}
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x00026464 File Offset: 0x00024664
	private void OnSelect(bool selected)
	{
		if (this.onSelect != null)
		{
			this.onSelect(base.gameObject, selected);
		}
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x00026480 File Offset: 0x00024680
	private void OnScroll(float delta)
	{
		if (this.onScroll != null)
		{
			this.onScroll(base.gameObject, delta);
		}
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0002649C File Offset: 0x0002469C
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag != null)
		{
			this.onDrag(base.gameObject, delta);
		}
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x000264B8 File Offset: 0x000246B8
	private void OnDrop(GameObject go)
	{
		if (this.onDrop != null)
		{
			this.onDrop(base.gameObject, go);
		}
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x000264D4 File Offset: 0x000246D4
	private void OnInput(string text)
	{
		if (this.onInput != null)
		{
			this.onInput(base.gameObject, text);
		}
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x000264F0 File Offset: 0x000246F0
	public static UIEventListener Get(GameObject go)
	{
		UIEventListener uieventListener = go.GetComponent<UIEventListener>();
		if (uieventListener == null)
		{
			uieventListener = go.AddComponent<UIEventListener>();
		}
		return uieventListener;
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x00026515 File Offset: 0x00024715
	[Obsolete("Please use UIEventListener.Get instead of UIEventListener.Add")]
	public static UIEventListener Add(GameObject go)
	{
		return UIEventListener.Get(go);
	}

	// Token: 0x0400068E RID: 1678
	public object parameter;

	// Token: 0x0400068F RID: 1679
	public UIEventListener.VoidDelegate onSubmit;

	// Token: 0x04000690 RID: 1680
	public UIEventListener.VoidDelegate onClick;

	// Token: 0x04000691 RID: 1681
	public UIEventListener.VoidDelegate onDoubleClick;

	// Token: 0x04000692 RID: 1682
	public UIEventListener.BoolDelegate onHover;

	// Token: 0x04000693 RID: 1683
	public UIEventListener.BoolDelegate onPress;

	// Token: 0x04000694 RID: 1684
	public UIEventListener.BoolDelegate onSelect;

	// Token: 0x04000695 RID: 1685
	public UIEventListener.FloatDelegate onScroll;

	// Token: 0x04000696 RID: 1686
	public UIEventListener.VectorDelegate onDrag;

	// Token: 0x04000697 RID: 1687
	public UIEventListener.ObjectDelegate onDrop;

	// Token: 0x04000698 RID: 1688
	public UIEventListener.StringDelegate onInput;

	// Token: 0x02000203 RID: 515
	// (Invoke) Token: 0x06000C51 RID: 3153
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x02000204 RID: 516
	// (Invoke) Token: 0x06000C55 RID: 3157
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x02000205 RID: 517
	// (Invoke) Token: 0x06000C59 RID: 3161
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x02000206 RID: 518
	// (Invoke) Token: 0x06000C5D RID: 3165
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x02000207 RID: 519
	// (Invoke) Token: 0x06000C61 RID: 3169
	public delegate void StringDelegate(GameObject go, string text);

	// Token: 0x02000208 RID: 520
	// (Invoke) Token: 0x06000C65 RID: 3173
	public delegate void ObjectDelegate(GameObject go, GameObject draggedObject);
}
