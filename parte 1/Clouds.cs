using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class Clouds : MonoBehaviour
{
	// Token: 0x06000224 RID: 548 RVA: 0x00009550 File Offset: 0x00007750
	private void Start()
	{
		for (int i = 0; i < this.numberOfClouds; i++)
		{
			float num = this.skyLength * (float)i / (float)this.numberOfClouds;
			GameObject gameObject = Object.Instantiate<GameObject>(this.cloudPrefab);
			Vector3 vector = Quaternion.Euler(0f, 0f, Random.Range(-45f, 45f)) * (Vector3.up * this.cloudDistance + Vector3.forward * num);
			gameObject.transform.position = vector;
			gameObject.transform.localScale = Vector3.one * this.cloudSize;
		}
	}

	// Token: 0x0400015B RID: 347
	public float skyLength = 1700f;

	// Token: 0x0400015C RID: 348
	public float cloudDistance = 200f;

	// Token: 0x0400015D RID: 349
	public int numberOfClouds = 10;

	// Token: 0x0400015E RID: 350
	public float cloudSize = 50f;

	// Token: 0x0400015F RID: 351
	public GameObject cloudPrefab;
}
