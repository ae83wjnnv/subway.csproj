using System;
using UnityEngine;

// Token: 0x02000143 RID: 323
public abstract class UIWidget : MonoBehaviour
{
	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06000971 RID: 2417 RVA: 0x00033A09 File Offset: 0x00031C09
	// (set) Token: 0x06000972 RID: 2418 RVA: 0x00033A11 File Offset: 0x00031C11
	public Color color
	{
		get
		{
			return this.mColor;
		}
		set
		{
			if (this.mColor != value)
			{
				this.mColor = value;
				this.mChanged = true;
			}
		}
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000973 RID: 2419 RVA: 0x00033A2F File Offset: 0x00031C2F
	// (set) Token: 0x06000974 RID: 2420 RVA: 0x00033A3C File Offset: 0x00031C3C
	public float alpha
	{
		get
		{
			return this.mColor.a;
		}
		set
		{
			Color color = this.mColor;
			color.a = value;
			this.color = color;
		}
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06000975 RID: 2421 RVA: 0x00033A5F File Offset: 0x00031C5F
	// (set) Token: 0x06000976 RID: 2422 RVA: 0x00033A67 File Offset: 0x00031C67
	public UIWidget.Pivot pivot
	{
		get
		{
			return this.mPivot;
		}
		set
		{
			if (this.mPivot != value)
			{
				this.mPivot = value;
				this.mChanged = true;
			}
		}
	}

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06000977 RID: 2423 RVA: 0x00033A80 File Offset: 0x00031C80
	// (set) Token: 0x06000978 RID: 2424 RVA: 0x00033A88 File Offset: 0x00031C88
	public int depth
	{
		get
		{
			return this.mDepth;
		}
		set
		{
			if (this.mDepth != value)
			{
				this.mDepth = value;
				if (this.mPanel != null)
				{
					this.mPanel.MarkMaterialAsChanged(this.material, true);
				}
			}
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06000979 RID: 2425 RVA: 0x00033ABA File Offset: 0x00031CBA
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

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x0600097A RID: 2426 RVA: 0x00033ADC File Offset: 0x00031CDC
	// (set) Token: 0x0600097B RID: 2427 RVA: 0x00033AE4 File Offset: 0x00031CE4
	public virtual Material material
	{
		get
		{
			return this.mMat;
		}
		set
		{
			if (this.mMat != value)
			{
				if (this.mMat != null && this.mPanel != null)
				{
					this.mPanel.RemoveWidget(this);
				}
				this.mPanel = null;
				this.mMat = value;
				this.mTex = null;
				if (this.mMat != null)
				{
					this.CreatePanel();
				}
			}
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x0600097C RID: 2428 RVA: 0x00033B50 File Offset: 0x00031D50
	public Texture mainTexture
	{
		get
		{
			if (this.mTex == null)
			{
				Material material = this.material;
				if (material != null)
				{
					this.mTex = material.mainTexture;
				}
			}
			return this.mTex;
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x0600097D RID: 2429 RVA: 0x00033B8D File Offset: 0x00031D8D
	// (set) Token: 0x0600097E RID: 2430 RVA: 0x00033BA9 File Offset: 0x00031DA9
	public UIPanel panel
	{
		get
		{
			if (this.mPanel == null)
			{
				this.CreatePanel();
			}
			return this.mPanel;
		}
		set
		{
			this.mPanel = value;
		}
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x0600097F RID: 2431 RVA: 0x00033BB2 File Offset: 0x00031DB2
	// (set) Token: 0x06000980 RID: 2432 RVA: 0x00033BBA File Offset: 0x00031DBA
	public int visibleFlag
	{
		get
		{
			return this.mVisibleFlag;
		}
		set
		{
			this.mVisibleFlag = value;
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06000981 RID: 2433 RVA: 0x00033BC4 File Offset: 0x00031DC4
	public virtual Vector2 pivotOffset
	{
		get
		{
			Vector2 zero = Vector2.zero;
			if (this.mPivot == UIWidget.Pivot.Top || this.mPivot == UIWidget.Pivot.Center || this.mPivot == UIWidget.Pivot.Bottom)
			{
				zero.x = -0.5f;
			}
			else if (this.mPivot == UIWidget.Pivot.TopRight || this.mPivot == UIWidget.Pivot.Right || this.mPivot == UIWidget.Pivot.BottomRight)
			{
				zero.x = -1f;
			}
			if (this.mPivot == UIWidget.Pivot.Left || this.mPivot == UIWidget.Pivot.Center || this.mPivot == UIWidget.Pivot.Right)
			{
				zero.y = 0.5f;
			}
			else if (this.mPivot == UIWidget.Pivot.BottomLeft || this.mPivot == UIWidget.Pivot.Bottom || this.mPivot == UIWidget.Pivot.BottomRight)
			{
				zero.y = 1f;
			}
			return zero;
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06000982 RID: 2434 RVA: 0x00033C78 File Offset: 0x00031E78
	[Obsolete("Use 'relativeSize' instead")]
	public Vector2 visibleSize
	{
		get
		{
			return this.relativeSize;
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x06000983 RID: 2435 RVA: 0x00033C80 File Offset: 0x00031E80
	public virtual Vector2 relativeSize
	{
		get
		{
			return Vector2.one;
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x06000984 RID: 2436 RVA: 0x00033C87 File Offset: 0x00031E87
	public virtual bool keepMaterial
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00033C8A File Offset: 0x00031E8A
	public static int CompareFunc(UIWidget left, UIWidget right)
	{
		if (left.mDepth > right.mDepth)
		{
			return 1;
		}
		if (left.mDepth < right.mDepth)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x00033CB0 File Offset: 0x00031EB0
	public virtual void MarkAsChanged()
	{
		this.mChanged = true;
		if (this.mPanel != null && base.enabled && base.gameObject.active && !Application.isPlaying && this.material != null)
		{
			this.mPanel.AddWidget(this);
			this.CheckLayer();
		}
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x00033D10 File Offset: 0x00031F10
	private void CreatePanel()
	{
		if (this.mPanel == null && base.enabled && base.gameObject.active && this.material != null)
		{
			this.mPanel = UIPanel.Find(this.cachedTransform);
			if (this.mPanel != null)
			{
				this.CheckLayer();
				this.mPanel.AddWidget(this);
				this.mChanged = true;
			}
		}
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x00033D88 File Offset: 0x00031F88
	private void CheckLayer()
	{
		if (this.mPanel != null && this.mPanel.gameObject.layer != base.gameObject.layer)
		{
			Debug.LogWarning("You can't place widgets on a layer different than the UIPanel that manages them.\nIf you want to move widgets to a different layer, parent them to a new panel instead.", this);
			base.gameObject.layer = this.mPanel.gameObject.layer;
		}
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x00033DE8 File Offset: 0x00031FE8
	private void CheckParent()
	{
		if (!(this.mPanel != null))
		{
			return;
		}
		bool flag = true;
		Transform transform = this.cachedTransform.parent;
		while (transform != null && !(transform == this.mPanel.cachedTransform))
		{
			if (!this.mPanel.WatchesTransform(transform))
			{
				flag = false;
				break;
			}
			transform = transform.parent;
		}
		if (!flag)
		{
			if (!this.keepMaterial)
			{
				this.material = null;
			}
			this.mPanel = null;
			this.CreatePanel();
		}
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x00033E68 File Offset: 0x00032068
	private void Awake()
	{
		if (base.GetComponents<UIWidget>().Length > 1)
		{
			Debug.LogError("Can't have more than one widget on the same game object.\nDestroying the second one.", this);
			NGUITools.Destroy(this);
			return;
		}
		this.mPlayMode = Application.isPlaying;
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x00033E94 File Offset: 0x00032094
	private void OnEnable()
	{
		this.mChanged = true;
		if (!this.keepMaterial)
		{
			this.mMat = null;
			this.mTex = null;
		}
		if (this.mPanel != null && this.material != null)
		{
			this.mPanel.MarkMaterialAsChanged(this.mMat, false);
		}
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x00033EEC File Offset: 0x000320EC
	private void Start()
	{
		this.OnStart();
		this.CreatePanel();
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x00033EFC File Offset: 0x000320FC
	private void Update()
	{
		this.CheckLayer();
		if (this.mPanel == null)
		{
			this.CreatePanel();
		}
		Vector3 localScale = this.cachedTransform.localScale;
		if (localScale.z != 1f)
		{
			localScale.z = 1f;
			this.mTrans.localScale = localScale;
		}
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x00033F54 File Offset: 0x00032154
	private void OnDisable()
	{
		if (!this.keepMaterial)
		{
			this.material = null;
		}
		else if (this.mPanel != null)
		{
			this.mPanel.RemoveWidget(this);
		}
		this.mPanel = null;
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x00033F88 File Offset: 0x00032188
	private void OnDestroy()
	{
		if (this.mPanel != null)
		{
			this.mPanel.RemoveWidget(this);
			this.mPanel = null;
		}
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x00033FAC File Offset: 0x000321AC
	public bool UpdateGeometry(ref Matrix4x4 worldToPanel, bool parentMoved, bool generateNormals)
	{
		if (this.material == null)
		{
			return false;
		}
		if (this.OnUpdate() || this.mChanged)
		{
			this.mChanged = false;
			this.mGeom.Clear();
			this.OnFill(this.mGeom.verts, this.mGeom.uvs, this.mGeom.cols);
			if (this.mGeom.hasVertices)
			{
				Vector3 vector = this.pivotOffset;
				Vector2 relativeSize = this.relativeSize;
				vector.x *= relativeSize.x;
				vector.y *= relativeSize.y;
				this.mGeom.ApplyOffset(vector);
				this.mGeom.ApplyTransform(worldToPanel * this.cachedTransform.localToWorldMatrix, generateNormals);
			}
			return true;
		}
		if (this.mGeom.hasVertices && parentMoved)
		{
			this.mGeom.ApplyTransform(worldToPanel * this.cachedTransform.localToWorldMatrix, generateNormals);
		}
		return false;
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x000340B9 File Offset: 0x000322B9
	public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color> c, BetterList<Vector3> n, BetterList<Vector4> t)
	{
		this.mGeom.WriteToBuffers(v, u, c, n, t);
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x000340D0 File Offset: 0x000322D0
	public virtual void MakePixelPerfect()
	{
		Vector3 localScale = this.cachedTransform.localScale;
		int num = Mathf.RoundToInt(localScale.x);
		int num2 = Mathf.RoundToInt(localScale.y);
		localScale.x = (float)num;
		localScale.y = (float)num2;
		localScale.z = 1f;
		Vector3 localPosition = this.cachedTransform.localPosition;
		localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
		if (num % 2 == 1 && (this.pivot == UIWidget.Pivot.Top || this.pivot == UIWidget.Pivot.Center || this.pivot == UIWidget.Pivot.Bottom))
		{
			localPosition.x = Mathf.Floor(localPosition.x) + 0.5f;
		}
		else
		{
			localPosition.x = Mathf.Round(localPosition.x);
		}
		if (num2 % 2 == 1 && (this.pivot == UIWidget.Pivot.Left || this.pivot == UIWidget.Pivot.Center || this.pivot == UIWidget.Pivot.Right))
		{
			localPosition.y = Mathf.Ceil(localPosition.y) - 0.5f;
		}
		else
		{
			localPosition.y = Mathf.Round(localPosition.y);
		}
		this.cachedTransform.localPosition = localPosition;
		this.cachedTransform.localScale = localScale;
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x000341F0 File Offset: 0x000323F0
	protected virtual void OnStart()
	{
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x000341F2 File Offset: 0x000323F2
	public virtual bool OnUpdate()
	{
		return false;
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x000341F5 File Offset: 0x000323F5
	public virtual void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color> cols)
	{
	}

	// Token: 0x04000840 RID: 2112
	[SerializeField]
	[HideInInspector]
	private Material mMat;

	// Token: 0x04000841 RID: 2113
	[SerializeField]
	[HideInInspector]
	private Color mColor = Color.white;

	// Token: 0x04000842 RID: 2114
	[HideInInspector]
	[SerializeField]
	private UIWidget.Pivot mPivot = UIWidget.Pivot.Center;

	// Token: 0x04000843 RID: 2115
	[HideInInspector]
	[SerializeField]
	private int mDepth;

	// Token: 0x04000844 RID: 2116
	private Transform mTrans;

	// Token: 0x04000845 RID: 2117
	private Texture mTex;

	// Token: 0x04000846 RID: 2118
	private UIPanel mPanel;

	// Token: 0x04000847 RID: 2119
	protected bool mChanged = true;

	// Token: 0x04000848 RID: 2120
	protected bool mPlayMode = true;

	// Token: 0x04000849 RID: 2121
	private Vector3 mDiffPos;

	// Token: 0x0400084A RID: 2122
	private Quaternion mDiffRot;

	// Token: 0x0400084B RID: 2123
	private Vector3 mDiffScale;

	// Token: 0x0400084C RID: 2124
	private int mVisibleFlag = -1;

	// Token: 0x0400084D RID: 2125
	private UIGeometry mGeom = new UIGeometry();

	// Token: 0x02000220 RID: 544
	public enum Pivot
	{
		// Token: 0x04000C45 RID: 3141
		TopLeft,
		// Token: 0x04000C46 RID: 3142
		Top,
		// Token: 0x04000C47 RID: 3143
		TopRight,
		// Token: 0x04000C48 RID: 3144
		Left,
		// Token: 0x04000C49 RID: 3145
		Center,
		// Token: 0x04000C4A RID: 3146
		Right,
		// Token: 0x04000C4B RID: 3147
		BottomLeft,
		// Token: 0x04000C4C RID: 3148
		Bottom,
		// Token: 0x04000C4D RID: 3149
		BottomRight
	}
}
