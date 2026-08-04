using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class TrackChunk : MonoBehaviour
{
	// Token: 0x06000654 RID: 1620 RVA: 0x0001FA6C File Offset: 0x0001DC6C
	public void Awake()
	{
		this.objects = base.GetComponentsInChildren<TrackObject>(true);
		if (!this.zMaximumActive)
		{
			this.zMaximum = float.MaxValue;
		}
		TrackChunkCollection.AddToChunks(this);
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x0001FA94 File Offset: 0x0001DC94
	public void Deactivate()
	{
		TrackObject[] array = this.objects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Deactivate();
		}
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x0001FAC0 File Offset: 0x0001DCC0
	public void DeactivateObstacles(float maxZ)
	{
		this.wasDisabledDueToHoverBoard = true;
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			this.DeactiveObstaclesRecursive(transform, maxZ);
		}
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x0001FB24 File Offset: 0x0001DD24
	private void DeactiveObstaclesRecursive(Transform target, float maxZ)
	{
		float num = ((!(target.GetComponent<Collider>() != null)) ? target.transform.position.z : target.GetComponent<Collider>().bounds.min.z);
		if (target.GetComponent<SnapObject>() == null)
		{
			foreach (object obj in target)
			{
				Transform transform = (Transform)obj;
				this.DeactiveObstaclesRecursive(transform, maxZ);
			}
			return;
		}
		if (num < maxZ && target.gameObject.layer != 16)
		{
			Vector3 localPosition = target.localPosition;
			if (!this.hiddenObstacles.ContainsKey(target))
			{
				this.hiddenObstacles.Add(target, localPosition);
			}
			target.localPosition = new Vector3(localPosition.x, -1000f, localPosition.z);
		}
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0001FC1C File Offset: 0x0001DE1C
	public void RestoreHiddenObstacles()
	{
		foreach (KeyValuePair<Transform, Vector3> keyValuePair in this.hiddenObstacles)
		{
			keyValuePair.Key.localPosition = keyValuePair.Value;
		}
		this.hiddenObstacles.Clear();
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0001FC88 File Offset: 0x0001DE88
	public float GetLastCheckPoint(float characterZ)
	{
		TrackChunk.TrackCheckPoint trackCheckPoint = (from c in this.CheckPoints
			orderby c.Z
			where c.Z <= characterZ
			select c).LastOrDefault<TrackChunk.TrackCheckPoint>();
		if (trackCheckPoint == null)
		{
			Debug.Log(" No checkpoint found in track chunk");
			return 0f;
		}
		return trackCheckPoint.Z;
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x0001FCFC File Offset: 0x0001DEFC
	private void DrawCheckPointGizmos()
	{
		foreach (TrackChunk.TrackCheckPoint trackCheckPoint in this.CheckPoints)
		{
			Vector3 position = base.transform.position;
			position.z = trackCheckPoint.Z;
			Gizmos.DrawSphere(position + Vector3.up * 5f, 5f);
		}
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x0001FD80 File Offset: 0x0001DF80
	public void OnDrawGizmos()
	{
		this.DrawCheckPointGizmos();
	}

	// Token: 0x04000564 RID: 1380
	public float zSize = 40f;

	// Token: 0x04000565 RID: 1381
	public int probability = 1;

	// Token: 0x04000566 RID: 1382
	public float zMinimum;

	// Token: 0x04000567 RID: 1383
	public bool zMaximumActive;

	// Token: 0x04000568 RID: 1384
	public float zMaximum;

	// Token: 0x04000569 RID: 1385
	public List<TrackChunk.TrackCheckPoint> CheckPoints;

	// Token: 0x0400056A RID: 1386
	public TrackObject[] objects;

	// Token: 0x0400056B RID: 1387
	public bool wasDisabledDueToHoverBoard;

	// Token: 0x0400056C RID: 1388
	public bool isTutorial;

	// Token: 0x0400056D RID: 1389
	private Dictionary<Transform, Vector3> hiddenObstacles = new Dictionary<Transform, Vector3>();

	// Token: 0x020001EA RID: 490
	[Serializable]
	public class TrackCheckPoint
	{
		// Token: 0x04000B74 RID: 2932
		public int TrackNumber;

		// Token: 0x04000B75 RID: 2933
		public float Z;
	}
}
