using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class CoinLine : MonoBehaviour
{
	// Token: 0x06000244 RID: 580 RVA: 0x00009FB4 File Offset: 0x000081B4
	private void Awake()
	{
		TrackObject component = base.GetComponent<TrackObject>();
		component.OnActivate = (TrackObject.OnActivateDelegate)Delegate.Combine(component.OnActivate, new TrackObject.OnActivateDelegate(this.OnActivate));
		component.OnDeactivate = (TrackObject.OnDeactivateDelegate)Delegate.Combine(component.OnDeactivate, new TrackObject.OnDeactivateDelegate(this.OnDeactivate));
		this.coinPool = CoinPool.Instance;
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0000A018 File Offset: 0x00008218
	private void OnActivate()
	{
		for (float num = 0f; num < this.length; num += this.coinSpacing)
		{
			Transform coin = this.coinPool.GetCoin();
			coin.parent = base.transform;
			coin.position = base.transform.position + base.transform.forward * num;
			TrackObject component = coin.GetComponent<TrackObject>();
			if (component != null)
			{
				component.OnActivate();
			}
			this.activeCoins.Add(coin);
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0000A0A8 File Offset: 0x000082A8
	private void OnDeactivate()
	{
		foreach (Transform transform in this.activeCoins)
		{
			transform.GetComponent<TrackObject>().OnDeactivate();
		}
		this.coinPool.Put(this.activeCoins);
		this.activeCoins.Clear();
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0000A120 File Offset: 0x00008320
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(base.transform.position, base.transform.position + base.transform.forward * this.length);
		for (float num = 0f; num < this.length; num += this.coinSpacing)
		{
			Gizmos.DrawSphere(base.transform.position + base.transform.forward * num, 1f);
		}
	}

	// Token: 0x04000183 RID: 387
	public float length = 100f;

	// Token: 0x04000184 RID: 388
	public float coinSpacing = 15f;

	// Token: 0x04000185 RID: 389
	private CoinPool coinPool;

	// Token: 0x04000186 RID: 390
	private List<Transform> activeCoins = new List<Transform>();
}
