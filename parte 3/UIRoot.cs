using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000126 RID: 294
[AddComponentMenu("NGUI/UI/Root")]
[ExecuteInEditMode]
public class UIRoot : MonoBehaviour
{
	// Token: 0x06000898 RID: 2200 RVA: 0x0002DE4A File Offset: 0x0002C04A
	private void Awake()
	{
		UIRoot.mRoots.Add(this);
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x0002DE57 File Offset: 0x0002C057
	private void OnDestroy()
	{
		UIRoot.mRoots.Remove(this);
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x0002DE68 File Offset: 0x0002C068
	private void Start()
	{
		this.mTrans = base.transform;
		UIOrthoCamera componentInChildren = base.GetComponentInChildren<UIOrthoCamera>();
		if (componentInChildren != null)
		{
			Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", componentInChildren);
			Camera component = componentInChildren.gameObject.GetComponent<Camera>();
			componentInChildren.enabled = false;
			if (component != null)
			{
				component.orthographicSize = 1f;
			}
		}
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x0002DEC4 File Offset: 0x0002C0C4
	private void Update()
	{
		this.manualHeight = Mathf.Max(2, (!this.automatic) ? this.manualHeight : Screen.height);
		float num = 2f / (float)this.manualHeight;
		Vector3 localScale = this.mTrans.localScale;
		if (!Mathf.Approximately(localScale.x, num) || !Mathf.Approximately(localScale.y, num) || !Mathf.Approximately(localScale.z, num))
		{
			this.mTrans.localScale = new Vector3(num, num, num);
		}
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x0002DF4C File Offset: 0x0002C14C
	public static void Broadcast(string funcName)
	{
		int i = 0;
		int count = UIRoot.mRoots.Count;
		while (i < count)
		{
			UIRoot uiroot = UIRoot.mRoots[i];
			if (uiroot != null)
			{
				uiroot.BroadcastMessage(funcName, SendMessageOptions.DontRequireReceiver);
			}
			i++;
		}
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x0002DF90 File Offset: 0x0002C190
	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
			return;
		}
		int i = 0;
		int count = UIRoot.mRoots.Count;
		while (i < count)
		{
			UIRoot uiroot = UIRoot.mRoots[i];
			if (uiroot != null)
			{
				uiroot.BroadcastMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
			}
			i++;
		}
	}

	// Token: 0x04000795 RID: 1941
	private static List<UIRoot> mRoots = new List<UIRoot>();

	// Token: 0x04000796 RID: 1942
	public bool automatic = true;

	// Token: 0x04000797 RID: 1943
	public int manualHeight = 800;

	// Token: 0x04000798 RID: 1944
	private Transform mTrans;
}
