using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000144 RID: 324
[AddComponentMenu("NGUI/Internal/Update Manager")]
[ExecuteInEditMode]
public class UpdateManager : MonoBehaviour
{
	// Token: 0x06000997 RID: 2455 RVA: 0x00034231 File Offset: 0x00032431
	private static int Compare(UpdateManager.UpdateEntry a, UpdateManager.UpdateEntry b)
	{
		if (a.index < b.index)
		{
			return 1;
		}
		if (a.index > b.index)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x00034254 File Offset: 0x00032454
	private static void CreateInstance()
	{
		if (UpdateManager.mInst == null)
		{
			UpdateManager.mInst = Object.FindObjectOfType(typeof(UpdateManager)) as UpdateManager;
			if (UpdateManager.mInst == null && Application.isPlaying)
			{
				GameObject gameObject = new GameObject("_UpdateManager");
				Object.DontDestroyOnLoad(gameObject);
				UpdateManager.mInst = gameObject.AddComponent<UpdateManager>();
			}
		}
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x000342B8 File Offset: 0x000324B8
	private void UpdateList(List<UpdateManager.UpdateEntry> list, float delta)
	{
		int i = list.Count;
		while (i > 0)
		{
			UpdateManager.UpdateEntry updateEntry = list[--i];
			if (updateEntry.isMonoBehaviour)
			{
				if (updateEntry.mb == null)
				{
					list.RemoveAt(i);
					continue;
				}
				if (!updateEntry.mb.enabled || !updateEntry.mb.gameObject.active)
				{
					continue;
				}
			}
			updateEntry.func(delta);
		}
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x00034328 File Offset: 0x00032528
	private void Start()
	{
		if (Application.isPlaying)
		{
			this.mTime = Time.realtimeSinceStartup;
			base.StartCoroutine(this.CoroutineFunction());
		}
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x00034349 File Offset: 0x00032549
	private void OnApplicationQuit()
	{
		Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x00034356 File Offset: 0x00032556
	private void Update()
	{
		if (UpdateManager.mInst != this)
		{
			NGUITools.Destroy(base.gameObject);
			return;
		}
		this.UpdateList(this.mOnUpdate, Time.deltaTime);
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x00034382 File Offset: 0x00032582
	private void LateUpdate()
	{
		this.UpdateList(this.mOnLate, Time.deltaTime);
		if (!Application.isPlaying)
		{
			this.CoroutineUpdate();
		}
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x000343A4 File Offset: 0x000325A4
	private bool CoroutineUpdate()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.mTime;
		if (num < 0.001f)
		{
			return true;
		}
		this.mTime = realtimeSinceStartup;
		this.UpdateList(this.mOnCoro, num);
		bool isPlaying = Application.isPlaying;
		int i = this.mDest.size;
		while (i > 0)
		{
			UpdateManager.DestroyEntry destroyEntry = this.mDest.buffer[--i];
			if (!isPlaying || destroyEntry.time < this.mTime)
			{
				if (destroyEntry.obj != null)
				{
					NGUITools.Destroy(destroyEntry.obj);
					destroyEntry.obj = null;
				}
				this.mDest.RemoveAt(i);
			}
		}
		if (this.mOnUpdate.Count == 0 && this.mOnLate.Count == 0 && this.mOnCoro.Count == 0 && this.mDest.size == 0)
		{
			NGUITools.Destroy(base.gameObject);
			return false;
		}
		return true;
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0003448C File Offset: 0x0003268C
	private IEnumerator CoroutineFunction()
	{
		while (Application.isPlaying && this.CoroutineUpdate())
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x0003449C File Offset: 0x0003269C
	private void Add(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func, List<UpdateManager.UpdateEntry> list)
	{
		int i = 0;
		int count = list.Count;
		while (i < count)
		{
			if (list[i].func == func)
			{
				return;
			}
			i++;
		}
		list.Add(new UpdateManager.UpdateEntry
		{
			index = updateOrder,
			func = func,
			mb = mb,
			isMonoBehaviour = (mb != null)
		});
		if (updateOrder != 0)
		{
			list.Sort(new Comparison<UpdateManager.UpdateEntry>(UpdateManager.Compare));
		}
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x00034519 File Offset: 0x00032719
	public static void AddUpdate(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func)
	{
		UpdateManager.CreateInstance();
		UpdateManager.mInst.Add(mb, updateOrder, func, UpdateManager.mInst.mOnUpdate);
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x00034537 File Offset: 0x00032737
	public static void AddLateUpdate(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func)
	{
		UpdateManager.CreateInstance();
		UpdateManager.mInst.Add(mb, updateOrder, func, UpdateManager.mInst.mOnLate);
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x00034555 File Offset: 0x00032755
	public static void AddCoroutine(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func)
	{
		UpdateManager.CreateInstance();
		UpdateManager.mInst.Add(mb, updateOrder, func, UpdateManager.mInst.mOnCoro);
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x00034574 File Offset: 0x00032774
	public static void AddDestroy(Object obj, float delay)
	{
		if (obj == null)
		{
			return;
		}
		if (!Application.isPlaying)
		{
			Object.DestroyImmediate(obj);
			return;
		}
		if (delay > 0f)
		{
			UpdateManager.CreateInstance();
			UpdateManager.DestroyEntry destroyEntry = new UpdateManager.DestroyEntry();
			destroyEntry.obj = obj;
			destroyEntry.time = Time.realtimeSinceStartup + delay;
			UpdateManager.mInst.mDest.Add(destroyEntry);
			return;
		}
		Object.Destroy(obj);
	}

	// Token: 0x0400084E RID: 2126
	private static UpdateManager mInst;

	// Token: 0x0400084F RID: 2127
	private List<UpdateManager.UpdateEntry> mOnUpdate = new List<UpdateManager.UpdateEntry>();

	// Token: 0x04000850 RID: 2128
	private List<UpdateManager.UpdateEntry> mOnLate = new List<UpdateManager.UpdateEntry>();

	// Token: 0x04000851 RID: 2129
	private List<UpdateManager.UpdateEntry> mOnCoro = new List<UpdateManager.UpdateEntry>();

	// Token: 0x04000852 RID: 2130
	private BetterList<UpdateManager.DestroyEntry> mDest = new BetterList<UpdateManager.DestroyEntry>();

	// Token: 0x04000853 RID: 2131
	private float mTime;

	// Token: 0x02000221 RID: 545
	public class UpdateEntry
	{
		// Token: 0x04000C4E RID: 3150
		public int index;

		// Token: 0x04000C4F RID: 3151
		public UpdateManager.OnUpdate func;

		// Token: 0x04000C50 RID: 3152
		public MonoBehaviour mb;

		// Token: 0x04000C51 RID: 3153
		public bool isMonoBehaviour;
	}

	// Token: 0x02000222 RID: 546
	public class DestroyEntry
	{
		// Token: 0x04000C52 RID: 3154
		public Object obj;

		// Token: 0x04000C53 RID: 3155
		public float time;
	}

	// Token: 0x02000223 RID: 547
	// (Invoke) Token: 0x06000C8F RID: 3215
	public delegate void OnUpdate(float delta);
}
