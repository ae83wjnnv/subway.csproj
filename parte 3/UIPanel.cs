using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

// Token: 0x02000122 RID: 290
[AddComponentMenu("NGUI/UI/Panel")]
[ExecuteInEditMode]
public class UIPanel : MonoBehaviour
{
	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06000859 RID: 2137 RVA: 0x0002BB0F File Offset: 0x00029D0F
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x0600085A RID: 2138 RVA: 0x0002BB31 File Offset: 0x00029D31
	public bool changedLastFrame
	{
		get
		{
			return this.mChangedLastFrame;
		}
	}

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x0600085B RID: 2139 RVA: 0x0002BB39 File Offset: 0x00029D39
	// (set) Token: 0x0600085C RID: 2140 RVA: 0x0002BB44 File Offset: 0x00029D44
	public UIPanel.DebugInfo debugInfo
	{
		get
		{
			return this.mDebugInfo;
		}
		set
		{
			if (this.mDebugInfo != value)
			{
				this.mDebugInfo = value;
				List<UIDrawCall> drawCalls = this.drawCalls;
				int i = 0;
				int count = drawCalls.Count;
				while (i < count)
				{
					GameObject gameObject = drawCalls[i].gameObject;
					gameObject.active = false;
					Object.DontDestroyOnLoad(gameObject);
					gameObject.active = true;
					i++;
				}
			}
		}
	}

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x0600085D RID: 2141 RVA: 0x0002BB9A File Offset: 0x00029D9A
	// (set) Token: 0x0600085E RID: 2142 RVA: 0x0002BBA2 File Offset: 0x00029DA2
	public UIDrawCall.Clipping clipping
	{
		get
		{
			return this.mClipping;
		}
		set
		{
			if (this.mClipping != value)
			{
				this.mCheckVisibility = true;
				this.mClipping = value;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x0600085F RID: 2143 RVA: 0x0002BBC1 File Offset: 0x00029DC1
	// (set) Token: 0x06000860 RID: 2144 RVA: 0x0002BBCC File Offset: 0x00029DCC
	public Vector4 clipRange
	{
		get
		{
			return this.mClipRange;
		}
		set
		{
			if (this.mClipRange != value)
			{
				this.mCullTime = ((this.mCullTime != 0f) ? (Time.realtimeSinceStartup + 0.15f) : 0.001f);
				this.mCheckVisibility = true;
				this.mClipRange = value;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x06000861 RID: 2145 RVA: 0x0002BC20 File Offset: 0x00029E20
	// (set) Token: 0x06000862 RID: 2146 RVA: 0x0002BC28 File Offset: 0x00029E28
	public Vector2 clipSoftness
	{
		get
		{
			return this.mClipSoftness;
		}
		set
		{
			if (this.mClipSoftness != value)
			{
				this.mClipSoftness = value;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x06000863 RID: 2147 RVA: 0x0002BC45 File Offset: 0x00029E45
	public List<UIWidget> widgets
	{
		get
		{
			return this.mWidgets;
		}
	}

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x06000864 RID: 2148 RVA: 0x0002BC50 File Offset: 0x00029E50
	public List<UIDrawCall> drawCalls
	{
		get
		{
			int i = this.mDrawCalls.Count;
			while (i > 0)
			{
				if (this.mDrawCalls[--i] == null)
				{
					this.mDrawCalls.RemoveAt(i);
				}
			}
			return this.mDrawCalls;
		}
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0002BC9C File Offset: 0x00029E9C
	private UINode GetNode(Transform t)
	{
		UINode uinode = null;
		if (t != null && this.mChildren.Contains(t))
		{
			uinode = (UINode)this.mChildren[t];
		}
		return uinode;
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0002BCD8 File Offset: 0x00029ED8
	private bool IsVisible(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		this.UpdateTransformMatrix();
		a = this.mWorldToLocal.MultiplyPoint3x4(a);
		b = this.mWorldToLocal.MultiplyPoint3x4(b);
		c = this.mWorldToLocal.MultiplyPoint3x4(c);
		d = this.mWorldToLocal.MultiplyPoint3x4(d);
		UIPanel.mTemp[0] = a.x;
		UIPanel.mTemp[1] = b.x;
		UIPanel.mTemp[2] = c.x;
		UIPanel.mTemp[3] = d.x;
		float num = Mathf.Min(UIPanel.mTemp);
		float num2 = Mathf.Max(UIPanel.mTemp);
		UIPanel.mTemp[0] = a.y;
		UIPanel.mTemp[1] = b.y;
		UIPanel.mTemp[2] = c.y;
		UIPanel.mTemp[3] = d.y;
		float num3 = Mathf.Min(UIPanel.mTemp);
		float num4 = Mathf.Max(UIPanel.mTemp);
		return num2 >= this.mMin.x && num4 >= this.mMin.y && num <= this.mMax.x && num3 <= this.mMax.y;
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0002BDFC File Offset: 0x00029FFC
	public bool IsVisible(UIWidget w)
	{
		if (!w.enabled || !w.gameObject.active || w.color.a < 0.001f)
		{
			return false;
		}
		if (this.mClipping == UIDrawCall.Clipping.None)
		{
			return true;
		}
		Vector2 relativeSize = w.relativeSize;
		Vector2 vector = Vector2.Scale(w.pivotOffset, relativeSize);
		Vector2 vector2 = vector;
		vector.x += relativeSize.x;
		vector.y -= relativeSize.y;
		Transform cachedTransform = w.cachedTransform;
		Vector3 vector3 = cachedTransform.TransformPoint(vector);
		Vector3 vector4 = cachedTransform.TransformPoint(new Vector2(vector.x, vector2.y));
		Vector3 vector5 = cachedTransform.TransformPoint(new Vector2(vector2.x, vector.y));
		Vector3 vector6 = cachedTransform.TransformPoint(vector2);
		return this.IsVisible(vector3, vector4, vector5, vector6);
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0002BEDD File Offset: 0x0002A0DD
	public void MarkMaterialAsChanged(Material mat, bool sort)
	{
		if (mat != null)
		{
			if (sort)
			{
				this.mDepthChanged = true;
			}
			if (!this.mChanged.Contains(mat))
			{
				this.mChanged.Add(mat);
				this.mChangedLastFrame = true;
			}
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0002BF13 File Offset: 0x0002A113
	public bool WatchesTransform(Transform t)
	{
		return t == this.cachedTransform || this.mChildren.Contains(t);
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0002BF34 File Offset: 0x0002A134
	private UINode AddTransform(Transform t)
	{
		UINode uinode = null;
		while (t != null && t != this.cachedTransform)
		{
			if (this.mChildren.Contains(t))
			{
				if (uinode == null)
				{
					uinode = (UINode)this.mChildren[t];
					break;
				}
				break;
			}
			else
			{
				UINode uinode2 = new UINode(t);
				if (uinode == null)
				{
					uinode = uinode2;
				}
				this.mChildren.Add(t, uinode2);
				t = t.parent;
			}
		}
		return uinode;
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x0002BFA8 File Offset: 0x0002A1A8
	private void RemoveTransform(Transform t)
	{
		if (!(t != null))
		{
			return;
		}
		while (this.mChildren.Contains(t))
		{
			this.mChildren.Remove(t);
			t = t.parent;
			if (t == null || t == this.mTrans || t.childCount > 1)
			{
				break;
			}
		}
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0002C004 File Offset: 0x0002A204
	public void AddWidget(UIWidget w)
	{
		if (!(w != null))
		{
			return;
		}
		UINode uinode = this.AddTransform(w.cachedTransform);
		if (uinode != null)
		{
			uinode.widget = w;
			if (!this.mWidgets.Contains(w))
			{
				this.mWidgets.Add(w);
				if (!this.mChanged.Contains(w.material))
				{
					this.mChanged.Add(w.material);
					this.mChangedLastFrame = true;
				}
				this.mDepthChanged = true;
				return;
			}
		}
		else
		{
			Debug.LogError("Unable to find an appropriate UIRoot for " + NGUITools.GetHierarchy(w.gameObject) + "\nPlease make sure that there is at least one game object above this widget!", w.gameObject);
		}
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0002C0A4 File Offset: 0x0002A2A4
	public void RemoveWidget(UIWidget w)
	{
		if (!(w != null))
		{
			return;
		}
		UINode node = this.GetNode(w.cachedTransform);
		if (node != null)
		{
			if (node.visibleFlag == 1 && !this.mChanged.Contains(w.material))
			{
				this.mChanged.Add(w.material);
				this.mChangedLastFrame = true;
			}
			this.RemoveTransform(w.cachedTransform);
		}
		this.mWidgets.Remove(w);
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x0002C118 File Offset: 0x0002A318
	private UIDrawCall GetDrawCall(Material mat, bool createIfMissing)
	{
		int i = 0;
		int count = this.drawCalls.Count;
		while (i < count)
		{
			UIDrawCall uidrawCall = this.drawCalls[i];
			if (uidrawCall.material == mat)
			{
				return uidrawCall;
			}
			i++;
		}
		UIDrawCall uidrawCall2 = null;
		if (createIfMissing)
		{
			GameObject gameObject = new GameObject("_UIDrawCall [" + mat.name + "]");
			Object.DontDestroyOnLoad(gameObject);
			gameObject.layer = base.gameObject.layer;
			uidrawCall2 = gameObject.AddComponent<UIDrawCall>();
			uidrawCall2.material = mat;
			this.mDrawCalls.Add(uidrawCall2);
		}
		return uidrawCall2;
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x0002C1AC File Offset: 0x0002A3AC
	private void Start()
	{
		this.mLayer = base.gameObject.layer;
		UICamera uicamera = UICamera.FindCameraForLayer(this.mLayer);
		this.mCam = ((!(uicamera != null)) ? NGUITools.FindCameraForLayer(this.mLayer) : uicamera.cachedCamera);
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x0002C1F8 File Offset: 0x0002A3F8
	private void OnEnable()
	{
		int i = 0;
		int count = this.mWidgets.Count;
		while (i < count)
		{
			this.AddWidget(this.mWidgets[i]);
			i++;
		}
		this.mRebuildAll = true;
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0002C238 File Offset: 0x0002A438
	private void OnDisable()
	{
		int i = this.mDrawCalls.Count;
		while (i > 0)
		{
			UIDrawCall uidrawCall = this.mDrawCalls[--i];
			if (uidrawCall != null)
			{
				NGUITools.DestroyImmediate(uidrawCall.gameObject);
			}
		}
		this.mDrawCalls.Clear();
		this.mChanged.Clear();
		this.mChildren.Clear();
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0002C2A0 File Offset: 0x0002A4A0
	private int GetChangeFlag(UINode start)
	{
		int num = start.changeFlag;
		if (num == -1)
		{
			Transform transform = start.trans.parent;
			while (this.mChildren.Contains(transform))
			{
				UINode uinode = (UINode)this.mChildren[transform];
				num = uinode.changeFlag;
				transform = transform.parent;
				if (num != -1)
				{
					IL_005B:
					int i = 0;
					int count = UIPanel.mHierarchy.Count;
					while (i < count)
					{
						UIPanel.mHierarchy[i].changeFlag = num;
						i++;
					}
					UIPanel.mHierarchy.Clear();
					return num;
				}
				UIPanel.mHierarchy.Add(uinode);
			}
			num = 0;
			goto IL_005B;
		}
		return num;
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0002C340 File Offset: 0x0002A540
	private void UpdateTransformMatrix()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (realtimeSinceStartup != 0f && this.mMatrixTime == realtimeSinceStartup)
		{
			return;
		}
		this.mMatrixTime = realtimeSinceStartup;
		this.mWorldToLocal = this.cachedTransform.worldToLocalMatrix;
		if (this.mClipping != UIDrawCall.Clipping.None)
		{
			Vector2 vector = new Vector2(this.mClipRange.z, this.mClipRange.w);
			if (vector.x == 0f)
			{
				vector.x = ((!(this.mCam == null)) ? ((float)this.mCam.pixelWidth) : ((float)Screen.width));
			}
			if (vector.y == 0f)
			{
				vector.y = ((!(this.mCam == null)) ? ((float)this.mCam.pixelHeight) : ((float)Screen.height));
			}
			vector *= 0.5f;
			this.mMin.x = this.mClipRange.x - vector.x;
			this.mMin.y = this.mClipRange.y - vector.y;
			this.mMax.x = this.mClipRange.x + vector.x;
			this.mMax.y = this.mClipRange.y + vector.y;
		}
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0002C494 File Offset: 0x0002A694
	private void UpdateTransforms()
	{
		this.mChangedLastFrame = false;
		bool flag = false;
		bool flag2 = Time.realtimeSinceStartup > this.mCullTime;
		if (!this.widgetsAreStatic || flag2 != this.mCulled)
		{
			int i = 0;
			int count = this.mChildren.Count;
			while (i < count)
			{
				UINode uinode = (UINode)this.mChildren[i];
				if (uinode.trans == null)
				{
					this.mRemoved.Add(uinode.trans);
				}
				else if (uinode.HasChanged())
				{
					uinode.changeFlag = 1;
					flag = true;
				}
				else
				{
					uinode.changeFlag = -1;
				}
				i++;
			}
			int j = 0;
			int count2 = this.mRemoved.Count;
			while (j < count2)
			{
				this.mChildren.Remove(this.mRemoved[j]);
				j++;
			}
			this.mRemoved.Clear();
		}
		if (!this.mCulled && flag2)
		{
			this.mCheckVisibility = true;
		}
		if (this.mCheckVisibility || flag || this.mRebuildAll)
		{
			int k = 0;
			int count3 = this.mChildren.Count;
			while (k < count3)
			{
				UINode uinode2 = (UINode)this.mChildren[k];
				if (uinode2.widget != null)
				{
					int num = 1;
					if (flag2 || flag)
					{
						if (uinode2.changeFlag == -1)
						{
							uinode2.changeFlag = this.GetChangeFlag(uinode2);
						}
						if (flag2)
						{
							num = ((!this.mCheckVisibility && uinode2.changeFlag != 1) ? uinode2.visibleFlag : (this.IsVisible(uinode2.widget) ? 1 : 0));
						}
					}
					if (uinode2.visibleFlag != num)
					{
						uinode2.changeFlag = 1;
					}
					if (uinode2.changeFlag == 1 && (num == 1 || uinode2.visibleFlag != 0))
					{
						uinode2.visibleFlag = num;
						Material material = uinode2.widget.material;
						if (!this.mChanged.Contains(material))
						{
							this.mChanged.Add(material);
							this.mChangedLastFrame = true;
						}
					}
				}
				k++;
			}
		}
		this.mCulled = flag2;
		this.mCheckVisibility = false;
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x0002C6B0 File Offset: 0x0002A8B0
	private void UpdateWidgets()
	{
		int i = 0;
		int count = this.mChildren.Count;
		while (i < count)
		{
			UINode uinode = (UINode)this.mChildren[i];
			UIWidget widget = uinode.widget;
			if (uinode.visibleFlag == 1 && widget != null && widget.UpdateGeometry(ref this.mWorldToLocal, uinode.changeFlag == 1, this.generateNormals) && !this.mChanged.Contains(widget.material))
			{
				this.mChanged.Add(widget.material);
				this.mChangedLastFrame = true;
			}
			uinode.changeFlag = 0;
			i++;
		}
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x0002C754 File Offset: 0x0002A954
	public void UpdateDrawcalls()
	{
		Vector4 zero = Vector4.zero;
		if (this.mClipping != UIDrawCall.Clipping.None)
		{
			zero = new Vector4(this.mClipRange.x, this.mClipRange.y, this.mClipRange.z * 0.5f, this.mClipRange.w * 0.5f);
		}
		if (zero.z == 0f)
		{
			zero.z = (float)Screen.width * 0.5f;
		}
		if (zero.w == 0f)
		{
			zero.w = (float)Screen.height * 0.5f;
		}
		RuntimePlatform platform = Application.platform;
		if (platform == RuntimePlatform.WindowsPlayer || platform == RuntimePlatform.WebGLPlayer || platform == RuntimePlatform.WindowsEditor)
		{
			zero.x -= 0.5f;
			zero.y += 0.5f;
		}
		Transform cachedTransform = this.cachedTransform;
		int i = 0;
		int count = this.mDrawCalls.Count;
		while (i < count)
		{
			UIDrawCall uidrawCall = this.mDrawCalls[i];
			uidrawCall.clipping = this.mClipping;
			uidrawCall.clipRange = zero;
			uidrawCall.clipSoftness = this.mClipSoftness;
			uidrawCall.depthPass = this.depthPass;
			Transform transform = uidrawCall.transform;
			transform.position = cachedTransform.position;
			transform.rotation = cachedTransform.rotation;
			transform.localScale = cachedTransform.lossyScale;
			i++;
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0002C8A4 File Offset: 0x0002AAA4
	private void Fill(Material mat)
	{
		int i = this.mWidgets.Count;
		while (i > 0)
		{
			if (this.mWidgets[--i] == null)
			{
				this.mWidgets.RemoveAt(i);
			}
		}
		int j = 0;
		int count = this.mWidgets.Count;
		while (j < count)
		{
			UIWidget uiwidget = this.mWidgets[j];
			if (uiwidget.visibleFlag == 1 && uiwidget.material == mat)
			{
				if (this.GetNode(uiwidget.cachedTransform) != null)
				{
					if (this.generateNormals)
					{
						uiwidget.WriteToBuffers(this.mVerts, this.mUvs, this.mCols, this.mNorms, this.mTans);
					}
					else
					{
						uiwidget.WriteToBuffers(this.mVerts, this.mUvs, this.mCols, null, null);
					}
				}
				else
				{
					Debug.LogError("No transform found for " + NGUITools.GetHierarchy(uiwidget.gameObject), this);
				}
			}
			j++;
		}
		if (this.mVerts.size > 0)
		{
			UIDrawCall drawCall = this.GetDrawCall(mat, true);
			drawCall.depthPass = this.depthPass;
			drawCall.Set(this.mVerts, (!this.generateNormals) ? null : this.mNorms, (!this.generateNormals) ? null : this.mTans, this.mUvs, this.mCols);
		}
		else
		{
			UIDrawCall drawCall2 = this.GetDrawCall(mat, false);
			if (drawCall2 != null)
			{
				this.mDrawCalls.Remove(drawCall2);
				NGUITools.DestroyImmediate(drawCall2.gameObject);
			}
		}
		this.mVerts.Clear();
		this.mNorms.Clear();
		this.mTans.Clear();
		this.mUvs.Clear();
		this.mCols.Clear();
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0002CA64 File Offset: 0x0002AC64
	private void LateUpdate()
	{
		this.UpdateTransformMatrix();
		this.UpdateTransforms();
		if (this.mLayer != base.gameObject.layer)
		{
			this.mLayer = base.gameObject.layer;
			UICamera uicamera = UICamera.FindCameraForLayer(this.mLayer);
			this.mCam = ((!(uicamera != null)) ? NGUITools.FindCameraForLayer(this.mLayer) : uicamera.cachedCamera);
			UIPanel.SetChildLayer(this.cachedTransform, this.mLayer);
			int i = 0;
			int count = this.drawCalls.Count;
			while (i < count)
			{
				this.mDrawCalls[i].gameObject.layer = this.mLayer;
				i++;
			}
		}
		this.UpdateWidgets();
		if (this.mDepthChanged)
		{
			this.mDepthChanged = false;
			this.mWidgets.Sort(new Comparison<UIWidget>(UIWidget.CompareFunc));
		}
		int j = 0;
		int count2 = this.mChanged.Count;
		while (j < count2)
		{
			this.Fill(this.mChanged[j]);
			j++;
		}
		this.UpdateDrawcalls();
		this.mChanged.Clear();
		this.mRebuildAll = false;
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x0002CB88 File Offset: 0x0002AD88
	public Vector3 CalculateConstrainOffset(Vector2 min, Vector2 max)
	{
		float num = this.clipRange.z * 0.5f;
		float num2 = this.clipRange.w * 0.5f;
		Vector2 vector = new Vector2(min.x, min.y);
		Vector2 vector2 = new Vector2(max.x, max.y);
		Vector2 vector3 = new Vector2(this.clipRange.x - num, this.clipRange.y - num2);
		Vector2 vector4 = new Vector2(this.clipRange.x + num, this.clipRange.y + num2);
		if (this.clipping == UIDrawCall.Clipping.SoftClip)
		{
			vector3.x += this.clipSoftness.x;
			vector3.y += this.clipSoftness.y;
			vector4.x -= this.clipSoftness.x;
			vector4.y -= this.clipSoftness.y;
		}
		return NGUIMath.ConstrainRect(vector, vector2, vector3, vector4);
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x0002CC90 File Offset: 0x0002AE90
	public bool ConstrainTargetToBounds(Transform target, ref Bounds targetBounds, bool immediate)
	{
		Vector3 vector = this.CalculateConstrainOffset(targetBounds.min, targetBounds.max);
		if (vector.magnitude > 0f)
		{
			if (immediate)
			{
				target.localPosition += vector;
				targetBounds.center += vector;
				SpringPosition component = target.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			else
			{
				SpringPosition springPosition = SpringPosition.Begin(target.gameObject, target.localPosition + vector, 13f);
				springPosition.ignoreTimeScale = true;
				springPosition.worldSpace = false;
			}
			return true;
		}
		return false;
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x0002CD34 File Offset: 0x0002AF34
	public bool ConstrainTargetToBounds(Transform target, bool immediate)
	{
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(this.cachedTransform, target);
		return this.ConstrainTargetToBounds(target, ref bounds, immediate);
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x0002CD58 File Offset: 0x0002AF58
	private static void SetChildLayer(Transform t, int layer)
	{
		for (int i = 0; i < t.childCount; i++)
		{
			Transform child = t.GetChild(i);
			if (child.GetComponent<UIPanel>() == null)
			{
				child.gameObject.layer = layer;
				UIPanel.SetChildLayer(child, layer);
			}
		}
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0002CDA0 File Offset: 0x0002AFA0
	public static UIPanel Find(Transform trans, bool createIfMissing)
	{
		Transform transform = trans;
		UIPanel uipanel = null;
		while (uipanel == null && trans != null)
		{
			uipanel = trans.GetComponent<UIPanel>();
			if (uipanel != null || trans.parent == null)
			{
				break;
			}
			trans = trans.parent;
		}
		if (createIfMissing && uipanel == null && trans != transform)
		{
			uipanel = trans.gameObject.AddComponent<UIPanel>();
			UIPanel.SetChildLayer(uipanel.cachedTransform, uipanel.gameObject.layer);
		}
		return uipanel;
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0002CE23 File Offset: 0x0002B023
	public static UIPanel Find(Transform trans)
	{
		return UIPanel.Find(trans, true);
	}

	// Token: 0x04000755 RID: 1877
	public bool showInPanelTool = true;

	// Token: 0x04000756 RID: 1878
	public bool generateNormals;

	// Token: 0x04000757 RID: 1879
	public bool depthPass;

	// Token: 0x04000758 RID: 1880
	public bool widgetsAreStatic;

	// Token: 0x04000759 RID: 1881
	[HideInInspector]
	[SerializeField]
	private UIPanel.DebugInfo mDebugInfo = UIPanel.DebugInfo.Gizmos;

	// Token: 0x0400075A RID: 1882
	[SerializeField]
	[HideInInspector]
	private UIDrawCall.Clipping mClipping;

	// Token: 0x0400075B RID: 1883
	[SerializeField]
	[HideInInspector]
	private Vector4 mClipRange = Vector4.zero;

	// Token: 0x0400075C RID: 1884
	[SerializeField]
	[HideInInspector]
	private Vector2 mClipSoftness = new Vector2(40f, 40f);

	// Token: 0x0400075D RID: 1885
	private OrderedDictionary mChildren = new OrderedDictionary();

	// Token: 0x0400075E RID: 1886
	private List<UIWidget> mWidgets = new List<UIWidget>();

	// Token: 0x0400075F RID: 1887
	private List<Material> mChanged = new List<Material>();

	// Token: 0x04000760 RID: 1888
	private List<UIDrawCall> mDrawCalls = new List<UIDrawCall>();

	// Token: 0x04000761 RID: 1889
	private BetterList<Vector3> mVerts = new BetterList<Vector3>();

	// Token: 0x04000762 RID: 1890
	private BetterList<Vector3> mNorms = new BetterList<Vector3>();

	// Token: 0x04000763 RID: 1891
	private BetterList<Vector4> mTans = new BetterList<Vector4>();

	// Token: 0x04000764 RID: 1892
	private BetterList<Vector2> mUvs = new BetterList<Vector2>();

	// Token: 0x04000765 RID: 1893
	private BetterList<Color> mCols = new BetterList<Color>();

	// Token: 0x04000766 RID: 1894
	private Transform mTrans;

	// Token: 0x04000767 RID: 1895
	private Camera mCam;

	// Token: 0x04000768 RID: 1896
	private int mLayer = -1;

	// Token: 0x04000769 RID: 1897
	private bool mDepthChanged;

	// Token: 0x0400076A RID: 1898
	private bool mRebuildAll;

	// Token: 0x0400076B RID: 1899
	private bool mChangedLastFrame;

	// Token: 0x0400076C RID: 1900
	private float mMatrixTime;

	// Token: 0x0400076D RID: 1901
	private Matrix4x4 mWorldToLocal = Matrix4x4.identity;

	// Token: 0x0400076E RID: 1902
	private static float[] mTemp = new float[4];

	// Token: 0x0400076F RID: 1903
	private Vector2 mMin = Vector2.zero;

	// Token: 0x04000770 RID: 1904
	private Vector2 mMax = Vector2.zero;

	// Token: 0x04000771 RID: 1905
	private List<Transform> mRemoved = new List<Transform>();

	// Token: 0x04000772 RID: 1906
	private bool mCheckVisibility;

	// Token: 0x04000773 RID: 1907
	private float mCullTime;

	// Token: 0x04000774 RID: 1908
	private bool mCulled;

	// Token: 0x04000775 RID: 1909
	private static List<UINode> mHierarchy = new List<UINode>();

	// Token: 0x02000211 RID: 529
	public enum DebugInfo
	{
		// Token: 0x04000C08 RID: 3080
		None,
		// Token: 0x04000C09 RID: 3081
		Gizmos,
		// Token: 0x04000C0A RID: 3082
		Geometry
	}
}
