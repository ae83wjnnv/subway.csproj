using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F8 RID: 248
[AddComponentMenu("NGUI/UI/Camera")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class UICamera : MonoBehaviour
{
	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0002244E File Offset: 0x0002064E
	[Obsolete("Use UICamera.currentCamera instead")]
	public static Camera lastCamera
	{
		get
		{
			return UICamera.currentCamera;
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x060006FA RID: 1786 RVA: 0x00022455 File Offset: 0x00020655
	[Obsolete("Use UICamera.currentTouchID instead")]
	public static int lastTouchID
	{
		get
		{
			return UICamera.currentTouchID;
		}
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x060006FB RID: 1787 RVA: 0x0002245C File Offset: 0x0002065C
	private bool handlesEvents
	{
		get
		{
			return UICamera.eventHandler == this;
		}
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x060006FC RID: 1788 RVA: 0x00022469 File Offset: 0x00020669
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.GetComponent<Camera>();
			}
			return this.mCam;
		}
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x060006FD RID: 1789 RVA: 0x0002248B File Offset: 0x0002068B
	public static GameObject hoveredObject
	{
		get
		{
			return UICamera.mHover;
		}
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x060006FE RID: 1790 RVA: 0x00022492 File Offset: 0x00020692
	// (set) Token: 0x060006FF RID: 1791 RVA: 0x0002249C File Offset: 0x0002069C
	public static GameObject selectedObject
	{
		get
		{
			return UICamera.mSel;
		}
		set
		{
			if (!(UICamera.mSel != value))
			{
				return;
			}
			if (UICamera.mSel != null)
			{
				UICamera uicamera = UICamera.FindCameraForLayer(UICamera.mSel.layer);
				if (uicamera != null)
				{
					UICamera.currentCamera = uicamera.mCam;
					UICamera.mSel.SendMessage("OnSelect", false, SendMessageOptions.DontRequireReceiver);
					if (uicamera.useController || uicamera.useKeyboard)
					{
						UICamera.Highlight(UICamera.mSel, false);
					}
				}
			}
			UICamera.mSel = value;
			if (!(UICamera.mSel != null))
			{
				return;
			}
			UICamera uicamera2 = UICamera.FindCameraForLayer(UICamera.mSel.layer);
			if (uicamera2 != null)
			{
				UICamera.currentCamera = uicamera2.mCam;
				if (uicamera2.useController || uicamera2.useKeyboard)
				{
					UICamera.Highlight(UICamera.mSel, true);
				}
				UICamera.mSel.SendMessage("OnSelect", true, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06000700 RID: 1792 RVA: 0x00022584 File Offset: 0x00020784
	public static Camera mainCamera
	{
		get
		{
			UICamera eventHandler = UICamera.eventHandler;
			if (eventHandler != null)
			{
				return eventHandler.cachedCamera;
			}
			return null;
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000701 RID: 1793 RVA: 0x000225A8 File Offset: 0x000207A8
	public static UICamera eventHandler
	{
		get
		{
			for (int i = 0; i < UICamera.mList.Count; i++)
			{
				UICamera uicamera = UICamera.mList[i];
				if (!(uicamera == null) && uicamera.enabled && uicamera.gameObject.active)
				{
					return uicamera;
				}
			}
			return null;
		}
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x000225F7 File Offset: 0x000207F7
	private void OnApplicationQuit()
	{
		UICamera.mHighlighted.Clear();
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x00022603 File Offset: 0x00020803
	private static int CompareFunc(UICamera a, UICamera b)
	{
		if (a.cachedCamera.depth < b.cachedCamera.depth)
		{
			return 1;
		}
		if (a.cachedCamera.depth > b.cachedCamera.depth)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0002263C File Offset: 0x0002083C
	private static bool Raycast(Vector3 inPos, ref RaycastHit hit)
	{
		for (int i = 0; i < UICamera.mList.Count; i++)
		{
			UICamera uicamera = UICamera.mList[i];
			if (uicamera.enabled && uicamera.gameObject.active)
			{
				UICamera.currentCamera = uicamera.cachedCamera;
				Vector3 vector = UICamera.currentCamera.ScreenToViewportPoint(inPos);
				if (vector.x >= 0f && vector.x <= 1f && vector.y >= 0f && vector.y <= 1f)
				{
					Ray ray = UICamera.currentCamera.ScreenPointToRay(inPos);
					int num = UICamera.currentCamera.cullingMask & uicamera.eventReceiverMask;
					float num2 = ((uicamera.rangeDistance <= 0f) ? (UICamera.currentCamera.farClipPlane - UICamera.currentCamera.nearClipPlane) : uicamera.rangeDistance);
					if (Physics.Raycast(ray, out hit, num2, num))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x00022738 File Offset: 0x00020938
	public static UICamera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		for (int i = 0; i < UICamera.mList.Count; i++)
		{
			UICamera uicamera = UICamera.mList[i];
			Camera cachedCamera = uicamera.cachedCamera;
			if (cachedCamera != null && (cachedCamera.cullingMask & num) != 0)
			{
				return uicamera;
			}
		}
		return null;
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0002278A File Offset: 0x0002098A
	private static int GetDirection(KeyCode up, KeyCode down)
	{
		if (Input.GetKeyDown(up))
		{
			return 1;
		}
		if (Input.GetKeyDown(down))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x000227A1 File Offset: 0x000209A1
	private static int GetDirection(KeyCode up0, KeyCode up1, KeyCode down0, KeyCode down1)
	{
		if (Input.GetKeyDown(up0) || Input.GetKeyDown(up1))
		{
			return 1;
		}
		if (Input.GetKeyDown(down0) || Input.GetKeyDown(down1))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x000227C8 File Offset: 0x000209C8
	private static int GetDirection(string axis)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (UICamera.mNextEvent < realtimeSinceStartup)
		{
			float axis2 = Input.GetAxis(axis);
			if (axis2 > 0.75f)
			{
				UICamera.mNextEvent = realtimeSinceStartup + 0.25f;
				return 1;
			}
			if (axis2 < -0.75f)
			{
				UICamera.mNextEvent = realtimeSinceStartup + 0.25f;
				return -1;
			}
		}
		return 0;
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x00022818 File Offset: 0x00020A18
	public static bool IsHighlighted(GameObject go)
	{
		int i = UICamera.mHighlighted.Count;
		while (i > 0)
		{
			if (UICamera.mHighlighted[--i].go == go)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x00022858 File Offset: 0x00020A58
	private static void Highlight(GameObject go, bool highlighted)
	{
		if (!(go != null))
		{
			return;
		}
		int i = UICamera.mHighlighted.Count;
		while (i > 0)
		{
			UICamera.Highlighted highlighted2 = UICamera.mHighlighted[--i];
			if (highlighted2 == null || highlighted2.go == null)
			{
				UICamera.mHighlighted.RemoveAt(i);
			}
			else if (highlighted2.go == go)
			{
				if (highlighted)
				{
					highlighted2.counter++;
					return;
				}
				UICamera.Highlighted highlighted3 = highlighted2;
				int num = highlighted3.counter - 1;
				highlighted3.counter = num;
				if (num < 1)
				{
					UICamera.mHighlighted.Remove(highlighted2);
					go.SendMessage("OnHover", false, SendMessageOptions.DontRequireReceiver);
				}
				return;
			}
		}
		if (highlighted)
		{
			UICamera.Highlighted highlighted4 = new UICamera.Highlighted();
			highlighted4.go = go;
			highlighted4.counter = 1;
			UICamera.mHighlighted.Add(highlighted4);
			go.SendMessage("OnHover", true, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0002293C File Offset: 0x00020B3C
	private UICamera.MouseOrTouch GetTouch(int id)
	{
		UICamera.MouseOrTouch mouseOrTouch;
		if (!this.mTouches.TryGetValue(id, out mouseOrTouch))
		{
			mouseOrTouch = new UICamera.MouseOrTouch();
			this.mTouches.Add(id, mouseOrTouch);
		}
		return mouseOrTouch;
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0002296D File Offset: 0x00020B6D
	private void RemoveTouch(int id)
	{
		this.mTouches.Remove(id);
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x0002297C File Offset: 0x00020B7C
	private void Awake()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			this.useMouse = false;
			this.useTouch = true;
			this.useKeyboard = false;
			this.useController = false;
		}
		else if (Application.platform == RuntimePlatform.PS3 || Application.platform == RuntimePlatform.XBOX360)
		{
			this.useMouse = false;
			this.useTouch = false;
			this.useKeyboard = false;
			this.useController = true;
		}
		else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
		{
			this.mIsEditor = true;
		}
		UICamera.mMouse[0].pos.x = Input.mousePosition.x;
		UICamera.mMouse[0].pos.y = Input.mousePosition.y;
		UICamera.lastTouchPosition = UICamera.mMouse[0].pos;
		UICamera.mList.Add(this);
		UICamera.mList.Sort(new Comparison<UICamera>(UICamera.CompareFunc));
		if (this.eventReceiverMask == -1)
		{
			this.eventReceiverMask = base.GetComponent<Camera>().cullingMask;
		}
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x00022A8A File Offset: 0x00020C8A
	private void OnDestroy()
	{
		UICamera.mList.Remove(this);
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x00022A98 File Offset: 0x00020C98
	private void FixedUpdate()
	{
		if (this.useMouse && Application.isPlaying && this.handlesEvents)
		{
			GameObject gameObject = ((!UICamera.Raycast(Input.mousePosition, ref UICamera.lastHit)) ? UICamera.fallThrough : UICamera.lastHit.collider.gameObject);
			for (int i = 0; i < 3; i++)
			{
				UICamera.mMouse[i].current = gameObject;
			}
		}
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x00022B00 File Offset: 0x00020D00
	private void Update()
	{
		if (!Application.isPlaying || !this.handlesEvents)
		{
			return;
		}
		if (this.useMouse || (this.useTouch && this.mIsEditor))
		{
			this.ProcessMouse();
		}
		if (this.useTouch)
		{
			this.ProcessTouches();
		}
		if (this.useKeyboard && UICamera.mSel != null && Input.GetKeyDown(KeyCode.Escape))
		{
			UICamera.selectedObject = null;
		}
		if (UICamera.mSel != null)
		{
			string text = Input.inputString;
			if (this.useKeyboard && Input.GetKeyDown(KeyCode.Delete))
			{
				text += "\b";
			}
			if (text.Length > 0)
			{
				if (this.mTooltip != null)
				{
					this.ShowTooltip(false);
				}
				UICamera.mSel.SendMessage("OnInput", text, SendMessageOptions.DontRequireReceiver);
			}
			this.ProcessOthers();
		}
		if (this.useMouse && UICamera.mHover != null)
		{
			float axis = Input.GetAxis(this.scrollAxisName);
			if (axis != 0f)
			{
				UICamera.mHover.SendMessage("OnScroll", axis, SendMessageOptions.DontRequireReceiver);
			}
			if (this.mTooltipTime != 0f && this.mTooltipTime < Time.realtimeSinceStartup)
			{
				this.mTooltip = UICamera.mHover;
				this.ShowTooltip(true);
			}
		}
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x00022C40 File Offset: 0x00020E40
	private void ProcessMouse()
	{
		bool flag = Time.timeScale < 0.9f;
		if (!flag)
		{
			for (int i = 0; i < 3; i++)
			{
				if (Input.GetMouseButton(i) || Input.GetMouseButtonUp(i))
				{
					flag = true;
					break;
				}
			}
		}
		UICamera.mMouse[0].pos = Input.mousePosition;
		UICamera.mMouse[0].delta = UICamera.mMouse[0].pos - UICamera.lastTouchPosition;
		bool flag2 = UICamera.mMouse[0].pos != UICamera.lastTouchPosition;
		UICamera.lastTouchPosition = UICamera.mMouse[0].pos;
		if (flag)
		{
			UICamera.mMouse[0].current = ((!UICamera.Raycast(Input.mousePosition, ref UICamera.lastHit)) ? UICamera.fallThrough : UICamera.lastHit.collider.gameObject);
		}
		for (int j = 1; j < 3; j++)
		{
			UICamera.mMouse[j].pos = UICamera.mMouse[0].pos;
			UICamera.mMouse[j].delta = UICamera.mMouse[0].delta;
			UICamera.mMouse[j].current = UICamera.mMouse[0].current;
		}
		bool flag3 = false;
		for (int k = 0; k < 3; k++)
		{
			if (Input.GetMouseButton(k))
			{
				flag3 = true;
				break;
			}
		}
		if (flag3)
		{
			this.mTooltipTime = 0f;
		}
		else if (flag2)
		{
			if (this.mTooltipTime != 0f)
			{
				this.mTooltipTime = Time.realtimeSinceStartup + this.tooltipDelay;
			}
			else if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
		}
		if (!flag3 && UICamera.mHover != null && UICamera.mHover != UICamera.mMouse[0].current)
		{
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			UICamera.Highlight(UICamera.mHover, false);
			UICamera.mHover = null;
		}
		for (int l = 0; l < 3; l++)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(l);
			bool mouseButtonUp = Input.GetMouseButtonUp(l);
			UICamera.currentTouch = UICamera.mMouse[l];
			UICamera.currentTouchID = -1 - l;
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			this.ProcessTouch(mouseButtonDown, mouseButtonUp);
		}
		UICamera.currentTouch = null;
		if (!flag3 && UICamera.mHover != UICamera.mMouse[0].current)
		{
			this.mTooltipTime = Time.realtimeSinceStartup + this.tooltipDelay;
			UICamera.mHover = UICamera.mMouse[0].current;
			UICamera.Highlight(UICamera.mHover, true);
		}
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00022EEC File Offset: 0x000210EC
	private void ProcessTouches()
	{
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			UICamera.currentTouchID = touch.fingerId;
			UICamera.currentTouch = this.GetTouch(UICamera.currentTouchID);
			bool flag = touch.phase == TouchPhase.Began;
			bool flag2 = touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended;
			if (flag)
			{
				UICamera.currentTouch.delta = Vector2.zero;
			}
			else
			{
				UICamera.currentTouch.delta = touch.position - UICamera.currentTouch.pos;
			}
			UICamera.currentTouch.pos = touch.position;
			UICamera.currentTouch.current = ((!UICamera.Raycast(UICamera.currentTouch.pos, ref UICamera.lastHit)) ? UICamera.fallThrough : UICamera.lastHit.collider.gameObject);
			UICamera.lastTouchPosition = UICamera.currentTouch.pos;
			if (flag)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			this.ProcessTouch(flag, flag2);
			if (flag2)
			{
				this.RemoveTouch(UICamera.currentTouchID);
			}
			UICamera.currentTouch = null;
		}
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00023038 File Offset: 0x00021238
	private void ProcessOthers()
	{
		UICamera.currentTouchID = -100;
		UICamera.currentTouch = UICamera.mController;
		bool flag = UICamera.mSel != null && UICamera.mSel.GetComponent<UIInput>() != null;
		bool flag2 = this.useKeyboard && (Input.GetKeyDown(KeyCode.Return) || (!flag && Input.GetKeyDown(KeyCode.Space)));
		bool flag3 = this.useController && Input.GetKeyDown(KeyCode.JoystickButton0);
		bool flag4 = this.useKeyboard && (Input.GetKeyUp(KeyCode.Return) || (!flag && Input.GetKeyUp(KeyCode.Space)));
		bool flag5 = this.useController && Input.GetKeyUp(KeyCode.JoystickButton0);
		bool flag6 = flag2 || flag3;
		bool flag7 = flag4 || flag5;
		if (flag6 || flag7)
		{
			UICamera.currentTouch.current = UICamera.mSel;
			this.ProcessTouch(flag6, flag7);
		}
		int num = 0;
		int num2 = 0;
		if (this.useKeyboard)
		{
			if (flag)
			{
				num += UICamera.GetDirection(KeyCode.UpArrow, KeyCode.DownArrow);
				num2 += UICamera.GetDirection(KeyCode.RightArrow, KeyCode.LeftArrow);
			}
			else
			{
				num += UICamera.GetDirection(KeyCode.W, KeyCode.UpArrow, KeyCode.S, KeyCode.DownArrow);
				num2 += UICamera.GetDirection(KeyCode.D, KeyCode.RightArrow, KeyCode.A, KeyCode.LeftArrow);
			}
		}
		if (this.useController)
		{
			if (!string.IsNullOrEmpty(this.verticalAxisName))
			{
				num += UICamera.GetDirection(this.verticalAxisName);
			}
			if (!string.IsNullOrEmpty(this.horizontalAxisName))
			{
				num2 += UICamera.GetDirection(this.horizontalAxisName);
			}
		}
		if (num != 0)
		{
			UICamera.mSel.SendMessage("OnKey", (num <= 0) ? KeyCode.DownArrow : KeyCode.UpArrow, SendMessageOptions.DontRequireReceiver);
		}
		if (num2 != 0)
		{
			UICamera.mSel.SendMessage("OnKey", (num2 <= 0) ? KeyCode.LeftArrow : KeyCode.RightArrow, SendMessageOptions.DontRequireReceiver);
		}
		if (this.useKeyboard && Input.GetKeyDown(KeyCode.Tab))
		{
			UICamera.mSel.SendMessage("OnKey", KeyCode.Tab, SendMessageOptions.DontRequireReceiver);
		}
		if (this.useController && Input.GetKeyUp(KeyCode.JoystickButton1))
		{
			UICamera.mSel.SendMessage("OnKey", KeyCode.Escape, SendMessageOptions.DontRequireReceiver);
		}
		UICamera.currentTouch = null;
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x00023270 File Offset: 0x00021470
	private void ProcessTouch(bool pressed, bool unpressed)
	{
		if (pressed)
		{
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			UICamera.currentTouch.pressed = UICamera.currentTouch.current;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.Always;
			UICamera.currentTouch.totalDelta = Vector2.zero;
			if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentTouch.pressed.SendMessage("OnPress", true, SendMessageOptions.DontRequireReceiver);
			}
			if (UICamera.currentTouch.pressed != UICamera.mSel)
			{
				if (this.mTooltip != null)
				{
					this.ShowTooltip(false);
				}
				UICamera.selectedObject = null;
			}
		}
		else if (UICamera.currentTouch.pressed != null && UICamera.currentTouch.delta.magnitude != 0f)
		{
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			UICamera.currentTouch.totalDelta += UICamera.currentTouch.delta;
			bool flag = UICamera.currentTouch.clickNotification == UICamera.ClickNotification.None;
			UICamera.currentTouch.pressed.SendMessage("OnDrag", UICamera.currentTouch.delta, SendMessageOptions.DontRequireReceiver);
			if (flag)
			{
				UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
			}
			else if (UICamera.currentTouch.clickNotification == UICamera.ClickNotification.BasedOnDelta)
			{
				float num = ((UICamera.currentTouch != UICamera.mMouse[0]) ? Mathf.Max(this.touchClickThreshold, (float)Screen.height * 0.1f) : this.mouseClickThreshold);
				if (UICamera.currentTouch.totalDelta.magnitude > num)
				{
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				}
			}
		}
		if (!unpressed)
		{
			return;
		}
		if (this.mTooltip != null)
		{
			this.ShowTooltip(false);
		}
		if (UICamera.currentTouch.pressed != null)
		{
			UICamera.currentTouch.pressed.SendMessage("OnPress", false, SendMessageOptions.DontRequireReceiver);
			if (UICamera.currentTouch.pressed == UICamera.mHover)
			{
				UICamera.currentTouch.pressed.SendMessage("OnHover", true, SendMessageOptions.DontRequireReceiver);
			}
			if (UICamera.currentTouch.pressed == UICamera.currentTouch.current)
			{
				if (UICamera.currentTouch.pressed != UICamera.mSel)
				{
					UICamera.mSel = UICamera.currentTouch.pressed;
					UICamera.currentTouch.pressed.SendMessage("OnSelect", true, SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					UICamera.mSel = UICamera.currentTouch.pressed;
				}
				if (UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None)
				{
					float realtimeSinceStartup = Time.realtimeSinceStartup;
					UICamera.currentTouch.pressed.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
					if (UICamera.currentTouch.clickTime + 0.25f > realtimeSinceStartup)
					{
						UICamera.currentTouch.pressed.SendMessage("OnDoubleClick", SendMessageOptions.DontRequireReceiver);
					}
					UICamera.currentTouch.clickTime = realtimeSinceStartup;
				}
			}
			else if (UICamera.currentTouch.current != null)
			{
				UICamera.currentTouch.current.SendMessage("OnDrop", UICamera.currentTouch.pressed, SendMessageOptions.DontRequireReceiver);
			}
		}
		UICamera.currentTouch.pressed = null;
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x000235AA File Offset: 0x000217AA
	private void ShowTooltip(bool val)
	{
		this.mTooltipTime = 0f;
		if (this.mTooltip != null)
		{
			this.mTooltip.SendMessage("OnTooltip", val, SendMessageOptions.DontRequireReceiver);
		}
		if (!val)
		{
			this.mTooltip = null;
		}
	}

	// Token: 0x04000600 RID: 1536
	public bool useMouse = true;

	// Token: 0x04000601 RID: 1537
	public bool useTouch = true;

	// Token: 0x04000602 RID: 1538
	public bool useKeyboard = true;

	// Token: 0x04000603 RID: 1539
	public bool useController = true;

	// Token: 0x04000604 RID: 1540
	public LayerMask eventReceiverMask = -1;

	// Token: 0x04000605 RID: 1541
	public float tooltipDelay = 1f;

	// Token: 0x04000606 RID: 1542
	public float mouseClickThreshold = 10f;

	// Token: 0x04000607 RID: 1543
	public float touchClickThreshold = 40f;

	// Token: 0x04000608 RID: 1544
	public float rangeDistance = -1f;

	// Token: 0x04000609 RID: 1545
	public string scrollAxisName = "Mouse ScrollWheel";

	// Token: 0x0400060A RID: 1546
	public string verticalAxisName = "Vertical";

	// Token: 0x0400060B RID: 1547
	public string horizontalAxisName = "Horizontal";

	// Token: 0x0400060C RID: 1548
	public static Vector2 lastTouchPosition = Vector2.zero;

	// Token: 0x0400060D RID: 1549
	public static RaycastHit lastHit;

	// Token: 0x0400060E RID: 1550
	public static Camera currentCamera = null;

	// Token: 0x0400060F RID: 1551
	public static int currentTouchID = -1;

	// Token: 0x04000610 RID: 1552
	public static UICamera.MouseOrTouch currentTouch = null;

	// Token: 0x04000611 RID: 1553
	public static GameObject fallThrough;

	// Token: 0x04000612 RID: 1554
	private static List<UICamera> mList = new List<UICamera>();

	// Token: 0x04000613 RID: 1555
	private static List<UICamera.Highlighted> mHighlighted = new List<UICamera.Highlighted>();

	// Token: 0x04000614 RID: 1556
	private static GameObject mSel = null;

	// Token: 0x04000615 RID: 1557
	private static UICamera.MouseOrTouch[] mMouse = new UICamera.MouseOrTouch[]
	{
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch()
	};

	// Token: 0x04000616 RID: 1558
	private static GameObject mHover;

	// Token: 0x04000617 RID: 1559
	private static UICamera.MouseOrTouch mController = new UICamera.MouseOrTouch();

	// Token: 0x04000618 RID: 1560
	private static float mNextEvent = 0f;

	// Token: 0x04000619 RID: 1561
	private Dictionary<int, UICamera.MouseOrTouch> mTouches = new Dictionary<int, UICamera.MouseOrTouch>();

	// Token: 0x0400061A RID: 1562
	private GameObject mTooltip;

	// Token: 0x0400061B RID: 1563
	private Camera mCam;

	// Token: 0x0400061C RID: 1564
	private LayerMask mLayerMask;

	// Token: 0x0400061D RID: 1565
	private float mTooltipTime;

	// Token: 0x0400061E RID: 1566
	private bool mIsEditor;

	// Token: 0x020001FC RID: 508
	public enum ClickNotification
	{
		// Token: 0x04000BC5 RID: 3013
		None,
		// Token: 0x04000BC6 RID: 3014
		Always,
		// Token: 0x04000BC7 RID: 3015
		BasedOnDelta
	}

	// Token: 0x020001FD RID: 509
	public class MouseOrTouch
	{
		// Token: 0x04000BC8 RID: 3016
		public Vector2 pos;

		// Token: 0x04000BC9 RID: 3017
		public Vector2 delta;

		// Token: 0x04000BCA RID: 3018
		public Vector2 totalDelta;

		// Token: 0x04000BCB RID: 3019
		public Camera pressedCam;

		// Token: 0x04000BCC RID: 3020
		public GameObject current;

		// Token: 0x04000BCD RID: 3021
		public GameObject pressed;

		// Token: 0x04000BCE RID: 3022
		public float clickTime;

		// Token: 0x04000BCF RID: 3023
		public UICamera.ClickNotification clickNotification = UICamera.ClickNotification.Always;
	}

	// Token: 0x020001FE RID: 510
	private class Highlighted
	{
		// Token: 0x04000BD0 RID: 3024
		public GameObject go;

		// Token: 0x04000BD1 RID: 3025
		public int counter;
	}
}
