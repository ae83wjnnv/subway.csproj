using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class CoinPlaceholder : MonoBehaviour
{
	// Token: 0x06000252 RID: 594 RVA: 0x0000A3B0 File Offset: 0x000085B0
	private void Awake()
	{
		this.coinPool = CoinPool.Instance;
		TrackObject trackObject = base.GetComponent<TrackObject>() ?? base.gameObject.AddComponent<TrackObject>();
		trackObject.OnActivate = (TrackObject.OnActivateDelegate)Delegate.Combine(trackObject.OnActivate, new TrackObject.OnActivateDelegate(this.OnActivate));
		trackObject.OnDeactivate = (TrackObject.OnDeactivateDelegate)Delegate.Combine(trackObject.OnDeactivate, new TrackObject.OnDeactivateDelegate(this.OnDeactivate));
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0000A420 File Offset: 0x00008620
	public void OnActivate()
	{
		this.coin = this.coinPool.GetCoin();
		this.coin.parent = base.transform;
		this.coin.position = base.transform.position;
		this.coin.GetComponent<TrackObject>().Activate();
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0000A475 File Offset: 0x00008675
	public void OnDeactivate()
	{
		if (this.coin != null)
		{
			this.coin.GetComponent<TrackObject>().Deactivate();
			this.coinPool.Put(this.coin);
			this.coin = null;
		}
	}

	// Token: 0x04000196 RID: 406
	private CoinPool coinPool;

	// Token: 0x04000197 RID: 407
	private Transform coin;
}
