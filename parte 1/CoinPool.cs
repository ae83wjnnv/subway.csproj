using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class CoinPool : MonoBehaviour
{
	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A4B5 File Offset: 0x000086B5
	public static CoinPool Instance
	{
		get
		{
			CoinPool coinPool;
			if ((coinPool = CoinPool.instance) == null)
			{
				coinPool = (CoinPool.instance = Object.FindObjectOfType(typeof(CoinPool)) as CoinPool);
			}
			return coinPool;
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0000A4DA File Offset: 0x000086DA
	public void Awake()
	{
		this.coins = new List<Transform>();
		this.GetCoins();
	}

	// Token: 0x06000258 RID: 600 RVA: 0x0000A4F0 File Offset: 0x000086F0
	private void GetCoins()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.GetComponent<TrackObject>() != null)
			{
				this.coins.Add(transform.transform);
			}
		}
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0000A564 File Offset: 0x00008764
	private Transform MakeNewCoin(int coinIndex)
	{
		Vector3 vector = this.spawnPoint + this.spawnSpacing * (float)coinIndex;
		Transform transform = Object.Instantiate<GameObject>(this.coinPrefab, vector, Quaternion.identity).transform;
		transform.parent = base.transform;
		this.coins.Add(transform);
		return transform;
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0000A5BC File Offset: 0x000087BC
	public Transform GetCoin()
	{
		Transform transform;
		if (this.coins.Count > 0)
		{
			transform = this.coins[0];
		}
		else
		{
			transform = this.MakeNewCoin(this.coins.Count);
			this.coinWarning = true;
		}
		this.coins.Remove(transform);
		GameObject gameObject = transform.gameObject;
		if (!gameObject.active)
		{
			gameObject.SetActiveRecursively(true);
		}
		this.numberOfActiveCoins++;
		this.numberOfActiveCoins_high = Mathf.Max(this.numberOfActiveCoins_high, this.numberOfActiveCoins);
		return transform;
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0000A648 File Offset: 0x00008848
	public void Put(Transform coin)
	{
		this.Put(new Transform[] { coin });
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000A65C File Offset: 0x0000885C
	public void Put(IEnumerable<Transform> coins)
	{
		foreach (Transform transform in coins)
		{
			transform.parent = base.transform;
			Vector3 position = transform.position;
			position.y = -1000f;
			transform.position = position;
			this.coins.Add(transform);
			this.numberOfActiveCoins--;
		}
	}

	// Token: 0x04000198 RID: 408
	public GameObject coinPrefab;

	// Token: 0x04000199 RID: 409
	private Vector3 spawnPoint = -1000f * Vector3.up;

	// Token: 0x0400019A RID: 410
	private Vector3 spawnSpacing = -20f * Vector3.right;

	// Token: 0x0400019B RID: 411
	private List<Transform> coins;

	// Token: 0x0400019C RID: 412
	private bool coinWarning;

	// Token: 0x0400019D RID: 413
	private static CoinPool instance;

	// Token: 0x0400019E RID: 414
	private int numberOfActiveCoins;

	// Token: 0x0400019F RID: 415
	private int numberOfActiveCoins_high;
}
