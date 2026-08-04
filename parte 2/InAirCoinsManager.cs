using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class InAirCoinsManager : MonoBehaviour
{
	// Token: 0x060003D1 RID: 977 RVA: 0x0001114D File Offset: 0x0000F34D
	public void Awake()
	{
		this.jetpack = Jetpack.Instance;
		this.track = Track.Instance;
		this.coinPool = CoinPool.Instance;
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00011170 File Offset: 0x0000F370
	public void Spawn(float startZ, float length, float height)
	{
		this.curve = new AnimationCurve();
		int num = 1;
		for (float num2 = startZ; num2 < startZ + length; num2 += this.jetpack.characterChangeTrackLength + this.stayInTrackDistance)
		{
			this.curve.AddKey(new Keyframe(num2, this.track.GetTrackX(num)));
			this.curve.AddKey(new Keyframe(num2 + this.stayInTrackDistance, this.track.GetTrackX(num)));
			num = Mathf.Clamp(num + Random.Range(-1, 2), 0, this.track.numberOfTracks - 1);
			this.curve.AddKey(new Keyframe(num2 + this.stayInTrackDistance + this.jetpack.characterChangeTrackLength, this.track.GetTrackX(num)));
		}
		base.StartCoroutine(this.MoveCoins(startZ, length, height));
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00011250 File Offset: 0x0000F450
	private IEnumerator MoveCoins(float StartZ, float length, float height)
	{
		float z = StartZ;
		while (z < StartZ + length)
		{
			Transform coin = this.coinPool.GetCoin();
			coin.position = Vector3.up * height + this.track.GetPosition(this.curve.Evaluate(z), z);
			coin.GetComponent<TrackObject>().Activate();
			z += this.coinDistance;
			this.coins.Add(coin);
			yield return null;
		}
		yield break;
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x00011274 File Offset: 0x0000F474
	public void ReleaseCoins()
	{
		this.coinPool.Put(this.coins);
		this.coins.Clear();
	}

	// Token: 0x04000323 RID: 803
	public GameObject coinPrefab;

	// Token: 0x04000324 RID: 804
	public int numberOfCoins = 200;

	// Token: 0x04000325 RID: 805
	public float stayInTrackDistance = 60f;

	// Token: 0x04000326 RID: 806
	public float coinDistance = 30f;

	// Token: 0x04000327 RID: 807
	private List<Transform> coins = new List<Transform>();

	// Token: 0x04000328 RID: 808
	private AnimationCurve curve;

	// Token: 0x04000329 RID: 809
	private Track track;

	// Token: 0x0400032A RID: 810
	private Jetpack jetpack;

	// Token: 0x0400032B RID: 811
	private CoinPool coinPool;
}
