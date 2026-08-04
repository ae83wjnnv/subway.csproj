using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class ColliderQueues : MonoBehaviour
{
	// Token: 0x06000264 RID: 612 RVA: 0x0000ABBC File Offset: 0x00008DBC
	public void Activate(Collider collider)
	{
		bool flag = this.activationQueue.Count == 0;
		this.activationQueue.Enqueue(collider);
		this.activatedQueued++;
		if (flag)
		{
			base.StartCoroutine(this.DequeueCoroutine(this.activationQueue, delegate(Collider c)
			{
				c.enabled = true;
				this.activated++;
			}));
		}
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0000AC14 File Offset: 0x00008E14
	public void Deactivate(Collider collider)
	{
		bool flag = this.deactivationQueue.Count == 0;
		this.deactivationQueue.Enqueue(collider);
		this.deactivatedQueued++;
		if (flag)
		{
			base.StartCoroutine(this.DequeueCoroutine(this.deactivationQueue, delegate(Collider c)
			{
				c.enabled = false;
				this.deactivated++;
			}));
		}
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0000AC6A File Offset: 0x00008E6A
	private IEnumerator DequeueCoroutine(Queue<Collider> queue, Action<Collider> action)
	{
		int num = this.dequeueBatchSize;
		while (queue.Count > 0)
		{
			if (num == 0)
			{
				yield return 0;
				num = this.dequeueBatchSize;
			}
			Collider collider = queue.Dequeue();
			action(collider);
			num--;
		}
		yield break;
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0000AC88 File Offset: 0x00008E88
	public void Flush()
	{
		foreach (Collider collider in this.activationQueue)
		{
			collider.enabled = true;
			this.activated++;
		}
		this.activationQueue.Clear();
		foreach (Collider collider2 in this.deactivationQueue)
		{
			collider2.enabled = false;
			this.deactivated++;
		}
		this.deactivationQueue.Clear();
	}

	// Token: 0x040001A5 RID: 421
	private Queue<Collider> activationQueue = new Queue<Collider>();

	// Token: 0x040001A6 RID: 422
	private Queue<Collider> deactivationQueue = new Queue<Collider>();

	// Token: 0x040001A7 RID: 423
	private int dequeueBatchSize = 5;

	// Token: 0x040001A8 RID: 424
	public int activated;

	// Token: 0x040001A9 RID: 425
	public int activatedQueued;

	// Token: 0x040001AA RID: 426
	public int deactivated;

	// Token: 0x040001AB RID: 427
	public int deactivatedQueued;
}
